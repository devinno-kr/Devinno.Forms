using Devinno.Forms.Dialogs;
using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Containers
{
    public class DvSplitterTableLayoutPanel : DvTableLayoutPanel
    {
        #region Properties
        #region ParentForm
        public Form ParentForm
        {
            get
            {
                Form ret = null;
                var Parent = this.Parent;
                while (Parent != null)
                {
                    if (Parent is Form) ret = Parent as Form;
                    Parent = Parent.Parent;
                }
                return ret;
            }
        }
        #endregion
        #region DrawSplitter
        public bool DrawSplitter { get; set; }
        #endregion
        #region SplitterSize
        public int SplitterSize { get { return 3; } }
        #endregion
        #region SplitterColor
        private Color cSplitterColor = DvTheme.DefaultTheme.FrameColor;
        public Color SplitterColor
        {
            get { return cSplitterColor; }
            set
            {
                if (cSplitterColor != value)
                {
                    cSplitterColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region UseThemeColor
        private bool bUseThemeColor = true;
        public bool UseThemeColor
        {
            get => bUseThemeColor;
            set
            {
                if (bUseThemeColor != value)
                {
                    bUseThemeColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        List<IdnSplitter> Splitter = new List<IdnSplitter>();
        Timer tmr = new Timer();
        Timer tmr2 = new Timer();
        Size PrevSize = Size.Empty;

        IdnSplitter downSplitter = null;
        Point downPos = Point.Empty;
        int[] downcws = null;
        int[] downrhs = null;
        #endregion

        #region Event
        public event EventHandler SplitterChanged;
        #endregion

        #region Constructor
        public DvSplitterTableLayoutPanel()
        {
            #region Update Style
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
            #endregion
            #region Timer
            tmr.Interval = 10;
            tmr.Tick += (o, s) =>
            {
                if (PrevSize != Size)
                {
                    PrevSize = Size;
                    MakeSplitterBounds();
                    Invalidate();
                }
            };
            if (!DesignMode) tmr.Enabled = true;
            #endregion
        }
        #endregion

        #region Override
        #region OnLayout
        protected override void OnLayout(LayoutEventArgs levent)
        {
            MakeSplitterBounds();
            Invalidate();
            base.OnLayout(levent);
        }
        #endregion
        #region OnResize
        protected override void OnResize(EventArgs eventargs)
        {
            MakeSplitterBounds();
            Invalidate();
            base.OnResize(eventargs);
        }
        #endregion
        #region OnSizeChanged
        protected override void OnSizeChanged(EventArgs e)
        {
            MakeSplitterBounds();
            Invalidate();
            base.OnSizeChanged(e);
        }
        #endregion

        #region OnEnabledChanged
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var SplitterColor = UseThemeColor ? Theme.FrameColor : this.SplitterColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            #endregion
            #region Init
            var p = new Pen(Color.Black, 2);
            var br = new SolidBrush(Color.Black);
            #endregion
            #region Draw
            if (DrawSplitter)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                br.Color = SplitterColor;
                foreach (var sp in Splitter)
                {
                    e.Graphics.FillRectangle(br, sp.Bounds);
                }
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            }
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion

        #region OnMouseLeave
        protected override void OnMouseLeave(EventArgs e)
        {
            Cursor = Cursors.Default;
            base.OnMouseLeave(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            #region Cursor
            var cur = Cursors.Default;
            foreach (var sp in Splitter)
            {
                if (sp.Bounds.Contains(e.Location))
                {
                    if (sp.SplitterType == DvSplitterType.VERTICAL) cur = Cursors.SizeNS;
                    if (sp.SplitterType == DvSplitterType.HORIZON) cur = Cursors.SizeWE;
                }
            }
            this.Cursor = cur;
            #endregion
            ProcessSplitter(e.Location);
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            foreach (var sp in Splitter)
            {
                if (sp.Bounds.Contains(e.Location))
                {
                    downSplitter = sp;
                    downPos = e.Location;
                    downrhs = GetRowHeights();
                    downcws = GetColumnWidths();
                }
            }

            MakeSplitterBounds();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            ProcessSplitter(e.Location);
            downSplitter = null;
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region MakeSplitterBounds
        void MakeSplitterBounds()
        {
            SuspendLayout();

            var lst = new List<IdnSplitter>();
            var cols = GetColumnWidths();
            var rows = GetRowHeights();

            if (cols.Length == ColumnStyles.Count && rows.Length == RowStyles.Count)
            {
                int x = 0, y = 0;
                for (int i = 0; i < cols.Length; i++)
                {
                    var w = cols[i];

                    y = 0;
                    for (int j = 0; j < rows.Length; j++)
                    {
                        var h = rows[j];

                        var rtTop = new Rectangle(x, y - (SplitterSize / 2), w, SplitterSize);
                        var rtLeft = new Rectangle(x - (SplitterSize / 2), y, SplitterSize, h);
                        if (j > 0) lst.Add(new IdnSplitter(rtTop, DvSplitterType.VERTICAL, i, j - 1));
                        if (i > 0) lst.Add(new IdnSplitter(rtLeft, DvSplitterType.HORIZON, i - 1, j));
                        y += h;
                    }
                    x += w;
                }
            }


            Splitter.Clear();
            Splitter.AddRange(lst);

            ResumeLayout();
        }
        #endregion
        #region ProcessSplitter
        void ProcessSplitter(Point e)
        {
            #region Process
            if (downSplitter != null)
            {
                if (downSplitter.SplitterType == DvSplitterType.VERTICAL)
                {
                    #region Init
                    var ri = downSplitter.RowIndex;
                    var rowhs = (int[])downrhs.Clone();
                    var rowst = RowStyles[ri];
                    var rowst2 = ri + 1 < RowStyles.Count ? RowStyles[ri + 1] : null;
                    var rowh = rowhs[ri];
                    var rowh2 = ri + 1 < rowhs.Length ? rowhs[ri + 1] : 0;
                    #endregion
                    #region Calc Resize
                    var gapy = (int)MathTool.Constrain((e.Y - downPos.Y), -rowh + (SplitterSize / 2), rowh2 - (SplitterSize / 2));
                    rowhs[ri] += gapy;
                    rowhs[ri + 1] -= gapy;
                    #endregion
                    #region ResultData
                    var ls = new List<RowStyle>();
                    for (int i = 0; i < rowhs.Length && i < RowStyles.Count; i++)
                    {
                        var cs = RowStyles[i];
                        if (cs.SizeType == SizeType.Absolute) ls.Add(new RowStyle(SizeType.Absolute, rowhs[i]));
                        else if (cs.SizeType == SizeType.Percent) ls.Add(new RowStyle(SizeType.Percent, ((float)rowhs[i] / (float)(rowhs.Sum())) * 100F));
                    }
                    #endregion
                    #region Layout
                    SuspendLayout();
                    RowStyles.Clear();
                    for (int i = 0; i < ls.Count; i++) RowStyles.Add(ls[i]);
                    ResumeLayout();
                    PerformLayout();
                    #endregion
                }
                else if (downSplitter.SplitterType == DvSplitterType.HORIZON)
                {
                    #region Init
                    var ci = downSplitter.ColIndex;
                    var colws = (int[])downcws.Clone();
                    var colst = ColumnStyles[ci];
                    var colst2 = ci + 1 < ColumnStyles.Count ? ColumnStyles[ci + 1] : null;
                    var colw = colws[ci];
                    var colw2 = ci + 1 < colws.Length ? colws[ci + 1] : 0;
                    #endregion
                    #region Calc Resize
                    var gapx = (int)MathTool.Constrain((e.X - downPos.X), -colw + (SplitterSize / 2), colw2 - (SplitterSize / 2));
                    colws[ci] += gapx;
                    colws[ci + 1] -= gapx;
                    #endregion
                    #region ResultData
                    var ls = new List<ColumnStyle>();
                    for (int i = 0; i < colws.Length && i < ColumnStyles.Count; i++)
                    {
                        var cs = ColumnStyles[i];
                        if (cs.SizeType == SizeType.Absolute) ls.Add(new ColumnStyle(SizeType.Absolute, colws[i]));
                        else if (cs.SizeType == SizeType.Percent) ls.Add(new ColumnStyle(SizeType.Percent, ((float)colws[i] / (float)(colws.Sum())) * 100F));
                    }
                    #endregion
                    #region Layout
                    SuspendLayout();
                    ColumnStyles.Clear();
                    for (int i = 0; i < ls.Count; i++) ColumnStyles.Add(ls[i]);
                    var d = ls.Sum(x => x.Width);
                    ResumeLayout();
                    PerformLayout();
                    #endregion
                }
            }
            #endregion
        }
        #endregion
        #endregion
    }

    #region Enum : DvSplitterType
    public enum DvSplitterType { VERTICAL, HORIZON }
    #endregion
    #region Class : IdnSplitter
    public class IdnSplitter
    {
        public Rectangle Bounds { get; private set; }
        public DvSplitterType SplitterType { get; private set; }
        public int RowIndex { get; private set; }
        public int ColIndex { get; private set; }

        public IdnSplitter(Rectangle Bounds, DvSplitterType SplitterType, int ColIndex, int RowIndex)
        {
            this.SplitterType = SplitterType;
            this.Bounds = Bounds;
            this.ColIndex = ColIndex;
            this.RowIndex = RowIndex;
        }
    }
    #endregion
}
