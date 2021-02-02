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
    public class DvMeter : DvControl
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
        #region NeedleColor
        private Color cNeedleColor = Color.White;
        public Color NeedleColor
        {
            get => cNeedleColor;
            set
            {
                if (cNeedleColor != value)
                {
                    cNeedleColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region NeedlePointColor
        private Color cNeedlePointColor = Color.Red;
        public Color NeedlePointColor
        {
            get => cNeedlePointColor;
            set
            {
                if (cNeedlePointColor != value)
                {
                    cNeedlePointColor = value;
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
                if (strValueFormatString != value)
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
        public DvMeter()
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
            var ngp1 = 7;
            var ngp2 = wh / 35;
            var rtCircle = DrawingTool.MakeRectangleAlign(rtContent, new Size(wh, wh), DvContentAlignment.MiddleCenter);

            SetArea("rtCircle", rtCircle);
            #region rtRemark
            var cp = MathTool.CenterPoint(rtCircle);
            float L = rtCircle.Left, R = rtCircle.Right, T = rtCircle.Top, B = rtCircle.Bottom;
            for (double i = Minimum; i <= Maximum; i += GraduationLarge)
            {
                var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(i, Minimum, Maximum), Minimum, Maximum, 0D, SweepAngle)) + StartAngle;

                var pT = MathTool.GetPointWithAngle(cp, gsang, rtCircle.Width / 2);
                var txt = i.ToString(string.IsNullOrWhiteSpace(RemarkFormatString) ? "0" : RemarkFormatString);
                var sz = g.MeasureString(txt, Font);
                var rt = MathTool.MakeRectangle(pT, (int)Math.Ceiling(sz.Width), (int)Math.Ceiling(sz.Height));

                L = Math.Min(rt.Left, L);
                R = Math.Max(rt.Right, R);
                T = Math.Min(rt.Top, T);
                B = Math.Max(rt.Bottom, B);
            }
            var vm = Math.Max(Math.Max(Math.Abs(L - rtCircle.Left), Math.Abs(R - rtCircle.Right)), Math.Max(Math.Abs(T - rtCircle.Top), Math.Abs(B - rtCircle.Bottom)));
            var rtRemark = new Rectangle(rtCircle.X, rtCircle.Y, rtCircle.Width, rtCircle.Height);
            rtRemark.Inflate(-Convert.ToInt32(vm / 2F) - ngp2, -Convert.ToInt32(vm / 2F) - ngp2);
            SetArea("rtRemark", rtRemark);
            #endregion
            #region rtGauge
            var ng = (rtCircle.Width - rtRemark.Width) / 2;
            var rtRemarkIn = new Rectangle(rtRemark.X, rtRemark.Y, rtRemark.Width, rtRemark.Height); rtRemarkIn.Inflate(-ng, -ng);
            var rtGauge = new Rectangle(rtRemarkIn.X, rtRemarkIn.Y, rtRemarkIn.Width, rtRemarkIn.Height); rtGauge.Inflate(-ng / 2, -ng / 2);
            SetArea("rtGauge", rtGauge);
            #endregion

        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var FillColor = UseThemeColor ? Theme.PointColor : this.FillColor;
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
            var rtGuage = Areas["rtGauge"];
            #endregion
            #region Init
            var p = new Pen(FillColor, 2);
            var br = new SolidBrush(FillColor);
            #endregion
            #region Draw
            e.Graphics.Clear(BackColor);
            var _cp =  MathTool.CenterPoint(rtRemark);
            var cp = new Point(Convert.ToInt32(_cp.X), Convert.ToInt32(_cp.Y));
            #region Remark
            var ng = (rtCircle.Width - rtRemark.Width) / 2;
            p.Color = RemarkColor;
            var thickL = 3;
            #region Graduation Large
            for (double i = Minimum; i <= Maximum; i += GraduationLarge)
            {
                var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(i, Minimum, Maximum), Minimum, Maximum, 0D, SweepAngle)) + StartAngle;

                var pT = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2);
                var p1 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - (ng / 2));
                var p2 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - ng);

                var txt = i.ToString(string.IsNullOrWhiteSpace(RemarkFormatString) ? "0" : RemarkFormatString);
                var sz = e.Graphics.MeasureString(txt, Font);
                var rt = MathTool.MakeRectangle(pT, sz.Width / 2F, sz.Height / 2F);
                p.Width = thickL;
                e.Graphics.DrawLine(p, p1, p2);

                br.Color = BackColor; e.Graphics.FillEllipse(br, rt);
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
                    var p1 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - (ng / 1.35F));
                    var p2 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - ng);

                    p.Width = 1; e.Graphics.DrawLine(p, p1, p2);
                }
            }
            #endregion
            #region Arc
            var rtRemarkIn = new Rectangle(rtRemark.X, rtRemark.Y, rtRemark.Width, rtRemark.Height); rtRemarkIn.Inflate(-ng, -ng);
            p.Width = thickL; e.Graphics.DrawArc(p, rtRemarkIn, StartAngle, SweepAngle);
            #endregion
            #endregion
            #region Gauge
            {
                var rt = new Rectangle(rtGuage.X, rtGuage.Y, rtGuage.Width, rtGuage.Height);
                using (var pth = new GraphicsPath())
                {
                    var vang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(Value, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle));

                    pth.AddArc(rt, StartAngle, vang);
                    rt.Inflate(-rt.Width / 20, -rt.Width / 20);
                    pth.AddArc(rt, StartAngle + vang, -vang);
                    pth.CloseAllFigures();

                    br.Color = FillColor;
                    e.Graphics.FillPath(br, pth);
                }
            }
            #endregion
            #region Needle
            {
                var vang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(Value, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle));
                var ngpin = rtCircle.Width / 12;

                using (var pth = new GraphicsPath())
                {
                    #region Path
                    var mat1 = new Matrix(); mat1.Translate(2, 2);
                    var mat2 = new Matrix(); mat2.Translate(-2, -2);

                    
                    var pt = MathTool.GetPointWithAngle(cp, vang + StartAngle, rtRemark.Width / 2);
                    var rtS = MathTool.MakeRectangle(pt, 3);
                    var rtL = MathTool.MakeRectangle(cp, ngpin);
                    var rtF = MathTool.MakeRectangle(pt, cp); rtF.Inflate(ngpin, ngpin);

                    /*
                    var pt = new Point(Convert.ToInt32(_pt.X), Convert.ToInt32(_pt.Y));
                    var rtS = new Rectangle(Convert.ToInt32(_rtS.X), Convert.ToInt32(_rtS.Y), Convert.ToInt32(_rtS.Width), Convert.ToInt32(_rtS.Height));
                    var rtL = new Rectangle(Convert.ToInt32(_rtL.X), Convert.ToInt32(_rtL.Y), Convert.ToInt32(_rtL.Width), Convert.ToInt32(_rtL.Height));
                    var rtF = new Rectangle(Convert.ToInt32(_rtF.X), Convert.ToInt32(_rtF.Y), Convert.ToInt32(_rtF.Width), Convert.ToInt32(_rtF.Height));
                    */

                    pth.AddArc(rtL, vang + StartAngle + 90, 180);
                    pth.AddArc(rtS, vang + StartAngle + 90 + 180, 180);
                    pth.CloseAllFigures();
                    #endregion
                
                    #region Shadow
                    pth.Flatten(mat1);
                    br.Color = Color.FromArgb(90, Color.Black); 
                    e.Graphics.FillPath(br, pth);
                    #endregion
                    #region Needle
                    pth.Flatten(mat2);
                    using (var pth2 = new GraphicsPath())
                    {
                        pth2.AddEllipse(rtF);
                        using (var lgbr = new LinearGradientBrush(rtF, NeedleColor, NeedleColor, vang + StartAngle)) 
                        {
                            var cb = new ColorBlend();
                            cb.Positions = new float[] { 0F, 0.6F, 0.6F, 1F };
                            cb.Colors = new Color[] { NeedleColor, NeedleColor, NeedlePointColor, NeedlePointColor };
                            lgbr.InterpolationColors = cb;

                            e.Graphics.FillPath(lgbr, pth);
                        }
                    }
                    #endregion
                    #region Border
                    p.Width = 1;
                    p.Color = BackColor.BrightnessTransmit(Theme.BorderBright);
                    e.Graphics.DrawPath(p, pth);
                    #endregion
                }

                var rtHole = MathTool.MakeRectangle(cp, ngpin / 3);
                br.Color = BackColor.BrightnessTransmit(Theme.BorderBright);
                e.Graphics.FillEllipse(br, rtHole);
            }
            #endregion
            #region Text
            if (DrawText)
            {
                var txt = Value.ToString(string.IsNullOrWhiteSpace(ValueFormatString) ? "0" : ValueFormatString);
                var sz = e.Graphics.MeasureString(txt, ValueFont);
                //var pt = MathTool.GetPointWithAngle(cp, StartAngle - (360 - SweepAngle) / 2, rtRemarkIn.Width / 2 - Convert.ToInt32(sz.Height / 2));
                var pt = MathTool.GetPointWithAngle(cp, StartAngle - (360 - SweepAngle) / 2, (rtRemarkIn.Width / 2) - Convert.ToInt32(sz.Height / 4));
                var rt = MathTool.MakeRectangle(new Point(Convert.ToInt32(pt.X), Convert.ToInt32(pt.Y)), Convert.ToInt32(sz.Width), Convert.ToInt32(sz.Height));

                Theme.DrawTextShadow(e.Graphics, null, txt, ValueFont, ForeColor, BackColor, rt, DvContentAlignment.MiddleCenter);
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
