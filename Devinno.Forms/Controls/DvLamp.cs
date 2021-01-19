using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Devinno.Forms.Extensions;
using Devinno.Forms.Tools;
using System.Drawing.Drawing2D;
using Devinno.Extensions;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace Devinno.Forms.Controls
{
    public class DvLamp : DvControl
    {
        #region Properties
        #region OnLampColor
        private Color cOnLampColor = DvTheme.DefaultTheme.PointColor;
        public Color OnLampColor
        {
            get { return cOnLampColor; }
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
        private Color cOffLampColor = DvTheme.DefaultTheme.Color3;
        public Color OffLampColor
        {
            get { return cOffLampColor; }
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
        #region LampBackColor
        private Color cLampBackColor = DvTheme.DefaultTheme.Color1;
        public Color LampBackColor
        {
            get { return cLampBackColor; }
            set
            {
                if (cLampBackColor != value)
                {
                    cLampBackColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region LampOffsetX
        private int nLampOffsetX = 0;
        public int LampOffsetX
        {
            get => nLampOffsetX;
            set
            {
                if (nLampOffsetX != value)
                {
                    nLampOffsetX = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LampOffsetY
        private int nLampOffsetY = 0;
        public int LampOffsetY
        {
            get => nLampOffsetY;
            set
            {
                if (nLampOffsetY != value)
                {
                    nLampOffsetY = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LampGap
        private int nLampGap = 0;
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
        private int nLampSize = 24;
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
        #region LampLightBright
        private double nLampLightBright = 0.5;
        public double LampLightBright
        {
            get => nLampLightBright;
            set
            {
                if (nLampLightBright != value)
                {
                    nLampLightBright = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LampDarkBright
        private double nLampDarkBright = -0.5;
        public double LampDarkBright
        {
            get => nLampDarkBright;
            set
            {
                if (nLampDarkBright != value)
                {
                    nLampDarkBright = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Text
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Category("- 모양")]
        public override string Text
        {
            get => base.Text;
            set { if (base.Text != value) { base.Text = value; Invalidate(); } }
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
        #endregion

        #region Constructor
        public DvLamp()
        {
            Size = new Size(36, 36);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);
         
            var rtContent = Areas["rtContent"];

            if (string.IsNullOrWhiteSpace(Text))
            {
                var ng = LampSize / 7;

                var szv = new Size(LampSize, LampSize);
                var rtFA = DrawingTool.MakeRectangleAlign(rtContent, szv, DvContentAlignment.MiddleCenter);
                var rtFAIN = new Rectangle(rtFA.X, rtFA.Y, rtFA.Width, rtFA.Height); rtFAIN.Inflate(-ng, -ng);

                SetArea("rtLampBack", rtFA);
                SetArea("rtLamp", rtFAIN);
            }
            else
            {
                var ng = LampSize / 7;
                var gap = LampGap;
                var f = DpiRatio;
                var szFA = new Size(LampSize, LampSize);
                var szTX = g.MeasureString(Text, Font);
                var szv = g.MeasureTextIcon(eLampAlignment, szFA, gap, Text, Font);

                var rt = DrawingTool.MakeRectangleAlign(rtContent, szv, DvContentAlignment.MiddleCenter);

                if (LampAlignment == DvTextIconAlignment.LeftRight)
                {
                    var rtFA = new Rectangle(rt.X, INTC(DrawingTool.CenterY(rt, szFA)), INTC(szFA.Width), INTC(szFA.Height)); rtFA.Offset(LampOffsetX, LampOffsetY);
                    var rtFAIN = new Rectangle(rtFA.X, rtFA.Y, rtFA.Width, rtFA.Height); rtFAIN.Inflate(-ng, -ng);
                    var rtTX = new Rectangle(rt.Right - INTC(szTX.Width), INTC(DrawingTool.CenterY(rt, szTX)), INTC(szTX.Width), INTC(szTX.Height));

                    SetArea("rtLampBack", rtFA);
                    SetArea("rtLamp", rtFAIN);
                    SetArea("rtText", rtTX);
                }
                else
                {
                    var rtFA = new Rectangle(INTC(DrawingTool.CenterX(rt, szFA)), rt.Y, INTC(szFA.Width), INTC(szFA.Height)); rtFA.Offset(LampOffsetX, LampOffsetY);
                    var rtFAIN = new Rectangle(rtFA.X, rtFA.Y, rtFA.Width, rtFA.Height); rtFAIN.Inflate(-ng, -ng);
                    var rtTX = new Rectangle(INTC(DrawingTool.CenterX(rt, szTX)), rt.Bottom - INTC(szTX.Height), INTC(szTX.Width), INTC(szTX.Height));

                    SetArea("rtLampBack", rtFA);
                    SetArea("rtLamp", rtFAIN);
                    SetArea("rtText", rtTX);
                }
            }
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var LampBackColor = UseThemeColor ? Theme.Color1 : this.LampBackColor ;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtText = Areas["rtText"];
            var rtBack = Areas["rtLampBack"];
            var rtLamp = Areas["rtLamp"];
            #endregion
            #region Init
            var p = new Pen(LampBackColor, 2);
            var br = new SolidBrush(LampBackColor);
            #endregion
            #region Draw
            Theme.DrawBox(e.Graphics, LampBackColor, BackColor, rtBack, RoundType.ELLIPSE, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL);
            #region Lamp
            if (OnOff)
            {
                using (var pth = new GraphicsPath())
                {
                    pth.AddEllipse(rtLamp);
                    using (var pbr = new PathGradientBrush(pth))
                    {
                        pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Left, rtLamp.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Top, rtLamp.Bottom)));
                        pbr.CenterColor = OnLampColor.BrightnessTransmit(LampLightBright);
                        pbr.SurroundColors = new Color[] { OnLampColor.BrightnessTransmit(LampDarkBright) };

                        e.Graphics.FillEllipse(pbr, rtLamp);
                    }
                    Theme.DrawBorder(e.Graphics, LampBackColor, rtLamp, RoundType.ELLIPSE);
                }
            }
            else
            {
                using (var pth = new GraphicsPath())
                {
                    pth.AddEllipse(rtLamp);
                    using (var pbr = new PathGradientBrush(pth))
                    {
                        pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Left, rtLamp.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Top, rtLamp.Bottom)));
                        pbr.CenterColor = OffLampColor.BrightnessTransmit(LampLightBright/2.0);
                        pbr.SurroundColors = new Color[] { OffLampColor.BrightnessTransmit(LampDarkBright/2.0) };

                        e.Graphics.FillEllipse(pbr, rtLamp);
                    }
                    Theme.DrawBorder(e.Graphics, LampBackColor, rtLamp, RoundType.ELLIPSE);
                }
            }
            #endregion
            #region Text
            if (!string.IsNullOrWhiteSpace(Text)) Theme.DrawTextShadow(e.Graphics, null, Text, Font, ForeColor, BackColor, rtText, DvContentAlignment.MiddleCenter);
            #endregion
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion

        #region Method
        private int INTR(float v) => Convert.ToInt32(Math.Round(v));
        private int INTC(float v) => Convert.ToInt32(Math.Ceiling(v));
        #endregion
    }
}
