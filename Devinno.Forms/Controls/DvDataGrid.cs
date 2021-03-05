using Devinno.Collections;
using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public partial class DvDataGrid : DvControl
    {
        #region Const 
        private const int SPECIAL_CELL_WIDTH = 30;
        private const int SELECTOR_BOX_WIDTH = 18;
        internal const double InputBright = -0.2;
        internal const double BoxBright = -0.3;
        internal const double ColumnBevelBright = 0.3;
        #endregion

        #region Properties
        #region BoxColor
        private Color cBoxColor = DvTheme.DefaultTheme.Color2;
        public Color BoxColor
        {
            get => cBoxColor;
            set
            {
                if (cBoxColor != value)
                {
                    cBoxColor = value;
                    bInv = true;
                }
            }
        }
        #endregion
        #region ColumnColor
        private Color cColumnColor = DvTheme.DefaultTheme.ColumnColor;
        public Color ColumnColor
        {
            get { return cColumnColor; }
            set
            {
                if (cColumnColor != value)
                {
                    cColumnColor = value;
                    bInv = true;
                }
            }
        }
        #endregion
        #region RowColor
        private Color cRowColor = DvTheme.DefaultTheme.Color3;
        public Color RowColor
        {
            get { return cRowColor; }
            set
            {
                if (cRowColor != value)
                {
                    cRowColor = value;
                    bInv = true;
                }
            }
        }
        #endregion
        #region SelectedRowColor
        private Color cSelectedRowColor = DvTheme.DefaultTheme.PointColor;
        public Color SelectedRowColor
        {
            get { return cSelectedRowColor; }
            set
            {
                if (cSelectedRowColor != value)
                {
                    cSelectedRowColor = value;
                    bInv = true;
                }
            }
        }
        #endregion
        #region SummaryRowColor
        private Color cSummaryRowColor = DvTheme.DefaultTheme.Color1;
        public Color SummaryRowColor
        {
            get { return cSummaryRowColor; }
            set
            {
                if (cSummaryRowColor != value)
                {
                    cSummaryRowColor = value;
                    bInv = true;
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
                    bInv = true;
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
                    bInv = true;
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
        public List<DvDataGridRow> Rows { get; private set; } = new List<DvDataGridRow>();
        #endregion
        #region SummaryRows
        [Browsable(false)]
        public List<DvDataGridSummaryRow> SummaryRows { get; private set; } = new List<DvDataGridSummaryRow>();
        #endregion

        #region SelectionMode
        public DvDataGridSelectionMode SelectionMode { get; set; } = DvDataGridSelectionMode.SINGLE;
        #endregion
        #region AutoSet
        public bool AutoSet { get; set; } = false;
        #endregion

        #region ScrollMode
        private ScrollMode eScrollMode = ScrollMode.Vertical;
        public ScrollMode ScrollMode
        {
            get => eScrollMode;
            set
            {
                if(eScrollMode != value)
                {
                    eScrollMode = value;
                    bInv = true;
                }
            }
        }
        #endregion
        #region TouchMode
        public bool TouchMode
        {
            get => vscroll.TouchMode;
            set => vscroll.TouchMode = hscroll.TouchMode = value;
        }
        #endregion

        #region TextShadow
        public bool TextShadow { get; set; } = true;
        #endregion
        #region RowBevel
        public bool RowBevel { get; set; } = true;
        #endregion
        #endregion

        #region Event

        #endregion

        #region Member Variable
        List<DvDataGridRow> mrows = new List<DvDataGridRow>();
        Scroll vscroll = new Scroll() {  Direction = ScrollDirection.Vertical };
        Scroll hscroll = new Scroll() { Direction = ScrollDirection.Horizon };

        bool bNotRaiseEvent = false;
        object objs = null;
        int PrevTotalHeight = -1;
        List<_DGSearch_> lsp = new List<_DGSearch_>();

        bool bInv = false;
        #endregion

        #region Event
        public event EventHandler SelectedChanged;
        public event EventHandler SortChanged;

        public event EventHandler<ColumnMouseEventArgs> ColumnMouseDown;
        public event EventHandler<ColumnMouseEventArgs> ColumnMouseUp;
        public event EventHandler<CellMouseEventArgs> CellMouseDown;
        public event EventHandler<CellMouseEventArgs> CellMouseUp;
        public event EventHandler<CellMouseEventArgs> CellMouseDoubleClick;
        public event EventHandler<CellValueChangedEventArgs> ValueChanged;
        public event EventHandler<CellButtonClickEventArgs> CellButtonClick;

        public event EventHandler AutoSetChanged;
        #endregion

        #region Constructor
        public DvDataGrid()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            #region hscroll
            hscroll.Direction = ScrollDirection.Horizon;
            hscroll.GetScrollTotal = () =>
            {
                if (Areas.ContainsKey("rtScrollContent"))
                {
                    var cols = GetColumnsWidths(Areas["rtScrollContent"]);
                    return Columns.Where(x => !x.Fixed).Select(x => cols[Columns.IndexOf(x)]).Sum();
                }
                else return 0;
            };
            hscroll.GetScrollTick = () => Areas.ContainsKey("rtScrollContent") ? Areas["rtScrollContent"].Width : 1;
            hscroll.GetScrollView = () =>
            {
                if (Areas.ContainsKey("rtScrollContent"))
                {
                    var cols = GetColumnsWidths(Areas["rtScrollContent"]);
                    return Areas["rtScrollContent"].Width - Columns.Where(x => x.Fixed).Select(x => cols[Columns.IndexOf(x)]).Sum();
                }
                else return 0;
            };
            #endregion
            #region vscroll
            vscroll.Direction = ScrollDirection.Vertical;
            vscroll.GetScrollTotal = () => GetRows().Sum(x => x.RowHeight);
            vscroll.GetScrollTick = () => RowHeight;
            vscroll.GetScrollView = () => Areas.ContainsKey("rtScrollContent") ? Areas["rtScrollContent"].Height : 0;
            #endregion

            #region Refresh Thread
            var th = new Thread(new ThreadStart(() => { 
            
                while(true)
                {
                    try
                    {
                        if (bInv || vscroll.IsTouchMoving || hscroll.IsTouchMoving || vscroll.IsScrolling || hscroll.IsScrolling || vscroll.IsTouchScrolling || hscroll.IsTouchScrolling)
                        {
                            this.Invoke(new Action(Invalidate));
                            bInv = false;
                        }
                    }
                    catch { }
                    Thread.Sleep(10);
                }
            
            })) { IsBackground = true };
            th.Start();
            #endregion

            Size = new Size(300, 200);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];

            var f = DpiRatio;
            var scwh = Convert.ToInt32(Scroll.SC_WH * f);
            var colrc = GetColumnRowCount();
            var columnH = (colrc + (Columns.Where(x => x.UseFilter).Count() > 0 ? 1 : 0)) * ColumnHeight;
            var summaryH = SummaryRows.Count * RowHeight;

            var ush = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Horizon;
            var usv = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Vertical;

            #region Base
            var rtColumn = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, columnH);
            var rtBox = new Rectangle(rtColumn.X, rtColumn.Bottom, rtContent.Width - (usv ? scwh : 0), rtContent.Height - columnH - (ush ? scwh : 0));
            var rtSummary = new Rectangle(rtBox.X, rtBox.Bottom - summaryH, rtContent.Width - (usv ? scwh : 0), summaryH);
            var rtScrollContent = new Rectangle(rtBox.X, rtBox.Y, rtBox.Width, rtBox.Height - summaryH);
            var rtScrollV = new Rectangle(rtBox.Right, rtBox.Top, usv ? scwh : 0, rtBox.Height);
            var rtScrollH = new Rectangle(rtBox.Left, rtBox.Bottom, rtBox.Width, ush ? scwh : 0);
            var rtScrollR = new Rectangle(rtScrollH.Right, rtScrollV.Bottom, usv ? scwh : 0, ush ? scwh : 0);

            SetArea("rtColumn", rtColumn);
            SetArea("rtBox", rtBox);
            SetArea("rtScrollContent", rtScrollContent);
            SetArea("rtSummary", rtSummary);
            SetArea("rtScrollV", rtScrollV);
            SetArea("rtScrollH", rtScrollH);
            SetArea("rtScrollR", rtScrollR);
            #endregion
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BoxColor = GetBoxColor(Theme);
            var ColumnColor = GetColumnColor(Theme);
            var RowColor = GetRowColor(Theme);
            var SelectRowColor = GetSelectedRowColor(Theme);
            var SummaryRowColor = GetSummaryRowColor(Theme);
            var ColumnBorderColor = BackColor.BrightnessTransmit(Theme.BorderBright);
            var ColumnBevelColor = ColumnColor.BrightnessTransmit(Theme.InBevelBright);
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
            var rtColumn = Areas["rtColumn"];
            var rtBox = Areas["rtBox"];
            var rtScrollContent = Areas["rtScrollContent"];
            var rtSummary = Areas["rtSummary"];
            var rtScrollV = Areas["rtScrollV"];
            var rtScrollH = Areas["rtScrollH"];
            var rtScrollR = Areas["rtScrollR"];
            var rtColumnV = new Rectangle(rtColumn.X, rtColumn.Y, rtScrollContent.Width, rtColumn.Height);

            var f = DpiRatio;
            var ush = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Horizon;
            var usv = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Vertical;
            var useFilter = Columns.Where(x => x.UseFilter).Count() > 0;
            var colrc = GetColumnRowCount() + (useFilter ? 1 : 0);
            var ColWidths = GetColumnsWidths(rtScrollContent);
            var rts = GetColumnBounds(rtColumn, rtScrollContent);
            var spw = Convert.ToInt32(f * SPECIAL_CELL_WIDTH);
            var sbw = Convert.ToInt32(f * SELECTOR_BOX_WIDTH);
            var vspos = vscroll.ScrollTotal > vscroll.ScrollView ? Convert.ToInt32(MathTool.Constrain(vscroll.ScrollPositionWithOffset, -(vscroll.ScrollTotal - vscroll.ScrollView), 0)) : 0;
            var hspos = hscroll.ScrollTotal > hscroll.ScrollView ? Convert.ToInt32(MathTool.Constrain(hscroll.ScrollPositionWithOffset, -(hscroll.ScrollTotal - hscroll.ScrollView), 0)) : 0;
            #endregion
            #region Draw
            #region Column
            #region ColumnBox
            Theme.DrawBox(e.Graphics, ColumnColor, BackColor, rtColumn, RoundType.T, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
            if (rtColumn.Right != rtScrollContent.Right)
            {
                p.Width = 1;
                p.Color = ColumnBorderColor;
                e.Graphics.DrawLine(p, rtScrollContent.Right, rtColumn.Top, rtScrollContent.Right, rtColumn.Bottom);
            }

            if (SelectionMode == DvDataGridSelectionMode.SELECTOR)
            {
                p.Width = 1;
                p.Color = ColumnBorderColor;
                e.Graphics.DrawLine(p, rtScrollContent.Left + spw, rtColumn.Top, rtScrollContent.Left + spw, rtColumn.Bottom);
            }
            #endregion
            e.Graphics.SetClip(rtColumnV);
            #region Column
            {
                #region !Fixed
                var lsnf = Columns.Where(x => !x.Fixed).ToList();
                var vsnf = lsnf.FirstOrDefault();
                var venf = lsnf.LastOrDefault();
                if (vsnf != null && venf != null && rts.ContainsKey("rtColumn" + vsnf.Name) && rts.ContainsKey("rtColumn" + venf.Name))
                {
                    var rtsv = rts["rtColumn" + vsnf.Name];
                    var rtev = rts["rtColumn" + venf.Name];
                    var mrt = new RectangleF(rtsv.Left, rtColumn.Y, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtColumn.Height);
                    var vls = lsnf.Where(x => CollisionTool.Check(mrt, RTI(new RectangleF(rts["rtColumn" + x.Name].X + hspos, rts["rtColumn" + x.Name].Y, rts["rtColumn" + x.Name].Width, rts["rtColumn" + x.Name].Height)))).ToList();
                    e.Graphics.SetClip(new RectangleF(mrt.X, mrt.Y, mrt.Width + 1, mrt.Height), CombineMode.Intersect);
                    foreach (var col in vls)
                    {
                        #region Column
                        {
                            var rt = RTI(rts["rtColumn" + col.Name]); rt.Offset(hspos, 0);
                            DrawColumnBox(e.Graphics, Theme, rtColumn, rtScrollContent, rt);
                            col.Paint(Theme, e.Graphics, rt);
                        }
                        #endregion
                        #region Filter
                        {
                            var rt = RTI(rts["rtFilter" + col.Name]); rt.Offset(hspos, 0);
                            var n2 = Convert.ToInt32(f * 2);
                            var rtin = new RectangleF(rt.X, rt.Y, rt.Width, rt.Height); rtin.Inflate(-n2, -n2);
                            DrawColumnBox(e.Graphics, Theme, rtColumn, rtScrollContent, rt);
                            if (col.UseFilter)
                            {
                                br.Color = ColumnColor.BrightnessTransmit(InputBright); e.Graphics.FillRectangle(br, rtin);
                                p.Color = ColumnColor.BrightnessTransmit(Theme.BorderBright); e.Graphics.DrawRectangle(p, rtin);
                                Theme.DrawTextShadow(e.Graphics, null, col.FilterText, Font, ForeColor, br.Color, RTI(rt));
                            }
                        }
                        #endregion
                    }
                    e.Graphics.ResetClip();
                    e.Graphics.SetClip(rtColumnV);
                }
                #endregion
                #region Fixed
                var lsf = Columns.Where(x => x.Fixed).ToList();
                var vsf = lsf.FirstOrDefault();
                var vef = lsf.LastOrDefault();
                if (vsf != null && vef != null && rts.ContainsKey("rtColumn" + vsf.Name) && rts.ContainsKey("rtColumn" + vef.Name))
                {
                    var rtsv = rts["rtColumn" + vsf.Name];
                    var rtev = rts["rtColumn" + vef.Name];
                    var mrt = new RectangleF(rtsv.Left, rtColumn.Y, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);
                    var vls = lsf.Where(x => CollisionTool.Check(mrt, RTI(new RectangleF(rts["rtColumn" + x.Name].X, rts["rtColumn" + x.Name].Y, rts["rtColumn" + x.Name].Width, rts["rtColumn" + x.Name].Height)))).ToList();
                    e.Graphics.SetClip(new RectangleF(mrt.X, mrt.Y, mrt.Width + 1, mrt.Height), CombineMode.Intersect);
                    foreach (var col in vls)
                    {
                        #region Column
                        {
                            var rt = rts["rtColumn" + col.Name];
                            DrawColumnBox(e.Graphics, Theme, rtColumn, rtScrollContent, rt);
                            col.Paint(Theme, e.Graphics, rt);
                        }
                        #endregion
                        #region Filter
                        {
                            var rt = rts["rtFilter" + col.Name];
                            var n2 = Convert.ToInt32(f * 2);
                            var rtin = new RectangleF(rt.X, rt.Y, rt.Width, rt.Height); rtin.Inflate(-n2, -n2);
                            DrawColumnBox(e.Graphics, Theme, rtColumn, rtScrollContent, rt);
                            if (col.UseFilter)
                            {
                                br.Color = ColumnColor.BrightnessTransmit(InputBright); e.Graphics.FillRectangle(br, rtin);
                                p.Color = ColumnColor.BrightnessTransmit(Theme.BorderBright); e.Graphics.DrawRectangle(p, rtin);
                                Theme.DrawTextShadow(e.Graphics, null, col.FilterText, Font, ForeColor, br.Color, RTI(rt));
                            }
                        }
                        #endregion
                    }
                    e.Graphics.ResetClip();
                    e.Graphics.SetClip(rtColumnV);
                }
                #endregion
            }
            #endregion
            #region ColumnGroup
            {
                #region !Fixed
                var lsnf = ColumnGroups.Where(x => !x.Fixed).ToList();
                var vsnf = lsnf.FirstOrDefault();
                var venf = lsnf.LastOrDefault();
                if (vsnf != null && venf != null && rts.ContainsKey("rtColumnGroup" + vsnf.Name) && rts.ContainsKey("rtColumnGroup" + venf.Name))
                {
                    var rtsv = rts["rtColumnGroup" + vsnf.Name];
                    var rtev = rts["rtColumnGroup" + venf.Name];
                    var mrt = new RectangleF(rtsv.Left, rtColumn.Y, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtColumn.Height);
                    var vls = lsnf.Where(x => CollisionTool.Check(mrt, RTI(new RectangleF(rts["rtColumnGroup" + x.Name].X + hspos, rts["rtColumnGroup" + x.Name].Y, rts["rtColumnGroup" + x.Name].Width, rts["rtColumnGroup" + x.Name].Height)))).ToList();

                    e.Graphics.SetClip(new RectangleF(mrt.X, mrt.Y, mrt.Width + 1, mrt.Height), CombineMode.Intersect);
                    foreach (var colgroup in vls)
                    {
                        var rt = RTI(rts["rtColumnGroup" + colgroup.Name]); rt.Offset(hspos, 0);
                        DrawColumnBox(e.Graphics, Theme, rtColumn, rtScrollContent, rt);
                        colgroup.Paint(Theme, e.Graphics, rt);
                    }
                    e.Graphics.ResetClip();
                    e.Graphics.SetClip(rtColumnV);
                }
                #endregion
                #region Fixed
                var lsf = ColumnGroups.Where(x => x.Fixed).ToList();
                var vsf = lsf.FirstOrDefault();
                var vef = lsf.LastOrDefault();
                if (vsf != null && vef != null && rts.ContainsKey("rtColumnGroup" + vsf.Name) && rts.ContainsKey("rtColumnGroup" + vef.Name))
                {
                    var rtsv = rts["rtColumnGroup" + vsf.Name];
                    var rtev = rts["rtColumnGroup" + vef.Name];
                    var mrt = new RectangleF(rtsv.Left, rtColumn.Y, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);
                    var vls = lsf.Where(x => CollisionTool.Check(mrt, RTI(new RectangleF(rts["rtColumnGroup" + x.Name].X, rts["rtColumnGroup" + x.Name].Y, rts["rtColumnGroup" + x.Name].Width, rts["rtColumnGroup" + x.Name].Height)))).ToList();

                    e.Graphics.SetClip(new RectangleF(mrt.X, mrt.Y, mrt.Width + 1, mrt.Height), CombineMode.Intersect);
                    foreach (var colgroup in vls)
                    {
                        var rt = rts["rtColumnGroup" + colgroup.Name];
                        DrawColumnBox(e.Graphics, Theme, rtColumn, rtScrollContent, rt);
                        colgroup.Paint(Theme, e.Graphics, rt);
                    }
                    e.Graphics.ResetClip();
                    e.Graphics.SetClip(rtColumnV);
                }
                #endregion
            }
            #endregion
            #region ColumnSelector
            if (SelectionMode == DvDataGridSelectionMode.SELECTOR)
            {
                var rtSelector = new RectangleF(rtColumn.X, rtColumn.Y, spw, rtColumn.Height);
                DrawColumnBox(e.Graphics, Theme, rtColumn, rtScrollContent, rtSelector);

                var rtSelectorBox = MathTool.MakeRectangle(RTI(rtSelector), new Size(sbw, sbw));
                Theme.DrawBox(e.Graphics, ColumnColor.BrightnessTransmit(BoxBright), ColumnColor, rtSelectorBox, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL | BoxDrawOption.IN_SHADOW);

                var bAllSelect = GetRows().Where(x => x.Selected).Count() > 0;
                if (bAllSelect)
                {
                    Rectangle rtCheck = new Rectangle(rtSelectorBox.X, rtSelectorBox.Y - 0, rtSelectorBox.Width, rtSelectorBox.Height); rtCheck.Inflate(-Convert.ToInt32(4 * f), -Convert.ToInt32(4 * f));
                    Rectangle rtCheckSH = new Rectangle(rtCheck.X, rtCheck.Y + 1, rtCheck.Width, rtCheck.Height);

                    p.Width = Convert.ToInt32(3 * f);
                    p.Color = ForeColor;
                    e.Graphics.DrawLine(p, rtCheck.X, rtCheck.Y + rtCheck.Height / 2, rtCheck.X + rtCheck.Width / 2, rtCheck.Y + rtCheck.Height);
                    e.Graphics.DrawLine(p, rtCheck.X + rtCheck.Width / 2 - 1, rtCheck.Y + rtCheck.Height, rtCheck.X + rtCheck.Width, rtCheck.Y);
                    p.Width = 1;
                }
            }
            #endregion
            e.Graphics.ResetClip();
            #endregion
            #region Box
            Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtScrollContent, RoundType.NONE, BoxDrawOption.BORDER);
            #endregion
            #region Rows
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                
                #region Bounds 
                var lsnf = Columns.Where(x => !x.Fixed).ToList(); var vsnf = lsnf.FirstOrDefault(); var venf = lsnf.LastOrDefault(); var mrtNF = (RectangleF?)null;
                int? isnf = null, ienf = null;
                if (vsnf != null && venf != null && rts.ContainsKey("rtColumn" + vsnf.Name) && rts.ContainsKey("rtColumn" + venf.Name))
                {
                    var rtsv = rts["rtColumn" + vsnf.Name];
                    var rtev = rts["rtColumn" + venf.Name];
                    var mrt = new RectangleF(rtsv.Left, rtColumn.Y, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left, rtColumn.Height);

                    var vls = lsnf.Where(x => CollisionTool.Check(mrt, RTI(new RectangleF(rts["rtColumn" + x.Name].X + hspos, rts["rtColumn" + x.Name].Y, rts["rtColumn" + x.Name].Width, rts["rtColumn" + x.Name].Height)))).ToList();
                    isnf = Columns.IndexOf(vls.FirstOrDefault());
                    ienf = Columns.IndexOf(vls.LastOrDefault());
                    mrtNF = mrt;
                }

                var lsf = Columns.Where(x => x.Fixed).ToList(); var vsf = lsf.FirstOrDefault(); var vef = lsf.LastOrDefault(); var mrtF = (RectangleF?)null;
                int? isf = null, ief = null;
                if (vsf != null && vef != null && rts.ContainsKey("rtColumn" + vsf.Name) && rts.ContainsKey("rtColumn" + vef.Name))
                {
                    var rtsv = rts["rtColumn" + vsf.Name];
                    var rtev = rts["rtColumn" + vef.Name];
                    var mrt = new RectangleF(rtsv.Left, rtColumn.Y, Math.Min(rtColumnV.Right, rtev.Right) - rtsv.Left + 1, rtColumn.Height);

                    var vls = lsf.Where(x => CollisionTool.Check(mrt, RTI(new RectangleF(rts["rtColumn" + x.Name].X, rts["rtColumn" + x.Name].Y, rts["rtColumn" + x.Name].Width, rts["rtColumn" + x.Name].Height)))).ToList();
                    isf = Columns.IndexOf(vls.FirstOrDefault());
                    ief = Columns.IndexOf(vls.LastOrDefault());
                    mrtF = mrt;
                }
                #endregion
                #region Row
                e.Graphics.SetClip(rtScrollContent);
                Loop((i, rtROW, v) =>
                {
                    #region !Fixed
                    if (mrtNF.HasValue && isnf.HasValue && ienf.HasValue)
                    {
                        var vls = v.Cells.GetRange(isnf.Value, ienf.Value - isnf.Value + 1).ToList();
                        var mrt = new RectangleF(mrtNF.Value.X, rtScrollContent.Y, mrtNF.Value.Width, rtScrollContent.Height);
                        e.Graphics.SetClip(mrt, CombineMode.Intersect);
                        foreach (var cell in vls)
                        {
                            if (cell.Visible)
                            {
                                var rtCol = rts["rtColumn" + cell.Column.Name];
                                var rt = new RectangleF(rtCol.X, rtROW.Y, rtCol.Width, rtROW.Height); rt.Offset(hspos, 0);

                                if (cell.ColSpan > 1 && cell.ColumnIndex + cell.ColSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColSpan).Sum();
                                if (cell.RowSpan > 1 && cell.RowIndex + cell.RowSpan <= Rows.Count) rt.Height = Rows.GetRange(cell.RowIndex, cell.RowSpan).Sum(x => x.RowHeight);
                                cell.Paint(Theme, e.Graphics, rt);
                            }
                        }
                        e.Graphics.ResetClip();
                    }
                    #endregion
                    #region Fixed
                    if (mrtF.HasValue && isf.HasValue && ief.HasValue)
                    {
                        var vls = v.Cells.GetRange(isf.Value, ief.Value - isf.Value + 1).ToList();
                        var mrt = new RectangleF(mrtF.Value.X, rtScrollContent.Y, mrtF.Value.Width + 1, rtScrollContent.Height);
                        e.Graphics.SetClip(mrt, CombineMode.Intersect);
                        foreach (var cell in vls)
                        {
                            if (cell.Visible)
                            {
                                var rtCol = rts["rtColumn" + cell.Column.Name];
                                var rt = new RectangleF(rtCol.X, rtROW.Y, rtCol.Width, rtROW.Height);
                                if (cell.ColSpan > 1 && cell.ColumnIndex + cell.ColSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColSpan).Sum();
                                if (cell.RowSpan > 1 && cell.RowIndex + cell.RowSpan <= Rows.Count) rt.Height = Rows.GetRange(cell.RowIndex, cell.RowSpan).Sum(x => x.RowHeight);
                                cell.Paint(Theme, e.Graphics, rt);
                            }
                        }
                        e.Graphics.ResetClip();
                    }
                    #endregion
                });
                e.Graphics.ResetClip();
                #endregion
                #region Summary 
                if (SummaryRows.Count > 0)
                {
                    for (int i = 0; i < SummaryRows.Count; i++)
                    {
                        var v = SummaryRows[i];
                        var rtROW = new Rectangle(rtSummary.X, rtSummary.Y + (i * RowHeight), rtSummary.Width, RowHeight);
                        #region !Fixed
                        if (mrtNF.HasValue && isnf.HasValue && ienf.HasValue)
                        {
                            var vls = v.Cells.GetRange(isnf.Value, ienf.Value - isnf.Value + 1).ToList();
                            var mrt = new RectangleF(mrtNF.Value.X, rtSummary.Y, mrtNF.Value.Width, rtSummary.Height);
                            e.Graphics.SetClip(mrt, CombineMode.Intersect);
                            foreach (var cell in vls)
                            {
                                if (cell.Visible)
                                {
                                    var rtCol = rts["rtColumn" + Columns[cell.ColumnIndex].Name];
                                    var rt = new RectangleF(rtCol.X, rtROW.Y, rtCol.Width, rtROW.Height); rt.Offset(hspos, 0);
                                    if (cell.ColumnSpan > 1 && cell.ColumnIndex + cell.ColumnSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColumnSpan).Sum();
                                    cell.Paint(Theme, e.Graphics, rt);
                                }
                            }
                            e.Graphics.ResetClip();
                        }
                        #endregion
                        #region Fixed
                        if (mrtF.HasValue && isf.HasValue && ief.HasValue)
                        {
                            var vls = v.Cells.GetRange(isf.Value, ief.Value - isf.Value + 1).ToList();
                            var mrt = new RectangleF(mrtF.Value.X, rtSummary.Y, mrtF.Value.Width + 1, rtSummary.Height);
                            e.Graphics.SetClip(mrt, CombineMode.Intersect);
                            foreach (var cell in vls)
                            {
                                if (cell.Visible)
                                {
                                    var rtCol = rts["rtColumn" + Columns[cell.ColumnIndex].Name];
                                    var rt = new RectangleF(rtCol.X, rtROW.Y, rtCol.Width, rtROW.Height);
                                    if (cell.ColumnSpan > 1 && cell.ColumnIndex + cell.ColumnSpan <= ColWidths.Count) rt.Width = (int)ColWidths.GetRange(cell.ColumnIndex, cell.ColumnSpan).Sum();
                                    cell.Paint(Theme, e.Graphics, rt);
                                }
                            }
                            e.Graphics.ResetClip();
                        }
                        #endregion
                    }
                }
                #endregion

                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            }
            #endregion
            #region Scroll
            switch (ScrollMode)
            {
                #region Horizon
                case ScrollMode.Horizon:
                    {
                        Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScrollH, RoundType.B, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);

                        var cCur = Theme.ScrollCursorColor;
                        if (hscroll.IsScrolling) cCur = Theme.ScrollCursorColor.BrightnessTransmit(0.3);
                        else if (hscroll.IsTouchMoving) cCur = Theme.PointColor.BrightnessTransmit(0.3);

                        var rtcur = hscroll.GetScrollCursorRect(rtScrollH);
                        e.Graphics.SetClip(rtScrollH);
                        if (rtcur.HasValue) Theme.DrawBox(e.Graphics, cCur, Theme.ScrollBarColor, rtcur.Value, RoundType.ALL, BoxDrawOption.BORDER);
                        e.Graphics.ResetClip();
                    }
                    break;
                #endregion
                #region Vertical
                case ScrollMode.Vertical:
                    {
                        Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScrollV, RoundType.RB, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);

                        var cCur = Theme.ScrollCursorColor;
                        if (vscroll.IsScrolling) cCur = Theme.ScrollCursorColor.BrightnessTransmit(0.3);
                        else if (vscroll.IsTouchMoving) cCur = Theme.PointColor.BrightnessTransmit(0.3);

                        var rtcur = vscroll.GetScrollCursorRect(rtScrollV);
                        e.Graphics.SetClip(rtScrollV);
                        if (rtcur.HasValue) Theme.DrawBox(e.Graphics, cCur, Theme.ScrollBarColor, rtcur.Value, RoundType.ALL, BoxDrawOption.BORDER);
                        e.Graphics.ResetClip();
                    }
                    break;
                #endregion
                #region Both
                case ScrollMode.Both:
                    {
                        Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScrollH, RoundType.LB, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                        Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScrollV, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                        Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScrollR, RoundType.RB, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);

                        var cCurH = Theme.ScrollCursorColor;
                        if (hscroll.IsScrolling) cCurH = Theme.ScrollCursorColor.BrightnessTransmit(0.3);
                        else if (hscroll.IsTouchMoving) cCurH = Theme.PointColor.BrightnessTransmit(0.3);
                        var rtcurH = hscroll.GetScrollCursorRect(rtScrollH);
                        e.Graphics.SetClip(rtScrollH);
                        if (rtcurH.HasValue) Theme.DrawBox(e.Graphics, cCurH, Theme.ScrollBarColor, rtcurH.Value, RoundType.ALL, BoxDrawOption.BORDER);
                        e.Graphics.ResetClip();

                        var cCurV = Theme.ScrollCursorColor;
                        if (vscroll.IsScrolling) cCurV = Theme.ScrollCursorColor.BrightnessTransmit(0.3);
                        else if (vscroll.IsTouchMoving) cCurV = Theme.PointColor.BrightnessTransmit(0.3);
                        var rtcurV = vscroll.GetScrollCursorRect(rtScrollV);
                        e.Graphics.SetClip(rtScrollV);
                        if (rtcurV.HasValue) Theme.DrawBox(e.Graphics, cCurV, Theme.ScrollBarColor, rtcurV.Value, RoundType.ALL, BoxDrawOption.BORDER);
                        e.Graphics.ResetClip();
                    }
                    break;
                    #endregion
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
            if (ScrollMode == ScrollMode.Horizon) hscroll.MouseWheel(e);
            else if (ScrollMode == ScrollMode.Vertical) vscroll.MouseWheel(e);
            else vscroll.MouseWheel(e);

            bInv = true;
            base.OnMouseWheel(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();

            if (Areas.Count > 1)
            {
                #region Set
                var f = DpiRatio;
                #endregion
                #region Bounds
                var rtContent = Areas["rtContent"];
                var rtScrollContent = Areas["rtScrollContent"];
                var rtColumn = Areas["rtColumn"];
                var rtScrollV = Areas["rtScrollV"];
                var rtScrollH = Areas["rtScrollH"];
                var rts = GetColumnBounds(rtColumn, rtScrollContent);
                var spw = Convert.ToInt32(f * SPECIAL_CELL_WIDTH);
                #endregion
                #region Column
                foreach (var col in Columns)
                {
                    var rt = rts["rtColumn" + col.Name];
                    col.MouseDown(rt, e.Location);
                }
                #endregion
                #region Filter
                foreach (var col in Columns)
                {
                    var rtFilter = rts["rtFilter" + col.Name];
                    if (col.UseFilter && CollisionTool.Check(rtFilter, e.X, e.Y))
                    {

                    }
                }
                #endregion
                #region SelectorAll
                if (SelectionMode == DvDataGridSelectionMode.SELECTOR)
                {
                    var wh = Convert.ToInt32(SELECTOR_BOX_WIDTH * f);
                    var rtSelector = new Rectangle(rtColumn.X, rtColumn.Y, spw, rtColumn.Height);
                    var rtSelectorBox = MathTool.MakeRectangle(rtSelector, new Size(wh, wh));

                    if (CollisionTool.Check(rtSelectorBox, e.Location))
                    {

                    }
                }
                #endregion

                #region Scroll / Touch
                if (ScrollMode == ScrollMode.Vertical)
                {
                    vscroll.MouseDown(e, rtScrollV);
                    if (vscroll.TouchMode && CollisionTool.Check(Areas["rtScrollContent"], e.Location)) vscroll.TouchDown(e);
                }
                else if (ScrollMode == ScrollMode.Horizon)
                {
                    hscroll.MouseDown(e, rtScrollH);
                    if (hscroll.TouchMode && CollisionTool.Check(Areas["rtScrollContent"], e.Location)) hscroll.TouchDown(e);
                }
                else
                {
                    vscroll.MouseDown(e, rtScrollV);
                    hscroll.MouseDown(e, rtScrollH);
                    if (hscroll.TouchMode && CollisionTool.Check(Areas["rtScrollContent"], e.Location)) hscroll.TouchDown(e);
                    if (vscroll.TouchMode && CollisionTool.Check(Areas["rtScrollContent"], e.Location)) vscroll.TouchDown(e);
                }
                #endregion
                bInv = true; 
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtScrollV") && Areas.ContainsKey("rtScrollH"))
            {
                var inv = false;
                #region Scroll / Touch
                if (ScrollMode == ScrollMode.Vertical)
                {
                    vscroll.MouseMove(e, Areas["rtScrollV"]);
                    if (vscroll.TouchMode) vscroll.TouchMove(e);
                    if (vscroll.IsScrolling) inv = true;
                    if (vscroll.TouchMode && vscroll.IsTouchScrolling) inv = true;
                }
                else if (ScrollMode == ScrollMode.Horizon)
                {
                    hscroll.MouseMove(e, Areas["rtScrollH"]);
                    if (hscroll.TouchMode) hscroll.TouchMove(e);
                    if (hscroll.IsScrolling) inv = true;
                    if (hscroll.TouchMode && hscroll.IsTouchScrolling) inv = true;
                }
                else
                {
                    vscroll.MouseMove(e, Areas["rtScrollV"]);
                    if (vscroll.TouchMode) vscroll.TouchMove(e);
                    if (vscroll.IsScrolling) inv = true;
                    if (vscroll.TouchMode && vscroll.IsTouchScrolling) inv = true;

                    hscroll.MouseMove(e, Areas["rtScrollH"]);
                    if (hscroll.TouchMode) hscroll.TouchMove(e);
                    if (hscroll.IsScrolling) inv = true;
                    if (hscroll.TouchMode && hscroll.IsTouchScrolling) inv = true;
                }
                #endregion
                if (inv) bInv = inv;
            }
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtScrollContent"))
            {
                var rtScrollContent = Areas["rtScrollContent"];
                #region Scroll / Touch
                if (ScrollMode == ScrollMode.Vertical)
                {
                    vscroll.MouseUp(e);
                    if (vscroll.TouchMode && CollisionTool.Check(rtScrollContent, e.Location)) vscroll.TouchUp(e);
                }
                else if (ScrollMode == ScrollMode.Horizon)
                {
                    hscroll.MouseUp(e);
                    if (hscroll.TouchMode && CollisionTool.Check(rtScrollContent, e.Location)) hscroll.TouchUp(e);
                }
                else
                {
                    vscroll.MouseUp(e);
                    if (vscroll.TouchMode && CollisionTool.Check(rtScrollContent, e.Location)) vscroll.TouchUp(e);

                    hscroll.MouseUp(e);
                    if (hscroll.TouchMode && CollisionTool.Check(rtScrollContent, e.Location)) hscroll.TouchUp(e);
                }
                #endregion
            }
            bInv = true;
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Draw
        #region DrawColumnBox
        void DrawColumnBox(Graphics g, DvTheme Theme, RectangleF rtColumn, Rectangle rtScrollContent, RectangleF rt)
        {
            var ColumnColor = GetColumnColor(Theme);
            var spw = Convert.ToInt32(DpiRatio * SPECIAL_CELL_WIDTH);

            #region Round 
            var rnd = RoundType.NONE;
            if (rtColumn.Top == rt.Top)
            {
                if (rtColumn.Left == rt.Left) rnd = RoundType.LT;
                else if (rtColumn.Right == rt.Right) rnd = RoundType.RT;
            }
            #endregion

            #region Line
            using (var p = new Pen(Color.White))
            {
                p.Width = 1;
               
                if (rnd == RoundType.LT)
                {
                    using (var pth = new GraphicsPath())
                    {
                        var rtk = new RectangleF(rt.Left + 1, rt.Top + 1, Theme.Corner * 2, Theme.Corner * 2);

                        pth.AddLine(rt.Right - 1, rt.Top + 1, rt.Left + 1 + Theme.Corner, rt.Top + 1);
                        pth.AddArc(rtk, 270, -90);
                        pth.AddLine(rt.Left + 1, rt.Top + 1 + Theme.Corner, rt.Left + 1, rt.Bottom - 1);

                        p.Color = ColumnColor.BrightnessTransmit(ColumnBevelBright);
                        g.DrawPath(p, pth);
                    }

                    p.Color = BackColor.BrightnessTransmit(Theme.BorderBright);
                    g.DrawLine(p, rt.Left, rt.Bottom, rt.Right, rt.Bottom);
                    if (Math.Abs(rtScrollContent.Right - rt.Right) > 3) g.DrawLine(p, rt.Right, rt.Top, rt.Right, rt.Bottom);
                }
                else
                {
                    p.Color = ColumnColor.BrightnessTransmit(ColumnBevelBright);
                    g.DrawLines(p, new PointF[] { new PointF(rt.Right - 1, rt.Top + 1), new PointF(rt.Left + 1, rt.Top + 1), new PointF(rt.Left + 1, rt.Bottom - 1) });

                    p.Color = BackColor.BrightnessTransmit(Theme.BorderBright);
                    g.DrawLine(p, rt.Left, rt.Bottom, rt.Right, rt.Bottom);
                    if (Math.Abs(rtScrollContent.Left + (SelectionMode == DvDataGridSelectionMode.SELECTOR ? spw : 0) - rt.Left) > 3) g.DrawLine(p, rt.Left, rt.Top, rt.Left, rt.Bottom);
                    if (Math.Abs(rtScrollContent.Right - rt.Right) > 3) g.DrawLine(p, rt.Right, rt.Top, rt.Right, rt.Bottom);
                }
            }
            #endregion
        }
        #endregion
        #endregion
        #region DataSource
        #region SetDataSource<T>
        public void SetDataSource<T>(IEnumerable<T> values)
        {
            objs = values;
            bNotRaiseEvent = true;
            var props = typeof(T).GetProperties();
            int nCnt = Columns.Where(x => props.Select(v => v.Name).Contains(x.Name)).Count();
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
                            var prop = dic[col.Name];

                            var cell = Activator.CreateInstance(col.CellType, this, row, col) as IDvDataGridCell;

                            cell.Value = prop.GetValue(src);

                            row.Cells.Add(cell);
                        }

                        Rows.Add(row);
                        ri++;
                    }
                }
                RefreshRows();
                bInv = true;
            }
            else throw new Exception("VALID COUNT");
            bNotRaiseEvent = false;
        }
        #endregion
        #region ResetDataSource
        public void ResetDataSource<T>()
        {
            #region Color
            var SummaryRowColor = GetSummaryRowColor(GetTheme());
            #endregion
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

                bInv = true;
            }
        }
        #endregion
        #endregion
        #region Color
        public Color GetBoxColor(DvTheme Theme) => !UseThemeColor ? this.BoxColor : Theme.Color2;
        public Color GetColumnColor(DvTheme Theme) => !UseThemeColor ? this.ColumnColor : Theme.ColumnColor;
        public Color GetRowColor(DvTheme Theme) => !UseThemeColor ? this.RowColor : Theme.Color3;
        public Color GetSelectedRowColor(DvTheme Theme) => !UseThemeColor ? this.SelectedRowColor : Theme.PointColor;
        public Color GetSummaryRowColor(DvTheme Theme) => !UseThemeColor ? this.SummaryRowColor : Theme.Color2;
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
        Dictionary<string, RectangleF> GetColumnBounds(Rectangle rtColumn, Rectangle rtScrollContent)
        {
            var ret = new Dictionary<string, RectangleF>();
            var colrc = GetColumnRowCount();
            var spw = Convert.ToInt32(DpiRatio * SPECIAL_CELL_WIDTH);

            #region Column
            var xws = new List<RectangleF>();
            #region XWs
            var cws = GetColumnsWidths(rtScrollContent);
            if (cws.Count == Columns.Count)
            {
                decimal x = rtScrollContent.X + (SelectionMode == DvDataGridSelectionMode.SELECTOR ? spw : 0);

                for (int i = 0; i < cws.Count; i++)
                {
                    var w = cws[i];
                    xws.Add(new RectangleF(Convert.ToSingle(x), rtColumn.Y, Convert.ToSingle(w), 0));
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

                    var rtFilterV = new RectangleF((rt.X), rtColumn.Bottom - ColumnHeight, (rt.Width), ColumnHeight);
                    var rtColumnV = new RectangleF((rt.X), rtFilterV.Top - (ColumnHeight * nc), (rt.Width), (ColumnHeight * nc));

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

                    var rtColumnV = new RectangleF((rt.X), rtColumn.Bottom - (ColumnHeight * nc), (rt.Width), (ColumnHeight * nc));

                    ret.Add("rtColumn" + col.Name, rtColumnV);
                    ret.Add("rtFilter" + col.Name, new RectangleF(rtColumnV.X, rtColumnV.Y, rtColumnV.Width, 0));
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
                var y = rtColumn.Y + ((colrc - Depth) * ColumnHeight);
                var w = (xws[imax].Right) - Convert.ToInt32(xws[imin].Left);
                var h = ColumnHeight;

                ret.Add("rtColumnGroup" + v.Name, new RectangleF(x, y, w, h));
            }
            #endregion
            #endregion

            return ret;
        }
        #endregion
        #region GetColumnsWidths
        List<int> GetColumnsWidths(Rectangle rtScrollContent)
        {
            var ret = new List<int>();
            var spw = Convert.ToInt32(DpiRatio * SPECIAL_CELL_WIDTH);
            var tw = rtScrollContent.Width - (SelectionMode == DvDataGridSelectionMode.SELECTOR ? spw : 0);
            var cw = tw - Columns.Where(x => x.SizeMode == SizeMode.Pixel).Sum(x => x.Width);
            foreach (var v in Columns) ret.Add(v.SizeMode == SizeMode.Pixel ? Convert.ToInt32(v.Width) : Convert.ToInt32((decimal)cw * (v.Width / 100M)));
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
                            case DvDataGridColumnSortState.ASC:
                                if (bFirst) { result = mrows.OrderBy(x => x.Cells[i].Value); bFirst = false; }
                                else if (result != null)
                                {
                                    result = result.ThenBy(x => x.Cells[i].Value);
                                }
                                break;
                            case DvDataGridColumnSortState.DESC:
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
                    PrevTotalHeight = sum;
                    lsp.Clear();
                    var nsum = 0;
                    foreach (var v in lsv) { lsp.Add(new _DGSearch_() { Height = v.RowHeight, Sum = nsum, Row = v }); nsum += v.RowHeight; }
                }
            }
            #endregion
            #region Summary Calc
            foreach (var row in SummaryRows)
            {
                foreach (var v in row.Cells.Where(x => x is DvDataGridSummaryCell)) ((DvDataGridSummaryCell)v).Calc();
            }
            #endregion
            bInv = true;
        }
        #endregion
        #region RefreshValues
        public bool RefreshValues()
        {
            bool bInv = false;
            foreach (var row in Rows)
            {
                var v = row.Source;
                foreach (var cell in row.Cells)
                {
                    var prop = v.GetType().GetProperty(cell.Column.Name);
                    var val = prop.GetValue(v);

                    if (cell.Value != null && val != null && !cell.Value.Equals(val) || (cell.Value == null && val != null) || (cell.Value != null && val == null))
                    {
                        cell.Value = val;
                        bInv = true;
                    }
                }
            }

            if (bInv) bInv = true;
            return bInv;
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

            if (!bNotRaiseEvent && AutoSet && cell.Row.Source != null)
            {
                var prop = cell.Row.Source.GetType().GetProperty(cell.Column.Name);
                prop.SetValue(cell.Row.Source, cell.Value);
            }
        }
        #endregion
        #region InvokeCellButtonClick
        public void InvokeCellButtonClick(DvDataGridCell cell)
        {
            if (!bNotRaiseEvent && CellButtonClick != null) CellButtonClick.Invoke(this, new CellButtonClickEventArgs(cell));
        }
        #endregion
        #endregion
        #region Rect
        public static Rectangle RTI(RectangleF rt) => new Rectangle(Convert.ToInt32(rt.X), Convert.ToInt32(rt.Y), Convert.ToInt32(rt.Width), Convert.ToInt32(rt.Height));
        public static RectangleF RTF(RectangleF rt) => new RectangleF(Convert.ToSingle(rt.X), Convert.ToSingle(rt.Y), Convert.ToSingle(rt.Width), Convert.ToSingle(rt.Height));
        #endregion
        #region Loop
        private void Loop(Action<int, Rectangle, DvDataGridRow> Func)
        {
            if (Areas.ContainsKey("rtScrollContent"))
            {
                var rtColumn = Areas["rtColumn"];
                var rtScrollContent = Areas["rtScrollContent"];
                var rts = GetColumnBounds(rtColumn, rtScrollContent);
                var rtsc = rtScrollContent;
                var vspos = vscroll.ScrollTotal > vscroll.ScrollView ? Convert.ToInt32(MathTool.Constrain(vscroll.ScrollPositionWithOffset, -(vscroll.ScrollTotal - vscroll.ScrollView), 0)) : 0;
                var hspos = hscroll.ScrollTotal > hscroll.ScrollView ? Convert.ToInt32(MathTool.Constrain(hscroll.ScrollPositionWithOffset, -(hscroll.ScrollTotal - hscroll.ScrollView), 0)) : 0;

                #region Make DBSearch
                {
                    var lsv = mrows;
                    var sum = lsv.Sum(x => x.RowHeight);
                    if (PrevTotalHeight != sum)
                    {
                        PrevTotalHeight = sum;
                        lsp.Clear();
                        var nsum = 0;
                        foreach (var v in lsv) { lsp.Add(new _DGSearch_() { Height = v.RowHeight, Sum = nsum, Row = v }); nsum += v.RowHeight; }
                    }
                }
                #endregion

                var ls = GetRows();
                var startidx = (int)MathTool.Constrain(BNSearch(lsp, 0, lsp.Count - 1, rtScrollContent.Top, vspos, rtScrollContent.Top) - 1, 0, ls.Count - 1);
                var endidx = (int)MathTool.Constrain(BNSearch(lsp, 0, lsp.Count - 1, rtScrollContent.Top, vspos, rtScrollContent.Bottom) + 1, 0, ls.Count - 1);

                for (int i = startidx; i <= endidx; i++)
                {
                    var v = ls[i];
                    var y = Convert.ToInt32(rtScrollContent.Y + lsp[i].Sum + vspos);
                    var rtITM = new Rectangle(rtScrollContent.X, y, rtScrollContent.Width, v.RowHeight);
                    Func(i, rtITM, v);
                }
                
            }
        }

        #region BNSearch
        int BNSearch(List<_DGSearch_> ls, int si, int ei, int top, int vpos, int value)
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

        #region InvalidateTH
        public void InvalidateTH() => bInv = true;
        #endregion
        #endregion
    }

}
