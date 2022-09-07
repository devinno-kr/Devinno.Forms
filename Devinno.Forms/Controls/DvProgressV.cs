using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
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
        #region BarColor
        private Color? cBarColor = null;
        public Color? BarColor
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
                if (nValue != value)
                {
                    nValue = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Round
        private RoundType? round = null;
        public RoundType? Round
        {
            get => round;
            set
            {
                if (round != value)
                {
                    round = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region FormatString
        private string sFormat = "0";
        public string FormatString
        {
            get => sFormat;
            set
            {
                if (sFormat != value)
                {
                    sFormat = value;
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
        #region InnerBar
        private bool bInnerBar = true;
        public bool InnerBar
        {
            get => bInnerBar;
            set
            {
                if (bInnerBar != value)
                {
                    bInnerBar = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region DrawText
        private bool bDrawText = false;
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
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, false);
            UpdateStyles();

            TabStop = false;
            #endregion

            Size = new Size(30, 200);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BoxColor = this.BoxColor ?? Theme.ConcaveBoxColor;
            var BarColor = this.BarColor ?? Theme.PointColor;
            var BorderColor = Theme.GetBorderColor(BarColor, BoxColor);
            var BoxBorderColor = Theme.GetBorderColor(BoxColor, BackColor);
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent, rtEmpty, rtFill) =>
            {
                Theme.DrawBox(e.Graphics, rtEmpty, BoxColor, BoxBorderColor, Round, Box.BackBox(ShadowGap));

                if (rtFill.Height > 0)
                    Theme.DrawBox(e.Graphics, rtFill, BarColor, BorderColor, InnerBar ? RoundType.All : Round, Box.ButtonUp_H(true, ShadowGap));

                if (DrawText && rtFill.Height > 0)
                {
                    e.Graphics.SetClip(rtFill);
                    var s = string.IsNullOrWhiteSpace(FormatString) ? Value.ToString() : Value.ToString(FormatString);
                    var sz = e.Graphics.MeasureString(s, Font); sz.Width += 2; sz.Height += 2;
                    var rt = Util.MakeRectangleAlign(Util.FromRect(rtFill, Reverse ? new Padding(0, 0, 0, 5) : new Padding(0, 5, 0, 0)), sz, Reverse ? DvContentAlignment.BottomCenter : DvContentAlignment.TopCenter);

                    Theme.DrawText(e.Graphics, s, Font, ForeColor, rt);
                    e.Graphics.ResetClip();
                }

            });

            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        public void Areas(Action<RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var ng = Convert.ToInt32(rtContent.Width * 0.1F);

            var rtEmpty = Util.FromRect(rtContent); 
            var hF = Convert.ToInt32(MathTool.Map(Value, Minimum, Maximum, 0, rtEmpty.Height));
            var rtFill = Util.FromRect(rtEmpty.Left, Reverse ? rtEmpty.Top : rtEmpty.Bottom - hF, rtEmpty.Width, hF);
            if (InnerBar) rtFill.Inflate(-ng, -ng);

            act(rtContent, rtEmpty, rtFill);
        }
        #endregion
        #endregion

    }
}
