using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Themes
{
    #region class : BlackTheme
    public class BlackTheme : DvTheme
    {
        public override ThemeBrightness Brightness => ThemeBrightness.Dark;

        public override string ThemeName => "Black";
        public override float DownBrightness { get; set; } = -0.25F;
        public override float BorderBrightness { get; set; } = -0.6F;
        public override float GradientLight { get; set; } = 0.2F;
        public override float GradientDark { get; set; } = -0.2F;
        public override int Corner { get; set; } = 6;
        public override byte OutShadowAlpha { get; set; } = 80;
        public override byte OutBevelAlpha { get; set; } = 30;
        public override byte InShadowAlpha { get; set; } = 80;
        public override float KeySpecialButtonBrightness => -0.2F;
        public override byte GradientLightAlpha => 30;
        public override byte GradientDarkAlpha => 30;
        public override float DataGridInputBright => -0.2F;
        public override float DataGridCheckBoxBright => -0.5F;
        public override float DataGridColumnBevelBright => 0.3F;
        public override float DataGridRowBevelBright => 0.3F;
        public override int DisableAlpha => 180;
        public override int TextOffsetX => 0;
        public override int TextOffsetY => 1;
        public override Color ForeColor => Color.White;
        public override Color BackColor => Util.FromArgb(50, 50, 50);
        public override Color ButtonColor => Util.FromArgb(90, 90, 90);
        public override Color LabelColor => Util.FromArgb(60, 60, 60);
        public override Color InputColor => Util.FromArgb(30, 30, 30);
        public override Color CheckBoxColor => Util.FromArgb(30, 30, 30);
        public override Color LampOnColor => Color.Red;
        public override Color LampOffColor => Util.FromArgb(90, 90, 90);
        public override Color StepOnColor => Color.Red;
        public override Color StepOffColor => Util.FromArgb(90, 90, 90);
        public override Color ConcaveBoxColor => Util.FromArgb(30, 30, 30);
        public override Color KnobColor => Util.FromArgb(60, 60, 60);
        public override Color KnobCursorColor => Color.White;
        public override Color NeedleColor => Color.White;
        public override Color NeedlePointColor => Color.Red;
        public override Color GridColor => Util.FromArgb(120, 120, 120);
        public override Color PanelColor => Util.FromArgb(60, 60, 60);
        public override Color BorderPanelColor => Util.FromArgb(90, 90, 90);
        public override Color GroupBoxColor => Color.Black;
        public override Color TabBackColor => Util.FromArgb(30, 30, 30);
        public override Color TabPageColor => Util.FromArgb(60, 60, 60);
        public override Color RowColor => Util.FromArgb(90, 90, 90);
        public override Color ColumnColor => Util.FromArgb(40, 45, 50);
        public override Color ListBackColor => Util.FromArgb(60, 60, 60);
        public override Color WindowTitleColor => Util.FromArgb(30, 30, 30);
        public override Color CalendarDaysColor => Util.FromArgb(60, 60, 60);
        public override Color CalendarWeeksColor => Util.FromArgb(30, 30, 30);
        public override Color CalendarMonthColor => Util.FromArgb(60, 60, 60);
        public override Color CalendarSelectColor => Color.Cyan;
        public override Color PointColor => Color.DarkRed;
        public override Color ScrollBarColor => Util.FromArgb(30, 30, 30);
        public override Color ScrollCursorOffColor => Util.FromArgb(180, 180, 180);
        public override Color ScrollCursorOnColor => Color.DarkRed;
        public override Color MenuNormalColor => Util.FromArgb(150, 150, 150);
        public override Color MenuSelectedColor => Color.White;

        public override ThemeMenuColorTable MenuColorTable { get; } = new BlackThemeMenuColorTable();

        #region DrawBox
        public override void DrawBox(Graphics g, RectangleF rect, Color boxColor, Color borderColor, RoundType round, BoxStyle style, int? Corner = null, bool Enable= true)
        {
            if (rect.Width >= 2 && rect.Height >= 2)
            {
                #region Init
                var br = new SolidBrush(Color.Black);
                var p = new Pen(Color.Black);
                #endregion

                #region Bounds
                var corner = Corner ?? this.Corner;
                var rt = Util.FromRect(rect.Left, rect.Top, rect.Width, rect.Height);
                #endregion
                #region OutShadow / OutBevel
                if ((style & BoxStyle.OutShadow) == BoxStyle.OutShadow)
                {
                    var rtv = new RectangleF(rt.X + 1, rt.Y + 1, rt.Width, rt.Height);
                    br.Color = Util.FromArgb(OutShadowAlpha, Color.Black);
                    switch (round)
                    {
                        case RoundType.Rect: g.FillRectangle(br, rtv); break;
                        case RoundType.All: g.FillRoundRectangle(br, rtv, corner); break;
                        case RoundType.L: g.FillRoundRectangleL(br, rtv, corner); break;
                        case RoundType.R: g.FillRoundRectangleR(br, rtv, corner); break;
                        case RoundType.T: g.FillRoundRectangleT(br, rtv, corner); break;
                        case RoundType.B: g.FillRoundRectangleB(br, rtv, corner); break;
                        case RoundType.LT: g.FillRoundRectangleLT(br, rtv, corner); break;
                        case RoundType.RT: g.FillRoundRectangleRT(br, rtv, corner); break;
                        case RoundType.LB: g.FillRoundRectangleLB(br, rtv, corner); break;
                        case RoundType.RB: g.FillRoundRectangleRB(br, rtv, corner); break;
                        case RoundType.Ellipse: g.FillEllipse(br, rtv); break;
                    }
                }
                else if ((style & BoxStyle.OutBevel) == BoxStyle.OutBevel)
                {
                    var rtv = new RectangleF(rt.X + 0, rt.Y + 1, rt.Width, rt.Height);
                    br.Color = Util.FromArgb(Convert.ToByte(OutBevelAlpha / (Enable ? 1F : 2F)), Color.White);
                    switch (round)
                    {
                        case RoundType.Rect: g.FillRectangle(br, rtv); break;
                        case RoundType.All: g.FillRoundRectangle(br, rtv, corner); break;
                        case RoundType.L: g.FillRoundRectangleL(br, rtv, corner); break;
                        case RoundType.R: g.FillRoundRectangleR(br, rtv, corner); break;
                        case RoundType.T: g.FillRoundRectangleT(br, rtv, corner); break;
                        case RoundType.B: g.FillRoundRectangleB(br, rtv, corner); break;
                        case RoundType.LT: g.FillRoundRectangleLT(br, rtv, corner); break;
                        case RoundType.RT: g.FillRoundRectangleRT(br, rtv, corner); break;
                        case RoundType.LB: g.FillRoundRectangleLB(br, rtv, corner); break;
                        case RoundType.RB: g.FillRoundRectangleRB(br, rtv, corner); break;
                        case RoundType.Ellipse: g.FillEllipse(br, rtv); break;
                    }
                }
                #endregion
                #region Fill / Gradient
                if ((style & BoxStyle.Fill) == BoxStyle.Fill)
                {
                    br.Color = boxColor;
                    switch (round)
                    {
                        case RoundType.Rect: g.FillRectangle(br, rt); break;
                        case RoundType.All: g.FillRoundRectangle(br, rt, corner); break;
                        case RoundType.L: g.FillRoundRectangleL(br, rt, corner); break;
                        case RoundType.R: g.FillRoundRectangleR(br, rt, corner); break;
                        case RoundType.T: g.FillRoundRectangleT(br, rt, corner); break;
                        case RoundType.B: g.FillRoundRectangleB(br, rt, corner); break;
                        case RoundType.LT: g.FillRoundRectangleLT(br, rt, corner); break;
                        case RoundType.RT: g.FillRoundRectangleRT(br, rt, corner); break;
                        case RoundType.LB: g.FillRoundRectangleLB(br, rt, corner); break;
                        case RoundType.RB: g.FillRoundRectangleRB(br, rt, corner); break;
                        case RoundType.Ellipse: g.FillEllipse(br, rt); break;
                    }
                }
                else
                {
                    #region Gradient Angle
                    int? angle = null;
                    var c1 = boxColor.BrightnessTransmit(GradientLight);
                    var c2 = boxColor.BrightnessTransmit(GradientDark);
                    if ((style & BoxStyle.GradientV) == BoxStyle.GradientV) angle = 90;
                    else if ((style & BoxStyle.GradientV_R) == BoxStyle.GradientV_R) angle = 90 + 180;
                    else if ((style & BoxStyle.GradientH) == BoxStyle.GradientH) angle = 0;
                    else if ((style & BoxStyle.GradientH_R) == BoxStyle.GradientH_R) angle = 0 + 180;
                    else if ((style & BoxStyle.GradientLT) == BoxStyle.GradientLT) angle = 45;
                    else if ((style & BoxStyle.GradientLT_R) == BoxStyle.GradientLT_R) angle = 45 + 180;
                    else if ((style & BoxStyle.GradientRT) == BoxStyle.GradientRT) angle = 135;
                    else if ((style & BoxStyle.GradientRT_R) == BoxStyle.GradientRT_R) angle = 135 + 180;
                    #endregion
                    #region Gradient Fill
                    if (angle.HasValue)
                    {
                        if (rt.Width + 2 > 0 && rt.Height + 2 > 0)
                            using (var lgbr = new LinearGradientBrush(new RectangleF(rt.X - 1, rt.Y - 1, rt.Width + 2, rt.Height + 2), c1, c2, angle.Value))
                            {
                                switch (round)
                                {
                                    case RoundType.Rect: g.FillRectangle(lgbr, rt); break;
                                    case RoundType.All: g.FillRoundRectangle(lgbr, rt, corner); break;
                                    case RoundType.L: g.FillRoundRectangleL(lgbr, rt, corner); break;
                                    case RoundType.R: g.FillRoundRectangleR(lgbr, rt, corner); break;
                                    case RoundType.T: g.FillRoundRectangleT(lgbr, rt, corner); break;
                                    case RoundType.B: g.FillRoundRectangleB(lgbr, rt, corner); break;
                                    case RoundType.LT: g.FillRoundRectangleLT(lgbr, rt, corner); break;
                                    case RoundType.RT: g.FillRoundRectangleRT(lgbr, rt, corner); break;
                                    case RoundType.LB: g.FillRoundRectangleLB(lgbr, rt, corner); break;
                                    case RoundType.RB: g.FillRoundRectangleRB(lgbr, rt, corner); break;
                                    case RoundType.Ellipse: g.FillEllipse(lgbr, rt); break;
                                }
                            }
                    }
                    #endregion
                }
                #endregion
                #region InBevel / InShadow
                if ((style & BoxStyle.InBevel) == BoxStyle.InBevel)
                {
                    p.Width = 1;
                    var c1 = GetInBevelColor(boxColor, Enable);
                    var c2 = Util.FromArgb(0, boxColor);
                    using (var lgbr = new LinearGradientBrush(new RectangleF(rt.X - 1, rt.Y - 1, rt.Width + 2, rt.Height + 2), c1, c2, 90))
                    {
                        using (var p2 = new Pen(lgbr, 1))
                        {
                            var rtv = Util.FromRect(rt);
                            rtv.Inflate(-1, -1);

                            switch (round)
                            {
                                case RoundType.Rect: g.DrawRectangle(p2, rtv); break;
                                case RoundType.All: g.DrawRoundRectangle(p2, rtv, corner); break;
                                case RoundType.L: g.DrawRoundRectangleL(p2, rtv, corner); break;
                                case RoundType.R: g.DrawRoundRectangleR(p2, rtv, corner); break;
                                case RoundType.T: g.DrawRoundRectangleT(p2, rtv, corner); break;
                                case RoundType.B: g.DrawRoundRectangleB(p2, rtv, corner); break;
                                case RoundType.LT: g.DrawRoundRectangleLT(p2, rtv, corner); break;
                                case RoundType.RT: g.DrawRoundRectangleRT(p2, rtv, corner); break;
                                case RoundType.LB: g.DrawRoundRectangleLB(p2, rtv, corner); break;
                                case RoundType.RB: g.DrawRoundRectangleRB(p2, rtv, corner); break;
                                case RoundType.Ellipse: g.DrawEllipse(p2, rtv); break;
                            }
                        }
                    }
                }
                else if ((style & BoxStyle.InShadow) == BoxStyle.InShadow)
                {
                    p.Width = 2;
                    p.Color = Util.FromArgb(InShadowAlpha, Color.Black);
                    var rtv = Util.FromRect(rt);
                    rtv.Inflate(-1F, -1F);

                    switch (round)
                    {
                        case RoundType.Rect: g.DrawRectangle(p, rtv); break;
                        case RoundType.All: g.DrawRoundRectangle(p, rtv, corner); break;
                        case RoundType.L: g.DrawRoundRectangleL(p, rtv, corner); break;
                        case RoundType.R: g.DrawRoundRectangleR(p, rtv, corner); break;
                        case RoundType.T: g.DrawRoundRectangleT(p, rtv, corner); break;
                        case RoundType.B: g.DrawRoundRectangleB(p, rtv, corner); break;
                        case RoundType.LT: g.DrawRoundRectangleLT(p, rtv, corner); break;
                        case RoundType.RT: g.DrawRoundRectangleRT(p, rtv, corner); break;
                        case RoundType.LB: g.DrawRoundRectangleLB(p, rtv, corner); break;
                        case RoundType.RB: g.DrawRoundRectangleRB(p, rtv, corner); break;
                        case RoundType.Ellipse: g.DrawEllipse(p, rtv); break;
                    }
                }
                #endregion
                #region Border
                if ((style & BoxStyle.Border) == BoxStyle.Border)
                {
                    p.Color = borderColor;
                    p.Width = 1;

                    switch (round)
                    {
                        case RoundType.Rect: g.DrawRectangle(p, rt); break;
                        case RoundType.All: g.DrawRoundRectangle(p, rt, corner); break;
                        case RoundType.L: g.DrawRoundRectangleL(p, rt, corner); break;
                        case RoundType.R: g.DrawRoundRectangleR(p, rt, corner); break;
                        case RoundType.T: g.DrawRoundRectangleT(p, rt, corner); break;
                        case RoundType.B: g.DrawRoundRectangleB(p, rt, corner); break;
                        case RoundType.LT: g.DrawRoundRectangleLT(p, rt, corner); break;
                        case RoundType.RT: g.DrawRoundRectangleRT(p, rt, corner); break;
                        case RoundType.LB: g.DrawRoundRectangleLB(p, rt, corner); break;
                        case RoundType.RB: g.DrawRoundRectangleRB(p, rt, corner); break;
                        case RoundType.Ellipse: g.DrawEllipse(p, rt); break;
                    }
                }
                #endregion

                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion
            }
        }
        #endregion
        #region DrawIcon
        public override void DrawIcon(Graphics g, DvIcon icon, Color color, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, bool shadow = true)
        {
            using (var br = new SolidBrush(color))
            {
                bounds.Offset(0, 1);

                var c = Util.FromArgb(80, Color.Black);
                if (Brightness == ThemeBrightness.Light) c = Util.FromArgb(120, Color.White);
                else if (Brightness == ThemeBrightness.Dark) c = Util.FromArgb(90, Color.Black);

                if (shadow)
                {
                    icon.Shadow = true;
                    var rt = new RectangleF(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height);
                    br.Color = c; g.DrawIcon(icon, br, rt, align);
                    icon.Shadow = false;
                }

                br.Color = color; g.DrawIcon(icon, br, bounds, align);
            }
        }
        #endregion
        #region DrawText
        public override void DrawText(Graphics g, string text, Font font, Color color, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, bool shadow = true)
        {
            using (var br = new SolidBrush(color))
            {
                var c = Util.FromArgb(80, Color.Black);
                if (Brightness == ThemeBrightness.Light) c = Util.FromArgb(120, Color.White);
                else if (Brightness == ThemeBrightness.Dark) c = Util.FromArgb(90, Color.Black);

                if (shadow)
                {
                    var rt = new RectangleF(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height);
                    br.Color = c; g.DrawText(text, font, br, rt, align, TextOffsetX, TextOffsetY);
                }

                br.Color = color; g.DrawText(text, font, br, bounds, align, TextOffsetX, TextOffsetY);
            }
        }
        #endregion
        #region DrawTextIcon
        public override void DrawTextIcon(Graphics g, DvIcon icon, Color colorIco, string text, Font font, Color colorText, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, bool shadow = true)
        {
            var brText = new SolidBrush(colorText);
            var brIco = new SolidBrush(colorIco);
            {
                var c = Util.FromArgb(80, Color.Black);
                if (Brightness == ThemeBrightness.Light) c = Util.FromArgb(120, Color.White);
                else if (Brightness == ThemeBrightness.Dark) c = Util.FromArgb(90, Color.Black);

                if (shadow)
                {
                    icon.Shadow = true;
                    var rt = new RectangleF(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height);
                    brText.Color = brIco.Color = c;
                    g.DrawTextIcon(icon, brIco, text, font, brText, rt, align, TextOffsetX, TextOffsetY);
                    icon.Shadow = false;
                }

                brText.Color = colorText;
                brIco.Color = colorIco;
                g.DrawTextIcon(icon, brIco, text, font, brText, bounds, align, TextOffsetX, TextOffsetY);
            }
            brText.Dispose();
            brIco.Dispose();
        }

        public override void DrawTextIcon(Graphics g, TextIcon texticon, Color colorIco, Color colorText, Font font, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, bool shadow = true)
        {
            var brText = new SolidBrush(colorText);
            var brIco = new SolidBrush(colorIco);
            {
                var c = Util.FromArgb(80, Color.Black);
                if (Brightness == ThemeBrightness.Light) c = Util.FromArgb(120, Color.White);
                else if (Brightness == ThemeBrightness.Dark) c = Util.FromArgb(90, Color.Black);

                if (shadow)
                {
                    texticon.Icon.Shadow = true;
                    var rt = new RectangleF(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height);
                    brText.Color = brIco.Color = c;
                    g.DrawTextIcon(texticon, brIco, brText, font, rt, align, TextOffsetX, TextOffsetY);
                    texticon.Icon.Shadow = false;
                }

                brText.Color = colorText;
                brIco.Color = colorIco;
                g.DrawTextIcon(texticon, brIco, brText, font, bounds, align, TextOffsetX, TextOffsetY);
            }
            brText.Dispose();
            brIco.Dispose();
        }
        #endregion
        #region DrawLamp
        public override void DrawLamp(Graphics g, RectangleF bounds, Color BackColor, Color OnLampColor, Color OffLampColor, bool OnOff, bool Animation, Animation ani)
        {
            Color cBS, cBE, cS, cE, cM, BorderColor;
            GetLampColors(BackColor, OnLampColor, OffLampColor, OnOff,
                        Animation, ani,
                        out cBE, out cBS, out cS, out cE, out cM);

            BorderColor = GetBorderColor(BackColor, cM);

            var c1 = Color.FromArgb(60, Color.Black);
            var c2 = Color.FromArgb(60, Color.White);
            var rtLamp = bounds;

            #region Back
            var old = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighQuality;
            var rtBK = rtLamp;
            rtBK.Inflate(1, 1);
            using (var lgbr = new LinearGradientBrush(rtBK, cBE, cBS, 90))
            {
                g.FillEllipse(lgbr, rtLamp);
            }
            #endregion

            #region Lamp
            var ng = rtLamp.Height / 8;
            rtLamp.Inflate(-ng, -ng);
            using (var pth = new GraphicsPath())
            {
                pth.AddEllipse(rtLamp);
                using (var pbr = new PathGradientBrush(pth))
                {
                    pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Left, rtLamp.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtLamp.Top, rtLamp.Bottom)));
                    pbr.CenterColor = cS;
                    pbr.SurroundColors = new Color[] { cE };

                    g.FillEllipse(pbr, rtLamp);
                }
             
                using (var p = new Pen(BorderColor)) g.DrawEllipse(p, rtLamp);
            }
            #endregion
        }
        #endregion

        #region GetBorderColor
        public override Color GetBorderColor(Color fillColor, Color backColor)
        {
            var f = fillColor.ToHSV();
            var b = backColor.ToHSV();
            var vc = new HsvColor() { A = f.A, H = f.H, S = f.S, V = Math.Min(f.V, b.V) };
            var c = vc.ToRGB();
            return c.BrightnessTransmit(BorderBrightness);
        }
        #endregion
        #region GetInBevelColor
        public override Color GetInBevelColor(Color BaseColor, bool Enable = true)
        {
            var b = BaseColor.ToHSV();
            var v = Convert.ToByte(MathTool.Constrain(b.V * 255 * 0.6 * (Enable ? 1.0 : 0.7), 0, 255));

            return Util.FromArgb(v, Color.White);
        }
        #endregion
        #region GetSwitchColors
        public override void GetSwitchColors(Color Color, out Color c1, out Color c2, out Color c3, out Color c4)
        {
            c1 = Color.BrightnessTransmit(0.3F);
            c2 = Color.BrightnessTransmit(0F);
            c3 = Color.BrightnessTransmit(-0.2F);
            c4 = Color.BrightnessTransmit(-0.4F);
        }
        #endregion
        #region GetLampColors
        public override void GetLampColors(Color BackColor, Color OnLampColor, Color OffLampColor, bool OnOff,
                                            bool Animation, Animation ani,
                                            out Color BackLightColor, out Color BackDarkColor,
                                            out Color LampLightColor, out Color LampDarkColor, out Color LampColor)
        {
            #region Brightness
            var vBS = 0.2F;
            var vBE = -0.6F;
            var vS = 0.2F;
            var vE = -0.2F;

            if (OnOff)
            {
                vS = 0.5F;
                vE = -0.2F;
            }
            #endregion

            var cM = OnOff ? OnLampColor : OffLampColor;
            if (Animation && ani != null && ani.IsPlaying) cM = ani.Value(AnimationAccel.Linear, ani.Variable == "On" ? OffLampColor : OnLampColor, ani.Variable == "On" ? OnLampColor : OffLampColor);

            BackDarkColor = BackColor.BrightnessTransmit(vBS);
            BackLightColor = BackColor.BrightnessTransmit(vBE);
            LampLightColor = cM.BrightnessTransmit(vS);
            LampDarkColor = cM.BrightnessTransmit(vE);
            LampColor = cM;
        }
        #endregion
    }
    #endregion
    #region class : BlackThemeMenuColorTable
    public class BlackThemeMenuColorTable : ThemeMenuColorTable
    {
        public override Color TextColor { get; set; } = Util.FromArgb(180, 180, 180);
        public override Color MenuStripColor { get; set; } = Util.FromArgb(30, 30, 30);
        public override Color MenuItemPressedColor { get; set; } = Util.FromArgb(30, 30, 30);
        public override Color ToolStripDropDownBackgroundColor { get; set; } = Util.FromArgb(30, 30, 30);

        public override Color MenuItemSelectedColor { get; set; } = Util.FromArgb(20, 20, 20);

        public override Color ImageMarginColor { get; set; } = Util.FromArgb(40, 40, 40);

        public override Color MenuItemBorderColor { get; set; } = Util.FromArgb(90, 90, 90);
        public override Color MenuBorderColor { get; set; } = Util.FromArgb(90, 90, 90);
        public override Color SeparatorColor { get; set; } = Util.FromArgb(90, 90, 90);

    }
    #endregion
}
