using Devinno.Extensions;
using Devinno.Forms.Controls;
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
    public class DvSliderV : DvControl
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
        #region BarColor
        private Color? cBarColor = null;
        public Color? BarColor
        {
            get => cBarColor;
            set
            {
                if (cBarColor != value)
                {
                    cBarColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Minimum
        private double nMinimum = 0D;
        public double Minimum
        {
            get => nMinimum;
            set
            {
                if (nMinimum != value)
                {
                    nMinimum = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Maximum
        private double nMaximum = 100D;
        public double Maximum
        {
            get => nMaximum;
            set
            {
                if (nMaximum != value)
                {
                    nMaximum = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Value
        private double nValue = 0D;
        public double Value
        {
            get => nValue;
            set
            {
                if (nValue != value)
                {
                    nValue = value;
                    ValueChanged?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion
        #region Tick
        private double? nTick = null;
        public double? Tick
        {
            get => nTick;
            set
            {
                if (nTick != value)
                {
                    nTick = value;
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

        #region FormatString
        private string sFormatString = "0";
        public string FormatString
        {
            get => sFormatString;
            set
            {
                if (sFormatString != value)
                {
                    sFormatString = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Reverse
        private bool bReverse = false;
        public bool Reverse
        {
            get => bReverse;
            set
            {
                if (bReverse != value)
                {
                    bReverse = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region DrawText
        private bool bDrawText = false;
        public bool DrawText
        {
            get => bDrawText;
            set
            {
                if (bDrawText != value)
                {
                    bDrawText = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        bool bDown = false;
        int dx = 0;
        int dy = 0;
        #endregion

        #region Event
        public event EventHandler ValueChanged;
        public event EventHandler CursorDown;
        public event EventHandler CursorUp;
        #endregion

        #region Constructor
        public DvSliderV()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(30, 150);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BoxColor = this.BoxColor ?? Theme.ConcaveBoxColor;
            var BarColor = this.BarColor ?? Theme.PointColor;
            var CursorColor = this.CursorColor ?? Theme.ButtonColor;
            var BorderColor = Theme.GetBorderColor(BoxColor, BarColor);
            var BoxBorderColor = Theme.GetBorderColor(BoxColor, BackColor);
            var CursorBorderColor = Theme.GetBorderColor(CursorColor, BoxColor);

            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            var p = new Pen(Color.Black);
            var br = new SolidBrush(Color.Black);
            #endregion

            Areas((rtContent, rtBack, rtBar, rtCursor, rtA1, rtA2, rtA3) =>
            {
                #region Tick            
                if (Tick.HasValue && Tick != 0)
                {
                    for (double i = Minimum + Tick.Value; i < Maximum; i += Tick.Value)
                    {
                        float y = 0;
                        if (!Reverse)
                            y = Convert.ToInt32(MathTool.Map(i, Minimum, Maximum, rtBack.Bottom, rtBack.Top));
                        else
                            y = Convert.ToInt32(MathTool.Map(i, Minimum, Maximum, rtBack.Top, rtBack.Bottom));

                        p.Width = 1;

                        var nv = 7;
                        p.Color = Util.FromArgb(15, Color.White);
                        e.Graphics.DrawLine(p, rtBar.Left - nv, y + 1, rtBar.Right + nv, y + 1);

                        p.Color = BoxBorderColor;
                        e.Graphics.DrawLine(p, rtBar.Left - nv, y, rtBar.Right + nv, y);
                    }
                }
                #endregion

                Theme.DrawBox(e.Graphics, rtBack, BoxColor, BoxBorderColor, Round, Box.BackBox(ShadowGap));
                Theme.DrawBox(e.Graphics, rtBar, BarColor, BorderColor, Round, Box.ButtonUp_H(true, ShadowGap));

                #region Cursor
                var cc = bDown ? CursorColor.BrightnessTransmit(Theme.DownBrightness * -1) : CursorColor;

                Theme.DrawBox(e.Graphics, rtCursor, cc, CursorBorderColor, RoundType.All, Box.ButtonUp_V(true, ShadowGap));

                #region Bevel
                {
                    var rt = rtCursor;
                    rt.Inflate(-1.5F, -1.5F);
                    using (var path = Util.GetBoxPath(rt, RoundType.All, Theme.Corner))
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

                if (DrawText)
                {
                    #region Text
                    var s = string.IsNullOrWhiteSpace(FormatString) ? Value.ToString() : Value.ToString(FormatString);

                    e.Graphics.SetClip(rtCursor);
                    Theme.DrawText(e.Graphics, s, Font, ForeColor, rtCursor);
                    e.Graphics.ResetClip();
                    #endregion
                }
                else
                {
                    #region Ach
                    var cD = cc.BrightnessTransmit(-0.6F);
                    var cL = cc.BrightnessTransmit(0.3F);

                    br.Color = cc;
                    e.Graphics.FillRectangle(br, rtA1);
                    e.Graphics.FillRectangle(br, rtA2);
                    e.Graphics.FillRectangle(br, rtA3);

                    p.Width = 1;
                    p.Color = cD;
                    e.Graphics.DrawLine(p, rtA1.Left, rtA1.Top, rtA1.Left, rtA1.Bottom); e.Graphics.DrawLine(p, rtA1.Left, rtA1.Top, rtA1.Right, rtA1.Top);
                    e.Graphics.DrawLine(p, rtA2.Left, rtA2.Top, rtA2.Left, rtA2.Bottom); e.Graphics.DrawLine(p, rtA2.Left, rtA2.Top, rtA2.Right, rtA2.Top);
                    e.Graphics.DrawLine(p, rtA3.Left, rtA3.Top, rtA3.Left, rtA3.Bottom); e.Graphics.DrawLine(p, rtA3.Left, rtA3.Top, rtA3.Right, rtA3.Top);

                    p.Color = cL;
                    e.Graphics.DrawLine(p, rtA1.Right, rtA1.Top, rtA1.Right, rtA1.Bottom); e.Graphics.DrawLine(p, rtA1.Left, rtA1.Bottom, rtA1.Right, rtA1.Bottom);
                    e.Graphics.DrawLine(p, rtA2.Right, rtA2.Top, rtA2.Right, rtA2.Bottom); e.Graphics.DrawLine(p, rtA2.Left, rtA2.Bottom, rtA2.Right, rtA2.Bottom);
                    e.Graphics.DrawLine(p, rtA3.Right, rtA3.Top, rtA3.Right, rtA3.Bottom); e.Graphics.DrawLine(p, rtA3.Left, rtA3.Bottom, rtA3.Right, rtA3.Bottom);
                    #endregion
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
            var x = e.X;
            var y = e.Y;

            dx = x;
            dy = y;
            
            Areas((rtContent, rtBack, rtBar, rtCursor, rtA1, rtA2, rtA3) =>
            {
                if (CollisionTool.Check(rtCursor, x, y))
                {
                    bDown = true;
                    CursorDown?.Invoke(this, null);
                }
            });

            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var x = e.X;
            var y = e.Y;
            Areas((rtContent, rtBack, rtBar, rtCursor, rtA1, rtA2, rtA3) =>
            {
                if (bDown)
                {
                    if (!Reverse)
                        Value = MathTool.Map(MathTool.Constrain(y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Maximum, Minimum);
                    else
                        Value = MathTool.Map(MathTool.Constrain(y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Minimum, Maximum);

                    if (Tick.HasValue && Tick != 0) Value = Math.Round(Value / Tick.Value) * Tick.Value;
                }
            });
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            var x = e.X;
            var y = e.Y;
            Areas((rtContent, rtBack, rtBar, rtCursor, rtA1, rtA2, rtA3) =>
            {
                if (bDown)
                {
                    bDown = false;
                    if (!Reverse)
                        Value = MathTool.Map(MathTool.Constrain(y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Maximum, Minimum);
                    else
                        Value = MathTool.Map(MathTool.Constrain(y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Minimum, Maximum);

                    if (Tick.HasValue && Tick != 0) Value = Math.Round(Value / Tick.Value) * Tick.Value;

                    CursorUp?.Invoke(this, null);
                }
                else if (Math.Abs(MathTool.GetDistance(new PointF(x, y), new PointF(dx, dy))) < 10)
                {
                    if (!Reverse)
                        Value = MathTool.Map(MathTool.Constrain(y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Maximum, Minimum);
                    else
                        Value = MathTool.Map(MathTool.Constrain(y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Minimum, Maximum);

                    if (Tick.HasValue && Tick != 0) Value = Math.Round(Value / Tick.Value) * Tick.Value;
                }
            });

            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtBack = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height); rtBack.Inflate(-(rtContent.Width / 4), -(rtContent.Width / 2));

            var ng = Convert.ToInt32(rtBack.Width * 0.1F);
            var rtEmpty = Util.FromRect(rtBack.Left, rtBack.Top, rtBack.Width, rtBack.Height); rtEmpty.Inflate(-ng, -ng);
            var rtBar = new RectangleF();

            var cp = MathTool.CenterPoint(rtEmpty);
            var ny = 0;
            if (!Reverse) ny = Convert.ToInt32(MathTool.Map(Value, Minimum, Maximum, rtEmpty.Bottom, rtEmpty.Top));
            else ny = Convert.ToInt32(MathTool.Map(Value, Minimum, Maximum, rtEmpty.Top, rtEmpty.Bottom));
            cp.Y = ny;

            if (!Reverse)
            {
                rtBar = Util.FromRect(rtEmpty.Left, ny, rtEmpty.Width, rtEmpty.Bottom - ny);
            }
            else
            {
                rtBar = Util.FromRect(rtEmpty.Left, rtEmpty.Top, rtEmpty.Width, ny - rtEmpty.Top);
            }

            var rtCursor = MathTool.MakeRectangle(cp, rtContent.Width);

            var rg = Convert.ToInt16(rtCursor.Height / 5D / 3D);
            var rtCursorAch = Util.MakeRectangleAlign(rtCursor, new SizeF(rg * 3F + 6F, rg * 3F + 6F), DvContentAlignment.MiddleCenter);    rtCursorAch.Offset(0.5F, 0);
            var rtCursorAch1 = Util.FromRect(rtCursorAch.Left, rtCursorAch.Top, rtCursorAch.Width, rg);
            var rtCursorAch2 = Util.FromRect(rtCursorAch.Left, rtCursorAch.Top + rg + 3F, rtCursorAch.Width, rg);
            var rtCursorAch3 = Util.FromRect(rtCursorAch.Left, rtCursorAch.Top + rg + 3F + rg + 3F, rtCursorAch.Width, rg);

            act(rtContent, rtBack, rtBar, rtCursor, rtCursorAch1, rtCursorAch2, rtCursorAch3);
        }
        #endregion
        #endregion
    }
}
