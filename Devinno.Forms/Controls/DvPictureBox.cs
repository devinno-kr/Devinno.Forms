using Devinno.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvPictureBox : DvControl
    {
        #region Properties
        #region Image
        private Bitmap imgImage = null;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap Image
        {
            get => imgImage; set
            {
                if (imgImage != value)
                {
                    imgImage = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ScaleMode
        private PictureScaleMode eScaleMode = PictureScaleMode.Strech;
        public PictureScaleMode ScaleMode
        {
            get => eScaleMode;
            set
            {
                if (eScaleMode != value)
                {
                    eScaleMode = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BoxColor
        private Color cBoxColor = DvTheme.DefaultTheme.Color2;
        public Color BoxColor
        {
            get => cBoxColor;
            set { if (cBoxColor != value) { cBoxColor = value; Invalidate(); } }
        }
        #endregion
        #endregion

        #region Constructor
        public DvPictureBox()
        {

        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BoxColor = UseThemeColor ? this.BoxColor : Theme.Color2;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            #endregion
            #region Init
            var p = new Pen(BoxColor, 2);
            var br = new SolidBrush(BoxColor);
            #endregion
            #region Draw
            Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);

            if (Image != null)
            {
                using (var pth = DrawingTool.GetRoundRectPath(rtContent, Theme.Corner))
                {
                    e.Graphics.SetClip(pth);
                    #region Image
                    int cx = rtContent.X + (rtContent.Width / 2);
                    int cy = rtContent.Y + (rtContent.Height / 2);
                    switch (ScaleMode)
                    {
                        case PictureScaleMode.Real:
                            e.Graphics.DrawImage(Image, new Rectangle(rtContent.X, rtContent.Y, Image.Width, Image.Height));
                            break;
                        case PictureScaleMode.CenterImage:
                            e.Graphics.DrawImage(Image, new Rectangle(cx - (Image.Width / 2), cy - (Image.Height / 2), Image.Width, Image.Height));
                            break;
                        case PictureScaleMode.Strech:
                            e.Graphics.DrawImage(Image, rtContent);
                            break;
                        case PictureScaleMode.Zoom:
                            double imgratio = 1D;
                            if ((Image.Width - rtContent.Width) > (Image.Height - rtContent.Height)) imgratio = (double)rtContent.Width / (double)Image.Width;
                            else imgratio = (double)rtContent.Height / (double)Image.Height;

                            int szw = Convert.ToInt32((double)Image.Width * imgratio);
                            int szh = Convert.ToInt32((double)Image.Height * imgratio);

                            e.Graphics.DrawImage(Image, new Rectangle(rtContent.X + (rtContent.Width / 2) - (szw / 2), rtContent.Y + (rtContent.Height / 2) - (szh / 2), szw, szh));
                            break;
                    }
                    #endregion
                    e.Graphics.ResetClip();
                }
            }
            else Theme.DrawTextShadow(e.Graphics, null, "No Image", Font, BoxColor.BrightnessTransmit(0.5), BoxColor, rtContent);

            Theme.DrawBorder(e.Graphics, BackColor.BrightnessTransmit(Theme.BorderBright), BackColor, 1, rtContent, RoundType.ALL, BoxDrawOption.BORDER);
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion
    }

    #region enum : PictureScaleMode
    public enum PictureScaleMode { Real, CenterImage, Strech, Zoom }
    #endregion
}
