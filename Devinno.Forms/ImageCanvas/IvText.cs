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
    public class IvText : Control
    {
        #region Properties
        #region Text
        public override string Text
        {
            get => base.Text; 
            set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Alignment
        private StringAlignment eAlign = StringAlignment.Center;
        public StringAlignment Alignment
        {
            get => eAlign;
            set
            {
                if(eAlign != value)
                {
                    eAlign = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LineAlignment
        private StringAlignment eLineAlign = StringAlignment.Center;
        public StringAlignment LineAlignment
        {
            get => eLineAlign;
            set
            {
                if (eLineAlign != value)
                {
                    eLineAlign = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public IvText()
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
                if (page.OffImage != null)
                {
                    var bm = page.OffImage;
                    e.Graphics.DrawImage(bm, Util.FromRect(0, 0, Width, Height), Bounds, GraphicsUnit.Pixel);
                }
            }

            using (var strfrm = new StringFormat { Alignment = eAlign, LineAlignment = eLineAlign })
            {
                using (var br = new SolidBrush(ForeColor))
                {
                    e.Graphics.DrawString(Text, Font, br, Util.FromRect(0, 0, Width, Height), strfrm);
                }
            }

            base.OnPaint(e);
        }
        #endregion
        #endregion
    }
}
