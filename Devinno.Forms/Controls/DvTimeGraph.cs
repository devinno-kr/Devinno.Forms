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
    public class DvTimeGraph : DvControl
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
        #region FormatString
        private string sFormatString = null;
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
                if(tsXScale !=value)
                {
                    tsXScale = value;
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

            Size = new System.Drawing.Size(300, 200);

            scroll.Direction = ScrollDirection.Horizon;
            scroll.ScrollChanged += (o, s) => Invalidate();
            scroll.GetScrollTotal = () => GraphDatas.Count > 1 && Series.Count > 0 && Areas.ContainsKey("rtGraph") ? Convert.ToInt32(Areas["rtGraph"].Width * ((new DateTime(Convert.ToInt64(Math.Ceiling(Convert.ToDouble(GraphDatas.Last().Time.Ticks) / Convert.ToDouble(XAxisGraduation.Ticks))) * XAxisGraduation.Ticks) - new DateTime(GraphDatas.First().Time.Ticks / XAxisGraduation.Ticks * XAxisGraduation.Ticks)).TotalSeconds / XScale.TotalSeconds)) : 0;
            scroll.GetScrollTick = () => GraphDatas.Count > 1 && Series.Count > 0 && Areas.ContainsKey("rtGraph") ? Convert.ToInt32(Areas["rtGraph"].Width * (XAxisGraduation.TotalSeconds / XScale.TotalSeconds)) : 0 ;
            scroll.GetScrollView = () => Areas.ContainsKey("rtGraph") ? Areas["rtGraph"].Width : 0;
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
                scroll.MouseDown(e, Areas["rtScroll"]);
                if (scroll.TouchMode && CollisionTool.Check(Areas["rtGraph"], e.Location)) scroll.TouchDown(e);
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
                scroll.MouseMove(e, Areas["rtScroll"]);
                if (scroll.TouchMode) scroll.TouchMove(e);
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
                scroll.MouseUP(e);
                if (scroll.TouchMode && CollisionTool.Check(Areas["rtGraph"], e.Location)) scroll.TouchUp(e);
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
            foreach(var x in Series)
            {
                var w = Math.Max(Math.Ceiling(g.MeasureString(string.IsNullOrWhiteSpace(FormatString) ? x.Minimum.ToString("0") : x.Minimum.ToString(FormatString), Font).Width) + 1 + GP,
                                 Math.Ceiling(g.MeasureString(string.IsNullOrWhiteSpace(FormatString) ? x.Maximum.ToString("0") : x.Maximum.ToString(FormatString), Font).Width) + 1 + GP) - GP;
                var vw = Math.Max(Convert.ToInt32(g.MeasureString(x.Alias, Font).Width+2), Convert.ToInt32(w));
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

            var sval = DateTime.Now.ToString("yyyy.MM.dd\r\nHH:mm:ss");
            var sz = g.MeasureString(sval, Font);

            var rtContent = Areas["rtContent"];
            var rtRemark = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtContent.Bottom - RemarkAreaHeight, rtContent.Width - (ValueAxisWidth + GP) - Convert.ToInt32(sz.Width / 2), RemarkAreaHeight);
            var rtNameAxis = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtRemark.Top - GP - scwh - NameAxisHeight - GP, rtContent.Width - (ValueAxisWidth + GP), NameAxisHeight);
            var rtValueAxis = new Rectangle(rtContent.X, rtContent.Y + gpTopMargin, ValueAxisWidth, rtNameAxis.Top - rtContent.Top - gpTopMargin - GP);
            var rtGraphAl = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtContent.Y + gpTopMargin, rtContent.Width - (ValueAxisWidth + GP), rtValueAxis.Height);
            var rtGraph = new Rectangle(rtGraphAl.Left, rtGraphAl.Top, rtGraphAl.Width - Convert.ToInt32(sz.Width/2) , rtGraphAl.Height);
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
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            #endregion
            #region Init
            var p = new Pen(Color.Black, 2);
            var br = new SolidBrush(Color.Black);

            var ShadowColor = BackColor.BrightnessTransmit(Theme.OutShadowBright);
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
                for (var i = 0; i <= YAxisGraduationCount; i ++)
                {
                    var y = Convert.ToInt32(MathTool.Map(i, 0, YAxisGraduationCount, rtGraph.Bottom, rtGraph.Top));
                    
                    if (i == 0) { p.Color = GridColor; p.Width = 2; p.DashStyle = DashStyle.Solid; e.Graphics.DrawLine(p, rtGraph.Left, y + 2, rtGraph.Right, y + 2); }
                    else if (i == YAxisGraduationCount) { p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Solid; e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y); }
                    else if(YAxisGridDraw) { p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot; e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y); }

                    foreach(var ser in Series)
                    {
                        if (Areas.ContainsKey(ser.Name))
                        {
                            var vrt = Areas[ser.Name];
                            var val = MathTool.Map(i, 0, YAxisGraduationCount, ser.Minimum, ser.Maximum);
                            var sval = string.IsNullOrWhiteSpace(FormatString) ? val.ToString("0") : val.ToString(FormatString);
                            var sz = e.Graphics.MeasureString(sval, Font);
                            var rt = new Rectangle(vrt.Left, y - Convert.ToInt32((sz.Height + 2) / 2), vrt.Width, Convert.ToInt32(sz.Height) + 2);
                            Theme.DrawTextShadow(e.Graphics, null, sval, Font, ser.SeriesColor, BackColor, rt, DvContentAlignment.MiddleRight);
                        
                            if(i==0)
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

                var st = new DateTime(GraphDatas.First().Time.Ticks / XAxisGraduation.Ticks * XAxisGraduation.Ticks);
                var ed = new DateTime(Convert.ToInt64(Math.Ceiling(Convert.ToDouble(GraphDatas.Last().Time.Ticks) / Convert.ToDouble(XAxisGraduation.Ticks))) * XAxisGraduation.Ticks);

                for (DateTime i = st; i <= ed; i += XAxisGraduation)
                {
                    var x = MathTool.Map(i.Ticks, st.Ticks, st.Ticks + XScale.Ticks, rtGraph.Left, rtGraph.Right) + scroll.ScrollPositionWithOffset;
                    p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot;
                    if (x >= rtGraph.Left && x <= rtGraph.Right)
                    {
                        if ( XAxisGridDraw && x > rtGraph.Left && x < rtGraph.Right) e.Graphics.DrawLine(p, x, rtGraph.Top, x, rtGraph.Bottom);

                        var sval = i.ToString("yyyy.MM.dd\r\nHH:mm:ss");
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
                var st = new DateTime(GraphDatas.First().Time.Ticks / XAxisGraduation.Ticks * XAxisGraduation.Ticks);
                var ed = new DateTime(Convert.ToInt64(Math.Ceiling(Convert.ToDouble(GraphDatas.Last().Time.Ticks) / Convert.ToDouble(XAxisGraduation.Ticks))) * XAxisGraduation.Ticks);

                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.SetClip(rtGraph);
                foreach (var v in Series)
                {
                    var pts = GraphDatas.Select(x => new Point(Convert.ToInt32(MathTool.Map(x.Time.Ticks, st.Ticks, st.Ticks + tsXScale.Ticks, rtGraph.Left, rtGraph.Right) + scroll.ScrollPositionWithOffset),
                                                               Convert.ToInt32(MathTool.Map(x.Values[v.Name], v.Minimum, v.Maximum, rtGraph.Bottom, rtGraph.Top)))).ToArray();

                    pts = pts.Where(x => x.X >= rtGraph.Left && x.X <= rtGraph.Right).ToArray();
                    p.Width = 2;
                    p.DashStyle = DashStyle.Solid;
                    p.Color = v.SeriesColor;
                    e.Graphics.DrawLines(p, pts);
                }
                e.Graphics.ResetClip();
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            }
            #endregion
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            #region Scroll
            Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScroll, RoundType.ALL);
            Theme.DrawBorder(e.Graphics, BackColor.BrightnessTransmit(-Theme.BorderBright), BackColor, 1, rtScroll, RoundType.ALL, BoxDrawOption.BORDER);

            e.Graphics.SetClip(new Rectangle(rtScroll.X + 6, rtScroll.Y + 0, rtScroll.Width - 12, rtScroll.Height - 0));

            var cCur = scroll.IsScrolling ? Theme.ScrollCursorColor.BrightnessTransmit(0.3) : Theme.ScrollCursorColor;
            var rtcur = scroll.GetScrollCursorRect(rtScroll);
            if (rtcur.HasValue) Theme.DrawBox(e.Graphics, cCur, Theme.ScrollBarColor, rtcur.Value, RoundType.ALL, BoxDrawOption.BORDER);

            e.Graphics.ResetClip();
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
