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
    public class DvKnob : DvControl
    {
        #region Properties
        #region KnobColor
        private Color cKnobColor = DvTheme.DefaultTheme.Color3;
        public Color KnobColor
        {
            get => cKnobColor;
            set
            {
                if (cKnobColor != value)
                {
                    cKnobColor = value;
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
                    ValueChanged?.Invoke(this, null);
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

        #region Event
        public event EventHandler ValueChanged;
        #endregion

        #region Constructor
        public DvKnob()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            var wh = Math.Min(rtContent.Width, rtContent.Height);
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
            #region rtRemarkIn
            var ng = (rtCircle.Width - rtRemark.Width) / 2;
            var rtRemarkIn = new Rectangle(rtRemark.X, rtRemark.Y, rtRemark.Width, rtRemark.Height); rtRemarkIn.Inflate(-ng, -ng);
            SetArea("rtRemarkIn", rtRemarkIn);
            #endregion
            #region rtKnob
            var rtKnob = new Rectangle(rtRemarkIn.X, rtRemarkIn.Y, rtRemarkIn.Width, rtRemarkIn.Height); rtKnob.Inflate(-ng / 2, -ng / 2);
            SetArea("rtKnob", rtKnob);

            var rtKnobIn = new Rectangle(rtKnob.X, rtKnob.Y, rtKnob.Width, rtKnob.Height); rtKnobIn.Inflate(-Convert.ToInt32(rtKnob.Width / 3), -Convert.ToInt32(rtKnob.Width / 3));
            SetArea("rtKnobIn", rtKnobIn);
            #endregion
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var KnobColor = UseThemeColor ? Theme.Color3 : this.KnobColor;
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
            var rtRemarkIn = Areas["rtRemarkIn"];
            var rtKnob = Areas["rtKnob"];
            var rtKnobIn = Areas["rtKnobIn"];
            #endregion
            #region Init
            var p = new Pen(KnobColor, 2);
            var br = new SolidBrush(KnobColor);
            #endregion
            #region Draw
            e.Graphics.Clear(BackColor);
            var cp = MathTool.CenterPoint(rtRemark);
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
                var p2 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - ng - 1);

                var txt = i.ToString(string.IsNullOrWhiteSpace(RemarkFormatString) ? "0" : RemarkFormatString);
                var sz = e.Graphics.MeasureString(txt, Font);
                var _rt = MathTool.MakeRectangle(pT, sz.Width / 2F, sz.Height / 2F);
                var rt = new Rectangle(Convert.ToInt32(_rt.X), Convert.ToInt32(_rt.Y), Convert.ToInt32(_rt.Width), Convert.ToInt32(_rt.Height)); rt.Inflate(1, 1);
                p.Width = thickL;
                e.Graphics.DrawLine(p, p1, p2);

                br.Color = BackColor; e.Graphics.FillEllipse(br, rt);
                Theme.DrawTextShadow(e.Graphics, null, txt, Font, RemarkColor, BackColor, rt, DvContentAlignment.MiddleCenter);
            }
            #endregion
            #region Graduation Small
            for (double i = Minimum; i <= Maximum; i += GraduationSmall)
            {
                if (i % GraduationLarge != 0)
                {
                    var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(i, Minimum, Maximum), Minimum, Maximum, 0D, SweepAngle)) + StartAngle;
                    var p1 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - (ng / 1.25F));
                    var p2 = MathTool.GetPointWithAngle(cp, gsang, rtRemark.Width / 2 - ng - 1);

                    p.Width = 1; e.Graphics.DrawLine(p, p1, p2);
                }
            }
            #endregion
            #endregion
            #region Knob
            using (var pth = new GraphicsPath())
            {
                pth.AddEllipse(rtKnob);
                //pth.AddEllipse(rtKnobIn);

                #region Shadow
                e.Graphics.TranslateTransform(Theme.ShadowGap, Theme.ShadowGap);
                br.Color = BackColor.BrightnessTransmit(Theme.OutShadowBright);
                e.Graphics.FillPath(br, pth);
                e.Graphics.TranslateTransform(-Theme.ShadowGap, -Theme.ShadowGap);
                #endregion
                #region Fill
                br.Color = KnobColor;
                e.Graphics.FillPath(br, pth);
                #endregion
                #region Mask
                e.Graphics.SetClip(pth);
                e.Graphics.DrawImage(ResourceTool.volumemask, rtKnob);
                e.Graphics.ResetClip();
                #endregion
                #region Border
                p.Width = 1;
                p.Color = BackColor.BrightnessTransmit(Theme.BorderBright);
                e.Graphics.DrawPath(p, pth);
                #endregion
            }
            #endregion
            #region Cursor
            {
                var vang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(Value, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle)) + StartAngle;

                var wh = rtKnob.Width / 2;
                var pt1 = MathTool.GetPointWithAngle(cp, vang, wh - (wh / 6));
                var pt2 = MathTool.GetPointWithAngle(cp, vang, wh - Convert.ToInt32(wh / 2.5));

                p.Width = Math.Max(1, rtKnob.Width / 32);
                p.StartCap = LineCap.Round;
                p.EndCap = LineCap.Round;
                #region Shadow
                var n = 1;// Theme.ShadowGap;
                e.Graphics.TranslateTransform(n, n);
                p.Color = KnobColor.BrightnessTransmit(Theme.BorderBright); 
                e.Graphics.DrawLine(p, pt1, pt2);
                e.Graphics.TranslateTransform(-n, -n);
                #endregion
                #region Fille
                p.Color = ForeColor;    
                e.Graphics.DrawLine(p, pt1, pt2);
                #endregion

            }
            #endregion
            #region Text
            if (DrawText)
            {
                var txt = Value.ToString(string.IsNullOrWhiteSpace(ValueFormatString) ? "0" : ValueFormatString);
                var sz = e.Graphics.MeasureString(txt, ValueFont);
                var pt = MathTool.GetPointWithAngle(cp, StartAngle - (360 - SweepAngle) / 2, (rtRemarkIn.Width / 2) - Convert.ToInt32(sz.Height / 4));
                var rt = MathTool.MakeRectangle(new Point(Convert.ToInt32(pt.X), Convert.ToInt32(pt.Y)), Convert.ToInt32(sz.Width), Convert.ToInt32(sz.Height));

                //Theme.DrawTextShadow(e.Graphics, null, txt, ValueFont, ForeColor, BackColor, rt, DvContentAlignment.MiddleCenter);
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
            base.OnMouseDown(e);
        }
        #endregion
        #endregion
    }
}
