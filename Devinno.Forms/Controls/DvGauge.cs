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
    public class DvGauge : DvControl
    {
        #region Properties
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
        #region EmptyColor
        private Color? cEmptyColor = null;
        public Color? EmptyColor
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
        #region Gradient
        private bool bGradient = true;
        public bool Gradient
        {
            get => bGradient;
            set
            {
                if (bGradient != value)
                {
                    bGradient = value;
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
        #endregion

        #region Member Variable
        Bitmap bmMask;
        int maskW, maskH;
        #endregion

        #region Constructor
        public DvGauge()
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
            var FillColor = this.FillColor ?? Theme.PointColor;
            var EmptyColor = this.EmptyColor ?? Theme.ConcaveBoxColor;
            var EmptyBorderColor = Theme.GetBorderColor(EmptyColor, BackColor);
            var FillBorderColor = Theme.GetBorderColor(FillColor, BackColor);
            var Corner = Theme.Corner;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            Pen p = new Pen(Color.Black);
            SolidBrush br = new SolidBrush(Color.Black);
            #endregion

            Areas((rtContent, rtCircleOut, rtCircleIn, rtCircleRmkS, rtCircleRmkL, rtCircleRmkT, rtText, rtUnit) =>
            {
                #region Make Mask
                if (maskW != Width || maskH != Height || bmMask == null)
                {
                    var w = Convert.ToInt32(rtCircleOut.Width);
                    var h = Convert.ToInt32(rtCircleOut.Height);

                    if (bmMask != null) bmMask.Dispose();
                    bmMask = new Bitmap(w, h);
                    using (var g = Graphics.FromImage(bmMask))
                        g.DrawImage(ResourceTool.circlegrad, new Rectangle(0, 0, w, h));

                    maskW = Width;
                    maskH = Height;
                }
                #endregion

                #region Draw
                {
                    var cp = MathTool.CenterPoint(rtContent);
                    var sp1 = MathTool.GetPointWithAngle(cp, StartAngle, rtCircleOut.Width / 2F);
                    var sp2 = MathTool.GetPointWithAngle(cp, StartAngle + SweepAngle, rtCircleIn.Width / 2F);

                    #region Remark
                    p.Color = ForeColor;
                    p.Width = 2;
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
                    #endregion
                    #region Empty
                    using(var path = new GraphicsPath())
                    {
                        path.AddArc(rtCircleOut, StartAngle, SweepAngle);
                        path.AddArc(rtCircleIn, StartAngle + SweepAngle, -SweepAngle);
                        path.CloseAllFigures();

                        br.Color = EmptyColor;
                        e.Graphics.FillPath(br, path);

                        p.Width = 1;
                        p.Color = EmptyBorderColor;
                        e.Graphics.DrawPath(p, path);
                    }
                    #endregion
                    #region Fill
                    using (var path = new GraphicsPath())
                    {
                        var Ang = Convert.ToSingle(MathTool.Map(Value, Minimum, Maximum, 0, SweepAngle));
                        if (Ang > 0)
                        {
                            path.AddArc(rtCircleOut, StartAngle, Ang);
                            path.AddArc(rtCircleIn, StartAngle + Ang, -Ang);
                            path.CloseAllFigures();

                            #region FIll
                            br.Color = FillColor;
                            e.Graphics.FillPath(br, path);
                            #endregion
                            #region Gradient
                            if (Gradient)
                            {
                                e.Graphics.SetClip(path);
                                if (bmMask != null) e.Graphics.DrawImage(bmMask, rtCircleOut);
                                e.Graphics.ResetClip();
                            }
                            #endregion
                            #region Bevel
                            e.Graphics.SetClip(path);
                            using (var lgbr = new LinearGradientBrush(rtCircleOut, Color.FromArgb(60, Color.White), Color.FromArgb(0, Color.White), 60))
                            {
                                using (var p2 = new Pen(lgbr, 5))
                                {
                                    e.Graphics.DrawArc(p2, rtCircleOut, StartAngle, Ang);
                                }
                            }

                            using (var lgbr = new LinearGradientBrush(rtCircleIn, Color.FromArgb(30, Color.Black), Color.FromArgb(0, Color.Black), 60))
                            {
                                using (var p2 = new Pen(lgbr, 5))
                                {
                                    e.Graphics.DrawArc(p2, rtCircleIn, StartAngle, Ang);
                                }
                            }
                            e.Graphics.ResetClip();
                            #endregion
                            #region Border
                            p.Width = 1;
                            p.Color = EmptyBorderColor;
                            e.Graphics.DrawPath(p, path);
                            #endregion
                        }
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
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();

            var whOut = Convert.ToInt32(Math.Min(rtContent.Width, rtContent.Height));
            var whIn = Convert.ToInt32(whOut * 0.7);
            var whRmkS = Convert.ToInt32(whOut * (0.7 - 0.035 * 1D));
            var whRmkL = Convert.ToInt32(whOut * (0.7 - 0.035 * 2D));
            var whRmkT = Convert.ToInt32(whOut * (0.7 - 0.035 * 4.5D));
            var whUnit = Convert.ToInt32(whOut * UnitDistance);
            var whText = Convert.ToInt32(whOut * TextDistance);

            var rtCircleOut = Util.MakeRectangle(rtContent, new SizeF(whOut, whOut));
            var rtCircleIn = Util.MakeRectangle(rtContent, new SizeF(whIn, whIn));
            var rtCircleRmkL = Util.MakeRectangle(rtContent, new SizeF(whRmkL, whRmkL));
            var rtCircleRmkS = Util.MakeRectangle(rtContent, new SizeF(whRmkS, whRmkS));
            var rtCircleRmkT = Util.MakeRectangle(rtContent, new SizeF(whRmkT, whRmkT));

            var cp = MathTool.CenterPoint(rtContent);
            var sp1 = MathTool.GetPointWithAngle(cp, 90, whUnit / 2F);
            var rtUnit = MathTool.MakeRectangle(sp1, 200, 100);

            var sp2 = MathTool.GetPointWithAngle(cp, 90, whText / 2F);
            var rtText = MathTool.MakeRectangle(sp2, 200, 100);

            /*
            rtCircleOut = Util.INT(rtCircleOut);
            rtCircleIn = Util.INT(rtCircleIn);
            rtCircleRmkS = Util.INT(rtCircleRmkS);
            rtCircleRmkL = Util.INT(rtCircleRmkL);
            rtCircleRmkT = Util.INT(rtCircleRmkT);
            rtText = Util.INT(rtText);
            rtUnit = Util.INT(rtUnit);
            */

            act(rtContent, rtCircleOut, rtCircleIn, rtCircleRmkS, rtCircleRmkL, rtCircleRmkT, rtText, rtUnit);
        }
        #endregion
        #endregion
    }
}
