using Devinno.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvTrendGraph : DvControl
    {
        #region Properties
        #region GraphBackColor
        private Color? cGraphBackColor = null;
        public Color? GraphBackColor
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
        private Color? cGridColor = null;
        public Color? GridColor
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
        #region ValueBoxColor
        private Color? cValueBoxColor = null;
        public Color? ValueBoxColor
        {
            get => cValueBoxColor;
            set
            {
                if (cValueBoxColor != value)
                {
                    cValueBoxColor = value;
                    Invalidate();
                }
            }
        }
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
        private bool bXAxisGridDraw = true;
        public bool XAxisGridDraw
        {
            get => bXAxisGridDraw;
            set
            {
                if (bXAxisGridDraw != value)
                {
                    bXAxisGridDraw = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region YAxisGridDraw
        private bool bYAxisGridDraw = true;
        public bool YAxisGridDraw
        {
            get => bYAxisGridDraw;
            set
            {
                if (bYAxisGridDraw != value)
                {
                    bYAxisGridDraw = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ValueDraw
        public bool ValueDraw { get; set; } = false;
        #endregion

        #region Scrollable
        /*
        private bool bScrollable = true;
        public bool Scrollable
        {
            get => bScrollable;
            set
            {
                if (bScrollable != value)
                {
                    bScrollable = value;
                    Invalidate();
                }
            }
        }
        */
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

        #region IsStart
        public bool IsStart { get; private set; } = false;
        #endregion
        #region Pause
        private bool bPause = false;
        public bool Pause
        {
            get => bPause;
            set
            {
                if (bPause != value)
                {
                    bPause = value;
                    if (bPause)
                    {
                        pGraphDatas.Clear();
                    }
                    else
                    {
                        if (pGraphDatas.Count > 0)
                        {
                            var last = pGraphDatas.LastOrDefault();
                            if (last != null && GraphDatas.Count == 0) firstAppendTime = last.Time;

                            GraphDatas.AddRange(pGraphDatas);
                            var ar = GraphDatas.ToArray();
                            GraphDatas = ar.Where(x => last.Time - MaximumXScale - TimeSpan.FromMilliseconds(Interval * 2) <= x.Time).ToList();
                            pGraphDatas.Clear();
                        }
                    }
                }
            }
        }
        #endregion

        #region Series
        public List<GraphSeries2> Series { get; } = new List<GraphSeries2>();
        #endregion
        #endregion

        #region Member Variable
        private List<TGV> GraphDatas = new List<TGV>();
        private List<TGV> pGraphDatas = new List<TGV>();

        private Scroll scroll = new Scroll();

        private TimeGraphData value = null;
        private Thread thData, thRefresh;
        private Dictionary<string, PropertyInfo> dicProps = new Dictionary<string, PropertyInfo>();
        private object oLock = new object();
        private DateTime firstAppendTime = DateTime.Now;
        private int mx = 0, my = 0;
        private bool bRightDown = false;
        #endregion

        #region Constructor
        public DvTrendGraph()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 100);

            #region Scroll
            scroll = new Scroll() { TouchMode = true, Direction = ScrollDirection.Horizon };
            scroll.ScrollChanged += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            scroll.ScrollEnded += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            scroll.GetScrollTotal = () =>
            {
                lock (oLock)
                {
                    return GraphDatas.Count > 1 && Series.Count > 0 ? GraphDatas[GraphDatas.Count - 1].Time.Ticks - GraphDatas[0].Time.Ticks : 0L;
                }
            };
            scroll.GetScrollTick = () => XAxisGraduation.Ticks;
            scroll.GetScrollView = () => XScale.Ticks;
            scroll.GetScrollScaleFactor = () =>
            {
                long v = 0;
                Areas((rtContent, rtRemark, rtTimeAxis, rtValueTitle, rtValueAxis, rtGraph, rtScroll, szRemarks) =>
                {
                    v = Convert.ToInt64(XScale.Ticks / (double)rtGraph.Width);
                });
                return v;
            };
            #endregion

            if (!DesignMode)
            {
                #region Thread
                #region thData
                thData = new Thread(() =>
                {
                    while (!IsDisposed)
                    {
                        if (IsStart && IsHandleCreated && value != null)
                        {
                            AddData();
                        }
                        Thread.Sleep(Interval);
                    }
                })
                { IsBackground = true };
                thData.Start();
                #endregion
                #region thRefresh
                thRefresh = new Thread(() =>
                {
                    while (!IsDisposed)
                    {
                        if (IsStart && IsHandleCreated)
                        {
                            if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate()));
                        }
                        Thread.Sleep(Interval / 2);
                    }
                })
                { IsBackground = true };
                thRefresh.Start();
                #endregion
                #endregion
            }
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var GridColor = this.GridColor ?? Theme.GridColor;
            var GraphBackColor = this.GraphBackColor ?? Color.Transparent;
            var ScrollBorderColor = Theme.GetBorderColor(Theme.ScrollBarColor, BackColor);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init 
            var p = new Pen(Color.Black);
            var br = new SolidBrush(Color.Black);
            #endregion

            scroll.TouchMode = Theme.TouchMode;
 
            Areas((rtContent, rtRemark, rtTimeAxis, rtValueTitle, rtValueAxis, rtGraph, rtScroll, szRemarks) =>
            {
                #region var
                var GridColor = this.GridColor ?? Theme.GridColor;
                var GraphBackColor = this.GraphBackColor ?? Color.Transparent;
                #endregion

                lock (oLock)
                {
                    Draw(e.Graphics, Theme,
                         rtContent, rtRemark, rtTimeAxis, rtValueTitle, rtValueAxis, rtGraph, rtScroll, szRemarks,
                         GridColor, GraphBackColor, ForeColor, BackColor,
                         Font,
                         XAxisGraduation, YAxisGraduationCount, XAxisGridDraw, YAxisGridDraw,
                         ValueFormatString, TimeFormatString,
                         XScale, Series.Where(x => x.Visible).ToList(),
                         scroll, true, Theme.TouchMode,
                         GraphDatas,
                         firstAppendTime);
                }

                #region Scroll
                if (scroll.ScrollTotal > scroll.ScrollView)
                {
                    Theme.DrawBox(e.Graphics, rtScroll, Theme.ScrollBarColor, ScrollBorderColor, RoundType.All, Box.FlatBox(true));

                    e.Graphics.SetClip(Util.FromRect(rtScroll.Left, rtScroll.Top + 0, rtScroll.Width, rtScroll.Height - 0));

                    var cCur = Theme.ScrollCursorOffColor;
                    if (scroll.IsScrolling || scroll.IsTouchMoving || scroll.IsTouchScrolling) cCur = Theme.ScrollCursorOnColor;

                    var rtcur = scroll.GetScrollCursorRectR(rtScroll);
                    if (rtcur.HasValue) Theme.DrawBox(e.Graphics, rtcur.Value, cCur, ScrollBorderColor, RoundType.All, Box.FlatBox());

                    e.Graphics.ResetClip();
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
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;

            Areas((rtContent, rtRemark, rtTimeAxis, rtValueTitle, rtValueAxis, rtGraph, rtScroll, szRemarks) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    scroll.MouseDownR(x, y, rtScroll);
                    if (scroll.TouchMode && CollisionTool.Check(rtGraph, x, y)) scroll.TouchDownR(x, y);
                }
                else if (e.Button == MouseButtons.Right) bRightDown = true;

            });

            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Areas((rtContent, rtRemark, rtTimeAxis, rtValueTitle, rtValueAxis, rtGraph, rtScroll, szRemarks) =>
            {
                int x = e.X, y = e.Y;
                mx = x; my = y;
                scroll.MouseMoveR(x, y, rtScroll);
                if (scroll.TouchMode) scroll.TouchMoveR(x, y);
            });
            Invalidate();
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            Areas((rtContent, rtRemark, rtTimeAxis, rtValueTitle, rtValueAxis, rtGraph, rtScroll, szRemarks) =>
            {
                scroll.MouseUpR(x, y);
                if (scroll.TouchMode) scroll.TouchUpR(x, y);

                bRightDown = false;
            });
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        public void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, List<SizeF>> act)
        {
            var rtContent = Util.FromRect(0, 0, Width, Height);
            var rtRemark = new RectangleF();
            var rtTimeAxis = new RectangleF();
            var rtValueTitle = new RectangleF();
            var rtValueAxis = new RectangleF();
            var rtGraph = new RectangleF();
            var rtScroll = new RectangleF();

            using (var g = CreateGraphics())
            {
                #region var
                var szCH = g.MeasureString("H", Font);
                var CH = szCH.Height;
                var GP = 10;
                var SCWH = Scroll.SC_WH;
                #endregion
                #region Min / Max / Remark
                var dic = new Dictionary<string, _ValueAxisBounds_>();
                foreach (var x in Series.Where(x => x.Visible))
                {
                    var vrt = new _ValueAxisBounds_();

                    var sMin = string.IsNullOrWhiteSpace(ValueFormatString) ? x.Minimum.ToString() : x.Minimum.ToString(ValueFormatString);
                    var sMax = string.IsNullOrWhiteSpace(ValueFormatString) ? x.Maximum.ToString() : x.Maximum.ToString(ValueFormatString);
                    var sTxt = x.Alias;

                    vrt.szMin = g.MeasureString(sMin, Font);
                    vrt.szMax = g.MeasureString(sMax, Font);
                    vrt.szAlias = g.MeasureString(sTxt, Font);

                    dic.Add(x.Name, vrt);
                }
                #endregion

                {
                    #region var
                    var rmkW = GP + dic.Sum(x => 10 + 7 + x.Value.szAlias.Width + GP);
                    var rmkH = GP + CH + GP;

                    var frmt = TimeFormatString ?? "yyyy.MM.dd\r\nHH:mm:ss";
                    var sval = DateTime.Now.ToString(frmt);
                    var szTime = g.MeasureString(sval, Font);

                    var nmW = dic.Select(x => Math.Max(Math.Max(x.Value.szMin.Width, x.Value.szMax.Width), x.Value.szAlias.Width) + GP).Sum();
                    var tmH = szTime.Height;
                    #endregion
                    #region bounds
                    var lsr = new List<SizeInfo>();
                    var lsc = new List<SizeInfo>();

                    lsr.Add(new SizeInfo(DvSizeMode.Pixel, CH + GP));
                    lsr.Add(new SizeInfo(DvSizeMode.Pixel, GP / 2));
                    lsr.Add(new SizeInfo(DvSizeMode.Percent, 100));
                    lsr.Add(new SizeInfo(DvSizeMode.Pixel, GP));
                    lsr.Add(new SizeInfo(DvSizeMode.Pixel, tmH));
                    lsr.Add(new SizeInfo(DvSizeMode.Pixel, GP));
                    lsr.Add(new SizeInfo(DvSizeMode.Pixel, SCWH));
                    lsr.Add(new SizeInfo(DvSizeMode.Pixel, GP));
                    lsr.Add(new SizeInfo(DvSizeMode.Pixel, rmkH));

                    lsc.Add(new SizeInfo(DvSizeMode.Pixel, nmW));
                    lsc.Add(new SizeInfo(DvSizeMode.Pixel, GP));
                    lsc.Add(new SizeInfo(DvSizeMode.Percent, 100));
                    lsc.Add(new SizeInfo(DvSizeMode.Pixel, szTime.Width / 2F));

                    var rts = Util.DevideSizeVH(rtContent, lsr, lsc);
                    #endregion
                    #region set
                    rtValueTitle = rts[0, 0];
                    rtValueAxis = rts[2, 0];
                    rtTimeAxis = rts[4, 2];
                    rtGraph = rts[2, 2];
                    rtScroll = rts[6, 2];
                    rtRemark = Util.MakeRectangleAlign(rts[8, 2], new SizeF(rmkW, rts[8, 2].Height - 2), DvContentAlignment.MiddleCenter);
                    var szRemarks = dic.Values.Select(x => x.szAlias).ToList();
                    #endregion

                    act(rtContent, rtRemark, rtTimeAxis, rtValueTitle, rtValueAxis, rtGraph, rtScroll, szRemarks);
                }
            }
        }
        #endregion

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
                        dicProps = props.Where(x => Series.Select(x => x.Name).Contains(x.Name)).ToDictionary(x => x.Name);
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

                    if (Pause)
                    {
                        pGraphDatas.Add(tgv);
                    }
                    else
                    {
                        if (GraphDatas.Count == 0) firstAppendTime = tgv.Time;
                        GraphDatas.Add(tgv);
                        var ar = GraphDatas.ToArray();
                        GraphDatas = ar.Where(x => tgv.Time - MaximumXScale - TimeSpan.FromMilliseconds(Interval * 2) <= x.Time).ToList();
                    }
                }
            }
        }
        #endregion

        #region Draw 
        void Draw(Graphics g, DvTheme thm,
            RectangleF rtContent, RectangleF rtRemark, RectangleF rtTimeAxis, RectangleF rtValueTitle, RectangleF rtValueAxis, RectangleF rtGraph, RectangleF rtScroll, List<SizeF> szRemarks,
            Color gridColor, Color graphBackColor, Color foreColor, Color backColor,
            Font font,
            TimeSpan xAxisGraduation, int yAxisGraduationCount,
            bool xAxisGridDraw, bool yAxisGridDraw, string valueFormatString, string timeFormatString,
            TimeSpan xScale, List<GraphSeries2> series,
            Scroll scroll, bool scrollable, bool touchMode,
            List<TGV> graphDatas, DateTime firstAppendTime)
        {
            #region var
            var scrollBorderColor = thm.GetBorderColor(thm.ScrollBarColor, backColor);
            var seriasBorderColor = thm.GetBorderColor(backColor, backColor);
            var barBorderColor = thm.GetBorderColor(graphBackColor, backColor);
            var valueBoxColor = ValueBoxColor ?? thm.LabelColor;

            var spos = scroll.ScrollPositionWithOffsetR;
            var GP = 10F;

            var br = new SolidBrush(backColor);
            var p = new Pen(gridColor);
            #endregion
          
            #region Draw
            {
                #region GraphBG
                if (backColor != Color.Transparent)
                {
                    br.Color = backColor;
                    g.FillRectangle(br, rtGraph);
                }
                #endregion
                #region Min / Max / Remark
                var dic = new Dictionary<string, _ValueAxisBounds_>();
                foreach (var x in Series)
                {
                    var vrt = new _ValueAxisBounds_();

                    var sMin = string.IsNullOrWhiteSpace(ValueFormatString) ? x.Minimum.ToString() : x.Minimum.ToString(ValueFormatString);
                    var sMax = string.IsNullOrWhiteSpace(ValueFormatString) ? x.Maximum.ToString() : x.Maximum.ToString(ValueFormatString);
                    var sTxt = x.Alias;

                    vrt.szMin = g.MeasureString(sMin, Font);
                    vrt.szMax = g.MeasureString(sMax, Font);
                    vrt.szAlias = g.MeasureString(sTxt, Font);

                    dic.Add(x.Name, vrt);
                }
                #endregion

                #region Remark
                if (series.Count > 0)
                {
                    thm.DrawBox(g, rtRemark, gridColor, gridColor, RoundType.All, BoxStyle.Border, thm.Corner);

                    var ix = GP;
                    for (int i = 0; i < series.Count; i++)
                    {
                        var s = series[i];
                        var sz = szRemarks[i];
                        var rt = Util.FromRect(rtRemark.Left + ix, rtRemark.Top, 10 + 7 + sz.Width + GP, rtRemark.Height);
                        var rticon = Util.FromRect(0, 0, 10, 10);

                        Util.TextIconBounds(g, rt, DvContentAlignment.MiddleLeft,
                            s.Alias, font, 7, rticon.Size, DvTextIconAlignment.LeftRight,
                            (rtico, rtText) =>
                            {
                                var rti = Util.INT(rtico); rti.Offset(0, 1);
                                thm.DrawBox(g, rti, s.SeriesColor, seriasBorderColor, RoundType.Rect, BoxStyle.Fill | BoxStyle.Border, thm.Corner);
                                thm.DrawText(g, s.Alias, font, foreColor, rtText, DvContentAlignment.MiddleLeft);
                            });

                        ix += rt.Width;
                    }
                }
                #endregion
                #region Value Axis
                if (yAxisGraduationCount > 0)
                {
                    for (var i = 0; i <= yAxisGraduationCount; i++)
                    {
                        var y = Convert.ToInt32(MathTool.Map(i, 0, yAxisGraduationCount, rtGraph.Bottom, rtGraph.Top));
                        #region Grid
                        var oo = 0.5f;
                        if (i == 0)
                        {
                            p.Color = gridColor; p.Width = 1;
                            g.DrawLine(p, rtGraph.Left + oo, y + 1 + oo,
                                          rtGraph.Right + oo, y + 1 + oo);
                        }
                        else if (i == yAxisGraduationCount)
                        {
                            p.Color = gridColor; p.Width = 1;
                            g.DrawLine(p, rtGraph.Left + oo, y + oo,
                                          rtGraph.Right + oo, y + oo);
                        }
                        else if (yAxisGridDraw)
                        {
                            p.DashStyle = DashStyle.Custom;
                            p.DashPattern = new float[] { 2, 2 };

                            p.Color = gridColor; p.Width = 1;
                            g.DrawLine(p, rtGraph.Left + oo, y + oo,
                                          rtGraph.Right + oo, y + oo);

                            p.DashStyle = DashStyle.Solid;
                        }
                        #endregion

                        var vx = 0F;
                        foreach (var ser in series)
                        {
                            if (dic.ContainsKey(ser.Name))
                            {
                                var c = ser.SeriesColor;
                                var v = dic[ser.Name];
                                var vw = Math.Max(Math.Max(v.szMin.Width, v.szMax.Width), v.szAlias.Width);
                                var val = MathTool.Map(i, 0, yAxisGraduationCount, ser.Minimum, ser.Maximum);
                                var sval = val.ToString(valueFormatString ?? "0");
                                var sz = g.MeasureString(sval, font);
                                var rt = Util.FromRect(vx, y - ((sz.Height + 2F) / 2F), vw, (sz.Height) + 2F);
                                thm.DrawText(g, sval, font, c, rt, DvContentAlignment.MiddleRight);

                                if (i == 0)
                                {
                                    var rtTitle = Util.FromRect(rt.Left, rtContent.Top, rt.Width, rt.Height);
                                    thm.DrawText(g, ser.Alias, font, c, rtTitle, DvContentAlignment.MiddleRight);
                                }

                                vx += vw + GP;
                            }
                        }
                    }
                }
                #endregion
                #region Time Axis
                if (graphDatas.Count > 0)
                {
                    p.Color = gridColor; p.Width = 1;
                    p.DashStyle = DashStyle.Custom;
                    p.DashPattern = new float[] { 2, 2 };

                    {
                        var st = graphDatas.First().Time;
                        var ed = graphDatas.Last().Time;

                        var ots = TimeSpan.FromTicks((ed.Ticks - firstAppendTime.Ticks) % xAxisGraduation.Ticks);
                        var ox = Convert.ToSingle(MathTool.Map(ots.Ticks, 0, xScale.Ticks, 0, rtGraph.Width));

                        for (DateTime i = ed; i >= st; i -= xAxisGraduation)
                        {
                            var vx = Convert.ToSingle(MathTool.Map(i.Ticks + spos, ed.Ticks, ed.Ticks - xScale.Ticks, rtGraph.Right, rtGraph.Left));
                            var x = vx - ox;
                            var vs = i - ots;
                            var sval = vs.ToString(timeFormatString ?? "yyyy.MM.dd\r\nHH:mm:ss");
                            var sz = g.MeasureString(sval, font);
                            var rt = MathTool.MakeRectangle(new PointF(x, rtTimeAxis.Top + (rtTimeAxis.Height / 2)), (sz.Width) + 2, (sz.Height) + 2);

                            if (x > rtGraph.Left && x < rtGraph.Right)
                            {
                                if (xAxisGridDraw) g.DrawLine(p, x, rtGraph.Top, x, rtGraph.Bottom);

                                thm.DrawText(g, sval, font, gridColor, rt, DvContentAlignment.MiddleCenter);       
                            }
                        }
                    }

                    p.DashStyle = DashStyle.Solid;
                }
                #endregion
                #region Data
                if (series.Count > 0 && graphDatas.Count > 0)
                {
                    var ls = graphDatas.ToList();
                    var st = ls.First().Time;
                    var ed = ls.Last().Time;

                    g.SetClip(rtGraph);
                    foreach (var v in series)
                    {
                        var pts = ls.Select(x => new PointF(Convert.ToSingle(MathTool.Map((double)x.Time.Ticks + (double)spos, ed.Ticks, ed.Ticks - xScale.Ticks, rtGraph.Right, rtGraph.Left)),
                                                            Convert.ToSingle(MathTool.Map(x.Values[v.Name], v.Minimum, v.Maximum, rtGraph.Bottom, rtGraph.Top)))).ToArray();

                        pts = pts.Where(x => x.X >= rtGraph.Left - 10 && x.X <= rtGraph.Right + 10).ToArray();
                        p.Width = 2F;
                        p.Color = v.SeriesColor;

                        if (pts.Length > 1) g.DrawLines(p, pts);
                    }
                    g.ResetClip();
                }
                #endregion
                #region Value
                if (ValueDraw && bRightDown && GraphDatas.Count > 0 && (!scroll.IsScrolling && !scroll.IsTouchMoving && !scroll.IsTouchScrolling))
                {
                    var vmx = MathTool.Constrain(mx, rtGraph.Left, rtGraph.Right);
                    var ed = GraphDatas.Last().Time;
                    var tm = ed - TimeSpan.FromTicks(Convert.ToInt64(spos)) - TimeSpan.FromSeconds(MathTool.Map(MathTool.Constrain(vmx, rtGraph.Left, rtGraph.Right), rtGraph.Left, rtGraph.Right, XScale.TotalSeconds, 0));

                    var v1 = GraphDatas.OrderBy(x => Math.Abs((x.Time - tm).TotalSeconds)).FirstOrDefault();
                    var iv1 = GraphDatas.IndexOf(v1);
                    if (iv1 >= 1 && iv1 <= graphDatas.Count - 2)
                    {
                        #region v1 / v2 
                        var v2 = (v1.Time < tm ? GraphDatas[iv1 + 1] : (GraphDatas[iv1 - 1]));

                        TGV tmp = null;
                        if (v1.Time > v2.Time) { tmp = v1; v1 = v2; v2 = tmp; }

                        var vr = MathTool.Map(tm.Ticks, v1.Time.Ticks, v2.Time.Ticks, 0D, 1D);
                        #endregion

                        if (v1.Values.Count == v2.Values.Count && v1.Values.Count == Series.Count)
                        {
                            #region var
                            var ls = new List<double>();
                            ls.AddRange(v1.Values.Values);
                            ls.AddRange(v2.Values.Values);
                            var szt = g.MeasureString(tm.ToString(timeFormatString ?? "yyyy.MM.dd\r\nHH:mm:ss"), Font);
                            var szn = g.MeasureString(series.OrderByDescending(x => x.Alias.Length).FirstOrDefault().Alias + " : ", Font);
                            var szv = g.MeasureString(string.IsNullOrWhiteSpace(ValueFormatString) ? ls.Select(x => x.ToString("0")).OrderByDescending(x => x.Length).FirstOrDefault() : ls.Select(x => x.ToString(ValueFormatString)).OrderByDescending(x => x.Length).FirstOrDefault(), Font);
                            var w = Convert.ToSingle(Math.Max(szt.Width + 20, szn.Width + 10 + szv.Width + 20)) + 10;
                            var sercnt = Series.Where(x => x.Visible).Count();
                            var h = Convert.ToSingle(((szt.Height + 4) * (sercnt + 1)) + ((10 * sercnt + 2)));
                            var cx = Convert.ToSingle(MathTool.Constrain(vmx, rtGraph.Left + w / 2D, rtGraph.Right - w / 2D));
                            var cy = Convert.ToSingle(MathTool.Constrain(my, rtGraph.Top + h / 2D, rtGraph.Bottom - h / 2D));
                            var rt = Util.INT(MathTool.MakeRectangle(new PointF(cx, cy), w / 2F, h / 2F));
                            var rtv = MathTool.MakeRectangle(rt, new Size(Convert.ToInt32(rt.Width) - 20, Convert.ToInt32(rt.Height) - 20));

                            var lsr = new List<SizeInfo>();
                            var lsc = new List<SizeInfo>();
                            var vrh = 100F / Convert.ToSingle(1 + sercnt);
                            lsc.Add(new SizeInfo(DvSizeMode.Pixel, szn.Width + 10));
                            lsc.Add(new SizeInfo(DvSizeMode.Percent, 100));

                            for (int i = 0; i < 1 + series.Count; i++)
                            {
                                lsr.Add(new SizeInfo(DvSizeMode.Percent, vrh));
                                if (i < sercnt) lsr.Add(new SizeInfo(DvSizeMode.Pixel, 10));
                            }

                            var rts = Util.DevideSizeVH(rtv, lsr, lsc);
                            #endregion

                            #region Line
                            p.Width = 1;
                            p.DashStyle = DashStyle.Solid;
                            p.Color = Color.Red;
                            g.DrawLine(p, vmx, rtGraph.Top, vmx, rtGraph.Bottom);
                            #region values
                            var idxs = Series.Where(x => x.Visible).Select(x => Series.IndexOf(x)).ToList();
                            for (int i = 0; i < idxs.Count; i++)
                            {
                                var ser = Series[idxs[i]];
                                var val = MathTool.Map(vr, 0D, 1D, v1.Values[ser.Name], v2.Values[ser.Name]);
                                var y = Convert.ToSingle(MathTool.Map(val, ser.Minimum, ser.Maximum, rtGraph.Bottom, rtGraph.Top));

                                thm.DrawBox(g, MathTool.MakeRectangle(new PointF(vmx, y), 5, 5), ser.SeriesColor.BrightnessTransmit(0.3), thm.GetBorderColor(ser.SeriesColor, graphBackColor), RoundType.Ellipse, BoxStyle.Fill | BoxStyle.Border | BoxStyle.OutShadow);
                            }
                            #endregion
                            #endregion
                            #region Box
                            thm.DrawBox(g, rt, Color.FromArgb(200, valueBoxColor), thm.GetBorderColor(valueBoxColor, graphBackColor), RoundType.All, BoxStyle.Fill | BoxStyle.Border | BoxStyle.OutShadow);
                            #endregion
                            #region Time
                            var rtTime = Util.MergeBounds(rts, 0, 0, 2, 1);
                            thm.DrawText(g, tm.ToString(timeFormatString ?? "yyyy.MM.dd\r\nHH:mm:ss"), Font, foreColor, rtTime);
                            #endregion
                            #region values
                            for (int i = 0; i < idxs.Count; i++)
                            {
                                var ser = Series[i];
                                var val = MathTool.Map(vr, 0D, 1D, v1.Values[ser.Name], v2.Values[ser.Name]);
                                var sVal = string.IsNullOrWhiteSpace(ValueFormatString) ? val.ToString("0") : val.ToString(valueFormatString);
                                var rtName = rts[i * 2 + 2, 0];
                                var rtVal = rts[i * 2 + 2, 1];
                                var c = ser.SeriesColor.BrightnessTransmit(0.3);

                                thm.DrawText(g, ser.Alias , Font, c, rtName, DvContentAlignment.MiddleRight);
                                thm.DrawText(g, sVal, Font, c, rtVal, DvContentAlignment.MiddleCenter);

                            }
                            #endregion
                        }
                    }
                    else if (iv1 == 0 || iv1 == GraphDatas.Count - 1)
                    {
                        #region var
                        var ls = new List<double>();
                        ls.AddRange(v1.Values.Values);
                        var szt = g.MeasureString(tm.ToString(timeFormatString ?? "yyyy.MM.dd\r\nHH:mm:ss"), Font);
                        var szn = g.MeasureString(series.OrderByDescending(x => x.Alias.Length).FirstOrDefault().Alias + " : ", Font);
                        var szv = g.MeasureString(string.IsNullOrWhiteSpace(ValueFormatString) ? ls.Select(x => x.ToString("0")).OrderByDescending(x => x.Length).FirstOrDefault() : ls.Select(x => x.ToString(ValueFormatString)).OrderByDescending(x => x.Length).FirstOrDefault(), Font);
                        var w = Convert.ToSingle(Math.Max(szt.Width + 20, szn.Width + 10 + szv.Width + 20)) + 10;
                        var h = Convert.ToSingle((szt.Height * (Series.Count + 1)) + ((10 * (Series.Count + 2))));
                        var cx = Convert.ToSingle(MathTool.Constrain(vmx, rtGraph.Left + w / 2D, rtGraph.Right - w / 2D));
                        var cy = Convert.ToSingle(MathTool.Constrain(my, rtGraph.Top + h / 2D, rtGraph.Bottom - h / 2D));
                        var rt = Util.INT(MathTool.MakeRectangle(new PointF(cx, cy), w / 2F, h / 2F));
                        var rtv = MathTool.MakeRectangle(rt, new Size(Convert.ToInt32(rt.Width) - 20, Convert.ToInt32(rt.Height) - 20));

                        var lsr = new List<SizeInfo>();
                        var lsc = new List<SizeInfo>();
                        var vrh = 100F / Convert.ToSingle(1 + Series.Count);
                        lsc.Add(new SizeInfo(DvSizeMode.Pixel, szn.Width + 10));
                        lsc.Add(new SizeInfo(DvSizeMode.Percent, 100));

                        for (int i = 0; i < 1 + series.Count; i++)
                        {
                            lsr.Add(new SizeInfo(DvSizeMode.Percent, vrh));
                            if (i < Series.Count) lsr.Add(new SizeInfo(DvSizeMode.Pixel, 10));
                        }

                        var rts = Util.DevideSizeVH(rtv, lsr, lsc);
                        #endregion

                        #region Line
                        p.Width = 1;
                        p.DashStyle = DashStyle.Solid;
                        p.Color = Color.Red;
                        g.DrawLine(p, vmx, rtGraph.Top, vmx, rtGraph.Bottom);
                        #region values
                        var idxs = Series.Where(x => x.Visible).Select(x => Series.IndexOf(x)).ToList();
                        for (int i = 0; i < idxs.Count; i++)
                        {
                            var ser = Series[idxs[i]];
                            var val = v1.Values[ser.Name];
                            var y = Convert.ToSingle(MathTool.Map(val, ser.Minimum, ser.Maximum, rtGraph.Bottom, rtGraph.Top));

                            thm.DrawBox(g, MathTool.MakeRectangle(new PointF(vmx, y), 5, 5), ser.SeriesColor.BrightnessTransmit(0.3), thm.GetBorderColor(ser.SeriesColor, graphBackColor), RoundType.Ellipse, BoxStyle.Fill | BoxStyle.Border | BoxStyle.OutShadow);
                        }
                        #endregion
                        #endregion
                        #region Box
                        thm.DrawBox(g, rt, Color.FromArgb(200, valueBoxColor), thm.GetBorderColor(valueBoxColor, graphBackColor), RoundType.All, BoxStyle.Fill | BoxStyle.Border | BoxStyle.OutShadow);
                        #endregion
                        #region Time
                        var rtTime = Util.MergeBounds(rts, 0, 0, 2, 1);
                        thm.DrawText(g, tm.ToString(timeFormatString ?? "yyyy.MM.dd\r\nHH:mm:ss"), Font, foreColor, rtTime);
                        #endregion
                        #region values
                        for (int i = 0; i < idxs.Count; i++)
                        {
                            var ser = Series[idxs[i]];
                            var val = v1.Values[ser.Name];
                            var sVal = string.IsNullOrWhiteSpace(ValueFormatString) ? val.ToString("0") : val.ToString(valueFormatString);
                            var rtName = rts[i * 2 + 2, 0];
                            var rtVal = rts[i * 2 + 2, 1];
                            var c = ser.SeriesColor.BrightnessTransmit(0.3);
                            thm.DrawText(g, ser.Alias , Font, c, rtName, DvContentAlignment.MiddleRight);
                            thm.DrawText(g, sVal, Font, c, rtVal, DvContentAlignment.MiddleCenter);
                        }
                        #endregion
                    }

                }
                #endregion
            }
            #endregion

            #region Dispose
            p.Dispose();
            br.Dispose();
            #endregion
        }
        #endregion
        #endregion
    }
}
