using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.ImageCanvas
{
    public class IvAnimate : Control
    {
        #region Image
        #region OnImages
        private List<IvAniImage> lsOn = new List<IvAniImage>();
        public IvAniImage[] OnImages
        {
            get => lsOn.ToArray();
            set
            {
                lsOn.Clear();
                if (value != null) lsOn.AddRange(value);
            }
        }
        #endregion
        #region OffImage
        private Bitmap bmOff = null;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap OffImage
        {
            get => bmOff;
            set
            {
                if (bmOff != value)
                {
                    bmOff = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OnOff
        public bool OnOff
        {
            get => tmr.Enabled;
            set
            {
                if (tmr.Enabled != value)
                {
                    tmr.Enabled = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Interval
        public int Interval
        {
            get => tmr.Interval;
            set
            {
                if (tmr.Interval != value)
                {
                    tmr.Interval = Convert.ToInt32(MathTool.Constrain(value, 10, 60000));
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        int idx = 0;
        System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer() { Interval = 10 };
        #endregion

        #region Constructor
        public IvAnimate()
        {
            DoubleBuffered = true;

            #region tmr.Tick
            tmr.Tick += (o, s) =>
            {
                if (OnOff)
                {
                    idx++;
                    if (idx >= lsOn.Count) idx = 0;

                    if (Created && !IsDisposed && Visible) Invalidate();
                }
            };
            #endregion
        }
        #endregion

        #region Override
        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent is IvPage)
            {
                var rtContent = Util.FromRect(0, 0, Width, Height);
                var page = Parent as IvPage;
                if (page.OffImage != null)
                {
                    var bm = page.OffImage;
                    e.Graphics.DrawImage(bm, rtContent, Bounds, GraphicsUnit.Pixel);
                }

                if (bmOff != null && lsOn.Count > 0)
                {
                    if (DesignMode)
                    {
                        if (bmOff != null) e.Graphics.DrawImage(bmOff, rtContent);
                    }
                    else
                    {
                        if (OnOff)
                        {
                            if (idx >= 0 && idx < lsOn.Count && lsOn[idx].Image != null) e.Graphics.DrawImage(lsOn[idx].Image, rtContent);
                        }
                        else
                        {
                            if (bmOff != null) e.Graphics.DrawImage(bmOff, rtContent);
                        }
                    }
                }
            }

            base.OnPaint(e);
        }
        #endregion
        #endregion
    }

    public class IvAniImage
    {
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap Image { get; set; }

        public override string ToString() => "Image";
    }
}
