using Devinno.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Color cCursorColor = DvTheme.DefaultTheme.Color3;
        public Color CursorColor
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
        #region BarColor
        private Color cBarColor = DvTheme.DefaultTheme.PointColor;
        public Color BarColor
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
                    if (nValue < nMinimum) nValue = nMinimum;
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
                    if (nValue > nMaximum) nValue = nMaximum;
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
                var v = MathTool.Constrain(value, Minimum, Maximum);
                if (nValue != v)
                {
                    nValue = v;
                    ValueChanged?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion
        #region Tick
        private double nTick = 0;
        public double Tick
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
        #region FormatString
        private string sFormatString = null;
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
        private bool bDrawText = true;
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

        #region Event
        public event EventHandler ValueChanged;
        public event EventHandler CursorDown;
        public event EventHandler CursorUp;
        #endregion

        #region Member Variable
        bool bDown = false;
        Point downPoint;
        #endregion

        #region Constructor
        public DvSliderV()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            this.Size = new Size(30, 300);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            var rtBack = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height); rtBack.Inflate(-(rtContent.Width / 4), -(rtContent.Width / 2));
            SetArea("rtBack", rtBack);

            var ng = rtBack.Width / 10;
            var rtEmpty = new Rectangle(rtBack.X, rtBack.Y, rtBack.Width, rtBack.Height); rtEmpty.Inflate(-ng, -ng);

            var cp = MathTool.CenterPoint(rtEmpty);
            var ny = 0;
            if (!Reverse) ny = Convert.ToInt32(MathTool.Map(Value, Minimum, Maximum, rtEmpty.Bottom, rtEmpty.Top));
            else ny = Convert.ToInt32(MathTool.Map(Value, Minimum, Maximum, rtEmpty.Top, rtEmpty.Bottom));
            cp.Y = ny;

            if (!Reverse)
            {
                var rtBar = new Rectangle(rtEmpty.X, ny, rtEmpty.Width, rtEmpty.Bottom - ny);
                SetArea("rtBar", rtBar);
            }
            else
            {
                var rtBar = new Rectangle(rtEmpty.X, rtEmpty.Top, rtEmpty.Width, ny - rtEmpty.Top);
                SetArea("rtBar", rtBar);
            }

            var rtCursor = MathTool.MakeRectangle(cp, rtContent.Width);
            SetArea("rtCursor", rtCursor);

            var rg = Convert.ToInt32(rtCursor.Height / 5D / 3D);
            var rtCursorAch = DrawingTool.MakeRectangleAlign(rtCursor, new Size(rg * 3 + 4, rg * 3 + 4), DvContentAlignment.MiddleCenter);
            var rtCursorAch1 = new Rectangle(rtCursorAch.X, rtCursorAch.Y, rg, rtCursorAch.Height);
            var rtCursorAch2 = new Rectangle(rtCursorAch.X + rg + 2, rtCursorAch.Y, rg, rtCursorAch.Height);
            var rtCursorAch3 = new Rectangle(rtCursorAch.X + rg + 2 + rg + 2, rtCursorAch.Y, rg, rtCursorAch.Height);
            SetArea("rtCursorA1", rtCursorAch1);
            SetArea("rtCursorA2", rtCursorAch2);
            SetArea("rtCursorA3", rtCursorAch3);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BoxColor = UseThemeColor ? Theme.Color1 : this.BoxColor;
            var FillColor = UseThemeColor ? Theme.PointColor : this.BarColor;
            var CursorColor = UseThemeColor ? Theme.Color3 : this.CursorColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtBack = Areas["rtBack"];
            var rtCursor = Areas["rtCursor"];
            var rtBar = Areas["rtBar"];
            var rtA1 = Areas["rtCursorA1"];
            var rtA2 = Areas["rtCursorA2"];
            var rtA3 = Areas["rtCursorA3"];
            #endregion
            #region Init
            var p = new Pen(BoxColor, 1);
            var br = new SolidBrush(BoxColor);
            #endregion
            #region Draw
            Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtBack, RoundType.ALL, BoxDrawOption.OUT_BEVEL | BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW);
            Theme.DrawBox(e.Graphics, FillColor, BoxColor, rtBar, RoundType.ALL, BoxDrawOption.OUT_SHADOW | BoxDrawOption.BORDER | BoxDrawOption.GRADIENT_H);

            #region Tick            
            if (Tick != 0)
            {
                for (double i = Minimum + Tick; i < Maximum; i += Tick)
                {
                    int y = 0;
                    if (!Reverse)
                        y = Convert.ToInt32(MathTool.Map(i, Minimum, Maximum, rtBack.Bottom, rtBack.Top));
                    else
                        y = Convert.ToInt32(MathTool.Map(i, Minimum, Maximum, rtBack.Top, rtBack.Bottom));

                    p.Width = 1;
                    p.Color = BackColor.BrightnessTransmit(Theme.OutBevelBright);
                    e.Graphics.DrawLine(p, rtContent.Left, y + 1, rtBack.Left, y + 1);
                    e.Graphics.DrawLine(p, rtContent.Right, y + 1, rtBack.Right, y + 1);

                    p.Color = BackColor.BrightnessTransmit(Theme.BorderBright * 2);
                    e.Graphics.DrawLine(p, rtContent.Left, y, rtBack.Left, y);
                    e.Graphics.DrawLine(p, rtContent.Right, y, rtBack.Right, y);
                }
            }
            #endregion

            #region Cursor
            var cc = bDown ? CursorColor.BrightnessTransmit(Theme.DownBright * -1) : CursorColor;

            Theme.DrawBox(e.Graphics, cc, BackColor, rtCursor, RoundType.ALL, BoxDrawOption.OUT_SHADOW | BoxDrawOption.BORDER | BoxDrawOption.GRADIENT_V | BoxDrawOption.IN_BEVEL2);

            if (DrawText)
            {
                var s = string.IsNullOrWhiteSpace(FormatString) ? Value.ToString() : Value.ToString(FormatString);
                e.Graphics.SetClip(rtCursor);
                Theme.DrawTextShadow(e.Graphics, null, s, Font, ForeColor, FillColor, rtCursor, DvContentAlignment.MiddleCenter);
                e.Graphics.ResetClip();
            }
            else
            {
                var c = cc.BrightnessTransmit(-0.3);
                var cD = c.BrightnessTransmit(Theme.InShadowBright);
                var cL = cc.BrightnessTransmit(Theme.OutBevelBright);

                br.Color = c;
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

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            downPoint = e.Location;
            if (Areas.ContainsKey("rtCursor") && Areas.ContainsKey("rtBack"))
            {
                var rtCursor = Areas["rtCursor"];
                if (CollisionTool.Check(rtCursor, e.Location))
                {
                    bDown = true;
                    CursorDown?.Invoke(this, null);
                    Invalidate();
                }
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtCursor") && Areas.ContainsKey("rtBack"))
            {
                var rtCursor = Areas["rtCursor"];
                var rtBack = Areas["rtBack"];

                if (bDown)
                {
                    if (!Reverse)
                        Value = MathTool.Map(MathTool.Constrain(e.Y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Maximum, Minimum);
                    else
                        Value = MathTool.Map(MathTool.Constrain(e.Y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Minimum, Maximum);

                    if (Tick != 0) Value = Math.Round(Value / Tick) * Tick;
                }
            }
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtCursor") && Areas.ContainsKey("rtBack"))
            {
                var rtCursor = Areas["rtCursor"];
                var rtBack = Areas["rtBack"];

                if (bDown)
                {
                    bDown = false;
                    if (!Reverse)
                        Value = MathTool.Map(MathTool.Constrain(e.Y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Maximum, Minimum);
                    else
                        Value = MathTool.Map(MathTool.Constrain(e.Y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Minimum, Maximum);

                    if (Tick != 0) Value = Math.Round(Value / Tick) * Tick;
                    Invalidate();

                    CursorUp?.Invoke(this, null);
                }
                else if (Math.Abs(MathTool.GetDistance(e.Location, downPoint)) < 10)
                {
                    if (!Reverse)
                        Value = MathTool.Map(MathTool.Constrain(e.Y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Maximum, Minimum);
                    else
                        Value = MathTool.Map(MathTool.Constrain(e.Y, rtBack.Top, rtBack.Bottom), rtBack.Top, rtBack.Bottom, Minimum, Maximum);

                    if (Tick != 0) Value = Math.Round(Value / Tick) * Tick;
                    Invalidate();
                }

            }
            base.OnMouseDown(e);
        }
        #endregion
        #endregion
    }
}
