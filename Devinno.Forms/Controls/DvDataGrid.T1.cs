using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Controls
{
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

        void Paint(DvTheme Theme, Graphics g, Rectangle CellBounds);
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
        public int SortOrder { get; private set; }

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

                var rt = DvDataGridTool.RTI(Bounds);
                if(Grid.TextShadow) Theme.DrawTextShadow(g, null, HeaderText, Grid.Font, Grid.ForeColor, ColumnColor, rt);
                else Theme.DrawText(g, null, HeaderText, Grid.Font, Grid.ForeColor, ColumnColor, rt);

                if (UseSort)
                {
                    var sizewh = Convert.ToInt32(12 * Grid.DpiRatio);
                    var rtSort = DvDataGridTool.RTI(new Rectangle(Bounds.Right - Bounds.Height, Bounds.Y, Bounds.Height, Bounds.Height));
                    Theme.DrawTextShadow(g, new DvIcon("fa-sort"), null, Grid.Font, Grid.ForeColor.BrightnessTransmit(-0.7), ColumnColor, rtSort);

                    switch (SortState)
                    {
                        case DvDataGridColumnSortState.ASC: Theme.DrawTextShadow(g, new DvIcon("fa-sort-up"), null, Grid.Font, Grid.ForeColor, ColumnColor, rtSort); break;
                        case DvDataGridColumnSortState.DESC: Theme.DrawTextShadow(g, new DvIcon("fa-sort-down"), null, Grid.Font, Grid.ForeColor, ColumnColor, rtSort); break;
                    }
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

                if (CollisionTool.Check(rtSort, MousePosition.X, MousePosition.Y))
                {
                    switch (SortState)
                    {
                        case DvDataGridColumnSortState.NONE:
                            SortState = DvDataGridColumnSortState.ASC;
                            SortOrder = (SortState != DvDataGridColumnSortState.NONE ? Grid.Columns.Where(x => x.UseSort && x.SortState != DvDataGridColumnSortState.NONE).Count() : 1000);
                            Grid.InvokeSortChanged();
                            break;
                        case DvDataGridColumnSortState.ASC:
                            SortState = DvDataGridColumnSortState.DESC;
                            SortOrder = (SortState != DvDataGridColumnSortState.NONE ? Grid.Columns.Where(x => x.UseSort && x.SortState != DvDataGridColumnSortState.NONE).Count() : 1000);
                            Grid.InvokeSortChanged();
                            break;
                        case DvDataGridColumnSortState.DESC:
                            SortState = DvDataGridColumnSortState.NONE;
                            SortOrder = (SortState != DvDataGridColumnSortState.NONE ? Grid.Columns.Where(x => x.UseSort && x.SortState != DvDataGridColumnSortState.NONE).Count() : 1000);
                            Grid.InvokeSortChanged();
                            break;
                    }
                    b = true;
                    Grid.InvalidateTH();
                }
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

        int nRowHeight;
        public int RowHeight
        {
            get => nRowHeight; 
            set
            {
                if(nRowHeight != value)
                {
                    nRowHeight = value;
                    if(Grid != null) Grid.bModSize = true;
                }
            }
        }
       
        public List<IDvDataGridCell> Cells { get; private set; } = new List<IDvDataGridCell>();
        public object Source { get; set; }
        public object Tag { get; set; }
        public bool Selected { get; set; }

        public DvDataGridRow(DvDataGrid Grid)
        {
            this.Grid = Grid;
            this.nRowHeight = Grid.RowHeight;
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
        public virtual void Paint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            if (Grid != null)
            {
                var rt = new Rectangle(CellBounds.X, CellBounds.Y, CellBounds.Width, CellBounds.Height);
                var bg = Grid.GetBoxColor(Theme);
                var c = Row.Selected ? SelectedCellBackColor : CellBackColor;

                var br = new SolidBrush(c);
                var p = new Pen(c);
                g.FillRectangle(br, rt);
                if(Grid.RowBevel)
                {
                    int n = 1;
                    var pts = new Point[] { new Point(rt.Right - n, rt.Top + n), new Point(rt.Left + n, rt.Top + n), new Point(rt.Left + n, rt.Bottom - n) };
                    p.Color = c.BrightnessTransmit(DvDataGrid.ColumnBevelBright);
                    g.DrawLines(p, pts);
                }
                p.Color = bg.BrightnessTransmit(Theme.BorderBright);
                g.DrawRectangle(p, rt);
                
                p.Dispose();
                br.Dispose();

                CellPaint(Theme, g, CellBounds);
            }
        }
        #endregion
        #region CellPaint
        public virtual void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds) { }
        #endregion
        #region MouseDown / MouseUp
        public virtual void MouseDown(Rectangle CellBounds, int x, int y)
        {
            if (Enabled && CollisionTool.Check(CellBounds, x, y))
                CellMouseDown(CellBounds, x, y);
        }
        public virtual void MouseUp(Rectangle CellBounds, int x, int y)
        {
            if (Enabled)
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

        public int ColumnIndex { get; set; } = 0;
        public int ColumnSpan { get; set; } = 1;
        
        public bool Visible { get; set; } = true;

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
        public virtual void Paint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var rt = new Rectangle(CellBounds.X, CellBounds.Y, CellBounds.Width, CellBounds.Height);
            var bg = Grid.GetBoxColor(Theme);
            var c = CellBackColor;
            
            var br = new SolidBrush(c);
            var p = new Pen(c);

            g.FillRectangle(br, rt);
            if (Grid.RowBevel)
            {
                int n = 1;
                var pts = new Point[] { new Point(rt.Right - n, rt.Top + n), new Point(rt.Left + n, rt.Top + n), new Point(rt.Left + n, rt.Bottom - n) };
                p.Color = c.BrightnessTransmit(DvDataGrid.ColumnBevelBright);
                g.DrawLines(p, pts);
            }
            p.Color = bg.BrightnessTransmit(Theme.BorderBright);
            g.DrawRectangle(p, rt);

            br.Dispose();
            p.Dispose();

            CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellPaint
        public virtual void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds) { }
        #endregion

        public virtual void Calc() { }
        #endregion
    }
    #endregion
    
    #region class : _DGSearch_
    class _DGSearch_
    {
        public int Height { get; set; }
        public int Sum { get; set; }
        public DvDataGridRow Row { get; set; }

        public int Top => Sum;
        public int Bottom => Top + Height;
    }
    #endregion

    #region classes : EventArgs
    #region class : CellButtonClickEventArgs
    public class CellButtonClickEventArgs : EventArgs
    {
        public IDvDataGridCell Cell { get; private set; }
        public CellButtonClickEventArgs(IDvDataGridCell Cell)
        {
            this.Cell = Cell;
        }
    }
    #endregion
    #region class : ColumnMouseEventArgs
    public class ColumnMouseEventArgs : EventArgs
    {
        public IDvDataGridColumn Column { get; private set; }
        public int MouseX { get; private set; }
        public int MouseY { get; private set; }
        public Point MouseLocation { get; private set; }

        public ColumnMouseEventArgs(IDvDataGridColumn column, int x, int y)
        {
            this.Column = column;
            this.MouseX = x;
            this.MouseY = y;
            this.MouseLocation = new Point(MouseX, MouseY);
        }
    }
    #endregion
    #region class : CellMouseEventArgs
    public class CellMouseEventArgs : EventArgs
    {
        public IDvDataGridCell Cell { get; private set; }
        public int MouseX { get; private set; }
        public int MouseY { get; private set; }
        public Point MouseLocation { get; private set; }
        public CellMouseEventArgs(IDvDataGridCell Cell, int x, int y)
        {
            this.Cell = Cell;
            this.MouseX = x;
            this.MouseY = y;
            this.MouseLocation = new Point(MouseX, MouseY);
        }
    }
    #endregion
    #region class : CellValueChangedEventArgs
    public class CellValueChangedEventArgs : EventArgs
    {
        public IDvDataGridCell Cell { get; private set; }
        public object OldValue { get; private set; }
        public object NewValue { get; private set; }
        public CellValueChangedEventArgs(IDvDataGridCell Cell, object OldValue, object NewValue)
        {
            this.Cell = Cell;
            this.OldValue = OldValue;
            this.NewValue = NewValue;
        }
    }
    #endregion
    #region class : RowDoubleClickEventArgs
    public class RowDoubleClickEventArgs : EventArgs
    {
        public DvDataGridRow Row { get; private set; }
        public int MouseX { get; private set; }
        public int MouseY { get; private set; }
        public Point MouseLocation { get; private set; }
        public RowDoubleClickEventArgs(DvDataGridRow Row, int x, int y)
        {
            this.Row = Row;
            this.MouseX = x;
            this.MouseY = y;
            this.MouseLocation = new Point(MouseX, MouseY);
        }
    }
    #endregion
    #endregion
}
