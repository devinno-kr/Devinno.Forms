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
    public class DvMeter : DvControl
    {
        #region Properties
        #region NeedleColor
        private Color? cNeedleColor = null;
        public Color? NeedleColor
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
        private Color? cNeedlePointColor = null;
        public Color? NeedlePointColor
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
        #region FillColor
        private Color? cFillColor = null;
        public Color? FillColor
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

        #region ValueDraw
        private bool bValueDraw = true;
        public bool ValueDraw
        {
            get => bValueDraw;
            set
            {
                if (bValueDraw != value)
                {
                    bValueDraw = value;
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
        #region Unit
        private string sUnit = null;
        public string Unit
        {
            get => sUnit;
            set
            {
                if (sUnit != value)
                {
                    sUnit = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region UnitDistance
        private float nUnitDistance = 0.7F;
        public float UnitDistance
        {
            get => nUnitDistance;
            set
            {
                if (nUnitDistance != value)
                {
                    nUnitDistance = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TextDistance
        private float nTextDistance = 0.5F;
        public float TextDistance
        {
            get => nTextDistance;
            set
            {
                if (nTextDistance != value)
                {
                    nTextDistance = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region ValueFont
        private Font ftValue = new Font("나눔고딕", 18, FontStyle.Regular);
        public Font ValueFont
        {
            get => ftValue;
            set
            {
                if (ftValue != value)
                {
                    ftValue = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region UnitFont
        private Font ftUnit = new Font("나눔고딕", 7.5F, FontStyle.Regular);
        public Font UnitFont
        {
            get => ftUnit;
            set
            {
                if (ftUnit != value)
                {
                    ftUnit = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region RemarkFont
        private Font ftRemark = new Font("나눔고딕", 7, FontStyle.Regular);
        public Font RemarkFont
        {
            get => ftRemark;
            set
            {
                if (ftRemark != value)
                {
                    ftRemark = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Bars
        public List<MeterBar> Bars { get; private set; } = new List<MeterBar>();
        #endregion
        #endregion

        #region Constructor
        public DvMeter()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, false);
            UpdateStyles();

            TabStop = false;
            #endregion

            Size = new Size(150, 150);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var NeedleColor = this.NeedleColor ?? Theme.NeedleColor;
            var NeedlePointColor = this.NeedlePointColor ?? Theme.NeedlePointColor;
            var FillColor = this.FillColor ?? Theme.PointColor;
            var BorderColor = Theme.GetBorderColor(NeedleColor, BackColor);
            var Corner = Theme.Corner;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            Pen p = new Pen(Color.Black);
            SolidBrush br = new SolidBrush(Color.Black);
            #endregion

            Areas((rtContent, rtCircleOut, rtCircleIn, rtCircleRmkS, rtCircleRmkL, rtCircleRmkT, rtCircleND, rtText, rtUnit, rtBar) =>
            {
                var cp = MathTool.CenterPoint(rtContent);

                #region Remark
                {
                    p.Color = ForeColor;
                    p.Width = 2;
                    e.Graphics.DrawArc(p, rtCircleIn, StartAngle, SweepAngle);

                    if (GraduationLarge > 0)
                    {
                        for (double i = Minimum; i <= Maximum; i += GraduationLarge)
                        {
                            var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(i, Minimum, Maximum), Minimum, Maximum, 0D, SweepAngle)) + StartAngle;

                            var p1 = MathTool.GetPointWithAngle(cp, gsang, rtCircleIn.Width / 2F);
                            var p2 = MathTool.GetPointWithAngle(cp, gsang, rtCircleRmkL.Width / 2F);
                            var pT = MathTool.GetPointWithAngle(cp, gsang, rtCircleRmkT.Width / 2F);
                            e.Graphics.DrawLine(p, p1.X, p1.Y, p2.X, p2.Y);

                            var rt = MathTool.MakeRectangle(pT, 30);
                            Theme.DrawText(e.Graphics, i.ToString(), RemarkFont, ForeColor, rt);
                        }
                    }

                    p.Width = 1;
                    if (GraduationSmall > 0)
                    {
                        for (double i = Minimum; i <= Maximum; i += GraduationSmall)
                        {
                            var gsang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(i, Minimum, Maximum), Minimum, Maximum, 0D, SweepAngle)) + StartAngle;

                            var p1 = MathTool.GetPointWithAngle(cp, gsang, rtCircleIn.Width / 2F);
                            var p2 = MathTool.GetPointWithAngle(cp, gsang, rtCircleRmkS.Width / 2F);

                            e.Graphics.DrawLine(p, p1.X, p1.Y, p2.X, p2.Y);
                        }
                    }
                }
                #endregion
                #region Bar
                if (Bars != null && Bars.Count > 0)
                {
                    foreach (var bar in Bars)
                    {
                        var vangMin = Convert.ToSingle(MathTool.Map(MathTool.Constrain(bar.Minimum, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle));
                        var vangMax = Convert.ToSingle(MathTool.Map(MathTool.Constrain(bar.Maximum, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle));

                        p.Color = bar.Color;
                        p.Width = 7;

                        e.Graphics.DrawArc(p, rtBar, vangMin + StartAngle, vangMax - vangMin);
                    }
                }
                #endregion
                #region Needle
                using (var path = new GraphicsPath())
                {
                    #region Path
                    var vang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(Value, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle));
                    var pt = MathTool.GetPointWithAngle(cp, vang + StartAngle, rtCircleND.Width / 2F);

                    var rtS = MathTool.MakeRectangle(pt, 3);
                    var rtL = MathTool.MakeRectangle(cp, rtCircleOut.Width / 12);

                    path.AddArc(rtL, vang + StartAngle + 90, 180);
                    path.AddArc(rtS, vang + StartAngle + 90 + 180, 180);
                    path.CloseAllFigures();
                    #endregion

                    #region Shadow
                    e.Graphics.TranslateTransform(2, 2);
                    br.Color = Color.FromArgb(Theme.OutShadowAlpha, Color.Black);
                    e.Graphics.FillPath(br, path);
                    e.Graphics.ResetTransform();
                    #endregion
                    #region Needle
                    var ngpin = rtCircleOut.Width / 12;
                    var rtF = MathTool.MakeRectangle(pt, cp); rtF.Inflate(ngpin, ngpin);

                    using (var lgbr = new LinearGradientBrush(rtF, NeedleColor, NeedleColor, vang + StartAngle))
                    {
                        var cb = new ColorBlend();
                        cb.Positions = new float[] { 0F, 0.6F, 0.601F, 1F };
                        cb.Colors = new Color[] { NeedleColor, NeedleColor, NeedlePointColor, NeedlePointColor };
                        lgbr.InterpolationColors = cb;

                        e.Graphics.FillPath(lgbr, path);
                    }
                    #endregion
                    #region Border
                    p.Width = 1;
                    p.Color = BorderColor;
                    e.Graphics.DrawPath(p, path);
                    #endregion
                    #region Hole
                    br.Color = BorderColor;
                    e.Graphics.FillEllipse(br,MathTool.MakeRectangle(cp, 5));
                    #endregion
                }
                #endregion
                #region Text
                if (ValueDraw)
                {
                    var s = FormatString != null ? Value.ToString(FormatString) : Value.ToString();
                    Theme.DrawText(e.Graphics, s, ValueFont, ForeColor, rtText);

                    if (!string.IsNullOrWhiteSpace(Unit))
                    {
                        var s2 = Unit;
                        Theme.DrawText(e.Graphics, s2, UnitFont, ForeColor, rtUnit);
                    }
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
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();

            var whOut = Convert.ToInt32(Math.Min(rtContent.Width, rtContent.Height));
            var whIn = Convert.ToInt32(whOut * 0.8);
            var whRmkS = Convert.ToInt32(whOut * (0.8 + 0.035 * 1D));
            var whRmkL = Convert.ToInt32(whOut * (0.8 + 0.035 * 2D));
            var whRmkT = Convert.ToInt32(whOut * (0.8 + 0.035 * 4.5D));
            var whBar = Convert.ToInt32(whOut * 0.70);
            var whNeedle = Convert.ToInt32(whOut * 0.85);
            var whUnit = Convert.ToInt32(whOut * UnitDistance);
            var whText = Convert.ToInt32(whOut * TextDistance);

            var rtCircleOut = Util.MakeRectangle(rtContent, new SizeF(whOut, whOut));
            var rtCircleIn = Util.MakeRectangle(rtContent, new SizeF(whIn, whIn));
            var rtCircleRmkL = Util.MakeRectangle(rtContent, new SizeF(whRmkL, whRmkL));
            var rtCircleRmkS = Util.MakeRectangle(rtContent, new SizeF(whRmkS, whRmkS));
            var rtCircleRmkT = Util.MakeRectangle(rtContent, new SizeF(whRmkT, whRmkT));
            var rtCircleND = Util.MakeRectangle(rtContent, new SizeF(whRmkT, whRmkT));
            var rtBar = Util.MakeRectangle(rtContent, new SizeF(whBar, whBar));

            var cp = MathTool.CenterPoint(rtContent);
            var sp1 = MathTool.GetPointWithAngle(cp, 90, whUnit / 2F);
            var rtUnit = MathTool.MakeRectangle(sp1, 200, 100);

            var sp2 = MathTool.GetPointWithAngle(cp, 90, whText / 2F);
            var rtText = MathTool.MakeRectangle(sp2, 200, 100);

            act(rtContent, rtCircleOut, rtCircleIn, rtCircleRmkS, rtCircleRmkL, rtCircleRmkT, rtCircleND, rtText, rtUnit, rtBar);
        }
        #endregion
        #endregion
    }

    #region MeterBar
    public class MeterBar
    {
        public double Minimum { get; private set; }
        public double Maximum { get; private set; }
        public Color Color { get; private set; }

        public MeterBar(double Minimum, double Maximum, Color Color)
        {
            this.Minimum = Minimum;
            this.Maximum = Maximum;
            this.Color = Color;
        }
    }
    #endregion
}
