using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Themes
{
    public class BlackTheme : DvTheme
    {
        #region Static Properteis
        public static Color StaticForeColor { get; } = Color.White;
        public static Color StaticBackColor { get; } = Color.FromArgb(50, 50, 50);
        public static Color StaticColor0 { get; } = Color.FromArgb(20, 20, 20);
        public static Color StaticColor1 { get; } = Color.FromArgb(30, 30, 30);
        public static Color StaticColor2 { get; } = Color.FromArgb(60, 60, 60);
        public static Color StaticColor3 { get; } = Color.FromArgb(90, 90, 90);
        public static Color StaticColor4 { get; } = Color.FromArgb(120, 120, 120);
        public static Color StaticColor5 { get; } = Color.FromArgb(150, 150, 150);
        public static Color StaticPointColor { get; } = Color.DarkRed;
        public static Color StaticFrameColor { get; } = Color.FromArgb(30, 30, 30);
        public static Color StaticScrollBarColor { get; } = Color.FromArgb(30, 30, 30);
        public static Color StaticScrollCursorColor { get; } = Color.FromArgb(180, 180, 180);
        #endregion

        #region Properties
        public override string ThemeName { get; } = "BlackTheme";

        public override Color ForeColor { get; set; } = BlackTheme.StaticForeColor;
        public override Color BackColor { get; set; } = BlackTheme.StaticBackColor;
        public override Color Color0 { get; set; } = BlackTheme.StaticColor0;
        public override Color Color1 { get; set; } = BlackTheme.StaticColor1;
        public override Color Color2 { get; set; } = BlackTheme.StaticColor2;
        public override Color Color3 { get; set; } = BlackTheme.StaticColor3;
        public override Color Color4 { get; set; } = BlackTheme.StaticColor4;
        public override Color Color5 { get; set; } = BlackTheme.StaticColor5;
        public override Color PointColor { get; set; } = BlackTheme.StaticPointColor;
        public override Color FrameColor { get; set; } = BlackTheme.StaticFrameColor;
        public override Color ScrollBarColor { get; set; } = BlackTheme.StaticScrollBarColor;
        public override Color ScrollCursorColor { get; set; } = BlackTheme.StaticScrollCursorColor;

        public override int Corner { get; set; } = 5;
        public override int TextOffsetX { get; set; } = 0;
        public override int TextOffsetY { get; set; } = 1;
        public override int ShadowGap { get; set; } = 1;

        public override double DownBright { get; set; } = -0.25;
        public override double BorderBright { get; set; } = -0.5;
        public override double GradientLightBright { get; set; } = 0.2;
        public override double GradientDarkBright { get; set; } = -0.2;
        public override double OutShadowBright { get; set; } = -0.4;
        public override double InShadowBright { get; set; } = -0.3;
        public override double OutBevelBright { get; set; } = 0.2;
        public override double InBevelBright { get; set; } = 0.4;
        public override int DisableAlpha { get; set; } = 180;
        public override int BevelAlpha { get; set; } = 30;
        public override int ShadowAlpha { get; set; } = 60;

        public override ThemeMenuColorTable MenuColorTable { get; } = new BlackThemeMenuColorTable();
        #endregion

        #region Method
        #region DrawBorder
        public override void DrawBorder(Graphics g, Color borderColor, Color bg, int borderWidth, Rectangle bounds, RoundType round = RoundType.ALL, BoxDrawOption option = BoxDrawOption.NONE)
        {
            try
            {
                #region Init
                var br = new SolidBrush(Color.Black);
                var p = new Pen(Color.Black);
                #endregion

                #region OUT SHADOW
                if ((option & BoxDrawOption.OUT_SHADOW) == BoxDrawOption.OUT_SHADOW)
                {
                    var rt = new Rectangle(bounds.X + ShadowGap, bounds.Y + ShadowGap, bounds.Width, bounds.Height);

                    p.Width = borderWidth;
                    p.Color = bg.BrightnessTransmit(OutShadowBright);

                    switch (round)
                    {
                        case RoundType.NONE: g.DrawRectangle(p, rt); break;
                        case RoundType.ALL: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.L: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.R: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.T: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.B: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.LT: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.RT: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.LB: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.RB: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.ELLIPSE: g.DrawEllipse(p, rt); break;
                        case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                    }
                }
                #endregion
                #region OUT BEVEL
                if ((option & BoxDrawOption.OUT_BEVEL) == BoxDrawOption.OUT_BEVEL)
                {
                    var rt = new Rectangle(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height);

                    p.Width = borderWidth;
                    p.Color = bg.BrightnessTransmit(OutBevelBright);
                    switch (round)
                    {
                        case RoundType.NONE: g.DrawRectangle(p, rt); break;
                        case RoundType.ALL: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.L: g.DrawRoundRectangleL(p, rt, Corner); break;
                        case RoundType.R: g.DrawRoundRectangleR(p, rt, Corner); break;
                        case RoundType.T: g.DrawRoundRectangleT(p, rt, Corner); break;
                        case RoundType.B: g.DrawRoundRectangleB(p, rt, Corner); break;
                        case RoundType.LT: g.DrawRoundRectangleLT(p, rt, Corner); break;
                        case RoundType.RT: g.DrawRoundRectangleRT(p, rt, Corner); break;
                        case RoundType.LB: g.DrawRoundRectangleLB(p, rt, Corner); break;
                        case RoundType.RB: g.DrawRoundRectangleRB(p, rt, Corner); break;
                        case RoundType.ELLIPSE: g.DrawEllipse(p, rt); break;
                        case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                    }
                }
                #endregion
                #region OUT BEVEL RB
                if ((option & BoxDrawOption.OUT_BEVEL_RB) == BoxDrawOption.OUT_BEVEL_RB)
                {
                    var rt = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width, bounds.Height);

                    p.Width = borderWidth;
                    p.Color = bg.BrightnessTransmit(OutBevelBright);
                    switch (round)
                    {
                        case RoundType.NONE: g.DrawRectangle(p, rt); break;
                        case RoundType.ALL: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.L: g.DrawRoundRectangleL(p, rt, Corner); break;
                        case RoundType.R: g.DrawRoundRectangleR(p, rt, Corner); break;
                        case RoundType.T: g.DrawRoundRectangleT(p, rt, Corner); break;
                        case RoundType.B: g.DrawRoundRectangleB(p, rt, Corner); break;
                        case RoundType.LT: g.DrawRoundRectangleLT(p, rt, Corner); break;
                        case RoundType.RT: g.DrawRoundRectangleRT(p, rt, Corner); break;
                        case RoundType.LB: g.DrawRoundRectangleLB(p, rt, Corner); break;
                        case RoundType.RB: g.DrawRoundRectangleRB(p, rt, Corner); break;
                        case RoundType.ELLIPSE: g.DrawEllipse(p, rt); break;
                        case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                    }
                }
                #endregion
                #region IN SHADOW
                if ((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW)
                {
                    p.Width = borderWidth;
                    p.Color = borderColor.BrightnessTransmit(InShadowBright);

                    var rt = bounds;
                    switch (round)
                    {
                        case RoundType.NONE: g.DrawRectangle(p, rt); break;
                        case RoundType.ALL: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.L: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.R: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.T: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.B: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.LT: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.RT: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.LB: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.RB: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.ELLIPSE: g.DrawEllipse(p, rt); break;
                        case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                    }
                }
                #endregion
                #region IN BEVEL
                if ((option & BoxDrawOption.IN_BEVEL) == BoxDrawOption.IN_BEVEL)
                {
                    var rt = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2);
                    var c1 = bg.BrightnessTransmit(InBevelBright);
                    var c2 = Color.Transparent;
                    if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                        using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 90))
                        {
                            using (var p2 = new Pen(lgbr, borderWidth))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.DrawRectangle(p2, rt); break;
                                    case RoundType.ALL: g.DrawRoundRectangle(p2, rt, Corner); break;
                                    case RoundType.L: g.DrawRoundRectangleL(p2, rt, Corner); break;
                                    case RoundType.R: g.DrawRoundRectangleR(p2, rt, Corner); break;
                                    case RoundType.T: g.DrawRoundRectangleT(p2, rt, Corner); break;
                                    case RoundType.B: g.DrawRoundRectangleB(p2, rt, Corner); break;
                                    case RoundType.LT: g.DrawRoundRectangleLT(p2, rt, Corner); break;
                                    case RoundType.RT: g.DrawRoundRectangleRT(p2, rt, Corner); break;
                                    case RoundType.LB: g.DrawRoundRectangleLB(p2, rt, Corner); break;
                                    case RoundType.RB: g.DrawRoundRectangleRB(p2, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.DrawEllipse(p2, rt); break;
                                    case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p2, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                        }
                }
                #endregion
                #region IN BEVEL LT
                if ((option & BoxDrawOption.IN_BEVEL_LT) == BoxDrawOption.IN_BEVEL_LT)
                {
                    var oldclip = g.Clip.GetBounds(g);
                    var rt = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width - 1, bounds.Height - 1);
                    var rtex = new Rectangle(rt.X, rt.Y, rt.Width - 1, rt.Height - 1);
                    g.SetClip(rtex, CombineMode.Intersect);
                    var c1 = bg.BrightnessTransmit(InBevelBright);
                    var c2 = Color.Transparent;
                    if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                        using (var lgbr = new LinearGradientBrush(rtex, c1, c2, 75))
                        {
                            using (var p2 = new Pen(lgbr, borderWidth))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.DrawRectangle(p2, rt); break;
                                    case RoundType.ALL: g.DrawRoundRectangle(p2, rt, Corner); break;
                                    case RoundType.L: g.DrawRoundRectangleL(p2, rt, Corner); break;
                                    case RoundType.R: g.DrawRoundRectangleR(p2, rt, Corner); break;
                                    case RoundType.T: g.DrawRoundRectangleT(p2, rt, Corner); break;
                                    case RoundType.B: g.DrawRoundRectangleB(p2, rt, Corner); break;
                                    case RoundType.LT: g.DrawRoundRectangleLT(p2, rt, Corner); break;
                                    case RoundType.RT: g.DrawRoundRectangleRT(p2, rt, Corner); break;
                                    case RoundType.LB: g.DrawRoundRectangleLB(p2, rt, Corner); break;
                                    case RoundType.RB: g.DrawRoundRectangleRB(p2, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.DrawEllipse(p2, rt); break;
                                    case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p2, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                        }
                    g.SetClip(oldclip, CombineMode.Replace);
                }
                #endregion
                #region BORDER
                if ((option & BoxDrawOption.BORDER) == BoxDrawOption.BORDER)
                {
                    p.Color = borderColor;
                    p.Width = borderWidth;

                    switch (round)
                    {
                        case RoundType.NONE: g.DrawRectangle(p, bounds); break;
                        case RoundType.ALL: g.DrawRoundRectangle(p, bounds, Corner); break;
                        case RoundType.L: g.DrawRoundRectangleL(p, bounds, Corner); break;
                        case RoundType.R: g.DrawRoundRectangleR(p, bounds, Corner); break;
                        case RoundType.T: g.DrawRoundRectangleT(p, bounds, Corner); break;
                        case RoundType.B: g.DrawRoundRectangleB(p, bounds, Corner); break;
                        case RoundType.LT: g.DrawRoundRectangleLT(p, bounds, Corner); break;
                        case RoundType.RT: g.DrawRoundRectangleRT(p, bounds, Corner); break;
                        case RoundType.LB: g.DrawRoundRectangleLB(p, bounds, Corner); break;
                        case RoundType.RB: g.DrawRoundRectangleRB(p, bounds, Corner); break;
                        case RoundType.ELLIPSE: g.DrawEllipse(p, bounds); break;
                        case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p, bounds, Math.Max(bounds.Height, bounds.Width) * 2); break;
                    }
                }
                #endregion

                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion
            }
            catch { }
        }
        #endregion
        #region DrawBox
        public override void DrawBox(Graphics g, Color c, Color bg, Rectangle bounds, RoundType round = RoundType.ALL, BoxDrawOption option = BoxDrawOption.NONE)
        {
            try
            {
                #region Init
                var br = new SolidBrush(Color.Black);
                var p = new Pen(Color.Black);
                #endregion

                #region OUT SHADOW
                if ((option & BoxDrawOption.OUT_SHADOW) == BoxDrawOption.OUT_SHADOW)
                {
                    var rt = new Rectangle(bounds.X + ShadowGap, bounds.Y + ShadowGap, bounds.Width, bounds.Height);

                    br.Color = bg.BrightnessTransmit(OutShadowBright);
                    switch (round)
                    {
                        case RoundType.NONE: g.FillRectangle(br, rt); break;
                        case RoundType.ALL: g.FillRoundRectangle(br, rt, Corner); break;
                        case RoundType.L: g.FillRoundRectangleL(br, rt, Corner); break;
                        case RoundType.R: g.FillRoundRectangleR(br, rt, Corner); break;
                        case RoundType.T: g.FillRoundRectangleT(br, rt, Corner); break;
                        case RoundType.B: g.FillRoundRectangleB(br, rt, Corner); break;
                        case RoundType.LT: g.FillRoundRectangleLT(br, rt, Corner); break;
                        case RoundType.RT: g.FillRoundRectangleRT(br, rt, Corner); break;
                        case RoundType.LB: g.FillRoundRectangleLB(br, rt, Corner); break;
                        case RoundType.RB: g.FillRoundRectangleRB(br, rt, Corner); break;
                        case RoundType.ELLIPSE: g.FillEllipse(br, rt); break;
                        case RoundType.FULL_HORIZON: g.FillRoundRectangle(br, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                    }
                }
                #endregion
                #region OUT BEVEL
                if ((option & BoxDrawOption.OUT_BEVEL) == BoxDrawOption.OUT_BEVEL)
                {
                    var rt = new Rectangle(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height);

                    p.Width = 1;
                    p.Color = bg.BrightnessTransmit(OutBevelBright);
                    switch (round)
                    {
                        case RoundType.NONE: g.DrawRectangle(p, rt); break;
                        case RoundType.ALL: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.L: g.DrawRoundRectangleL(p, rt, Corner); break;
                        case RoundType.R: g.DrawRoundRectangleR(p, rt, Corner); break;
                        case RoundType.T: g.DrawRoundRectangleT(p, rt, Corner); break;
                        case RoundType.B: g.DrawRoundRectangleB(p, rt, Corner); break;
                        case RoundType.LT: g.DrawRoundRectangleLT(p, rt, Corner); break;
                        case RoundType.RT: g.DrawRoundRectangleRT(p, rt, Corner); break;
                        case RoundType.LB: g.DrawRoundRectangleLB(p, rt, Corner); break;
                        case RoundType.RB: g.DrawRoundRectangleRB(p, rt, Corner); break;
                        case RoundType.ELLIPSE: g.DrawEllipse(p, rt); break;
                        case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                    }
                }
                #endregion
                #region OUT BEVEL RB
                if ((option & BoxDrawOption.OUT_BEVEL_RB) == BoxDrawOption.OUT_BEVEL_RB)
                {
                    var rt = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width, bounds.Height);

                    p.Width = 1;
                    p.Color = bg.BrightnessTransmit(OutBevelBright);
                    switch (round)
                    {
                        case RoundType.NONE: g.DrawRectangle(p, rt); break;
                        case RoundType.ALL: g.DrawRoundRectangle(p, rt, Corner); break;
                        case RoundType.L: g.DrawRoundRectangleL(p, rt, Corner); break;
                        case RoundType.R: g.DrawRoundRectangleR(p, rt, Corner); break;
                        case RoundType.T: g.DrawRoundRectangleT(p, rt, Corner); break;
                        case RoundType.B: g.DrawRoundRectangleB(p, rt, Corner); break;
                        case RoundType.LT: g.DrawRoundRectangleLT(p, rt, Corner); break;
                        case RoundType.RT: g.DrawRoundRectangleRT(p, rt, Corner); break;
                        case RoundType.LB: g.DrawRoundRectangleLB(p, rt, Corner); break;
                        case RoundType.RB: g.DrawRoundRectangleRB(p, rt, Corner); break;
                        case RoundType.ELLIPSE: g.DrawEllipse(p, rt); break;
                        case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                    }
                }
                #endregion
                #region IN SHADOW
                if ((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW)
                {
                    br.Color = c.BrightnessTransmit(InShadowBright);

                    switch (round)
                    {
                        case RoundType.NONE: g.FillRectangle(br, bounds); break;
                        case RoundType.ALL: g.FillRoundRectangle(br, bounds, Corner); break;
                        case RoundType.L: g.FillRoundRectangleL(br, bounds, Corner); break;
                        case RoundType.R: g.FillRoundRectangleR(br, bounds, Corner); break;
                        case RoundType.T: g.FillRoundRectangleT(br, bounds, Corner); break;
                        case RoundType.B: g.FillRoundRectangleB(br, bounds, Corner); break;
                        case RoundType.LT: g.FillRoundRectangleLT(br, bounds, Corner); break;
                        case RoundType.RT: g.FillRoundRectangleRT(br, bounds, Corner); break;
                        case RoundType.LB: g.FillRoundRectangleLB(br, bounds, Corner); break;
                        case RoundType.RB: g.FillRoundRectangleRB(br, bounds, Corner); break;
                        case RoundType.ELLIPSE: g.FillEllipse(br, bounds); break;
                        case RoundType.FULL_HORIZON: g.FillRoundRectangle(br, bounds, Math.Max(bounds.Height, bounds.Width) * 2); break;
                    }
                }
                #endregion
                #region FILL / GRADIENT
                {
                    if ((option & BoxDrawOption.GRADIENT_V) == BoxDrawOption.GRADIENT_V)
                    {
                        var nInSH = 2;
                        var rt = (((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW) ? new Rectangle(bounds.X + nInSH, bounds.Y + nInSH, bounds.Width - (nInSH * 2), bounds.Height - (nInSH * 2)) : bounds);
                        var c1 = c.BrightnessTransmit(GradientLightBright);
                        var c2 = c.BrightnessTransmit(GradientDarkBright);

                        if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                            using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 90))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.FillRectangle(lgbr, rt); break;
                                    case RoundType.ALL: g.FillRoundRectangle(lgbr, rt, Corner); break;
                                    case RoundType.L: g.FillRoundRectangleL(lgbr, rt, Corner); break;
                                    case RoundType.R: g.FillRoundRectangleR(lgbr, rt, Corner); break;
                                    case RoundType.T: g.FillRoundRectangleT(lgbr, rt, Corner); break;
                                    case RoundType.B: g.FillRoundRectangleB(lgbr, rt, Corner); break;
                                    case RoundType.LT: g.FillRoundRectangleLT(lgbr, rt, Corner); break;
                                    case RoundType.RT: g.FillRoundRectangleRT(lgbr, rt, Corner); break;
                                    case RoundType.LB: g.FillRoundRectangleLB(lgbr, rt, Corner); break;
                                    case RoundType.RB: g.FillRoundRectangleRB(lgbr, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.FillEllipse(lgbr, rt); break;
                                    case RoundType.FULL_HORIZON: g.FillRoundRectangle(lgbr, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                    }
                    else if ((option & BoxDrawOption.GRADIENT_V_REVERSE) == BoxDrawOption.GRADIENT_V_REVERSE)
                    {
                        var nInSH = 2;
                        var rt = (((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW) ? new Rectangle(bounds.X + nInSH, bounds.Y + nInSH, bounds.Width - (nInSH * 2), bounds.Height - (nInSH * 2)) : bounds);
                        var c1 = c.BrightnessTransmit(GradientDarkBright);
                        var c2 = c.BrightnessTransmit(GradientLightBright);

                        if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                            using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 90))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.FillRectangle(lgbr, rt); break;
                                    case RoundType.ALL: g.FillRoundRectangle(lgbr, rt, Corner); break;
                                    case RoundType.L: g.FillRoundRectangleL(lgbr, rt, Corner); break;
                                    case RoundType.R: g.FillRoundRectangleR(lgbr, rt, Corner); break;
                                    case RoundType.T: g.FillRoundRectangleT(lgbr, rt, Corner); break;
                                    case RoundType.B: g.FillRoundRectangleB(lgbr, rt, Corner); break;
                                    case RoundType.LT: g.FillRoundRectangleLT(lgbr, rt, Corner); break;
                                    case RoundType.RT: g.FillRoundRectangleRT(lgbr, rt, Corner); break;
                                    case RoundType.LB: g.FillRoundRectangleLB(lgbr, rt, Corner); break;
                                    case RoundType.RB: g.FillRoundRectangleRB(lgbr, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.FillEllipse(lgbr, rt); break;
                                    case RoundType.FULL_HORIZON: g.FillRoundRectangle(lgbr, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                    }
                    else if ((option & BoxDrawOption.GRADIENT_H) == BoxDrawOption.GRADIENT_H)
                    {
                        var nInSH = 2;
                        var rt = (((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW) ? new Rectangle(bounds.X + nInSH, bounds.Y + nInSH, bounds.Width - (nInSH * 2), bounds.Height - (nInSH * 2)) : bounds);
                        var c1 = c.BrightnessTransmit(GradientDarkBright);
                        var c2 = c.BrightnessTransmit(GradientLightBright);

                        if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                            using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 180F))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.FillRectangle(lgbr, rt); break;
                                    case RoundType.ALL: g.FillRoundRectangle(lgbr, rt, Corner); break;
                                    case RoundType.L: g.FillRoundRectangleL(lgbr, rt, Corner); break;
                                    case RoundType.R: g.FillRoundRectangleR(lgbr, rt, Corner); break;
                                    case RoundType.T: g.FillRoundRectangleT(lgbr, rt, Corner); break;
                                    case RoundType.B: g.FillRoundRectangleB(lgbr, rt, Corner); break;
                                    case RoundType.LT: g.FillRoundRectangleLT(lgbr, rt, Corner); break;
                                    case RoundType.RT: g.FillRoundRectangleRT(lgbr, rt, Corner); break;
                                    case RoundType.LB: g.FillRoundRectangleLB(lgbr, rt, Corner); break;
                                    case RoundType.RB: g.FillRoundRectangleRB(lgbr, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.FillEllipse(lgbr, rt); break;
                                    case RoundType.FULL_HORIZON: g.FillRoundRectangle(lgbr, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                    }
                    else if ((option & BoxDrawOption.GRADIENT_H_REVERSE) == BoxDrawOption.GRADIENT_H_REVERSE)
                    {
                        var nInSH = 2;
                        var rt = (((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW) ? new Rectangle(bounds.X + nInSH, bounds.Y + nInSH, bounds.Width - (nInSH * 2), bounds.Height - (nInSH * 2)) : bounds);
                        var c1 = c.BrightnessTransmit(GradientDarkBright);
                        var c2 = c.BrightnessTransmit(GradientLightBright);

                        if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                            using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 0F))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.FillRectangle(lgbr, rt); break;
                                    case RoundType.ALL: g.FillRoundRectangle(lgbr, rt, Corner); break;
                                    case RoundType.L: g.FillRoundRectangleL(lgbr, rt, Corner); break;
                                    case RoundType.R: g.FillRoundRectangleR(lgbr, rt, Corner); break;
                                    case RoundType.T: g.FillRoundRectangleT(lgbr, rt, Corner); break;
                                    case RoundType.B: g.FillRoundRectangleB(lgbr, rt, Corner); break;
                                    case RoundType.LT: g.FillRoundRectangleLT(lgbr, rt, Corner); break;
                                    case RoundType.RT: g.FillRoundRectangleRT(lgbr, rt, Corner); break;
                                    case RoundType.LB: g.FillRoundRectangleLB(lgbr, rt, Corner); break;
                                    case RoundType.RB: g.FillRoundRectangleRB(lgbr, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.FillEllipse(lgbr, rt); break;
                                    case RoundType.FULL_HORIZON: g.FillRoundRectangle(lgbr, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                    }
                    else if ((option & BoxDrawOption.GRADIENT_LT) == BoxDrawOption.GRADIENT_LT)
                    {
                        var nInSH = 2;
                        var rt = (((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW) ? new Rectangle(bounds.X + nInSH, bounds.Y + nInSH, bounds.Width - (nInSH * 2), bounds.Height - (nInSH * 2)) : bounds);
                        var c1 = c.BrightnessTransmit(GradientLightBright);
                        var c2 = c.BrightnessTransmit(GradientDarkBright);

                        if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                            using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 45))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.FillRectangle(lgbr, rt); break;
                                    case RoundType.ALL: g.FillRoundRectangle(lgbr, rt, Corner); break;
                                    case RoundType.L: g.FillRoundRectangleL(lgbr, rt, Corner); break;
                                    case RoundType.R: g.FillRoundRectangleR(lgbr, rt, Corner); break;
                                    case RoundType.T: g.FillRoundRectangleT(lgbr, rt, Corner); break;
                                    case RoundType.B: g.FillRoundRectangleB(lgbr, rt, Corner); break;
                                    case RoundType.LT: g.FillRoundRectangleLT(lgbr, rt, Corner); break;
                                    case RoundType.RT: g.FillRoundRectangleRT(lgbr, rt, Corner); break;
                                    case RoundType.LB: g.FillRoundRectangleLB(lgbr, rt, Corner); break;
                                    case RoundType.RB: g.FillRoundRectangleRB(lgbr, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.FillEllipse(lgbr, rt); break;
                                    case RoundType.FULL_HORIZON: g.FillRoundRectangle(lgbr, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                    }
                    else if ((option & BoxDrawOption.GRADIENT_RT) == BoxDrawOption.GRADIENT_RT)
                    {
                        var nInSH = 2;
                        var rt = (((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW) ? new Rectangle(bounds.X + nInSH, bounds.Y + nInSH, bounds.Width - (nInSH * 2), bounds.Height - (nInSH * 2)) : bounds);
                        var c1 = c.BrightnessTransmit(GradientLightBright);
                        var c2 = c.BrightnessTransmit(GradientDarkBright);

                        if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                            using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 135))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.FillRectangle(lgbr, rt); break;
                                    case RoundType.ALL: g.FillRoundRectangle(lgbr, rt, Corner); break;
                                    case RoundType.L: g.FillRoundRectangleL(lgbr, rt, Corner); break;
                                    case RoundType.R: g.FillRoundRectangleR(lgbr, rt, Corner); break;
                                    case RoundType.T: g.FillRoundRectangleT(lgbr, rt, Corner); break;
                                    case RoundType.B: g.FillRoundRectangleB(lgbr, rt, Corner); break;
                                    case RoundType.LT: g.FillRoundRectangleLT(lgbr, rt, Corner); break;
                                    case RoundType.RT: g.FillRoundRectangleRT(lgbr, rt, Corner); break;
                                    case RoundType.LB: g.FillRoundRectangleLB(lgbr, rt, Corner); break;
                                    case RoundType.RB: g.FillRoundRectangleRB(lgbr, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.FillEllipse(lgbr, rt); break;
                                    case RoundType.FULL_HORIZON: g.FillRoundRectangle(lgbr, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                    }
                    else if ((option & BoxDrawOption.GRADIENT_RB) == BoxDrawOption.GRADIENT_RB)
                    {
                        var nInSH = 2;
                        var rt = (((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW) ? new Rectangle(bounds.X + nInSH, bounds.Y + nInSH, bounds.Width - (nInSH * 2), bounds.Height - (nInSH * 2)) : bounds);
                        var c1 = c.BrightnessTransmit(GradientLightBright);
                        var c2 = c.BrightnessTransmit(GradientDarkBright);

                        if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                            using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 225))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.FillRectangle(lgbr, rt); break;
                                    case RoundType.ALL: g.FillRoundRectangle(lgbr, rt, Corner); break;
                                    case RoundType.L: g.FillRoundRectangleL(lgbr, rt, Corner); break;
                                    case RoundType.R: g.FillRoundRectangleR(lgbr, rt, Corner); break;
                                    case RoundType.T: g.FillRoundRectangleT(lgbr, rt, Corner); break;
                                    case RoundType.B: g.FillRoundRectangleB(lgbr, rt, Corner); break;
                                    case RoundType.LT: g.FillRoundRectangleLT(lgbr, rt, Corner); break;
                                    case RoundType.RT: g.FillRoundRectangleRT(lgbr, rt, Corner); break;
                                    case RoundType.LB: g.FillRoundRectangleLB(lgbr, rt, Corner); break;
                                    case RoundType.RB: g.FillRoundRectangleRB(lgbr, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.FillEllipse(lgbr, rt); break;
                                    case RoundType.FULL_HORIZON: g.FillRoundRectangle(lgbr, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                    }
                    else if ((option & BoxDrawOption.GRADIENT_LB) == BoxDrawOption.GRADIENT_LB)
                    {
                        var nInSH = 2;
                        var rt = (((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW) ? new Rectangle(bounds.X + nInSH, bounds.Y + nInSH, bounds.Width - (nInSH * 2), bounds.Height - (nInSH * 2)) : bounds);
                        var c1 = c.BrightnessTransmit(GradientLightBright);
                        var c2 = c.BrightnessTransmit(GradientDarkBright);

                        if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                            using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 315))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.FillRectangle(lgbr, rt); break;
                                    case RoundType.ALL: g.FillRoundRectangle(lgbr, rt, Corner); break;
                                    case RoundType.L: g.FillRoundRectangleL(lgbr, rt, Corner); break;
                                    case RoundType.R: g.FillRoundRectangleR(lgbr, rt, Corner); break;
                                    case RoundType.T: g.FillRoundRectangleT(lgbr, rt, Corner); break;
                                    case RoundType.B: g.FillRoundRectangleB(lgbr, rt, Corner); break;
                                    case RoundType.LT: g.FillRoundRectangleLT(lgbr, rt, Corner); break;
                                    case RoundType.RT: g.FillRoundRectangleRT(lgbr, rt, Corner); break;
                                    case RoundType.LB: g.FillRoundRectangleLB(lgbr, rt, Corner); break;
                                    case RoundType.RB: g.FillRoundRectangleRB(lgbr, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.FillEllipse(lgbr, rt); break;
                                    case RoundType.FULL_HORIZON: g.FillRoundRectangle(lgbr, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                    }
                    else
                    {
                        var nInSH = 2;
                        var rt = (((option & BoxDrawOption.IN_SHADOW) == BoxDrawOption.IN_SHADOW) ? new Rectangle(bounds.X + nInSH, bounds.Y + nInSH, bounds.Width - (nInSH * 2), bounds.Height - (nInSH * 2)) : bounds);

                        br.Color = c;
                        switch (round)
                        {
                            case RoundType.NONE: g.FillRectangle(br, rt); break;
                            case RoundType.ALL: g.FillRoundRectangle(br, rt, Corner); break;
                            case RoundType.L: g.FillRoundRectangleL(br, rt, Corner); break;
                            case RoundType.R: g.FillRoundRectangleR(br, rt, Corner); break;
                            case RoundType.T: g.FillRoundRectangleT(br, rt, Corner); break;
                            case RoundType.B: g.FillRoundRectangleB(br, rt, Corner); break;
                            case RoundType.LT: g.FillRoundRectangleLT(br, rt, Corner); break;
                            case RoundType.RT: g.FillRoundRectangleRT(br, rt, Corner); break;
                            case RoundType.LB: g.FillRoundRectangleLB(br, rt, Corner); break;
                            case RoundType.RB: g.FillRoundRectangleRB(br, rt, Corner); break;
                            case RoundType.ELLIPSE: g.FillEllipse(br, rt); break;
                            case RoundType.FULL_HORIZON: g.FillRoundRectangle(br, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                        }
                    }
                }
                #endregion
                #region IN BEVEL
                if ((option & BoxDrawOption.IN_BEVEL) == BoxDrawOption.IN_BEVEL)
                {
                    var rt = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2);
                    var c1 = c.BrightnessTransmit(InBevelBright);
                    var c2 = Color.Transparent;
                    if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                        using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 90))
                        {
                            using (var p2 = new Pen(lgbr, 1F))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.DrawRectangle(p2, rt); break;
                                    case RoundType.ALL: g.DrawRoundRectangle(p2, rt, Corner); break;
                                    case RoundType.L: g.DrawRoundRectangleL(p2, rt, Corner); break;
                                    case RoundType.R: g.DrawRoundRectangleR(p2, rt, Corner); break;
                                    case RoundType.T: g.DrawRoundRectangleT(p2, rt, Corner); break;
                                    case RoundType.B: g.DrawRoundRectangleB(p2, rt, Corner); break;
                                    case RoundType.LT: g.DrawRoundRectangleLT(p2, rt, Corner); break;
                                    case RoundType.RT: g.DrawRoundRectangleRT(p2, rt, Corner); break;
                                    case RoundType.LB: g.DrawRoundRectangleLB(p2, rt, Corner); break;
                                    case RoundType.RB: g.DrawRoundRectangleRB(p2, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.DrawEllipse(p2, rt); break;
                                    case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p2, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                        }
                }
                #endregion
                #region IN BEVEL LT
                if ((option & BoxDrawOption.IN_BEVEL_LT) == BoxDrawOption.IN_BEVEL_LT)
                {
                    var oldclip = g.Clip.GetBounds(g);
                    var rt = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width - 1, bounds.Height - 1);
                    var rtex = new Rectangle(rt.X, rt.Y, rt.Width - 1, rt.Height - 1);
                    g.SetClip(rtex, CombineMode.Intersect);
                    var c1 = c.BrightnessTransmit(InBevelBright);
                    var c2 = Color.Transparent;
                    if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                        using (var lgbr = new LinearGradientBrush(rtex, c1, c2, 75))
                        {
                            using (var p2 = new Pen(lgbr, 1F))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.DrawRectangle(p2, rt); break;
                                    case RoundType.ALL: g.DrawRoundRectangle(p2, rt, Corner); break;
                                    case RoundType.L: g.DrawRoundRectangleL(p2, rt, Corner); break;
                                    case RoundType.R: g.DrawRoundRectangleR(p2, rt, Corner); break;
                                    case RoundType.T: g.DrawRoundRectangleT(p2, rt, Corner); break;
                                    case RoundType.B: g.DrawRoundRectangleB(p2, rt, Corner); break;
                                    case RoundType.LT: g.DrawRoundRectangleLT(p2, rt, Corner); break;
                                    case RoundType.RT: g.DrawRoundRectangleRT(p2, rt, Corner); break;
                                    case RoundType.LB: g.DrawRoundRectangleLB(p2, rt, Corner); break;
                                    case RoundType.RB: g.DrawRoundRectangleRB(p2, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.DrawEllipse(p2, rt); break;
                                    case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p2, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                        }
                    g.SetClip(oldclip, CombineMode.Replace);
                }
                #endregion
                #region IN BEVEL2
                if ((option & BoxDrawOption.IN_BEVEL2) == BoxDrawOption.IN_BEVEL2)
                {
                    var rt = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2);
                    var c1 = c.BrightnessTransmit(InBevelBright);
                    var c2 = Color.Transparent;
                    if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                        using (var lgbr = new LinearGradientBrush(new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 2), c1, c2, 90))
                        {
                            using (var p2 = new Pen(lgbr, 2F))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.DrawRectangle(p2, rt); break;
                                    case RoundType.ALL: g.DrawRoundRectangle(p2, rt, Corner); break;
                                    case RoundType.L: g.DrawRoundRectangleL(p2, rt, Corner); break;
                                    case RoundType.R: g.DrawRoundRectangleR(p2, rt, Corner); break;
                                    case RoundType.T: g.DrawRoundRectangleT(p2, rt, Corner); break;
                                    case RoundType.B: g.DrawRoundRectangleB(p2, rt, Corner); break;
                                    case RoundType.LT: g.DrawRoundRectangleLT(p2, rt, Corner); break;
                                    case RoundType.RT: g.DrawRoundRectangleRT(p2, rt, Corner); break;
                                    case RoundType.LB: g.DrawRoundRectangleLB(p2, rt, Corner); break;
                                    case RoundType.RB: g.DrawRoundRectangleRB(p2, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.DrawEllipse(p2, rt); break;
                                    case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p2, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                        }
                }
                #endregion
                #region IN BEVEL LT2
                if ((option & BoxDrawOption.IN_BEVEL_LT2) == BoxDrawOption.IN_BEVEL_LT2)
                {
                    var oldclip = g.Clip.GetBounds(g);
                    var rt = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width - 1, bounds.Height - 1);
                    var rtex = new Rectangle(rt.X, rt.Y, rt.Width - 1, rt.Height - 1);
                    g.SetClip(rtex, CombineMode.Intersect);
                    var c1 = c.BrightnessTransmit(InBevelBright);
                    var c2 = Color.Transparent;
                    if (bounds.Width + 2 > 0 && bounds.Height + 2 > 0)
                        using (var lgbr = new LinearGradientBrush(rtex, c1, c2, 75))
                        {
                            using (var p2 = new Pen(lgbr, 2F))
                            {
                                switch (round)
                                {
                                    case RoundType.NONE: g.DrawRectangle(p2, rt); break;
                                    case RoundType.ALL: g.DrawRoundRectangle(p2, rt, Corner); break;
                                    case RoundType.L: g.DrawRoundRectangleL(p2, rt, Corner); break;
                                    case RoundType.R: g.DrawRoundRectangleR(p2, rt, Corner); break;
                                    case RoundType.T: g.DrawRoundRectangleT(p2, rt, Corner); break;
                                    case RoundType.B: g.DrawRoundRectangleB(p2, rt, Corner); break;
                                    case RoundType.LT: g.DrawRoundRectangleLT(p2, rt, Corner); break;
                                    case RoundType.RT: g.DrawRoundRectangleRT(p2, rt, Corner); break;
                                    case RoundType.LB: g.DrawRoundRectangleLB(p2, rt, Corner); break;
                                    case RoundType.RB: g.DrawRoundRectangleRB(p2, rt, Corner); break;
                                    case RoundType.ELLIPSE: g.DrawEllipse(p2, rt); break;
                                    case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p2, rt, Math.Max(bounds.Height, bounds.Width) * 2); break;
                                }
                            }
                        }
                    g.SetClip(oldclip, CombineMode.Replace);
                }
                #endregion
                #region BORDER
                if ((option & BoxDrawOption.BORDER) == BoxDrawOption.BORDER)
                {
                    p.Color = bg.BrightnessTransmit(BorderBright);
                    p.Width = 1;

                    switch (round)
                    {
                        case RoundType.NONE: g.DrawRectangle(p, bounds); break;
                        case RoundType.ALL: g.DrawRoundRectangle(p, bounds, Corner); break;
                        case RoundType.L: g.DrawRoundRectangleL(p, bounds, Corner); break;
                        case RoundType.R: g.DrawRoundRectangleR(p, bounds, Corner); break;
                        case RoundType.T: g.DrawRoundRectangleT(p, bounds, Corner); break;
                        case RoundType.B: g.DrawRoundRectangleB(p, bounds, Corner); break;
                        case RoundType.LT: g.DrawRoundRectangleLT(p, bounds, Corner); break;
                        case RoundType.RT: g.DrawRoundRectangleRT(p, bounds, Corner); break;
                        case RoundType.LB: g.DrawRoundRectangleLB(p, bounds, Corner); break;
                        case RoundType.RB: g.DrawRoundRectangleRB(p, bounds, Corner); break;
                        case RoundType.ELLIPSE: g.DrawEllipse(p, bounds); break;
                        case RoundType.FULL_HORIZON: g.DrawRoundRectangle(p, bounds, Math.Max(bounds.Height, bounds.Width) * 2); break;
                    }
                }
                #endregion

                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion
            }
            catch { }
        }
        #endregion
        #region DrawText
        public override void DrawText(Graphics g, DvIcon icon, string Text, Font ft, Color c, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter)
        {
            using (var br = new SolidBrush(c))
            {
                g.DrawTextIcon(icon, Text, ft, br, bounds, align, TextOffsetX, TextOffsetY);
            }
        }

        public override void DrawTextShadow(Graphics g, DvIcon icon, string Text, Font ft, Color c, Color bg, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter)
        {
            var h = bg.GetBrightness() > c.GetBrightness() ? 0.15 : -0.5;
            if (icon != null) icon.Shadow = true; bounds.Offset(0, ShadowGap); DrawText(g, icon, Text, ft, Color.FromArgb(c.A, bg.BrightnessTransmit(h)), bounds, align);
            if (icon != null) icon.Shadow = false; bounds.Offset(0, -ShadowGap); DrawText(g, icon, Text, ft, c, bounds, align);
        }

        public override void DrawText(Graphics g, DvIcon icon, string Text, Font ft, Color c, Color cico, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter)
        {
            using (var br = new SolidBrush(c))
            {
                using (var brico = new SolidBrush(cico))
                {
                    g.DrawTextIcon(icon, Text, ft, br, brico, bounds, align, TextOffsetX, TextOffsetY);
                }
            }
        }

        public override void DrawTextShadow(Graphics g, DvIcon icon, string Text, Font ft, Color c, Color cico, Color bg, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter)
        {
            var h = bg.GetBrightness() > c.GetBrightness() ? 0.15 : -0.5;
            if (icon != null) icon.Shadow = true; bounds.Offset(0, ShadowGap); DrawText(g, icon, Text, ft, Color.FromArgb(c.A, bg.BrightnessTransmit(h)), Color.FromArgb(c.A, bg.BrightnessTransmit(h)), bounds, align);
            if (icon != null) icon.Shadow = false; bounds.Offset(0, -ShadowGap); DrawText(g, icon, Text, ft, c, cico, bounds, align);
        }
        #endregion
        #endregion

        #region class : MenuColorTable
        public class BlackThemeMenuColorTable : ThemeMenuColorTable
        {
            public override Color MenuStripColor { get; set; } = BlackTheme.StaticColor1;
            public override Color MenuItemPressedColor { get; set; } = BlackTheme.StaticColor1;
            public override Color ToolStripDropDownBackgroundColor { get; set; } = BlackTheme.StaticColor1;

            public override Color MenuItemSelectedColor { get; set; } = BlackTheme.StaticColor0;

            public override Color ImageMarginColor { get; set; } = Color.FromArgb(BlackTheme.StaticColor1.R + 10, BlackTheme.StaticColor1.G + 10, BlackTheme.StaticColor1.B + 10);
            //public override Color ImageMarginColor { get; set; } = BlackTheme.StaticColor2;

            public override Color MenuItemBorderColor { get; set; } = BlackTheme.StaticColor3;
            public override Color MenuBorderColor { get; set; } = BlackTheme.StaticColor3;
            public override Color SeparatorColor { get; set; } = BlackTheme.StaticColor3;
        }
        #endregion
    }
}
