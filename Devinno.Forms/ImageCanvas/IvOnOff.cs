using Devinno.Forms.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.ImageCanvas
{
    public class IvOnOff : Control
    {
        #region Properties
        private bool bOnOff = false;
        public bool OnOff
        {
            get => bOnOff;
            set
            {
                if(bOnOff != value)
                {
                    bOnOff = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Constructor
        public IvOnOff()
        {
            DoubleBuffered = true;
        }
        #endregion

        #region Override
        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent is IvPage)
            {
                var page = Parent as IvPage;
                if (page.OffImage != null && page.OnImage != null)
                {
                    var bm = OnOff ? page.OnImage : page.OffImage;
                    e.Graphics.DrawImage(bm, Util.FromRect(0, 0, Width, Height), Bounds, GraphicsUnit.Pixel);
                }
            }

            base.OnPaint(e);
        }
        #endregion
        #endregion
    }
}
