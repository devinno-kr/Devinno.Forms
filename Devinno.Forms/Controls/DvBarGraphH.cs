using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Devinno.Extensions;
using Devinno.Tools;
using Devinno.Forms.Extensions;
using System.Drawing.Drawing2D;
using Devinno.Forms.Tools;

namespace Devinno.Forms.Controls
{
    public class DvBarGraphH : DvControl
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
        public List<DvGraphSeries> Series { get; } = new List<DvGraphSeries>();
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
        #region ValueDraw
        private bool bValueDraw = false;
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
        #endregion

        #region Member Variable
        private List<GV> GraphDatas = new List<GV>();
        private bool bDown = false;
        public int mY = 0;
        public int mX = 0;
        #endregion

        #region Constructor
        public DvBarGraphH()
        {
            Size = new System.Drawing.Size(300, 200);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var GridColor = UseThemeColor ? Theme.Color4 : this.GridColor;
            var bg = GraphBackColor == Color.Transparent ? BackColor : GraphBackColor;
            #endregion
            #region Set
            var f = (float)this.LogicalToDeviceUnits(1000) / 1000F;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            var p = new Pen(Color.Black, 2);
            var br = new SolidBrush(Color.Black);

            var ShadowColor = BackColor.BrightnessTransmit(Theme.OutShadowBright);
            #endregion
            #region Bounds
            var rtContent = GetContentBounds();

            var chsz = e.Graphics.MeasureString("H", Font);
            var lsNameSize = GraphDatas.ToDictionary(x => x.Name, y => e.Graphics.MeasureString(y.Name, Font));
            var lsSerSize = Series.ToDictionary(x => x.Name, y => e.Graphics.MeasureString(y.Alias, Font));

            var GP = Convert.ToInt32(6 * f);
            var ValueAxisHeight = Convert.ToInt32(GP + chsz.Height);
            var NameAxisWidth = lsNameSize.Count > 0 ? Convert.ToInt32(lsNameSize.Values.Select(x => x.Width).Max() + GP) : 0;
            var RemarkAreaWidth = lsSerSize.Count > 0 ? Convert.ToInt32(GP + (10 * f) + (5 * f) + lsSerSize.Values.Select(x => x.Width).Max() + GP) : 0;

            var gpTopMargin = 0;
            var rtRemark = new Rectangle(rtContent.Right - RemarkAreaWidth, rtContent.Y + gpTopMargin, RemarkAreaWidth, rtContent.Height - (gpTopMargin * 2) - GP - ValueAxisHeight);
            var rtNameAxis = new Rectangle(rtContent.X, rtContent.Y + gpTopMargin, NameAxisWidth, rtContent.Height - (gpTopMargin * 2) - GP - ValueAxisHeight);
            var rtValueAxis = new Rectangle(rtNameAxis.Right + GP, rtContent.Y + gpTopMargin + rtRemark.Height + GP, rtContent.Width - (GP * 2) - rtRemark.Width - rtNameAxis.Width - GP, ValueAxisHeight);
            var rtGraph = new Rectangle(rtNameAxis.Right + GP, rtContent.Y + gpTopMargin, rtValueAxis.Width, rtContent.Height - (gpTopMargin * 2) - GP - ValueAxisHeight);
            #endregion
            #region Draw
            #region GraphBG
            if(GraphBackColor != Color.Transparent)
            {
                br.Color = GraphBackColor;
                e.Graphics.FillRectangle(br, rtGraph);
            }
            #endregion
            #region Min/Max
            var Minimum = this.Minimum;
            var Maximum = this.Maximum;
            if(GraphDatas.Count > 0)
            {
                var rMinimum = GraphMode == DvBarGraphMode.LIST ? GraphDatas.Select(x => x.Values.Min(x => x.Value)).Min() : GraphDatas.Select(x => x.Values.Sum(x => x.Value)).Min();
                var rMaximum = GraphMode == DvBarGraphMode.LIST ? GraphDatas.Select(x => x.Values.Max(x => x.Value)).Max() : GraphDatas.Select(x => x.Values.Sum(x => x.Value)).Max();
                Minimum = Math.Min(this.Minimum, rMinimum);
                Maximum = Math.Max(this.Maximum, Math.Ceiling(rMaximum / Graduation) * Graduation);
            }
            #endregion
            #region Remark
            if(Series.Count > 0)
            {
                var RemarkH = Convert.ToInt32(chsz.Height * 1.5F);
                var rtRemarkBox = DrawingTool.MakeRectangleAlign(rtRemark, new SizeF(RemarkAreaWidth, Convert.ToInt32(RemarkH * Series.Count) + GP), DvContentAlignment.MiddleRight);
                Theme.DrawBox(e.Graphics, BackColor.BrightnessTransmit(0.2), BackColor, rtRemarkBox, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
                for (int i = 0; i < Series.Count; i++)
                {
                    var s = Series[i];
                    var rt = new Rectangle(rtRemarkBox.Left, rtRemarkBox.Top + (RemarkH * i) + (GP / 2), rtRemarkBox.Width, RemarkH);
                    var rtBR = MathTool.MakeRectangle(rt, new Size(Convert.ToInt32(10 * f), Convert.ToInt32(5 * f))); rtBR.X = rt.X + GP;
                    var rtTX = new Rectangle(rtBR.Right + Convert.ToInt32(5 * f), rt.Y, rt.Width - Convert.ToInt32(5 * f) - rtBR.Width, rt.Height);

                    Theme.DrawBox(e.Graphics, s.SeriesColor, BackColor, rtBR, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
                    Theme.DrawTextShadow(e.Graphics, null, s.Alias, Font, ForeColor, BackColor, rtTX, DvContentAlignment.MiddleLeft);
                }
            }
            #endregion
            #region Value Axis
            if (Graduation > 0)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                for (var i = Minimum; i <= Maximum; i += Graduation)
                {
                    var n = i;
                    var s = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                    var x = Convert.ToInt32(MathTool.Map(n, Minimum, Maximum, rtGraph.Left, rtGraph.Right));
                    var y = rtValueAxis.Top + (rtValueAxis.Height / 2);
                    var sz = e.Graphics.MeasureString(s, Font);
                    var rt = MathTool.MakeRectangle(new Point(x, y), Convert.ToInt32(sz.Width), Convert.ToInt32(sz.Height)); rt.Inflate(2, 2);
                    Theme.DrawText(e.Graphics, null, s, Font, GridColor, rt, DvContentAlignment.MiddleCenter);

                    if (n == Minimum) { p.Color = GridColor; p.Width = 2; p.DashStyle = DashStyle.Solid; e.Graphics.DrawLine(p, x, rtGraph.Top, x, rtGraph.Bottom); }
                    if (n == Maximum) { p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Solid; e.Graphics.DrawLine(p, x, rtGraph.Top, x, rtGraph.Bottom); }
                    else { p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot; e.Graphics.DrawLine(p, x, rtGraph.Top, x, rtGraph.Bottom); }
                }
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            }
            #endregion
            #region Name Axis
            if (GraphDatas.Count > 0)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                p.Color = GridColor; p.Width = 1; p.DashStyle = DashStyle.Dot;

                var DataH = (float)rtNameAxis.Height / (float)GraphDatas.Count;
                for (int i = 0; i < GraphDatas.Count; i++)
                {
                    var itm = GraphDatas[i];
                    var rt = new Rectangle(rtNameAxis.Left, rtNameAxis.Top + Convert.ToInt32(DataH * i), rtNameAxis.Width, Convert.ToInt32(DataH));
                    Theme.DrawTextShadow(e.Graphics, null, itm.Name, Font, GridColor, BackColor, rt, DvContentAlignment.MiddleRight);
                }
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
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
                    var DataH = (float)rtNameAxis.Height / (float)GraphDatas.Count;

                    if (ValueDraw)
                    {
                        var pt = MathTool.Constrain(Convert.ToInt32(Math.Floor(((DataH - (DataH / 5D * 2D)) / (double)Series.Count - 4) / 1.33 / f)), 5, 9);
                        ft = new Font(Font.FontFamily, pt, FontStyle.Regular);
                    }

                    for (int i = 0; i < GraphDatas.Count; i++)
                    {
                        var itm = GraphDatas[i];
                        var rt = new Rectangle(rtGraph.Left, rtNameAxis.Top + Convert.ToInt32(DataH * i), rtGraph.Width, Convert.ToInt32(DataH));
                        rt.Inflate(0, -(rt.Height / 5));

                        var ih = Convert.ToInt32((double)rt.Height / (double)Series.Count);
                        var ic = 0;
                        foreach (var vk in itm.Values.Keys)
                        {
                            if (dicSer.ContainsKey(vk))
                            {
                                var n = itm.Values[vk];
                                var w = MathTool.Map(n, Minimum, Maximum, 0, rtGraph.Width);
                                var rtv = new Rectangle(rtGraph.Left, rt.Top + (ic * ih), Convert.ToInt32(w), ih);
                                var ser = dicSer[vk];
                                Theme.DrawBox(e.Graphics, ser.SeriesColor, bg, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL);
                                if (ValueDraw)
                                {
                                    var txt = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                                    Theme.DrawTextShadow(e.Graphics, null, txt, ft, ForeColor, ser.SeriesColor, rtv, DvContentAlignment.MiddleLeft);
                                }
                            }
                            ic++;
                        }
                    }

                    if (ft != null) ft.Dispose();
                    #endregion
                }
                else if(GraphMode == DvBarGraphMode.STACK)
                {
                    #region Stack
                    Font ft = null;
                    var DataH = (float)rtNameAxis.Height / (float)GraphDatas.Count;
                    
                    if (ValueDraw)
                    {
                        var pt = MathTool.Constrain(Convert.ToInt32(Math.Floor(((DataH - (DataH / 5D * 2D)) - 4) / 1.33 / f)), 5, 9);
                        ft = new Font(Font.FontFamily, pt, FontStyle.Regular);
                    }

                    for (int i = 0; i < GraphDatas.Count; i++)
                    {
                        var itm = GraphDatas[i];
                        var rt = new Rectangle(rtGraph.Left, rtNameAxis.Top + Convert.ToInt32(DataH * i), rtGraph.Width, Convert.ToInt32(DataH));
                        rt.Inflate(0, -(rt.Height / 5));

                        var ix = rt.Left;
                        foreach (var vk in itm.Values.Keys)
                        {
                            if (dicSer.ContainsKey(vk))
                            {
                                var n = itm.Values[vk];
                                var w = MathTool.Map(n, Minimum, Maximum, 0, rtGraph.Width);
                                var rtv = new Rectangle(ix, rt.Top, Convert.ToInt32(w), rt.Height);
                                var ser = dicSer[vk];
                                Theme.DrawBox(e.Graphics, ser.SeriesColor, bg, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL);
                                if (ValueDraw)
                                {
                                    var txt = string.IsNullOrWhiteSpace(FormatString) ? n.ToString() : n.ToString(FormatString);
                                    var sz = e.Graphics.MeasureString(txt, ft);
                                    //if (sz.Width + 2 < w) Theme.DrawTextShadow(e.Graphics, null, txt, ft, ForeColor, ser.SeriesColor, rtv, DvContentAlignment.MiddleCenter);
                                    if (n > 0) Theme.DrawTextShadow(e.Graphics, null, txt, ft, ForeColor, ser.SeriesColor, rtv, DvContentAlignment.MiddleLeft);
                                }
                                ix = rtv.Right;
                            }
                        }
                    }

                    if (ft != null) ft.Dispose();
                    #endregion
                }
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
            bDown = true;
            mY = e.Y;
            mX = e.X;
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            bDown = false;
            Invalidate();
            base.OnMouseUp(e);
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
