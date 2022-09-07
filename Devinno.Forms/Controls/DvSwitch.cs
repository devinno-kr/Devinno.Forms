using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
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
    public class DvSwitch : DvControl
    {
        #region Properties
        #region SwitchColor
        private Color? cSwitchColor = null;
        public Color? SwitchColor
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
                    OnOffChanged?.Invoke(this, null);

                    if (Animation)
                    {
                        ani.Stop();
                        ani.Start(200, OnOff ? "On" : "Off", () => this.Invoke(new Action(() => Invalidate())));
                    }

                    Invalidate();
                }
            }
        }
        #endregion
        #region OnText
        private string sOnText = "ON";
        public string OnText
        {
            get => sOnText;
            set
            {
                if (sOnText != value)
                {
                    sOnText = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OffText
        private string sOffText = "OFF";
        public string OffText
        {
            get => sOffText;
            set
            {
                if (sOffText != value)
                {
                    sOffText = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Animation
        public bool Animation { get; set; } = true;
        #endregion
        #endregion

        #region Member Variable
        private Animation ani = new Animation();
        #endregion

        #region Event
        public event EventHandler OnOffChanged;
        #endregion

        #region Constructor
        public DvSwitch()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(90, 30);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            Areas((rtContent, rtSwitch, rtOn, rtOff, rtLamp, rtOnText, rtOffText, VC) =>
            {
                #region Var
                var SwitchColor = this.SwitchColor ?? Theme.ButtonColor;
                var OnLampColor = this.OnLampColor ?? Theme.LampOnColor;
                var OffLampColor = this.OffLampColor ?? Theme.LampOffColor;
                var BoxColor = this.BoxColor ?? Theme.ConcaveBoxColor;
                var BorderColor = Theme.GetBorderColor(BoxColor, BackColor);
                var Corner = Theme.Corner;

                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                #endregion
                #region Init
                var p = new Pen(Color.White);
                var br = new SolidBrush(Color.White);
                #endregion

                {
                    #region Color
                    Color c1, c2, c3, c4;
                    Theme.GetSwitchColors(SwitchColor, out c1, out c2, out c3, out c4);

                    var ca1 = !OnOff ? c2 : c4;
                    var ca2 = !OnOff ? c3 : c1;
                    var ca3 = !OnOff ? c1 : c3;
                    var ca4 = !OnOff ? c4 : c2;

                    var va1 = !OnOff ? 0 : 0;
                    var va2 = !OnOff ? 0.5F : 0.1F - VC;
                    var va3 = !OnOff ? 0.9F + VC : 0.5F;
                    var va4 = !OnOff ? 1 : 1;

                    if (Animation && ani.IsPlaying)
                    {
                        if (ani.Variable == "On")
                        {
                            ca1 = ani.Value(AnimationAccel.DCL, c2, c4);
                            ca2 = ani.Value(AnimationAccel.DCL, c3, c1);
                            ca3 = ani.Value(AnimationAccel.DCL, c1, c3);
                            ca4 = ani.Value(AnimationAccel.DCL, c4, c2);

                            va2 = ani.Value(AnimationAccel.DCL, 0.5F, 0.1F - VC);
                            va3 = ani.Value(AnimationAccel.DCL, 0.9F + VC, 0.5F);
                        }
                        else if (ani.Variable == "Off")
                        {
                            ca1 = ani.Value(AnimationAccel.DCL, c4, c2);
                            ca2 = ani.Value(AnimationAccel.DCL, c1, c3);
                            ca3 = ani.Value(AnimationAccel.DCL, c3, c1);
                            ca4 = ani.Value(AnimationAccel.DCL, c2, c4);

                            va2 = ani.Value(AnimationAccel.DCL, 0.1F - VC, 0.5F);
                            va3 = ani.Value(AnimationAccel.DCL, 0.5F, 0.9F + VC);
                        }
                    }
                    #endregion
                    #region Back
                    Theme.DrawBox(e.Graphics, rtContent, BoxColor, BorderColor, RoundType.All, Box.BackBox(ShadowGap));
                    #endregion
                    #region Fill
                    using (var lgbr = new LinearGradientBrush(rtSwitch, Color.White, Color.Black, 0F))
                    {
                        var cb = new ColorBlend();
                        cb.Colors = new Color[] { ca1, ca2, ca3, ca4 };
                        cb.Positions = new float[] { va1, va2, va3, va4 };
                        lgbr.InterpolationColors = cb;

                        e.Graphics.FillRoundRectangle(lgbr, rtSwitch, Corner);
                    }
                    #endregion
                    #region Bevel
                    {
                        var VLC = 0.1F - VC;
                        var VRC = 0.9F + VC;

                        var vLx = MathTool.Map(VLC, 0D, 1D, rtSwitch.Left, rtSwitch.Right) - (rtSwitch.Width * 0.0005);
                        var vRx = MathTool.Map(VRC, 0D, 1D, rtSwitch.Left, rtSwitch.Right) + (rtSwitch.Width * 0.0005);
                        var cpx = MathTool.CenterPoint(rtSwitch).X;
                        var rtL = Util.FromRect(rtOff.Left, rtOff.Top, rtOff.Width, rtOff.Height); rtL.Inflate(-1, -1);
                        var rtR = Util.FromRect(rtOn.Left, rtOn.Top, rtOn.Width, rtOn.Height); rtR.Inflate(-1, -1);
                        var rtLV = Util.FromRect(Convert.ToInt32(vLx), rtOn.Top, Convert.ToInt32(cpx - vLx), rtOn.Height);
                        var rtRV = Util.FromRect(Convert.ToInt32(cpx), rtOff.Top, Convert.ToInt32(vRx - cpx), rtOff.Height);

                        p.Width = 2;
                        var bc = Theme.GetInBevelColor(SwitchColor);
                        if (OnOff)
                        {
                            #region Right
                            {
                                var rtm = (Animation && ani.IsPlaying ? ani.Value(AnimationAccel.DCL, rtRV, rtR) : rtR);
                                var va = bc.A / 2;
                                var n = (Animation && ani.IsPlaying ? Convert.ToInt32(Math.Abs(ani.Value(AnimationAccel.DCL, -(bc.A - va), (bc.A - va)) + va)) : bc.A);
                                using (var lg = new LinearGradientBrush(new PointF(rtm.Right, rtm.Top), new PointF(rtm.Left, rtm.Bottom), Color.FromArgb(n, bc), Color.FromArgb(0, bc)))
                                {
                                    using (var p2 = new Pen(lg, 2)) e.Graphics.DrawRoundRectangleR(p2, rtm, Corner);
                                }
                            }
                            #endregion
                            #region Left
                            {
                                var rtm = (Animation && ani.IsPlaying ? ani.Value(AnimationAccel.DCL, rtL, rtLV) : rtLV);
                                var va = bc.A / 2;
                                var n = (Animation && ani.IsPlaying ? Convert.ToInt32(Math.Abs(ani.Value(AnimationAccel.DCL, -(bc.A - va), (bc.A - va)) + va)) : bc.A);
                                using (var lg = new LinearGradientBrush(new PointF(rtm.Left, rtm.Top), new PointF(rtm.Right, rtm.Bottom), Color.FromArgb(n, bc), Color.FromArgb(0, bc)))
                                {
                                    rtm.Inflate(-1, -1);
                                    using (var p2 = new Pen(lg, 2)) e.Graphics.DrawRoundRectangleL(p2, rtm, Corner);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region Left
                            {
                                var rtm = (Animation && ani.IsPlaying ? ani.Value(AnimationAccel.DCL, rtLV, rtL) : rtL);
                                var va = bc.A / 2;
                                var n = (Animation && ani.IsPlaying ? Convert.ToInt32(Math.Abs(ani.Value(AnimationAccel.DCL, -(bc.A - va), (bc.A - va)) + va)) : bc.A);
                                using (var lg = new LinearGradientBrush(new PointF(rtm.Left, rtm.Top), new PointF(rtm.Right, rtm.Bottom), Color.FromArgb(n, bc), Color.FromArgb(0, bc)))
                                {
                                    using (var p2 = new Pen(lg, 2)) e.Graphics.DrawRoundRectangleL(p2, rtm, Corner);
                                }
                            }
                            #endregion
                            #region Right
                            {
                                var rtm = (Animation && ani.IsPlaying ? ani.Value(AnimationAccel.DCL, rtR, rtRV) : rtRV);
                                var va = bc.A / 2;
                                var n = (Animation && ani.IsPlaying ? Convert.ToInt32(Math.Abs(ani.Value(AnimationAccel.DCL, -(bc.A - va), (bc.A - va)) + va)) : bc.A);
                                using (var lg = new LinearGradientBrush(new PointF(rtm.Right, rtm.Top), new PointF(rtm.Left, rtm.Bottom), Color.FromArgb(n, bc), Color.FromArgb(0, bc)))
                                {
                                    rtm.Inflate(-1, -1);
                                    using (var p2 = new Pen(lg, 2)) e.Graphics.DrawRoundRectangleR(p2, rtm, Corner);
                                }
                            }
                            #endregion
                        }

                        var cp = MathTool.CenterPoint(rtSwitch);
                        p.Width = 2;
                        p.Color = Util.FromArgb(30, Color.Black);
                        e.Graphics.DrawLine(p, cp.X, rtSwitch.Top, cp.X, rtSwitch.Bottom);
                    }
                    #endregion
                    #region Lamp
                    Theme.DrawLamp(e.Graphics, rtLamp, SwitchColor, OnLampColor, OffLampColor, OnOff, true, ani);
                    #endregion
                    #region Text
                    Theme.DrawText(e.Graphics, OnText, Font, OnOff ? ForeColor : ForeColor.BrightnessTransmit(-0.4F), rtOnText);
                    Theme.DrawText(e.Graphics, OffText, Font, !OnOff ? ForeColor : ForeColor.BrightnessTransmit(-0.4F), rtOffText);
                    #endregion

                    #region Border
                    p.Width = 1;
                    p.Color = BorderColor;
                    e.Graphics.DrawRoundRectangle(p, rtSwitch, Corner);
                    #endregion
                }

                #region Dispose
                p.Dispose();
                br.Dispose();
                #endregion
            });
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtSwitch, rtOn, rtOff, rtLamp, rtOnText, rtOffText, VC) =>
            {
                if (CollisionTool.Check(rtOff, e.X, e.Y)) OnOff = false;
                if (CollisionTool.Check(rtOn, e.X, e.Y)) OnOff = true;
            });
            base.OnMouseDown(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, float> act)
        {
            var rtContent = GetContentBounds();
            var ng = Convert.ToInt32(rtContent.Height * 0.1F);

            var rtSwitch = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height); rtSwitch.Inflate(-ng, -ng);
            var rtOff = Util.FromRect(rtSwitch.Left, rtSwitch.Top, rtSwitch.Width / 2, rtSwitch.Height);
            var rtOn = Util.FromRect(rtSwitch.Left + (rtSwitch.Width / 2), rtSwitch.Top, rtSwitch.Width / 2, rtSwitch.Height);

            var whLamp = Convert.ToInt32(rtSwitch.Height * 0.5F);
            var VC = 0.025F;
            var n = Convert.ToInt32(this.Width * (0.1 - VC)) / 2;
            var GAP = Animation && ani.IsPlaying ? ani.Value(AnimationAccel.DCL, (OnOff ? 3F : 5F), (OnOff ? 5F : 3F)) : (OnOff ? 5F : 3F);

            using (var g = CreateGraphics())
            {
                Util.TextIconBounds(g, rtOn, DvContentAlignment.MiddleCenter, 
                    OnText, Font, GAP, new SizeF(whLamp, whLamp), DvTextIconAlignment.LeftRight, 
                    (_rtLamp, _rtOnText) =>
                    {
                        var rtLamp = _rtLamp;
                        var rtOnText = _rtOnText;
                        var rtOffText = Util.FromRect(rtOff.Left, rtOnText.Top, rtOff.Width, rtOnText.Height);

                        if (Animation && ani.IsPlaying)
                        {
                            if (ani.Variable == "On")
                            {
                                rtLamp.Offset(ani.Value(AnimationAccel.DCL, -n, 0), 0);
                                rtOnText.Offset(ani.Value(AnimationAccel.DCL, -n, 0), 0);
                                rtOffText.Offset(ani.Value(AnimationAccel.DCL, 0, n), 0);
                            }
                            else if (ani.Variable == "Off")
                            {
                                rtLamp.Offset(ani.Value(AnimationAccel.DCL, 0, -n), 0);
                                rtOnText.Offset(ani.Value(AnimationAccel.DCL, 0, -n), 0);
                                rtOffText.Offset(ani.Value(AnimationAccel.DCL, n, 0), 0);
                            }
                        }
                        else
                        {
                            if (!OnOff)
                            {
                                rtLamp.Offset(-n, 0);
                                rtOnText.Offset(-n, 0);
                                rtOffText.Offset(0, 0);
                            }
                            else
                            {
                                rtLamp.Offset(0, 0);
                                rtOnText.Offset(0, 0);
                                rtOffText.Offset(n, 0);
                            }
                        }
                        act(rtContent, rtSwitch, rtOn, rtOff, rtLamp, rtOnText, rtOffText, VC);
                    });
            }
        }
        #endregion
        #endregion
    }
}
