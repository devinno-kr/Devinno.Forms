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

        #region LampStyle
        private LampStyle eLampStyle = LampStyle.CIRCLE;
        public LampStyle LampStyle
        {
            get => eLampStyle;
            set
            {
                if(eLampStyle != value)
                {
                    eLampStyle = value;
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
                    if (nLampSize <= 0) nLampSize = 1;
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

        #region UseAnimation
        public bool UseAnimation { get; set; } = false;
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
                    if (UseAnimation)
                    {
                        if (bOnOff) StartOn();
                        else StartOff();
                    }
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        double oa = 0.0;
        bool ing = false;
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

            var wratio = (Width > Height ? (double)Width / (double)Height : 1D);
            var hratio = (Width < Height ? (double)Height / (double)Width : 1D);

            if (string.IsNullOrWhiteSpace(Text))
            {
                var ng = Math.Min(LampSize / 8, 9);
                var szv = LampStyle == LampStyle.CIRCLE ? new Size(LampSize, LampSize) : new Size(Convert.ToInt32(LampSize * wratio), Convert.ToInt32(LampSize * hratio));
                var rtFA = DrawingTool.MakeRectangleAlign(rtContent, szv, DvContentAlignment.MiddleCenter);
                var rtFAIN = new Rectangle(rtFA.X, rtFA.Y, rtFA.Width, rtFA.Height); rtFAIN.Inflate(-ng, -ng);

                SetArea("rtLampBack", rtFA);
                SetArea("rtLamp", rtFAIN);
            }
            else
            {
                if (LampStyle == LampStyle.CIRCLE)
                {
                    var ng = Math.Min(LampSize / 8, 9);
                    var gap = LampGap;
                    var f = DpiRatio;
                    var szFA = LampStyle == LampStyle.CIRCLE ? new Size(LampSize, LampSize) : new Size(Convert.ToInt32(LampSize * wratio), Convert.ToInt32(LampSize * hratio));
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
                else
                {
                    var ng = Math.Min(LampSize / 8, 9);
                    var szv = LampStyle == LampStyle.CIRCLE ? new Size(LampSize, LampSize) : new Size(Convert.ToInt32(LampSize * wratio), Convert.ToInt32(LampSize * hratio));
                    var rtFA = DrawingTool.MakeRectangleAlign(rtContent, szv, DvContentAlignment.MiddleCenter);
                    var rtFAIN = new Rectangle(rtFA.X, rtFA.Y, rtFA.Width, rtFA.Height); rtFAIN.Inflate(-ng, -ng);
                   
                    SetArea("rtLampBack", rtFA);
                    SetArea("rtLamp", rtFAIN);
                    SetArea("rtText", rtFAIN);
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
            var rtBack = Areas["rtLampBack"];
            var rtLamp = Areas["rtLamp"];
            #endregion
            #region Init
            var p = new Pen(LampBackColor, 2);
            var br = new SolidBrush(LampBackColor);
            #endregion
            #region Draw
            if (rtLamp.Width > 1 && rtLamp.Height > 1)
            {
                try
                {
                    if (LampStyle == LampStyle.CIRCLE)
                    {
                        Theme.DrawBox(e.Graphics, LampBackColor, BackColor, rtBack, RoundType.ELLIPSE, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL | BoxDrawOption.IN_SHADOW);

                        #region Lamp
                        if (OnOff)
                        {
                            var c = OnLampColor;
                            #region Animation
                            if (UseAnimation && ing)
                            {
                                var r = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OffLampColor.R, OnLampColor.R));
                                var g = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OffLampColor.G, OnLampColor.G));
                                var b = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OffLampColor.B, OnLampColor.B));
                                c = Color.FromArgb(r, g, b);
                            }
                            #endregion
                            #region Fill
                            using (var pth = new GraphicsPath())
                            {
                                pth.AddEllipse(rtLamp);
                                using (var pbr = new PathGradientBrush(pth))
                                {
                                    pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Left, rtLamp.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Top, rtLamp.Bottom)));
                                    pbr.CenterColor = c.BrightnessTransmit(LampLightBright);
                                    pbr.SurroundColors = new Color[] { c.BrightnessTransmit(LampDarkBright) };

                                    e.Graphics.FillEllipse(pbr, rtLamp);
                                }
                                Theme.DrawBorder(e.Graphics, LampBackColor.BrightnessTransmit(Theme.BorderBright), LampBackColor, 1, rtLamp, RoundType.ELLIPSE, BoxDrawOption.BORDER);
                            }
                            #endregion
                            #region InBevel
                            {
                                var rt = new Rectangle(rtLamp.X + 1, rtLamp.Y + 1, rtLamp.Width - 1, rtLamp.Height - 1);
                                var rtex = new Rectangle(rt.X, rt.Y, rt.Width - 1, rt.Height - 1);
                                if (rtex.X > 0 && rtex.Y > 0 && rtex.Width > 0 && rtex.Height > 0)
                                {
                                    e.Graphics.SetClip(rtex);
                                    var c1 = Color.FromArgb(Theme.BevelAlpha, Color.White);
                                    var c2 = Color.Transparent;

                                    using (var lgbr = new LinearGradientBrush(rtex, c1, c2, 75))
                                    {
                                        using (var p2 = new Pen(lgbr, 1F))
                                        {
                                            e.Graphics.DrawEllipse(p2, rt);
                                        }
                                    }
                                    e.Graphics.ResetClip();
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            var c = OffLampColor;
                            #region Animation
                            if (UseAnimation && ing)
                            {
                                var r = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OnLampColor.R, OffLampColor.R));
                                var g = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OnLampColor.G, OffLampColor.G));
                                var b = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OnLampColor.B, OffLampColor.B));
                                c = Color.FromArgb(r, g, b);
                            }
                            #endregion
                            #region Fill
                            using (var pth = new GraphicsPath())
                            {
                                pth.AddEllipse(rtLamp);
                                using (var pbr = new PathGradientBrush(pth))
                                {
                                    pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Left, rtLamp.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Top, rtLamp.Bottom)));
                                    pbr.CenterColor = c.BrightnessTransmit(LampLightBright);
                                    pbr.SurroundColors = new Color[] { c.BrightnessTransmit(LampDarkBright / 2.0) };

                                    e.Graphics.FillEllipse(pbr, rtLamp);
                                }
                                Theme.DrawBorder(e.Graphics, LampBackColor.BrightnessTransmit(Theme.BorderBright), LampBackColor, 1, rtLamp, RoundType.ELLIPSE, BoxDrawOption.BORDER);
                            }
                            #endregion
                            #region InBevel
                            {
                                var rt = new Rectangle(rtLamp.X + 1, rtLamp.Y + 1, rtLamp.Width - 1, rtLamp.Height - 1);
                                var rtex = new Rectangle(rt.X, rt.Y, rt.Width - 1, rt.Height - 1);
                                if (rtex.X > 0 && rtex.Y > 0 && rtex.Width > 0 && rtex.Height > 0)
                                {
                                    e.Graphics.SetClip(rtex);
                                    var c1 = Color.FromArgb(Theme.BevelAlpha, Color.White);
                                    var c2 = Color.Transparent;

                                    using (var lgbr = new LinearGradientBrush(rtex, c1, c2, 75))
                                    {
                                        using (var p2 = new Pen(lgbr, 1F))
                                        {
                                            e.Graphics.DrawEllipse(p2, rt);
                                        }
                                    }
                                    e.Graphics.ResetClip();
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        Theme.DrawBox(e.Graphics, LampBackColor, BackColor, rtBack, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL | BoxDrawOption.IN_SHADOW);
                        #region Lamp
                        if (OnOff)
                        {
                            var c = OnLampColor;
                            var AON = Convert.ToInt32(OnLampColor.ToHSV().V * 180);
                            var AOFF = AON / 3;
                            var BA = AON;
                            #region Animation
                            if (UseAnimation && ing)
                            {
                                var r = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OffLampColor.R, OnLampColor.R));
                                var g = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OffLampColor.G, OnLampColor.G));
                                var b = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OffLampColor.B, OnLampColor.B));
                                c = Color.FromArgb(r, g, b);
                                BA = Convert.ToInt32(MathTool.Map(oa, 0D, 1D, AOFF, AON));
                            }
                            #endregion
                            #region Fill
                            using (var pv = DrawingTool.GetRoundRectPath(rtLamp, Theme.Corner))
                            {
                                var old = e.Graphics.ClipBounds;
                                e.Graphics.ResetClip();
                                e.Graphics.SetClip(pv);
                                using (var pth = new GraphicsPath())
                                {
                                    var rt = new Rectangle(rtBack.X, rtBack.Y, rtBack.Width, rtBack.Height); rt.Inflate(rtBack.Width / 3, rtBack.Height / 3);
                                    pth.AddEllipse(rt);
                                    using (var pbr = new PathGradientBrush(pth))
                                    {
                                        pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Left, rtLamp.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Top, rtLamp.Bottom)));
                                        pbr.CenterColor = c.BrightnessTransmit(LampLightBright);
                                        pbr.SurroundColors = new Color[] { c.BrightnessTransmit(LampDarkBright) };
                                        e.Graphics.FillEllipse(pbr, rt);
                                    }
                                }
                                e.Graphics.ResetClip();
                                e.Graphics.SetClip(old);
                                Theme.DrawBorder(e.Graphics, LampBackColor.BrightnessTransmit(Theme.BorderBright), LampBackColor, 1, rtLamp, RoundType.ALL, BoxDrawOption.BORDER);
                            }
                            #endregion
                            #region Gradient
                            using (var lgbr = new LinearGradientBrush(rtLamp, Color.FromArgb(Convert.ToByte(MathTool.Constrain(Theme.BevelAlpha * 1.5, 0, 255)), Color.White), Color.FromArgb(0, Color.White), 90))
                            {
                                e.Graphics.FillRoundRectangle(lgbr, rtLamp, Theme.Corner);
                            }
                            #endregion
                            #region InBevel
                            {
                                var rt = new Rectangle(rtLamp.X + 1, rtLamp.Y + 1, rtLamp.Width - 1, rtLamp.Height - 1);
                                var rtex = new Rectangle(rt.X, rt.Y, rt.Width - 1, rt.Height - 1);
                                if (rtex.X > 0 && rtex.Y > 0 && rtex.Width > 0 && rtex.Height > 0)
                                {
                                    e.Graphics.SetClip(rtex);
                                    var c1 = Color.FromArgb(Theme.BevelAlpha, Color.White);
                                    var c2 = Color.Transparent;

                                    using (var lgbr = new LinearGradientBrush(rtex, c1, c2, 75))
                                    {
                                        using (var p2 = new Pen(lgbr, 1F))
                                        {
                                            e.Graphics.DrawRoundRectangle(p2, rt, Theme.Corner);
                                        }
                                    }
                                    e.Graphics.ResetClip();
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            var c = OffLampColor;
                            var AON = Convert.ToInt32(OnLampColor.ToHSV().V * 180);
                            var AOFF = AON / 3;
                            var BA = AOFF;
                            #region Animation
                            if (UseAnimation && ing)
                            {
                                var r = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OnLampColor.R, OffLampColor.R));
                                var g = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OnLampColor.G, OffLampColor.G));
                                var b = Convert.ToByte(MathTool.Map(oa, 0D, 1D, OnLampColor.B, OffLampColor.B));
                                c = Color.FromArgb(r, g, b);
                                BA = Convert.ToInt32(MathTool.Map(oa, 0D, 1D, AON, AOFF));
                            }
                            #endregion
                            #region Fill
                            using (var pv = DrawingTool.GetRoundRectPath(rtLamp, Theme.Corner))
                            {
                                var old = e.Graphics.ClipBounds;
                                e.Graphics.ResetClip();
                                e.Graphics.SetClip(pv);
                                using (var pth = new GraphicsPath())
                                {
                                    var rt = new Rectangle(rtBack.X, rtBack.Y, rtBack.Width, rtBack.Height); rt.Inflate(rtBack.Width / 3, rtBack.Height / 3);
                                    pth.AddEllipse(rt);
                                    using (var pbr = new PathGradientBrush(pth))
                                    {
                                        pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Left, rtLamp.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Top, rtLamp.Bottom)));
                                        pbr.CenterColor = c.BrightnessTransmit(LampLightBright);
                                        pbr.SurroundColors = new Color[] { c.BrightnessTransmit(LampDarkBright / 2.0) };
                                        e.Graphics.FillEllipse(pbr, rt);
                                    }
                                }
                                e.Graphics.ResetClip();
                                e.Graphics.SetClip(old);
                                Theme.DrawBorder(e.Graphics, LampBackColor.BrightnessTransmit(Theme.BorderBright), LampBackColor, 1, rtLamp, RoundType.ALL, BoxDrawOption.BORDER);
                            }
                            #endregion
                            #region Gardient
                            using (var lgbr = new LinearGradientBrush(rtLamp, Color.FromArgb(Convert.ToByte(MathTool.Constrain(Theme.BevelAlpha / 1.5, 0, 255)), Color.White), Color.FromArgb(0, Color.White), 90))
                            {
                                e.Graphics.FillRoundRectangle(lgbr, rtLamp, Theme.Corner);
                            }
                            #endregion
                            #region InBevel
                            {
                                var rt = new Rectangle(rtLamp.X + 1, rtLamp.Y + 1, rtLamp.Width - 1, rtLamp.Height - 1);
                                var rtex = new Rectangle(rt.X, rt.Y, rt.Width - 1, rt.Height - 1);
                                if (rtex.X > 0 && rtex.Y > 0 && rtex.Width > 0 && rtex.Height > 0)
                                {
                                    e.Graphics.SetClip(rtex);
                                    var c1 = Color.FromArgb(Theme.BevelAlpha, Color.White);
                                    var c2 = Color.Transparent;

                                    using (var lgbr = new LinearGradientBrush(rtex, c1, c2, 75))
                                    {
                                        using (var p2 = new Pen(lgbr, 1F))
                                        {
                                            e.Graphics.DrawRoundRectangle(p2, rt, Theme.Corner);
                                        }
                                    }
                                    e.Graphics.ResetClip();
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                }
                catch { }
            }
            #region Text
            if (!string.IsNullOrWhiteSpace(Text))
            {
                var rtText = Areas["rtText"];

                Theme.DrawTextShadow(e.Graphics, null, Text, Font, ForeColor, BackColor, rtText, DvContentAlignment.MiddleCenter);
            }
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
        #region INT
        private int INTR(float v) => Convert.ToInt32(Math.Round(v));
        private int INTC(float v) => Convert.ToInt32(Math.Ceiling(v));
        #endregion
        #region StartOn
        void StartOn()
        {
            var th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                var prev = DateTime.Now;
                var M = 150D;
                ing = true;
                while (bOnOff && (DateTime.Now - prev).TotalMilliseconds <= M)
                {
                    oa = (DateTime.Now - prev).TotalMilliseconds / M;
                    this.Invoke(new Action(() => Invalidate()));
                    System.Threading.Thread.Sleep(10);
                }
                ing = false;
                this.Invoke(new Action(() => Invalidate()));
            }))
            { IsBackground = true };
            th.Start();
        }
        #endregion
        #region StartOff
        void StartOff()
        {
            var th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                var prev = DateTime.Now;
                var M = 150D;
                ing = true;
                while (!bOnOff && (DateTime.Now - prev).TotalMilliseconds <= M)
                {
                    oa = ((DateTime.Now - prev).TotalMilliseconds / M);
                    this.Invoke(new Action(() => Invalidate()));
                    System.Threading.Thread.Sleep(10);
                }
                ing = false;
                this.Invoke(new Action(() => Invalidate()));
            }))
            { IsBackground = true };
            th.Start();
        }
        #endregion
        #endregion
    }

    #region enum : LampStyle
    public enum LampStyle { CIRCLE = 0, RECT = 1 }
    #endregion
}
