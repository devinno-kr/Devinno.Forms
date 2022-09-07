using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        #endregion

    }
}
