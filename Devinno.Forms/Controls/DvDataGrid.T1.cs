using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Controls
{
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
            #region Init
            var br = new SolidBrush(Color.Black);
            var p = new Pen(Color.Black);
            #endregion
            #region Draw
            {
                var s = "";

                if (Value is string) s = (string)Value;
                else if (Value != null) s = Value.ToString();
                if (!string.IsNullOrWhiteSpace(s))
                {
                    var old = g.ClipBounds;
                    g.SetClip(CellBounds);

                    var rt = new Rectangle((int)CellBounds.X, (int)CellBounds.Y, (int)CellBounds.Width, (int)CellBounds.Height);
                    var c = CellTextColor;
                    var bg = (Row.Selected ? SelectedCellBackColor : CellBackColor);
                    Theme.DrawTextShadow(g, null, s, Grid.Font, c, bg, rt);

                    g.ResetClip();
                    g.SetClip(CellBounds);
                }
            }
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #endregion
    }
    #endregion
}
