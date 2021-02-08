using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvProgressV : DvControl
    {
        #region Properties
        #region BoxColor
        private Color cBoxColor = DvTheme.DefaultTheme.Color1;
        public Color BoxColor
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
        #region BarColor
        private Color cBarColor = DvTheme.DefaultTheme.PointColor;
        public Color BarColor
        {
            get => cBarColor;
            set
            {
                if (cBarColor != value)
                {
                    cBarColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Minimum
        private double nMinimum = 0D;
        public double Minimum
        {
            get => nMinimum;
            set
            {
                if (nMinimum != value)
                {
                    nMinimum = value;
                    if (nValue < nMinimum) nValue = nMinimum;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Maximum
        private double nMaximum = 100D;
        public double Maximum
        {
            get => nMaximum;
            set
            {
                if (nMaximum != value)
                {
                    nMaximum = value;
                    if (nValue > nMaximum) nValue = nMaximum;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Value
        private double nValue = 0D;
        public double Value
        {
            get => nValue;
            set
            {
                var v = MathTool.Constrain(value, Minimum, Maximum);
                if (nValue != v)
                {
                    nValue = v;
                    Invalidate();
                }
            }
        }
        #endregion
        #region FormatString
        private string sFormatString = null;
        public string FormatString
        {
            get => sFormatString;
            set
            {
                if (sFormatString != value)
                {
                    sFormatString = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Reverse
        private bool bReverse = false;
        public bool Reverse
        {
            get => bReverse;
            set
            {
                if (bReverse != value)
                {
                    bReverse = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region DrawText
        private bool bDrawText = true;
        public bool DrawText
        {
            get => bDrawText;
            set
            {
                if (bDrawText != value)
                {
                    bDrawText = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvProgressV()
        {
            this.Size = new Size(30, 300);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            var w = Convert.ToInt32(rtContent.Width * 0.6);
            var ng = rtContent.Width / 10;

            var rtEmpty = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height); rtEmpty.Inflate(-ng, -ng);

            var hF = Convert.ToInt32(MathTool.Map(Value, Minimum, Maximum, 0, rtEmpty.Height));
            var rtFill = new Rectangle(rtEmpty.X, Reverse ? rtEmpty.Y : rtEmpty.Bottom - hF, rtEmpty.Width, hF);
            SetArea("rtFill", rtFill);

            var s = string.IsNullOrWhiteSpace(FormatString) ? Maximum.ToString() : Maximum.ToString(FormatString);
            var sz = g.MeasureString(s, Font);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BoxColor = UseThemeColor ? Theme.Color1 : this.BoxColor;
            var FillColor = UseThemeColor ? Theme.PointColor : this.BarColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtFill = Areas["rtFill"];
            #endregion
            #region Init
            var p = new Pen(BoxColor, 1);
            var br = new SolidBrush(BoxColor);
            #endregion
            #region Draw
            Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.OUT_BEVEL | BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW);

            if (rtFill.Height > 0)
                Theme.DrawBox(e.Graphics, FillColor, BoxColor, rtFill, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW | BoxDrawOption.IN_BEVEL | BoxDrawOption.GRADIENT_H);

            if (DrawText)
            {
                var s = string.IsNullOrWhiteSpace(FormatString) ? Value.ToString() : Value.ToString(FormatString);
                var sz = e.Graphics.MeasureString(s, Font);
                var h = Convert.ToInt32(sz.Height + 5);
                var rt = new Rectangle(rtFill.X-1, Reverse ? rtFill.Bottom - h : rtFill.Top, rtFill.Width, h);

                if (rtFill.Width > 0)
                {
                    e.Graphics.SetClip(rtFill);
                    Theme.DrawTextShadow(e.Graphics, null, s, Font, ForeColor, FillColor, rt, Reverse ? DvContentAlignment.TopCenter : DvContentAlignment.BottomCenter);
                    e.Graphics.ResetClip();
                }
            }
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
}
