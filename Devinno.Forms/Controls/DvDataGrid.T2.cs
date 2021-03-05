using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Controls
{
    #region class : DvDataGridSummaryLabelCell
    public class DvDataGridSummaryLabelCell : DvDataGridSummaryCell
    {
        #region Properties
        public string Text { get; set; }
        public Color CellTextColor { get; set; }
        #endregion
        #region Constructor
        public DvDataGridSummaryLabelCell(DvDataGrid Grid, DvDataGridSummaryRow Row) : base(Grid, Row)
        {
            this.CellTextColor = Grid.ForeColor;
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds)
        {
            var rt = DvDataGrid.RTI(CellBounds);
            var s = !string.IsNullOrWhiteSpace(Text) ? Text : "";
            if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, CellTextColor, CellBackColor, rt);
            else Theme.DrawText(g, null, s, Grid.Font, CellTextColor, CellBackColor, rt);
            base.CellPaint(Theme, g, CellBounds);
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
        public Color CellTextColor { get; set; }
        public decimal Value { get; private set; }
        #endregion
        #region Constructor
        public DvDataGridSummarySumCell(DvDataGrid Grid, DvDataGridSummaryRow Row) : base(Grid, Row)
        {
            this.CellTextColor = Grid.ForeColor;
        }
        #endregion
        #region Virtual Method
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds)
        {
            var rt = DvDataGrid.RTI(CellBounds);
            var s = string.IsNullOrWhiteSpace(Format) ? Value.ToString() : Value.ToString(Format);
            if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, CellTextColor, CellBackColor, rt);
            else Theme.DrawText(g, null, s, Grid.Font, CellTextColor, CellBackColor, rt);
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region Calc
        public override void Calc()
        {
            Value = Grid.GetRows().Select(x => Convert.ToDecimal(x.Cells[ColumnIndex].Value)).Sum();
            base.Calc();
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
        public Color CellTextColor { get; set; }
        public decimal Value { get; private set; }
        #endregion
        #region Constructor
        public DvDataGridSummaryAverageCell(DvDataGrid Grid, DvDataGridSummaryRow Row) : base(Grid, Row)
        {
            this.CellTextColor = Grid.ForeColor;
        }
        #endregion
        #region Virtual Method
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds)
        {
            var rt = DvDataGrid.RTI(CellBounds);
            var s = string.IsNullOrWhiteSpace(Format) ? Value.ToString() : Value.ToString(Format);
            Theme.DrawTextShadow(g, null, s, Grid.Font, CellTextColor, CellBackColor, rt);
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region Calc
        public override void Calc()
        {
            Value = Grid.GetRows().Select(x => Convert.ToDecimal(x.Cells[ColumnIndex].Value)).Average();
            base.Calc();
        }
        #endregion
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
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds)
        {
            var s = "";
            if (Value is string) s = (string)Value;
            else if (Value != null) s = Value.ToString();
            
            if (!string.IsNullOrWhiteSpace(s))
            {
                var rt = DvDataGrid.RTI(CellBounds); 
                var c = CellTextColor;
                var bg = (Row.Selected ? SelectedCellBackColor : CellBackColor);
                if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, c, bg, rt);
                else Theme.DrawText(g, null, s, Grid.Font, c, bg, rt);
            }
         
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #endregion
    }
    #endregion
}
