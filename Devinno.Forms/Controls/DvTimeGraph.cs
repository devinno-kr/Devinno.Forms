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
    public class DvTimeGraph : DvControl
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

        #region Series
        public List<GraphSeries2> Series { get; } = new List<GraphSeries2>();
        #endregion
        #endregion

        #region Member Variable
        private List<TGV> GraphDatas = new List<TGV>();
        private Scroll scroll = new Scroll();
        #endregion

        #region Constructor
        public DvTimeGraph()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 100);

            scroll = new Scroll() { TouchMode = true, Direction = ScrollDirection.Horizon };
            scroll.ScrollChanged += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            scroll.ScrollEnded += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            scroll.GetScrollTotal = () => GraphDatas.Count > 1 && Series.Count > 0 ? (new DateTime(Convert.ToInt64(Math.Ceiling(Convert.ToDouble(GraphDatas.Last().Time.Ticks) / Convert.ToDouble(XAxisGraduation.Ticks))) * XAxisGraduation.Ticks) - new DateTime(GraphDatas.First().Time.Ticks / XAxisGraduation.Ticks * XAxisGraduation.Ticks)).Ticks : 0L;
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

                Draw(e.Graphics, Theme,
                        rtContent, rtRemark, rtTimeAxis, rtValueTitle, rtValueAxis, rtGraph, rtScroll, szRemarks,
                        GridColor, GraphBackColor, ForeColor, BackColor,
                        Font,
                        XAxisGraduation, YAxisGraduationCount, XAxisGridDraw, YAxisGridDraw,
                        ValueFormatString, TimeFormatString,
                        XScale, Series,
                        scroll, true, Theme.TouchMode,
                        GraphDatas);

                #region Scroll
                if (scroll.ScrollTotal > scroll.ScrollView)
                {
                    Theme.DrawBox(e.Graphics, rtScroll, Theme.ScrollBarColor, ScrollBorderColor, RoundType.All, Box.FlatBox(true));

                    e.Graphics.SetClip(Util.FromRect(rtScroll.Left, rtScroll.Top + 0, rtScroll.Width, rtScroll.Height - 0));

                    var cCur = Theme.ScrollCursorOffColor;
                    if (scroll.IsScrolling || scroll.IsTouchMoving || scroll.IsTouchScrolling) cCur = Theme.ScrollCursorOnColor;

                    var rtcur = scroll.GetScrollCursorRect(rtScroll);
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

            scroll.TouchMode = GetTheme()?.TouchMode ?? false;

            Areas((rtContent, rtRemark, rtTimeAxis, rtValueTitle, rtValueAxis, rtGraph, rtScroll, szRemarks) =>
            {
                scroll.MouseDown(x, y, rtScroll);
                if (scroll.TouchMode && CollisionTool.Check(rtGraph, x, y)) scroll.TouchDown(x, y);
            });
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            Areas((rtContent, rtRemark, rtTimeAxis, rtValueTitle, rtValueAxis, rtGraph, rtScroll, szRemarks) =>
            {
                scroll.MouseMove(x, y, rtScroll);
                if (scroll.TouchMode) scroll.TouchMove(x, y);
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
                scroll.MouseUp(x, y);
                if (scroll.TouchMode) scroll.TouchUp(x, y);
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

        #region SetDataSource
        public void SetDataSource<T>(IEnumerable<T> values) where T : TimeGraphData
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
                    var dic = props.ToDictionary(x => x.Name);

                    GraphDatas.Clear();
                    foreach (var v in values)
                    {
                        var tgv = new TGV() { Time = v.Time };

                        foreach (var vk in dic.Keys) tgv.Values.Add(vk, (double)dic[vk].GetValue(v));

                        GraphDatas.Add(tgv);
                    }

                    Invalidate();
                }
                else throw new Exception("잘못된 데이터 입니다.");
            }
            else throw new Exception("GraphSeries는 최소 1개 이상이어야 합니다.");
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
            List<TGV> graphDatas)
        {
            #region var
            var scrollBorderColor = thm.GetBorderColor(thm.ScrollBarColor, backColor);
            var seriasBorderColor = thm.GetBorderColor(backColor, backColor);
            var barBorderColor = thm.GetBorderColor(graphBackColor, backColor);

            var spos = scroll.ScrollPositionWithOffset;
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
                        var st = new DateTime(graphDatas.First().Time.Ticks / xAxisGraduation.Ticks * xAxisGraduation.Ticks);
                        var ed = new DateTime(Convert.ToInt64(Math.Ceiling(Convert.ToDouble(graphDatas.Last().Time.Ticks) / Convert.ToDouble(xAxisGraduation.Ticks))) * xAxisGraduation.Ticks);

                        for (DateTime i = st; i <= ed; i += xAxisGraduation)
                        {
                            var x = Convert.ToSingle(MathTool.Map(i.Ticks + spos, st.Ticks, st.Ticks + xScale.Ticks, rtGraph.Left, rtGraph.Right));
                            var sval = i.ToString(timeFormatString ?? "yyyy.MM.dd\r\nHH:mm:ss");
                            var sz = g.MeasureString(sval, font);
                            var rt = MathTool.MakeRectangle(new PointF(x, rtTimeAxis.Top + (rtTimeAxis.Height / 2)), (sz.Width) + 2, (sz.Height) + 2);

                            if (x >= rtGraph.Left && x <= rtGraph.Right)
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
                    var st = new DateTime(graphDatas.First().Time.Ticks / xAxisGraduation.Ticks * xAxisGraduation.Ticks);
                    var ed = new DateTime(Convert.ToInt64(Math.Ceiling(Convert.ToDouble(graphDatas.Last().Time.Ticks) / Convert.ToDouble(xAxisGraduation.Ticks))) * xAxisGraduation.Ticks);

                    g.SetClip(rtGraph);
                    foreach (var v in series)
                    {
                        var pts = graphDatas.Select(x => new PointF(Convert.ToSingle(MathTool.Map((double)x.Time.Ticks + (double)spos, st.Ticks, st.Ticks + xScale.Ticks, rtGraph.Left, rtGraph.Right)),
                                                                    Convert.ToSingle(MathTool.Map((double)x.Values[v.Name], v.Minimum, v.Maximum, rtGraph.Bottom, rtGraph.Top)))).ToArray();

                        pts = pts.Where(x => x.X >= rtGraph.Left && x.X <= rtGraph.Right).ToArray();
                        p.Width = 2F;
                        p.Color = v.SeriesColor;

                        if (pts.Length > 1) g.DrawLines(p, pts);
                    }
                    g.ResetClip();
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

    #region class : _ValueAxisBounds_
    class _ValueAxisBounds_
    {
        public SizeF szMax = new SizeF();
        public SizeF szMin = new SizeF();
        public SizeF szAlias = new SizeF();
    }
    #endregion
}
