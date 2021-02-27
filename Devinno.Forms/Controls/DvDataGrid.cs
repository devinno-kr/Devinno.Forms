using Devinno.Collections;
using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvDataGrid : DvControl
    {
        #region Const 
        private const int SPECIAL_CELL_WIDTH = 30;
        private const int SELECTOR_BOX_WIDTH = 20;
        internal const double DataGridInputBright = 0.5;
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
                    Invalidate();
                }
            }
        }
        #endregion
        #region ColumnColor
        private Color cColumnColor = DvTheme.DefaultTheme.Color0;
        public Color ColumnColor
        {
            get { return cColumnColor; }
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
                    Invalidate();
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
                    Invalidate();
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
                    Invalidate();
                }
            }
        }
        #endregion
        #region TouchMode
        public bool TouchMode { get; set; } = false;
        #endregion
        #endregion

        #region Event

        #endregion

        #region Member Variable
        List<DvDataGridRow> mrows = new List<DvDataGridRow>();
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
        }
        #endregion

        #region Override
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);
            
            var rtContent = Areas["rtContent"];

            var scwh = Convert.ToInt32(Scroll.SC_WH * DpiRatio);
            var columnH = (ColumnRowCount() + (Columns.Where(x => x.UseFilter).Count() > 0 ? 1 : 0)) * ColumnHeight;
            var summaryH = SummaryRows.Count * RowHeight;

            var ush = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Horizon;
            var usv = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Vertical;

            var rtColumn = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, columnH);
            var rtBox = new Rectangle(rtColumn.X, rtColumn.Bottom, rtContent.Width - (usv ? scwh : 0), rtContent.Height - columnH - (ush ? scwh : 0));
            var rtSummary = new Rectangle(rtBox.X, rtBox.Bottom - summaryH, rtContent.Width - (usv  ? scwh : 0), summaryH);
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
        }

        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BoxColor = GetBoxColor(Theme);
            var ColumnColor = GetColumnColor(Theme);
            var RowColor = GetRowColor(Theme);
            var SelectRowColor = GetSelectedRowColor(Theme);
            var SummaryRowColor = GetSummaryRowColor(Theme);
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

            var ush = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Horizon;
            var usv = ScrollMode == ScrollMode.Both || ScrollMode == ScrollMode.Vertical;
            #endregion

            p.Width = 1;
            p.Color = Color.FromArgb(120, Color.Red); e.Graphics.DrawRectangle(p, rtColumn);
            p.Color = Color.FromArgb(120, Color.Red); e.Graphics.DrawRectangle(p, rtSummary);
            p.Color = Color.FromArgb(120, Color.Magenta); e.Graphics.DrawRectangle(p, rtScrollV);
            p.Color = Color.FromArgb(120, Color.Magenta); e.Graphics.DrawRectangle(p, rtScrollH);
            p.Color = Color.FromArgb(120, Color.Magenta); e.Graphics.DrawRectangle(p, rtScrollR);
            p.Color = Color.FromArgb(120, Color.Yellow); e.Graphics.DrawRectangle(p, rtScrollContent);

            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion

            base.OnThemeDraw(e, Theme);
        }
        #endregion

        #region Method
        #region GetColor
        public Color GetBoxColor(DvTheme Theme) => !UseThemeColor ? this.BoxColor : Theme.Color2;
        public Color GetColumnColor(DvTheme Theme) => !UseThemeColor ? this.ColumnColor : Theme.Color0;
        public Color GetRowColor(DvTheme Theme) => !UseThemeColor ? this.RowColor : Theme.Color3;
        public Color GetSelectedRowColor(DvTheme Theme) => !UseThemeColor ? this.SelectedRowColor : Theme.PointColor;
        public Color GetSummaryRowColor(DvTheme Theme) => !UseThemeColor ? this.SummaryRowColor : Theme.Color1;
        #endregion
        #region ColumnRowCount
        private int ColumnRowCount()
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
        #region GetColumnsWidths
        List<decimal> GetColumnsWidths(Rectangle rtScrollContent)
        {
            var ret = new List<decimal>();
            var tw = rtScrollContent.Width - (SelectionMode == DvDataGridSelectionMode.SELECTOR ? SPECIAL_CELL_WIDTH : 0);
            int cw = tw - (int)Columns.Where(x => x.SizeMode == SizeMode.Pixel).Sum(x => x.Width);
            foreach (var v in Columns) ret.Add(v.SizeMode == SizeMode.Pixel ? v.Width : ((decimal)cw * (v.Width / 100M)));
            return ret;
        }
        #endregion
        #endregion

    }

    #region enum : SizeMode
    public enum SizeMode { Percent, Pixel }
    #endregion
    #region enum : DvDataGridSelectionMode
    public enum DvDataGridSelectionMode { NONE, SELECTOR, SINGLE, MULTI }
    #endregion
    #region enum : DvDataGridColumnSortState
    public enum DvDataGridColumnSortState { NONE, ASC, DESC };
    #endregion

    #region interface : IDvDataGridColumn
    public interface IDvDataGridColumn
    {
        string Name { get; set; }
        string GroupName { get; set; }
        string HeaderText { get; set; }

        SizeMode SizeMode { get; set; }
        decimal Width { get; set; }

        bool UseFilter { get; set; }
        string FilterText { get; set; }

        bool UseSort { get; set; }
        DvDataGridColumnSortState SortState { get; set; }
        int SortOrder { get; }

        Color TextColor { get; set; }
        Type CellType { get; set; }
        bool Fixed { get; set; }

        void Paint(DvTheme Theme, Graphics g, Rectangle ColumnBounds);
        void MouseDown(Rectangle ColumnBounds, Point MousePosition);
        void MouseUp(Rectangle ColumnBounds, Point MousePosition);
    }
    #endregion
    #region interface : IDvDataGridCell
    public interface IDvDataGridCell
    {
        string Name { get; set; }

        int ColSpan { get; set; }
        int RowSpan { get; set; }
        int ColumnIndex { get; }
        int RowIndex { get; }

        bool Visible { get; set; }
        bool Enabled { get; set; }
        object Value { get; set; }
        object Tag { get; set; }

        Color CellBackColor { get; set; }
        Color SelectedCellBackColor { get; set; }

        DvDataGrid Grid { get; }
        DvDataGridRow Row { get; }
        IDvDataGridColumn Column { get; }

        void Paint(DvTheme Theme, Graphics g, RectangleF CellBounds);
        void MouseDown(Rectangle CellBounds, int x, int y);
        void MouseDoubleClick(Rectangle CellBounds, int x, int y);
        void MouseUp(Rectangle CellBounds, int x, int y);
    }
    #endregion

    #region class : DvDataGridColumn
    public class DvDataGridColumn : IDvDataGridColumn
    {
        #region Properties
        public DvDataGrid Grid { get; private set; }

        public string Name { get; set; }
        public string GroupName { get; set; }
        public string HeaderText { get; set; }

        public SizeMode SizeMode { get; set; }
        public decimal Width { get; set; }

        public bool UseFilter { get; set; }
        public string FilterText { get; set; }

        public bool UseSort { get; set; }
        public DvDataGridColumnSortState SortState { get; set; }
        public int SortOrder { get; }

        public Color TextColor { get; set; }
        public Type CellType { get; set; }
        public bool Fixed { get; set; }
        #endregion

        #region Constructor
        public DvDataGridColumn(DvDataGrid dataGrid)
        {
            TextColor = dataGrid.ForeColor;
            Grid = dataGrid;
            CellType = typeof(DvDataGridLabelCell);
        }
        #endregion

        #region Paint
        public virtual void Paint(DvTheme Theme, Graphics g, Rectangle Bounds)
        {
            if (Grid != null)
            {
                #region Color
                var nc = Grid;
                var BoxColor = nc.GetBoxColor(Theme);
                var ColumnColor = nc.GetColumnColor(Theme);
                var RowColor = nc.GetRowColor(Theme);
                var SelectRowColor = nc.GetSelectedRowColor(Theme);
                var SummaryRowColor = nc.GetSummaryRowColor(Theme);
                #endregion
                Theme.DrawTextShadow(g, null, HeaderText, Grid.Font, Grid.ForeColor, ColumnColor, Bounds);

                if (UseSort)
                {
                    #region Rect
                    var sizewh = Convert.ToInt32(12 * Grid.DpiRatio);
                    var rtSort = new Rectangle(Bounds.Right - Bounds.Height, Bounds.Y, Bounds.Height, Bounds.Height);
                    var rtSortDraw = MathTool.MakeRectangle(rtSort, new Size(sizewh, sizewh));
                    #endregion
                    #region Sort
                    switch (SortState)
                    {
                        case DvDataGridColumnSortState.NONE:
                            break;
                        case DvDataGridColumnSortState.ASC:
                            break;
                        case DvDataGridColumnSortState.DESC:
                            break;
                    }
                    #endregion
                }
            }
        }
        #endregion
        #region MouseDown
        public virtual void MouseDown(Rectangle Bounds, Point MousePosition)
        {
            bool b = false;
            if (UseSort)
            {
                var sizewh = Convert.ToInt32(12 * Grid.DpiRatio);
                var rtSort = new Rectangle(Bounds.Right - Bounds.Height, Bounds.Y, Bounds.Height, Bounds.Height);
                var rtSortDraw = MathTool.MakeRectangle(rtSort, new Size(sizewh, sizewh));
            }
        }
        #endregion
        #region MouseUp
        public virtual void MouseUp(Rectangle Bounds, Point MousePosition)
        {
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridRow
    public class DvDataGridRow
    {
        public DvDataGrid Grid { get; private set; }
        public int RowHeight { get; set; }
        public List<IDvDataGridCell> Cells { get; private set; } = new List<IDvDataGridCell>();
        public object Source { get; set; }
        public object Tag { get; set; }
        public bool Selected { get; set; }

        public DvDataGridRow(DvDataGrid Grid)
        {
            this.Grid = Grid;
            this.RowHeight = Grid.RowHeight;
        }
    }
    #endregion
    #region class : DvDataGridCell
    public class DvDataGridCell : IDvDataGridCell
    {
        #region Properties
        public string Name { get; set; }

        public int ColSpan { get; set; } = 1;
        public int RowSpan { get; set; } = 1;
        public int ColumnIndex { get { return Row.Cells.IndexOf(this); } }
        public int RowIndex { get { return Grid.Rows.IndexOf(Row); } }

        public bool Visible { get; set; } = true;
        public bool Enabled { get; set; } = true;
        public virtual object Value { get; set; }
        public object Tag { get; set; }

        public Color CellBackColor { get; set; }
        public Color SelectedCellBackColor { get; set; }
        public Color CellTextColor { get; set; }

        public DvDataGrid Grid { get; private set; }
        public DvDataGridRow Row { get; private set; }
        public IDvDataGridColumn Column { get; private set; }
        #endregion

        #region Constructor
        public DvDataGridCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column)
        {
            this.Grid = Grid;
            this.Row = Row;
            this.Column = Column;

            var Theme = Grid.GetTheme();

            this.CellTextColor = Grid.ForeColor;
            this.CellBackColor = Grid.GetRowColor(Theme);
            this.SelectedCellBackColor = Grid.GetSelectedRowColor(Theme);
        }
        #endregion

        #region Virtual Method
        #region Paint
        public virtual void Paint(DvTheme Theme, Graphics g, RectangleF CellBounds)
        {
            if (Grid != null)
            {
                var rt = new Rectangle((int)CellBounds.X, (int)CellBounds.Y, (int)CellBounds.Width, (int)CellBounds.Height);
                var bg = Grid.GetBoxColor(Theme);
                var c = Row.Selected ? SelectedCellBackColor : CellBackColor;
                Theme.DrawBox(g, c, bg, rt, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL);
                CellPaint(Theme, g, CellBounds);
            }
        }
        #endregion
        #region CellPaint
        public virtual void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds) { }
        #endregion
        #region MouseDown / MouseUp
        public virtual void MouseDown(Rectangle CellBounds, int x, int y)
        {
            if (Enabled && CollisionTool.Check(CellBounds, x, y))
                CellMouseDown(CellBounds, x, y);
        }
        public virtual void MouseUp(Rectangle CellBounds, int x, int y)
        {
            if (Enabled && CollisionTool.Check(CellBounds, x, y))
                CellMouseUp(CellBounds, x, y);
        }
        public virtual void MouseDoubleClick(Rectangle CellBounds, int x, int y)
        {
            if (Enabled && CollisionTool.Check(CellBounds, x, y))
                CellMouseDoubleClick(CellBounds, x, y);
        }
        #endregion
        #region Cell MouseDown / MouseUp
        public virtual void CellMouseDown(Rectangle CellBounds, int x, int y) { }
        public virtual void CellMouseUp(Rectangle CellBounds, int x, int y) { }
        public virtual void CellMouseDoubleClick(Rectangle CellBounds, int x, int y) { }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridSummaryRow
    public class DvDataGridSummaryRow
    {
        public DvDataGrid Grid { get; private set; }
        public List<DvDataGridSummaryCell> Cells { get; private set; } = new List<DvDataGridSummaryCell>();
        public object Tag { get; set; }

        public DvDataGridSummaryRow(DvDataGrid Grid)
        {
            this.Grid = Grid;
        }
    }
    #endregion
    #region class : DvDataGridSummaryCell
    public class DvDataGridSummaryCell
    {
        #region Properties
        public int RowIndex { get { return Grid.SummaryRows.IndexOf(Row); } }
        public object Tag { get; set; }

        public int ColIndex { get; set; } = 0;
        public int ColSpan { get; set; } = 1;

        public Color CellBackColor { get; set; }

        public DvDataGrid Grid { get; private set; }
        public DvDataGridSummaryRow Row { get; private set; }
        #endregion

        #region Constructor
        public DvDataGridSummaryCell(DvDataGrid Grid, DvDataGridSummaryRow Row)
        {
            this.Grid = Grid;
            this.Row = Row;

            var Theme = Grid.GetTheme();
            var SummaryRowColor = Grid.GetSummaryRowColor(Theme);

            this.CellBackColor = SummaryRowColor;
        }
        #endregion

        #region Virtual Method
        #region Paint
        public virtual void Paint(DvTheme Theme, Graphics g, RectangleF CellBounds)
        {
            var rt = new Rectangle((int)CellBounds.X, (int)CellBounds.Y, (int)CellBounds.Width, (int)CellBounds.Height);
            var bg = Grid.GetBoxColor(Theme);
            var c = CellBackColor;
            Theme.DrawBox(g, c, bg, rt, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL);
            CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellPaint
        public virtual void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds) { }
        #endregion
        #endregion
    }
    #endregion

}
