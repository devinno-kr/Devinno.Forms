using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thread = System.Threading.Thread;
using ThreadPool = System.Threading.ThreadPool;

namespace Devinno.Forms.Controls
{
    public class DvLampButton : DvControl
    {
        #region Properties
        #region Text
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Text
        {
            get => base.Text;
            set { if (base.Text != value) { base.Text = value; Invalidate(); } }
        }

        private Padding padText = new Padding(0);
        public Padding TextPadding
        {
            get => padText;
            set { if (padText != value) { padText = value; Invalidate(); } }
        }
        #endregion
        #region ContentAlignment
        private DvContentAlignment eContentAlignment = DvContentAlignment.MiddleCenter;
        public DvContentAlignment ContentAlignment
        {
            get { return eContentAlignment; }
            set { if (eContentAlignment != value) { eContentAlignment = value; Invalidate(); } }
        }
        #endregion
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

        #region OnLampColor
        private Color? cOnLampColor = null;
        public Color? OnLampColor
        {
            get => cOnLampColor;
            set
            {
                if (cOnLampColor != value)
                {
                    cOnLampColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OffLampColor
        private Color? cOffLampColor = null;
        public Color? OffLampColor
        {
            get => cOffLampColor;
            set
            {
                if (cOffLampColor != value)
                {
                    cOffLampColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LampAlignment
        private DvTextIconAlignment eLampAlignment = DvTextIconAlignment.LeftRight;
        public DvTextIconAlignment LampAlignment
        {
            get => eLampAlignment;
            set
            {
                if(eLampAlignment != value)
                {
                    eLampAlignment = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LampGap
        private int nLampGap = 3;
        public int LampGap
        {
            get => nLampGap;
            set
            {
                if (nLampGap != value)
                {
                    nLampGap = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LampSize
        private int nLampSize = 20;
        public int LampSize
        {
            get => nLampSize;
            set
            {
                if(nLampSize != value)
                {
                    nLampSize = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OnOff
        private bool bOnOff = false;
        public bool OnOff
        {
            get { return bOnOff; }
            set
            {
                if (bOnOff != value)
                {
                    bOnOff = value;
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

        #region Gradient
        private bool bGradient = true;
        public bool Gradient
        {
            get => bGradient;
            set
            {
                if (bGradient != value)
                {
                    bGradient = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Clickable
        public bool Clickable { get; set; } = true;
        #endregion
        #region UseKey
        public bool UseKey { get; set; } = false;
        #endregion
        #endregion

        #region Member Variable
        private bool bDown = false;
        #endregion

        #region Event
        public event EventHandler ButtonClick;
        #endregion

        #region Constructor
        public DvLampButton()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 30);

            #region KeyPress
            KeyPress += (o, s) =>
            {
                if (UseKey && Focused)
                {
                    if (s.KeyChar == '\r' || s.KeyChar == ' ')
                    {
                        ThreadPool.QueueUserWorkItem((o) =>
                        {
                            this.Invoke(new Action(() =>
                            {
                                bDown = true;
                                Invalidate();

                            }));

                            Thread.Sleep(50);

                            this.Invoke(new Action(() =>
                            {
                                if (bDown)
                                {
                                    bDown = false;
                                    Invalidate();
                                    ButtonClick?.Invoke(this, null);
                                }
                            }));

                        });
                    }
                }
            };
            #endregion
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
            var BorderColor = Theme.GetBorderColor(ButtonColor, BackColor);
            var OnLampColor = this.OnLampColor ?? Theme.LampOnColor;
            var OffLampColor = this.OffLampColor ?? Theme.LampOffColor;
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent, rtLamp, rtText) =>
            {
                var cON = bDown ? OnLampColor.BrightnessTransmit(Theme.DownBrightness) : OnLampColor;
                var cOFF = bDown ? OffLampColor.BrightnessTransmit(Theme.DownBrightness) : OffLampColor;
                var cB = bDown ? BorderColor.BrightnessTransmit(Theme.DownBrightness) : BorderColor;
                var cF = bDown ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                var cT = bDown ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;
                var cL = Util.FromArgb(Theme.OutBevelAlpha, Color.White);


                if (!bDown) Theme.DrawBox(e.Graphics, rtContent, cF, cB, Round, Box.ButtonUp_V(Gradient, ShadowGap), Corner);
                else Theme.DrawBox(e.Graphics, rtContent, cF, cB, Round, Box.ButtonDown(ShadowGap), Corner);

                if (bDown)
                {
                    rtLamp.Offset(0, 1);
                    rtText.Offset(0, 1);
                }
                Theme.DrawLamp(e.Graphics, rtLamp, cF, cON, cOFF, OnOff);
                Theme.DrawText(e.Graphics, Text, Font, cT, rtText, ALIGN(ContentAlignment));
            });

            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Clickable)
            {
                Focus();

                bDown = true;
                Invalidate();
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Clickable)
            {
                if (bDown)
                {
                    bDown = false;
                    Invalidate();
                    ButtonClick?.Invoke(this, null);
                }
            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        /// <summary>
        /// ( rtContent, rtLamp, rtText )
        /// </summary>
        /// <param name="act"></param>
        public void Areas(Action<RectangleF, RectangleF, RectangleF> act)
        {
            var INF = LampSize / 8;

            var rtContent = GetContentBounds();
            var rtBounds = Util.FromRect(rtContent, TextPadding);
            using (var g = CreateGraphics())
            {
                Util.TextIconBounds(g,
                                    rtBounds, ContentAlignment,
                                    Text, Font,
                                    LampGap, new SizeF(LampSize, LampSize), LampAlignment,
                                    (rtLamp, rtText) =>
                {
                    act(rtContent, rtLamp, rtText);
                });
            }
        }
        #endregion
        #region ALIGN
        DvContentAlignment ALIGN(DvContentAlignment align)
        {
            var ret = DvContentAlignment.MiddleCenter;
            switch (align)
            {
                case DvContentAlignment.TopLeft:
                case DvContentAlignment.MiddleLeft:
                case DvContentAlignment.BottomLeft:
                    ret = DvContentAlignment.MiddleLeft;
                    break;

                case DvContentAlignment.TopCenter:
                case DvContentAlignment.MiddleCenter:
                case DvContentAlignment.BottomCenter:
                    ret = DvContentAlignment.MiddleCenter;
                    break;

                case DvContentAlignment.TopRight:
                case DvContentAlignment.MiddleRight:
                case DvContentAlignment.BottomRight:
                    ret = DvContentAlignment.MiddleRight;
                    break;
            }
            return ret;
        }
        #endregion
        #endregion
    }
}
