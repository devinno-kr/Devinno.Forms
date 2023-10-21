using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    #region enum : DvDataGridSelectionMode
    public enum DvDataGridSelectionMode { None, Selector, Single, Multi}
    #endregion
    #region enum : DvDataGridColumnSortState
    public enum DvDataGridColumnSortState { None, Asc, Desc };
    #endregion

    #region interface : IDvDataGridColumn
    public interface IDvDataGridColumn
    {
        string Name { get; set; }
        string GroupName { get; set; }
        string HeaderText { get; set; }

        DvSizeMode SizeMode { get; set; }
        decimal Width { get; set; }

        bool UseFilter { get; set; }
        string FilterText { get; set; }

        bool UseSort { get; set; }
        DvDataGridColumnSortState SortState { get; set; }
        int SortOrder { get; }

        Color? TextColor { get; set; }
        Type CellType { get; set; }
        bool Fixed { get; set; }

        void Draw(Graphics g, DvTheme Theme, RectangleF ColumnBounds);
        void MouseDown(RectangleF ColumnBounds, int x, int y);
        void MouseUp(RectangleF ColumnBounds, int x, int y);
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

        Color? CellBackColor { get; set; }
        Color? SelectedCellBackColor { get; set; }

        DvDataGrid Grid { get; }
        DvDataGridRow Row { get; }
        IDvDataGridColumn Column { get; }

        void Draw(Graphics g, DvTheme Theme, RectangleF ColumnBounds);
        void MouseDown(RectangleF ColumnBounds, int x, int y);
        void MouseUp(RectangleF ColumnBounds, int x, int y);
        void MouseDoubleClick(RectangleF ColumnBounds, int x, int y);
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

        public ColumnMouseEventArgs(IDvDataGridColumn column, int x, int y)
        {
            this.Column = column;
            this.MouseX = x;
            this.MouseY = y;
        }
    }
    #endregion
    #region class : CellMouseEventArgs
    public class CellMouseEventArgs : EventArgs
    {
        public IDvDataGridCell Cell { get; private set; }
        public int MouseX { get; private set; }
        public int MouseY { get; private set; }
        public CellMouseEventArgs(IDvDataGridCell Cell, int x, int y)
        {
            this.Cell = Cell;
            this.MouseX = x;
            this.MouseY = y;
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
        public RowDoubleClickEventArgs(DvDataGridRow Row, int x, int y)
        {
            this.Row = Row;
            this.MouseX = x;
            this.MouseY = y;
        }
    }
    #endregion
    #endregion
    #region class : DvDataGridTool
    internal class DvDataGridTool
    {
        #region GetText
        public static string GetText(object Value)
        {
            var s = (string)null;
            if (Value is string) s = (string)Value;
            else if (Value != null) s = Value.ToString();
            return s;
        }
        #endregion
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

        public DvSizeMode SizeMode { get; set; }
        public decimal Width { get; set; }

        public bool UseFilter { get; set; }
        public string FilterText { get; set; }

        public bool UseSort { get; set; }
        public DvDataGridColumnSortState SortState { get; set; }
        public int SortOrder { get; private set; }

        public Color? TextColor { get; set; }
        public Type CellType { get; set; }
        public bool Fixed { get; set; }
        #endregion

        #region Constructor
        public DvDataGridColumn(DvDataGrid dataGrid)
        {
            Grid = dataGrid;
            CellType = typeof(DvDataGridLabelCell);
        }
        #endregion

        #region Draw
        public virtual void Draw(Graphics g, DvTheme Theme, RectangleF Bounds)
        {
            if (Grid != null)
            {
                #region Color
                var nc = Grid;
                var TextColor = this.TextColor ?? Theme.ForeColor;
                var BoxColor = nc.GetBoxColor(Theme);
                var ColumnColor = nc.GetColumnColor(Theme);
                var RowColor = nc.GetRowColor(Theme);
                var SelectRowColor = nc.GetSelectedRowColor(Theme);
                var SummaryRowColor = nc.GetSummaryRowColor(Theme);

                var Font = nc.Font;
                #endregion

                var rt = Bounds;
                Theme.DrawText(g, HeaderText, Font, TextColor, rt);

                if (UseSort)
                {
                    var sizewh = Convert.ToInt32(12);
                    var rtSort = Util.FromRect(Bounds.Right - Bounds.Height, Bounds.Top, Bounds.Height, Bounds.Height);
                    Theme.DrawIcon(g, new DvIcon("fa-sort"), TextColor.BrightnessTransmit(-0.7F), rtSort);

                    switch (SortState)
                    {
                        case DvDataGridColumnSortState.Asc: Theme.DrawIcon(g, new DvIcon("fa-sort-up"), TextColor, rtSort); break;
                        case DvDataGridColumnSortState.Desc: Theme.DrawIcon(g, new DvIcon("fa-sort-down"), TextColor, rtSort); break;
                    }
                }
            }
        }
        #endregion
        #region MouseDown
        public virtual void MouseDown(RectangleF Bounds, int x, int y)
        {
            bool b = false;
            if (UseSort)
            {
                var sizewh = Convert.ToInt32(12);
                var rtSort = Util.FromRect(Bounds.Right - Bounds.Height, Bounds.Top, Bounds.Height, Bounds.Height);

                if (CollisionTool.Check(rtSort, x, y))
                {
                    switch (SortState)
                    {
                        case DvDataGridColumnSortState.None:
                            SortState = DvDataGridColumnSortState.Asc;
                            SortOrder = (SortState != DvDataGridColumnSortState.None ? Grid.Columns.Where(x => x.UseSort && x.SortState != DvDataGridColumnSortState.None).Count() : 1000);
                            Grid.InvokeSortChanged();
                            break;
                        case DvDataGridColumnSortState.Asc:
                            SortState = DvDataGridColumnSortState.Desc;
                            SortOrder = (SortState != DvDataGridColumnSortState.None ? Grid.Columns.Where(x => x.UseSort && x.SortState != DvDataGridColumnSortState.None).Count() : 1000);
                            Grid.InvokeSortChanged();
                            break;
                        case DvDataGridColumnSortState.Desc:
                            SortState = DvDataGridColumnSortState.None;
                            SortOrder = (SortState != DvDataGridColumnSortState.None ? Grid.Columns.Where(x => x.UseSort && x.SortState != DvDataGridColumnSortState.None).Count() : 1000);
                            Grid.InvokeSortChanged();
                            break;
                    }
                    b = true;
                }
            }
        }
        #endregion
        #region MouseUp
        public virtual void MouseUp(RectangleF Bounds, int x, int y)
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
                if (nRowHeight != value)
                {
                    nRowHeight = value;
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
    public abstract class DvDataGridCell : IDvDataGridCell
    {
        #region Properties
        public string Name { get; set; }

        public int ColSpan { get; set; } = 1;
        public int RowSpan { get; set; } = 1;
        public int ColumnIndex { get { return Row.Cells.IndexOf(this); } }
        public int RowIndex { get { return Grid.Rows.IndexOf(Row); } }

        public bool Visible { get; set; } = true;
        public bool Enabled { get; set; } = true;
        public object Tag { get; set; }

        public Color? CellBackColor { get; set; }
        public Color? SelectedCellBackColor { get; set; }
        public Color? CellTextColor { get; set; }

        public DvDataGrid Grid { get; private set; }
        public DvDataGridRow Row { get; private set; }
        public IDvDataGridColumn Column { get; private set; }

        protected PropertyInfo ValueInfo { get; }
        public object Value
        {
            get
            {
                return ValueInfo?.GetValue(Row.Source);
            }
            set
            {
                if (ValueInfo != null && ValueInfo.CanWrite)
                {
                    ValueInfo.SetValue(Row.Source, value);
                }
            }
        }
        #endregion

        #region Constructor
        public DvDataGridCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column)
        {
            this.Grid = Grid;
            this.Row = Row;
            this.Column = Column;

            if (Grid.DataType != null && !(Column is DvDataGridButtonColumn))
                ValueInfo = Grid.DataType.GetProperty(Column.Name);
        }
        #endregion

        #region Member Variable
        bool bDown = false;
        #endregion

        #region Virtual Method
        #region Draw
        public virtual void Draw(Graphics g, DvTheme Theme, RectangleF CellBounds)
        {
            if (Grid != null)
            {
                var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
                var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
                var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
                var BorderColor = Theme.GetBorderColor(CellBackColor, Grid.BackColor);

                var rt = CellBounds;
                var bg = Grid.GetBoxColor(Theme);
                var c = Row.Selected ? SelectedCellBackColor : CellBackColor;
                var border = Theme.GetBorderColor(c, Grid.BackColor);
                Theme.DrawBox(g, rt, c, border, RoundType.Rect, BoxStyle.Fill);

                var inf = new DvDataGridCellDrawInfo();
                CellDraw(g, Theme, CellBounds, inf);
                using (var p = new Pen(Color.Black))
                {
                    #region Bevel
                    if (Grid.Bevel && inf.Bevel)
                    {
                        var n = 0F;
                        var pts = new PointF[] {
                                            new PointF(rt.Right - n, rt.Top + 1 + n),
                                            new PointF(rt.Left + 1 + n, rt.Top + 1 + n),
                                            new PointF(rt.Left + 1 + n, rt.Bottom - n)
                                        };
                        p.Color = c.BrightnessTransmit(Theme.DataGridRowBevelBright);
                        p.Width = 1;
                        g.DrawLine(p, pts[0], pts[1]);
                        g.DrawLine(p, pts[1], pts[2]);
                    }
                    #endregion
                    #region Border
                    if (inf.Border)
                    {
                        p.Color = BorderColor;
                        p.Width = 1;
                        g.DrawLine(p, rt.Left, rt.Top, rt.Right, rt.Top);
                        g.DrawLine(p, rt.Left, rt.Top, rt.Left, rt.Bottom);
                        g.DrawLine(p, rt.Left, rt.Bottom, rt.Right, rt.Bottom);
                    }
                    #endregion
                }
            }
        }
        #endregion
        #region CellDraw
        public virtual void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info) { }
        #endregion
        #region MouseDown / MouseUp
        public virtual void MouseDown(RectangleF CellBounds, int x, int y)
        {
            if (Enabled && CollisionTool.Check(CellBounds, x, y))
            {
                bDown = true;
                CellMouseDown(CellBounds, x, y);
                Grid.InvokeCellMouseDown(this, x, y);
            }
        }
        public virtual void MouseUp(RectangleF CellBounds, int x, int y)
        {
            if (Enabled)
            {
                if (bDown)
                {
                    bDown = false;
        
                    CellMouseUp(CellBounds, x, y);
                    Grid.InvokeCellMouseUp(this, x, y);

                    if (CollisionTool.Check(CellBounds, x, y))
                    {
                        CellMouseClick(CellBounds, x, y);
                        Grid.InvokeCellMouseClick(this, x, y);
                    }
                }
            }
        }
        
        public virtual void MouseDoubleClick(RectangleF CellBounds, int x, int y)
        {
            if (Enabled && CollisionTool.Check(CellBounds, x, y))
            {
                CellMouseDoubleClick(CellBounds, x, y);
                Grid.InvokeCellDoubleClick(this, x, y);
            }
        }
        #endregion
        #region Cell MouseDown / MouseUp
        public virtual void CellMouseDown(RectangleF CellBounds, int x, int y) { }
        public virtual void CellMouseUp(RectangleF CellBounds, int x, int y) { }
        public virtual void CellMouseClick(RectangleF CellBounds, int x, int y) { }
        public virtual void CellMouseDoubleClick(RectangleF CellBounds, int x, int y) { }
        #endregion
        #region OnSetValue / OnGetValue
        protected virtual void OnSetValue() { }
        protected virtual void OnGetValue() { }
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

        public Color? CellBackColor { get; set; }
        public Color? CellTextColor { get; set; }

        public DvDataGrid Grid { get; private set; }
        public DvDataGridSummaryRow Row { get; private set; }
        #endregion

        #region Constructor
        public DvDataGridSummaryCell(DvDataGrid Grid, DvDataGridSummaryRow Row)
        {
            this.Grid = Grid;
            this.Row = Row;
        }
        #endregion

        #region Virtual Method
        #region Draw
        public virtual void Draw(Graphics g, DvTheme Theme, RectangleF CellBounds)
        {
            var rt = CellBounds;
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetSummaryRowColor(Theme);
            var BorderColor = Theme.GetBorderColor(CellBackColor, Grid.BackColor);

            Theme.DrawBox(g, rt, CellBackColor, BorderColor, RoundType.Rect, BoxStyle.Fill);

            using (var p = new Pen(Color.Black))
            {
                #region Bevel
                if (Grid.Bevel)
                {
                    var n = 0F;
                    var pts = new PointF[] {
                                            new PointF(rt.Right - n, rt.Top + 1 + n),
                                            new PointF(rt.Left + 1 + n, rt.Top + 1 + n),
                                            new PointF(rt.Left + 1 + n, rt.Bottom - n)
                                        };
                    p.Color = Theme.GetInBevelColor(CellBackColor);
                    p.Width = 1;
                    g.DrawLine(p, pts[0], pts[1]);
                    g.DrawLine(p, pts[1], pts[2]);
                }
                #endregion
                #region Border
                {
                    p.Color = BorderColor;
                    p.Width = 1;
                    g.DrawLine(p, rt.Left, rt.Top, rt.Right, rt.Top);
                    g.DrawLine(p, rt.Left, rt.Top, rt.Left, rt.Bottom);
                }
                #endregion
            }

            CellDraw(g, Theme, CellBounds);
        }
        #endregion
        #region CellDraw
        public virtual void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds) { }
        #endregion
        #region Calculation
        public virtual void Calculation() { }
        #endregion
        #endregion
    }
    #endregion

    #region class : DvDataGridTextFormatColumn
    public class DvDataGridTextFormatColumn : DvDataGridColumn
    {
        #region Properties
        public string Format { get; set; }
        #endregion
        #region Constructor
        public DvDataGridTextFormatColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridLabelCell);
        }
        #endregion
        #region Method
        #region GetText
        public string GetText(object Value)
        {
            var s = (string)null;
            if (string.IsNullOrWhiteSpace(Format)) s = (Value != null) ? Value.ToString() : null;
            else
            {
                if (Value is string) s = (string)Value;
                else if (Value is byte) s = (!string.IsNullOrWhiteSpace(Format) ? ((byte)Value).ToString(Format) : ((byte)Value).ToString());
                else if (Value is short) s = (!string.IsNullOrWhiteSpace(Format) ? ((short)Value).ToString(Format) : ((short)Value).ToString());
                else if (Value is ushort) s = (!string.IsNullOrWhiteSpace(Format) ? ((ushort)Value).ToString(Format) : ((ushort)Value).ToString());
                else if (Value is int) s = (!string.IsNullOrWhiteSpace(Format) ? ((int)Value).ToString(Format) : ((int)Value).ToString());
                else if (Value is uint) s = (!string.IsNullOrWhiteSpace(Format) ? ((uint)Value).ToString(Format) : ((uint)Value).ToString());
                else if (Value is long) s = (!string.IsNullOrWhiteSpace(Format) ? ((long)Value).ToString(Format) : ((long)Value).ToString());
                else if (Value is ulong) s = (!string.IsNullOrWhiteSpace(Format) ? ((ulong)Value).ToString(Format) : ((ulong)Value).ToString());
                else if (Value is float) s = (!string.IsNullOrWhiteSpace(Format) ? ((float)Value).ToString(Format) : ((float)Value).ToString());
                else if (Value is double) s = (!string.IsNullOrWhiteSpace(Format) ? ((double)Value).ToString(Format) : ((double)Value).ToString());
                else if (Value is decimal) s = (!string.IsNullOrWhiteSpace(Format) ? ((decimal)Value).ToString(Format) : ((decimal)Value).ToString());
                else if (Value is DateTime) s = (!string.IsNullOrWhiteSpace(Format) ? ((DateTime)Value).ToString(Format) : ((DateTime)Value).ToString());
                else if (Value is Enum) s = Value.ToString();
            }
            return s;
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridTextConverterColumn
    public class DvDataGridTextConverterColumn : DvDataGridColumn
    {
        #region Properties
        public Func<object, string> Converter { get; set; }
        #endregion
        #region Constructor
        public DvDataGridTextConverterColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridLabelCell);
        }
        #endregion
        #region Method
        #region GetText
        public string GetText(object Value)
        {
            var s = (string)null;
            if (Converter != null) s = Converter(Value);
            else if (Value != null) s = Value.ToString();
            return s;
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridLampColumn
    public class DvDataGridLampColumn : DvDataGridColumn
    {
        #region Properties
        public Color? OnColor { get; set; } = null;
        public Color? OffColor { get; set; } = null;
        public int? LampSize { get; set; } = null;
        #endregion
        #region Constructor
        public DvDataGridLampColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridLampCell);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridButtonColumn
    public class DvDataGridButtonColumn : DvDataGridColumn
    {
        #region Properties
        public string Text { get; set; }
        public string IconString { get; set; } = null;
        public int IconSize { get; set; } = 14;
        public int IconGap { get; set; } = 0;
        public DvTextIconAlignment IconAlignment { get; set; } = DvTextIconAlignment.LeftRight;
        #endregion
        #region Constructor
        public DvDataGridButtonColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridButtonCell);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridImageColumn
    public class DvDataGridImageColumn : DvDataGridColumn
    {
        #region Properties
        public DvPictureScaleMode ScaleMode { get; set; }
        #endregion
        #region Constructor
        public DvDataGridImageColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            ScaleMode = DvPictureScaleMode.Strech;
            CellType = typeof(DvDataGridImageCell);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridCheckBoxColumn
    public class DvDataGridCheckBoxColumn : DvDataGridColumn
    {
        #region Properties
        public Color? BoxColor { get; set; }
        public Color? CheckColor { get; set; }
        #endregion
        #region Constructor
        public DvDataGridCheckBoxColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridCheckBoxCell);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridComboBoxColumn
    public class DvDataGridComboBoxColumn : DvDataGridColumn
    {
        #region Properties
        public int ButtonWidth { get; set; } = 40;
        public int MaximumViewCount { get; set; } = 8;
        public int ItemHeight { get; set; } = 30;
        public int IconSize { get; set; } = 15;
        public int IconGap { get; set; } = 8;
        public DvTextIconAlignment IconAlignment { get; set; } = DvTextIconAlignment.LeftRight;
        public List<TextIcon> Items { get; set; }
        #endregion
        #region Constructor
        public DvDataGridComboBoxColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            Items = new List<TextIcon>();
            CellType = typeof(DvDataGridComboBoxCell);
            ItemHeight = Grid.RowHeight;
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridEditTextColumn
    public class DvDataGridEditTextColumn : DvDataGridColumn
    {
        #region Properties
        public KeyboardMode Mode { get; set; } = KeyboardMode.Korea;
        #endregion
        #region Constructor
        public DvDataGridEditTextColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridEditTextCell);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridEditNumberColumn
    public class DvDataGridEditNumberColumn<T> : DvDataGridColumn where T : struct
    {
        #region Properties
        public T? Maximum { get; set; } = null;
        public T? Minimum { get; set; } = null;
        public string Format { get; set; } = null;
        #endregion
        #region Constructor
        public DvDataGridEditNumberColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridEditNumberCell<T>);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridEditBoolColumn
    public class DvDataGridEditBoolColumn : DvDataGridColumn
    {
        #region Properties
        public string OnText { get; set; } = "ON";
        public string OffText { get; set; } = "OFF";
        #endregion
        #region Constructor
        public DvDataGridEditBoolColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridEditBoolCell);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridDateTimePickerColumn
    public class DvDataGridDateTimePickerColumn : DvDataGridColumn
    {
        #region Properties
        public string Format { get; set; }
        public DateTimePickerType PickerMode { get; set; }
        #endregion
        #region Constructor
        public DvDataGridDateTimePickerColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridEditDateTimeCell);
            PickerMode = DateTimePickerType.DateTime;
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridColorPickerColumn
    public class DvDataGridColorPickerColumn : DvDataGridColumn
    {
        #region Properties
        public ColorCodeType CodeType { get; set; } = ColorCodeType.CodeRGB;
        #endregion
        #region Constructor
        public DvDataGridColorPickerColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridEditColorCell);
        }
        #endregion
    }
    #endregion

    #region class : DvDataGridLabelCell
    public class DvDataGridLabelCell : DvDataGridCell
    {
        #region Constructor
        public DvDataGridLabelCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column) : base(Grid, Row, Column)
        {
        }
        #endregion

        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
            var BorderColor = Theme.GetBorderColor(CellTextColor, Grid.BackColor);

            var nc = Grid;
            var s = "";
            var Font = nc.Font;
            var val = Value;

            if (Column is DvDataGridTextFormatColumn) s = ((DvDataGridTextFormatColumn)Column).GetText(val);
            else if (Column is DvDataGridTextConverterColumn) s = ((DvDataGridTextConverterColumn)Column).GetText(val);
            else s = DvDataGridTool.GetText(val);

            if (!string.IsNullOrWhiteSpace(s))
                Theme.DrawText(g, s, Font, CellTextColor, CellBounds);

            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridLampCell
    public class DvDataGridLampCell : DvDataGridCell
    {
        #region Properties
        public Color? OnColor { get; set; }
        public Color? OffColor { get; set; }
        public int? LampSize { get; set; }
        #endregion

        #region Constructor
        public DvDataGridLampCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridLampColumn)
            {
                var c = Column as DvDataGridLampColumn;
                this.OnColor = c.OnColor;
                this.OffColor = c.OffColor;
                this.LampSize = c.LampSize;
            }
        }
        #endregion

        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            var ov = Value;
            var v = ov is bool ? (bool)ov : false;

            #region Color
            var OnColor = this.OnColor ?? Theme.LampOnColor;
            var OffColor = this.OffColor ?? Theme.LampOffColor;
            var vc = v ? OnColor : OffColor;
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
            var BorderColor = Theme.GetBorderColor(vc, vc);
            #endregion
            #region Lamp 
            {
                var c = v ? OnColor : OffColor;
                var sz = LampSize ?? Math.Min(CellBounds.Width, CellBounds.Height) - 4;
                var rt = Util.MakeRectangleAlign(CellBounds, new SizeF(sz, sz), DvContentAlignment.MiddleCenter);

                Theme.DrawLamp(g, rt, Row.Selected ? SelectedCellBackColor : CellBackColor, OnColor, OffColor, v);
            }
            #endregion
            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridButtonCell
    public class DvDataGridButtonCell : DvDataGridCell
    {
        #region Properties
        #endregion
        #region Member Variable
        private bool bDown = false;
        #endregion
        #region Constructor
        public DvDataGridButtonCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
        }
        #endregion
        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            #region Set
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
            var BorderColor = Theme.GetBorderColor(CellTextColor, Grid.BackColor);
            var ButtonColor = Row.Selected ? SelectedCellBackColor : CellBackColor;

            var nc = Grid;
            var Font = nc.Font;
            #endregion
            #region Draw
            {
                var rt = CellBounds;
                var rtText = CellBounds;

                var cF = bDown ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                var cB = bDown ? BorderColor.BrightnessTransmit(Theme.DownBrightness) : BorderColor;
                var cT = bDown ? CellTextColor.BrightnessTransmit(Theme.DownBrightness) : CellTextColor;
                var cL = Util.FromArgb(Theme.OutBevelAlpha, Color.White);

                if (!bDown) Theme.DrawBox(g, rt, cF, cB, RoundType.Rect, BoxStyle.GradientV);
                else Theme.DrawBox(g, rt, cF, cB, RoundType.Rect, BoxStyle.Fill | BoxStyle.InShadow);

                Info.Bevel = !bDown;
                

                if (Column is DvDataGridButtonColumn)
                {
                    var col = Column as DvDataGridButtonColumn;
                    var txtico = new TextIcon
                    {
                        Text = col.Text,
                        IconGap = col.IconGap,
                        IconAlignment = col.IconAlignment,
                        IconSize = col.IconSize,
                        IconString = col.IconString,
                    };
                    if (bDown) rtText.Offset(0, 1);
                    Theme.DrawTextIcon(g, txtico, Font, cT, rtText);
                }
            }
            #endregion
            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #region CellMouseDown
        public override void CellMouseDown(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                bDown = true;
            }
            base.CellMouseDown(CellBounds, x, y);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(RectangleF CellBounds, int x, int y)
        {
            if (bDown)
            {
                bDown = false;
                if (CollisionTool.Check(CellBounds, x, y)) Grid.InvokeCellButtonClick(this);
            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridImageCell
    public class DvDataGridImageCell : DvDataGridCell
    {
        #region Properties
        public DvPictureScaleMode ScaleMode { get; set; } = DvPictureScaleMode.Strech;
        #endregion
        #region Constructor
        public DvDataGridImageCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridImageColumn)
            {
                this.ScaleMode = ((DvDataGridImageColumn)Column).ScaleMode;
            }
        }
        #endregion
        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            #region Draw
            if (Value is Bitmap)
            {
                var rtv = CellBounds;

                var Image = (Bitmap)Value;
                var rtContent = CellBounds;
                #region Image
                var cp = MathTool.CenterPoint(rtContent);
                var cx = cp.X;
                var cy = cp.Y;
                switch (ScaleMode)
                {
                    case DvPictureScaleMode.Real:
                        g.DrawImage(Image, Util.FromRect(rtContent.Left, rtContent.Top, Image.Width, Image.Height));
                        break;
                    case DvPictureScaleMode.CenterImage:
                        g.DrawImage(Image, Util.FromRect(cx - (Image.Width / 2), cy - (Image.Height / 2), Image.Width, Image.Height));
                        break;
                    case DvPictureScaleMode.Strech:
                        g.DrawImage(Image, rtContent);
                        break;
                    case DvPictureScaleMode.Zoom:
                        double imgratio = 1D;
                        if ((Image.Width - rtContent.Width) > (Image.Height - rtContent.Height)) imgratio = (double)rtContent.Width / (double)Image.Width;
                        else imgratio = (double)rtContent.Height / (double)Image.Height;

                        int szw = Convert.ToInt32((double)Image.Width * imgratio);
                        int szh = Convert.ToInt32((double)Image.Height * imgratio);

                        g.DrawImage(Image, Util.FromRect(rtContent.Left + (rtContent.Width / 2) - (szw / 2), rtContent.Top + (rtContent.Height / 2) - (szh / 2), szw, szh));
                        break;
                }
                #endregion

            }
            #endregion

            Info.Bevel = false;
            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridCheckBoxCell
    public class DvDataGridCheckBoxCell : DvDataGridCell
    {
        #region Properties
        public Color? CheckBoxColor { get; set; }
        public Color? CheckColor { get; set; }
        #endregion
        #region Constructor
        public DvDataGridCheckBoxCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridCheckBoxColumn)
            {
                CheckBoxColor = ((DvDataGridCheckBoxColumn)Column).BoxColor;
                CheckColor = ((DvDataGridCheckBoxColumn)Column).CheckColor;
            }
        }
        #endregion
        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            if (Grid != null)
            {
                #region Var
                var sbw = Convert.ToInt32(DvDataGrid.SELECTOR_BOX_WIDTH);
                var BoxBright = Theme.DataGridCheckBoxBright;
                var RowColor = Grid.GetRowColor(Theme);
                var SelectedRowColor = Grid.GetSelectedRowColor(Theme);

                var Value = this.Value is bool ? (bool)this.Value : false;
                var cRow = Row.Selected ? SelectedRowColor : RowColor;
                var CheckBoxColor = this.CheckBoxColor ?? cRow.BrightnessTransmit(BoxBright);
                var CheckColor = this.CheckColor ?? Grid.ForeColor;
                var BorderColor = Theme.GetBorderColor(cRow, Grid.BackColor);
                #endregion

                #region Draw
                {
                    var Checked = Value;
                    var rtSelectorBox = MathTool.MakeRectangle(Util.INT(CellBounds), new SizeF(sbw, sbw));
                    Theme.DrawBox(g, rtSelectorBox, CheckBoxColor, BorderColor, RoundType.Rect, BoxStyle.Border | BoxStyle.Fill | BoxStyle.OutBevel | BoxStyle.InShadow);
                    if (Checked)
                    {
                        var INF = sbw / 4;
                        var rtCheck = Util.FromRect(rtSelectorBox.Left, rtSelectorBox.Top - 0, rtSelectorBox.Width, rtSelectorBox.Height); rtCheck.Inflate(-INF, -INF);
                        rtCheck.Inflate(-1, -1);

                        using (var p = new Pen(Color.Black))
                        {
                            p.Width = 3F;
                            p.Color = CheckColor;

                            p.Width = Convert.ToInt32(3);
                            p.Color = CheckColor;

                            var p1 = new PointF(rtCheck.X, rtCheck.Y + rtCheck.Height * 0.5F);
                            var p2 = new PointF(rtCheck.X + rtCheck.Width * 0.5F, rtCheck.Y + rtCheck.Height);
                            var p3 = new PointF(rtCheck.X + rtCheck.Width, rtCheck.Y);

                            g.DrawLines(p, new PointF[] { p1, p2, p3 });
                        }
                    }
                }
                #endregion
            }
            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #region CellMouseDown
        public override void CellMouseDown(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                var Value = this.Value is bool ? (bool)this.Value : false;
                var v = !((bool)Value);
                if (v != (bool)Value)
                {
                    #region Value Set
                    var old = Value;
                    this.Value = v;
                    Grid.InvokeValueChanged(this, old, v);
                    #endregion
                }
            }
            base.CellMouseDown(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridComboBoxCell
    public class DvDataGridComboBoxCell : DvDataGridCell
    {
        #region Properties
        public int ButtonWidth { get; set; } = 40;
        public int MaximumViewCount { get; set; } = 8;
        public int ItemHeight { get; set; } = 30;
        public List<TextIcon> Items { get; } = new List<TextIcon>();

        public Padding ItemPadding { get; set; } = new Padding(0, 0, 0, 0);
        public DvContentAlignment ContentAlignment { get; set; } = DvContentAlignment.MiddleCenter;
        #endregion

        #region Constructor
        public DvDataGridComboBoxCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridComboBoxColumn)
            {
                var col = ((DvDataGridComboBoxColumn)Column);
                Items.AddRange(col.Items);
                ItemHeight = col.ItemHeight;
                ButtonWidth = col.ButtonWidth;
                MaximumViewCount = col.MaximumViewCount;
            }
        }
        #endregion

        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            if (Grid != null  )
            {
                #region Var
                var rtContent = CellBounds;
                var rtIco = Util.FromRect(rtContent.Right - ButtonWidth, rtContent.Top, ButtonWidth, rtContent.Height);
                var rtBox = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width - rtIco.Width, rtContent.Height);
                var rtText = Util.FromRect(rtBox.Left + ItemPadding.Left, rtBox.Top + ItemPadding.Top, rtBox.Width - (ItemPadding.Left + ItemPadding.Right), rtBox.Height - (ItemPadding.Top + ItemPadding.Bottom));

                var BoxColor = Row.Selected ? Grid.GetSelectedRowColor(Theme) : Grid.GetRowColor(Theme);
                var ForeColor = Grid.ForeColor;
                var ItemColor = Grid.GetRowColor(Theme);
                var SelectedItemColor = Grid.GetSelectedRowColor(Theme);
                var BorderColor = Theme.GetBorderColor(BoxColor, Grid.BackColor);

                var Font  = Grid.Font;

                var col = Column as DvDataGridComboBoxColumn;
                var IconGap = col != null ? col.IconGap : 8;
                var IconSize = col != null ? col.IconSize : 15;
                var IconAlignment = col != null ? col.IconAlignment : DvTextIconAlignment.LeftRight;
                #endregion

                var vsel = Items.Where(x => x.Value.Equals(Value)).FirstOrDefault();
                var SelectedIndex = vsel != null ? Items.IndexOf(vsel) : -1;

                using (var p = new Pen(Color.Black))
                {
                    var rt = rtContent;

                    Theme.DrawBox(g, rtContent, BoxColor, BorderColor, RoundType.Rect, BoxStyle.Fill);

                    if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                    {
                        var v = Items[SelectedIndex];
                        Theme.DrawTextIcon(g, v, Font, ForeColor, rtText, ContentAlignment);
                    }

                    #region Icon
                    var nisz = Convert.ToInt32(rtIco.Height / 2);
                    Theme.DrawIcon(g, new DvIcon("fa-chevron-down", nisz), ForeColor, rtIco);
                    #endregion
                    #region Unit Sep
                    {
                        var szh = Convert.ToInt32(rtIco.Height / 2);

                        p.Width = 1;

                        p.Color = BoxColor.BrightnessTransmit(Theme.BorderBrightness);
                        g.DrawLine(p, rtIco.Left + 0F, (rtContent.Top + (rtContent.Height / 2)) - (szh / 2) + 1, rtIco.Left + 0F, (rtContent.Top + (rtContent.Height / 2)) + (szh / 2) + 1);

                        p.Color = Theme.GetInBevelColor(BoxColor);
                        g.DrawLine(p, rtIco.Left + 1F, (rtContent.Top + (rtContent.Height / 2)) - (szh / 2) + 1, rtIco.Left + 1F, (rtContent.Top + (rtContent.Height / 2)) + (szh / 2) + 1);
                    }
                    #endregion
                }
            }

            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #region CellMouseDown
        DateTime downTime;
        PointF downPoint;
        public override void CellMouseDown(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                downPoint = new PointF(x, y);
                downTime = DateTime.Now;
            }
            base.CellMouseDown(CellBounds, x, y);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && MathTool.GetDistance(downPoint, new PointF(x, y)) < 10 
                                                      && Items != null && Items.Count > 0)
            {
                OpenDropDown(CellBounds);
            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion

        #region DropDown
        #region Member Variable
        private bool closedWhileInControl;
        private DropDownContainer dropContainer;
        #endregion

        #region Properties
        #region CanDrop
        protected virtual bool CanDrop
        {
            get
            {
                if (dropContainer != null)
                    return false;

                if (dropContainer == null && closedWhileInControl)
                {
                    closedWhileInControl = false;
                    return false;
                }

                return !closedWhileInControl;
            }
        }
        #endregion
        #region DropState
        public DvDropState DropState { get; private set; }
        #endregion
        #endregion

        #region Method
        #region FreezeDropDown
        internal void FreezeDropDown(bool remainVisible)
        {
            if (dropContainer != null)
            {
                dropContainer.Freeze = true;
                if (!remainVisible)
                    dropContainer.Visible = false;
            }
        }
        #endregion
        #region UnFreezeDropDown
        internal void UnFreezeDropDown()
        {
            if (dropContainer != null)
            {
                dropContainer.Freeze = false;
                if (!dropContainer.Visible)
                    dropContainer.Visible = true;
            }
        }
        #endregion
        #region OpenDropDown
        private void OpenDropDown(RectangleF bounds)
        {
            if (Grid != null)
            {
                Grid.Move += (o, s) => { if (dropContainer != null) dropContainer.Bounds = GetDropDownBounds(bounds); };

                var vsel = Items.Where(x => x.Value.Equals(Value)).FirstOrDefault();
                var SelectedIndex = vsel != null ? Items.IndexOf(vsel) : -1;

                var vpos = SelectedIndex == -1 ? 0 : SelectedIndex * ItemHeight;
                vpos = (int)MathTool.Constrain(vpos - (ItemHeight * 2), 0, (Items.Count * ItemHeight));
                
                dropContainer = new DropDownContainer(this);
                dropContainer.Bounds = GetDropDownBounds(bounds);
                dropContainer.DropStateChanged += (o, s) => { DropState = s.DropState; };
                dropContainer.FormClosed += (o, s) =>
                {
                    if (!dropContainer.IsDisposed) dropContainer.Dispose();
                    dropContainer = null;
                    closedWhileInControl = (Grid.RectangleToScreen(Grid.ClientRectangle).Contains(Cursor.Position));
                    DropState = DvDropState.Closed;
                    
                    Grid.Invalidate();

                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        Thread.Sleep(20);
                        Grid.VisibleDropDown = false;
                    });
                };
                dropContainer.Shown += (o, s) => Grid.VisibleDropDown = true;
                
                DropState = DvDropState.Dropping;
                dropContainer.VScrollPosition = vpos;
                dropContainer.Show(Grid);
                
                DropState = DvDropState.Dropped;

                Grid.Invalidate();
            }
        }
        #endregion
        #region GetDropDownBounds
        private Rectangle GetDropDownBounds(RectangleF bounds)
        {
            if (Grid != null)
            {
                var col = Column as DvDataGridComboBoxColumn;
                var ItemViewCount = col.MaximumViewCount;
                
                int n = Items.Count;
                Point pt = Grid.Parent.PointToScreen(new Point(Grid.Left + Convert.ToInt32(bounds.Left), Grid.Top + Convert.ToInt32(bounds.Bottom) - 1));
                if (ItemViewCount != -1) n = Items.Count > ItemViewCount ? ItemViewCount : Items.Count;
                Size inflatedDropSize = new Size(Convert.ToInt32(bounds.Width), n * ItemHeight + 2);
                Rectangle screenBounds = new Rectangle(pt, inflatedDropSize);
                Rectangle workingArea = Screen.GetWorkingArea(screenBounds);

                if (screenBounds.X < workingArea.X) screenBounds.X = workingArea.X;
                if (screenBounds.Y < workingArea.Y) screenBounds.Y = workingArea.Y;

                if (screenBounds.Right > workingArea.Right && workingArea.Width > screenBounds.Width) screenBounds.X = workingArea.Right - screenBounds.Width;
                if (screenBounds.Bottom > workingArea.Bottom && workingArea.Height > screenBounds.Height) screenBounds.Y = pt.Y - Grid.Height - screenBounds.Height + 3;
                return screenBounds;
            }
            else
            {
                return new Rectangle(0, 0, 0, 0);
            }
        }
        #endregion
        #region CloseDropDown
        public void CloseDropDown()
        {
            if (dropContainer != null)
            {
                DropState = DvDropState.Closing;
                dropContainer.Freeze = false;
                dropContainer.Close();
            }
        }
        #endregion
        #region GetDropDownContainerDir
        internal int GetDropDownContainerDir()
        {
            int ret = -1;
            if (Grid != null)
            {
                if (DropState == DvDropState.Dropping || DropState == DvDropState.Dropped)
                {
                    var p1 = Grid.PointToScreen(new Point(0, 0));
                    var p2 = dropContainer.Location;

                    ret = p1.Y < p2.Y ? 1 : 2;
                }
            }
            return ret;
        }
        #endregion
        #region SetSelectIndexForNotRaiseEvent
        public void SetSelectIndexForNotRaiseEvent(int index)
        {
            if (index >= 0) Value = Items[index].Value;
            else Value = null;
            Grid?.Invalidate();
        }
        #endregion
        #endregion

        #region Class
        #region DropWindowEventArgs
        internal class DropWindowEventArgs : EventArgs
        {
            internal DvDropState DropState { get; private set; }
            public DropWindowEventArgs(DvDropState DropState)
            {
                this.DropState = DropState;
            }
        }
        #endregion
        #region DropDownContainer
        public class DropDownContainer : DvForm, IMessageFilter
        {
            #region Properties
            internal bool Freeze { get; set; }
            public DvDataGridComboBoxCell ComboBox { get; private set; }
            public double VScrollPosition
            {
                get => ListBox.ScrollPosition;
                set
                {
                    if (ListBox.ScrollPosition != value)
                    {
                        ListBox.ScrollPosition = value;
                        ListBox.Invalidate();
                    }
                }
            }
            #endregion

            #region Member Variable
            private DvListBox ListBox = new DvListBox();
            #endregion

            #region Event
            internal event EventHandler<DropWindowEventArgs> DropStateChanged;
            #endregion

            #region Constructor
            public DropDownContainer(DvDataGridComboBoxCell c)
            {
                #region Init
                this.BlankForm = true;
                this.DoubleBuffered = true;
                this.StartPosition = FormStartPosition.Manual;
                this.ShowInTaskbar = false;
                this.ControlBox = false;
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.AutoSize = false;
                this.AutoScroll = false;
                this.MinimumSize = new Size(10, 10);
                this.Padding = new Padding(0, 0, 0, 0);

                this.Theme = c.Grid.GetTheme();
                #endregion
                #region Set
                this.ComboBox = c;
                this.Font = c.Grid.Font;
                Application.AddMessageFilter(this);
                #endregion
                #region ListBox
                ListBox.Dock = DockStyle.Fill;
                ListBox.BoxColor = c.Grid.RowColor ?? Theme.RowColor;
                ListBox.Round = RoundType.Rect;
                ListBox.Items.AddRange(c.Items);
                ListBox.SelectionMode = ItemSelectionMode.Single;
                ListBox.ItemHeight = c.ItemHeight;

                ListBox.ItemClicked += (o, s) =>
                {
                    if (s.Item != null)
                    {
                        if (DropStateChanged != null) DropStateChanged.Invoke(this, new DropWindowEventArgs(DvDropState.Closing));

                        var v = s.Item.Value;
                        var old = c.Value;
                        c.Value = v;

                        c.Grid.InvokeValueChanged(c, old, v);
                        this.Close();
                    }
                };

                if (c.Value != null)
                {
                    var itm = c.Items.Where(x => x.Value.Equals(c.Value)).FirstOrDefault();
                    if (itm != null)
                        ListBox.SelectedItems.Add(itm);
                    //ListBox.SelectedItems.Add(c.Value);
                }
                this.Controls.Add(ListBox);
                #endregion

                #region Color
                var BoxColor = Theme.ListBackColor;
                var ItemColor = c.Grid.RowColor ?? Theme.RowColor;
                var SelectedItemColor = Theme.PointColor;
 
                this.BackColor = ListBox.BackColor = c.Grid.BoxColor ?? Theme.ListBackColor;
                this.ForeColor = ListBox.ForeColor = c.Grid.ForeColor;
                ListBox.BoxColor = BoxColor;
                ListBox.RowColor = ItemColor;
                ListBox.SelectedColor = SelectedItemColor;
                #endregion
            }
            #endregion

            #region Implements
            #region PreFilterMessage
            public bool PreFilterMessage(ref Message m)
            {
                if (!Freeze && this.Visible && (Form.ActiveForm == null || !Form.ActiveForm.Equals(this)))
                {
                    if (DropStateChanged != null) DropStateChanged.Invoke(this, new DropWindowEventArgs(DvDropState.Closing));
                    this.Close();
                }
                return false;
            }
            #endregion
            #endregion
        }
        #endregion
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridEditTextCell 
    public class DvDataGridEditTextCell : DvDataGridCell
    {
        #region Properties
        public bool ReadOnly { get; set; }
        #endregion

        #region Constructor
        public DvDataGridEditTextCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
        }
        #endregion

        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
            var BoxColor = (Row.Selected ? SelectedCellBackColor : CellBackColor).BrightnessTransmit(Theme.DataGridInputBright);

            var nc = Grid;
            var s = "";
            var Font = nc.Font;
            var val = Value;
            Theme.DrawBox(g, CellBounds, BoxColor, BoxColor.BrightnessTransmit(Theme.BorderBrightness), RoundType.Rect, BoxStyle.Fill);
            if (Value != null)
            {
                if (Value is string) s = (string)Value;
                else s = Value.ToString();

                if (!string.IsNullOrWhiteSpace(s))
                {
                    Theme.DrawText(g, s, Font, CellTextColor, CellBounds);
                }
            }
            Info.Bevel = false;
            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #region CellMouseDown
        DateTime downTime;
        Point downPoint;
        public override void CellMouseDown(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && !ReadOnly)
            {
                downPoint = new Point(x, y);
                downTime = DateTime.Now;
            }
            base.CellMouseDown(CellBounds, x, y);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && !ReadOnly && MathTool.GetDistance(downPoint, new Point(x, y)) < 10)
            {
                #region Click
                var mode = Column is DvDataGridEditTextColumn ? ((DvDataGridEditTextColumn)Column).Mode : KeyboardMode.Korea;

                var Wnd = Grid.FindForm() as DvForm;
                var Theme = Grid.GetTheme();
                var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
                var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
                var BoxColor = (Row.Selected ? SelectedCellBackColor : CellBackColor).BrightnessTransmit(Theme.DataGridInputBright);
                if (CollisionTool.Check(CellBounds, x, y))
                {

                    if (Theme.KeyboardInput) Grid.SetInput(this, CellBounds, BoxColor,(string)this.Value);
                    else
                    {
                        Wnd.Block = true;
                        var ret = DvDialogs.Keyboard.ShowKeyboard(Column.HeaderText, mode, Value as string);
                        if (ret != null)
                        {
                            var v = ret;
                            if (v != null)
                            {
                                var old = Value;
                                Value = v;
                                Grid.InvokeValueChanged(this, old, v);
                            }
                        }
                        Wnd.Block = false;
                    }
                }
                #endregion
            }

            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion

        #region Method
        #region TextChange
        internal void TextChange(TextBox txt)
        {
            var v = txt.Text;
            var old = Value;
            Value = v;
            Grid.InvokeValueChanged(this, old, v);
        }
        #endregion
        #region Flush
        internal void Flush()
        {
           
        }
        #endregion
        #region SetFocus
        internal void SetFocus(RectangleF rt)
        {
            var Wnd = Grid.FindForm() as DvForm;
            var Theme = Grid.GetTheme();

            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
            var BoxColor = (Row.Selected ? SelectedCellBackColor : CellBackColor).BrightnessTransmit(Theme.DataGridInputBright);

            Grid.SetInput(this, rt, BoxColor, (string)this.Value);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridEditNumberCell 
    public class DvDataGridEditNumberCell<T> : DvDataGridCell where T : struct
    {
        #region Properties
        public T? Maximum { get; set; } = null;
        public T? Minimum { get; set; } = null;
        public string Format { get; set; }
        public bool ReadOnly { get; set; }

        #region IsMinusInput
        private bool IsMinusInput
        {
            get
            {
                bool ret = false;
                if (typeof(T) == typeof(sbyte))
                {
                    #region short
                    var nMax = (sbyte?)(object)Maximum ?? sbyte.MaxValue;
                    var nMin = (sbyte?)(object)Minimum ?? sbyte.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(short))
                {
                    #region short
                    var nMax = (short?)(object)Maximum ?? short.MaxValue;
                    var nMin = (short?)(object)Minimum ?? short.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(int))
                {
                    #region int
                    var nMax = (int?)(object)Maximum ?? int.MaxValue;
                    var nMin = (int?)(object)Minimum ?? int.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(long))
                {
                    #region long
                    var nMax = (long?)(object)Maximum ?? long.MaxValue;
                    var nMin = (long?)(object)Minimum ?? long.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(float))
                {
                    #region float
                    var nMax = (float?)(object)Maximum ?? float.MaxValue;
                    var nMin = (float?)(object)Minimum ?? float.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(double))
                {
                    #region double
                    var nMax = (double?)(object)Maximum ?? double.MaxValue;
                    var nMin = (double?)(object)Minimum ?? double.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(decimal))
                {
                    #region decimal
                    var nMax = (decimal?)(object)Maximum ?? decimal.MaxValue;
                    var nMin = (decimal?)(object)Minimum ?? decimal.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                return ret;
            }
        }
        #endregion
        #region Error
        public InputError Error
        {
            get
            {
                var ret = InputError.None;

                if (typeof(T) == typeof(sbyte))
                {
                    #region short
                    sbyte n;
                    var state = sbyte.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((sbyte)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((sbyte)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(short))
                {
                    #region short
                    short n;
                    var state = short.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((short)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((short)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(int))
                {
                    #region int
                    int n;
                    var state = int.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((int)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((int)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(long))
                {
                    #region long
                    long n;
                    var state = long.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((long)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((long)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(byte))
                {
                    #region byte
                    byte n;
                    var state = byte.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((byte)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((byte)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(ushort))
                {
                    #region ushort
                    ushort n;
                    var state = ushort.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((ushort)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((ushort)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(uint))
                {
                    #region uint
                    uint n;
                    var state = uint.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((uint)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((uint)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(ulong))
                {
                    #region float
                    ulong n;
                    var state = ulong.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((ulong)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((ulong)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(float))
                {
                    #region float
                    float n;
                    var state = float.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((float)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((float)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(double))
                {
                    #region double
                    double n;
                    var state = double.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((double)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((double)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(decimal))
                {
                    #region decimal
                    decimal n;
                    var state = decimal.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((decimal)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((decimal)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }

                return ret;
            }
        }
        #endregion
        #endregion

        #region Member Variable
        string sVal = "";
        object old;
        #endregion

        #region Constructor
        public DvDataGridEditNumberCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridTextFormatColumn)
            {
                this.Format = ((DvDataGridTextFormatColumn)Column).Format;
            }
            else if (Column is DvDataGridEditNumberColumn<T>)
            {
                this.Minimum = ((DvDataGridEditNumberColumn<T>)Column).Minimum;
                this.Maximum = ((DvDataGridEditNumberColumn<T>)Column).Maximum;
                this.Format = ((DvDataGridEditNumberColumn<T>)Column).Format;
            }

            sVal = Value.ToString();
        }
        #endregion

        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            var col = ((DvDataGridEditNumberColumn<T>)Column);
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
            var BoxColor = (Row.Selected ? SelectedCellBackColor : CellBackColor).BrightnessTransmit(Theme.DataGridInputBright);
            
            var nc = Grid;
            var s = "";
            var Font = nc.Font;
            var val = Value;

            Theme.DrawBox(g, CellBounds, BoxColor, BoxColor.BrightnessTransmit(Theme.BorderBrightness), RoundType.Rect, BoxStyle.Fill);
            if (Value != null)
            {
                #region Value
                if (typeof(T) == typeof(byte)) s = (!string.IsNullOrWhiteSpace(Format) ? ((byte)Value).ToString(Format) : ((byte)Value).ToString());
                else if (typeof(T) == typeof(sbyte)) s = (!string.IsNullOrWhiteSpace(Format) ? ((sbyte)Value).ToString(Format) : ((sbyte)Value).ToString());
                else if (typeof(T) == typeof(short)) s = (!string.IsNullOrWhiteSpace(Format) ? ((short)Value).ToString(Format) : ((short)Value).ToString());
                else if (typeof(T) == typeof(ushort)) s = (!string.IsNullOrWhiteSpace(Format) ? ((ushort)Value).ToString(Format) : ((ushort)Value).ToString());
                else if (typeof(T) == typeof(int)) s = (!string.IsNullOrWhiteSpace(Format) ? ((int)Value).ToString(Format) : ((int)Value).ToString());
                else if (typeof(T) == typeof(uint)) s = (!string.IsNullOrWhiteSpace(Format) ? ((uint)Value).ToString(Format) : ((uint)Value).ToString());
                else if (typeof(T) == typeof(long)) s = (!string.IsNullOrWhiteSpace(Format) ? ((long)Value).ToString(Format) : ((long)Value).ToString());
                else if (typeof(T) == typeof(ulong)) s = (!string.IsNullOrWhiteSpace(Format) ? ((ulong)Value).ToString(Format) : ((ulong)Value).ToString());
                else if (typeof(T) == typeof(float)) s = (!string.IsNullOrWhiteSpace(Format) ? ((float)Value).ToString(Format) : ((float)Value).ToString());
                else if (typeof(T) == typeof(double)) s = (!string.IsNullOrWhiteSpace(Format) ? ((double)Value).ToString(Format) : ((double)Value).ToString());
                else if (typeof(T) == typeof(decimal)) s = (!string.IsNullOrWhiteSpace(Format) ? ((decimal)Value).ToString(Format) : ((decimal)Value).ToString());

                if (!string.IsNullOrWhiteSpace(s))
                {
                    Theme.DrawText(g, s, Font, CellTextColor, CellBounds);
                }
                #endregion
            }
            Info.Bevel = false;
            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #region CellMouseDown
        DateTime downTime;
        Point downPoint;
        public override void CellMouseDown(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && !ReadOnly)
            {
                downPoint = new Point(x, y);
                downTime = DateTime.Now;
            }
            base.CellMouseDown(CellBounds, x, y);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && !ReadOnly && MathTool.GetDistance(downPoint, new Point(x, y)) < 10)
            {
                #region Click
                var Wnd = Grid.FindForm() as DvForm;
                var Theme = Grid.GetTheme();

                var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
                var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
                var BoxColor = (Row.Selected ? SelectedCellBackColor : CellBackColor).BrightnessTransmit(Theme.DataGridInputBright);
                if (CollisionTool.Check(CellBounds, x, y))
                {
                    if (Theme.KeyboardInput)
                    {
                        sVal = Value.ToString();
                        Grid.SetInput(this, CellBounds, BoxColor, sVal);
                    }
                    else
                    {
                        Wnd.Block = true;

                        var ret = DvDialogs.Keypad.ShowKeypad<T>(Column.HeaderText, (T?)Value, Minimum, Maximum);
                        if (ret.HasValue && !((object)ret.Value).Equals(Value))
                        {
                            #region Set
                            var val = ret.Value;
                            var old = Value;

                            if (Minimum.HasValue && Maximum.HasValue)
                            {
                                var min = (object)Minimum.Value;
                                var max = (object)Maximum.Value;

                                if (typeof(T) == typeof(byte)) val = (T)(object)MathTool.Constrain((byte)(object)val, (byte)min, (byte)max);
                                else if (typeof(T) == typeof(sbyte)) val = (T)(object)MathTool.Constrain((sbyte)(object)val, (sbyte)min, (sbyte)max);
                                else if (typeof(T) == typeof(short)) val = (T)(object)MathTool.Constrain((short)(object)val, (short)min, (short)max);
                                else if (typeof(T) == typeof(ushort)) val = (T)(object)MathTool.Constrain((ushort)(object)val, (ushort)min, (ushort)max);
                                else if (typeof(T) == typeof(int)) val = (T)(object)MathTool.Constrain((int)(object)val, (int)min, (int)max);
                                else if (typeof(T) == typeof(uint)) val = (T)(object)MathTool.Constrain((uint)(object)val, (uint)min, (uint)max);
                                else if (typeof(T) == typeof(long)) val = (T)(object)MathTool.Constrain((long)(object)val, (long)min, (long)max);
                                else if (typeof(T) == typeof(ulong)) val = (T)(object)MathTool.Constrain((ulong)(object)val, (ulong)min, (ulong)max);
                                else if (typeof(T) == typeof(float)) val = (T)(object)MathTool.Constrain((float)(object)val, (float)min, (float)max);
                                else if (typeof(T) == typeof(double)) val = (T)(object)MathTool.Constrain((double)(object)val, (double)min, (double)max);
                                else if (typeof(T) == typeof(decimal)) val = (T)(object)MathTool.Constrain((decimal)(object)val, (decimal)min, (decimal)max);
                            }

                            Value = val;
                            Grid.InvokeValueChanged(this, old, (object)(ret.Value));
                            #endregion
                        }

                        Wnd.Block = false;
                    }
                }
                #endregion
            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion

        #region Method
        #region TextChange
        internal void TextChange(TextBox txt)
        {
            var textbox = txt;
            var t = typeof(T);
            if (typeof(T) == typeof(float) || typeof(T) == typeof(double) || typeof(T) == typeof(decimal))
            {
                #region Floating
                Int32 selectionStart = textbox.SelectionStart;
                Int32 selectionLength = textbox.SelectionLength;

                String newText = String.Empty;
                bool bComma = false;
                foreach (Char c in textbox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && !bComma && textbox.Text != ".") || (newText.Length == 0 && (c == '-' || c == '+') && IsMinusInput)) newText += c;
                    if (c == '.' && textbox.Text != ".") bComma = true;
                }
                textbox.Text = newText;
                textbox.SelectionStart = selectionStart <= textbox.Text.Length ? selectionStart : textbox.Text.Length;
                #endregion
            }
            else if (typeof(T) == typeof(sbyte) || typeof(T) == typeof(short) || typeof(T) == typeof(int) || typeof(T) == typeof(long) ||
                     typeof(T) == typeof(byte) || typeof(T) == typeof(ushort) || typeof(T) == typeof(uint) || typeof(T) == typeof(ulong))
            {
                #region Number
                Int32 selectionStart = textbox.SelectionStart;
                Int32 selectionLength = textbox.SelectionLength;

                String newText = String.Empty;
                foreach (Char c in textbox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || (newText.Length == 0 && (c == '-' || c == '+') && IsMinusInput)) newText += c;
                }
                textbox.Text = newText;
                textbox.SelectionStart = selectionStart <= textbox.Text.Length ? selectionStart : textbox.Text.Length;
                #endregion
            }

            sVal = txt.Text;
            Flush();

            var v = Value;
            if ((old != null && !old.Equals(v)) || old == null)
            {
                old = v;
                Grid.InvokeValueChanged(this, old, v);
            }

        }
        #endregion
        #region Flush
        internal void Flush()
        {
            #region Var
            byte nByte;
            ushort nUShort;
            uint nUInt;
            ulong nULong;
            sbyte nSByte;
            short nShort;
            int nInt;
            long nLong;
            float nFloat;
            double nDouble;
            decimal nDecimal;
            #endregion
            #region Parse
            {
                if (typeof(T) == typeof(byte) && byte.TryParse(sVal, out nByte))
                {
                    var nMax = (byte?)(object)Maximum ?? byte.MaxValue;
                    var nMin = (byte?)(object)Minimum ?? byte.MinValue;
                    if (nByte >= nMin && nByte <= nMax)
                    {
                        var v = nByte;
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
                else if (typeof(T) == typeof(ushort) && ushort.TryParse(sVal, out nUShort))
                {
                    var nMax = (ushort?)(object)Maximum ?? ushort.MaxValue;
                    var nMin = (ushort?)(object)Minimum ?? ushort.MinValue;
                    if (nUShort >= nMin && nUShort <= nMax)
                    {
                        var v = nUShort;
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
                else if (typeof(T) == typeof(uint) && uint.TryParse(sVal, out nUInt))
                {
                    var nMax = (uint?)(object)Maximum ?? uint.MaxValue;
                    var nMin = (uint?)(object)Minimum ?? uint.MinValue;
                    if (nUInt >= nMin && nUInt <= nMax)
                    {
                        var v = nUInt;
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
                else if (typeof(T) == typeof(ulong) && ulong.TryParse(sVal, out nULong))
                {
                    var nMax = (ulong?)(object)Maximum ?? ulong.MaxValue;
                    var nMin = (ulong?)(object)Minimum ?? ulong.MinValue;
                    if (nULong >= nMin && nULong <= nMax)
                    {
                        var v = nULong;
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
                else if (typeof(T) == typeof(sbyte) && sbyte.TryParse(sVal, out nSByte))
                {
                    var nMax = (sbyte?)(object)Maximum ?? sbyte.MaxValue;
                    var nMin = (sbyte?)(object)Minimum ?? sbyte.MinValue;
                    if (nSByte >= nMin && nSByte <= nMax)
                    {
                        Value = nSByte;
                    }
                }
                else if (typeof(T) == typeof(short) && short.TryParse(sVal, out nShort))
                {
                    var nMax = (short?)(object)Maximum ?? short.MaxValue;
                    var nMin = (short?)(object)Minimum ?? short.MinValue;
                    if (nShort >= nMin && nShort <= nMax)
                    {
                        var v = nShort;
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
                else if (typeof(T) == typeof(int) && int.TryParse(sVal, out nInt))
                {
                    var nMax = (int?)(object)Maximum ?? int.MaxValue;
                    var nMin = (int?)(object)Minimum ?? int.MinValue;
                    if (nInt >= nMin && nInt <= nMax)
                    {
                        var v = nInt;
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
                else if (typeof(T) == typeof(long) && long.TryParse(sVal, out nLong))
                {
                    var nMax = (long?)(object)Maximum ?? long.MaxValue;
                    var nMin = (long?)(object)Minimum ?? long.MinValue;
                    if (nLong >= nMin && nLong <= nMax)
                    {
                        var v = nLong;
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
                else if (typeof(T) == typeof(float) && float.TryParse(sVal, out nFloat))
                {
                    var nMax = (float?)(object)Maximum ?? float.MaxValue;
                    var nMin = (float?)(object)Minimum ?? float.MinValue;
                    if (nFloat >= nMin && nFloat <= nMax)
                    {
                        var v = nFloat;
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
                else if (typeof(T) == typeof(double) && double.TryParse(sVal, out nDouble))
                {
                    var nMax = (double?)(object)Maximum ?? double.MaxValue;
                    var nMin = (double?)(object)Minimum ?? double.MinValue;
                    if (nDouble >= nMin && nDouble <= nMax)
                    {
                        var v = nDouble;
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
                else if (typeof(T) == typeof(decimal) && decimal.TryParse(sVal, out nDecimal))
                {
                    var nMax = (decimal?)(object)Maximum ?? decimal.MaxValue;
                    var nMin = (decimal?)(object)Minimum ?? decimal.MinValue;
                    if (nDecimal >= nMin && nDecimal <= nMax)
                    {
                        var v = nDecimal;
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
            }
            #endregion
        }
        #endregion
        #region SetFocus
        internal void SetFocus(RectangleF rt)
        {
            var Wnd = Grid.FindForm() as DvForm;
            var Theme = Grid.GetTheme();

            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
            var BoxColor = (Row.Selected ? SelectedCellBackColor : CellBackColor).BrightnessTransmit(Theme.DataGridInputBright);

            sVal = Value.ToString();
            Grid.SetInput(this, rt, BoxColor, sVal);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridEditBoolCell 
    public class DvDataGridEditBoolCell : DvDataGridCell
    {
        #region Properties
        public bool ReadOnly { get; set; }

        public string OnText { get; set; } = "ON";
        public string OffText { get; set; } = "OFF";
        #endregion
        #region Member Variable
        Animation ani = new Animation();
        #endregion
        #region Constructor
        public DvDataGridEditBoolCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridEditBoolColumn)
            {
                var col = ((DvDataGridEditBoolColumn)Column);
                OnText = col.OnText;
                OffText = col.OffText;
            }
        }
        #endregion
        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            var frm = Grid.FindForm() as DvForm;

            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
            var BoxColor = (Row.Selected ? SelectedCellBackColor : CellBackColor).BrightnessTransmit(Theme.DataGridInputBright);

            var nc = Grid;
            var s = "";
            var Font = nc.Font;
            var val = Value;
            Theme.DrawBox(g, CellBounds, BoxColor, BoxColor.BrightnessTransmit(Theme.BorderBrightness), RoundType.Rect, BoxStyle.Fill);
            if (Value != null && val is bool)
            {
                bounds(CellBounds, (rtOn, rtOff) => {
                    using (var p = new Pen(Color.Black))
                    {
                        var v = (bool)val;
                        var thm = Theme;
                        var cL = CellTextColor;
                        var cD = Util.FromArgb(75, cL);
                        var cOn = v ? cL : cD;
                        var cOff = v ? cD : cL;

                        var isOn = v ? 12 : 0;
                        var isOff = v ? 0 : 12;
                        var igOn = v ? 5 : 0;
                        var igOff = v ? 0 : 5;

                        if (Theme.Animation && ani.IsPlaying)
                        {
                            if (v)
                            {
                                cOn = ani.Value(AnimationAccel.DCL, cD, cL);
                                cOff = ani.Value(AnimationAccel.DCL, cL, cD);
                                isOn = ani.Value(AnimationAccel.DCL, 0, 12);
                                isOff = ani.Value(AnimationAccel.DCL, 12, 0);
                                igOn = ani.Value(AnimationAccel.DCL, 0, 5);
                                igOff = ani.Value(AnimationAccel.DCL, 5, 0);
                            }
                            else
                            {
                                cOn = ani.Value(AnimationAccel.DCL, cL, cD);
                                cOff = ani.Value(AnimationAccel.DCL, cD, cL);
                                isOff = ani.Value(AnimationAccel.DCL, 0, 12);
                                isOn = ani.Value(AnimationAccel.DCL, 12, 0);
                                igOff = ani.Value(AnimationAccel.DCL, 0, 5);
                                igOn = ani.Value(AnimationAccel.DCL, 5, 0);
                            }
                        }

                        var tiOn = new TextIcon { Text = OnText, IconString = "fa-check", IconSize = isOn, IconGap = igOn };
                        var tiOff = new TextIcon { Text = OffText, IconString = "fa-check", IconSize = isOff, IconGap = igOff };

                        Theme.DrawTextIcon(g, tiOn, Font, cOn, rtOn);
                        Theme.DrawTextIcon(g, tiOff, Font, cOff, rtOff);

                        #region Unit Sep
                        var szh = Convert.ToInt32(CellBounds.Height / 2);
                        var x = Convert.ToInt32(CellBounds.Left + CellBounds.Width / 2);
                        {
                            p.Width = 1;

                            var vc = BoxColor.ToHSV();
                            var b = vc.V;

                            var y1 = (CellBounds.Top + (CellBounds.Height / 2)) - (szh / 2) + 1;
                            var y2 = (CellBounds.Top + (CellBounds.Height / 2)) + (szh / 2) + 1;

                            p.Color = b < 0.5 ? thm.GetInBevelColor(BoxColor) : BoxColor.BrightnessTransmit(thm.BorderBrightness);
                            g.DrawLine(p, x + 1F, y1, x + 1F, y2);

                            p.Color = b < 0.5 ? BoxColor.BrightnessTransmit(thm.BorderBrightness) : thm.GetInBevelColor(BoxColor);
                            g.DrawLine(p, x + 0F, y1, x + 0F, y2);
                        }
                        #endregion
                    }
                });
            }
            Info.Bevel = false;
            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #region CellMouseDown
        DateTime downTime;
        Point downPoint;
        public override void CellMouseDown(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && !ReadOnly)
            {
                downPoint = new Point(x, y);
                downTime = DateTime.Now;
            }
            base.CellMouseDown(CellBounds, x, y);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && !ReadOnly && MathTool.GetDistance(downPoint, new Point(x, y)) < 10)
            {
                var Theme = Grid.GetTheme();

                #region Click
                bounds(CellBounds, (rtOn, rtOff) =>
                {
                    var val = Value;
                    if (val != null && val is bool)
                    {
                        var vb = (bool)val;
                        if (CollisionTool.Check(rtOn, x, y) && !vb)
                        {
                            var v = true;
                            var old = Value;
                            Value = v;
                            Grid.InvokeValueChanged(this, old, v);

                            if (Theme?.Animation ?? false)
                            {
                                ani.Stop(); 
                                ani.Start(150, "", () => { if (Grid.Created && !Grid.IsDisposed && Grid.Visible) Grid.Invoke(new Action(() => Grid.Invalidate())); });
                            }
                        }
                        if (CollisionTool.Check(rtOff, x, y) && vb)
                        {
                            var v = false;
                            var old = Value;
                            Value = v;
                            Grid.InvokeValueChanged(this, old, v);

                            if (Theme?.Animation ?? false)
                            {
                                ani.Stop();
                                ani.Start(150, "", () => { if (Grid.Created && !Grid.IsDisposed && Grid.Visible) Grid.Invoke(new Action(() => Grid.Invalidate())); });
                            }
                        }
                    }
                });
                #endregion
            }

            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
        #region Method
        #region bounds
        void bounds(RectangleF rtValue, Action<RectangleF, RectangleF> act)
        {
            var w = rtValue.Width / 2F;
            var rtOn = Util.FromRect(rtValue.Left, rtValue.Top, w, rtValue.Height);
            var rtOff = Util.FromRect(rtValue.Left + w, rtValue.Top, w, rtValue.Height);
            act(rtOn, rtOff);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridEditDateTimeCell 
    public class DvDataGridEditDateTimeCell : DvDataGridCell
    {
        #region Properties
        public bool ReadOnly { get; set; }
        public string Format { get; set; }
        public DateTimePickerType PickerMode { get; set; }
        #endregion
        #region Constructor
        public DvDataGridEditDateTimeCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridDateTimePickerColumn)
            {
                this.Format = ((DvDataGridDateTimePickerColumn)Column).Format;
                this.PickerMode = ((DvDataGridDateTimePickerColumn)Column).PickerMode;
            }
        }
        #endregion
        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
            var BoxColor = (Row.Selected ? SelectedCellBackColor : CellBackColor).BrightnessTransmit(Theme.DataGridInputBright);

            var nc = Grid;
            var s = "";
            var Font = nc.Font;
            var val = Value;

            Theme.DrawBox(g, CellBounds, BoxColor, CellBackColor, RoundType.Rect, BoxStyle.Fill);
            if (Value != null)
            {
                if (Value is DateTime)
                {
                    if (Format == null)
                    {
                        switch (PickerMode)
                        {
                            case DateTimePickerType.DateTime: s = ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"); break;
                            case DateTimePickerType.Date: s = ((DateTime)Value).ToString("yyyy-MM-dd"); break;
                            case DateTimePickerType.Time: s = ((DateTime)Value).ToString("HH:mm:ss"); break;
                        }
                    }
                    else s = ((DateTime)Value).ToString(Format);
                }
                else s = Value.ToString();

                if (!string.IsNullOrWhiteSpace(s))
                {
                    Theme.DrawText(g, s, Font, CellTextColor, CellBounds);
                }
            }
            Info.Bevel = false;
            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #region CellMouseDown
        DateTime downTime;
        Point downPoint;
        public override void CellMouseDown(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && !ReadOnly)
            {
                downPoint = new Point(x, y);
                downTime = DateTime.Now;
            }
            base.CellMouseDown(CellBounds, x, y);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && !ReadOnly && MathTool.GetDistance(downPoint, new Point(x, y)) < 10)
            {
                #region Click
                if (PickerMode == DateTimePickerType.DateTime)
                {
                    var time = DvDialogs.DateTimeBox.ShowDateTimePicker(Column.HeaderText, (DateTime?)Value);
                    if (time.HasValue)
                    {
                        if (time.Value != (DateTime?)Value)
                        {
                            var old = Value;
                            Value = time.Value;
                            Grid.InvokeValueChanged(this, old, time.Value);
                        }
                    }
                }
                else if (PickerMode == DateTimePickerType.Date)
                {
                    var time = DvDialogs.DateTimeBox.ShowDatePicker(Column.HeaderText, (DateTime?)Value);
                    if (time.HasValue)
                    {
                        if (time.Value != (DateTime?)Value)
                        {
                            var old = Value;
                            Value = time.Value;
                            Grid.InvokeValueChanged(this, old, time.Value);
                        }
                    }
                }
                else if (PickerMode == DateTimePickerType.Time)
                {
                    var time = DvDialogs.DateTimeBox.ShowTimePicker(Column.HeaderText, (DateTime?)Value);
                    if (time.HasValue)
                    {
                        if (time.Value != (DateTime?)Value)
                        {
                            var old = Value;
                            Value = time.Value;
                            Grid.InvokeValueChanged(this, old, time.Value);
                        }
                    }
                }
                #endregion
            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridEditColorCell 
    public class DvDataGridEditColorCell : DvDataGridCell
    {
        #region Properties
        public bool ReadOnly { get; set; }
        #endregion
        #region Constructor
        public DvDataGridEditColorCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridColorPickerColumn)
            {
            }
        }
        #endregion
        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds, DvDataGridCellDrawInfo Info)
        {
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var SelectedCellBackColor = this.SelectedCellBackColor ?? Grid.GetSelectedRowColor(Theme);
            var BoxColor = (Row.Selected ? SelectedCellBackColor : CellBackColor).BrightnessTransmit(Theme.DataGridInputBright);

            var nc = Grid;
            var s = "";
            var Font = nc.Font;
            var val = Value;

            Theme.DrawBox(g, CellBounds, BoxColor, CellBackColor, RoundType.Rect, BoxStyle.Fill);
            if (Value != null && Value is Color)
            {
                var ForeColor = Grid.ForeColor;
                var BorderColor = Theme.GetBorderColor(BoxColor, Grid.BackColor);
                var Corner = Theme.Corner;
                var CodeType = (Column is DvDataGridColorPickerColumn) ? ((DvDataGridColorPickerColumn)Column).CodeType : ColorCodeType.CodeRGB;

                using (var p = new Pen(Color.Black))
                {
                    var vc = (Color)Value;
                    var sz = Math.Max(12, CellBounds.Height / 3);
                    s = ColorTool.GetName(vc, CodeType);

                    Util.TextIconBounds(g, CellBounds, DvContentAlignment.MiddleCenter, s, Font, 8, new SizeF(sz, sz), DvTextIconAlignment.LeftRight, (rtIcon, rtText) =>
                    {
                        rtIcon.Offset(0, 0);
                        Theme.DrawBox(g, rtIcon, vc, Color.Black, RoundType.Rect, BoxStyle.Fill | BoxStyle.Border | BoxStyle.OutShadow);
                        Theme.DrawText(g, s, Font, ForeColor, rtText, DvContentAlignment.MiddleCenter, true);
                    });
 
                }
            }
            Info.Bevel = false;
            base.CellDraw(g, Theme, CellBounds, Info);
        }
        #endregion
        #region CellMouseDown
        DateTime downTime;
        Point downPoint;
        public override void CellMouseDown(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && !ReadOnly)
            {
                downPoint = new Point(x, y);
                downTime = DateTime.Now;
            }
            base.CellMouseDown(CellBounds, x, y);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(RectangleF CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y) && !ReadOnly && MathTool.GetDistance(downPoint, new Point(x, y)) < 10)
            {
                #region Click
                var ret = DvDialogs.ColorBox.ShowColorPicker(Column.HeaderText, (Color?)Value);

                if (ret.HasValue)
                {
                    if (ret.Value != (Color?)Value)
                    {
                        var old = Value;
                        Value = ret.Value;
                        Grid.InvokeValueChanged(this, old, ret.Value);
                    }
                }
                #endregion
            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion

    #region class : DvDataGridSummaryLabelCell
    public class DvDataGridSummaryLabelCell : DvDataGridSummaryCell
    {
        #region Properties
        public string Text { get; set; }
        #endregion
        #region Constructor
        public DvDataGridSummaryLabelCell(DvDataGrid Grid, DvDataGridSummaryRow Row) : base(Grid, Row)
        {

        }
        #endregion
        #region Override
        #region CellDraw
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds)
        {
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var BorderColor = Theme.GetBorderColor(CellBackColor, Grid.BackColor);

            var nc = Grid;
            var Font = nc.Font;

            var s = !string.IsNullOrWhiteSpace(Text) ? Text : "";
            Theme.DrawText(g, s, Font, CellTextColor, CellBounds);

            base.CellDraw(g, Theme, CellBounds);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridSummarySumCell
    public class DvDataGridSummarySumCell : DvDataGridSummaryCell
    {
        #region Properties
        public string Format { get; set; }
        public decimal Value { get; private set; }
        #endregion
        #region Constructor
        public DvDataGridSummarySumCell(DvDataGrid Grid, DvDataGridSummaryRow Row) : base(Grid, Row)
        {

        }
        #endregion
        #region Virtual Method
        #region CellPaint
        public override void CellDraw(Graphics g, DvTheme Theme, RectangleF CellBounds)
        {
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var BorderColor = Theme.GetBorderColor(CellBackColor, Grid.BackColor);

            var nc = Grid;
            var Font = nc.Font;

            var s = string.IsNullOrWhiteSpace(Format) ? Value.ToString() : Value.ToString(Format);
            Theme.DrawText(g, s, Font, CellTextColor, CellBounds);

            base.CellDraw(g, Theme,  CellBounds);
        }
        #endregion
        #region Calc
        public override void Calculation()
        {
            Value = Grid.GetRows().Select(x => Convert.ToDecimal(x.Cells[ColumnIndex].Value)).Sum();
            base.Calculation();
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridSummaryAverageCell
    public class DvDataGridSummaryAverageCell : DvDataGridSummaryCell
    {
        #region Properties
        public string Format { get; set; }
        public decimal Value { get; private set; }
        #endregion
        #region Constructor
        public DvDataGridSummaryAverageCell(DvDataGrid Grid, DvDataGridSummaryRow Row) : base(Grid, Row)
        {

        }
        #endregion
        #region Virtual Method
        #region CellPaint
        public override void CellDraw(Graphics g, DvTheme Theme,  RectangleF CellBounds)
        {
            var CellTextColor = this.CellTextColor ?? Grid.ForeColor;
            var CellBackColor = this.CellBackColor ?? Grid.GetRowColor(Theme);
            var BorderColor = Theme.GetBorderColor(CellBackColor, Grid.BackColor);

            var nc = Grid;
            var Font = nc.Font;

            var rt = Util.INT(CellBounds);
            var s = string.IsNullOrWhiteSpace(Format) ? Value.ToString() : Value.ToString(Format);
            Theme.DrawText(g, s, Font, CellTextColor, rt);

            base.CellDraw(g, Theme, CellBounds);
        }
        #endregion
        #region Calc
        public override void Calculation()
        {
            Value = Grid.GetRows().Select(x => Convert.ToDecimal(x.Cells[ColumnIndex].Value)).Average();
            base.Calculation();
        }
        #endregion
        #endregion
    }
    #endregion

    #region class : DvDataGridCellDrawInfo
    public class DvDataGridCellDrawInfo
    {
        public bool Bevel { get; set; } = true;
        public bool Border { get; set; } = true;
    }
    #endregion
}
