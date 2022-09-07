using Devinno.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Forms.Utils;
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
        private DvPictureScaleMode eScaleMode = DvPictureScaleMode.Strech;
        public DvPictureScaleMode ScaleMode
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
        private Color? cBoxColor = null;
        public Color? BoxColor
        {
            get => cBoxColor;
            set
            {
                if (cBoxColor != value)
                {
                    cBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BoxDraw
        public bool BoxDraw { get; set; } = true;
        #endregion
        #endregion

        #region Constructor
        public DvPictureBox()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, false);
            UpdateStyles();

            TabStop = false;
            #endregion

            Size = new Size(100, 100);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BoxColor = this.BoxColor ?? Theme.LabelColor;
            var BorderColor = Theme.GetBorderColor(BoxColor, BackColor);
            var Corner = Theme.Corner;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent) =>
            {
                if (BoxDraw)
                {
                    Theme.DrawBox(e.Graphics, rtContent, BoxColor, BorderColor, RoundType.All, Box.Style(Fill.Fill, Embossing.Convex, ShadowGap, false));

                    #region Image
                    if (Image != null)
                    {
                        using (var pth = DrawingTool.GetRoundRectPath(rtContent, Theme.Corner))
                        {
                            e.Graphics.SetClip(pth);
                            if (Image != null)
                            {
                                #region Rect
                                var cx = rtContent.X + (rtContent.Width / 2F);
                                var cy = rtContent.Y + (rtContent.Height / 2F);
                                var rt = rtContent;
                                switch (ScaleMode)
                                {
                                    case DvPictureScaleMode.Real:
                                        rt = new RectangleF(rtContent.X, rtContent.Y, Image.Width, Image.Height);
                                        break;
                                    case DvPictureScaleMode.CenterImage:
                                        rt = new RectangleF(cx - (Image.Width / 2), cy - (Image.Height / 2), Image.Width, Image.Height);
                                        break;
                                    case DvPictureScaleMode.Strech:
                                        rt = rtContent;
                                        break;
                                    case DvPictureScaleMode.Zoom:
                                        double imgratio = 1D;
                                        if ((Image.Width - rtContent.Width) > (Image.Height - rtContent.Height)) imgratio = (double)rtContent.Width / (double)Image.Width;
                                        else imgratio = (double)rtContent.Height / (double)Image.Height;

                                        int szw = Convert.ToInt32((double)Image.Width * imgratio);
                                        int szh = Convert.ToInt32((double)Image.Height * imgratio);

                                        rt = new RectangleF(rtContent.X + (rtContent.Width / 2) - (szw / 2), rtContent.Y + (rtContent.Height / 2) - (szh / 2), szw, szh);
                                        break;
                                }
                                #endregion
                                e.Graphics.DrawImage(Image, rt);
                            }
                            e.Graphics.ResetClip();
                        }
                    }
                    else
                    {
                        Theme.DrawText(e.Graphics, "No Image", Font, BoxColor.BrightnessTransmit(0.5), rtContent);
                    }
                    #endregion

                    Theme.DrawBox(e.Graphics, rtContent, BoxColor, BorderColor, RoundType.All, BoxStyle.Border);
                }
                else
                {
                    if (Image != null)
                    {
                        #region Rect
                        var cx = rtContent.X + (rtContent.Width / 2F);
                        var cy = rtContent.Y + (rtContent.Height / 2F);
                        var rt = rtContent;
                        switch (ScaleMode)
                        {
                            case DvPictureScaleMode.Real:
                                rt = new RectangleF(rtContent.X, rtContent.Y, Image.Width, Image.Height);
                                break;
                            case DvPictureScaleMode.CenterImage:
                                rt = new RectangleF(cx - (Image.Width / 2), cy - (Image.Height / 2), Image.Width, Image.Height);
                                break;
                            case DvPictureScaleMode.Strech:
                                rt = rtContent;
                                break;
                            case DvPictureScaleMode.Zoom:
                                double imgratio = 1D;
                                if ((Image.Width - rtContent.Width) > (Image.Height - rtContent.Height)) imgratio = (double)rtContent.Width / (double)Image.Width;
                                else imgratio = (double)rtContent.Height / (double)Image.Height;

                                int szw = Convert.ToInt32((double)Image.Width * imgratio);
                                int szh = Convert.ToInt32((double)Image.Height * imgratio);

                                rt = new RectangleF(rtContent.X + (rtContent.Width / 2) - (szw / 2), rtContent.Y + (rtContent.Height / 2) - (szh / 2), szw, szh);
                                break;
                        }
                        #endregion
                        e.Graphics.DrawImage(Image, rt);
                    }
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
