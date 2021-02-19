using Devinno.Extensions;
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
    public class DvLineGraph : DvControl
    {
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
        /// 계열
        /// </summary>
        [Category("- 동작")]
        public List<GraphSeries> Series { get; } = new List<GraphSeries>();
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
        #region PointDraw
        private bool bPointDraw = false;
        [Category("- 모양")]
        public bool PointDraw
        {
            get => bPointDraw;
            set
            {
                if (bPointDraw != value)
                {
                    bPointDraw = value;
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

        #region GapWidth
        public int GapWidth { get; set; } = 80;
        #endregion

        private int DataW => GapWidth;

        #endregion

        #region Member Variable
        private List<GV> GraphDatas = new List<GV>();
        private Scroll scroll = new Scroll();
        #endregion

        #region Constructor
        public DvLineGraph()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new System.Drawing.Size(300, 200);

            scroll.Direction = ScrollDirection.Horizon;
            scroll.ScrollChanged += (o, s) => this.Invoke(new Action(() => Invalidate()));
            scroll.GetScrollTotal = () => GraphDatas.Count > 0 && Series.Count > 0 ? GraphDatas.Count * DataW : 0;
            scroll.GetScrollTick = () => ((Series.Count + 2) * GapWidth);
            scroll.GetScrollView = () => Areas.ContainsKey("rtGraph") ? Areas["rtGraph"].Width : 0;
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
            Focus();

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
                scroll.MouseUp(e);
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
            var rtContent = GetContentBounds();
            var f = DpiRatio;
            var GP = Convert.ToInt32(6 * f);
            var sMin = string.IsNullOrWhiteSpace(FormatString) ? Minimum.ToString("0") : Minimum.ToString(FormatString);
            var sMax = string.IsNullOrWhiteSpace(FormatString) ? Maximum.ToString("0") : Maximum.ToString(FormatString);
            var ValueAxisWidth = Convert.ToInt32(Math.Max(g.MeasureString(sMin, Font).Width * 1.5, g.MeasureString(sMax, Font).Width * 1.5));
            var NameAxisHeight = Convert.ToInt32(GP + (CHSZ.Height * 1.5));
            var RemarkAreaHeight = Convert.ToInt32(GP + (CHSZ.Height * 1.5) + GP);
            var gpTopMargin = Convert.ToInt32(CHSZ.Height) + 2;
            var rtRemark = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtContent.Bottom - RemarkAreaHeight, rtContent.Width - (ValueAxisWidth + GP), RemarkAreaHeight);
            var rtNameAxis = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtRemark.Top - GP - NameAxisHeight, rtContent.Width - (ValueAxisWidth + GP), NameAxisHeight);
            var rtValueAxis = new Rectangle(rtContent.X, rtContent.Y + gpTopMargin, ValueAxisWidth, rtNameAxis.Top - rtContent.Top - gpTopMargin);
            var rtGraphAl = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtContent.Y + gpTopMargin, rtContent.Width - (ValueAxisWidth + GP), rtValueAxis.Height);

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
                SetArea("rtScroll", new Rectangle(0, 0, 0, 0));
            }
            else
            {
                var scwh = Convert.ToInt32(Scroll.SC_WH * f);

                rtNameAxis = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtRemark.Top - GP - scwh - NameAxisHeight - GP, rtContent.Width - (ValueAxisWidth + GP), NameAxisHeight);
                rtValueAxis = new Rectangle(rtContent.X, rtContent.Y + gpTopMargin, ValueAxisWidth, rtNameAxis.Top - rtContent.Top - gpTopMargin - GP);
                rtGraphAl = new Rectangle(rtContent.X + ValueAxisWidth + GP, rtContent.Y + gpTopMargin, rtContent.Width - (ValueAxisWidth + GP), rtValueAxis.Height);

                var rtGraph = new Rectangle(rtGraphAl.Left, rtGraphAl.Top, rtGraphAl.Width, rtGraphAl.Height);
                var rtScroll = new Rectangle(rtGraph.Left, rtRemark.Top - GP - scwh, rtGraph.Width, scwh);
                rtValueAxis.Height = rtGraphAl.Height = rtGraph.Height;

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
                    var rMinimum = GraphDatas.Select(x => x.Values.Min(x2 => x2.Value)).Min();
                    var rMaximum = GraphDatas.Select(x => x.Values.Max(x2 => x2.Value)).Max();
                    Minimum = Math.Min(this.Minimum, rMinimum);
                    Maximum = Math.Max(this.Maximum, Math.Ceiling(rMaximum / Graduation) * Graduation);
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
                        var rtTX = new Rectangle(ix, rtRemarkBox.Y, Convert.ToInt32(ls[i].Width + 2), rtRemarkBox.Height);
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
                        var s = string.IsNullOrWhiteSpace(FormatString) ? n.ToString("0") : n.ToString(FormatString);
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
                    var DataW = (float)rtNameAxis.Width / (float)GraphDatas.Count;

                    e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                    foreach (var vk in dicSer.Keys)
                    {
                        var ser = dicSer[vk];

                        var ls = new List<LGV>();

                        for (int i = 0; i < GraphDatas.Count; i++)
                        {
                            var itm = GraphDatas[i];
                            var x = rtGraph.Left + Convert.ToInt32(DataW * i) + Convert.ToInt32(DataW / 2);
                            var y = Convert.ToInt32(MathTool.Map(itm.Values[vk], Minimum, Maximum, rtGraph.Bottom, rtGraph.Top));
                            ls.Add(new LGV() { Position = new Point(x, y), Value = itm.Values[vk] });
                        }

                        p.Width = 2;
                        p.DashStyle = DashStyle.Solid;

                        p.Color = ser.SeriesColor;
                        e.Graphics.DrawLines(p, ls.Select(x => x.Position).ToArray());

                        if (PointDraw || ValueDraw)
                        {
                            foreach (var v in ls)
                            {
                                var s = string.IsNullOrWhiteSpace(FormatString) ? v.Value.ToString() : v.Value.ToString(FormatString);
                                var sz = e.Graphics.MeasureString(s, Font);
                                var rt = MathTool.MakeRectangle(v.Position, 10);
                                var rtIN = new Rectangle(rt.X, rt.Y, rt.Width, rt.Height); rtIN.Inflate(-2, -2);
                                var rtTxt = MathTool.MakeRectangle(v.Position, Convert.ToInt32(sz.Width) + 2, Convert.ToInt32(sz.Height) + 2); rtTxt.Offset(0, -Convert.ToInt32(sz.Height / 2 + 3));
                                if (PointDraw)
                                {
                                    br.Color = ser.SeriesColor;
                                    e.Graphics.FillEllipse(br, rt);

                                    br.Color = BackColor;
                                    e.Graphics.FillEllipse(br, rtIN);
                                }
                                if (ValueDraw) Theme.DrawTextShadow(e.Graphics, null, s, Font, ser.SeriesColor.BrightnessTransmit(-Theme.BorderBright), BackColor, rtTxt);
                            }
                        }
                    }

                    e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                }
                #endregion
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                #endregion
            }
            else
            {
                #region Draw
                var rtScroll = Areas["rtScroll"];
                var DataW = this.DataW;
                var spos = Convert.ToInt32(scroll.ScrollPositionWithOffset);
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
                    var rMinimum = GraphDatas.Select(x => x.Values.Min(x2 => x2.Value)).Min();
                    var rMaximum = GraphDatas.Select(x => x.Values.Max(x2 => x2.Value)).Max();
                    Minimum = Math.Min(this.Minimum, rMinimum);
                    Maximum = Math.Max(this.Maximum, Math.Ceiling(rMaximum / Graduation) * Graduation);
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
                        var rtTX = new Rectangle(ix, rtRemarkBox.Y, Convert.ToInt32(ls[i].Width) +2, rtRemarkBox.Height);
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
                        var s = string.IsNullOrWhiteSpace(FormatString) ? n.ToString("0") : n.ToString(FormatString);
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
                        var rt = new Rectangle(spos + rtNameAxis.Left + Convert.ToInt32(DataW * i), rtNameAxis.Top, Convert.ToInt32(DataW), rtNameAxis.Height);
                        if (CollisionTool.Check(rt, rtNameAxis))
                            Theme.DrawTextShadow(e.Graphics, null, itm.Name, Font, GridColor, BackColor, rt, DvContentAlignment.MiddleCenter);
                    }

                    e.Graphics.ResetClip();
                }
                #endregion
                #region Data
                if (Series.Count > 0 && GraphDatas.Count > 0)
                {
                    var dicSer = Series.ToDictionary(x => x.Name);

                    e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                    e.Graphics.SetClip(new Rectangle(rtGraph.Left, rtContent.Top, rtGraph.Width, rtGraph.Bottom - rtContent.Top));
                    foreach (var vk in dicSer.Keys)
                    {
                        var ser = dicSer[vk];

                        var ls = new List<LGV>();

                        var sc = scroll.ScrollPosition;
                        var si = Convert.ToInt32(Math.Floor((double)(sc - scroll.TouchOffset) / (double)DataW));
                        var cnt = Convert.ToInt32(Math.Ceiling((double)(rtGraph.Width - Math.Min(0, scroll.TouchOffset)) / (double)DataW));
                        var ei = si + cnt;

                        for (int i = Math.Max(0, si - 1); i < ei + 1 && i < GraphDatas.Count; i++)
                        {
                            var itm = GraphDatas[i];
                            var x = spos + rtGraph.Left + Convert.ToInt32(DataW * i) + Convert.ToInt32(DataW / 2);
                            var y = Convert.ToInt32(MathTool.Map(itm.Values[vk], Minimum, Maximum, rtGraph.Bottom, rtGraph.Top));
                            ls.Add(new LGV() { Position = new Point(x, y), Value = itm.Values[vk] });
                        }

                        p.Width = 2;
                        p.DashStyle = DashStyle.Solid;

                        if (ls.Count >= 2)
                        {
                            p.Color = ser.SeriesColor;
                            e.Graphics.DrawLines(p, ls.Select(x => x.Position).ToArray());
                        }

                        p.Width = 1;
                        if (PointDraw || ValueDraw)
                        {
                            foreach (var v in ls)
                            {
                                var s = string.IsNullOrWhiteSpace(FormatString) ? v.Value.ToString() : v.Value.ToString(FormatString);
                                var sz = e.Graphics.MeasureString(s, Font);
                                var rt = MathTool.MakeRectangle(v.Position, 10);
                                var rtIN = new Rectangle(rt.X, rt.Y, rt.Width, rt.Height); rtIN.Inflate(-2, -2);
                                var rtTxt = MathTool.MakeRectangle(v.Position, Convert.ToInt32(sz.Width) + 2, Convert.ToInt32(sz.Height) + 2); rtTxt.Offset(0, -Convert.ToInt32(sz.Height / 2 + 5));

                                if (PointDraw)
                                {
                                    br.Color = ser.SeriesColor;
                                    e.Graphics.FillEllipse(br, rt);

                                    p.Color = BackColor.BrightnessTransmit(Theme.BorderBright);
                                    e.Graphics.DrawEllipse(p, rt);
                                }
                                if (ValueDraw) Theme.DrawTextShadow(e.Graphics, null, s, Font, ser.SeriesColor.BrightnessTransmit(-Theme.BorderBright), BackColor, rtTxt);
                            }
                        }
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

                var cCur = Theme.ScrollCursorColor;
                if (scroll.IsScrolling) cCur = Theme.ScrollCursorColor.BrightnessTransmit(0.3);
                else if (scroll.IsTouchMoving) cCur = Theme.PointColor.BrightnessTransmit(0.3);

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
                        GraphDatas.Add(new GV() { Name = v.Name, Props = dic, Data = v });


                    Invalidate();
                }
                else throw new Exception("잘못된 데이터 입니다.");
            }
            else throw new Exception("GraphSeries는 최소 1개 이상이어야 합니다.");
        }
        #endregion
        #endregion
    }

    #region class : LGV
    class LGV
    {
        public Point Position { get; set; }
        public double Value{ get; set; }
    }
    #endregion
}
