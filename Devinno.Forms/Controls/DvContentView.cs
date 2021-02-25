using Devinno.Collections;
using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvContentView : DvControl
    {
        #region Properties
        #region SelectedColor
        private Color cSelectedColor = DvTheme.DefaultTheme.PointColor;
        public Color SelectedColor
        {
            get => cSelectedColor;
            set
            {
                if (cSelectedColor != value)
                {
                    cSelectedColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region ContentSize
        private Size szContent = new Size(100, 100);
        public Size ContentSize
        {
            get => szContent;
            set
            {
                if (szContent != value) { szContent = value; Invalidate(); }
            }
        }
        #endregion

        #region SelectedItems
        public EventList<DvContent> SelectedItems { get; } = new EventList<DvContent>();
        #endregion

        #region Items
        public EventList<DvContent> Items { get; } = new EventList<DvContent>();
        #endregion

        #region TouchMode
        public bool TouchMode
        {
            get => scroll.TouchMode;
            set
            {
                if (scroll.TouchMode != value)
                {
                    scroll.TouchMode = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Gap
        public int Gap { get; set; } = 3;
        #endregion
        #region AutoArrange
        public bool AutoArrange { get; set; } = true;
        #endregion
        #region TouchAreaSize
        public int TouchAreaSize { get; set; } = 60;
        #endregion
        #region Selectable
        public bool Selectable { get; set; } = false;
        #endregion

        #region ScrollDirection
        public ScrollDirection ScrollDirection
        {
            get => scroll.Direction;
            set
            {
                if (scroll.Direction != value)
                {
                    scroll.Direction = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        private Scroll scroll = new Scroll();
        private List<DvContent> vls = new List<DvContent>();
        private Size szprev;
        private MouseEventArgs evdown;
        private Point? downPoint = null;
        private Point? movePoint = null;
        private double pcw, pch;
        #endregion

        #region Event
        public event EventHandler SelectedChanged;
        #endregion

        #region Constructor
        public DvContentView()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(300, 200);

            scroll.Direction = ScrollDirection;
            scroll.ScrollChanged += (o, s) => this.Invoke(new Action(() => Invalidate()));
            scroll.GetScrollTotal = () => ScrollDirection == ScrollDirection.Vertical ? (vls.Count > 0 ? vls.Max(x => x.Bounds.Bottom) : 0) : (vls.Count > 0 ? vls.Max(x => x.Bounds.Right) : 0);
            scroll.GetScrollTick = () => ScrollDirection == ScrollDirection.Vertical? ContentSize.Height : ContentSize.Width;
            scroll.GetScrollView = () => ScrollDirection == ScrollDirection.Vertical ? (Areas.ContainsKey("rtBox") ? Areas["rtBox"].Height : 0) : (Areas.ContainsKey("rtBox") ? Areas["rtBox"].Width : 0);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var scwh = Convert.ToInt32(Scroll.SC_WH * DpiRatio);
            var gp = Convert.ToInt32(10 * DpiRatio);
            var rtContent = Areas["rtContent"];

            if (ScrollDirection == ScrollDirection.Vertical)
            {
                var twh = TouchMode && Selectable ? TouchAreaSize : 0;
                var rtBox = new Rectangle(rtContent.Left, rtContent.Top, rtContent.Width - twh - scwh - (gp * 2), rtContent.Height); //rtBox.Inflate(-1, -1);
                var rtTouchArea = new Rectangle(rtBox.Right + gp, rtBox.Top, twh, rtBox.Height);
                var rtScroll = new Rectangle(rtTouchArea.Right + gp, rtBox.Top, scwh, rtBox.Height);
                SetArea("rtBox", rtBox);
                SetArea("rtScroll", rtScroll);
                SetArea("rtTouchArea", rtTouchArea);
            }
            else
            {
                var twh = TouchMode && Selectable ? TouchAreaSize : 0;
                var rtBox = new Rectangle(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height - twh - scwh - (gp * 2)); //rtBox.Inflate(-1, -1);
                var rtTouchArea = new Rectangle(rtBox.Left, rtBox.Bottom + gp, rtBox.Width, twh);
                var rtScroll = new Rectangle(rtBox.Left, rtTouchArea.Bottom + gp, rtBox.Width, scwh);
                SetArea("rtBox", rtBox);
                SetArea("rtScroll", rtScroll);
                SetArea("rtTouchArea", rtTouchArea);
            }
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var SelectedColor = UseThemeColor ? Theme.PointColor : this.SelectedColor;
            #endregion
            #region Set
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            #endregion
            #region Init
            var p = new Pen(Color.Black, 2);
            var br = new SolidBrush(Color.Black);
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtBox = Areas["rtBox"];
            var rtScroll = Areas["rtScroll"];
            var rtTouchArea = Areas["rtTouchArea"];
            #endregion
            #region Draw
            #region Items
            e.Graphics.SetClip(new Rectangle(rtBox.X - 1, rtBox.Y - 1, rtBox.Width + 2, rtBox.Height + 2));
            Loop((ls, i, rt, itm) =>
            {
                if (itm.Visible)
                {
                    if (SelectedItems.Contains(itm))
                    {
                        var rtv = new Rectangle(rt.X, rt.Y, rt.Width, rt.Height); rtv.Inflate(Gap, Gap);
                        Theme.DrawBorder(e.Graphics, SelectedColor, BackColor, 3, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                    }
                    itm.Draw(e.Graphics, Theme, rt);
                }
            });
            e.Graphics.ResetClip();
            #endregion
            #region Scroll
            br.Color = Theme.ScrollBarColor;

            Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScroll, RoundType.ALL);
            Theme.DrawBorder(e.Graphics, BackColor.BrightnessTransmit(-Theme.BorderBright), BackColor, 1, rtScroll, RoundType.ALL, BoxDrawOption.BORDER);

            var cCur = Theme.ScrollCursorColor;
            if (scroll.IsScrolling) cCur = Theme.ScrollCursorColor.BrightnessTransmit(0.3);
            else if (scroll.IsTouchMoving) cCur = Theme.PointColor.BrightnessTransmit(0.3);

            var rtcur = scroll.GetScrollCursorRect(rtScroll);
            if (rtcur.HasValue) Theme.DrawBox(e.Graphics, cCur, Theme.ScrollBarColor, rtcur.Value, RoundType.ALL, BoxDrawOption.BORDER);
            #endregion
            #region TouchArea
            if (TouchMode && Selectable)
            {
                var rtTA = new Rectangle(rtTouchArea.X, rtTouchArea.Y, rtTouchArea.Width, rtTouchArea.Height);

                br.Color = Color.FromArgb(30, Color.Black);
                e.Graphics.FillRoundRectangle(br, rtTA, Theme.Corner);

                p.Color = Color.FromArgb(30, Color.White); p.Width = 1;
                e.Graphics.DrawRoundRectangle(p, rtTA, Theme.Corner);

                using (var ft = new Font(Font.FontFamily, 7, FontStyle.Bold))
                {
                    Theme.DrawText(e.Graphics, null, "TOUCH\r\nAREA", ft, p.Color, rtTA);
                }
            }
            #endregion
            #region Drag
            if(Selectable)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                if (downPoint.HasValue && movePoint.HasValue)
                {
                    var rt = MathTool.MakeRectangle(downPoint.Value, movePoint.Value);
                    p.Width = 1; p.Color = Color.Black; p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    e.Graphics.DrawRectangle(p, rt);
                    p.Width = 1; p.Color = Color.White; p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawRectangle(p, rt);
                }
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            }
            #endregion
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion

            base.OnThemeDraw(e, Theme);
        }
        #endregion

        #region OnMouseWheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (TouchMode)
            {
                if(!scroll.IsTouchMoving) scroll.MouseWheel(e);
            }
            else scroll.MouseWheel(e);

            Invalidate();
            base.OnMouseWheel(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            evdown = e;
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtScroll = Areas["rtScroll"];

                Loop((ls, i, rt, itm) =>
                {
                    if (itm.Enabled && itm.Visible && itm.Collision(rt, e.Location)) itm.MouseDown(rt, e.Location);
                });

                scroll.MouseDown(e, Areas["rtScroll"]);
                if (Selectable)
                {
                    if (scroll.TouchMode && CollisionTool.Check(Areas["rtTouchArea"], e.Location)) scroll.TouchDown(e);
                    if (CollisionTool.Check(rtBox, e.Location))
                    {
                        downPoint = e.Location;
                        movePoint = null;
                    }
                }
                else
                {
                    if (scroll.TouchMode && CollisionTool.Check(Areas["rtBox"], e.Location)) scroll.TouchDown(e);
                }
            }
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                scroll.MouseMove(e, Areas["rtScroll"]);
                if (scroll.TouchMode) scroll.TouchMove(e);
                if (scroll.IsScrolling) Invalidate();
                if (scroll.TouchMode && scroll.IsTouchScrolling) Invalidate();

                Loop((ls, i, rt, itm) =>
                {
                    if (itm.Enabled && itm.Visible) itm.MouseMove(rt, e.Location);
                });

                if (Selectable && downPoint.HasValue) movePoint = e.Location;
            }
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtScroll = Areas["rtScroll"];
                var rtTouchArea = Areas["rtTouchArea"];

                scroll.MouseUp(e);
                if (scroll.TouchMode) scroll.TouchUp(e);
            }

            Loop((ls, i, rt, itm) =>
            {
                if (itm.Enabled && itm.Visible) itm.MouseUp(rt, e.Location);
            });

            if (Selectable && downPoint.HasValue)
            {
                if (Math.Abs(downPoint.Value.X - e.X) >= 5 || Math.Abs(downPoint.Value.Y - e.Y) >= 5)
                {
                    var rtdrag = MathTool.MakeRectangle(e.Location, downPoint.Value);
                    #region SelectedClear
                    if ((ModifierKeys & Keys.Shift) != Keys.Shift) { SelectedItems.Clear(); Invalidate(); }
                    #endregion
                    #region Selected
                    bool bChange = false;
                    Loop((ls, i, rt, itm) =>
                    {
                        var v = itm;
                        if (v.Enabled && v.Collision(rt, rtdrag))
                        {
                            bChange = true;
                            SelectedItems.Add(v);
                        }
                    });
                    if (bChange) { SelectedChanged?.Invoke(this, null); Invalidate(); }
                    #endregion
                }
                else
                {
                    #region SelectedClear
                    if ((ModifierKeys & Keys.Shift) != Keys.Shift) { SelectedItems.Clear(); Invalidate(); }
                    #endregion
                    #region Selected
                    bool bChange = false;
                    Loop((ls, i, rt, itm) =>
                    {
                        var v = itm;
                        if (v.Enabled && v.Collision(rt, e.Location))
                        {
                            bChange = true;
                            SelectedItems.Add(v);
                        }
                    });
                    if (bChange) { SelectedChanged?.Invoke(this, null); Invalidate(); }
                    #endregion
                }
                downPoint = movePoint = null;
            }

            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                Loop((ls, i, rt, itm) =>
                {
                    if (itm.Enabled && itm.Visible) itm.MouseDoubleClick(rt, e.Location);
                });

                Invalidate();
            }
            base.OnMouseDoubleClick(e);
        }
        #endregion
        #endregion

        #region Method
        #region Arrange
        public void Arrange()
        {
            LoadAreas(null);
            if (Areas.Count > 1)
            {
                var ls = this.Items.Where(x => x.Visible).ToList();
                var rtBox = Areas["rtBox"];

                var ch = Math.Round((double)(rtBox.Height) / (double)ContentSize.Height);
                var cw = Math.Round((double)(rtBox.Width) / (double)ContentSize.Width);

                if (vls.Count != ls.Count || szprev.Width != this.Size.Width || szprev.Height != this.Size.Height || (ScrollDirection == ScrollDirection.Horizon ? ch != pch : cw != pcw ))
                {
                    #region Arrange
                    if (ScrollDirection == ScrollDirection.Horizon)
                    {
                        //var ch = Math.Round((double)(rtBox.Height) / (double)ContentSize.Height);

                        int ir = 0, ic = 0;
                        vls.Clear();
                        for (int i = 0; i < ls.Count; i++)
                        {
                            var itm = ls[i];

                            itm.ColIndex = ic;
                            itm.RowIndex = ir;

                            vls.Add(itm);

                            var vitm = i + 1 < ls.Count ? ls[i + 1] : null;
                            do
                            {
                                ir++;
                                if (vitm != null && ir + vitm.RowSpan > ch) { ic++; ir = 0; }

                            } while (vitm != null && vls.Where(x => x.Check(ic, ir, vitm.ColSpan, vitm.RowSpan)).Count() > 0);
                        }
                        pch = ch;
                    }
                    else
                    {
                        //var cw = Math.Round((double)(rtBox.Width) / (double)ContentSize.Width);

                        int ir = 0, ic = 0;
                        vls.Clear();
                        for (int i = 0; i < ls.Count; i++)
                        {
                            var itm = ls[i];

                            itm.ColIndex = ic;
                            itm.RowIndex = ir;

                            vls.Add(itm);

                            var vitm = i + 1 < ls.Count ? ls[i + 1] : null;
                            do
                            {
                                ic++;
                                if (vitm != null && ic + vitm.ColSpan > cw) { ir++; ic = 0; }

                            } while (vitm != null && vls.Where(x => x.Check(ic, ir, vitm.ColSpan, vitm.RowSpan)).Count() > 0);
                        }
                        pcw = cw;
                    }
                    #endregion

                    szprev = this.Size;
                }
            }
        }

        void NoArrange()
        {
            if (Areas.Count > 1)
            {
                var ls = this.Items.Where(x => x.Visible).ToList();

                if (vls.Count != ls.Count || szprev.Width != this.Size.Width || szprev.Height != this.Size.Height)
                {
                    vls.Clear();
                    vls.AddRange(ls);
                    szprev = this.Size;
                }
            }
        }
        #endregion
        #region Loop
        private void Loop(Action<List<DvContent>, int, Rectangle, DvContent> Func)
        {
            if (Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var sc = scroll.ScrollPosition;
                var spos = Convert.ToInt32(scroll.ScrollPositionWithOffset);

                var CWRH = ScrollDirection == ScrollDirection.Vertical ? ContentSize.Height : ContentSize.Width;
                var MaxWH = ScrollDirection == ScrollDirection.Vertical ? rtBox.Height : rtBox.Width;

                int cw = (int)Math.Floor((double)(rtBox.Width) / (double)ContentSize.Width);
                int ch = (int)Math.Floor((double)(rtBox.Height) / (double)ContentSize.Height);

                if (AutoArrange) Arrange();
                else NoArrange();

                var ls = vls.Where(itm => CollisionTool.Check(rtBox, new Rectangle(rtBox.X + itm.Bounds.X + (ScrollDirection == ScrollDirection.Horizon ? spos : 0), rtBox.Y + itm.Bounds.Y + (ScrollDirection == ScrollDirection.Vertical ? spos : 0), itm.Bounds.Width, itm.Bounds.Height))).ToList();
                foreach (var itm in ls)
                {
                    var i = vls.IndexOf(itm);
                    var rt = new Rectangle(rtBox.X + itm.Bounds.X + (ScrollDirection == ScrollDirection.Horizon ? spos : 0), rtBox.Y + itm.Bounds.Y + (ScrollDirection == ScrollDirection.Vertical ? spos : 0), itm.Bounds.Width, itm.Bounds.Height);
                    rt.Inflate(-Gap, -Gap);
                    if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                    {
                        Func(vls, i, rt, itm);
                    }
                }
            }
        }
        #endregion
        #endregion
    }
        
    #region class : DvContent
    public abstract class DvContent
    {
        #region Properties
        #region ColIndex
        public int ColIndex { get; set; }
        #endregion
        #region RowIndex
        public int RowIndex { get; set; }
        #endregion
        #region RowSpan
        private int nRowSpan = 1;
        public int RowSpan { get => nRowSpan; set { var n = value; if (n < 1) n = 1; nRowSpan = n; } }
        #endregion
        #region ColSpan
        private int nColSpan = 1;
        public int ColSpan { get => nColSpan; set { var n = value; if (n < 1) n = 1; nColSpan = n; } }
        #endregion

        #region Control
        public DvControl Control { get; private set; }
        #endregion
        #region Selected
        public bool Selected
        {
            get
            {
                if (Control != null && Control is DvContentView) return ((DvContentView)Control).SelectedItems.Contains(this);
                else if (Control != null && Control is DvContentGrid) return ((DvContentGrid)Control).SelectedItems.Contains(this);
                else return false;
            }
            set
            {
                if (Control != null)
                {
                    if (Control is DvContentView)
                    {
                        if (value && !((DvContentView)Control).SelectedItems.Contains(this)) { ((DvContentView)Control).SelectedItems.Add(this); Control.Invalidate(); }
                        if (!value && ((DvContentView)Control).SelectedItems.Contains(this)) { ((DvContentView)Control).SelectedItems.Remove(this); Control.Invalidate(); }
                    }
                    else if (Control is DvContentGrid)
                    {
                        if (value && !((DvContentGrid)Control).SelectedItems.Contains(this)) { ((DvContentGrid)Control).SelectedItems.Add(this); Control.Invalidate(); }
                        if (!value && ((DvContentGrid)Control).SelectedItems.Contains(this)) { ((DvContentGrid)Control).SelectedItems.Remove(this); Control.Invalidate(); }
                    }
                }
            }
        }
        #endregion
        #region Visible
        private bool bVisible = true;
        public bool Visible { get => bVisible; set { if (bVisible != value) { bVisible = value; Control?.Invalidate(); } } }
        #endregion
        #region Enabled
        private bool bEnabled = true;
        public bool Enabled { get => bEnabled; set { if (bEnabled != value) { bEnabled = value; Control?.Invalidate(); } } }
        #endregion
        #region Tag
        public object Tag { get; set; }
        #endregion
        #endregion
        #region Constructor
        public DvContent(DvControl Control) { this.Control = Control; }
        #endregion
        #region Abstract
        public abstract Rectangle GetBounds(Rectangle Bounds);
        #endregion
        #region Virtual
        public virtual void Draw(Graphics g, DvTheme Theme, Rectangle Bounds) { }
        public virtual void MouseDown(Rectangle Bounds, Point p) { }
        public virtual void MouseUp(Rectangle Bounds, Point p) { }
        public virtual void MouseMove(Rectangle Bounds, Point p) { }
        public virtual void MouseDoubleClick(Rectangle Bounds, Point p) { }
        public virtual bool Collision(Rectangle Bounds, Point p) { return CollisionTool.Check(GetBounds(Bounds), p); }
        public virtual bool Collision(Rectangle Bounds, Rectangle Bounds2) { return CollisionTool.Check(GetBounds(Bounds), Bounds2); }
        #endregion

        #region Internal
        internal Rectangle Bounds
        {
            get
            {
                if (Control is DvContentView)
                {
                    var c = Control as DvContentView;
                    var ContentSize = new SizeF(c.ContentSize.Width, c.ContentSize.Height);
                    if (c.Areas.ContainsKey("rtBox"))
                    {
                        var rtBox = c.Areas["rtBox"];

                        if (c.ScrollDirection == ScrollDirection.Vertical)
                        {
                            var cw = (int)Math.Round((double)(rtBox.Width) / (double)ContentSize.Width);
                            var sw = rtBox.Width / (float)cw;
                            ContentSize.Width = sw;
                        }
                        else
                        {
                            var ch = (int)Math.Round((double)(rtBox.Height) / (double)ContentSize.Height);
                            var sh = rtBox.Height / (float)ch;
                            ContentSize.Height = sh;
                        }
                    }
                    return new Rectangle(Convert.ToInt32(ColIndex * ContentSize.Width), Convert.ToInt32(RowIndex * ContentSize.Height), Convert.ToInt32(ContentSize.Width * ColSpan), Convert.ToInt32(ContentSize.Height * RowSpan));
                }
                if (Control is DvContentGrid)
                {
                    var c = Control as DvContentGrid;
                    var ContentSize = new SizeF(c.ContentSize.Width, c.ContentSize.Height);
                    if (c.Areas.ContainsKey("rtBox"))
                    {
                        var rtBox = c.Areas["rtBox"];

                        var cw = (int)Math.Round((double)(rtBox.Width - (c.PageGap * 2)) / (double)ContentSize.Width);
                        var sw = (rtBox.Width - (c.PageGap * 2)) / (float)cw;
                        ContentSize.Width = sw;
                    }
                    return new Rectangle(Convert.ToInt32(ColIndex * ContentSize.Width) + c.PageGap, Convert.ToInt32(RowIndex * ContentSize.Height), Convert.ToInt32(ContentSize.Width * ColSpan), Convert.ToInt32(ContentSize.Height * RowSpan));
                }
                else
                {
                    return new Rectangle(0, 0, 0, 0);
                }
            }
        }
        internal Rectangle GridBounds => new Rectangle(ColIndex, RowIndex, ColSpan, RowSpan);
        internal bool Check(int col, int row, int colspan, int rowspan)
        {
            var Left1 = ColIndex;
            var Right1 = ColIndex + ColSpan;
            var Top1 = RowIndex;
            var Bottom1 = RowIndex + RowSpan;

            var Left2 = col;
            var Right2 = col + colspan;
            var Top2 = row;
            var Bottom2 = row + rowspan;

            return Right2 > Left1 && Right1 > Left2 && Bottom2 > Top1 && Bottom1 > Top2;
        }
        #endregion
    }
    #endregion
}
