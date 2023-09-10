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
    public class DvKnob : DvControl
    {
        #region Properties
        #region KnobColor
        private Color? cKnobColor = null;
        public Color? KnobColor
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
        #region CursorDownColor
        private Color? cCursorDownColor = null;
        public Color? CursorDownColor
        {
            get => cCursorDownColor;
            set
            {
                if (cCursorDownColor != value)
                {
                    cCursorDownColor = value;
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

        #region CursorDownState
        public bool CursorDownState { get; private set; }
        #endregion
        #endregion

        #region Member Variable
        Bitmap bmMask;
        int maskW, maskH;

        double DownValue;
        double calcAngle;
        double downAngle;
        Point prev;
        #endregion

        #region Constructor
        public DvKnob()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 150);
        }
        #endregion

        #region Event
        public event EventHandler ValueChanged;
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var KnobColor = this.KnobColor ?? Theme.KnobColor;
            var FillColor = this.FillColor ?? Theme.PointColor;
            var EmptyColor = this.EmptyColor ?? Theme.ConcaveBoxColor;
            var CursorColor = this.EmptyColor ?? Theme.KnobCursorColor;
            var CursorDownColor = this.CursorDownColor ?? Color.Red;
            var BorderColor = Theme.GetBorderColor(EmptyColor, BackColor);
            var Corner = Theme.Corner;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            Pen p = new Pen(Color.Black);
            SolidBrush br = new SolidBrush(Color.Black);
            #endregion

            Areas((rtContent, rtCircleOut, rtCircleIn, rtGauge, rtKnob, rtText, rtUnit) =>
            {
                #region Make Mask
                if (maskW != Width || maskH != Height || bmMask == null)
                {
                    var w = Convert.ToInt32(rtKnob.Width);
                    var h = Convert.ToInt32(rtKnob.Height);

                    if (bmMask != null) bmMask.Dispose();
                    bmMask = new Bitmap(w, h);
                    using (var g = Graphics.FromImage(bmMask))
                        g.DrawImage(ResourceTool.volumemask, new Rectangle(0, 0, w, h));

                    maskW = Width;
                    maskH = Height;
                }
                #endregion

                #region Draw
                var cp = MathTool.CenterPoint(rtContent);
                var sp1 = MathTool.GetPointWithAngle(cp, StartAngle, rtCircleOut.Width / 2F);
                var sp2 = MathTool.GetPointWithAngle(cp, StartAngle + SweepAngle, rtCircleIn.Width / 2F);

                #region Bar
                {
                    var mang = (float)MathTool.Map(Maximum, Minimum, Maximum, 0, Math.Min(SweepAngle, 360));
                    var vang = (float)MathTool.Map(Value, Minimum, Maximum, 0, Math.Min(SweepAngle, 360));
                    var ng = Convert.ToInt32(rtCircleOut.Width * 0.05F);

                    p.Width = ng;
                    p.Color = EmptyColor;
                    e.Graphics.DrawArc(p, rtGauge, StartAngle, mang);
                    p.Color = FillColor;
                    if (vang >= 0.5) e.Graphics.DrawArc(p, rtGauge, StartAngle, vang);
                }
                #endregion
                #region Knob
                {
                    br.Color = KnobColor;

                    e.Graphics.FillEllipse(br, rtKnob);
                    if (bmMask != null) e.Graphics.DrawImage(bmMask, rtKnob);

                    p.Width = 1;
                    p.Color = BorderColor;
                    e.Graphics.DrawEllipse(p, rtKnob);
                }
                #endregion
                #region Cursor
                {
                    var vang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(Value, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle)) + StartAngle;
                    var wh = rtKnob.Width / 2;
                    var pt1 = MathTool.GetPointWithAngle(cp, vang, wh - (wh / 6));
                    var pt2 = MathTool.GetPointWithAngle(cp, vang, wh - Convert.ToInt32(wh / 2.5));

                    using(var path = new GraphicsPath())
                    {
                        var sz = Math.Max(1, rtKnob.Width / 32);
                        var rt1 = MathTool.MakeRectangle(pt1, sz);
                        var rt2 = MathTool.MakeRectangle(pt2, sz);
                        var c = CursorDownState ? CursorDownColor : CursorColor;
                        path.AddArc(rt1, vang - 90, 180);
                        path.AddArc(rt2, vang + 90, 180);
                        path.CloseAllFigures();

                        e.Graphics.TranslateTransform(1, 1);
                        br.Color = Color.FromArgb(Theme.OutShadowAlpha, Color.Black);
                        e.Graphics.FillPath(br, path);
                        e.Graphics.ResetTransform();

                        br.Color = c;
                        e.Graphics.FillPath(br, path);

                        p.Width = 1;
                        p.Color = Theme.GetBorderColor(c, KnobColor);
                        e.Graphics.DrawPath(p, path);
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
            int x = e.X, y = e.Y;

            Areas((rtContent, rtCircleOut, rtCircleIn, rtGauge, rtKnob, rtText, rtUnit) =>
            {
                var cp = MathTool.CenterPoint(rtContent);
                var vang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(Value, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle)) + StartAngle;

                var wh = rtKnob.Width / 2;
                var pt1 = MathTool.GetPointWithAngle(cp, vang, wh - (wh / 6));
                var pt2 = MathTool.GetPointWithAngle(cp, vang, wh - Convert.ToInt32(wh / 2.5));
                var ptc = MathTool.CenterPoint(pt1, pt2);
                var rtCur = MathTool.MakeRectangle(ptc, Convert.ToSingle(MathTool.GetDistance(pt1, pt2) * 2));
                var v = Convert.ToSingle(Math.Abs(wh / 6 - wh / 2.5));
                var ptLoc = e.Location;
                if (CollisionTool.CheckCircle(rtKnob, ptLoc) && CollisionTool.Check(rtCur, ptLoc))
                {
                    CursorDownState = true;
                    calcAngle = 0;
                    downAngle = MathTool.Map(Value, Minimum, Maximum, StartAngle, StartAngle + SweepAngle);
                    prev = ptLoc;
                    DownValue = Value;
                }
                Invalidate();
            });
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;

            Areas((rtContent, rtCircleOut, rtCircleIn, rtGauge, rtKnob, rtText, rtUnit) =>
            {
                var cp = MathTool.CenterPoint(rtContent);
                var ptLoc = e.Location;

                if (CursorDownState)
                {
                    #region Value
                    var pv = MathTool.GetAngle(cp, prev);
                    var nv = MathTool.GetAngle(cp, ptLoc);

                    var v = nv - pv;
                    if (v < -300) v = 360 + v;
                    else if (v > 300) v = v - 360;
                    calcAngle += v;

                    var va = downAngle + calcAngle;
                    if (va > StartAngle + SweepAngle + 360) calcAngle -= 360;
                    else if (va < StartAngle - 360) calcAngle += 360;

                    var cv = MathTool.Map(calcAngle, 0D, SweepAngle, Minimum, Maximum);
                    Value = MathTool.Constrain(DownValue + cv, Minimum, Maximum);
                    if (Tick.HasValue && Tick != 0) Value = Math.Round(Value / Tick.Value) * Tick.Value;
                    #endregion
                    prev = ptLoc;
                }
            });
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;

            Areas((rtContent, rtCircleOut, rtCircleIn, rtGauge, rtKnob, rtText, rtUnit) =>
            {
                var cp = MathTool.CenterPoint(rtContent);
                var ptLoc = e.Location;

                if (SweepAngle > 360)
                {
                    var vang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(Value, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle));
                    var maxpage = Math.Floor(SweepAngle / 360D);
                    var nowpage = Math.Floor((vang + calcAngle) / 360D);
                }

                if (CursorDownState)
                {
                    #region Value
                    var pv = MathTool.GetAngle(cp, prev);
                    var nv = MathTool.GetAngle(cp, ptLoc);

                    var v = nv - pv;
                    if (v < -300) v = 360 + v;
                    else if (v > 300) v = v - 360;
                    calcAngle += v;

                    var cv = MathTool.Map(calcAngle, 0D, SweepAngle, Minimum, Maximum);
                    Value = MathTool.Constrain(DownValue + cv, Minimum, Maximum);
                    if (Tick.HasValue && Tick != 0) Value = Math.Round(Value / Tick.Value) * Tick.Value;
                    #endregion
                    CursorDownState = false;
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

            var whOut = Convert.ToInt32(Math.Min(rtContent.Width, rtContent.Height));
            var whIn = Convert.ToInt32(whOut * 0.7);
            var whGauge = Convert.ToInt32(whOut * 0.95);
            var whKnob = Convert.ToInt32(whOut * 0.9);
            var whUnit = Convert.ToInt32(whOut * UnitDistance);
            var whText = Convert.ToInt32(whOut * TextDistance);

            var rtCircleOut = Util.MakeRectangle(rtContent, new SizeF(whOut, whOut));
            var rtCircleIn = Util.MakeRectangle(rtContent, new SizeF(whIn, whIn));
            var rtGauge = Util.MakeRectangle(rtContent, new SizeF(whGauge, whGauge));
            var rtKnob = Util.MakeRectangle(rtContent, new SizeF(whKnob, whKnob));

            var cp = MathTool.CenterPoint(rtContent);
            var sp1 = MathTool.GetPointWithAngle(cp, 90, whUnit / 2F);
            var rtUnit = MathTool.MakeRectangle(sp1, 200, 100);

            var sp2 = MathTool.GetPointWithAngle(cp, 90, whText / 2F);
            var rtText = MathTool.MakeRectangle(sp2, 200, 100);

            act(rtContent, rtCircleOut, rtCircleIn, rtGauge, rtKnob, rtText, rtUnit);
        }
        #endregion
        #endregion
    }
}
