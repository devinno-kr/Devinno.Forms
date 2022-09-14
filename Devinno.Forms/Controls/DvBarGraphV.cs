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
    public class DvBarGraphV : DvControl
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
        private float DataW => GraphMode == DvBarGraphMode.List ? (Series.Count * BarSize) + (BarGap * 2) : BarSize + (BarGap * 2);
        #endregion
        #endregion

        #region Member Variable
        private List<GV> GraphDatas = new List<GV>();
        private Scroll scroll = new Scroll();
        private Dictionary<string, SizeF> lsNameSize = new Dictionary<string, SizeF>();
        private Dictionary<string, SizeF> lsSerSize = new Dictionary<string, SizeF>();
        #endregion

        #region Constructor
        public DvBarGraphV()
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
            scroll.GetScrollTotal = () => 
            GraphDatas.Count > 0 && Series.Count > 0 ? GraphDatas.Count * DataW - DataW : 0;
            scroll.GetScrollTick = () => DataW;
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
                float DataW = this.DataW;
                if (Scrollable) spos = Convert.ToSingle(scroll.ScrollPositionWithOffset);
                else DataW = rtNameAxis.Width / GraphDatas.Count;
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
                    var RemarkW = (GP * 2) + lsSerSize.Values.Sum(x => Convert.ToInt32(10) + Convert.ToInt32(5) + Convert.ToInt32(x.Width) + (GP * 2) + 2);
                    var rtRemarkBox = Util.INT(Util.MakeRectangleAlign(rtRemark, new SizeF(RemarkW, rtRemark.Height), DvContentAlignment.MiddleCenter));
                    Theme.DrawBox(e.Graphics, rtRemarkBox, RemarkColor, RemarkBorderColor, RoundType.All, Box.ButtonUp_Flat(ShadowGap));

                    var ix = rtRemarkBox.Left + (GP * 2);
                    var nwbr = Convert.ToInt32(10);
                    var nwgp = Convert.ToInt32(5);
                    for (int i = 0; i < Series.Count; i++)
                    {
                        var s = Series[i];
                        var sz = lsSerSize[s.Name];
                        var rtBR = Util.MakeRectangleAlign(Util.FromRect(ix, rtRemarkBox.Top, nwbr, rtRemarkBox.Height), new SizeF(nwbr, nwgp), DvContentAlignment.MiddleCenter);
                        ix += rtBR.Width;
                        ix += nwgp;
                        var rtTX = Util.FromRect(ix, rtRemarkBox.Top, Convert.ToInt32(sz.Width) + 2, rtRemarkBox.Height);
                        ix += rtTX.Width;
                        ix += GP * 2;

                        var SeriesBorderColor = Theme.GetBorderColor(s.SeriesColor, BackColor);
                        Theme.DrawBox(e.Graphics, Util.INT(rtBR), s.SeriesColor, SeriesBorderColor, RoundType.Rect, Box.ButtonUp_Flat(1));
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
                        var y = Convert.ToInt32(MathTool.Map(n, Minimum, Maximum, rtGraph.Bottom, rtGraph.Top));
                      
                        var sz = e.Graphics.MeasureString(s, Font);
                        var rt = MathTool.MakeRectangle(new PointF(0, y), 10F, sz.Height);
                        rt.X = rtValueAxis.Left; rt.Width = rtValueAxis.Right - rt.Left;
                        Theme.DrawText(e.Graphics, s, Font, GridColor, rt);

                        if (n == Minimum)
                        {
                            p.Color = GridColor;
                            p.Width = 1;
                            p.DashStyle = DashStyle.Solid;
                            e.Graphics.DrawLine(p, rtGraph.Left, y + 1, rtGraph.Right, y + 1);
                        }
                        else if (n == Maximum)
                        {
                            p.Color = GridColor;
                            p.Width = 1;
                            p.DashStyle = DashStyle.Solid;
                            e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y);
                        }
                        else
                        {
                            p.Color = GridColor;
                            p.Width = 1;
                            p.DashStyle = DashStyle.Custom;
                            p.DashPattern = new float[] { 2, 2 };
                            e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y);
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
                        var rt = Util.FromRect(spos + rtNameAxis.Left + Convert.ToInt32(DataW * i), rtNameAxis.Top, Convert.ToInt32(DataW), rtNameAxis.Height);
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
                            var rt = Util.FromRect(spos + rtGraph.Left + (DataW * i), rtGraph.Top, (DataW), rtGraph.Height);
                            
                            rt.Inflate(-BarGap, 0);

                            if (CollisionTool.Check(rt, rtGraph))
                            {
                                var iw = !Scrollable ? Math.Min(this.BarSize, rt.Width / Series.Count) : (rt.Width / Series.Count);
                                var ic = 0;

                                var lgp = !Scrollable ? (rt.Width - (iw * Series.Count)) / 2F : 0;
                                foreach (var vk in itm.Values.Keys)
                                {
                                    if (dicSer.ContainsKey(vk))
                                    {
                                        var n = itm.Values[vk];
                                        var h = Convert.ToSingle(MathTool.Map(n, Minimum, Maximum, 0, rtGraph.Height));
                                        var rtv = Util.FromRect(lgp + rt.Left + (ic * iw), rtGraph.Bottom - (h), iw, (h + 1));
                                        var ser = dicSer[vk];
                                        var bc = ser.SeriesColor.BrightnessTransmit(Theme.BorderBrightness);
                                        Theme.DrawBox(e.Graphics, (rtv), ser.SeriesColor, bc, RoundType.Rect, Box.ButtonUp_H(true, ShadowGap));

                                        if (ValueDraw)
                                        {
                                            var txt = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                                            var rtt = Util.FromRect(rtv.Left, rtv.Top + 5, rtv.Width, rtv.Height);
                                            Theme.DrawText(e.Graphics, txt, Font, ForeColor, rtt, DvContentAlignment.TopCenter, true);
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
                            var rt = Util.FromRect(spos + rtGraph.Left + (DataW * i), rtGraph.Top, (DataW), rtGraph.Height);
                            rt.Inflate(-BarGap, 0);
                            var iy = rt.Bottom;
                            var BarSize = !Scrollable ? Math.Min(this.BarSize, rt.Width) : this.BarSize;
                            if (CollisionTool.Check(rt, rtGraph))
                            {
                                foreach (var vk in itm.Values.Keys)
                                {
                                    if (dicSer.ContainsKey(vk))
                                    {
                                        var n = itm.Values[vk];
                                        var h = Convert.ToSingle(MathTool.Map(n, Minimum, Maximum, 0, rtGraph.Height));
                                        var rtv = !Scrollable ? Util.FromRect(rt.Left + (rt.Width / 2) - (BarSize / 2), iy - (h), BarSize, (h + 1)) : Util.FromRect(rt.Left, iy - (h), rt.Width, (h + 1));

                                        var ser = dicSer[vk];
                                        var bc = ser.SeriesColor.BrightnessTransmit(Theme.BorderBrightness);
                                        Theme.DrawBox(e.Graphics, (rtv), ser.SeriesColor, bc, RoundType.Rect, Box.ButtonUp_H(true, ShadowGap));

                                        if (ValueDraw)
                                        {
                                            if (n > 0)
                                            {
                                                var txt = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                                                var rtt = Util.FromRect(rtv.Left, rtv.Top + 5, rtv.Width, rtv.Height);
                                                Theme.DrawText(e.Graphics, txt, Font, ForeColor, rtt, DvContentAlignment.TopCenter);
                                            }
                                        }
                                        iy = rtv.Top;
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

                var ValueAxisWidth = Math.Max(szMin.Width * 1.5F, szMax.Width * 1.5F);
                var NameAxisHeight = (GP + (CH * 1.5F));
                var RemarkAreaHeight = (GP + (CH * 1.5F) + GP);
                var gpTopMargin = (CH / 2F);

                var rtContent = GetContentBounds();
                var rtRemark = Util.FromRect(rtContent.Left + ValueAxisWidth + GP, rtContent.Bottom - RemarkAreaHeight, rtContent.Width - (ValueAxisWidth + GP), RemarkAreaHeight);
                var rtNameAxis = Util.FromRect(rtContent.Left + ValueAxisWidth + GP, rtRemark.Top - 10 - NameAxisHeight, rtContent.Width - (ValueAxisWidth + GP), NameAxisHeight);
                var rtValueAxis = Util.FromRect(rtContent.Left, rtContent.Top + gpTopMargin, ValueAxisWidth, rtNameAxis.Top - rtContent.Top - gpTopMargin);
                var rtGraphAl = Util.FromRect(rtContent.Left + ValueAxisWidth + GP, rtContent.Top + gpTopMargin, rtContent.Width - (ValueAxisWidth + GP), rtValueAxis.Height);
                var rtScroll = new RectangleF(0, 0, 0, 0);
                var rtGraph = new RectangleF();

                if (!Scrollable)
                {
                    rtGraph = rtGraphAl;
                }
                else
                {
                    var scwh = Convert.ToInt32(Scroll.SC_WH);

                    rtNameAxis = Util.FromRect(rtContent.Left + ValueAxisWidth + GP, rtRemark.Top - 10 - scwh - NameAxisHeight - GP, rtContent.Width - (ValueAxisWidth + GP), NameAxisHeight);
                    rtValueAxis = Util.FromRect(rtContent.Left, rtContent.Top + gpTopMargin, ValueAxisWidth, rtNameAxis.Top - rtContent.Top - gpTopMargin - GP);
                    rtGraphAl = Util.FromRect(rtContent.Left + ValueAxisWidth + GP, rtContent.Top + gpTopMargin, rtContent.Width - (ValueAxisWidth + GP), rtValueAxis.Height);

                    rtGraph = Util.FromRect(rtGraphAl.Left, rtGraphAl.Top, rtGraphAl.Width, rtGraphAl.Height);
                    rtScroll = Util.FromRect(rtGraph.Left, rtRemark.Top - 10 - scwh, rtGraph.Width, scwh);
                 
                    rtValueAxis.Height = rtGraph.Bottom - rtValueAxis.Top;
                    rtGraphAl.Height = rtGraph.Bottom - rtGraphAl.Top;
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
