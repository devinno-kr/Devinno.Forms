using Devinno.Extensions;
using Devinno.Forms.Extensions;
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
    public class DvBarGraphH : DvControl
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
        #region Graduation
        private double nGraduation = 10D;
        public double Graduation
        {
            get => nGraduation;
            set
            {
                if (nGraduation != value)
                {
                    nGraduation = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Gradient
        public bool Gradient { get; set; } = true;
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

        #region GraphMode
        private DvBarGraphMode eGraphMode = DvBarGraphMode.List;
        public DvBarGraphMode GraphMode
        {
            get => eGraphMode;
            set
            {
                if (eGraphMode != value)
                {
                    eGraphMode = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Scrollable
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
        #endregion
 
        #region BarSize
        private int nBarSize = 24;
        public int BarSize
        {
            get => nBarSize;
            set
            {
                if (nBarSize != value)
                {
                    nBarSize = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BarGap
        private int nBarGap = 8;
        public int BarGap
        {
            get => nBarGap;
            set
            {
                if (nBarGap != value)
                {
                    nBarGap = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Series
        public List<GraphSeries> Series { get; } = new List<GraphSeries>();
        #endregion

        #region DataH
        private float DataH => GraphMode == DvBarGraphMode.List ? (Series.Count * BarSize) + (BarGap * 2) : BarSize + (BarGap * 2);
        #endregion

        public double ScrollPosition
        {
            get => scroll.ScrollPosition;
            set => scroll.ScrollPosition = value;
        }
        #endregion

        #region Member Variable
        private List<GV> GraphDatas = new List<GV>();
        private Scroll scroll = new Scroll();
        private Dictionary<string, SizeF> lsNameSize = new Dictionary<string, SizeF>();
        private Dictionary<string, SizeF> lsSerSize = new Dictionary<string, SizeF>();
        #endregion

        #region Constructor
        public DvBarGraphH()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 100);

            scroll = new Scroll() { TouchMode = true, Direction = ScrollDirection.Vertical };
            scroll.ScrollChanged += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            scroll.ScrollEnded += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            scroll.GetScrollTotal = () => GraphDatas.Count > 0 && Series.Count > 0 ? GraphDatas.Count * DataH : 0;
            scroll.GetScrollTick = () => DataH;
            scroll.GetScrollView = () =>
            {
                long v = 0;
                Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, GP, CH) =>
                {
                    v = Convert.ToInt64(rtGraph.Height);
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

            scroll.TouchMode = Theme.TouchMode;

            Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, GP, CH) =>
            {
                #region Var
                var spos = 0F;
                var DataH = this.DataH;
                if (Scrollable) spos = Convert.ToSingle(scroll.ScrollPositionWithOffset);
                else DataH = rtNameAxis.Height / GraphDatas.Count;
                #endregion
                #region GraphBG
                if (GraphBackColor != Color.Transparent)
                {
                    br.Color = GraphBackColor;
                    e.Graphics.FillRectangle(br, rtGraph);
                }
                #endregion
                #region Min/Max
                var Minimum = this.Minimum;
                var Maximum = this.Maximum;
                if (GraphDatas.Count > 0)
                {
                    var rMinimum = GraphMode == DvBarGraphMode.List ? GraphDatas.Select(x => x.Values.Min(x2 => x2.Value)).Min() : GraphDatas.Select(x => x.Values.Sum(x2 => x2.Value)).Min();
                    var rMaximum = GraphMode == DvBarGraphMode.List ? GraphDatas.Select(x => x.Values.Max(x2 => x2.Value)).Max() : GraphDatas.Select(x => x.Values.Sum(x2 => x2.Value)).Max();
                    Minimum = Math.Min(this.Minimum, rMinimum);
                    Maximum = Math.Max(this.Maximum, Math.Ceiling(rMaximum / Graduation) * Graduation);
                }
                #endregion
                #region Remark
                if (Series.Count > 0)
                {
                    var RemarkH = (CH * 1.5F);
                    var rtRemarkBox = Util.INT(Util.MakeRectangleAlign(rtRemark, new SizeF(rtRemark.Width, (RemarkH * Series.Count) + GP + (GP / 2F * (Series.Count - 1))), DvContentAlignment.MiddleRight));
                    Theme.DrawBox(e.Graphics, rtRemarkBox, RemarkColor, RemarkBorderColor, RoundType.All, Box.ButtonUp_Flat(ShadowGap));

                    for (int i = 0; i < Series.Count; i++)
                    {
                        var s = Series[i];
                        var rt = Util.FromRect(rtRemarkBox.Left, rtRemarkBox.Top + (RemarkH * i) + (GP / 2) + (GP / 2F * i), rtRemarkBox.Width, RemarkH);
                        var rtBR = MathTool.MakeRectangle(rt, new SizeF(10F, 5F));
                        rtBR.Offset(0, 1);
                        rtBR.X = rt.Left + GP; rtBR.Width = (rtBR.Left + 10) - rtBR.Left;
                        var rtTX = Util.FromRect(rtBR.Right + Convert.ToInt32(5), rt.Top, rt.Width - Convert.ToInt32(5) - rtBR.Width, rt.Height);
                        var SeriasBorderColor = Theme.GetBorderColor(s.SeriesColor, RemarkColor);

                        Theme.DrawBox(e.Graphics, Util.INT(rtBR), s.SeriesColor, SeriasBorderColor, RoundType.Rect, Box.ButtonUp_Flat(1));
                        Theme.DrawText(e.Graphics, s.Alias, Font, ForeColor, rtTX, DvContentAlignment.MiddleLeft);
                    }
                }
                #endregion
                #region Value Axis
                if (Graduation > 0)
                {
                    for (var i = Minimum; i <= Maximum; i += Graduation)
                    {
                        var n = i;
                        var s = string.IsNullOrWhiteSpace(FormatString) ? n.ToString("0") : n.ToString(FormatString);
                        var x = Convert.ToInt32(MathTool.Map(n, Minimum, Maximum, rtGraph.Left, rtGraph.Right));
                        var y = rtValueAxis.Top + (rtValueAxis.Height / 2);
                        
                        var sz = e.Graphics.MeasureString(s, Font);
                        var rt = MathTool.MakeRectangle(new PointF(x, y), sz.Width, sz.Height); rt.Inflate(2, 2);
                        Theme.DrawText(e.Graphics, s, Font, GridColor, rt);

                        if (n == Minimum)
                        {
                            p.Color = GridColor; 
                            p.Width = 1;
                            p.DashStyle = DashStyle.Solid;
                            e.Graphics.DrawLine(p, x - 1, rtGraph.Top, x - 1, rtGraph.Bottom);
                        }
                        else if (n == Maximum)
                        {
                            p.Color = GridColor; 
                            p.Width = 1;
                            p.DashStyle = DashStyle.Solid;
                            e.Graphics.DrawLine(p, x - 0, rtGraph.Top, x - 0, rtGraph.Bottom);
                        }
                        else
                        {
                            p.Color = GridColor; 
                            p.Width = 1;
                            p.DashStyle = DashStyle.Custom;
                            p.DashPattern = new float[] { 2, 2 };
                            e.Graphics.DrawLine(p, x - 0, rtGraph.Top, x - 0, rtGraph.Bottom);
                        }
                    }
                }
                #endregion
                #region Name Axis
                if (GraphDatas.Count > 0)
                {
                    e.Graphics.SetClip(rtNameAxis);

                    for (int i = 0; i < GraphDatas.Count; i++)
                    {
                        var itm = GraphDatas[i];
                        var rt = Util.FromRect(rtNameAxis.Left, spos + rtNameAxis.Top + Convert.ToInt32(DataH * i), rtNameAxis.Width, Convert.ToInt32(DataH));
                        if (CollisionTool.Check(rt, rtNameAxis)) Theme.DrawText(e.Graphics, itm.Name, Font, GridColor, rt);
                    }

                    e.Graphics.ResetClip();
                }
                #endregion
                #region Data
                if (Series.Count > 0 && GraphDatas.Count > 0)
                {
                    e.Graphics.SetClip(rtGraph);

                    var dicSer = Series.ToDictionary(x => x.Name);
                    if (GraphMode == DvBarGraphMode.List)
                    {
                        #region List
                        for (int i = 0; i < GraphDatas.Count; i++)
                        {
                            var itm = GraphDatas[i];
                            var rt = Util.FromRect(rtGraph.Left, spos + rtNameAxis.Top + (DataH * i), rtGraph.Width, (DataH));
                            rt.Inflate(0, -BarGap);

                            if (CollisionTool.Check(rt, rtGraph))
                            {
                                var ih = Math.Min(BarSize, (rt.Height / Series.Count));
                                var ic = 0;
                                var tgp = !Scrollable ? ((rt.Height) - (ih * Series.Count)) / 2 : 0;

                                foreach (var vk in itm.Values.Keys)
                                {
                                    if (dicSer.ContainsKey(vk))
                                    {
                                        var gp = 2;
                                        var n = itm.Values[vk];
                                        var w = Convert.ToSingle(MathTool.Map(n, Minimum, Maximum, 0, rtGraph.Width));
                                        var rtv = Util.FromRect(rtGraph.Left - gp, tgp + rt.Top + (ic * ih), (w + gp), ih);
                                        var ser = dicSer[vk];
                                        var bc = ser.SeriesColor.BrightnessTransmit(Theme.BorderBrightness);
                                        Theme.DrawBox(e.Graphics, (rtv), ser.SeriesColor, bc, RoundType.Rect, Gradient ? Box.ButtonUp_V(true, 0) : (BoxStyle.Fill | BoxStyle.Border));
                                        if (ValueDraw)
                                        {
                                            if (n > 0)
                                            {
                                                var txt = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                                                var rtm = e.Graphics.MeasureString(txt, Font);
                                                var rtt = Util.FromRect(rtv.Left, rtv.Top, rtv.Width - 5, rtv.Height);

                                                if (rtt.Width < rtm.Width) 
                                                    Theme.DrawText(e.Graphics, txt, Font, ForeColor, new RectangleF(rtv.Right, rtv.Top, rtm.Width + 10, rtv.Height), DvContentAlignment.MiddleCenter); 
                                                else 
                                                    Theme.DrawText(e.Graphics, txt, Font, ForeColor, rtt, DvContentAlignment.MiddleRight);
                                            }
                                        }
                                    }
                                    ic++;
                                }
                            }
                        }
                        #endregion
                    }
                    else if (GraphMode == DvBarGraphMode.Stack)
                    {
                        #region Stack
                        for (int i = 0; i < GraphDatas.Count; i++)
                        {
                            var itm = GraphDatas[i];
                            var rt = Util.FromRect(rtGraph.Left, spos + rtNameAxis.Top + (DataH * i), rtGraph.Width, (DataH));
                            rt.Inflate(0, -BarGap);

                            if (CollisionTool.Check(rt, rtGraph))
                            {
                                var BarSize = !Scrollable ? Math.Min(this.BarSize, rt.Height) : this.BarSize;
                                var ix = rt.Left;
                                foreach (var vk in itm.Values.Keys)
                                {
                                    if (dicSer.ContainsKey(vk))
                                    {
                                        var n = itm.Values[vk];
                                        var w = Convert.ToSingle(MathTool.Map(n, Minimum, Maximum, 0, rtGraph.Width));
                                        var rtv = !Scrollable ? Util.FromRect(ix, rt.Top + (rt.Height / 2) - (BarSize / 2), (w), BarSize) : Util.FromRect(ix, rt.Top, Convert.ToInt32(w), rt.Height);
                                        var ser = dicSer[vk];
                                        var bc = ser.SeriesColor.BrightnessTransmit(Theme.BorderBrightness);
                                        Theme.DrawBox(e.Graphics, (rtv), ser.SeriesColor, bc, RoundType.Rect, Gradient ? Box.ButtonUp_V(true, 0) : (BoxStyle.Fill | BoxStyle.Border));
                                        if (ValueDraw)
                                        {
                                            if (n > 0)
                                            {
                                                var txt = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                                                var rtm = e.Graphics.MeasureString(txt, Font);
                                                var rtt = Util.FromRect(rtv.Left, rtv.Top, rtv.Width - 5, rtv.Height);
                                                Theme.DrawText(e.Graphics, txt, Font, ForeColor, rtt, DvContentAlignment.MiddleRight);
                                            }
                                        }
                                        ix = rtv.Right;
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    e.Graphics.ResetClip();
                }
                #endregion
                #region Scroll
                if (Scrollable)
                {
                    Theme.DrawBox(e.Graphics, rtScroll, Theme.ScrollBarColor, ScrollBorderColor, RoundType.All, Box.FlatBox(true));

                    e.Graphics.SetClip(Util.FromRect(rtScroll.Left, rtScroll.Top + 0, rtScroll.Width, rtScroll.Height - 0));

                    var cCur = Theme.ScrollCursorOffColor;
                    if (scroll.IsScrolling || scroll.IsTouchMoving || scroll.IsTouchScrolling) cCur = Theme.ScrollCursorOnColor;

                    var rtcur = scroll.GetScrollCursorRect(rtScroll);
                    if (rtcur.HasValue) Theme.DrawBox(e.Graphics, Util.INT(rtcur.Value), cCur, ScrollBorderColor, RoundType.All, Box.FlatBox(true));

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

            Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, GP, CH) =>
            {
                if (Scrollable)
                {
                    scroll.MouseDown(x, y, rtScroll);
                    if (scroll.TouchMode && CollisionTool.Check(rtGraph, x, y)) scroll.TouchDown(x, y);
                }
            });

            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;

            Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, GP, CH) =>
            {
                if (Scrollable)
                {
                    scroll.MouseMove(x, y, rtScroll);
                    if (scroll.TouchMode) scroll.TouchMove(x, y);
                }
            });

            if (scroll.IsScrolling || scroll.IsTouchMoving || scroll.IsTouchScrolling) Invalidate();
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;

            Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, GP, CH) =>
            {
                if (Scrollable)
                {
                    scroll.MouseUp(x, y);
                    if (scroll.TouchMode) scroll.TouchUp(x, y);
                }
            });

            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseWheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;

            Areas((rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, GP, CH) =>
            {
                if (Scrollable)
                {
                    ((HandledMouseEventArgs)e).Handled = true;
                    scroll.MouseWheel(e.Delta, rtScroll);
                }
            });

            Invalidate();
            base.OnMouseWheel(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, float, float> act)
        {
            using (var g = CreateGraphics())
            {
                if (lsSerSize.Count == 0 || Series.Count != lsSerSize.Count)
                {
                    lsSerSize.Clear();
                    foreach (var v in Series)
                    {
                        var sz = g.MeasureString(v.Alias, Font);
                        if (!lsSerSize.ContainsKey(v.Name)) lsSerSize.Add(v.Name, sz);
                    }
                }

                var sMin = string.IsNullOrWhiteSpace(FormatString) ? Minimum.ToString("0") : Minimum.ToString(FormatString);
                var sMax = string.IsNullOrWhiteSpace(FormatString) ? Maximum.ToString("0") : Maximum.ToString(FormatString);

                var szCHSZ = g.MeasureString("H", Font);
                var szMin = g.MeasureString(sMin, Font);
                var szMax = g.MeasureString(sMax, Font);

                var CH = szCHSZ.Height;
                var GP = 5;

                var ValueAxisHeight = (GP + (CH * 1.5F));
                var NameAxisWidth = lsNameSize.Count > 0 ? (lsNameSize.Values.Select(x => x.Width).Max() + GP) : 0F;
                var RemarkAreaWidth = lsSerSize.Count > 0 ? (GP + (10) + (5) + lsSerSize.Values.Select(x => x.Width).Max() + GP) : 0F;
                var gpTopMargin = 0;

                var rtContent = GetContentBounds();
                var rtRemark = Util.FromRect(rtContent.Right - RemarkAreaWidth, rtContent.Top + gpTopMargin, RemarkAreaWidth, rtContent.Height - (gpTopMargin * 2) - GP - ValueAxisHeight);
                var rtNameAxis = Util.FromRect(rtContent.Left, rtContent.Top + gpTopMargin, NameAxisWidth, rtContent.Height - (gpTopMargin * 2) - GP - ValueAxisHeight);
                var rtValueAxis = Util.FromRect(rtNameAxis.Right + GP, rtContent.Top + gpTopMargin + rtRemark.Height + GP, rtContent.Width - (GP * 2) - rtRemark.Width - rtNameAxis.Width - GP, ValueAxisHeight);
                var rtGraphAl = Util.FromRect(rtNameAxis.Right + GP, rtContent.Top + gpTopMargin, rtValueAxis.Width, rtContent.Height - (gpTopMargin * 2) - GP - ValueAxisHeight);
                var rtScroll = new RectangleF(0, 0, 0, 0);
                var rtGraph = new RectangleF();

                if (!Scrollable)
                {
                    rtGraph = rtGraphAl;
                }
                else
                {
                    var scwh = Convert.ToInt32(Scroll.SC_WH);

                    rtGraph = Util.FromRect(rtGraphAl.Left, rtGraphAl.Top, rtGraphAl.Width - scwh - GP * 2, rtGraphAl.Height);
                    rtScroll = Util.FromRect(rtGraph.Right + GP * 2, rtGraph.Top, scwh, rtGraph.Height);
                }

                //rtRemark = Util.INT(rtRemark);
                //rtScroll = Util.INT(rtScroll);

                act(rtContent, rtRemark, rtNameAxis, rtValueAxis, rtGraphAl, rtGraph, rtScroll, GP, CH);
            }
        }
        #endregion
        #region SetDataSource
        public void SetDataSource<T>(IEnumerable<T> values) where T : GraphData
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
                        GraphDatas.Add(new GV() { Name = v.Name, Props = dic, Data = v });


                    using (var g = CreateGraphics())
                    {
                        lsNameSize.Clear();
                        foreach (var v in GraphDatas)
                        {
                            var sz = g.MeasureString(v.Name, Font);
                            if (!lsNameSize.ContainsKey(v.Name)) lsNameSize.Add(v.Name, sz);
                        }

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
