using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Tools;
using Devinno.Forms.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        private Color cButtonColor = DvTheme.DefaultTheme.Color3;
        [Category("- 색상")]
        public Color ButtonColor
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
        private Color cOnColor = DvTheme.DefaultTheme.PointColor;
        [Category("- 색상")]
        public Color OnColor
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
        private Color cOffColor = DvTheme.DefaultTheme.Color2;
        [Category("- 색상")]
        public Color OffColor
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
        private StepButtonStyle eButtonStyle = StepButtonStyle.LeftRight;
        [Category("- 모양")]
        public StepButtonStyle ButtonStyle
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
        private int nStep = -1;
        public int Step
        {
            get => nStep;
            set
            {
                if (nStep != value)
                {
                    nStep = value;
                    StepChanged?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion

        #region UseButton
        private bool bUseButton = true;
        [Category("- 모양")]
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
        #endregion

        #region Member Variable
        private bool bLeft = false;
        private bool bRight = false;
        #endregion

        #region Event
        public event EventHandler StepChanged;
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
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);
            var CHSZ = g.MeasureString("H", Font);
            var rtContent = Areas["rtContent"];

            var rtGP = new Rectangle(0, 0, 10, 10);
            var rtBar = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height);
            SetArea("rtGP", rtGP);
            SetArea("rtBar", rtBar);

            if (UseButton)
            {
                var rtLeft = DrawingTool.MakeRectangleAlign(rtBar, new Size(rtBar.Height, rtBar.Height), DvContentAlignment.MiddleLeft);
                var rtRight = DrawingTool.MakeRectangleAlign(rtBar, new Size(rtBar.Height, rtBar.Height), DvContentAlignment.MiddleRight);
                var rtGauge = new Rectangle(rtBar.X, rtBar.Y, rtBar.Width, rtBar.Height); rtGauge.Inflate(-(rtGP.Width + rtBar.Height), 0);
                SetArea("rtLeft", rtLeft);
                SetArea("rtRight", rtRight);
                SetArea("rtGauge", rtGauge);
            }
            else
            {
                var rtGauge = new Rectangle(rtBar.X, rtBar.Y, rtBar.Width, rtBar.Height); 
                SetArea("rtGauge", rtGauge);
            }
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            if (Areas.Count > 1)
            {
                #region Color
                var ButtonColor = UseThemeColor ? Theme.Color3 : this.ButtonColor;
                var OnColor = UseThemeColor ? Theme.PointColor : this.OnColor;
                var OffColor = UseThemeColor ? Theme.Color2 : this.OffColor;
                #endregion
                #region Set
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                #endregion
                #region Bounds
                var GP = Areas["rtGP"].Width;
                var rtContent = Areas["rtContent"];
                var rtBar = Areas["rtBar"];
                var rtGauge = Areas["rtGauge"];
                #endregion
                #region Init
                var p = new Pen(ButtonColor, 1);
                var br = new SolidBrush(ButtonColor);
                #endregion
                #region Draw
                #region Button
                if (UseButton)
                {
                    var rtLeft = Areas["rtLeft"];
                    var rtRight = Areas["rtRight"];

                    br.Color = ForeColor;
                    if (!bLeft)
                    {
                        var c = ButtonColor;
                        var s = ButtonStyle == StepButtonStyle.LeftRight ? "fa-chevron-left" : "fa-minus";
                        Theme.DrawBox(e.Graphics, c, BackColor, rtLeft, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                        Theme.DrawTextShadow(e.Graphics, new DvIcon(s) { IconSize = rtLeft.Height / 5 }, null, Font, ForeColor, c, rtLeft, DvContentAlignment.MiddleCenter);
                    }
                    else
                    {
                        rtLeft.Offset(0, 1);
                        var c = ButtonColor.BrightnessTransmit(Theme.DownBright);
                        var s = ButtonStyle == StepButtonStyle.LeftRight ? "fa-chevron-left" : "fa-minus";
                        Theme.DrawBox(e.Graphics, c, BackColor, rtLeft, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                        Theme.DrawTextShadow(e.Graphics, new DvIcon(s) { IconSize = rtLeft.Height / 5 }, null, Font, ForeColor, c, rtLeft, DvContentAlignment.MiddleCenter);
                    }

                    if (!bRight)
                    {
                        var c = ButtonColor;
                        var s = ButtonStyle == StepButtonStyle.LeftRight ? "fa-chevron-right" : "fa-plus";
                        Theme.DrawBox(e.Graphics, c, BackColor, rtRight, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                        Theme.DrawTextShadow(e.Graphics, new DvIcon(s) { IconSize = rtRight.Height / 5 }, null, Font, ForeColor, c, rtRight, DvContentAlignment.MiddleCenter);
                    }
                    else
                    {
                        rtRight.Offset(0, 1);
                        var c = ButtonColor.BrightnessTransmit(Theme.DownBright);
                        var s = ButtonStyle == StepButtonStyle.LeftRight ? "fa-chevron-right" : "fa-plus";
                        Theme.DrawBox(e.Graphics, c, BackColor, rtRight, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                        Theme.DrawTextShadow(e.Graphics, new DvIcon(s) { IconSize = rtRight.Height / 5 }, null, Font, ForeColor, c, rtRight, DvContentAlignment.MiddleCenter);
                    }
                }
                #endregion
                #region Gauge
                var w = ((float)rtGauge.Width - ((float)GP * ((float)StepCount - 1F))) / (float)StepCount;
                for (int i = 0; i < StepCount; i++)
                {
                    var x = (w + GP) * i;
                    var rt = new Rectangle(rtGauge.X + Convert.ToInt32((w + GP) * i), rtGauge.Y, Convert.ToInt32(w), rtGauge.Height);
                    var rtin = new Rectangle(rt.X, rt.Y, rt.Width, rt.Height); rtin.Inflate(-3, -3);

                    var bg = BackColor.BrightnessTransmit(-0.3);

                    Theme.DrawBox(e.Graphics, (i != Step ? OffColor : OnColor), BackColor, rt, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.GRADIENT_V | BoxDrawOption.OUT_SHADOW);
                }
                #endregion
                #endregion
                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion
            }
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Areas.Count > 1)
            {
                if (UseButton && Areas.ContainsKey("rtLeft") && Areas.ContainsKey("rtRight"))
                {
                    if (CollisionTool.Check(Areas["rtLeft"], e.Location)) bLeft = true;
                    if (CollisionTool.Check(Areas["rtRight"], e.Location)) bRight = true;

                    Invalidate();
                }
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Areas.Count > 1)
            {
                if (UseButton && Areas.ContainsKey("rtLeft") && Areas.ContainsKey("rtRight"))
                {
                    if (bLeft && CollisionTool.Check(Areas["rtLeft"], e.Location))
                    {
                        Step = Convert.ToInt32(MathTool.Constrain(Step - 1, 0, StepCount - 1));
                    }

                    if (bRight && CollisionTool.Check(Areas["rtRight"], e.Location))
                    {
                        Step = Convert.ToInt32(MathTool.Constrain(Step + 1, 0, StepCount - 1));
                    }
                }
            }
            bLeft = bRight = false;
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion
    }

    #region enum : StepButtonStyle
    public enum StepButtonStyle { PlusMinus, LeftRight }
    #endregion
}
