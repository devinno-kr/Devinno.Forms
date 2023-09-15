using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.ImageCanvas
{
    public class IvButton : Control
    {
        #region Event
        public event EventHandler ButtonClick;
        #endregion

        #region Member Variable
        bool bDown = false;
        #endregion

        #region Constructor
        public IvButton()
        {
            DoubleBuffered = true;
        }
        #endregion

        #region Override
        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            if(Parent is IvPage)
            {
                var page = Parent as IvPage;
                if (page.OffImage != null && page.OnImage != null)
                {
                    var bm = bDown ? page.OnImage : page.OffImage;
                    e.Graphics.DrawImage(bm, Util.FromRect(0, 0, Width, Height), Bounds, GraphicsUnit.Pixel);
                }
            }

            base.OnPaint(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(CollisionTool.Check(Util.FromRect(0,0,Width, Height), e.Location))
            {
                bDown = true;
                Invalidate();
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (bDown)
            {
                bDown = false;
                Invalidate();
                //ThreadPool.QueueUserWorkItem((o) => ButtonClick?.Invoke(this, null));
                ButtonClick?.Invoke(this, null);
            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion
    }
}
