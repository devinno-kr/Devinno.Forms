using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Thread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;

namespace Devinno.Forms.Controls
{
    public class DvTrendGraph : DvControl
    {
        #region Properties
        #region GraphBackColor
        private Color cGraphBackColor = Color.Transparent;
        public Color GraphBackColor
        {
            get => cGraphBackColor;
            set
            {
                if (cGraphBackColor != value)
                {
                    cGraphBackColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region GridColor
        private Color cGridColor = DvTheme.DefaultTheme.Color4;
        public Color GridColor
        {
            get => cGridColor;
            set
            {
                if (cGridColor != value)
                {
                    cGridColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Series
        public List<GraphSeries2> Series { get; } = new List<GraphSeries2>();
        #endregion

        #region YAxisGraduationCount
        private int nYAxisGraduationCount = 10;
        public int YAxisGraduationCount
        {
            get => nYAxisGraduationCount;
            set
            {
                if (nYAxisGraduationCount != value)
                {
                    nYAxisGraduationCount = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region XAxisGraduation
        private TimeSpan tsXAxisGraduation = new TimeSpan(0, 10, 0);
        public TimeSpan XAxisGraduation
        {
            get => tsXAxisGraduation;
            set
            {
                if (tsXAxisGraduation != value)
                {
                    tsXAxisGraduation = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ValueFormatString
        private string sValueFormatString = null;
        public string ValueFormatString
        {
            get => sValueFormatString;
            set
            {
                if (sValueFormatString != value)
                {
                    sValueFormatString = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TimeFormatString
        private string sTimeFormatString = null;
        public string TimeFormatString
        {
            get => sTimeFormatString;
            set
            {
                if (sTimeFormatString != value)
                {
                    sTimeFormatString = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TouchMode
        public bool TouchMode
        {
            get => scroll.TouchMode;
            set
            {
                if (scroll.TouchMode != value)
                {
                    scroll.TouchMode = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region XScale
        private TimeSpan tsXScale = new TimeSpan(1, 0, 0);
        public TimeSpan XScale
        {
            get => tsXScale;
            set
            {
                if (tsXScale != value)
                {
                    tsXScale = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region MaximumXScale
        private TimeSpan tsMaximumXScale = new TimeSpan(1, 0, 0, 0);
        public TimeSpan MaximumXScale
        {
            get => tsMaximumXScale;
            set
            {
                if (tsMaximumXScale != value)
                {
                    tsMaximumXScale = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region XAxisGridDraw
        bool bXAxisGridDraw = false;
        public bool XAxisGridDraw { get => bXAxisGridDraw; set { if (bXAxisGridDraw != value) { bXAxisGridDraw = value; Invalidate(); } } }
        #endregion
        #region YAxisGridDraw
        bool bYAxisGridDraw = true;
        public bool YAxisGridDraw { get => bYAxisGridDraw; set { if (bYAxisGridDraw != value) { bYAxisGridDraw = value; Invalidate(); } } }
        #endregion

        #region Interval
        private int nInterval = 1000;
        public int Interval
        {
            get => nInterval;
            set
            {
                if (nInterval != value)
                {
                    nInterval = value;
                    Invalidate();
                }
            }
        }
        #endregion

        public bool IsStart { get; private set; } = false;
        #endregion

        #region Member Variable
        private List<TGV> GraphDatas = new List<TGV>();
        private List<TGV> TempGraphDatas = new List<TGV>();

        private Scroll scroll = new Scroll();

        private TimeGraphData value = null;
        private Thread thData, thRefresh;
        private Dictionary<string, PropertyInfo> dicProps = new Dictionary<string, PropertyInfo>();
        private object oLock = new object();
        #endregion

        #region Constructor
        public DvTrendGraph()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new System.Drawing.Size(300, 200);

            #region Scroll
            scroll.Direction = ScrollDirection.Horizon;
            scroll.ScrollChanged += (o, s) => { if (!IsStart) this.Invoke(new Action(() => Invalidate())); };

            scroll.GetScrollTotal = () =>
            {
                lock (oLock)
                {
                    return GraphDatas.Count > 1 && Series.Count > 0 ? GraphDatas[GraphDatas.Count - 1].Time.Ticks - GraphDatas[0].Time.Ticks : 0L;
                }
            };
            scroll.GetScrollTick = () => XAxisGraduation.Ticks;
            scroll.GetScrollView = () => XScale.Ticks;
            scroll.GetScrollScaleFactor = () => Areas.ContainsKey("rtGraph") ? (double)XScale.Ticks / (double)Areas["rtGraph"].Width : 1D;
            #endregion
            #region Thread
            #region Data
            thData = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    if (IsStart && value != null && !DesignMode)
                    {
                        AddData();
                    }
                    Thread.Sleep(Interval);
                }
            }))
            { IsBackground = true };
            thData.Start();
            #endregion
            #region Refresh
            thRefresh = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    if (IsStart && !DesignMode)
                    {
                        this.Invoke(new Action(() => Invalidate()));
                    }
                    Thread.Sleep(10);
                }
            }))
            { IsBackground = true };
            thRefresh.Start();
            #endregion
            #endregion
        }
        #endregion

        #region Override
        #region OnMouseWheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            scroll.MouseWheel(e);
            Invalidate();
            base.OnMouseWheel(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtGraph"))
            {
                scroll.MouseDownR(e, Areas["rtScroll"]);
                if (scroll.TouchMode && CollisionTool.Check(Areas["rtGraph"], e.Location)) scroll.TouchDownR(e);
            }

            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtGraph"))
            {
                scroll.MouseMoveR(e, Areas["rtScroll"]);
                if (scroll.TouchMode) scroll.TouchMoveR(e);
                if (scroll.IsScrolling) Invalidate();
                if (scroll.TouchMode && scroll.IsTouchScrolling) Invalidate();
            }
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtGraph"))
            {
                scroll.MouseUpR(e);
                if (scroll.TouchMode && CollisionTool.Check(Areas["rtGraph"], e.Location)) scroll.TouchUpR(e);
            }
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion

        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var CHSZ = g.MeasureString("H", Font);
            var f = DpiRatio;
            var GP = Convert.ToInt32(6 * f);

            int vx = 0;
            foreach (var x in Series)
            {
                var w = Math.Max(Math.Ceiling(g.MeasureString(string.IsNullOrWhiteSpace(ValueFormatString) ? x.Minimum.ToString("0") : x.Minimum.ToString(ValueFormatString), Font).Width) + 1 + GP,
                                 Math.Ceiling(g.MeasureString(string.IsNullOrWhiteSpace(ValueFormatString) ? x.Maximum.ToString("0") : x.Maximum.ToString(ValueFormatString), Font).Width) + 1 + GP) - GP;
                var vw = Math.Max(Convert.ToInt32(g.MeasureString(x.Alias, Font).Width + 2), Convert.ToInt32(w));
                SetArea(x.Name, new Rectangle(vx, 0, vw, 1));
                vx += vw;
                vx += GP;
            }
            vx -= GP;

            var ValueAxisWidth = Series.Count > 0 ? vx : 0;
            var NameAxisHeight = Convert.ToInt32(GP + (CHSZ.Height * 1.5));
            var RemarkAreaHeight = Convert.ToInt32(GP + (CHSZ.Height * 1.5) + GP);
            var gpTopMargin = Convert.ToInt32(CHSZ.Height + 2) + GP;
            var scwh = Convert.ToInt32(Scroll.SC_WH * f);

            var sval = DateTime.Now.ToString(string.IsNullOrWhiteSpace(TimeFormatString) ? "yyyy.MM.dd\r\nHH:mm:ss" : TimeFormatString);
            var sz = g.MeasureString(sval, Font);

            var rtContent = Areas["rtContent"];
            var rtRemark = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtContent.Bottom - RemarkAreaHeight, rtContent.Width - (ValueAxisWidth + GP) - Convert.ToInt32(sz.Width / 2), RemarkAreaHeight);
            var rtNameAxis = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtRemark.Top - GP - scwh - NameAxisHeight - GP, rtContent.Width - (ValueAxisWidth + GP), NameAxisHeight);
            var rtValueAxis = new Rectangle(rtContent.X, rtContent.Y + gpTopMargin, ValueAxisWidth, rtNameAxis.Top - rtContent.Top - gpTopMargin - GP);
            var rtGraphAl = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtContent.Y + gpTopMargin, rtContent.Width - (ValueAxisWidth + GP), rtValueAxis.Height);
            var rtGraph = new Rectangle(rtGraphAl.Left, rtGraphAl.Top, rtGraphAl.Width - Convert.ToInt32(sz.Width / 2), rtGraphAl.Height);
            var rtScroll = new Rectangle(rtGraph.Left, rtRemark.Top - GP - scwh, rtGraph.Width, scwh);
            rtValueAxis.Height = rtGraphAl.Height = rtGraph.Height;
            rtNameAxis.Y += 2;
            SetArea("rtRemark", rtRemark);
            SetArea("rtNameAxis", rtNameAxis);
            SetArea("rtValueAxis", rtValueAxis);
            SetArea("rtGraphAl", rtGraphAl);
            SetArea("rtGP", new Rectangle(0, 0, GP, GP));
            SetArea("rtGraph", rtGraph);
            SetArea("rtScroll", rtScroll);
            SetArea("rtCHSZ", new Rectangle(0, 0, Convert.ToInt32(Math.Ceiling(CHSZ.Width)), Convert.ToInt32(Math.Ceiling(CHSZ.Height))));
            SetArea("rtIco", new Rectangle(rtValueAxis.Left, rtScroll.Top, rtValueAxis.Width, rtRemark.Bottom - rtScroll.Top));
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var GridColor = UseThemeColor ? Theme.Color4 : this.GridColor;
            var bg = GraphBackColor == Color.Transparent ? BackColor : GraphBackColor;
            #endregion
            #region Set
            var f = DpiRatio;
            var spos = scroll.ScrollPositionWithOffsetR;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            var GraphDatas = new List<TGV>();
            lock (oLock)
            {
                GraphDatas.AddRange(this.GraphDatas);
            }
            #endregion
            #region Init
            var p = new Pen(Color.Black, 2);
            var br = new SolidBrush(Color.Black);

            //var ShadowColor = BackColor.BrightnessTransmit(Theme.OutShadowBright);
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtCHSZ = Areas["rtCHSZ"];
            var rtGP = Areas["rtGP"];
            var rtRemark = Areas["rtRemark"];
            var rtNameAxis = Areas["rtNameAxis"];
            var rtValueAxis = Areas["rtValueAxis"];
            var rtGraphAl = Areas["rtGraphAl"];
            var rtGraph = Areas["rtGraph"];
            var rtScroll = Areas["rtScroll"];
            var rtIco = Areas["rtIco"];
            var GP = rtGP.Width;
            #endregion
            #region Draw
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            #region GraphBG
            if (GraphBackColor != Color.Transparent)
            {
                br.Color = GraphBackColor;
                e.Graphics.FillRectangle(br, rtGraph);
            }
            #endregion
            #region Remark
            if (Series.Count > 0)
            {
                var ls = Series.Select(x => e.Graphics.MeasureString(x.Alias, Font)).ToList();
                var RemarkW = (GP * 2) + ls.Sum(x => Convert.ToInt32(10 * f) + Convert.ToInt32(5 * f) + Convert.ToInt32(x.Width) + (GP * 2) + 2);
                var rtRemarkBox = DrawingTool.MakeRectangleAlign(rtRemark, new Size(RemarkW, rtRemark.Height), DvContentAlignment.MiddleCenter);
                Theme.DrawBox(e.Graphics, BackColor.BrightnessTransmit(0.2), BackColor, rtRemarkBox, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);

                var ix = rtRemarkBox.X + (GP * 2);
                var nwbr = Convert.ToInt32(10 * f);
                var nwgp = Convert.ToInt32(5 * f);
                for (int i = 0; i < Series.Count; i++)
                {
                    var s = Series[i];
                    var sz = ls[i];
                    var rtBR = DrawingTool.MakeRectangleAlign(new Rectangle(ix, rtRemarkBox.Top, nwbr, rtRemarkBox.Height), new Size(nwbr, nwgp), DvContentAlignment.MiddleCenter);
                    ix += rtBR.Width;
                    ix += nwgp;
                    var rtTX = new Rectangle(ix, rtRemarkBox.Y, Convert.ToInt32(ls[i].Width) + 2, rtRemarkBox.Height);
                    ix += rtTX.Width;
                    ix += GP * 2;

                    Theme.DrawBox(e.Graphics, s.SeriesColor, BackColor, rtBR, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
                    Theme.DrawTextShadow(e.Graphics, null, s.Alias, Font, ForeColor, BackColor, rtTX, DvContentAlignment.MiddleLeft);
                }
            }
            #endregion
            #region Value Axis
            if (YAxisGraduationCount > 0)
            {
                for (var i = 0; i <= YAxisGraduationCount; i++)
                {
                    var y = Convert.ToInt32(MathTool.Map(i, 0, YAxisGraduationCount, rtGraph.Bottom, rtGraph.Top));

                    if (i == 0) { p.Color = GridColor; p.Width = 2; p.DashStyle = DashStyle.Solid; e.Graphics.DrawLine(p, rtGraph.Left, y + 2, rtGraph.Right, y + 2); }
                    else if (i == YAxisGraduationCount) { p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Solid; e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y); }
                    else if (YAxisGridDraw) { p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot; e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y); }

                    foreach (var ser in Series)
                    {
                        if (Areas.ContainsKey(ser.Name))
                        {
                            var vrt = Areas[ser.Name];
                            var val = MathTool.Map(i, 0, YAxisGraduationCount, ser.Minimum, ser.Maximum);
                            var sval = string.IsNullOrWhiteSpace(ValueFormatString) ? val.ToString("0") : val.ToString(ValueFormatString);
                            var sz = e.Graphics.MeasureString(sval, Font);
                            var rt = new Rectangle(vrt.Left, y - Convert.ToInt32((sz.Height + 2) / 2), vrt.Width, Convert.ToInt32(sz.Height) + 2);
                            Theme.DrawTextShadow(e.Graphics, null, sval, Font, ser.SeriesColor, BackColor, rt, DvContentAlignment.MiddleRight);

                            if (i == 0)
                            {
                                var rtTitle = new Rectangle(rt.Left, rtContent.Top, rt.Width, rt.Height);
                                Theme.DrawTextShadow(e.Graphics, null, ser.Alias, Font, ser.SeriesColor, BackColor, rtTitle, DvContentAlignment.TopRight);
                            }
                        }
                    }

                }
            }
            #endregion
            #region Time Axis
            if (GraphDatas.Count > 0)
            {
                p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot;

                var st = GraphDatas.First().Time;
                var ed = GraphDatas.Last().Time;
                for (DateTime i = ed; i >= st; i -= XAxisGraduation)
                {
                    var ts = ed - i;
                    var x = MathTool.Map(i.Ticks + spos, ed.Ticks, ed.Ticks - XScale.Ticks, rtGraph.Right, rtGraph.Left);
                    p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot;
                    if (x >= rtGraph.Left && x <= rtGraph.Right)
                    {
                        if (XAxisGridDraw && x > rtGraph.Left && x < rtGraph.Right) e.Graphics.DrawLine(p, x, rtGraph.Top, x, rtGraph.Bottom);

                        var sval = (ts.TotalSeconds == 0 ? "" : "-") + ts.ToString(string.IsNullOrWhiteSpace(TimeFormatString) ? @"d\.hh\:mm\:ss\.fff" : TimeFormatString);
                        var sz = e.Graphics.MeasureString(sval, Font);
                        var rt = MathTool.MakeRectangle(new Point(Convert.ToInt32(x), Convert.ToInt32(rtNameAxis.Y + (rtNameAxis.Height / 2))), Convert.ToInt32(sz.Width) + 2, Convert.ToInt32(sz.Height) + 2);
                        Theme.DrawTextShadow(e.Graphics, null, sval, Font, GridColor, BackColor, rt, DvContentAlignment.MiddleCenter);
                    }
                }
            }
            #endregion
            #region Data
            if (Series.Count > 0 && GraphDatas.Count > 0)
            {
                var st = GraphDatas.First().Time;
                var ed = GraphDatas.Last().Time;

                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.SetClip(rtGraph);
                foreach (var v in Series)
                {
                    var pts = GraphDatas.Select(x => new PointF(Convert.ToSingle(MathTool.Map((double)x.Time.Ticks + (double)spos, ed.Ticks, ed.Ticks - tsXScale.Ticks, rtGraph.Right, rtGraph.Left)),
                                                               Convert.ToSingle(MathTool.Map(x.Values[v.Name], v.Minimum, v.Maximum, rtGraph.Bottom, rtGraph.Top)))).ToArray();

                    pts = pts.Where(x => x.X >= rtGraph.Left - 10 && x.X <= rtGraph.Right + 10).ToArray();
                    p.Width = 2;
                    p.DashStyle = DashStyle.Solid;
                    p.Color = v.SeriesColor;
                    if (pts.Length > 1) e.Graphics.DrawLines(p, pts);
                }
                e.Graphics.ResetClip();
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            }
            #endregion
            #region Icon
            {
            }
            #endregion
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            #region Scroll
            if (MaximumXScale > XScale)
            {
                Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScroll, RoundType.ALL);
                Theme.DrawBorder(e.Graphics, BackColor.BrightnessTransmit(-Theme.BorderBright), BackColor, 1, rtScroll, RoundType.ALL, BoxDrawOption.BORDER);

                e.Graphics.SetClip(new Rectangle(rtScroll.X + 6, rtScroll.Y + 0, rtScroll.Width - 12, rtScroll.Height - 0));

                var cCur = Theme.ScrollCursorColor;
                if (scroll.IsScrolling) cCur = Theme.ScrollCursorColor.BrightnessTransmit(0.3);
                else if (scroll.IsTouchMoving) cCur = Theme.PointColor.BrightnessTransmit(0.3);

                var rtcur = scroll.GetScrollCursorRectR(rtScroll);
                if (rtcur.HasValue) Theme.DrawBox(e.Graphics, cCur, Theme.ScrollBarColor, rtcur.Value, RoundType.ALL, BoxDrawOption.BORDER);

                e.Graphics.ResetClip();
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

        #region Method
        #region Start
        public void Start<T>(T value) where T : TimeGraphData
        {
            if (value != null)
            {
                if (Series.Count > 0)
                {
                    var pls = typeof(T).GetProperties();
                    var props = typeof(T).GetProperties().Where(x => x.PropertyType == typeof(double) || x.PropertyType == typeof(float) || x.PropertyType == typeof(decimal) ||
                                                                     x.PropertyType == typeof(byte) || x.PropertyType == typeof(sbyte) ||
                                                                     x.PropertyType == typeof(short) || x.PropertyType == typeof(ushort) ||
                                                                     x.PropertyType == typeof(int) || x.PropertyType == typeof(uint) ||
                                                                     x.PropertyType == typeof(long) || x.PropertyType == typeof(ulong));

                    var nmls = props.Select(x => x.Name).ToList();
                    int nCnt = Series.Where(x => nmls.Contains(x.Name)).Count();
                    if (nCnt == Series.Count)
                    {
                        dicProps = props.ToDictionary(x => x.Name);
                        GraphDatas.Clear();
                        this.value = value;
                        IsStart = true;
                    }
                    else throw new Exception("잘못된 데이터 입니다.");
                }
                else throw new Exception("GraphSeries는 최소 1개 이상이어야 합니다.");
            }
            else throw new Exception("Data가 Null 일 수 없습니다.");
        }
        #endregion
        #region Stop
        public void Stop()
        {
            IsStart = false;
        }
        #endregion
        #region SetData
        public void SetData<T>(T Data) where T : TimeGraphData
        {
            if (IsStart && this.value.GetType() == typeof(T))
                this.value = Data;
        }
        #endregion
        #region AddData
        void AddData()
        {
            if (value != null)
            {
                lock (oLock)
                {
                    var tgv = new TGV() { Time = DateTime.Now };

                    foreach (var vk in dicProps.Keys) tgv.Values.Add(vk, (double)dicProps[vk].GetValue(value));

                    GraphDatas.Add(tgv);
                    var ar = GraphDatas.ToArray();
                    GraphDatas = ar.Where(x => tgv.Time - MaximumXScale - TimeSpan.FromMilliseconds(Interval * 2) <= x.Time).ToList();
                }
            }
        }
        #endregion
        #endregion
    }
}
