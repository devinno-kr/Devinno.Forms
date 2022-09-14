using Devinno.Extensions;
using Devinno.Forms.Dialogs;
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
    public class DvOnOff : DvControl
    {
        #region Properties
        #region CursorColor
        private Color? cCursorColor = null;
        public Color? CursorColor
        {
            get => cCursorColor;
            set
            {
                if (cCursorColor != value)
                {
                    cCursorColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OnBoxColor
        private Color? cOnBoxColor = null;
        public Color? OnBoxColor
        {
            get => cOnBoxColor;
            set
            {
                if (cOnBoxColor != value)
                {
                    cOnBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OffBoxColor
        private Color? cOffBoxColor = null;
        public Color? OffBoxColor
        {
            get => cOffBoxColor;
            set
            {
                if (cOffBoxColor != value)
                {
                    cOffBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OnTextColor
        private Color? cOnTextColor = null;
        public Color? OnTextColor
        {
            get => cOnTextColor;
            set
            {
                if (cOnTextColor != value)
                {
                    cOnTextColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OffTextColor
        private Color? cOffTextColor = null;
        public Color? OffTextColor
        {
            get => cOffTextColor;
            set
            {
                if (cOffTextColor != value)
                {
                    cOffTextColor = value;
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

                    if (Animation && !bDown)
                    {
                        ani.Stop();
                        ani.Start(200, bOnOff ? "ON" : "OFF", () => this.Invoke(new Action(() => Invalidate())));
                    }
                    Invalidate();
                }
            }
        }
        #endregion

        #region Animation
        private bool Animation => GetTheme()?.Animation ?? false;
        #endregion
        #endregion

        #region Member Variable
        private Animation ani = new Animation();
        private bool bDown = false;
        private Point? ptDown = null;
        private float? curX = null;
        #endregion

        #region Event 
        public event EventHandler OnOffChanged;
        #endregion

        #region Constructor
        public DvOnOff()
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
            #region Var
            var CursorColor = this.CursorColor ?? Theme.ButtonColor;
            var OnBoxColor = this.OnBoxColor ?? Theme.PointColor;
            var OffBoxColor = this.OffBoxColor ?? Theme.ConcaveBoxColor;
            var OnTextColor = this.OnTextColor ?? Theme.ForeColor;
            var OffTextColor = this.OffBoxColor ?? Util.FromArgb(128, OnTextColor);
            var CursorBorderColor = (OnOff ? OnBoxColor : OffBoxColor).BrightnessTransmit(-0.5F);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent, rtArea, rtCursor, rtOnText, rtOffText, rtA1, rtA2, rtA3, onX, offX, aOn, aOff) =>
            {
                var cBox = OnOff ? OnBoxColor : OffBoxColor;
                var cBorder = Theme.GetBorderColor(cBox, BackColor);

                if (bDown) CursorColor = CursorColor.BrightnessTransmit(0.2F);

                #region Box
                Theme.DrawBox(e.Graphics, rtContent, cBox, cBorder, RoundType.All, Box.BackBox(ShadowGap), Convert.ToInt32(rtContent.Height));
                #endregion
                #region Text
                using (var pth = DrawingTool.GetRoundRectPath(rtArea, rtArea.Height))
                {
                    e.Graphics.SetClip(pth);
                    Theme.DrawText(e.Graphics, OnText, Font, Color.FromArgb(aOn, OnTextColor), rtOnText);
                    Theme.DrawText(e.Graphics, OffText, Font, Color.FromArgb(aOff, OffTextColor), rtOffText);
                    e.Graphics.ResetClip();
                }
                #endregion
                #region Cursor
                Theme.DrawBox(e.Graphics, rtCursor, CursorColor, CursorBorderColor, RoundType.All, Box.ButtonUp_V(true, ShadowGap), Convert.ToInt32(rtCursor.Height));

                #region Bevel
                {
                    var rt = rtCursor;
                    rt.Inflate(-1.5F, -1.5F);
                    using (var path = Util.GetBoxPath(rt, RoundType.All, Convert.ToInt32(rtCursor.Height)))
                    {
                        using (var lg = new LinearGradientBrush(rtCursor, Color.FromArgb(Theme.GradientLightAlpha, Color.White), Color.FromArgb(Theme.GradientDarkAlpha, Color.Black), 90))
                        {
                            using (var p2 = new Pen(lg))
                            {
                                p2.Width = 3;
                                e.Graphics.DrawPath(p2, path);
                            }
                        }
                    }
                }
                #endregion
                #endregion
                #region Ach
                {
                    var cc = CursorColor.BrightnessTransmit(-0.1F);
                    var cD = cc.BrightnessTransmit(-0.6F);
                    var cL = cc.BrightnessTransmit(0.2F);

                    var rt1 = Util.INT(rtA1);
                    var rt2 = Util.INT(rtA2);
                    var rt3 = Util.INT(rtA3);

                    var old = e.Graphics.SmoothingMode;
                    e.Graphics.SmoothingMode = SmoothingMode.None;
                    using (var br = new SolidBrush(cc))
                    {
                        using (var p = new Pen(cL))
                        {
                            e.Graphics.FillRectangle(br, rt1);
                            e.Graphics.FillRectangle(br, rt2);
                            e.Graphics.FillRectangle(br, rt3);

                            p.Color = cD;
                            e.Graphics.DrawLine(p, rt1.Left, rt1.Top, rt1.Right, rt1.Top);
                            e.Graphics.DrawLine(p, rt1.Left, rt1.Top, rt1.Left, rt1.Bottom);
                            e.Graphics.DrawLine(p, rt2.Left, rt2.Top, rt2.Right, rt2.Top);
                            e.Graphics.DrawLine(p, rt2.Left, rt2.Top, rt2.Left, rt2.Bottom);
                            e.Graphics.DrawLine(p, rt3.Left, rt3.Top, rt3.Right, rt3.Top);
                            e.Graphics.DrawLine(p, rt3.Left, rt3.Top, rt3.Left, rt3.Bottom);
                            
                            p.Color = cL;
                            e.Graphics.DrawLine(p, rt1.Left, rt1.Bottom, rt1.Right, rt1.Bottom);
                            e.Graphics.DrawLine(p, rt1.Right, rt1.Top, rt1.Right, rt1.Bottom);
                            e.Graphics.DrawLine(p, rt2.Left, rt2.Bottom, rt2.Right, rt2.Bottom);
                            e.Graphics.DrawLine(p, rt2.Right, rt2.Top, rt2.Right, rt2.Bottom);
                            e.Graphics.DrawLine(p, rt3.Left, rt3.Bottom, rt3.Right, rt3.Bottom);
                            e.Graphics.DrawLine(p, rt3.Right, rt3.Top, rt3.Right, rt3.Bottom);
                        }
                    }
                    e.Graphics.SmoothingMode = old;
                }
                #endregion
            });
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtArea, rtCursor, rtOnText, rtOffText, rtA1, rtA2, rtA3, onX, offX, aOn, aOff) =>
            {
                if (CollisionTool.Check(rtCursor, e.X, e.Y))
                {
                    bDown = true;
                    ptDown = e.Location;
                    curX = rtCursor.Left;
                }
                else if (CollisionTool.Check(rtOnText, e.X, e.Y) || CollisionTool.Check(rtOffText, e.X, e.Y)) OnOff = !OnOff;
                Invalidate();
            });
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtArea, rtCursor, rtOnText, rtOffText, rtA1, rtA2, rtA3, onX, offX, aOn, aOff) =>
            {
                if (bDown)
                {
                    bDown = false;
                    ptDown = null;

                    var cp = MathTool.CenterPoint(rtContent);
                    OnOff = e.X > cp.X;
                    Invalidate();
                }
            });
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Areas((rtContent, rtArea, rtCursor, rtOnText, rtOffText, rtA1, rtA2, rtA3, onX, offX, aOn, aOff) =>
            {
                if (bDown)
                {
                    var cp = MathTool.CenterPoint(rtContent);
                    OnOff = e.X > cp.X;
                    Invalidate();
                }
            });
            base.OnMouseMove(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, float, float, byte, byte> act)
        {
            var rtContent = GetContentBounds();
            var w = rtContent.Width * 0.55F;
            var ng = rtContent.Height * 0.1F;

            var rtArea = Util.FromRect(rtContent); rtArea.Inflate(-ng, -ng);
            var onX = (rtArea.Right - w);
            var offX = (rtArea.Left);
            var ngp = Convert.ToSingle(Math.Abs(offX - onX));

            var rtCursor = Util.FromRect(rtArea.Left, rtArea.Top, w, rtArea.Height);
            var rtOnText = new RectangleF(rtArea.Left, rtArea.Top, onX - rtArea.Left, rtArea.Height);
            var rtOffText = new RectangleF(offX + w, rtArea.Top, rtArea.Right - (offX + w), rtArea.Height);

            byte aOn = 255, aOff = 128;

            if (bDown)
            {
                if (ptDown.HasValue && curX.HasValue)
                {
                    var pt = PointToClient(MousePosition);
                    rtCursor.X = Convert.ToSingle(MathTool.Constrain(curX.Value + (pt.X - ptDown.Value.X), offX, onX));
                }
            }
            else
            {
                if (Animation && ani.IsPlaying)
                {
                    var v = ani.Value(AnimationAccel.DCL, OnOff ? offX : onX, OnOff ? onX : offX);
                    rtCursor.X = Convert.ToSingle(MathTool.Constrain(v, offX, onX));


                    var vOn = ani.Value(AnimationAccel.DCL, -ngp, 0);
                    var vOff = ani.Value(AnimationAccel.DCL, 0, -ngp);

                    var vOn2 = ani.Value(AnimationAccel.DCL, 0, ngp);
                    var vOff2 = ani.Value(AnimationAccel.DCL, ngp, 0);

                    if (OnOff)
                    {
                        rtOnText.Offset(vOn, 0);
                        rtOffText.Offset(vOn2, 0);

                        aOn = Convert.ToByte(ani.Value(AnimationAccel.DCL, 0, 255));
                        aOff = Convert.ToByte(ani.Value(AnimationAccel.DCL, 128, 0));
                    }
                    else
                    {
                        rtOnText.Offset(vOff, 0);
                        rtOffText.Offset(vOff2, 0);

                        aOn = Convert.ToByte(ani.Value(AnimationAccel.DCL, 255, 0));
                        aOff = Convert.ToByte(ani.Value(AnimationAccel.DCL, 0, 128));
                    }
                }
                else
                {
                    var v = (OnOff ? onX : offX);
                    rtCursor.X = Convert.ToSingle(MathTool.Constrain(v, offX, onX));
                }
            }

            var rg = Convert.ToInt16(rtCursor.Height / 3 / 3);
            var rtCursorAch = Util.MakeRectangleAlign(rtCursor, new SizeF(rg * 3 + 6, rg * 3 + 6), DvContentAlignment.MiddleCenter);
            var rtCursorAch1 = Util.FromRect(rtCursorAch.Left, rtCursorAch.Top, rg, rtCursorAch.Height);
            var rtCursorAch2 = Util.FromRect(rtCursorAch.Left + rg + 3, rtCursorAch.Top, rg, rtCursorAch.Height);
            var rtCursorAch3 = Util.FromRect(rtCursorAch.Left + rg + 3 + rg + 3, rtCursorAch.Top, rg, rtCursorAch.Height);

            act(rtContent, rtArea, Util.INT(rtCursor), rtOnText, rtOffText, rtCursorAch1, rtCursorAch2, rtCursorAch3, onX, offX, aOn, aOff);
        }
        #endregion
        #endregion
    }
}
