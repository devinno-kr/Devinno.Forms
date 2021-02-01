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
    public class DvGauge : DvControl
    {
        #region Properties
        #region FillColor
        private Color cFillColor = DvTheme.DefaultTheme.PointColor;
        public Color FillColor
        {
            get => cFillColor;
            set
            {
                if (cFillColor != value)
                {
                    cFillColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region EmptyColor
        private Color cEmptyColor = DvTheme.DefaultTheme.Color1;
        public Color EmptyColor
        {
            get => cEmptyColor;
            set
            {
                if (cEmptyColor != value)
                {
                    cEmptyColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region RemarkColor
        private Color cRemarkColor = DvTheme.DefaultTheme.Color5;
        public Color RemarkColor
        {
            get => cRemarkColor;
            set
            {
                if (cRemarkColor != value)
                {
                    cRemarkColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Minimum
        private decimal nMinimum = 0M;
        public decimal Minimum
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
        private decimal nMaximum = 100M;
        public decimal Maximum
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
        private decimal nValue = 0M;
        public decimal Value
        {
            get => nValue;
            set
            {
                var v = Convert.ToDecimal(MathTool.Constrain(Convert.ToDouble(value), Convert.ToDouble(Minimum), Convert.ToDouble(Maximum)));
                if (nValue != v)
                {
                    nValue = v;
                    Invalidate();
                }
            }
        }
        #endregion
        #region GraduationLarge
        private decimal nGraduationLarge = 10M;
        public decimal GraduationLarge
        {
            get => nGraduationLarge;
            set
            {
                if (nGraduationLarge != value)
                {
                    nGraduationLarge = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region GraduationSmall
        private decimal nGraduationSmall = 2M;
        public decimal GraduationSmall
        {
            get => nGraduationSmall;
            set
            {
                if (nGraduationSmall != value)
                {
                    nGraduationSmall = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region StartAngle
        private int nStartAngle = 135;
        public int StartAngle
        {
            get => nStartAngle;
            set
            {
                if (nStartAngle != value)
                {
                    nStartAngle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region SweepAngle
        private int nSweepAngle = 270;
        public int SweepAngle
        {
            get => nSweepAngle;
            set
            {
                if (nSweepAngle != value)
                {
                    nSweepAngle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvGauge()
        {

        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            var wh = Math.Min(rtContent.Width, rtContent.Height);
            var rtCircle = DrawingTool.MakeRectangleAlign(rtContent, new Size(wh, wh), DvContentAlignment.MiddleCenter);
            SetArea("rtCircle", rtCircle);

            #region rtRemark
            var cp = MathTool.CenterPoint(rtCircle);
            float L = rtCircle.Left, R = rtCircle.Right, T = rtCircle.Top, B = rtCircle.Bottom;
            for (decimal i = Minimum; i <= Maximum; i += GraduationLarge)
            {
                var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain((double)i, (double)Minimum, (double)Maximum), (double)Minimum, (double)Maximum, 0D, SweepAngle)) + StartAngle;

                var pT = MathTool.GetPointWithAngle(cp, gsang, rtCircle.Width / 2);
                var txt = i.ToString("0");
                var sz = g.MeasureString(txt, Font);
                var rt = MathTool.MakeRectangle(pT, (int)Math.Ceiling(sz.Width), (int)Math.Ceiling(sz.Height));

                L = Math.Min(rt.Left, L);
                R = Math.Max(rt.Right, R);
                T = Math.Min(rt.Top, T);
                B = Math.Max(rt.Bottom, B);
            }
            var vm = Math.Max(Math.Max(Math.Abs(L - rtCircle.Left), Math.Abs(R - rtCircle.Right)), Math.Max(Math.Abs(T - rtCircle.Top), Math.Abs(B - rtCircle.Bottom)));
            var rtRemark = new Rectangle(rtCircle.X, rtCircle.Y, rtCircle.Width, rtCircle.Height);
            rtRemark.Inflate(-Convert.ToInt32(vm / 2F), -Convert.ToInt32(vm / 2F));
            SetArea("rtRemark", rtRemark);
            #endregion
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var FillColor = UseThemeColor ? Theme.PointColor : this.FillColor;
            var EmptyColor = UseThemeColor ? Theme.Color1 : this.EmptyColor;
            var RemarkColor = UseThemeColor ? Theme.Color5 : this.RemarkColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtCircle = Areas["rtCircle"];
            var rtRemark = Areas["rtRemark"];
            #endregion
            #region Init
            var p = new Pen(EmptyColor, 2);
            var br = new SolidBrush(EmptyColor);
            #endregion
            #region Draw
            e.Graphics.Clear(BackColor);
            var cp = MathTool.CenterPoint(rtRemark);
            #region Remark
            var ng = Convert.ToInt32(16 * DpiRatio);
            p.Color = RemarkColor;
            #region Graduation Large
            for (decimal i = Minimum; i <= Maximum; i += GraduationLarge)
            {
                var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain((double)i, (double)Minimum, (double)Maximum), (double)Minimum, (double)Maximum, 0D, SweepAngle)) + StartAngle;

                var pT = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2);
                var p1 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - (ng / 2));
                var p2 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - ng);

                var txt = i.ToString("0");
                var sz = e.Graphics.MeasureString(txt, Font);
                var rt = MathTool.MakeRectangle(pT, sz.Width / 2F, sz.Height / 2F);
                p.Width = 2;
                e.Graphics.DrawLine(p, p1, p2);

                br.Color = BackColor; e.Graphics.FillEllipse(br, rt);
                rt.Offset(0, 1); br.Color = BackColor.BrightnessTransmit(Theme.OutShadowBright); e.Graphics.DrawString(txt, Font, br, rt);
                rt.Offset(0, -1); br.Color = RemarkColor; e.Graphics.DrawString(txt, Font, br, rt);
            }
            #endregion
            #region Graduation Small
            for (decimal i = Minimum; i <= Maximum; i += GraduationSmall)
            {
                if (i % GraduationLarge != 0)
                {
                    var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain((double)i, (double)Minimum, (double)Maximum), (double)Minimum, (double)Maximum, 0D, SweepAngle)) + StartAngle;
                    var p1 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - (ng / 1.35F));
                    var p2 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - ng);

                    p.Width = 1; e.Graphics.DrawLine(p, p1, p2);
                }
            }
            #endregion
            #region Arc
            var rtRemarkIn = new Rectangle(rtRemark.X, rtRemark.Y, rtRemark.Width, rtRemark.Height); rtRemarkIn.Inflate(-ng, -ng);
            p.Width = 2;    e.Graphics.DrawArc(p, rtRemarkIn, StartAngle, SweepAngle);
            #endregion
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
    }
}
