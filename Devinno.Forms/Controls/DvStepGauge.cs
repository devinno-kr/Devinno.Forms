using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvStepGauge : DvControl
    {
        #region Properties
        #region ButtonColor
        private Color? cButtonColor = null;
        public Color? ButtonColor
        {
            get => cButtonColor;
            set
            {
                if (cButtonColor != value)
                {
                    cButtonColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OnColor
        private Color? cOnColor = null;
        public Color? OnColor
        {
            get => cOnColor;
            set
            {
                if (cOnColor != value)
                {
                    cOnColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OffColor
        private Color? cOffColor = null;
        public Color? OffColor
        {
            get => cOffColor;
            set
            {
                if (cOffColor != value)
                {
                    cOffColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region ButtonStyle
        private DvStepButtonStyle eButtonStyle = DvStepButtonStyle.LeftRight;
        public DvStepButtonStyle ButtonStyle
        {
            get => eButtonStyle;
            set
            {
                if (eButtonStyle != value)
                {
                    eButtonStyle = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region UseButton
        private bool bUseButton = true;
        public bool UseButton
        {
            get => bUseButton;
            set
            {
                if (bUseButton != value)
                {
                    bUseButton = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Gap
        private int nGap = 7;
        public int Gap
        {
            get => nGap;
            set
            {
                if (nGap != value)
                {
                    nGap = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region StepCount
        private int nStepCount = 7;
        public int StepCount
        {
            get => nStepCount;
            set
            {
                if (nStepCount != value)
                {
                    nStepCount = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Step
        private int nStep = 0;
        public int Step
        {
            get => nStep;
            set
            {
                var v = Convert.ToInt32(MathTool.Constrain(value, 0, StepCount - 1));
                if (nStep != v)
                {
                    nStep = v;
                    StepChagend?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        bool bLeft = false;
        bool bRight = false;
        #endregion

        #region Event 
        public event EventHandler StepChagend;
        #endregion

        #region Constructor
        public DvStepGauge()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 30);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
            var OnColor = this.OnColor ?? Theme.StepOnColor;
            var OffColor = this.OffColor ?? Theme.StepOffColor;
            var Corner = Theme.Corner;
            var ButtonBorderColor = Theme.GetBorderColor(ButtonColor, BackColor);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            Pen p = new Pen(Color.Black);
            SolidBrush br = new SolidBrush(Color.Black);
            #endregion

            Areas((rtContent, rtLeft, rtRight, lsGauges, GP) =>
            {
                #region LeftButton
                if (rtLeft.HasValue)
                {
                    var rt = rtLeft.Value;
                    var cF = bLeft ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                    var cB = bLeft ? ButtonBorderColor.BrightnessTransmit(Theme.DownBrightness) : ButtonBorderColor;
                    var cT = bLeft ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;

                    if (!bLeft) Theme.DrawBox(e.Graphics, rt, cF, cB, RoundType.All, Box.ButtonUp_V(true, ShadowGap));
                    else Theme.DrawBox(e.Graphics, rt, cF, cB, RoundType.All, Box.ButtonDown(ShadowGap));

                    var s = ButtonStyle == DvStepButtonStyle.LeftRight ? "fa-chevron-left" : "fa-minus";
                    var ico = new DvIcon(s, 12);

                    if (bLeft) rt.Offset(0, 1);
                    Theme.DrawIcon(e.Graphics, ico, cT, rt);
                }
                #endregion
                #region RightButton
                if (rtRight.HasValue)
                {
                    var rt = rtRight.Value;
                    var cF = bRight ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                    var cB = bRight ? ButtonBorderColor.BrightnessTransmit(Theme.DownBrightness) : ButtonBorderColor;
                    var cT = bRight ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;

                    if (!bRight) Theme.DrawBox(e.Graphics, rt, cF, cB, RoundType.All, Box.ButtonUp_V(true, ShadowGap));
                    else Theme.DrawBox(e.Graphics, rt, cF, cB, RoundType.All, Box.ButtonDown(ShadowGap));

                    var s = ButtonStyle == DvStepButtonStyle.LeftRight ? "fa-chevron-right" : "fa-plus";
                    var ico = new DvIcon(s, 12);

                    if (bRight) rt.Offset(0, 1);
                    Theme.DrawIcon(e.Graphics, ico, cT, rt);
                }
                #endregion
                #region Gauge
                for (int i = 0; i < lsGauges.Count; i++)
                {
                    var rt = lsGauges[i];
                    if (rt.Width > 0 && rt.Height > 0)
                    {
                        var vc = (i != Step ? OffColor : OnColor);
                        var GaugeBorderColor = Theme.GetBorderColor(vc, BackColor);
                        Theme.DrawBox(e.Graphics, rt, vc, GaugeBorderColor, RoundType.All, Box.ButtonUp_V(true, ShadowGap));

                        var rtc = rt;
                        rtc.Inflate(-1.5F, -1.5F);
                        using (var opath = Util.GetBoxPath(rt, RoundType.All, Corner))
                        {
                            e.Graphics.SetClip(opath);
                            using (var path = Util.GetBoxPath(rtc, RoundType.All, Corner))
                            {
                                using (var lg = new LinearGradientBrush(rt, Color.FromArgb(Theme.GradientLightAlpha, Color.White), Color.FromArgb(Theme.GradientDarkAlpha, Color.Black), 90))
                                {
                                    using (var p2 = new Pen(lg))
                                    {
                                        p2.Width = 3;
                                        e.Graphics.DrawPath(p2, path);
                                    }
                                }
                            }
                            e.Graphics.ResetClip();
                        }
                    }
                }
                #endregion
            });

            #region Dispose
            p.Dispose();
            br.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            Areas((rtContent, rtLeft, rtRight, lsGauges, GP) =>
            {
                if (rtLeft.HasValue && CollisionTool.Check(rtLeft.Value, x, y)) bLeft = true;
                if (rtRight.HasValue && CollisionTool.Check(rtRight.Value, x, y)) bRight = true;
            });

            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            Areas((rtContent, rtLeft, rtRight, lsGauges, GP) =>
            {
                if (bLeft)
                {
                    if (rtLeft.HasValue && CollisionTool.Check(rtLeft.Value, x, y))
                        Step = Convert.ToInt32(MathTool.Constrain(Step - 1, 0, StepCount - 1));

                    bLeft = false;
                }

                if (bRight)
                {
                    if (rtRight.HasValue && CollisionTool.Check(rtRight.Value, x, y))
                        Step = Convert.ToInt32(MathTool.Constrain(Step + 1, 0, StepCount - 1));

                    bRight = false;
                }

                Invalidate();
            });
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF?, RectangleF?, List<RectangleF>, float> act)
        {
            var rtContent = GetContentBounds();
            var rtLeft = (RectangleF?)null;
            var rtRight = (RectangleF?)null;
            var rtGauge = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height);
            var lsGauges = new List<RectangleF>();
            var GP = Gap;

            if (UseButton)
            {
                rtGauge.Inflate(-(rtContent.Height + GP), 0);
                rtLeft = Util.MakeRectangleAlign(rtContent, new SizeF(rtContent.Height, rtContent.Height), DvContentAlignment.MiddleLeft);
                rtRight = Util.MakeRectangleAlign(rtContent, new SizeF(rtContent.Height, rtContent.Height), DvContentAlignment.MiddleRight);
            }

            var w = ((float)rtGauge.Width - ((float)GP * ((float)StepCount - 1F))) / (float)StepCount;
            for (int i = 0; i < StepCount; i++)
            {
                var x = (w + GP) * i;
                var rt = Util.FromRect(rtGauge.Left + ((w + GP) * i), rtGauge.Top, (w), rtGauge.Height);
                lsGauges.Add(rt);
            }

            act(rtContent, rtLeft, rtRight, lsGauges, GP);
        }
        #endregion
        #endregion
    }
}
