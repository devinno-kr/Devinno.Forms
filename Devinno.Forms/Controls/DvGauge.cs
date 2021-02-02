using Devinno.Extensions;
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
    public class DvGauge : DvControl
    {
        #region Cosnt
        const float DIV = 7F;
        #endregion

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
                    Invalidate();
                }
            }
        }
        #endregion
        #region GraduationLarge
        private double nGraduationLarge = 10D;
        public double GraduationLarge
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
        private double nGraduationSmall = 2D;
        public double GraduationSmall
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
        #region ValueFormatString
        private string strValueFormatString = null;
        public string ValueFormatString
        {
            get => strValueFormatString;
            set
            {
                if(strValueFormatString != value)
                {
                    strValueFormatString = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region RemarkFormatString
        private string strRemarkFormatString = null;
        public string RemarkFormatString
        {
            get => strRemarkFormatString;
            set
            {
                if (strRemarkFormatString != value)
                {
                    strRemarkFormatString = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region ValueFont
        private Font ftValueFont = new Font("나눔고딕", 12, FontStyle.Bold);
        public Font ValueFont
        {
            get => ftValueFont;
            set
            {
                if (ftValueFont != value)
                {
                    ftValueFont = value;
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
            var ngp1 = DIV;
            var ngp2 = wh / 35;
            var rtCircle = DrawingTool.MakeRectangleAlign(rtContent, new Size(wh, wh), DvContentAlignment.MiddleCenter);
            var rtCircleIn = new Rectangle(rtCircle.X, rtCircle.Y, rtCircle.Width, rtCircle.Height); rtCircleIn.Inflate(-Convert.ToInt32(wh / ngp1), -Convert.ToInt32(wh / ngp1));

            SetArea("rtCircle", rtCircle);
            SetArea("rtCircleIn", rtCircleIn);
            #region rtRemark
            var cp = MathTool.CenterPoint(rtCircleIn);
            float L = rtCircleIn.Left, R = rtCircleIn.Right, T = rtCircleIn.Top, B = rtCircleIn.Bottom;
            for (double i = Minimum; i <= Maximum; i += GraduationLarge)
            {
                var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(i, Minimum, Maximum), Minimum, Maximum, 0D, SweepAngle)) + StartAngle;

                var pT = MathTool.GetPointWithAngle(cp, gsang, rtCircleIn.Width / 2);
                var txt = i.ToString(string.IsNullOrWhiteSpace(RemarkFormatString) ? "0" : RemarkFormatString);
                var sz = g.MeasureString(txt, Font);
                var rt = MathTool.MakeRectangle(pT, (int)Math.Ceiling(sz.Width), (int)Math.Ceiling(sz.Height));

                L = Math.Min(rt.Left, L);
                R = Math.Max(rt.Right, R);
                T = Math.Min(rt.Top, T);
                B = Math.Max(rt.Bottom, B);
            }
            var vm = Math.Max(Math.Max(Math.Abs(L - rtCircleIn.Left), Math.Abs(R - rtCircleIn.Right)), Math.Max(Math.Abs(T - rtCircleIn.Top), Math.Abs(B - rtCircleIn.Bottom)));
            var rtRemark = new Rectangle(rtCircleIn.X, rtCircleIn.Y, rtCircleIn.Width, rtCircleIn.Height);
            rtRemark.Inflate(-Convert.ToInt32(vm / 2F) - ngp2, -Convert.ToInt32(vm / 2F) - ngp2);
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
            var BorderColor = (EmptyColor.GetBrightness() > BackColor.GetBrightness() ? BackColor : EmptyColor).BrightnessTransmit(Theme.BorderBright * 1.5);
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtCircle = Areas["rtCircle"];
            var rtCircleIn = Areas["rtCircleIn"];
            var rtRemark = Areas["rtRemark"];
            var cp = MathTool.CenterPoint(rtCircle);
            #endregion
            #region Init
            var p = new Pen(EmptyColor, 2);
            var br = new SolidBrush(EmptyColor);
            #endregion
            #region Draw
            e.Graphics.Clear(BackColor);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            #region Shadow
            e.Graphics.ResetTransform();
            using (var pth = new GraphicsPath())
            {
                pth.AddArc(rtCircle, StartAngle, SweepAngle);
                pth.AddArc(rtCircleIn, StartAngle + SweepAngle, -SweepAngle);
                pth.CloseAllFigures();

                e.Graphics.TranslateTransform(Theme.ShadowGap, Theme.ShadowGap);
                #region Fill
                br.Color = BackColor.BrightnessTransmit(Theme.OutShadowBright);
                e.Graphics.FillPath(br, pth);
                #endregion
                e.Graphics.ResetTransform();
            }
            #endregion
            #region Remark
            #region Graduation Large
            var ng = (rtCircleIn.Width - rtRemark.Width) / 4;
            for (double i = Minimum; i <= Maximum; i += GraduationLarge)
            {
                var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(i, Minimum, Maximum), Minimum, Maximum, 0D, SweepAngle)) + StartAngle;

                var pT = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2);
                var p1 = MathTool.GetPointWithAngle(cp, gsang, rtCircleIn.Width / 2);
                var p2 = MathTool.GetPointWithAngle(cp, gsang, rtCircleIn.Width / 2 - ng);

                var txt = i.ToString(string.IsNullOrWhiteSpace(RemarkFormatString) ? "0" : RemarkFormatString);
                var sz = e.Graphics.MeasureString(txt, Font);
                var rt = MathTool.MakeRectangle(pT, sz.Width / 2F, sz.Height / 2F);
                p.Width = 1;
                p.Color = RemarkColor;
                e.Graphics.DrawLine(p, p1, p2);

                //br.Color = BackColor; e.Graphics.FillEllipse(br, rt);
                rt.Offset(0, 1); br.Color = BackColor.BrightnessTransmit(Theme.OutShadowBright); e.Graphics.DrawString(txt, Font, br, rt);
                rt.Offset(0, -1); br.Color = RemarkColor; e.Graphics.DrawString(txt, Font, br, rt);
            }
            #endregion
            #region Graduation Small
            for (double i = Minimum; i <= Maximum; i += GraduationSmall)
            {
                if (i % GraduationLarge != 0)
                {
                    var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(i, Minimum, Maximum), Minimum, Maximum, 0D, SweepAngle)) + StartAngle;
                    var p1 = MathTool.GetPointWithAngle(cp, gsang, rtCircleIn.Width / 2);
                    var p2 = MathTool.GetPointWithAngle(cp, gsang, rtCircleIn.Width / 2 - (ng / 2F));

                    p.Width = 1; e.Graphics.DrawLine(p, p1, p2);
                }
            }
            #endregion
            #endregion
            #region Empty
            e.Graphics.ResetTransform();
            using (var pth = new GraphicsPath())
            {
                pth.AddArc(rtCircle, StartAngle, SweepAngle);
                pth.AddArc(rtCircleIn, StartAngle + SweepAngle, -SweepAngle);
                pth.CloseAllFigures();

                #region Fill
                br.Color = EmptyColor;
                e.Graphics.FillPath(br, pth);
                #endregion
                #region Bevel
                e.Graphics.SetClip(pth);
                e.Graphics.TranslateTransform(1, 1);
                p.Width = 1;
                p.Color = EmptyColor.BrightnessTransmit(Theme.InBevelBright);
                //e.Graphics.DrawPath(p, pth);
                e.Graphics.TranslateTransform(-1, -1);
                e.Graphics.ResetClip();
                #endregion
            }
            #endregion
            #region Fill
            e.Graphics.ResetTransform();
            using (var pth = new GraphicsPath())
            {
                var Ang = Convert.ToSingle(MathTool.Map(Value, Minimum, Maximum, 0, SweepAngle));
                pth.AddArc(rtCircle, StartAngle, Ang);
                pth.AddArc(rtCircleIn, StartAngle + Ang, -Ang);
                pth.CloseAllFigures();

                #region Gradient
                using (var lgbr = new LinearGradientBrush(rtCircle, FillColor.BrightnessTransmit(Theme.GradientLightBright), FillColor.BrightnessTransmit(Theme.GradientDarkBright), 45))
                {
                    e.Graphics.FillPath(lgbr, pth);
                }
                #endregion
                #region Gradient2
                e.Graphics.SetClip(pth);
                e.Graphics.DrawImage(ResourceTool.circlegrad, rtCircle);
                e.Graphics.ResetClip();
                #endregion
                #region Bevel
                e.Graphics.SetClip(pth);
                e.Graphics.TranslateTransform(1, 1);
                p.Width = 2;
                p.Color = Color.FromArgb(30, Color.White);
                e.Graphics.DrawPath(p, pth);
                e.Graphics.TranslateTransform(-1, -1);
                e.Graphics.ResetClip();
                #endregion
                
            }
            #endregion
            #region Border
            using (var pth = new GraphicsPath())
            {
                pth.AddArc(rtCircle, StartAngle, SweepAngle);
                pth.AddArc(rtCircleIn, StartAngle + SweepAngle, -SweepAngle);
                pth.CloseAllFigures();

                p.Width = 1;
                p.Color = BorderColor;
                e.Graphics.DrawPath(p, pth);
            }
            #endregion
            #region Text
            {
                var txt = Value.ToString(string.IsNullOrWhiteSpace(ValueFormatString) ? "0" : ValueFormatString);
                Theme.DrawTextShadow(e.Graphics, null, txt, ValueFont, ForeColor, BackColor, rtCircle);
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
    }
}
