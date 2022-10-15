using Devinno.Forms.Extensions;
using Devinno.Forms.Themes;
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

namespace Devinno.Forms.Controls
{
    public class DvAnimate : DvControl
    {
        #region Properties
        #region OnImages
        public List<Bitmap> OnImages { get; } = new List<Bitmap>();
        #endregion
        #region OffImage
        private Bitmap bmOff = null;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap OffImage
        {
            get => bmOff;
            set
            {
                if(bmOff != value)
                {
                    bmOff = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OnOff
        private bool bOnOff = false;
        public bool OnOff
        {
            get => bOnOff;
            set
            {
                if (bOnOff != value)
                {
                    bOnOff = value;
                    if (bOnOff) ev.Set();
                    Invalidate();
                }
            }
        }
        #endregion
        #region Interval
        private int nInterval = 10;
        public int Interval
        {
            get => nInterval;
            set
            {
                if(nInterval != value)
                {
                    nInterval = Convert.ToInt32(MathTool.Constrain(value, 10, 60000));
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        int idx = 0;
        Thread th;
        AutoResetEvent ev = new AutoResetEvent(true);
        #endregion

        #region Constructor
        public DvAnimate()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, false);
            UpdateStyles();

            TabStop = false;
            #endregion

            Size = new Size(100, 100);

            #region Thread
            th = new Thread(() => { 
            
                while(!IsDisposed && !DesignMode)
                {
                    if (OnOff)
                    {
                        idx++;
                        if (idx >= OnImages.Count) idx = 0;

                        if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate()));
                        Thread.Sleep(Interval);
                    }
                    else
                    {
                        //ev.WaitOne();
                        Thread.Sleep(100);
                    }
                   
                }
            
            }) { IsBackground = true };
            th.Start();
            #endregion
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var Corner = Theme.Corner;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent) =>
            {
                if (bmOff != null && OnImages.Count > 0)
                {
                    if (DesignMode)
                    {
                        if (bmOff != null) e.Graphics.DrawImage(bmOff, rtContent);
                    }
                    else
                    {
                        if (OnOff)
                        {
                            if (idx >= 0 && idx < OnImages.Count) e.Graphics.DrawImage(OnImages[idx], rtContent);
                        }
                        else
                        {
                            if (bmOff != null) e.Graphics.DrawImage(bmOff, rtContent);
                        }
                    }

                }
                else
                {
                    var c = Color.FromArgb(120, ForeColor);
                    using (var p = new Pen(c)) e.Graphics.DrawRoundRectangle(p, rtContent, Corner);
                    Theme.DrawText(e.Graphics, "No Image", Font, c, rtContent);
                }
            });
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        public void Areas(Action<RectangleF> act)
        {
            var rtContent = GetContentBounds();

            act(rtContent);
        }
        #endregion
        #endregion
    }
}
