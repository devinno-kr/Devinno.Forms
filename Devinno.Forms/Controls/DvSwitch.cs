using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
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
    public class DvSwitch : DvControl
    {
        #region Properties
        #region SwitchColor
        private Color cSwitchColor = DvTheme.DefaultTheme.Color3;
        public Color SwitchColor
        {
            get => cSwitchColor;
            set
            {
                if (cSwitchColor != value)
                {
                    cSwitchColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region PointColor
        private Color cPointColor = DvTheme.DefaultTheme.PointColor;
        public Color PointColor
        {
            get => cPointColor;
            set
            {
                if(cPointColor != value)
                {
                    cPointColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
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
        #region OnOff
        private bool bOnOff = false;
        public bool OnOff
        {
            get => bOnOff;
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
        public DvSwitch()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 60);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            var ng = rtContent.Height / 10;

            var rtSwitch = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height); rtSwitch.Inflate(-ng, -ng);
            SetArea("rtSwitch", rtSwitch);
            
            var rtOff = new Rectangle(rtSwitch.X, rtSwitch.Y, rtSwitch.Width / 2, rtSwitch.Height);
            SetArea("rtOff", rtOff);

            var rtOn = new Rectangle(rtSwitch.X + (rtSwitch.Width / 2), rtSwitch.Y, rtSwitch.Width / 2, rtSwitch.Height);
            SetArea("rtOn", rtOn);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BoxColor = UseThemeColor ? Theme.Color1 : this.BoxColor;
            var SwitchColor = UseThemeColor ? Theme.Color3 : this.SwitchColor;
            var PointColor = UseThemeColor ? Theme.PointColor : this.PointColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtSwitch = Areas["rtSwitch"];
            var rtOff = Areas["rtOff"];
            var rtOn = Areas["rtOn"];
            #endregion
            #region Init
            var p = new Pen(BoxColor, 1);
            var br = new SolidBrush(BoxColor);
            #endregion
            #region Draw
            var VC = 0.025F;
            Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.OUT_BEVEL | BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW);

            using (var pth = DrawingTool.GetRoundRectPath(rtSwitch, Theme.Corner))
            {
                #region Fill
                using (var lgbr = new LinearGradientBrush(new Rectangle(rtSwitch.X - 1, rtSwitch.Y - 1, rtSwitch.Width + 2, rtSwitch.Height + 2), Color.Black, Color.White, 0F))
                {
                    var cb = new ColorBlend();

                    var c1 = SwitchColor.BrightnessTransmit(0.3);
                    var c2 = SwitchColor.BrightnessTransmit(0);
                    var c3 = SwitchColor.BrightnessTransmit(-0.2);
                    var c4 = SwitchColor.BrightnessTransmit(-0.6);

                    if (OnOff)
                    {
                        cb.Colors = new Color[] { c4, c1, c3, c2 };
                        cb.Positions = new float[] { 0, 0.1F - VC, 0.5F, 1 };
                    }
                    else
                    {
                        cb.Colors = new Color[] { c2, c3, c1, c4 };
                        cb.Positions = new float[] { 0, 0.5F, 0.9F + VC, 1 };
                    }
                    lgbr.InterpolationColors = cb;

                    e.Graphics.FillRoundRectangle(lgbr, rtSwitch, Theme.Corner);
                }
                #endregion
                #region Bevel
                var vLx = MathTool.Map(0.1F - VC, 0D, 1D, rtSwitch.Left, rtSwitch.Right) - (rtSwitch.Width * 0.0005);
                var vRx = MathTool.Map(0.9F + VC, 0D, 1D, rtSwitch.Left, rtSwitch.Right) + (rtSwitch.Width * 0.0005);
                var cpx = MathTool.CenterPoint(rtSwitch).X;
                var rtL = new Rectangle(rtOff.X, rtOff.Y, rtOff.Width, rtOff.Height); rtL.Inflate(-1, -1);
                var rtR = new Rectangle(rtOn.X, rtOn.Y, rtOn.Width, rtOn.Height); rtR.Inflate(-1, -1);
                var rtLV = new Rectangle(Convert.ToInt32(vLx), rtOn.Top, Convert.ToInt32(cpx - vLx), rtOn.Height);
                var rtRV = new Rectangle(Convert.ToInt32(cpx), rtOff.Top, Convert.ToInt32(vRx - cpx), rtOff.Height);
                
                if (OnOff)
                {
                    e.Graphics.SetClip(new Rectangle(rtR.X+1, rtR.Y, rtR.Width, rtR.Height));
                    using (var lgbr = new LinearGradientBrush(rtSwitch, Color.FromArgb(Theme.BevelAlpha, Color.White), Color.Transparent, 120))
                    {
                        using (var p2 = new Pen(lgbr, 2F))
                        {
                            e.Graphics.DrawRoundRectangleR(p2, rtR, Theme.Corner);
                        }
                    }
                    e.Graphics.ResetClip();

                    e.Graphics.SetClip(new Rectangle(rtLV.X, rtLV.Y, rtLV.Width - 1, rtLV.Height));
                    var rtLV2 = new Rectangle(rtLV.X, rtLV.Y, rtLV.Width, rtLV.Height); rtLV2.Inflate(-1, -1);
                    using (var lgbr = new LinearGradientBrush(rtSwitch, Color.FromArgb(Convert.ToInt32(Theme.BevelAlpha * 1.5), Color.White), Color.Transparent, 30))
                    {
                        using (var p2 = new Pen(lgbr, 2F))
                        {
                            e.Graphics.DrawRoundRectangleL(p2, rtLV2, Theme.Corner);
                        }
                    }
                    e.Graphics.ResetClip();
                }
                else
                {
                    e.Graphics.SetClip(new Rectangle(rtL.X, rtL.Y, rtL.Width, rtL.Height));
                    using (var lgbr = new LinearGradientBrush(rtSwitch, Color.FromArgb(Theme.BevelAlpha, Color.White), Color.Transparent, 30))
                    {
                        using (var p2 = new Pen(lgbr, 2F))
                        {
                            e.Graphics.DrawRoundRectangleL(p2, rtL, Theme.Corner);
                        }
                    }
                    e.Graphics.ResetClip();

                    e.Graphics.SetClip(new Rectangle(rtRV.X + 2, rtRV.Y, rtRV.Width - 1, rtRV.Height));
                    var rtRV2 = new Rectangle(rtRV.X, rtRV.Y, rtRV.Width, rtRV.Height); rtRV2.Inflate(-1, -1);
                    using (var lgbr = new LinearGradientBrush(rtSwitch, Color.FromArgb(Convert.ToInt32(Theme.BevelAlpha * 1.5), Color.White), Color.Transparent, 120))
                    {
                        using (var p2 = new Pen(lgbr, 2F))
                        {
                            e.Graphics.DrawRoundRectangleR(p2, rtRV2, Theme.Corner);
                        }
                    }
                    e.Graphics.ResetClip();
                }

                p.Width = 2;
                p.Color = Color.FromArgb(30, Color.Black);
                e.Graphics.DrawLine(p, cpx, rtSwitch.Top, cpx, rtSwitch.Bottom);
                #endregion
                #region Border
                p.Width = 1;
                p.Color = BoxColor.BrightnessTransmit(Theme.BorderBright);
                e.Graphics.DrawRoundRectangle(p, rtSwitch, Theme.Corner);
                #endregion
                #region Text
                {
                    var n = Convert.ToInt32(this.Width * (0.1 - VC)) / 2;
                    var rtvTextOff = new Rectangle(rtOff.X, rtOff.Y, rtOff.Width, rtOff.Height);
                    var rtvTextOn = new Rectangle(rtOn.X - n, rtOn.Y, rtOn.Width, rtOn.Height);

                    if (OnOff) { rtvTextOff.Offset(n, 0); rtvTextOn.Offset(n, 0); }

                    var cT1 = ForeColor.BrightnessTransmit(-0.4);
                    var cT2 = ForeColor;
                    var cI1 = PointColor.BrightnessTransmit(-0.4);
                    var cI2 = PointColor;

                    if (OnOff)
                    {
                        Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-dot-circle") { IconSize = Font.Size, Gap = 3 }, "ON", Font, cT2, cI2, SwitchColor, rtvTextOn, DvContentAlignment.MiddleCenter);
                        Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-circle-notch") { IconSize = Font.Size, Gap = 3 }, "OFF", Font, cT1, SwitchColor, rtvTextOff, DvContentAlignment.MiddleCenter);
                    }
                    else
                    {
                        Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-dot-circle") { IconSize = Font.Size, Gap = 3 }, "ON", Font, cT1, SwitchColor, rtvTextOn, DvContentAlignment.MiddleCenter);
                        Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-circle-notch") { IconSize = Font.Size, Gap = 3 }, "OFF", Font, cT2, cI1, SwitchColor, rtvTextOff, DvContentAlignment.MiddleCenter);
                    }
                }
                #endregion
            }
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion

            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            if (Areas.Count > 1)
            {
                var rtOff = Areas["rtOff"];
                var rtOn = Areas["rtOn"];

                if (CollisionTool.Check(rtOff, e.X, e.Y)) OnOff = false;
                if (CollisionTool.Check(rtOn, e.X, e.Y)) OnOff = true;
            }
            base.OnMouseDown(e);
        }
        #endregion
        #endregion
    }
}
