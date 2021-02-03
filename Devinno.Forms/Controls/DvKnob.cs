using Devinno.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Color cKnobColor = DvTheme.DefaultTheme.Color1;
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
        private Color cEmptyColor = DvTheme.DefaultTheme.Color0;
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
        #region CursorColor
        private Color cCursorColor = Color.White;
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
        #region CursorDownColor
        private Color cCursorDownColor = Color.Red;
        public Color CursorDownColor
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

        #region CursorDownState
        public bool CursorDownState { get; private set; }
        #endregion
        #endregion

        #region Event
        public event EventHandler ValueChanged;
        #endregion

        #region Member Variable
        double DownValue;
        double calcAngle;
        Point prev;
        Bitmap bm;
        #endregion

        #region Constructor
        public DvKnob()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion
            #region Size
            this.Size = new Size(300, 300);
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
            var ng = wh / 20;

            var rtCircle = DrawingTool.MakeRectangleAlign(rtContent, new Size(wh, wh), DvContentAlignment.MiddleCenter);
            SetArea("rtCircle", rtCircle);

            var rtGauge = new Rectangle(rtCircle.X, rtCircle.Y, rtCircle.Width, rtCircle.Height); rtGauge.Inflate(-ng, -ng);
            SetArea("rtGauge", rtGauge);

            var rtKnob = new Rectangle(rtGauge.X, rtGauge.Y, rtGauge.Width, rtGauge.Height); rtKnob.Inflate(-ng / 2, -ng / 2);
            SetArea("rtKnob", rtKnob);

            var rtKnobIn = new Rectangle(rtKnob.X, rtKnob.Y, rtKnob.Width, rtKnob.Height); rtKnobIn.Inflate(-Convert.ToInt32(rtKnob.Width / 3), -Convert.ToInt32(rtKnob.Width / 3));
            SetArea("rtKnobIn", rtKnobIn);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var KnobColor = UseThemeColor ? Theme.Color1 : this.KnobColor;
            var FillColor = UseThemeColor ? Theme.PointColor : this.FillColor;
            var EmptyColor = UseThemeColor ? Theme.Color0 : this.FillColor;
            var KnobBorderColor = KnobColor.BrightnessTransmit(0.75);
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtCircle = Areas["rtCircle"];
            var rtGauge = Areas["rtGauge"];
            var rtKnob = Areas["rtKnob"];
            var rtKnobIn = Areas["rtKnobIn"];

            var cp = MathTool.CenterPoint(rtCircle);
            #endregion
            #region Buf
            if (bm == null || (bm != null && (bm.Width != rtKnob.Width || bm.Width != rtKnob.Width)))
            {
                if (bm != null) bm.Dispose();
                bm = new Bitmap(rtKnob.Width, rtKnob.Height);
                using (var g = Graphics.FromImage(bm))
                {
                    g.DrawImage(ResourceTool.volumemask, 0, 0, rtKnob.Width, rtKnob.Height);
                }
            }
            #endregion
            #region Init
            var p = new Pen(KnobColor, 2);
            var br = new SolidBrush(KnobColor);
            #endregion
            #region Draw
            e.Graphics.Clear(BackColor);

            #region Remark
            var ng = rtCircle.Width / 20;
            p.Width = ng;

            p.Color = EmptyColor;
            e.Graphics.DrawArc(p, rtGauge, StartAngle, (float)MathTool.Map(Maximum, Minimum, Maximum, 0, Math.Min(SweepAngle, 360)));

            p.Color = FillColor;
            e.Graphics.DrawArc(p, rtGauge, StartAngle, (float)MathTool.Map(Value, Minimum, Maximum, 0, Math.Min(SweepAngle, 360)));
            #endregion
            #region Knob
            #region Shadow
            e.Graphics.TranslateTransform(Theme.ShadowGap, Theme.ShadowGap);
            br.Color = BackColor.BrightnessTransmit(Theme.OutShadowBright);
            e.Graphics.FillEllipse(br, rtKnob);
            e.Graphics.TranslateTransform(-Theme.ShadowGap, -Theme.ShadowGap);
            #endregion
            #region Fill
            br.Color = KnobColor;
            e.Graphics.FillEllipse(br, rtKnob);

            var nk = rtKnob.Width / 50;
            var rtKnobBorder = new Rectangle(rtKnob.X, rtKnob.Y, rtKnob.Width, rtKnob.Height); rtKnobBorder.Inflate(-nk, -nk);
            p.Width = nk * 2;
            p.Color = KnobBorderColor;
            e.Graphics.DrawEllipse(p, rtKnobBorder);
            #endregion
            #region Mask
            e.Graphics.DrawImage(bm, rtKnob);
            #endregion
            #region Border
            p.Width = 1;
            p.Color = BackColor.BrightnessTransmit(Theme.BorderBright);
            e.Graphics.DrawEllipse(p, rtKnob);
            #endregion
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
                #region Fill
                p.Color = CursorDownState ? CursorDownColor : CursorColor;
                e.Graphics.DrawLine(p, pt1, pt2);
                #endregion
            }
            #endregion
            #region Text
            if (DrawText)
            {
                var txt = Value.ToString(string.IsNullOrWhiteSpace(ValueFormatString) ? "0" : ValueFormatString);
                Theme.DrawTextShadow(e.Graphics, null, txt, ValueFont, ForeColor, KnobColor, rtCircle, DvContentAlignment.MiddleCenter);
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
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtCircle = Areas["rtCircle"];
            var rtKnob = Areas["rtKnob"];
            var rtKnobIn = Areas["rtKnobIn"];

            var cp = MathTool.CenterPoint(rtCircle);
            #endregion
            #region Cursor
            var vang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(Value, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle)) + StartAngle;
            var wh = rtKnob.Width / 2;
            var pt1 = MathTool.GetPointWithAngle(cp, vang, wh - (wh / 6));
            var pt2 = MathTool.GetPointWithAngle(cp, vang, wh - Convert.ToInt32(wh / 2.5));
            var v = Convert.ToSingle(Math.Abs(wh / 6 - wh / 2.5));
            //if(CollisionTool.CheckLine(pt1, pt2, e.Location, v/2) && CollisionTool.CheckCircle(rtKnob, e.Location))
            if(CollisionTool.CheckCircle(rtKnob, e.Location))
            {
                CursorDownState = true;
                calcAngle = 0;
                prev = e.Location;
                DownValue = Value; 
                Invalidate();
            }
            #endregion
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Areas.Count > 0)
            {
                #region Bounds
                var rtContent = Areas["rtContent"];
                var rtCircle = Areas["rtCircle"];
                var rtKnob = Areas["rtKnob"];
                var rtKnobIn = Areas["rtKnobIn"];

                var cp = MathTool.CenterPoint(rtCircle);

                if (SweepAngle > 360)
                {
                    var vang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(Value, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle));
                    var maxpage = Math.Floor(SweepAngle / 360D);
                    var nowpage = Math.Floor((vang + calcAngle) / 360D);
                }
                #endregion

                if (CursorDownState)
                {
                    #region Value
                    var pv = MathTool.GetAngle(cp, prev);
                    var nv = MathTool.GetAngle(cp, e.Location);

                    var v = nv - pv;
                    if (v < -300) v = 360 + v;
                    else if (v > 300) v = v - 360;
                    calcAngle += v;

                    var cv = MathTool.Map(calcAngle, 0D, SweepAngle, Minimum, Maximum);
                    Value = MathTool.Constrain(DownValue + cv, Minimum, Maximum);
                    #endregion
                    CursorDownState = false;
                    Invalidate();
                }
                Invalidate();
            }
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Areas.Count > 0)
            {
                #region Bounds
                var rtContent = Areas["rtContent"];
                var rtCircle = Areas["rtCircle"];
                var rtKnob = Areas["rtKnob"];
                var rtKnobIn = Areas["rtKnobIn"];

                var cp = MathTool.CenterPoint(rtCircle);
                       
                if (SweepAngle > 360)
                {
                    var vang = Convert.ToSingle(MathTool.Map(MathTool.Constrain(Value, Minimum, Maximum), Minimum, Maximum, 0, SweepAngle));
                    var maxpage = Math.Floor(SweepAngle / 360D);
                    var nowpage = Math.Floor((vang + calcAngle) / 360D);
                }
                #endregion

                if (CursorDownState)
                {
                    #region Value
                    var pv = MathTool.GetAngle(cp, prev);
                    var nv = MathTool.GetAngle(cp, e.Location);

                    var v = nv - pv;
                    if (v < -300) v = 360 + v;
                    else if (v > 300) v = v - 360;
                    calcAngle += v;

                    var cv = MathTool.Map(calcAngle, 0D, SweepAngle, Minimum, Maximum);
                    Value = MathTool.Constrain(DownValue + cv, Minimum, Maximum);
                    #endregion
                    prev = e.Location;
                }
            }
            base.OnMouseMove(e);
        }
        #endregion
        #endregion

        #region Method
        #endregion
    }
}
