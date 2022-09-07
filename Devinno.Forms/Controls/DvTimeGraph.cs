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
        #region RemarkColor
        private Color? cRemarkColor = null;
        public Color? RemarkColor
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
        #region TouchMode
        public bool TouchMode
        {
            get => scroll.TouchMode;
            set => scroll.TouchMode = value;
        }
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
            scroll.ScrollChanged += (o, s) => this.Invoke(new Action(() => Invalidate()));
            scroll.ScrollEnded += (o, s) => this.Invoke(new Action(() => Invalidate()));
            scroll.GetScrollTotal = () => GraphDatas.Count > 1 && Series.Count > 0 ? (new DateTime(Convert.ToInt64(Math.Ceiling(Convert.ToDouble(GraphDatas.Last().Time.Ticks) / Convert.ToDouble(XAxisGraduation.Ticks))) * XAxisGraduation.Ticks) - new DateTime(GraphDatas.First().Time.Ticks / XAxisGraduation.Ticks * XAxisGraduation.Ticks)).Ticks : 0L;
            scroll.GetScrollTick = () => XAxisGraduation.Ticks;
            scroll.GetScrollView = () => XScale.Ticks;
            scroll.GetScrollScaleFactor = () =>
            {
                long v = 0;
                Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, dicSer, GP, CH) =>
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
            var RemarkColor = this.RemarkColor ?? Theme.ButtonColor;
            var GraphBackColor = this.GraphBackColor ?? Color.Transparent;
            var RemarkBorderColor = Theme.GetBorderColor(RemarkColor, BackColor);
            var ScrollBorderColor = Theme.GetBorderColor(Theme.ScrollBarColor, BackColor);
            
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init 
            var p = new Pen(Color.Black);
            var br = new SolidBrush(Color.Black);
            #endregion

            Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, dicSer, GP, CH) =>
            {
                var spos = scroll.ScrollPositionWithOffset;

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
                    #region var ls;
                    var ls = new List<SizeF>();
                    foreach (var v in Series)
                    {
                        ls.Add(e.Graphics.MeasureString(v.Alias, Font));
                    }
                    #endregion
                    var nwbr = 10;
                    var nwgp = 5;
                    var RemarkW = (GP * 2) + ls.Sum(x => nwbr + nwgp + Convert.ToInt32(x.Width) + (GP * 2) + 2);
                    var rtRemarkBox = Util.MakeRectangleAlign(rtRemark, new SizeF(RemarkW, rtRemark.Height), DvContentAlignment.MiddleCenter);
                    Theme.DrawBox(e.Graphics, Util.INT(rtRemarkBox), RemarkColor, RemarkBorderColor, RoundType.All, Box.ButtonUp_Flat(ShadowGap));

                    var ix = rtRemarkBox.Left + (GP * 2);
                    for (int i = 0; i < Series.Count; i++)
                    {
                        var s = Series[i];
                        var sz = ls[i];
                        var rtBR = Util.MakeRectangleAlign(Util.FromRect(ix, rtRemarkBox.Top, nwbr, rtRemarkBox.Height), new SizeF(nwbr, nwgp), DvContentAlignment.MiddleCenter);
                        ix += rtBR.Width;
                        ix += nwgp;
                        var rtTX = Util.FromRect(ix, rtRemarkBox.Top, Convert.ToInt32(ls[i].Width) + 2, rtRemarkBox.Height);
                        ix += rtTX.Width;
                        ix += GP * 2;

                        var SeriesBorderColor = Theme.GetBorderColor(s.SeriesColor, BackColor);
                        Theme.DrawBox(e.Graphics, Util.INT(rtBR), s.SeriesColor, SeriesBorderColor, RoundType.Rect, Box.ButtonUp_Flat(ShadowGap));
                        Theme.DrawText(e.Graphics, s.Alias, Font, ForeColor, rtTX, DvContentAlignment.MiddleLeft, true);
                    }
                }
                #endregion
                #region Value Axis
                if (YAxisGraduationCount > 0)
                {
                    for (var i = 0; i <= YAxisGraduationCount; i++)
                    {
                        var y = Convert.ToInt32(MathTool.Map(i, 0, YAxisGraduationCount, rtGraph.Bottom, rtGraph.Top));

                        if (i == 0)
                        {
                            p.Color = GridColor;
                            p.Width = 1;
                            p.DashStyle = DashStyle.Solid;
                            e.Graphics.DrawLine(p, rtGraph.Left, y + 1, rtGraph.Right, y + 1);
                        }
                        else if (i == YAxisGraduationCount)
                        {
                            p.Color = GridColor;
                            p.Width = 1;
                            p.DashStyle = DashStyle.Solid;
                            e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y);
                        }
                        else if (YAxisGridDraw)
                        {
                            p.Color = GridColor;
                            p.Width = 1;
                            p.DashStyle = DashStyle.Custom;
                            p.DashPattern = new float[] { 2, 2 };
                            e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y);
                        }

                        foreach (var ser in Series)
                        {
                            if (dicSer.ContainsKey(ser.Name))
                            {
                                var vrt = dicSer[ser.Name];
                                var val = MathTool.Map(i, 0, YAxisGraduationCount, ser.Minimum, ser.Maximum);
                                var sval = string.IsNullOrWhiteSpace(ValueFormatString) ? val.ToString("0") : val.ToString(ValueFormatString);
                                var sz = e.Graphics.MeasureString(sval, Font);
                                var rt = Util.FromRect(vrt.Left, y - ((sz.Height + 2F) / 2F), vrt.Width, (sz.Height) + 2F);
                                Theme.DrawText(e.Graphics, sval, Font, ser.SeriesColor, rt, DvContentAlignment.MiddleRight, true);

                                if (i == 0)
                                {
                                    var rtTitle = Util.FromRect(rt.Left, rtContent.Top, rt.Width, rt.Height);
                                    Theme.DrawText(e.Graphics, ser.Alias, Font, ser.SeriesColor, rtTitle, DvContentAlignment.TopRight, true);
                                }
                            }
                        }

                    }
                }
                #endregion
                #region Time Axis
                if (GraphDatas.Count > 0)
                {
                    p.Color = GridColor; 
                    p.Width = 1;

                    var st = new DateTime(GraphDatas.First().Time.Ticks / XAxisGraduation.Ticks * XAxisGraduation.Ticks);
                    var ed = new DateTime(Convert.ToInt64(Math.Ceiling(Convert.ToDouble(GraphDatas.Last().Time.Ticks) / Convert.ToDouble(XAxisGraduation.Ticks))) * XAxisGraduation.Ticks);

                    for (DateTime i = st; i <= ed; i += XAxisGraduation)
                    {
                        var x = Convert.ToSingle(MathTool.Map(Convert.ToDouble(i.Ticks + spos), st.Ticks, st.Ticks + XScale.Ticks, rtGraph.Left, rtGraph.Right));
                        if (x >= rtGraph.Left && x <= rtGraph.Right)
                        {
                            if (XAxisGridDraw && x > rtGraph.Left && x < rtGraph.Right)
                            {
                                p.Color = GridColor;
                                p.Width = 1;
                                p.DashStyle = DashStyle.Custom;
                                p.DashPattern = new float[] { 2, 2 };

                                e.Graphics.DrawLine(p, x, rtGraph.Top, x, rtGraph.Bottom);
                            }

                            var sval = i.ToString(string.IsNullOrWhiteSpace(TimeFormatString) ? "yyyy.MM.dd\r\nHH:mm:ss" : TimeFormatString);
                            var sz = e.Graphics.MeasureString(sval, Font);
                            var rt = MathTool.MakeRectangle(new PointF(x, rtNameAxis.Top + (rtNameAxis.Height / 2)), (sz.Width) + 2, (sz.Height) + 2);
                            Theme.DrawText(e.Graphics, sval, Font, GridColor, rt);
                        }

                    }
                }
                #endregion
                #region Data
                if (Series.Count > 0 && GraphDatas.Count > 0)
                {
                    var st = new DateTime(GraphDatas.First().Time.Ticks / XAxisGraduation.Ticks * XAxisGraduation.Ticks);
                    var ed = new DateTime(Convert.ToInt64(Math.Ceiling(Convert.ToDouble(GraphDatas.Last().Time.Ticks) / Convert.ToDouble(XAxisGraduation.Ticks))) * XAxisGraduation.Ticks);

                    e.Graphics.SetClip(rtGraph);
                    foreach (var v in Series)
                    {
                        var pts = GraphDatas.Select(x => new PointF(Convert.ToSingle(MathTool.Map((double)x.Time.Ticks + (double)spos, st.Ticks, st.Ticks + XScale.Ticks, rtGraph.Left, rtGraph.Right)),
                                                                   Convert.ToSingle(MathTool.Map((double)x.Values[v.Name], v.Minimum, v.Maximum, rtGraph.Bottom, rtGraph.Top)))).ToArray();

                        pts = pts.Where(x => x.X >= rtGraph.Left && x.X <= rtGraph.Right).ToArray();
                        p.Width = 2;
                        p.Color = v.SeriesColor;
                        p.DashStyle = DashStyle.Solid;
                        if (pts.Length > 1) e.Graphics.DrawLines(p, pts);
                    }
                    e.Graphics.ResetClip();
                }
                #endregion
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
            Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, dicSer, GP, CH) =>
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
            Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, dicSer, GP, CH) =>
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
            Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, dicSer, GP, CH) =>
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
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, Dictionary<string, RectangleF>, float, float> act)
        {
            using (var g = CreateGraphics())
            {
                var CH = g.MeasureString("H", Font).Height;
                var GP = 5;
                #region var dicSer;
                var vx = 0F;
                var dicSer = new Dictionary<string, RectangleF>();
                foreach (var x in Series)
                {
                    var sMin = string.IsNullOrWhiteSpace(ValueFormatString) ? x.Minimum.ToString("0") : x.Minimum.ToString(ValueFormatString);
                    var sMax = string.IsNullOrWhiteSpace(ValueFormatString) ? x.Maximum.ToString("0") : x.Maximum.ToString(ValueFormatString);
                    var sTxt = x.Alias;

                    var szMin = g.MeasureString(sMin, Font);
                    var szMax = g.MeasureString(sMax, Font);
                    var szTxt = g.MeasureString(sTxt, Font);

                    var w = Math.Max(Math.Ceiling(szMax.Width) + 1 + GP, Math.Ceiling(szMin.Width) + 1 + GP) - GP;

                    var vw = Math.Max(Convert.ToInt32(szTxt.Width + 2), Convert.ToInt32(w));
                    dicSer.Add(x.Name, Util.FromRect(vx, 0, vw, 1));
                    vx += vw;
                    vx += GP;
                }
                vx -= GP;
                #endregion

                var sz = g.MeasureString(DateTime.Now.ToString(string.IsNullOrWhiteSpace(TimeFormatString) ? "yyyy.MM.dd\r\nHH:mm:ss" : TimeFormatString), Font);
                var ValueAxisWidth = Series.Count > 0 ? vx : 0;
                var NameAxisHeight = (GP + sz.Height);
                var RemarkAreaHeight = (GP + (CH * 1.5F) + GP);
                var gpTopMargin = (CH + 2) + GP;
                var scwh = Scroll.SC_WH;


                var rtContent = GetContentBounds();
                var rtRemark = Util.FromRect(rtContent.Left + ValueAxisWidth + GP, rtContent.Bottom - RemarkAreaHeight, rtContent.Width - (ValueAxisWidth + GP) - Convert.ToInt32(sz.Width / 2), RemarkAreaHeight);
                var rtNameAxis = Util.FromRect(rtContent.Left + ValueAxisWidth + GP, rtRemark.Top - 10 - scwh - NameAxisHeight - GP, rtContent.Width - (ValueAxisWidth + GP), NameAxisHeight);
                var rtValueAxis = Util.FromRect(rtContent.Left, rtContent.Left + gpTopMargin, ValueAxisWidth, rtNameAxis.Top - rtContent.Top - gpTopMargin - GP);
                var rtGraphAl = Util.FromRect(rtContent.Left + ValueAxisWidth + GP, rtContent.Left + gpTopMargin, rtContent.Width - (ValueAxisWidth + GP), rtValueAxis.Height);
                var rtGraph = Util.FromRect(rtGraphAl.Left, rtGraphAl.Top, rtGraphAl.Width - Convert.ToInt32(sz.Width / 2), rtGraphAl.Height);
                var rtScroll = Util.FromRect(rtGraph.Left, rtRemark.Top - 10 - scwh, rtGraph.Width, scwh);

                rtValueAxis.Height = (rtValueAxis.Top + rtGraph.Height) - rtValueAxis.Top;
                rtGraphAl.Height = (rtGraphAl.Top + rtGraph.Height) - rtGraphAl.Top;
                rtNameAxis.Y += 2;

                //rtScroll = Util.INT(rtScroll);
                //rtRemark = Util.INT(rtRemark);

                act(rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, dicSer, GP, CH);
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
        #endregion
    }
}
