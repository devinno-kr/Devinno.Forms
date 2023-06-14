using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvDataGrid : DvControl
    {
        #region Const 
        internal const int SPECIAL_CELL_WIDTH = 30;
        internal const int SELECTOR_BOX_WIDTH = 18;
        #endregion

        #region Properties
        #region BoxColor
        private Color? cBoxColor = null;
        public Color? BoxColor
        {
            get => cBoxColor;
            set
            {
                if(cBoxColor != value)
                {
                    cBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ColumnColor
        private Color? cColumnColor = null;
        public Color? ColumnColor
        {
            get => cColumnColor;
            set
            {
                if (cColumnColor != value)
                {
                    cColumnColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region InputColor
        private Color? cInputColor = null;
        public Color? InputColor
        {
            get => cInputColor;
            set
            {
                if(cInputColor != null)
                {
                    cInputColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region RowColor
        private Color? cRowColor = null;
        public Color? RowColor
        {
            get => cRowColor;
            set
            {
                if (cRowColor != value)
                {
                    cRowColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region SelectedRowColor
        private Color? cSelectedRowColor = null;
        public Color? SelectedRowColor
        {
            get => cSelectedRowColor;
            set
            {
                if (cSelectedRowColor != value)
                {
                    cSelectedRowColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region SummaryRowColor
        private Color? cSummaryRowColor = null;
        public Color? SummaryRowColor
        {
            get => cSummaryRowColor;
            set
            {
                if (cSummaryRowColor != value)
                {
                    cSummaryRowColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region RowHeight
        private int nRowHeight = 30;
        public int RowHeight
        {
            get => nRowHeight;
            set
            {
                if (nRowHeight != value)
                {
                    nRowHeight = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ColumnHeight
        private int nColumnHeight = 30;
        public int ColumnHeight
        {
            get => nColumnHeight;
            set
            {
                if (nColumnHeight != value)
                {
                    nColumnHeight = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region ColumnGroups
        [Browsable(false)]
        public List<IDvDataGridColumn> ColumnGroups { get; private set; } = new List<IDvDataGridColumn>();
        #endregion
        #region Columns
        [Browsable(false)]
        public List<IDvDataGridColumn> Columns { get; private set; } = new List<IDvDataGridColumn>();
        #endregion
        #region Rows
        [Browsable(false)]
        public List<DvDataGridRow> Rows { get; } = new List<DvDataGridRow>();
        #endregion
        #region SummaryRows
        [Browsable(false)]
        public List<DvDataGridSummaryRow> SummaryRows { get; } = new List<DvDataGridSummaryRow>();
        #endregion

        #region SelectionMode
        public DvDataGridSelectionMode SelectionMode { get; set; } = DvDataGridSelectionMode.Single;
        #endregion

        #region ScrollMode
        public ScrollMode ScrollMode { get; set; } = ScrollMode.Vertical;
        #endregion
        #region VScrollPosition
        public double VScrollPosition
        {
            get => vscroll.ScrollPosition;
            set => vscroll.ScrollPosition = value;
        }
        #endregion
        #region HScrollPosition
        public double HScrollPosition
        {
            get => hscroll.ScrollPosition; 
            set => hscroll.ScrollPosition = value;
        }
        #endregion

        #region Bevel
        private bool bBevel = true;
        public bool Bevel
        {
            get => bBevel;
            set
            {
                if (bBevel != value)
                {
                    bBevel = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Internal
        #region DataType
        internal Type DataType { get; private set; } = null;
        #endregion
        #region VisibleDropDown 
        internal bool VisibleDropDown { get; set; } = false;
        #endregion
        #endregion
        #endregion

        #region Member Variable
        List<DvDataGridRow> mrows = new List<DvDataGridRow>();
        Scroll vscroll = new Scroll() { Direction = ScrollDirection.Vertical, Cut = true, TouchMode = true };
        Scroll hscroll = new Scroll() { Direction = ScrollDirection.Horizon, Cut = true, TouchMode = true };
        TextBox OriginalTextBox;

        bool bNotRaiseEvent = false;
        object objs = null;
        int PrevTotalHeight = -1;
        List<_DGSearch_> lsp = new List<_DGSearch_>();

        internal bool bModSize = false;
       
        double hST = 0, vST = 0;
        double hSV = 0, vSV = 0;
        #endregion

        #region Event
        public event EventHandler SelectedChanged;
        public event EventHandler SortChanged;

        public event EventHandler<ColumnMouseEventArgs> ColumnMouseDown;
        public event EventHandler<ColumnMouseEventArgs> ColumnMouseUp;
        public event EventHandler<CellMouseEventArgs> CellMouseDown;
        public event EventHandler<CellMouseEventArgs> CellMouseUp;
        public event EventHandler<CellMouseEventArgs> CellMouseClick;
        public event EventHandler<CellMouseEventArgs> CellMouseDoubleClick;
        public event EventHandler<CellValueChangedEventArgs> ValueChanged;
        public event EventHandler<CellButtonClickEventArgs> CellButtonClick;
        #endregion

        #region Constructor
        public DvDataGrid()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(300, 200);

            #region Scroll
            hscroll.GetScrollTotal = () => hST;
            hscroll.GetScrollTick = () => 1;
            hscroll.GetScrollView = () => hSV;
            hscroll.ScrollChanged += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            hscroll.ScrollEnded += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };

            vscroll.GetScrollTotal = () => vST;
            vscroll.GetScrollTick = () => RowHeight;
            vscroll.GetScrollView = () => vSV;
            vscroll.ScrollChanged += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            vscroll.ScrollEnded += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            #endregion

            #region TextBox
            OriginalTextBox = new TextBox();
            OriginalTextBox.Location = new System.Drawing.Point(0, 0);
            OriginalTextBox.Name = "OriginalTextBox";
            OriginalTextBox.Size = new System.Drawing.Size(60, 28);
            OriginalTextBox.BorderStyle = BorderStyle.None;
            OriginalTextBox.TabIndex = 0;
            OriginalTextBox.TextAlign = HorizontalAlignment.Center;
            OriginalTextBox.Tag = null;
            OriginalTextBox.Visible = false;
            Controls.Add(OriginalTextBox);

            OriginalTextBox.LostFocus += (o, s) => ClearInput();
            OriginalTextBox.TextChanged += (o, s) => FlushInput();

            OriginalTextBox.KeyDown += (o, s) =>
            {
                if (s.KeyCode == Keys.Up || s.KeyCode == Keys.Down || s.KeyCode == Keys.Left || s.KeyCode == Keys.Right || s.KeyCode == Keys.Enter)
                {
                    Areas((rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR) =>
                    {
                        s.SuppressKeyPress = true;
                        var Theme = GetTheme();
                        var current = OriginalTextBox.Tag as DvDataGridCell;
                        if (current != null && mrows.Count > 0)
                        {
                            var ri = mrows.IndexOf(current.Row);
                            var ci = current.ColumnIndex;
                            var rtCurrent = GetCellBounds(ri, ci, current);
                             
                            #region Keys.Down
                            if (s.KeyCode == Keys.Down && ri + 1 < mrows.Count && rtCurrent.HasValue)
                            {
                                var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                                var next = mrows[ri + 1].Cells[ci] as DvDataGridCell;
                                var rtNext = GetCellBounds(ri + 1, ci, current);

                                if (next != null && rtNext.HasValue)
                                {
                                    if (rtNext.Value.Bottom > rtScrollContent.Bottom)
                                        vscroll.ScrollPosition = MathTool.Constrain(-vspos + rtNext.Value.Top - vscroll.ScrollView, 0, vscroll.ScrollTotal); 

                                    ClearInput();
                                    SetCellFocus(next, GetCellBounds(ri + 1, ci, next).Value);
                                }
                            }
                            #endregion
                            #region Keys.Up
                            if (s.KeyCode == Keys.Up && ri - 1 >= 0 && rtCurrent.HasValue)
                            {
                                var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                                var prev = mrows[ri - 1].Cells[ci] as DvDataGridCell;
                                var rtPrev = GetCellBounds(ri - 1, ci, current);

                                if (prev != null && rtPrev.HasValue)
                                {
                                    if (rtPrev.Value.Top < rtScrollContent.Top)
                                        vscroll.ScrollPosition = MathTool.Constrain(-vspos + rtPrev.Value.Top - vscroll.ScrollTick, 0, vscroll.ScrollTotal);

                                    ClearInput();
                                    SetCellFocus(prev, GetCellBounds(ri - 1, ci, prev).Value);
                                }
                            }
                            #endregion
                            #region Keys.Right
                            if (s.KeyCode == Keys.Right && rtCurrent.HasValue)
                            {
                                var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);
                                var next = current.Row.Cells.Where(x => (x.GetType().Name.StartsWith("DvDataGridEditTextCell") || x.GetType().Name.StartsWith("DvDataGridEditNumberCell")) && x.ColumnIndex > ci)
                                           .OrderBy(x => x.ColumnIndex).Select(x => x as DvDataGridCell).FirstOrDefault();

                                if (next != null)
                                {
                                    var ci2 = next.ColumnIndex;
                                    var ri2 = next.RowIndex;
                                    var rtNext = GetCellBounds(ri2, ci2, next);
                                    if (rtNext.HasValue)
                                    {
                                        if (rtNext.Value.Right > rtScrollArea.Right)
                                            hscroll.ScrollPosition = MathTool.Constrain(-hspos + rtNext.Value.Right - hscroll.ScrollView, 0, hscroll.ScrollTotal) - rtScrollArea.Left;

                                        ClearInput();
                                        SetCellFocus(next, GetCellBounds(ri2, ci2, next).Value);
                                    }
                                }
                            }
                            #endregion
                            #region Keys.Left
                            if (s.KeyCode == Keys.Left && rtCurrent.HasValue)
                            {
                                var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);
                                var prev = current.Row.Cells.Where(x => (x.GetType().Name.StartsWith("DvDataGridEditTextCell") || x.GetType().Name.StartsWith("DvDataGridEditNumberCell")) && x.ColumnIndex < ci)
                                           .OrderBy(x => x.ColumnIndex).Select(x => x as DvDataGridCell).LastOrDefault();

                                if (prev != null)
                                {
                                    var ci2 = prev.ColumnIndex;
                                    var ri2 = prev.RowIndex;
                                    var rtPrev = GetCellBounds(ri2, ci2, prev);
                                    if (rtPrev.HasValue)
                                    {
                                        if (rtPrev.Value.Left < rtScrollArea.Left)
                                            hscroll.ScrollPosition = MathTool.Constrain(-hspos + rtPrev.Value.Left, 0, hscroll.ScrollTotal) - rtScrollArea.Left;

                                        ClearInput();
                                        SetCellFocus(prev, GetCellBounds(ri2, ci2, prev).Value);
                                    }
                                }
                            }
                            #endregion
                            #region Keys.Enter
                            if (s.KeyCode == Keys.Enter && rtCurrent.HasValue)
                            {
                                if (ri + 1 < mrows.Count)
                                {
                                    var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                                    var next = mrows[ri + 1].Cells[ci] as DvDataGridCell;
                                    var rtNext = GetCellBounds(ri + 1, ci, current);

                                    if (next != null && rtNext.HasValue)
                                    {
                                        if (rtNext.Value.Bottom > rtScrollContent.Bottom)
                                            vscroll.ScrollPosition = MathTool.Constrain(-vspos + rtNext.Value.Top - vscroll.ScrollView, 0, vscroll.ScrollTotal);

                                        ClearInput();
                                        SetCellFocus(next, GetCellBounds(ri + 1, ci, next).Value);
                                    }
                                }
                                else ClearInput();
                            }
                            #endregion
                        }
                    });
                    Invalidate();
                }

            };

            #endregion
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BoxColor = GetBoxColor(Theme);
            var ColumnColor = GetColumnColor(Theme);
            var RowColor = GetRowColor(Theme);
            var SelectRowColor = GetSelectedRowColor(Theme);
            var SummaryRowColor = GetSummaryRowColor(Theme);
            var BorderColor = Theme.GetBorderColor(BoxColor, BackColor);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init 
            var p = new Pen(Color.Black);
            var br = new SolidBrush(Color.Black);
            #endregion

            vscroll.TouchMode = hscroll.TouchMode = Theme.TouchMode;

            Areas((rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR) =>
            {
                var thm = Theme;
                if (thm != null)
                {
                    #region Set
                    var Corner = thm.Corner;

                    var rtColumnV = Util.FromRect(rtColumn.Left, rtColumn.Top, rtScrollContent.Width, rtColumn.Height);

                    var ush = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Horizon;
                    var usv = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Vertical;
                    var useFilter = Columns.Where(x => x.UseFilter).Count() > 0;
                    var colrc = GetColumnRowCount() + (useFilter ? 1 : 0);
                    var ColWidths = GetColumnsWidths(rtScrollContent);
                    var rts = GetColumnBounds(rtColumn, rtScrollContent);
                    var spw = Convert.ToInt32(SPECIAL_CELL_WIDTH);
                    var sbw = Convert.ToInt32(SELECTOR_BOX_WIDTH);
                    var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                    var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);

                    hST = Columns.Where(x => !x.Fixed).Select(x => ColWidths[Columns.IndexOf(x)]).Sum();
                    hSV = rtScrollArea.Width;
                    vSV = rtScrollArea.Height;

                    var bAllSelect = GetRows().Where(x => x.Selected).Count() > 0;
                    var bUseFilter = Columns.Where(x => x.UseFilter).Count() > 0;
                    #endregion
                    #region Draw
                    Theme.DrawBox(e.Graphics, rtBox, BoxColor, BorderColor, RoundType.L, Box.ListBox(ShadowGap));


                    using (var path = DrawingTool.GetRoundRectPath(rtContent, Corner))
                    {
                        e.Graphics.SetClip(path);

                        #region Column Index 
                        var rtnm = "rtColumn";
                        var lsnf = Columns.Where(x => !x.Fixed).ToList(); var vsnf = lsnf.FirstOrDefault(); var venf = lsnf.LastOrDefault(); var mrtNF = (RectangleF?)null;
                        int? isnf = null, ienf = null;
                        if (vsnf != null && venf != null && rts.ContainsKey(rtnm + vsnf.Name) && rts.ContainsKey(rtnm + venf.Name))
                        {
                            var rtsv = rts[rtnm + vsnf.Name];
                            var rtev = rts[rtnm + venf.Name];
                            var mrt = Util.FromRect(rtsv.Left, rtColumn.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtColumn.Height);

                            var vls = lsnf.Where(x => CollisionTool.Check(mrt, (Util.FromRect(rts[rtnm + x.Name].Left + hspos, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                            isnf = Columns.IndexOf(vls.FirstOrDefault());
                            ienf = Columns.IndexOf(vls.LastOrDefault());
                            mrtNF = mrt;
                        }

                        var lsf = Columns.Where(x => x.Fixed).ToList(); var vsf = lsf.FirstOrDefault(); var vef = lsf.LastOrDefault(); var mrtF = (RectangleF?)null;
                        int? isf = null, ief = null;
                        if (vsf != null && vef != null && rts.ContainsKey(rtnm + vsf.Name) && rts.ContainsKey(rtnm + vef.Name))
                        {
                            var rtsv = rts[rtnm + vsf.Name];
                            var rtev = rts[rtnm + vef.Name];
                            var mrt = Util.FromRect(rtsv.Left, rtColumn.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);

                            var vls = lsf.Where(x => CollisionTool.Check(mrt, (Util.FromRect(rts[rtnm + x.Name].Left, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                            isf = Columns.IndexOf(vls.FirstOrDefault());
                            ief = Columns.IndexOf(vls.LastOrDefault());
                            mrtF = mrt;
                        }

                        rtnm = "rtColumnGroup";
                        var lsgnf = ColumnGroups.Where(x => !x.Fixed).ToList(); var vsgnf = lsgnf.FirstOrDefault(); var vegnf = lsgnf.LastOrDefault(); var mrtGNF = (RectangleF?)null;
                        int? isgnf = null, iegnf = null;
                        if (vsgnf != null && vegnf != null && rts.ContainsKey(rtnm + vsgnf.Name) && rts.ContainsKey(rtnm + vegnf.Name))
                        {
                            var rtsv = rts[rtnm + vsgnf.Name];
                            var rtev = rts[rtnm + vegnf.Name];
                            var mrt = Util.FromRect(rtsv.Left, rtsv.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtsv.Height);

                            var vls = lsgnf.Where(x => CollisionTool.Check(mrt, (Util.FromRect(rts[rtnm + x.Name].Left + hspos, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                            isgnf = ColumnGroups.IndexOf(vls.FirstOrDefault());
                            iegnf = ColumnGroups.IndexOf(vls.LastOrDefault());
                            mrtGNF = mrt;
                        }

                        var lsgf = ColumnGroups.Where(x => x.Fixed).ToList(); var vsgf = lsgf.FirstOrDefault(); var vegf = lsgf.LastOrDefault(); var mrtGF = (RectangleF?)null;
                        int? isgf = null, iegf = null;
                        if (vsgf != null && vegf != null && rts.ContainsKey(rtnm + vsgf.Name) && rts.ContainsKey(rtnm + vegf.Name))
                        {
                            var rtsv = rts[rtnm + vsgf.Name];
                            var rtev = rts[rtnm + vegf.Name];
                            var mrt = Util.FromRect(rtsv.Left, rtsv.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);

                            var vls = lsgf.Where(x => CollisionTool.Check(mrt, (Util.FromRect(rts[rtnm + x.Name].Left, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                            isgf = ColumnGroups.IndexOf(vls.FirstOrDefault());
                            iegf = ColumnGroups.IndexOf(vls.LastOrDefault());
                            mrtGF = mrt;
                        }
                        #endregion
                        #region Content
                        {
                            //e.Graphics.SetClip(rtScrollContent);
                            #region Rows
                            {
                                var last = GetRows().LastOrDefault();

                                Loop((i, rtROW, v) =>
                                {
                                    #region !Fixed
                                    if (mrtNF.HasValue && isnf.HasValue && ienf.HasValue)
                                    {
                                        var vls = v.Cells.GetRange(isnf.Value, ienf.Value - isnf.Value + 1).ToList();
                                        var mrt = Util.FromRect(mrtNF.Value.Left, rtScrollContent.Top, mrtNF.Value.Width, rtScrollContent.Height);

                                        foreach (var cell in vls)
                                        {
                                            if (cell.Visible)
                                            {
                                                var rtCol = rts["rtColumn" + cell.Column.Name];
                                                var rt = Util.FromRect(rtCol.Left, rtROW.Top, rtCol.Width, rtROW.Height); rt.Offset(hspos, 0);

                                                if (cell.ColSpan > 1 && cell.ColumnIndex + cell.ColSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColSpan).Sum();
                                                if (cell.RowSpan > 1 && cell.RowIndex + cell.RowSpan <= Rows.Count) rt.Height = Rows.GetRange(cell.RowIndex, cell.RowSpan).Sum(x => x.RowHeight);
                                                cell.Draw(e.Graphics, thm, rt);

                                                if (last != null && cell == last)
                                                {
                                                    p.Width = 1F;
                                                    p.Color = BorderColor;

                                                    var l = rt.Left;
                                                    var r = rt.Right;
                                                    var b = rt.Bottom;
                                                    var t = rt.Top;
                                                    e.Graphics.DrawLine(p, r, t, r, b);
                                                }

                                                if (CheckInput((DvDataGridCell)cell))
                                                {
                                                    e.Graphics.SetClip(rt);

                                                    var type = cell.GetType();
                                                    var rtv = rt; rtv.Inflate(-1, -1);
                                                    
                                                    p.Width = 1;
                                                    p.Color = Color.Orange;
                                                    
                                                    if (type == typeof(DvDataGridEditNumberCell<byte>) && ((DvDataGridEditNumberCell<byte>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);
                                                    else if (type == typeof(DvDataGridEditNumberCell<ushort>) && ((DvDataGridEditNumberCell<ushort>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);
                                                    else if (type == typeof(DvDataGridEditNumberCell<uint>) && ((DvDataGridEditNumberCell<uint>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);
                                                    else if (type == typeof(DvDataGridEditNumberCell<ulong>) && ((DvDataGridEditNumberCell<ulong>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);
                                                    else if (type == typeof(DvDataGridEditNumberCell<sbyte>) && ((DvDataGridEditNumberCell<sbyte>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);
                                                    else if (type == typeof(DvDataGridEditNumberCell<short>) && ((DvDataGridEditNumberCell<short>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);
                                                    else if (type == typeof(DvDataGridEditNumberCell<int>) && ((DvDataGridEditNumberCell<int>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);
                                                    else if (type == typeof(DvDataGridEditNumberCell<long>) && ((DvDataGridEditNumberCell<long>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);
                                                    else if (type == typeof(DvDataGridEditNumberCell<float>) && ((DvDataGridEditNumberCell<float>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);
                                                    else if (type == typeof(DvDataGridEditNumberCell<double>) && ((DvDataGridEditNumberCell<double>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);
                                                    else if (type == typeof(DvDataGridEditNumberCell<decimal>) && ((DvDataGridEditNumberCell<decimal>)cell).Error != InputError.None) p.Color = Color.FromArgb(220, 0, 0);

                                                    e.Graphics.DrawRectangle(p, rtv);
                                                    
                                                    e.Graphics.ResetClip();
                                                    e.Graphics.SetClip(rtScrollContent);
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    #region Fixed
                                    if (mrtF.HasValue && isf.HasValue && ief.HasValue)
                                    {
                                        var vls = v.Cells.GetRange(isf.Value, ief.Value - isf.Value + 1).ToList();
                                        var mrt = Util.FromRect(mrtF.Value.Left, rtScrollContent.Top, mrtF.Value.Width + 1, rtScrollContent.Height);

                                        var last = vls.Where(x => x.Visible).LastOrDefault();
                                        foreach (var cell in vls)
                                        {
                                            if (cell.Visible)
                                            {
                                                var rtCol = rts["rtColumn" + cell.Column.Name];
                                                var rt = Util.FromRect(rtCol.Left, rtROW.Top, rtCol.Width, rtROW.Height);
                                                if (cell.ColSpan > 1 && cell.ColumnIndex + cell.ColSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColSpan).Sum();
                                                if (cell.RowSpan > 1 && cell.RowIndex + cell.RowSpan <= Rows.Count) rt.Height = Rows.GetRange(cell.RowIndex, cell.RowSpan).Sum(x => x.RowHeight);
                                                cell.Draw(e.Graphics, thm, rt);

                                                if (last != null && cell == last)
                                                {
                                                    p.Width = 1F;
                                                    p.Color = BorderColor;

                                                    var l = rt.Left;
                                                    var r = rt.Right;
                                                    var b = rt.Bottom;
                                                    var t = rt.Top;
                                                    e.Graphics.DrawLine(p, r, t, r, b);
                                                }

                                                if (CheckInput((DvDataGridCell)cell))
                                                {
                                                    e.Graphics.SetClip(rt);

                                                    var rtv = rt; rtv.Inflate(-1, -1);
                                                    p.Width = 1;
                                                    p.Color = Color.Orange;
                                                    e.Graphics.DrawRectangle(p, rtv);

                                                    e.Graphics.ResetClip();
                                                    e.Graphics.SetClip(rtScrollContent);
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    #region Selector
                                    if (SelectionMode == DvDataGridSelectionMode.Selector)
                                    {
                                        var rt = Util.INT(Util.FromRect(rtROW.Left, rtROW.Top, spw, rtROW.Height));

                                        #region Background
                                        #region Fill
                                        var bg = BoxColor;
                                        var c = v.Selected ? SelectRowColor : RowColor;
                                        br.Color = c;
                                        e.Graphics.FillRectangle(br, rt);
                                        #endregion
                                        #region Bevel
                                        if (Bevel)
                                        {

                                            var pts = new PointF[] {
                                            new PointF(rt.Right , rt.Top + 1 ),
                                            new PointF(rt.Left + 1 , rt.Top + 1 ),
                                            new PointF(rt.Left + 1 , rt.Bottom )
                                            };
                                            p.Color = thm.GetInBevelColor(c);
                                            p.Width = 1;
                                            e.Graphics.DrawLine(p, pts[0], pts[1]);
                                            e.Graphics.DrawLine(p, pts[1], pts[2]);
                                        }
                                        #endregion
                                        #region Border
                                        {
                                            p.Color = BorderColor;
                                            p.Width = 1;
                                            e.Graphics.DrawLine(p, rt.Left, rt.Top, rt.Right, rt.Top);
                                            e.Graphics.DrawLine(p, rt.Left, rt.Bottom, rt.Right, rt.Bottom);
                                            e.Graphics.DrawLine(p, rt.Left, rt.Top, rt.Left, rt.Bottom);
                                            e.Graphics.DrawLine(p, rt.Right, rt.Top, rt.Right, rt.Bottom);
                                        }
                                        #endregion
                                        #endregion
                                        #region CheckBox
                                        var rtSelectorBox = MathTool.MakeRectangle(rt, new SizeF(sbw, sbw));
                                        thm.DrawBox(e.Graphics, rtSelectorBox, c.BrightnessTransmit(thm.DataGridCheckBoxBright), BorderColor, RoundType.Rect, Box.Concave(ShadowGap));
                                        #endregion
                                        #region Check
                                        if (v.Selected)
                                        {
                                            var INF = sbw / 4;
                                            var rtCheck = Util.FromRect(rtSelectorBox.Left, rtSelectorBox.Top - 0, rtSelectorBox.Width, rtSelectorBox.Height); rtCheck.Inflate(-INF, -INF);
                                            rtCheck.Inflate(-1, -1);

                                            p.Width = Convert.ToInt32(3);
                                            p.Color = ForeColor;

                                            var p1 = new PointF(rtCheck.X, rtCheck.Y + rtCheck.Height * 0.5F);
                                            var p2 = new PointF(rtCheck.X + rtCheck.Width * 0.5F, rtCheck.Y + rtCheck.Height);
                                            var p3 = new PointF(rtCheck.X + rtCheck.Width, rtCheck.Y);

                                            e.Graphics.DrawLines(p, new PointF[] { p1, p2, p3 });
                                            p.Width = 1;
                                        }
                                        #endregion
                                    }
                                    #endregion

                                });
                            }
                            #endregion
                            #region Summary 
                            if (SummaryRows.Count > 0)
                            {
                                for (int i = 0; i < SummaryRows.Count; i++)
                                {
                                    var v = SummaryRows[i];
                                    var rtROW = Util.FromRect(rtSummary.Left, rtSummary.Top + (i * RowHeight), rtSummary.Width, RowHeight);
                                    #region Selector
                                    if (SelectionMode == DvDataGridSelectionMode.Selector)
                                    {
                                        var rt = Util.INT(Util.FromRect(rtROW.Left, rtROW.Top, spw, rtROW.Height));

                                        #region Background
                                        #region Fill
                                        var bg = BoxColor;
                                        var c = SummaryRowColor;
                                        br.Color = c;
                                        e.Graphics.FillRectangle(br, rt);
                                        #endregion
                                        #region Bevel
                                        if (Bevel)
                                        {
                                            var pts = new PointF[] {
                                                new PointF(rt.Right, rt.Top + 1),
                                                new PointF(rt.Left + 1, rt.Top + 1),
                                                new PointF(rt.Left + 1, rt.Bottom)
                                            };
                                            p.Color = thm.GetInBevelColor(c);
                                            p.Width = 1;
                                            e.Graphics.DrawLine(p, pts[0], pts[1]);
                                            e.Graphics.DrawLine(p, pts[1], pts[2]);
                                        }
                                        #endregion
                                        #region Border
                                        {
                                            p.Color = BorderColor;
                                            p.Width = 1;
                                            e.Graphics.DrawLine(p, rt.Left, rt.Top, rt.Right, rt.Top);
                                            e.Graphics.DrawLine(p, rt.Left, rt.Top, rt.Left, rt.Bottom);
                                        }
                                        #endregion
                                        #endregion
                                    }
                                    #endregion
                                    #region !Fixed
                                    if (mrtNF.HasValue && isnf.HasValue && ienf.HasValue)
                                    {
                                        var vls = v.Cells.GetRange(isnf.Value, ienf.Value - isnf.Value + 1).ToList();
                                        var mrt = Util.FromRect(mrtNF.Value.Left, rtSummary.Top, mrtNF.Value.Width, rtSummary.Height);

                                        foreach (var cell in vls)
                                        {
                                            if (cell.Visible)
                                            {
                                                var rtCol = rts["rtColumn" + Columns[cell.ColumnIndex].Name];
                                                var rt = Util.FromRect(rtCol.Left, rtROW.Top, rtCol.Width, rtROW.Height); rt.Offset(hspos, 0);
                                                if (cell.ColumnSpan > 1 && cell.ColumnIndex + cell.ColumnSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColumnSpan).Sum();
                                                cell.Draw(e.Graphics, thm, rt);
                                            }
                                        }
                                    }
                                    #endregion
                                    #region Fixed
                                    if (mrtF.HasValue && isf.HasValue && ief.HasValue)
                                    {
                                        var vls = v.Cells.GetRange(isf.Value, ief.Value - isf.Value + 1).ToList();
                                        var mrt = Util.FromRect(mrtF.Value.Left, rtSummary.Top, mrtF.Value.Width + 1, rtSummary.Height);

                                        var last = vls.Where(x => x.Visible).LastOrDefault();
                                        foreach (var cell in vls)
                                        {
                                            if (cell.Visible)
                                            {
                                                var rtCol = rts["rtColumn" + Columns[cell.ColumnIndex].Name];
                                                var rt = Util.FromRect(rtCol.Left, rtROW.Top, rtCol.Width, rtROW.Height);
                                                if (cell.ColumnSpan > 1 && cell.ColumnIndex + cell.ColumnSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColumnSpan).Sum();
                                                cell.Draw(e.Graphics, thm, rt);

                                                if (last != null && cell == last)
                                                {
                                                    p.Width = 1F;
                                                    p.Color = BorderColor;

                                                    var l = rt.Left;
                                                    var r = rt.Right;
                                                    var b = rt.Bottom;
                                                    var t = rt.Top;
                                                    e.Graphics.DrawLine(p, r, t, r, b);
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                            //e.Graphics.ResetClip();
                            //e.Graphics.SetClip(path);
                        }
                        #endregion
                        #region Column
                        if (Columns.Count > 0)
                        {
                            #region Column
                            {
                                #region !Fixed
                                if (mrtNF.HasValue && isnf.HasValue && ienf.HasValue)
                                {
                                    var vls = Columns.GetRange(isnf.Value, ienf.Value - isnf.Value + 1).ToList();
                                    var mrt = Util.FromRect(mrtNF.Value.Left, rtColumn.Top, mrtNF.Value.Width, rtColumn.Height);

                                    foreach (var col in vls)
                                    {
                                        #region Column
                                        {
                                            var rt = (rts["rtColumn" + col.Name]); rt.Offset(hspos, 0);
                                            thm.DrawBox(e.Graphics, rt, ColumnColor, BorderColor, RoundType.Rect, BoxStyle.Fill);
                                            DrawColumnBorder(e.Graphics, thm, rtColumn, rtScrollContent, rt);
                                            col.Draw(e.Graphics, thm, rt);
                                        }
                                        #endregion
                                        #region Filter
                                        if (col.UseFilter)
                                        {
                                            var rt = (rts["rtFilter" + col.Name]); rt.Offset(hspos, 0);
                                            var n2 = Convert.ToInt32(0);
                                            var rtin = Util.FromRect(rt.Left, rt.Top, rt.Width, rt.Height); rtin.Inflate(-n2, -n2);
                                            var cbox = ColumnColor.BrightnessTransmit(thm.DataGridInputBright);
                                            var cbor = ColumnColor.BrightnessTransmit(thm.BorderBrightness);
                                            thm.DrawBox(e.Graphics, rtin, cbox, cbor, RoundType.Rect, BoxStyle.Fill);
                                            Theme.DrawText(e.Graphics, col.FilterText, Font, ForeColor, rt);
                                            DrawColumnBorder(e.Graphics, thm, rtColumn, rtScrollContent, rt, false);
                                        }
                                        else
                                        {
                                            var rt = (rts["rtFilter" + col.Name]); rt.Offset(hspos, 0);
                                            if (bUseFilter)
                                            {
                                                thm.DrawBox(e.Graphics, rt, ColumnColor, BorderColor, RoundType.Rect, BoxStyle.Fill);

                                                p.Width = 1F;
                                                p.Color = BorderColor;

                                                var b = rt.Bottom;
                                                var t = rt.Top;
                                                e.Graphics.DrawLine(p, rt.Left, t, rt.Right, t);
                                                e.Graphics.DrawLine(p, rt.Left, b, rt.Right, b);
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                                #region Fixed
                                if (mrtF.HasValue && isf.HasValue && ief.HasValue)
                                {
                                    var vls = Columns.GetRange(isf.Value, ief.Value - isf.Value + 1).ToList();
                                    var mrt = Util.FromRect(mrtF.Value.Left, rtColumn.Top, mrtF.Value.Width, rtColumn.Height);

                                    var last = vls.LastOrDefault();
                                    foreach (var col in vls)
                                    {
                                        #region Column
                                        {
                                            var rt = (rts["rtColumn" + col.Name]);
                                            thm.DrawBox(e.Graphics, rt, ColumnColor, BorderColor, RoundType.Rect, BoxStyle.Fill);
                                            DrawColumnBorder(e.Graphics, thm, rtColumn, rtScrollContent, rt);
                                            col.Draw(e.Graphics, thm, rt);

                                            if (last != null && col == last)
                                            {
                                                p.Width = 1F;
                                                p.Color = BorderColor;

                                                var l = rt.Left;
                                                var r = rt.Right;
                                                var b = rt.Bottom;
                                                var t = rt.Top;
                                                e.Graphics.DrawLine(p, r, t, r, b);
                                            }
                                        }
                                        #endregion
                                        #region Filter
                                        if (col.UseFilter)
                                        {
                                            var rt = (rts["rtFilter" + col.Name]);
                                            var n2 = Convert.ToInt32(0);
                                            var rtin = Util.FromRect(rt.Left, rt.Top + 1, rt.Width, rt.Height - 1); rtin.Inflate(-n2, -n2);
                                            var cbox = ColumnColor.BrightnessTransmit(thm.DataGridInputBright);
                                            var cbor = ColumnColor.BrightnessTransmit(thm.BorderBrightness);
                                            thm.DrawBox(e.Graphics, rtin, cbox, cbor, RoundType.Rect, BoxStyle.Fill);
                                            thm.DrawText(e.Graphics, col.FilterText, Font, ForeColor, rt);
                                            DrawColumnBorder(e.Graphics, thm, rtColumn, rtScrollContent, rt, false);
                                        }
                                        else
                                        {
                                            var rt = (rts["rtFilter" + col.Name]);
                                            if (bUseFilter)
                                            {
                                                thm.DrawBox(e.Graphics, rt, ColumnColor, BorderColor, RoundType.Rect, BoxStyle.Fill);

                                               p.Width = 1F;
                                                p.Color = BorderColor;

                                                var b = rt.Bottom;
                                                var t = rt.Top;
                                                e.Graphics.DrawLine(p, rt.Left, t, rt.Right, t);
                                                e.Graphics.DrawLine(p, rt.Left, b, rt.Right, b);
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            #region ColumnGroup
                            {
                                #region !Fixed
                                if (mrtGNF.HasValue && isgnf.HasValue && iegnf.HasValue)
                                {
                                    var vls = ColumnGroups.GetRange(isgnf.Value, iegnf.Value - isgnf.Value + 1).ToList();
                                    var mrt = Util.FromRect(mrtGNF.Value.Left, rtColumn.Top, mrtGNF.Value.Width, rtColumn.Height);

                                    foreach (var colgroup in vls)
                                    {
                                        var rt = Util.INT(rts["rtColumnGroup" + colgroup.Name]); rt.Offset(hspos, 0);
                                        thm.DrawBox(e.Graphics, rt, ColumnColor, BorderColor, RoundType.Rect, BoxStyle.Fill);
                                        DrawColumnBorder(e.Graphics, thm, rtColumn, rtScrollContent, rt);
                                        colgroup.Draw(e.Graphics, thm, rt);
                                    }
                                }
                                #endregion
                                #region Fixed
                                if (mrtGF.HasValue && isgf.HasValue && iegf.HasValue)
                                {
                                    var vls = ColumnGroups.GetRange(isgf.Value, iegf.Value - isgf.Value + 1).ToList();
                                    var mrt = Util.FromRect(mrtGF.Value.Left, rtColumn.Top, mrtGF.Value.Width, rtColumn.Height);

                                    var last = vls.LastOrDefault();
                                    foreach (var colgroup in vls)
                                    {
                                        var rt = Util.INT(rts["rtColumnGroup" + colgroup.Name]);
                                        thm.DrawBox(e.Graphics, rt, ColumnColor, BorderColor, RoundType.Rect, BoxStyle.Fill);
                                        DrawColumnBorder(e.Graphics, thm, rtColumn, rtScrollContent, rt);
                                        colgroup.Draw(e.Graphics, thm, rt);

                                        if (last != null && colgroup == last)
                                        {
                                            p.Width = 1F;
                                            p.Color = BorderColor;

                                            var l = rt.Left;
                                            var r = rt.Right;
                                            var b = rt.Bottom;
                                            var t = rt.Top;
                                            e.Graphics.DrawLine(p, r, t, r, b);
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            #region ColumnSelector
                            if (SelectionMode == DvDataGridSelectionMode.Selector)
                            {
                                var rtSelector = Util.FromRect(rtColumn.Left, rtColumn.Top, spw, rtColumn.Height);
                                thm.DrawBox(e.Graphics, rtSelector, ColumnColor, BorderColor, RoundType.Rect, BoxStyle.Fill);
                                DrawColumnBorder(e.Graphics, thm, rtColumn, rtScrollContent, rtSelector);

                                var rtSelectorBox = MathTool.MakeRectangle(Util.INT(rtSelector), new SizeF(sbw, sbw));
                                thm.DrawBox(e.Graphics, rtSelectorBox, ColumnColor.BrightnessTransmit(thm.DataGridCheckBoxBright), BorderColor, RoundType.Rect, BoxStyle.Border | BoxStyle.Fill | BoxStyle.OutBevel | BoxStyle.InShadow);

                                if (bAllSelect)
                                {
                                    var INF = sbw / 4;
                                    var rtCheck = Util.FromRect(rtSelectorBox.Left, rtSelectorBox.Top - 0, rtSelectorBox.Width, rtSelectorBox.Height); rtCheck.Inflate(-INF, -INF);
                                    rtCheck.Inflate(-1, -1);

                                    p.Width = Convert.ToInt32(3);
                                    p.Color = ForeColor;

                                    var p1 = new PointF(rtCheck.X, rtCheck.Y + rtCheck.Height * 0.5F);
                                    var p2 = new PointF(rtCheck.X + rtCheck.Width * 0.5F, rtCheck.Y + rtCheck.Height);
                                    var p3 = new PointF(rtCheck.X + rtCheck.Width, rtCheck.Y);

                                    e.Graphics.DrawLines(p, new PointF[] { p1, p2, p3 });
                                    p.Width = 1;
                                }
                            }
                            #endregion
                            #region Column Scroll
                            {
                                var rt = new RectangleF(rtScrollV.Left, rtColumn.Top, rtScrollV.Right, rtColumn.Bottom);
                                thm.DrawBox(e.Graphics, rt, ColumnColor, BorderColor, RoundType.RT, BoxStyle.Fill);
                            }
                            #endregion
                            #region Column Border
                            p.Width = 1;
                            p.Color = BorderColor;
                            e.Graphics.DrawLine(p, rtColumn.Left, rtColumn.Bottom, rtColumn.Right, rtColumn.Bottom);

                            if (rtColumn.Right != rtScrollContent.Right)
                            {
                                p.Width = 1;
                                p.Color = BorderColor;
                                e.Graphics.DrawLine(p, rtScrollContent.Right, rtColumn.Top, rtScrollContent.Right, rtColumn.Bottom);
                            }

                            if (SelectionMode == DvDataGridSelectionMode.Selector)
                            {
                                p.Width = 1;
                                p.Color = BorderColor;
                                e.Graphics.DrawLine(p, rtScrollContent.Left + spw, rtColumn.Top, rtScrollContent.Left + spw, rtColumn.Bottom);
                            }
                            #endregion
                        }
                        #endregion
                        #region Scroll
                        switch (ScrollMode)
                        {
                            #region Horizon
                            case ScrollMode.Horizon:
                                {
                                    thm.DrawBox(e.Graphics, rtScrollH, thm.ScrollBarColor, BorderColor, RoundType.B, BoxStyle.Border | BoxStyle.Fill | BoxStyle.OutShadow);

                                    var cCur = thm.ScrollCursorOffColor;
                                    if (hscroll.IsScrolling || hscroll.IsTouchMoving) cCur = thm.ScrollCursorOnColor;

                                    var rtcur = hscroll.GetScrollCursorRect(rtScrollH);
                                    if (rtcur.HasValue) thm.DrawBox(e.Graphics, rtcur.Value, cCur, BorderColor, RoundType.All, BoxStyle.Fill);
                                }
                                break;
                            #endregion
                            #region Vertical
                            case ScrollMode.Vertical:
                                {
                                    thm.DrawBox(e.Graphics, rtScrollV, thm.ScrollBarColor, BorderColor, Columns.Count > 0 ? RoundType.RB : RoundType.R, BoxStyle.Border | BoxStyle.Fill | BoxStyle.OutShadow);

                                    var cCur = thm.ScrollCursorOffColor;
                                    if (vscroll.IsScrolling || vscroll.IsTouchMoving) cCur = thm.ScrollCursorOnColor;

                                    var rtcur = vscroll.GetScrollCursorRect(rtScrollV);
                                    if (rtcur.HasValue) thm.DrawBox(e.Graphics, rtcur.Value, cCur, BorderColor, RoundType.All, BoxStyle.Fill);
                                }
                                break;
                            #endregion
                            #region Both
                            case ScrollMode.Both:
                                {
                                    thm.DrawBox(e.Graphics, rtScrollH, thm.ScrollBarColor, BorderColor, RoundType.LB, BoxStyle.Border | BoxStyle.Fill | BoxStyle.OutShadow);
                                    thm.DrawBox(e.Graphics, rtScrollV, thm.ScrollBarColor, BorderColor, RoundType.Rect, BoxStyle.Border | BoxStyle.Fill | BoxStyle.OutShadow);
                                    thm.DrawBox(e.Graphics, rtScrollR, thm.ScrollBarColor, BorderColor, Columns.Count > 0 ? RoundType.RB : RoundType.R, BoxStyle.Border | BoxStyle.Fill | BoxStyle.OutShadow);

                                    var cCurH = thm.ScrollCursorOffColor;
                                    if (hscroll.IsScrolling || hscroll.IsTouchMoving) cCurH = thm.ScrollCursorOnColor;
                                    var rtcurH = hscroll.GetScrollCursorRect(rtScrollH);
                                    if (rtcurH.HasValue) thm.DrawBox(e.Graphics, rtcurH.Value, cCurH, BorderColor, RoundType.All, BoxStyle.Fill);

                                    var cCurV = thm.ScrollCursorOffColor;
                                    if (vscroll.IsScrolling || vscroll.IsTouchMoving) cCurV = thm.ScrollCursorOnColor;
                                    var rtcurV = vscroll.GetScrollCursorRect(rtScrollV);
                                    if (rtcurV.HasValue) thm.DrawBox(e.Graphics, rtcurV.Value, cCurV, BorderColor, RoundType.All, BoxStyle.Fill);
                                }
                                break;
                                #endregion
                        }
                        #endregion

                        e.Graphics.ResetClip();
                    }

                    thm.DrawBox(e.Graphics, rtContent, BoxColor, BorderColor, RoundType.All, Box.Border());
                    #endregion
                }
            });

            #region Dispose
            p.Dispose();
            br.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            ClearInput();

            vscroll.TouchMode = hscroll.TouchMode = GetTheme()?.TouchMode ?? false;

            Areas((rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR) =>
            {
                var x = e.X;
                var y = e.Y;
                var thm = GetTheme();
                if (thm != null)
                {
                    #region Bounds
                    var rts = GetColumnBounds(rtColumn, rtScrollContent);
                    var spw = Convert.ToInt32(SPECIAL_CELL_WIDTH);
                    var sbw = Convert.ToInt32(SELECTOR_BOX_WIDTH);

                    var rtColumnV = Util.FromRect(rtColumn.Left, rtColumn.Top, rtScrollContent.Width, rtColumn.Height);
                    var ColWidths = GetColumnsWidths(rtScrollContent);
                    var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                    var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);
                    #endregion
                    #region Column
                    foreach (var col in Columns)
                    {
                        var rt = rts["rtColumn" + col.Name];
                        col.MouseDown(rt, x, y);
                    }
                    #endregion
                    #region SelectorAll
                    if (SelectionMode == DvDataGridSelectionMode.Selector)
                    {
                        var wh = Convert.ToInt32(SELECTOR_BOX_WIDTH);
                        var rtSelector = Util.FromRect(rtColumn.Left, rtColumn.Top, spw, rtColumn.Height);
                        var rtSelectorBox = MathTool.MakeRectangle(rtSelector, new SizeF(wh, wh));

                        if (CollisionTool.Check(rtSelectorBox, x, y))
                        {
                            var bAllSelect = GetRows().Where(x => x.Selected).Count() > 0;
                            foreach (var v in GetRows()) v.Selected = !bAllSelect;
                        }
                    }
                    #endregion
                    #region Rows
                    if (CollisionTool.Check(rtScrollContent, x, y))
                    {
                        #region Column Index 
                        var rtnm = "rtColumn";
                        var lsnf = Columns.Where(x => !x.Fixed).ToList(); var vsnf = lsnf.FirstOrDefault(); var venf = lsnf.LastOrDefault(); var mrtNF = (RectangleF?)null;
                        int? isnf = null, ienf = null;
                        if (vsnf != null && venf != null && rts.ContainsKey(rtnm + vsnf.Name) && rts.ContainsKey(rtnm + venf.Name))
                        {
                            var rtsv = rts[rtnm + vsnf.Name];
                            var rtev = rts[rtnm + venf.Name];
                            var mrt = Util.FromRect(rtsv.Left, rtColumn.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtColumn.Height);

                            var vls = lsnf.Where(x => CollisionTool.Check(mrt, Util.INT(Util.FromRect(rts[rtnm + x.Name].Left + hspos, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                            isnf = Columns.IndexOf(vls.FirstOrDefault());
                            ienf = Columns.IndexOf(vls.LastOrDefault());
                            mrtNF = mrt;
                        }

                        var lsf = Columns.Where(x => x.Fixed).ToList(); var vsf = lsf.FirstOrDefault(); var vef = lsf.LastOrDefault(); var mrtF = (RectangleF?)null;
                        int? isf = null, ief = null;
                        if (vsf != null && vef != null && rts.ContainsKey(rtnm + vsf.Name) && rts.ContainsKey(rtnm + vef.Name))
                        {
                            var rtsv = rts[rtnm + vsf.Name];
                            var rtev = rts[rtnm + vef.Name];
                            var mrt = Util.FromRect(rtsv.Left, rtColumn.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);

                            var vls = lsf.Where(x => CollisionTool.Check(mrt, Util.INT(Util.FromRect(rts[rtnm + x.Name].Left, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                            isf = Columns.IndexOf(vls.FirstOrDefault());
                            ief = Columns.IndexOf(vls.LastOrDefault());
                            mrtF = mrt;
                        }

                        rtnm = "rtColumnGroup";
                        var lsgnf = ColumnGroups.Where(x => !x.Fixed).ToList(); var vsgnf = lsgnf.FirstOrDefault(); var vegnf = lsgnf.LastOrDefault(); var mrtGNF = (RectangleF?)null;
                        int? isgnf = null, iegnf = null;
                        if (vsgnf != null && vegnf != null && rts.ContainsKey(rtnm + vsgnf.Name) && rts.ContainsKey(rtnm + vegnf.Name))
                        {
                            var rtsv = rts[rtnm + vsgnf.Name];
                            var rtev = rts[rtnm + vegnf.Name];
                            var mrt = Util.FromRect(rtsv.Left, rtsv.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtsv.Height);

                            var vls = lsgnf.Where(x => CollisionTool.Check(mrt, Util.INT(Util.FromRect(rts[rtnm + x.Name].Left + hspos, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                            isgnf = ColumnGroups.IndexOf(vls.FirstOrDefault());
                            iegnf = ColumnGroups.IndexOf(vls.LastOrDefault());
                            mrtGNF = mrt;
                        }

                        var lsgf = ColumnGroups.Where(x => x.Fixed).ToList(); var vsgf = lsgf.FirstOrDefault(); var vegf = lsgf.LastOrDefault(); var mrtGF = (RectangleF?)null;
                        int? isgf = null, iegf = null;
                        if (vsgf != null && vegf != null && rts.ContainsKey(rtnm + vsgf.Name) && rts.ContainsKey(rtnm + vegf.Name))
                        {
                            var rtsv = rts[rtnm + vsgf.Name];
                            var rtev = rts[rtnm + vegf.Name];
                            var mrt = Util.FromRect(rtsv.Left, rtsv.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);

                            var vls = lsgf.Where(x => CollisionTool.Check(mrt, Util.INT(Util.FromRect(rts[rtnm + x.Name].Left, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                            isgf = ColumnGroups.IndexOf(vls.FirstOrDefault());
                            iegf = ColumnGroups.IndexOf(vls.LastOrDefault());
                            mrtGF = mrt;
                        }
                        #endregion

                        if (!VisibleDropDown)
                        {
                            Loop((i, rtROW, v) =>
                            {
                                #region Selector
                                if (SelectionMode == DvDataGridSelectionMode.Selector)
                                {
                                    var rt = Util.INT(Util.FromRect(rtROW.Left, rtROW.Top, spw, rtROW.Height));
                                    var rtSelectorBox = MathTool.MakeRectangle(rt, new SizeF(sbw, sbw));
                                    if (CollisionTool.Check(rtSelectorBox, x, y))
                                    {
                                        v.Selected = !v.Selected;
                                    }
                                }
                                #endregion
                                #region !Fixed
                                if (mrtNF.HasValue && isnf.HasValue && ienf.HasValue)
                                {
                                    var vls = v.Cells.GetRange(isnf.Value, ienf.Value - isnf.Value + 1).ToList();
                                    var mrt = Util.FromRect(mrtNF.Value.Left, rtScrollContent.Top, mrtNF.Value.Width, rtScrollContent.Height);
                                    foreach (var cell in vls)
                                    {
                                        if (cell.Visible)
                                        {
                                            var rtCol = rts["rtColumn" + cell.Column.Name];
                                            var rt = Util.FromRect(rtCol.Left, rtROW.Top, rtCol.Width, rtROW.Height); rt.Offset(hspos, 0);

                                            if (cell.ColSpan > 1 && cell.ColumnIndex + cell.ColSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColSpan).Sum();
                                            if (cell.RowSpan > 1 && cell.RowIndex + cell.RowSpan <= Rows.Count) rt.Height = Rows.GetRange(cell.RowIndex, cell.RowSpan).Sum(x => x.RowHeight);
                                            cell.MouseDown(Util.INT(rt), x, y);
                                        }
                                    }
                                }
                                #endregion
                                #region Fixed
                                if (mrtF.HasValue && isf.HasValue && ief.HasValue)
                                {
                                    var vls = v.Cells.GetRange(isf.Value, ief.Value - isf.Value + 1).ToList();
                                    var mrt = Util.FromRect(mrtF.Value.Left, rtScrollContent.Top, mrtF.Value.Width + 1, rtScrollContent.Height);
                                    foreach (var cell in vls)
                                    {
                                        if (cell.Visible)
                                        {
                                            var rtCol = rts["rtColumn" + cell.Column.Name];
                                            var rt = Util.FromRect(rtCol.Left, rtROW.Top, rtCol.Width, rtROW.Height);
                                            if (cell.ColSpan > 1 && cell.ColumnIndex + cell.ColSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColSpan).Sum();
                                            if (cell.RowSpan > 1 && cell.RowIndex + cell.RowSpan <= Rows.Count) rt.Height = Rows.GetRange(cell.RowIndex, cell.RowSpan).Sum(x => x.RowHeight);
                                            cell.MouseDown(Util.INT(rt), x, y);
                                        }
                                    }
                                }
                                #endregion
                            });
                        }
                    }
                    #endregion

                    #region Scroll / Touch
                    if (ScrollMode == ScrollMode.Vertical)
                    {
                        vscroll.MouseDown(x, y, rtScrollV);
                        if (vscroll.TouchMode && CollisionTool.Check(rtScrollContent, x, y) && !CollisionTool.Check(rtScrollV, x, y)) vscroll.TouchDown(x, y);
                    }
                    else if (ScrollMode == ScrollMode.Horizon)
                    {
                        hscroll.MouseDown(x, y, rtScrollH);
                        if (hscroll.TouchMode && CollisionTool.Check(rtScrollContent, x, y) && !CollisionTool.Check(rtScrollH, x, y)) hscroll.TouchDown(x, y);
                    }
                    else
                    {
                        vscroll.MouseDown(x, y, rtScrollV);
                        hscroll.MouseDown(x, y, rtScrollH);
                        if (hscroll.TouchMode && CollisionTool.Check(rtScrollContent, x, y) && !CollisionTool.Check(rtScrollH, x, y)) hscroll.TouchDown(x, y);
                        if (vscroll.TouchMode && CollisionTool.Check(rtScrollContent, x, y) && !CollisionTool.Check(rtScrollV, x, y)) vscroll.TouchDown(x, y);
                    }
                    #endregion
                }
            });
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Areas((rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR) =>
            {
                var x = e.X;
                var y = e.Y;
                var thm = GetTheme();
                if (thm != null)
                {
                    #region Bounds
                    var rts = GetColumnBounds(rtColumn, rtScrollContent);
                    var spw = Convert.ToInt32(SPECIAL_CELL_WIDTH);

                    var rtColumnV = Util.FromRect(rtColumn.Left, rtColumn.Top, rtScrollContent.Width, rtColumn.Height);
                    var ColWidths = GetColumnsWidths(rtScrollContent);
                    var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                    var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);
                    #endregion
                    #region Scroll / Touch
                    if (ScrollMode == ScrollMode.Vertical)
                    {
                        vscroll.MouseMove(x, y, rtScrollV);
                        if (vscroll.TouchMode) vscroll.TouchMove(x, y);
                    }
                    else if (ScrollMode == ScrollMode.Horizon)
                    {
                        hscroll.MouseMove(x, y, rtScrollH);
                        if (hscroll.TouchMode) hscroll.TouchMove(x, y);
                    }
                    else
                    {
                        vscroll.MouseMove(x, y, rtScrollV);
                        if (vscroll.TouchMode) vscroll.TouchMove(x, y);

                        hscroll.MouseMove(x, y, rtScrollH);
                        if (hscroll.TouchMode) hscroll.TouchMove(x, y);
                    }
                    #endregion
                }
            });
            Invalidate();
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR) =>
            {
                var x = e.X;
                var y = e.Y;
                var thm = GetTheme();
                if (thm != null)
                {
                    #region Bounds
                    var rts = GetColumnBounds(rtColumn, rtScrollContent);
                    var spw = Convert.ToInt32(SPECIAL_CELL_WIDTH);
                    var sbw = Convert.ToInt32(SELECTOR_BOX_WIDTH);

                    var rtColumnV = Util.FromRect(rtColumn.Left, rtColumn.Top, rtScrollContent.Width, rtColumn.Height);
                    var ColWidths = GetColumnsWidths(rtScrollContent);
                    var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                    var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);
                    #endregion
                    #region Scroll / Touch
                    if (ScrollMode == ScrollMode.Vertical)
                    {
                        vscroll.MouseUp(x, y);
                        if (vscroll.TouchMode) vscroll.TouchUp(x, y);
                    }
                    else if (ScrollMode == ScrollMode.Horizon)
                    {
                        hscroll.MouseUp(x, y);
                        if (hscroll.TouchMode) hscroll.TouchUp(x, y);
                    }
                    else
                    {
                        vscroll.MouseUp(x, y);
                        if (vscroll.TouchMode) vscroll.TouchUp(x, y);

                        hscroll.MouseUp(x, y);
                        if (hscroll.TouchMode) hscroll.TouchUp(x, y);
                    }
                    #endregion
                    #region Rows
                    #region Column Index 

                    var rtnm = "rtColumn";
                    var lsnf = Columns.Where(x => !x.Fixed).ToList(); var vsnf = lsnf.FirstOrDefault(); var venf = lsnf.LastOrDefault(); var mrtNF = (RectangleF?)null;
                    int? isnf = null, ienf = null;
                    if (vsnf != null && venf != null && rts.ContainsKey(rtnm + vsnf.Name) && rts.ContainsKey(rtnm + venf.Name))
                    {
                        var rtsv = rts[rtnm + vsnf.Name];
                        var rtev = rts[rtnm + venf.Name];
                        var mrt = Util.FromRect(rtsv.Left, rtColumn.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtColumn.Height);

                        var vls = lsnf.Where(x => CollisionTool.Check(mrt, Util.INT(Util.FromRect(rts[rtnm + x.Name].Left + hspos, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                        isnf = Columns.IndexOf(vls.FirstOrDefault());
                        ienf = Columns.IndexOf(vls.LastOrDefault());
                        mrtNF = mrt;
                    }

                    var lsf = Columns.Where(x => x.Fixed).ToList(); var vsf = lsf.FirstOrDefault(); var vef = lsf.LastOrDefault(); var mrtF = (RectangleF?)null;
                    int? isf = null, ief = null;
                    if (vsf != null && vef != null && rts.ContainsKey(rtnm + vsf.Name) && rts.ContainsKey(rtnm + vef.Name))
                    {
                        var rtsv = rts[rtnm + vsf.Name];
                        var rtev = rts[rtnm + vef.Name];
                        var mrt = Util.FromRect(rtsv.Left, rtColumn.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);

                        var vls = lsf.Where(x => CollisionTool.Check(mrt, Util.INT(Util.FromRect(rts[rtnm + x.Name].Left, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                        isf = Columns.IndexOf(vls.FirstOrDefault());
                        ief = Columns.IndexOf(vls.LastOrDefault());
                        mrtF = mrt;
                    }

                    rtnm = "rtColumnGroup";
                    var lsgnf = ColumnGroups.Where(x => !x.Fixed).ToList(); var vsgnf = lsgnf.FirstOrDefault(); var vegnf = lsgnf.LastOrDefault(); var mrtGNF = (RectangleF?)null;
                    int? isgnf = null, iegnf = null;
                    if (vsgnf != null && vegnf != null && rts.ContainsKey(rtnm + vsgnf.Name) && rts.ContainsKey(rtnm + vegnf.Name))
                    {
                        var rtsv = rts[rtnm + vsgnf.Name];
                        var rtev = rts[rtnm + vegnf.Name];
                        var mrt = Util.FromRect(rtsv.Left, rtsv.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtsv.Height);

                        var vls = lsgnf.Where(x => CollisionTool.Check(mrt, Util.INT(Util.FromRect(rts[rtnm + x.Name].Left + hspos, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                        isgnf = ColumnGroups.IndexOf(vls.FirstOrDefault());
                        iegnf = ColumnGroups.IndexOf(vls.LastOrDefault());
                        mrtGNF = mrt;
                    }

                    var lsgf = ColumnGroups.Where(x => x.Fixed).ToList(); var vsgf = lsgf.FirstOrDefault(); var vegf = lsgf.LastOrDefault(); var mrtGF = (RectangleF?)null;
                    int? isgf = null, iegf = null;
                    if (vsgf != null && vegf != null && rts.ContainsKey(rtnm + vsgf.Name) && rts.ContainsKey(rtnm + vegf.Name))
                    {
                        var rtsv = rts[rtnm + vsgf.Name];
                        var rtev = rts[rtnm + vegf.Name];
                        var mrt = Util.FromRect(rtsv.Left, rtsv.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);

                        var vls = lsgf.Where(x => CollisionTool.Check(mrt, Util.INT(Util.FromRect(rts[rtnm + x.Name].Left, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                        isgf = ColumnGroups.IndexOf(vls.FirstOrDefault());
                        iegf = ColumnGroups.IndexOf(vls.LastOrDefault());
                        mrtGF = mrt;
                    }
                    #endregion

                    var SelectedRows = Rows.Where(x => x.Selected).ToList();
                    var bSelectionChange = false;

                    if (!VisibleDropDown)
                    {
                        Loop((i, rtROW, v) =>
                        {
                            #region MultiSelect
                            if (SelectionMode == DvDataGridSelectionMode.Multi)
                            {
                                if (CollisionTool.Check(rtROW, x, y))
                                {
                                    if (SelectedRows.Contains(v))
                                    {
                                        SelectedRows.Remove(v);
                                        bSelectionChange = true;
                                    }
                                    else
                                    {
                                        SelectedRows.Add(v);
                                        bSelectionChange = true;
                                    }
                                }
                            }
                            #endregion
                            #region SingleSelect
                            if (SelectionMode == DvDataGridSelectionMode.Single)
                            {
                                if (CollisionTool.Check(rtROW, x, y))
                                {
                                    SelectedRows.Clear();
                                    SelectedRows.Add(v);
                                    bSelectionChange = true;
                                }
                            }
                            #endregion
                        });

                        foreach (var v in Rows) v.Selected = SelectedRows.Contains(v);

                        Loop((i, rtROW, v) =>
                        {
                            #region !Fixed
                            if (mrtNF.HasValue && isnf.HasValue && ienf.HasValue)
                            {
                                var vls = v.Cells.GetRange(isnf.Value, ienf.Value - isnf.Value + 1).ToList();
                                var mrt = Util.FromRect(mrtNF.Value.Left, rtScrollContent.Top, mrtNF.Value.Width, rtScrollContent.Height);
                                foreach (var cell in vls)
                                {
                                    if (cell.Visible)
                                    {
                                        var rtCol = rts["rtColumn" + cell.Column.Name];
                                        var rt = Util.FromRect(rtCol.Left, rtROW.Top, rtCol.Width, rtROW.Height); rt.Offset(hspos, 0);

                                        if (cell.ColSpan > 1 && cell.ColumnIndex + cell.ColSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColSpan).Sum();
                                        if (cell.RowSpan > 1 && cell.RowIndex + cell.RowSpan <= Rows.Count) rt.Height = Rows.GetRange(cell.RowIndex, cell.RowSpan).Sum(x => x.RowHeight);
                                        cell.MouseUp(Util.INT(rt), x, y);
                                    }
                                }
                            }
                            #endregion
                            #region Fixed
                            if (mrtF.HasValue && isf.HasValue && ief.HasValue)
                            {
                                var vls = v.Cells.GetRange(isf.Value, ief.Value - isf.Value + 1).ToList();
                                var mrt = Util.FromRect(mrtF.Value.Left, rtScrollContent.Top, mrtF.Value.Width + 1, rtScrollContent.Height);
                                foreach (var cell in vls)
                                {
                                    if (cell.Visible)
                                    {
                                        var rtCol = rts["rtColumn" + cell.Column.Name];
                                        var rt = Util.FromRect(rtCol.Left, rtROW.Top, rtCol.Width, rtROW.Height);
                                        if (cell.ColSpan > 1 && cell.ColumnIndex + cell.ColSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColSpan).Sum();
                                        if (cell.RowSpan > 1 && cell.RowIndex + cell.RowSpan <= Rows.Count) rt.Height = Rows.GetRange(cell.RowIndex, cell.RowSpan).Sum(x => x.RowHeight);
                                        cell.MouseUp(Util.INT(rt), x, y);
                                    }
                                }
                            }
                            #endregion
                        });

                    }
                    if (bSelectionChange) SelectedChanged?.Invoke(this, null);
                    #endregion
                }
            });
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseClick
        protected override void OnMouseClick(MouseEventArgs e)
        {
            Areas((rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR) =>
            {
                var x = e.X;
                var y = e.Y;
                var thm = GetTheme();
                if (thm != null)
                {
                    #region Bounds
                    var rts = GetColumnBounds(rtColumn, rtScrollContent);
                    var spw = Convert.ToInt32(SPECIAL_CELL_WIDTH);
                    var sbw = Convert.ToInt32(SELECTOR_BOX_WIDTH);

                    var rtColumnV = Util.FromRect(rtColumn.Left, rtColumn.Top, rtScrollContent.Width, rtColumn.Height);
                    var ColWidths = GetColumnsWidths(rtScrollContent);
                    var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                    var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);
                    #endregion
                    #region Filter
                    foreach (var col in Columns)
                    {
                        var rtFilter = rts["rtFilter" + col.Name];
                        if (col.UseFilter && CollisionTool.Check(rtFilter, x, y))
                        {
                            var ret = DvDialogs.InputBox.ShowString("필터 : " + col.HeaderText, col.FilterText);
                            if (ret != null)
                            {
                                col.FilterText = ret;
                                RefreshRows();
                                MovingStop();
                            }
                        }
                    }
                    #endregion
                }
            });
            base.OnMouseClick(e);
        }
        #endregion
        #region OnMouseWheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            ClearInput();
            Areas((rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR) =>
            {
                if (CollisionTool.Check(rtContent, e.Location))
                {
                    if (ScrollMode == ScrollMode.Vertical || ScrollMode == ScrollMode.Both) vscroll.MouseWheel(e.Delta, rtScrollV);
                    else hscroll.MouseWheel(e.Delta, rtScrollV);
                    Invalidate();
                }
            });
            base.OnMouseWheel(e);
        }
        #endregion
        #endregion

        #region Method
        #region Clear
        public void Clear()
        {
            SelectionMode = DvDataGridSelectionMode.Single;
            ScrollMode = ScrollMode.Vertical;
            bNotRaiseEvent = true;
            objs = null;
            ColumnGroups.Clear();
            Columns.Clear();
            SummaryRows.Clear();
            Rows.Clear();
            bNotRaiseEvent = false;
            RefreshRows();
        }
        #endregion
        #region Draw
        void DrawColumnBorder(Graphics g, DvTheme thm, RectangleF rtColumn, RectangleF rtScrollContent, RectangleF rt, bool bevel = true)
        {
            var BoxColor = GetBoxColor(thm);
            var ColumnColor = GetColumnColor(thm);
            var BorderColor = thm.GetBorderColor(BoxColor, BackColor);
            var BevelColor = ColumnColor.BrightnessTransmit(thm.DataGridColumnBevelBright);
            var spw = Convert.ToInt32(SPECIAL_CELL_WIDTH);

            using (var p = new Pen(BorderColor))
            {
                p.Width = 1;

                var l = Convert.ToInt32(rt.Left);
                var r = Convert.ToInt32(rt.Right);
                var t = Convert.ToInt32(rt.Top);
                var b = Convert.ToInt32(rt.Bottom);
                var nx = rt.Left == 1 ? 0 : 1;
                var ny = rt.Top == 1 ? 0 : 1;

                if (bevel && Bevel)
                {
                    p.Color = BevelColor;
                    g.DrawLine(p, r - 1, t + ny, l + 1, t + ny);
                    g.DrawLine(p, l + nx, t + 1, l + nx, b - 1);
                }

                p.Color = BorderColor;
                g.DrawLine(p, l, b, r, b);
                if (Math.Abs(rtScrollContent.Left - rt.Left) > 3) g.DrawLine(p, l, t, l, b);
                if (Math.Abs(rtScrollContent.Right - rt.Right) > 3) g.DrawLine(p, r, t, r, b);
            }
        }
        #endregion
        #region DataSource
        #region SetDataSource<T>
        public void SetDataSource<T>(IEnumerable<T> values)
        {
            objs = values;
            bNotRaiseEvent = true;
            DataType = typeof(T);
            var props = typeof(T).GetProperties();
            int nCnt = Columns.Where(x => props.Select(v => v.Name).Contains(x.Name) || x is DvDataGridButtonColumn).Count();
            if (nCnt == Columns.Count)
            {
                var dic = props.ToDictionary(x => x.Name);
                Rows.Clear();
                if (values != null)
                {
                    int ri = 0;
                    foreach (var src in values)
                    {
                        var row = new DvDataGridRow(this) { Source = src };

                        for (int i = 0; i < Columns.Count; i++)
                        {
                            var col = Columns[i];
                            if (col is DvDataGridButtonColumn)
                            {
                                var cell = new DvDataGridButtonCell(this, row, (DvDataGridButtonColumn)col);
                                row.Cells.Add(cell);
                            }
                            else
                            {
                                var prop = dic[col.Name];
                                var cell = Activator.CreateInstance(col.CellType, this, row, col) as IDvDataGridCell;
                                row.Cells.Add(cell);
                            }
                        }

                        Rows.Add(row);
                        ri++;
                    }
                }
                RefreshRows();
                bModSize = true;
            }
            else throw new Exception("VALID COUNT");
            bNotRaiseEvent = false;

            Invalidate();
        }
        #endregion
        #region ResetDataSource
        public void ResetDataSource<T>()
        {
            var SummaryRowColor = GetSummaryRowColor(GetTheme());
            if (objs != null)
            {
                var values = (IEnumerable<T>)objs;
                SetDataSource<T>(values);

                foreach (var srow in SummaryRows)
                    foreach (var cell in srow.Cells)
                    {
                        cell.CellBackColor = SummaryRowColor;

                        var prop = cell.GetType().GetProperty("CellTextColor");
                        if (prop != null) prop.SetValue(cell, ForeColor);
                    }
            }
        }
        #endregion
        #endregion
        #region Color
        public Color GetBoxColor(DvTheme Theme) => this.BoxColor ?? Theme.ListBackColor;
        public Color GetColumnColor(DvTheme Theme) => this.ColumnColor ?? Theme.ColumnColor;
        public Color GetRowColor(DvTheme Theme) => this.RowColor ?? Theme.RowColor;
        public Color GetInputColor(DvTheme Theme) => this.InputColor ?? Theme.InputColor;
        public Color GetSelectedRowColor(DvTheme Theme) => this.SelectedRowColor ?? Theme.PointColor;
        public Color GetSummaryRowColor(DvTheme Theme) => this.SummaryRowColor ?? Theme.RowColor.BrightnessTransmit(-0.1F);
        #endregion
        #region Column
        #region GetColumnRowCount
        private int GetColumnRowCount()
        {
            int ret = 0;
            foreach (var col in Columns)
            {
                var ls = new List<IDvDataGridColumn>();
                ReentCol(col, ls);
                ret = Math.Max(ls.Count, ret);
            }
            return ret;
        }

        private void ReentCol(IDvDataGridColumn col, List<IDvDataGridColumn> ls)
        {
            ls.Add(col);

            if (col.GroupName != null)
            {
                var v = ColumnGroups.Where(x => x.Name == col.GroupName).FirstOrDefault();
                if (v != null) ReentCol(v, ls);
            }
        }
        #endregion
        #region GetColumnBounds
        Dictionary<string, RectangleF> GetColumnBounds(RectangleF rtColumn, RectangleF rtScrollContent)
        {
            var ret = new Dictionary<string, RectangleF>();
            var colrc = GetColumnRowCount();
            var spw = Convert.ToInt32(SPECIAL_CELL_WIDTH);

            #region Column
            var xws = new List<RectangleF>();
            #region XWs
            var cws = GetColumnsWidths(rtScrollContent);
            if (cws.Count == Columns.Count)
            {
                var x = rtScrollContent.Left + (SelectionMode == DvDataGridSelectionMode.Selector ? spw : 0);

                for (int i = 0; i < cws.Count; i++)
                {
                    var w = cws[i];
                    xws.Add(Util.FromRect(x, rtColumn.Top, w, 0));
                    x += w;
                }
            }
            else throw new Exception("Column Count Mismatch");
            #endregion
            #region Column / Filter
            if (Columns.Where(x => x.UseFilter).Count() > 0)
            {
                for (int i = 0; i < Columns.Count; i++)
                {
                    var rt = xws[i];
                    var col = Columns[i];
                    var ls = new List<IDvDataGridColumn>();
                    ReentCol(col, ls);
                    var nc = colrc - ls.Count + 1;

                    var rtFilterV = Util.FromRect(rt.Left, rtColumn.Bottom - ColumnHeight, (rt.Width), ColumnHeight);
                    var rtColumnV = Util.FromRect(rt.Left, rtFilterV.Top - (ColumnHeight * nc), (rt.Width), (ColumnHeight * nc));

                    ret.Add("rtColumn" + col.Name, rtColumnV);
                    ret.Add("rtFilter" + col.Name, rtFilterV);
                }
            }
            else
            {
                for (int i = 0; i < Columns.Count; i++)
                {
                    var rt = xws[i];
                    var col = Columns[i];
                    var ls = new List<IDvDataGridColumn>();
                    ReentCol(col, ls);
                    var nc = colrc - ls.Count + 1;

                    var rtColumnV = Util.FromRect(rt.Left, rtColumn.Bottom - (ColumnHeight * nc), (rt.Width), (ColumnHeight * nc));

                    ret.Add("rtColumn" + col.Name, rtColumnV);
                    ret.Add("rtFilter" + col.Name, Util.FromRect(rtColumnV.Left, rtColumnV.Top, rtColumnV.Width, 0));
                }
            }
            #endregion
            #region ColumnGroup
            for (int i = 0; i < ColumnGroups.Count; i++)
            {
                var rt = xws[i];
                var col = Columns[i];
                var v = ColumnGroups[i];
                var vls = new List<IDvDataGridColumn>();
                var Depth = GetColumnDepth(v);
                GetColumnChildList(v, vls);

                int imin = vls.Min(_x => Columns.IndexOf(_x));
                int imax = vls.Max(_x => Columns.IndexOf(_x));

                var x = (xws[imin].Left);
                var y = rtColumn.Top + ((colrc - Depth) * ColumnHeight);
                var w = (xws[imax].Right) - Convert.ToInt32(xws[imin].Left);
                var h = ColumnHeight;

                ret.Add("rtColumnGroup" + v.Name, Util.FromRect(x, y, w, h));
            }
            #endregion
            #endregion

            return ret;
        }
        #endregion
        #region GetColumnsWidths
        List<float> GetColumnsWidths(RectangleF rtScrollContent)
        {
            var ret = new List<float>();
            var spw = Convert.ToInt32(SPECIAL_CELL_WIDTH);
            var tw = rtScrollContent.Width - (SelectionMode == DvDataGridSelectionMode.Selector ? spw : 0);
            var cw = tw - Convert.ToSingle(Columns.Where(x => x.SizeMode == DvSizeMode.Pixel).Sum(x => x.Width));
            foreach (var v in Columns) ret.Add(v.SizeMode == DvSizeMode.Pixel ? Convert.ToSingle(v.Width) : Convert.ToSingle((decimal)cw * (v.Width / 100M)));
            return ret;
        }
        #endregion
        #region GetColumnDepth
        int GetColumnDepth(IDvDataGridColumn col)
        {
            if (ColumnGroups.Where(x => x.GroupName == col.Name).Count() > 0)
            {
                return ColumnGroups.Where(x => x.GroupName == col.Name).Select(x => GetColumnDepth(x) + 1).Max();
            }
            else if (Columns.Where(x => x.GroupName == col.Name).Count() > 0)
            {
                return Columns.Where(x => x.GroupName == col.Name).Select(x => GetColumnDepth(x) + 1).Max();
            }
            else return 1;
        }
        #endregion
        #region GetColumnChildList
        void GetColumnChildList(IDvDataGridColumn col, List<IDvDataGridColumn> ls)
        {
            if (col != null)
            {
                if (Columns.Contains(col)) { ls.Add(col); }
                else
                {
                    if (Columns.Count(x => x.GroupName == col.Name) > 0) ls.AddRange(Columns.Where(x => x.GroupName == col.Name));
                    else if (ColumnGroups.Count(x => x.GroupName == col.Name) > 0)
                    {
                        var vls = ColumnGroups.Where(x => x.GroupName == col.Name);
                        foreach (var v in vls) GetColumnChildList(v, ls);
                    }
                }
            }
        }
        #endregion
        #endregion
        #region Row
        #region GetRows
        internal List<DvDataGridRow> GetRows()
        {
            return mrows;
        }
        #endregion
        #region RefreshRows
        public void RefreshRows()
        {
            mrows.Clear();
            mrows.AddRange(Rows);

            #region Sort
            {
                var cols = Columns.Where(x => x.UseSort).ToList();
                var ls = cols.Where(x => x.UseSort).OrderBy(x => x.SortOrder);
                if (ls.Count() > 0)
                {
                    IOrderedEnumerable<DvDataGridRow> result = null;
                    bool bFirst = true;
                    foreach (var col in ls)
                    {
                        int i = Columns.IndexOf(col);
                        switch (col.SortState)
                        {
                            case DvDataGridColumnSortState.Asc:
                                if (bFirst) { result = mrows.OrderBy(x => x.Cells[i].Value); bFirst = false; }
                                else if (result != null)
                                {
                                    result = result.ThenBy(x => x.Cells[i].Value);
                                }
                                break;
                            case DvDataGridColumnSortState.Desc:
                                if (bFirst) { result = mrows.OrderByDescending(x => x.Cells[i].Value); bFirst = false; }
                                else if (result != null)
                                {
                                    result = result.ThenByDescending(x => x.Cells[i].Value);
                                }
                                break;
                        }
                    }

                    if (result != null)
                    {
                        var l = result.ToList();
                        mrows.Clear();
                        mrows.AddRange(l);
                    }
                }
            }
            #endregion
            #region Filter
            {
                var cols = Columns.ToList();
                for (int i = 0; i < cols.Count; i++)
                {
                    if (cols[i].UseFilter && !string.IsNullOrWhiteSpace(cols[i].FilterText))
                    {
                        mrows = mrows.Where(m => (m.Cells[i].Value != null ? m.Cells[i].Value.ToString().ToLower().IndexOf(cols[i].FilterText.ToLower()) != -1 : false)).ToList();
                    }
                }
            }
            #endregion
            #region Make DBSearch
            {
                var lsv = mrows;
                var sum = lsv.Sum(x => x.RowHeight);
                if (PrevTotalHeight != sum)
                {
                    vST = PrevTotalHeight = sum;
                    lsp.Clear();
                    var nsum = 0;
                    foreach (var v in lsv) { lsp.Add(new _DGSearch_() { Height = v.RowHeight, Sum = nsum, Row = v }); nsum += v.RowHeight; }
                }
            }
            #endregion
            #region Summary Calc
            foreach (var row in SummaryRows)
            {
                foreach (var v in row.Cells.Where(x => x is DvDataGridSummaryCell)) ((DvDataGridSummaryCell)v).Calculation();
            }
            #endregion
        }
        #endregion
        #endregion
        #region Invoke
        #region InvokeColumnMouseDown
        public void InvokeColumnMouseDown(IDvDataGridColumn column, int x, int y)
        {
            if (!bNotRaiseEvent && ColumnMouseDown != null) ColumnMouseDown.Invoke(this, new ColumnMouseEventArgs(column, x, y));
        }
        #endregion
        #region InvokeColumnMouseUp
        public void InvokeColumnMouseUp(IDvDataGridColumn column, int x, int y)
        {
            if (!bNotRaiseEvent && ColumnMouseUp != null) ColumnMouseUp.Invoke(this, new ColumnMouseEventArgs(column, x, y));
        }
        #endregion
        #region InvokeCellMouseDown
        public void InvokeCellMouseDown(IDvDataGridCell cell, int x, int y)
        {
            if (!bNotRaiseEvent && CellMouseDown != null) CellMouseDown.Invoke(this, new CellMouseEventArgs(cell, x, y));
        }
        #endregion
        #region InvokeCellMouseUp
        public void InvokeCellMouseUp(IDvDataGridCell cell, int x, int y)
        {
            if (!bNotRaiseEvent && CellMouseUp != null) CellMouseUp.Invoke(this, new CellMouseEventArgs(cell, x, y));
        }
        #endregion
        #region InvokeCellMouseClick
        public void InvokeCellMouseClick(IDvDataGridCell cell, int x, int y)
        {
            if (!bNotRaiseEvent && CellMouseClick != null) CellMouseClick.Invoke(this, new CellMouseEventArgs(cell, x, y));
        }
        #endregion
        #region InvokeCellDoubleClick
        public void InvokeCellDoubleClick(IDvDataGridCell cell, int x, int y)
        {
            if (!bNotRaiseEvent && CellMouseDoubleClick != null) CellMouseDoubleClick.Invoke(this, new CellMouseEventArgs(cell, x, y));
        }
        #endregion
        #region InvokeSortChanged
        public void InvokeSortChanged()
        {
            RefreshRows();
            if (!bNotRaiseEvent && SortChanged != null) SortChanged.Invoke(this, new EventArgs());
        }
        #endregion
        #region InvokeValueChanged
        public void InvokeValueChanged(IDvDataGridCell cell, object oldValue, object newValue)
        {
            if (!bNotRaiseEvent && ValueChanged != null) ValueChanged.Invoke(this, new CellValueChangedEventArgs(cell, oldValue, newValue));
        }
        #endregion
        #region InvokeCellButtonClick
        public void InvokeCellButtonClick(DvDataGridCell cell)
        {
            if (!bNotRaiseEvent && CellButtonClick != null) CellButtonClick.Invoke(this, new CellButtonClickEventArgs(cell));
        }
        #endregion
        #endregion

        #region SetCellFocus
        private void SetCellFocus(DvDataGridCell cell, RectangleF rt)
        {
            var type = cell.GetType();

            if (type == typeof(DvDataGridEditTextCell)) ((DvDataGridEditTextCell)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<byte>)) ((DvDataGridEditNumberCell<byte>)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<ushort>)) ((DvDataGridEditNumberCell<ushort>)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<uint>)) ((DvDataGridEditNumberCell<uint>)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<ulong>)) ((DvDataGridEditNumberCell<ulong>)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<sbyte>)) ((DvDataGridEditNumberCell<sbyte>)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<short>)) ((DvDataGridEditNumberCell<short>)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<int>)) ((DvDataGridEditNumberCell<int>)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<long>)) ((DvDataGridEditNumberCell<long>)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<float>)) ((DvDataGridEditNumberCell<float>)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<double>)) ((DvDataGridEditNumberCell<double>)cell).SetFocus(rt);
            else if (type == typeof(DvDataGridEditNumberCell<decimal>)) ((DvDataGridEditNumberCell<decimal>)cell).SetFocus(rt);
        }
        #endregion
        #region GetCellBounds
        private RectangleF? GetCellBounds(int ri, int ci, DvDataGridCell cell)
        {
            RectangleF? ret = null;

            Areas((rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR) =>
            {
                #region Bounds
                var rts = GetColumnBounds(rtColumn, rtScrollContent);
                var rtsc = rtScrollContent;
                var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);
                #endregion
                #region Make DBSearch
                if (bModSize)
                {
                    var lsv = mrows;
                    var sum = lsv.Sum(x => x.RowHeight);
                    if (PrevTotalHeight != sum)
                    {
                        vST = PrevTotalHeight = sum;
                        lsp.Clear();
                        var nsum = 0;
                        foreach (var v in lsv) { lsp.Add(new _DGSearch_() { Height = v.RowHeight, Sum = nsum, Row = v }); nsum += v.RowHeight; }
                    }
                    bModSize = false;
                }
                #endregion
                #region Index Calc
                var ls = GetRows();
                var startidx = (int)MathTool.Constrain(BNSearch(lsp, 0, lsp.Count - 1, rtScrollContent.Top, vspos, rtScrollContent.Top) - 1, 0, ls.Count - 1);
                var endidx = (int)MathTool.Constrain(BNSearch(lsp, 0, lsp.Count - 1, rtScrollContent.Top, vspos, rtScrollContent.Bottom) + 1, 0, ls.Count - 1);
                #endregion

                if (ls.Count > 0)
                {
                    #region Var
                    var v = ls[ri];
                    var y = Convert.ToInt32(rtScrollContent.Top + lsp[ri].Sum + vspos);
                    var rtROW = Util.FromRect(rtScrollContent.Left, y, rtScrollContent.Width, v.RowHeight);
                    var rtColumnV = Util.FromRect(rtColumn.Left, rtColumn.Top, rtScrollContent.Width, rtColumn.Height);
                    #endregion
                    #region Column Index 
                    var rtnm = "rtColumn";
                    var lsnf = Columns.Where(x => !x.Fixed).ToList(); var vsnf = lsnf.FirstOrDefault(); var venf = lsnf.LastOrDefault(); var mrtNF = (RectangleF?)null;
                    int? isnf = null, ienf = null;
                    if (vsnf != null && venf != null && rts.ContainsKey(rtnm + vsnf.Name) && rts.ContainsKey(rtnm + venf.Name))
                    {
                        var rtsv = rts[rtnm + vsnf.Name];
                        var rtev = rts[rtnm + venf.Name];
                        var mrt = Util.FromRect(rtsv.Left, rtColumn.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtColumn.Height);

                        var vls = lsnf.Where(x => CollisionTool.Check(mrt, (Util.FromRect(rts[rtnm + x.Name].Left + hspos, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                        isnf = Columns.IndexOf(vls.FirstOrDefault());
                        ienf = Columns.IndexOf(vls.LastOrDefault());
                        mrtNF = mrt;
                    }

                    var lsf = Columns.Where(x => x.Fixed).ToList(); var vsf = lsf.FirstOrDefault(); var vef = lsf.LastOrDefault(); var mrtF = (RectangleF?)null;
                    int? isf = null, ief = null;
                    if (vsf != null && vef != null && rts.ContainsKey(rtnm + vsf.Name) && rts.ContainsKey(rtnm + vef.Name))
                    {
                        var rtsv = rts[rtnm + vsf.Name];
                        var rtev = rts[rtnm + vef.Name];
                        var mrt = Util.FromRect(rtsv.Left, rtColumn.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);

                        var vls = lsf.Where(x => CollisionTool.Check(mrt, (Util.FromRect(rts[rtnm + x.Name].Left, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                        isf = Columns.IndexOf(vls.FirstOrDefault());
                        ief = Columns.IndexOf(vls.LastOrDefault());
                        mrtF = mrt;
                    }

                    rtnm = "rtColumnGroup";
                    var lsgnf = ColumnGroups.Where(x => !x.Fixed).ToList(); var vsgnf = lsgnf.FirstOrDefault(); var vegnf = lsgnf.LastOrDefault(); var mrtGNF = (RectangleF?)null;
                    int? isgnf = null, iegnf = null;
                    if (vsgnf != null && vegnf != null && rts.ContainsKey(rtnm + vsgnf.Name) && rts.ContainsKey(rtnm + vegnf.Name))
                    {
                        var rtsv = rts[rtnm + vsgnf.Name];
                        var rtev = rts[rtnm + vegnf.Name];
                        var mrt = Util.FromRect(rtsv.Left, rtsv.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtsv.Height);

                        var vls = lsgnf.Where(x => CollisionTool.Check(mrt, (Util.FromRect(rts[rtnm + x.Name].Left + hspos, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                        isgnf = ColumnGroups.IndexOf(vls.FirstOrDefault());
                        iegnf = ColumnGroups.IndexOf(vls.LastOrDefault());
                        mrtGNF = mrt;
                    }

                    var lsgf = ColumnGroups.Where(x => x.Fixed).ToList(); var vsgf = lsgf.FirstOrDefault(); var vegf = lsgf.LastOrDefault(); var mrtGF = (RectangleF?)null;
                    int? isgf = null, iegf = null;
                    if (vsgf != null && vegf != null && rts.ContainsKey(rtnm + vsgf.Name) && rts.ContainsKey(rtnm + vegf.Name))
                    {
                        var rtsv = rts[rtnm + vsgf.Name];
                        var rtev = rts[rtnm + vegf.Name];
                        var mrt = Util.FromRect(rtsv.Left, rtsv.Top, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);

                        var vls = lsgf.Where(x => CollisionTool.Check(mrt, (Util.FromRect(rts[rtnm + x.Name].Left, rts[rtnm + x.Name].Top, rts[rtnm + x.Name].Width, rts[rtnm + x.Name].Height)))).ToList();
                        isgf = ColumnGroups.IndexOf(vls.FirstOrDefault());
                        iegf = ColumnGroups.IndexOf(vls.LastOrDefault());
                        mrtGF = mrt;
                    }
                    #endregion
                    #region Result
                    var rtCol = rts["rtColumn" + cell.Column.Name];
                    var rt = Util.FromRect(rtCol.Left, rtROW.Top, rtCol.Width, rtROW.Height); rt.Offset(hspos, 0);
                    #endregion

                    ret = rt;
                }
            });

            return ret;
        }
        #endregion
        #region Loop
        private void Loop(Action<int, RectangleF, DvDataGridRow> Func)
        {
            Areas((rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR) =>
            {
                #region Bounds
                var rts = GetColumnBounds(rtColumn, rtScrollContent);
                var rtsc = rtScrollContent;
                var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);
                #endregion
                #region Make DBSearch
                if (bModSize)
                {
                    var lsv = mrows;
                    var sum = lsv.Sum(x => x.RowHeight);
                    if (PrevTotalHeight != sum)
                    {
                        vST = PrevTotalHeight = sum;
                        lsp.Clear();
                        var nsum = 0;
                        foreach (var v in lsv) { lsp.Add(new _DGSearch_() { Height = v.RowHeight, Sum = nsum, Row = v }); nsum += v.RowHeight; }
                    }
                    bModSize = false;
                }
                #endregion
                #region Index Calc
                var ls = GetRows();
                var startidx = (int)MathTool.Constrain(BNSearch(lsp, 0, lsp.Count - 1, rtScrollContent.Top, vspos, rtScrollContent.Top) - 1, 0, ls.Count - 1);
                var endidx = (int)MathTool.Constrain(BNSearch(lsp, 0, lsp.Count - 1, rtScrollContent.Top, vspos, rtScrollContent.Bottom) + 1, 0, ls.Count - 1);
                #endregion

                if (ls.Count > 0)
                {
                    for (int i = startidx; i <= endidx; i++)
                    {
                        var v = ls[i];
                        var y = Convert.ToInt32(rtScrollContent.Top + lsp[i].Sum + vspos);
                        var rtITM = Util.FromRect(rtScrollContent.Left, y, rtScrollContent.Width, v.RowHeight);
                        Func(i, rtITM, v);
                    }
                }
            });
        }

        #region BNSearch
        int BNSearch(List<_DGSearch_> ls, int si, int ei, float top, int vpos, float value)
        {
            int idx = (int)MathTool.Map((double)1, 0, 2, si, ei);
            if (si != ei && idx != si && idx != ei)
            {
                if (idx >= 0 && idx < ls.Count)
                {
                    if (value > ls[idx].Top + vpos + top)
                    {
                        return BNSearch(ls, idx, ei, top, vpos, value);
                    }
                    else if (value < ls[idx].Top + vpos + top)
                    {
                        return BNSearch(ls, si, idx, top, vpos, value);
                    }
                    else return idx;
                }
                return idx;
            }
            else return idx;
        }
        #endregion
        #endregion

        #region MovingStop
        public void MovingStop()
        {
            vscroll.TouchStop();
            hscroll.TouchStop();
        }
        #endregion

        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();

            var scwh = Convert.ToInt32(Scroll.SC_WH);
            var colrc = GetColumnRowCount();
            var columnH = (colrc + (Columns.Where(x => x.UseFilter).Count() > 0 ? 1 : 0)) * ColumnHeight;
            var summaryH = SummaryRows.Count * RowHeight;

            var ush = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Horizon;
            var usv = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Vertical;

            var ColWidths = GetColumnsWidths(Util.FromRect(rtContent.Left, rtContent.Bottom, rtContent.Width - (usv ? scwh : 0), rtContent.Height - columnH - (ush ? scwh : 0)));
            var cfx = Columns.Where(x => x.Fixed).LastOrDefault();
            var nfx = (cfx != null ? ColWidths.GetRange(0, Columns.IndexOf(cfx)+1).Sum() + 1 : 0) + (SelectionMode == DvDataGridSelectionMode.Selector ? SPECIAL_CELL_WIDTH : 0);

            var rtColumn = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, columnH);
            var rtBox = Util.FromRect(rtColumn.Left, rtColumn.Bottom, rtContent.Width - (usv ? scwh : 0), rtContent.Height - columnH - (ush ? scwh : 0));
            var rtSummary = Util.FromRect(rtBox.Left, rtBox.Bottom - summaryH, rtContent.Width - (usv ? scwh : 0), summaryH);
            var rtScrollContent = Util.FromRect(rtBox.Left, rtBox.Top, rtBox.Width, rtBox.Height - summaryH);
            var rtScrollArea = Util.FromRect(nfx, rtBox.Top, rtBox.Width - nfx, rtBox.Height - summaryH);
            var rtScrollV = Util.FromRect(rtBox.Right, rtBox.Top, usv ? scwh : 0, rtBox.Height);
            var rtScrollH = Util.FromRect(rtBox.Left, rtBox.Bottom, rtBox.Width, ush ? scwh : 0);
            var rtScrollR = Util.FromRect(rtScrollH.Right, rtScrollV.Bottom, usv ? scwh : 0, ush ? scwh : 0);

            act(rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR);
        }
        #endregion

        #region Input
        #region SetInput
        public void SetInput(DvDataGridCell cell, RectangleF rtValue, Color BackColor, string Value)
        {
            var Wnd = FindForm() as DvForm;
            var Theme = GetTheme();
            if (Theme.KeyboardInput)
            {
                Areas((rtContent, rtColumn, rtBox, rtSummary, rtScrollContent, rtScrollArea, rtScrollV, rtScrollH, rtScrollR) =>
                {
                    if (CollisionTool.Check(rtScrollContent, rtValue))
                    {
                        #region Left
                        if (rtValue.Left < rtScrollArea.Left)
                        {
                            var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);
                            hscroll.ScrollPosition = MathTool.Constrain(-hspos + rtValue.Left, 0, hscroll.ScrollTotal) - rtScrollArea.Left;
                        }
                        #endregion
                        #region Right
                        if (rtValue.Right > rtScrollArea.Right)
                        {
                            var hspos = Convert.ToInt32(hscroll.ScrollPositionWithOffset);
                            hscroll.ScrollPosition = MathTool.Constrain(-hspos + rtValue.Right - hscroll.ScrollView, 0, hscroll.ScrollTotal) - rtScrollArea.Left;
                        }
                        #endregion
                        #region Down
                        if (rtValue.Bottom > rtScrollContent.Bottom)
                        {
                            var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                            vscroll.ScrollPosition = MathTool.Constrain(-vspos + rtValue.Top - vscroll.ScrollView, 0, vscroll.ScrollTotal);
                        }
                        #endregion
                        #region Up
                        if (rtValue.Top < rtScrollContent.Top)
                        {
                            var vspos = Convert.ToInt32(vscroll.ScrollPositionWithOffset);
                            vscroll.ScrollPosition = MathTool.Constrain(-vspos + rtValue.Top - vscroll.ScrollTick, 0, vscroll.ScrollTotal);
                        }
                        #endregion

                        var rt = GetCellBounds(cell.RowIndex, cell.ColumnIndex, cell);
                        if (rt.HasValue)
                        {
                            OriginalTextBox.BackColor = BackColor;
                            OriginalTextBox.ForeColor = ForeColor;
                            OriginalTextBox.Text = Value;
                            OriginalTextBox.Tag = cell;
                            AlignInput(rt.Value);
                            OriginalTextBox.Visible = true;
                            OriginalTextBox.Focus();
                        }
                    }
                });
            }
            else OriginalTextBox.Visible = false;
            Invalidate();
        }
        #endregion
        #region ClearInput
        void ClearInput()
        {
            var Wnd = FindForm() as DvForm;
            var Theme = GetTheme();
            if (Theme.KeyboardInput)
            {
                FlushInput();

                OriginalTextBox.Tag = null;
                OriginalTextBox.Visible = false;
            }
            else OriginalTextBox.Visible = false;
        }
        #endregion
        #region FlushInput
        void FlushInput()
        {
            var Wnd = FindForm() as DvForm;
            var Theme = GetTheme();
            if (Theme.KeyboardInput)
            {
                var node = OriginalTextBox.Tag as DvDataGridCell;
                if (node != null)
                {
                    var s = node.GetType().Name;
                    if (node is DvDataGridEditTextCell)
                    {
                        ((DvDataGridEditTextCell)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditTextCell)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<byte>)
                    {
                        ((DvDataGridEditNumberCell<byte>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<byte>)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<ushort>)
                    {
                        ((DvDataGridEditNumberCell<ushort>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<ushort>)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<uint>)
                    {
                        ((DvDataGridEditNumberCell<uint>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<uint>)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<ulong>)
                    {
                        ((DvDataGridEditNumberCell<ulong>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<ulong>)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<sbyte>)
                    {
                        ((DvDataGridEditNumberCell<sbyte>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<sbyte>)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<short>)
                    {
                        ((DvDataGridEditNumberCell<short>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<short>)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<int>)
                    {
                        ((DvDataGridEditNumberCell<int>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<int>)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<long>)
                    {
                        ((DvDataGridEditNumberCell<long>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<long>)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<float>)
                    {
                        ((DvDataGridEditNumberCell<float>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<float>)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<double>)
                    {
                        ((DvDataGridEditNumberCell<double>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<double>)node).Flush();
                    }
                    else if (node is DvDataGridEditNumberCell<decimal>)
                    {
                        ((DvDataGridEditNumberCell<decimal>)node).TextChange(OriginalTextBox);
                        ((DvDataGridEditNumberCell<decimal>)node).Flush();
                    }

                }
                Invalidate();
            }
            else OriginalTextBox.Visible = false;
        }
        #endregion
        #region AlignInput
        void AlignInput(RectangleF rtValue)
        {
            var Wnd = FindForm() as DvForm;
            var Theme = GetTheme();
            if (Theme.KeyboardInput)
            {
                #region Align
                OriginalTextBox.TextAlign = HorizontalAlignment.Center;
                #endregion
                bool bv = this.Enabled && (Wnd == null || (Wnd != null && !Wnd.Block)) && (Theme?.KeyboardInput ?? false);
                if (bv != OriginalTextBox.Visible) OriginalTextBox.Visible = bv;

                var sz = TextRenderer.MeasureText(Text, Font);
                var sz2 = TextRenderer.MeasureText("H", Font);
                var rt = Util.FromRect(rtValue, new Padding(5));
                var rtText = Util.MakeRectangleAlign(rt, new SizeF(rt.Width, Math.Max(Convert.ToInt32(sz2.Height), Convert.ToInt32(sz.Height))), DvContentAlignment.MiddleCenter);

                OriginalTextBox.Bounds = Util.INT(rtText);
            }
            else OriginalTextBox.Visible = false;
        }
        #endregion
        #region CheckInput
        public bool CheckInput(DvDataGridCell cell) => cell == OriginalTextBox.Tag && OriginalTextBox.Visible;
        #endregion
        #endregion
        #endregion
    }
}
