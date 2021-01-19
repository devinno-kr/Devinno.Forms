using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        #region Const
        const int GraphGap = 8;
        #endregion

        #region Properties
        #region GraphBackColor
        private Color cGraphBackColor = Color.Transparent;
        [Category("- 색상")]
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
        [Category("- 색상")]
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
        /// <summary>
        /// 그례프 계열
        /// </summary>
        [Category("- 동작")]
        public List<DvGraphSeries> Series { get; } = new List<DvGraphSeries>();
        #endregion

        #region Graduation
        private double nGraduation = 10D;
        [Category("- 동작")]
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
        #region Minimum
        private double nMinimum = 0D;
        [Category("- 동작")]
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
        [Category("- 동작")]
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
        #region FormatString
        private string sFormatString = null;
        [Category("- 모양")]
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
        private bool bValueDraw = false;
        [Category("- 모양")]
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
        private DvBarGraphMode eGraphMode = DvBarGraphMode.LIST;
        [Category("- 동작")]
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
        private bool bScrollable = false;
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

        public int BarSize { get; set; } = 48;
        private int DataW => GraphMode == DvBarGraphMode.LIST ? (Series.Count * BarSize) + (GraphGap * 2) : BarSize + (GraphGap * 2);
        #endregion

        #region Member Variable
        private List<GV> GraphDatas = new List<GV>();
        private DvScroll scroll = new DvScroll();
        #endregion

        #region Constructor
        public DvBarGraphV()
        {
            Size = new System.Drawing.Size(300, 200);

            scroll.Direction = ScrollDirection.Horizon;
            scroll.ScrollChanged += (o, s) => Invalidate();
            scroll.GetScrollTotal = () => GraphDatas.Count > 0 && Series.Count > 0 ? GraphDatas.Count * DataW : 0;
            scroll.GetScrollTick = () => (GraphMode == DvBarGraphMode.LIST ? (Series.Count + 2) * BarSize : 3 * BarSize);
            scroll.GetScrollView = () => Areas["rtGraph"].Width;
        }
        #endregion

        #region Override
        #region OnMouseWheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (Scrollable) scroll.MouseWheel(e);
            Invalidate();
            base.OnMouseWheel(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Scrollable && Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtGraph"))
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
            if (Scrollable && Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtGraph"))
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
            if (Scrollable && Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtGraph"))
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

            #region Min/Max
            var Minimum = this.Minimum;
            var Maximum = this.Maximum;
            if (GraphDatas.Count > 0)
            {
                var rMinimum = GraphMode == DvBarGraphMode.LIST ? GraphDatas.Select(x => x.Values.Min(x => x.Value)).Min() : GraphDatas.Select(x => x.Values.Sum(x => x.Value)).Min();
                var rMaximum = GraphMode == DvBarGraphMode.LIST ? GraphDatas.Select(x => x.Values.Max(x => x.Value)).Max() : GraphDatas.Select(x => x.Values.Sum(x => x.Value)).Max();
                Minimum = Math.Min(this.Minimum, rMinimum);
                Maximum = Math.Max(this.Maximum, Math.Ceiling(rMaximum / Graduation) * Graduation);
            }
            #endregion

            var CHSZ = g.MeasureString("H", Font);
            var rtContent = GetContentBounds();
            var f = DpiRatio;
            var GP = Convert.ToInt32(6 * f);
            var ValueAxisWidth = Convert.ToInt32(Math.Max(g.MeasureString(Minimum.ToString(), Font).Width * 1.5, g.MeasureString(Maximum.ToString(), Font).Width * 1.5));
            var NameAxisHeight = Convert.ToInt32(GP + (CHSZ.Height * 1.5));
            var RemarkAreaHeight = Convert.ToInt32(GP + (CHSZ.Height * 1.5) + GP);
            var gpTopMargin = Convert.ToInt32(CHSZ.Height / 2);
            var rtRemark = new Rectangle(rtContent.X + ValueAxisWidth + GP,     rtContent.Bottom - RemarkAreaHeight,    rtContent.Width - (ValueAxisWidth + GP),    RemarkAreaHeight);
            var rtNameAxis = new Rectangle(rtContent.X + ValueAxisWidth + GP,   rtRemark.Top - GP - NameAxisHeight ,    rtContent.Width - (ValueAxisWidth + GP),    NameAxisHeight);
            var rtValueAxis = new Rectangle(rtContent.X, rtContent.Y + gpTopMargin, ValueAxisWidth, rtNameAxis.Top - rtContent.Top - gpTopMargin);
            var rtGraphAl = new Rectangle(rtContent.X + ValueAxisWidth + GP,    rtContent.Y + gpTopMargin,              rtContent.Width - (ValueAxisWidth + GP),    rtValueAxis.Height);

            SetArea("rtCHSZ", new Rectangle(0, 0, Convert.ToInt32(Math.Ceiling(CHSZ.Width)), Convert.ToInt32(Math.Ceiling(CHSZ.Height))));
           
            if (!Scrollable)
            {
                var rtGraph = rtGraphAl;
                SetArea("rtRemark", rtRemark);
                SetArea("rtNameAxis", rtNameAxis);
                SetArea("rtValueAxis", rtValueAxis);
                SetArea("rtGraphAl", rtGraphAl);
                SetArea("rtGP", new Rectangle(0, 0, GP, GP));
                SetArea("rtGraph", rtGraph);
                if (Areas.ContainsKey("rtScroll")) Areas.Remove("rtScroll");
            }
            else
            {
                var scwh = Convert.ToInt32(DvScroll.SC_WH * f);
                var rtGraph = new Rectangle(rtGraphAl.Left, rtGraphAl.Top, rtGraphAl.Width, rtGraphAl.Height - scwh);
                var rtScroll = new Rectangle(rtGraph.Left, rtGraph.Bottom + 2, rtGraph.Width, scwh);
                rtValueAxis.Height = rtGraphAl.Height = rtGraph.Height;
                rtNameAxis.Y += 2;
                SetArea("rtRemark", rtRemark);
                SetArea("rtNameAxis", rtNameAxis);
                SetArea("rtValueAxis", rtValueAxis);
                SetArea("rtGraphAl", rtGraphAl);
                SetArea("rtGP", new Rectangle(0, 0, GP, GP));
                SetArea("rtGraph", rtGraph);
                SetArea("rtScroll", rtScroll);
            }
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
            var GP = rtGP.Width;
            #endregion
            #region Draw
            if (!Scrollable)
            {
                #region Draw
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
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
                    var rMinimum = GraphMode == DvBarGraphMode.LIST ? GraphDatas.Select(x => x.Values.Min(x => x.Value)).Min() : GraphDatas.Select(x => x.Values.Sum(x => x.Value)).Min();
                    var rMaximum = GraphMode == DvBarGraphMode.LIST ? GraphDatas.Select(x => x.Values.Max(x => x.Value)).Max() : GraphDatas.Select(x => x.Values.Sum(x => x.Value)).Max();
                    Minimum = Math.Min(this.Minimum, rMinimum);
                    Maximum = Math.Max(this.Maximum, Math.Ceiling(rMaximum / Graduation) * Graduation);
                }
                #endregion
                #region Remark
                if (Series.Count > 0)
                {
                    var ls = Series.Select(x => e.Graphics.MeasureString(x.Alias, Font)).ToList();
                    var RemarkW = (GP * 2) + ls.Sum(x => Convert.ToInt32(10 * f) + Convert.ToInt32(5 * f) + Convert.ToInt32(x.Width) + (GP * 2));
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
                        var rtTX = new Rectangle(ix, rtRemarkBox.Y, Convert.ToInt32(ls[i].Width), rtRemarkBox.Height);
                        ix += rtTX.Width;
                        ix += GP * 2;

                        Theme.DrawBox(e.Graphics, s.SeriesColor, BackColor, rtBR, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
                        Theme.DrawTextShadow(e.Graphics, null, s.Alias, Font, ForeColor, BackColor, rtTX, DvContentAlignment.MiddleLeft);
                    }
                }
                #endregion
                #region Value Axis
                if (Graduation > 0)
                {
                    for (var i = Minimum; i <= Maximum; i += Graduation)
                    {
                        var n = i;
                        var s = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                        var y = Convert.ToInt32(MathTool.Map(n, Minimum, Maximum, rtGraph.Bottom, rtGraph.Top));
                        var sz = e.Graphics.MeasureString(s, Font);
                        var rt = MathTool.MakeRectangle(new Point(0, y), Convert.ToInt32(10), Convert.ToInt32(sz.Height)); 
                        rt.X = rtValueAxis.Left;    rt.Width = rtValueAxis.Width;
                        Theme.DrawText(e.Graphics, null, s, Font, GridColor, rt, DvContentAlignment.MiddleRight);

                        if (n == Minimum) { p.Color = GridColor; p.Width = 2; p.DashStyle = DashStyle.Solid; e.Graphics.DrawLine(p, rtGraph.Left, y + 2, rtGraph.Right, y + 2); }
                        else if (n == Maximum) { p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Solid; e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y); }
                        else { p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot; e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y); }
                    }
                }
                #endregion
                #region Name Axis
                if (GraphDatas.Count > 0)
                {
                    p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot;

                    var DataW = (float)rtNameAxis.Width / (float)GraphDatas.Count;
                    for (int i = 0; i < GraphDatas.Count; i++)
                    {
                        var itm = GraphDatas[i];
                        var rt = new Rectangle(rtNameAxis.Left + Convert.ToInt32(DataW * i), rtNameAxis.Top, Convert.ToInt32(DataW), rtNameAxis.Height);
                        Theme.DrawTextShadow(e.Graphics, null, itm.Name, Font, GridColor, BackColor, rt, DvContentAlignment.MiddleCenter);
                    }
                }
                #endregion
                #region Data
                if (Series.Count > 0 && GraphDatas.Count > 0)
                {
                    var dicSer = Series.ToDictionary(x => x.Name);
                    if (GraphMode == DvBarGraphMode.LIST)
                    {
                        #region List
                        Font ft = null;
                        var DataW = (float)rtNameAxis.Width / (float)GraphDatas.Count;

                        if (ValueDraw)
                        {
                            var len = Math.Max(Maximum.ToString().Length, Minimum.ToString().Length);
                            var pt = Convert.ToSingle(MathTool.Constrain((DataW - (GraphGap * 2)) / Series.Count / 1.33 / len, 5, 9));
                            ft = new Font(Font.FontFamily, pt, FontStyle.Regular);
                        }

                        for (int i = 0; i < GraphDatas.Count; i++)
                        {
                            var itm = GraphDatas[i];
                            var rt = new Rectangle(rtGraph.Left + Convert.ToInt32(DataW * i), rtGraph.Top, Convert.ToInt32(DataW), rtGraph.Height);

                            rt.Inflate(-GraphGap, 0);

                            var iw = Convert.ToInt32((double)rt.Width / (double)Series.Count);
                            var ic = 0;
                            foreach (var vk in itm.Values.Keys)
                            {
                                if (dicSer.ContainsKey(vk))
                                {
                                    var n = itm.Values[vk];
                                    var h = MathTool.Map(n, Minimum, Maximum, 0, rtGraph.Height);
                                    var rtv = new Rectangle(rt.Left + (ic * iw), rtGraph.Bottom - Convert.ToInt32(h), iw, Convert.ToInt32(h));
                                    var ser = dicSer[vk];
                                    Theme.DrawBox(e.Graphics, ser.SeriesColor, bg, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT);

                                    if (ValueDraw)
                                    {
                                        br.Color = ForeColor;
                                        var txt = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                                        e.Graphics.DrawText(txt, ft, br, rtv, DvContentAlignment.BottomCenter);
                                    }
                                }
                                ic++;
                            }
                        }

                        if (ft != null) ft.Dispose();
                        #endregion
                    }
                    else if (GraphMode == DvBarGraphMode.STACK)
                    {
                        #region Stack
                        Font ft = null;
                        var DataW = (float)rtNameAxis.Width / (float)GraphDatas.Count;

                        if (ValueDraw)
                        {
                            var len = Math.Max(Maximum.ToString().Length, Minimum.ToString().Length);
                            var pt = Convert.ToSingle(MathTool.Constrain((DataW - (GraphGap * 2)) / 1.33 / len, 5, 9));
                            ft = new Font(Font.FontFamily, pt, FontStyle.Regular);
                        }

                        for (int i = 0; i < GraphDatas.Count; i++)
                        {
                            var itm = GraphDatas[i];
                            var rt = new Rectangle(rtGraph.Left + Convert.ToInt32(DataW * i), rtGraph.Top, Convert.ToInt32(DataW), rtGraph.Height);
                            rt.Inflate(-GraphGap, 0);

                            var iy = rt.Bottom;
                            foreach (var vk in itm.Values.Keys)
                            {
                                if (dicSer.ContainsKey(vk))
                                {
                                    var n = itm.Values[vk];
                                    var h = MathTool.Map(n, Minimum, Maximum, 0, rtGraph.Height);
                                    var rtv = new Rectangle(rt.X, iy - Convert.ToInt32(h), rt.Width, Convert.ToInt32(h));
                                    var ser = dicSer[vk];
                                    Theme.DrawBox(e.Graphics, ser.SeriesColor, bg, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT);
                                    if (ValueDraw)
                                    {
                                        if (n > 0)
                                        {
                                            br.Color = ForeColor;
                                            var txt = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                                            e.Graphics.DrawText(txt, ft, br, new RectangleF(rtv.X, rtv.Y, rtv.Width, rtv.Height), DvContentAlignment.BottomCenter);
                                        }
                                    }
                                    iy = rtv.Top;
                                }
                            }
                        }

                        if (ft != null) ft.Dispose();
                        #endregion
                    }
                }
                #endregion
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                #endregion
            }
            else
            {
                #region Draw
                var rtScroll = Areas["rtScroll"];
                var scv = -scroll.ScrollPosition + scroll.TouchOffset;
                var DataW = this.DataW;

                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
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
                    var rMinimum = GraphMode == DvBarGraphMode.LIST ? GraphDatas.Select(x => x.Values.Min(x => x.Value)).Min() : GraphDatas.Select(x => x.Values.Sum(x => x.Value)).Min();
                    var rMaximum = GraphMode == DvBarGraphMode.LIST ? GraphDatas.Select(x => x.Values.Max(x => x.Value)).Max() : GraphDatas.Select(x => x.Values.Sum(x => x.Value)).Max();
                    Minimum = Math.Min(this.Minimum, rMinimum);
                    Maximum = Math.Max(this.Maximum, Math.Ceiling(rMaximum / Graduation) * Graduation);
                }
                #endregion
                #region Remark
                if (Series.Count > 0)
                {
                    var ls = Series.Select(x => e.Graphics.MeasureString(x.Alias, Font)).ToList();
                    var RemarkW = (GP * 2) + ls.Sum(x => Convert.ToInt32(10 * f) + Convert.ToInt32(5 * f) + Convert.ToInt32(x.Width) + (GP * 2));
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
                        var rtTX = new Rectangle(ix, rtRemarkBox.Y, Convert.ToInt32(ls[i].Width), rtRemarkBox.Height);
                        ix += rtTX.Width;
                        ix += GP * 2;

                        Theme.DrawBox(e.Graphics, s.SeriesColor, BackColor, rtBR, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
                        Theme.DrawTextShadow(e.Graphics, null, s.Alias, Font, ForeColor, BackColor, rtTX, DvContentAlignment.MiddleLeft);
                    }
                }
                #endregion
                #region Value Axis
                if (Graduation > 0)
                {
                    for (var i = Minimum; i <= Maximum; i += Graduation)
                    {
                        var n = i;
                        var s = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                        var y = Convert.ToInt32(MathTool.Map(n, Minimum, Maximum, rtGraph.Bottom, rtGraph.Top));
                        var sz = e.Graphics.MeasureString(s, Font);
                        var rt = MathTool.MakeRectangle(new Point(0, y), Convert.ToInt32(10), Convert.ToInt32(sz.Height));
                        rt.X = rtValueAxis.Left; rt.Width = rtValueAxis.Width;
                        Theme.DrawText(e.Graphics, null, s, Font, GridColor, rt, DvContentAlignment.MiddleRight);

                        if (n == Minimum) { p.Color = GridColor; p.Width = 2; p.DashStyle = DashStyle.Solid; e.Graphics.DrawLine(p, rtGraph.Left, y + 2, rtGraph.Right, y + 2); }
                        else if (n == Maximum) { p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Solid; e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y); }
                        else { p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot; e.Graphics.DrawLine(p, rtGraph.Left, y, rtGraph.Right, y); }
                    }
                }
                #endregion
                #region Name Axis
                if (GraphDatas.Count > 0)
                {
                    e.Graphics.SetClip(rtNameAxis);

                    p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot;

                    for (int i = 0; i < GraphDatas.Count; i++)
                    {
                        var itm = GraphDatas[i];
                        var rt = new Rectangle(scv + rtNameAxis.Left + Convert.ToInt32(DataW * i), rtNameAxis.Top, Convert.ToInt32(DataW), rtNameAxis.Height);
                        if (CollisionTool.Check(rt, rtNameAxis))
                            Theme.DrawTextShadow(e.Graphics, null, itm.Name, Font, GridColor, BackColor, rt, DvContentAlignment.MiddleCenter);
                    }

                    e.Graphics.ResetClip();
                }
                #endregion
                #region Data
                if (Series.Count > 0 && GraphDatas.Count > 0)
                {
                    e.Graphics.SetClip(rtGraph);

                    var dicSer = Series.ToDictionary(x => x.Name);
                    if (GraphMode == DvBarGraphMode.LIST)
                    {
                        #region List
                        Font ft = null;

                        if (ValueDraw)
                        {
                            var len = Math.Max(Maximum.ToString().Length, Minimum.ToString().Length);
                            var pt = Convert.ToSingle(MathTool.Constrain((DataW - (GraphGap * 2)) / Series.Count / 1.33 / len, 5, 9));
                            ft = new Font(Font.FontFamily, pt, FontStyle.Regular);
                        }

                        for (int i = 0; i < GraphDatas.Count; i++)
                        {
                            var itm = GraphDatas[i];
                            var rt = new Rectangle(scv + rtGraph.Left + Convert.ToInt32(DataW * i), rtGraph.Top, Convert.ToInt32(DataW), rtGraph.Height);
                            rt.Inflate(-GraphGap, 0);

                            if (CollisionTool.Check(rt, rtGraph))
                            {
                                var iw = Convert.ToInt32((double)rt.Width / (double)Series.Count);
                                var ic = 0;
                                foreach (var vk in itm.Values.Keys)
                                {
                                    if (dicSer.ContainsKey(vk))
                                    {
                                        var n = itm.Values[vk];
                                        var h = MathTool.Map(n, Minimum, Maximum, 0, rtGraph.Height);
                                        var rtv = new Rectangle(rt.Left + (ic * iw), rtGraph.Bottom - Convert.ToInt32(h), iw, Convert.ToInt32(h));
                                        var ser = dicSer[vk];
                                        Theme.DrawBox(e.Graphics, ser.SeriesColor, bg, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT);

                                        if (ValueDraw)
                                        {
                                            br.Color = ForeColor;
                                            var txt = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                                            e.Graphics.DrawText(txt, ft, br, rtv, DvContentAlignment.BottomCenter);
                                        }
                                    }
                                    ic++;
                                }
                            }
                        }

                        if (ft != null) ft.Dispose();
                        #endregion
                    }
                    else if (GraphMode == DvBarGraphMode.STACK)
                    {
                        #region Stack
                        Font ft = null;

                        if (ValueDraw)
                        {
                            var len = Math.Max(Maximum.ToString().Length, Minimum.ToString().Length);
                            var pt = Convert.ToSingle(MathTool.Constrain((DataW - (GraphGap * 2)) / 1.33 / len, 5, 9));
                            ft = new Font(Font.FontFamily, pt, FontStyle.Regular);
                        }

                        for (int i = 0; i < GraphDatas.Count; i++)
                        {
                            var itm = GraphDatas[i];
                            var rt = new Rectangle(scv + rtGraph.Left + Convert.ToInt32(DataW * i), rtGraph.Top, Convert.ToInt32(DataW), rtGraph.Height);
                            rt.Inflate(-GraphGap, 0);
                            if (CollisionTool.Check(rt, rtGraph))
                            {
                                var iy = rt.Bottom;
                                foreach (var vk in itm.Values.Keys)
                                {
                                    if (dicSer.ContainsKey(vk))
                                    {
                                        var n = itm.Values[vk];
                                        var h = MathTool.Map(n, Minimum, Maximum, 0, rtGraph.Height);
                                        var rtv = new Rectangle(rt.X, iy - Convert.ToInt32(h), rt.Width, Convert.ToInt32(h));
                                        var ser = dicSer[vk];
                                        Theme.DrawBox(e.Graphics, ser.SeriesColor, bg, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT);
                                        if (ValueDraw)
                                        {
                                            if (n > 0)
                                            {
                                                br.Color = ForeColor;
                                                var txt = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                                                e.Graphics.DrawText(txt, ft, br, new RectangleF(rtv.X, rtv.Y, rtv.Width, rtv.Height), DvContentAlignment.BottomCenter);
                                            }
                                        }
                                        iy = rtv.Top;
                                    }
                                }
                            }
                        }

                        if (ft != null) ft.Dispose();
                        #endregion
                    }

                    e.Graphics.ResetClip();
                }
                #endregion
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                #region Scroll
                e.Graphics.SetClip(rtScroll);
                br.Color = Theme.ScrollBarColor;
                e.Graphics.FillRectangle(br, rtScroll);

                var cCur = scroll.IsScrolling ? Theme.ScrollCursorColor.BrightnessTransmit(0.3) : Theme.ScrollCursorColor;
                var rtcur = scroll.GetScrollCursorRect(rtScroll);
                if (rtcur.HasValue) Theme.DrawBox(e.Graphics, cCur, Theme.ScrollBarColor, rtcur.Value, RoundType.ALL, BoxDrawOption.BORDER);
                e.Graphics.ResetClip();
                #endregion
                #endregion
            }
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
                    {
                        var r = new GV() { Name = v.Name };
                        foreach (var sv in Series) r.Values.Add(sv.Name, (double)dic[sv.Name].GetValue(v));
                        GraphDatas.Add(r);
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
