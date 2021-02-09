using Devinno.Extensions;
using Devinno.Forms.Icons;
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
    public class DvCircleGraph : DvControl
    {
        #region Properties
        #region Series
        [Category("- 동작")]
        public List<GraphSeries> Series { get; } = new List<GraphSeries>();
        #endregion
        #endregion

        #region Member Variable
        private List<GV> GraphDatas = new List<GV>();
        private int nSelectedIndex = -1;
        private bool bLeftSel = false;
        private bool bRightSel = false;
        private Point mpt;
        #endregion

        #region Constructor
        public DvCircleGraph()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion
            Size = new Size(300, 300);
        }
        #endregion

        #region Override
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            if (Areas.Count > 1 && Series.Count>1)
            {
                if (CollisionTool.Check(Areas["rtSelectLeft"], e.Location))
                {
                    bLeftSel = true;
                    if (nSelectedIndex - 1 < 0) nSelectedIndex = Series.Count - 1;
                    else nSelectedIndex--;
                }
                if (CollisionTool.Check(Areas["rtSelectRight"], e.Location))
                {
                    bRightSel = true;
                    if (nSelectedIndex + 1 >= Series.Count) nSelectedIndex = 0;
                    else nSelectedIndex++;
                }
            }
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            bLeftSel = bRightSel = false;
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            mpt = e.Location;
            Invalidate();
            base.OnMouseMove(e);
        }
        #endregion
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var CHSZ = g.MeasureString("H", Font);
            var GP = Convert.ToInt32(10 * DpiRatio);
            var SelectorAreaHeight = Convert.ToInt32(CHSZ.Height * 2);
            var rtContent = Areas["rtContent"];
            var rtSelector = new Rectangle(rtContent.X, rtContent.Bottom - SelectorAreaHeight, rtContent.Width, SelectorAreaHeight);

            var wSelectLabel = Series.Count > 0 ? Convert.ToInt32(Series.Max(x => g.MeasureString(x.Alias, Font).Width + (GP * 2))) : 0;
            var rtSelectLabel = DrawingTool.MakeRectangleAlign(rtSelector, new Size(wSelectLabel, rtSelector.Height), DvContentAlignment.MiddleCenter);
            var rtSelectLeft = new Rectangle(rtSelectLabel.Left - rtSelectLabel.Height, rtSelectLabel.Y, rtSelectLabel.Height, rtSelectLabel.Height);
            var rtSelectRight = new Rectangle(rtSelectLabel.Right, rtSelectLabel.Y, rtSelectLabel.Height, rtSelectLabel.Height);

            var rt = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height - rtSelector.Height);
            var wh = Math.Min(rt.Width, rt.Height) - (GP * 2);
            var rtGraph = DrawingTool.MakeRectangleAlign(rt, new Size(wh, wh), DvContentAlignment.MiddleCenter);
         
            SetArea("rtGraph", rtGraph);
            SetArea("rtSelectLabel", rtSelectLabel);
            SetArea("rtSelectLeft", rtSelectLeft);
            SetArea("rtSelectRight", rtSelectRight);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            #endregion
            #region Set
            var f = DpiRatio;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            var p = new Pen(Color.Black, 2);
            var br = new SolidBrush(Color.Black);
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtGraph = Areas["rtGraph"];
            var rtSelectLabel = Areas["rtSelectLabel"];
            var rtSelectLeft = Areas["rtSelectLeft"];
            var rtSelectRight = Areas["rtSelectRight"];
            #endregion
            #region Draw
            if (Series.Count > 0)
            {
                #region Selector
                if (Series.Count > 1)
                {
                    if (bLeftSel) rtSelectLeft.Offset(0, 1);
                    if (bRightSel) rtSelectRight.Offset(0, 1);

                    Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-chevron-left") { IconSize = 12 }, null, Font, ForeColor, BackColor, rtSelectLeft);
                    Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-chevron-right") { IconSize = 12 }, null, Font, ForeColor, BackColor, rtSelectRight);
                }
                if (nSelectedIndex >= 0 && nSelectedIndex < Series.Count) Theme.DrawTextShadow(e.Graphics, null, Series[nSelectedIndex].Alias, Font, ForeColor, BackColor, rtSelectLabel);
                #endregion

                #region Graph
                if (nSelectedIndex >= 0)
                {
                    if (GraphDatas.Count > 0)
                    {
                        #region Var
                        var ls = GraphDatas.Select(x => new CGV() { Name = x.Name, Value = x.Values[Series[nSelectedIndex].Name], Color = x.Color });
                        var cp = MathTool.CenterPoint(rtGraph);

                        var _rtGR = new Rectangle(rtGraph.X, rtGraph.Y, rtGraph.Width, rtGraph.Height); _rtGR.Inflate(-(_rtGR.Width / 8), -(_rtGR.Width / 8));
                        var _rtGI = new Rectangle(rtGraph.X, rtGraph.Y, rtGraph.Width, rtGraph.Height); _rtGI.Inflate(-(_rtGI.Width / 4), -(_rtGI.Height / 4));

                        var startAngle = 315F;
                        var sum = ls.Sum(x => x.Value);
                        var vv = (_rtGR.Width - _rtGI.Width) / 2;
                        var pt = Convert.ToSingle(MathTool.Constrain(vv / 6 / 1.33, 6, 14));
                        #endregion

                        #region Draw
                        CGV sel = null;
                        using (var ft = new Font(Font.FontFamily, pt, Font.Style))
                        {
                            using (var lgbr = new LinearGradientBrush(_rtGR, Color.FromArgb(90, Color.White), Color.FromArgb(90, Color.Black), 45))
                            {
                                foreach (var v in ls)
                                {
                                    if (v.Value > 0)
                                    {
                                        using (var pth = new GraphicsPath())
                                        {
                                            #region Var
                                            var rtGR = new Rectangle(_rtGR.X, _rtGR.Y, _rtGR.Width, _rtGR.Height);
                                            var rtGI = new Rectangle(_rtGI.X, _rtGI.Y, _rtGI.Width, _rtGI.Height);

                                            var sweepAngle = Convert.ToSingle(MathTool.Map(v.Value, 0, sum, 0, 360));
                                            var dist = Convert.ToSingle(rtGI.Width - (rtGI.Width / 8));
                                            var mcp = MathTool.GetPointWithAngle(cp, Convert.ToSingle(MathTool.Map(5, 0, 10, startAngle, startAngle + sweepAngle)), dist);
                                            var ang = MathTool.GetAngle(cp, mpt);
                                            var bSel = CollisionTool.CheckCircle(_rtGR, mpt) && !CollisionTool.CheckCircle(_rtGI, mpt) && MathTool.CompareAngle(ang, startAngle, startAngle + sweepAngle);
                                            var gpoff = bSel ? 10 : Theme.ShadowGap * 2;
                                            var ptOff = MathTool.GetPointWithAngle(cp, Convert.ToSingle(startAngle + sweepAngle / 2.0), gpoff);

                                            if (bSel) sel = v;
                                            #endregion
                                            #region Offset
                                            rtGI.Offset(Convert.ToInt32(ptOff.X - cp.X), Convert.ToInt32(ptOff.Y - cp.Y));
                                            rtGR.Offset(Convert.ToInt32(ptOff.X - cp.X), Convert.ToInt32(ptOff.Y - cp.Y));
                                            mcp.X += Convert.ToInt32(ptOff.X - cp.X);
                                            mcp.Y += Convert.ToInt32(ptOff.Y - cp.Y);
                                            #endregion
                                            #region Path
                                            pth.AddArc(rtGI, startAngle, sweepAngle);
                                            pth.AddArc(rtGR, startAngle + sweepAngle, -sweepAngle);
                                            pth.CloseFigure();
                                            #endregion
                                            #region Fill
                                            using (var pth2 = pth.Clone() as GraphicsPath)
                                            {
                                                var mt = new Matrix(); mt.Translate(Theme.ShadowGap, Theme.ShadowGap);
                                                pth2.Flatten(mt);
                                                br.Color = Color.FromArgb(Theme.ShadowAlpha, Color.Black);
                                                e.Graphics.FillPath(br, pth2);
                                            }

                                            br.Color = v.Color;
                                            e.Graphics.FillPath(br, pth);
                                            e.Graphics.FillPath(lgbr, pth);
                                            #endregion
                                            #region Border
                                            p.Width = 2; p.Alignment = PenAlignment.Inset; p.Color = Color.FromArgb(Theme.BevelAlpha, Color.White);
                                            e.Graphics.DrawPath(p, pth);
                                            
                                            p.Width = 1; p.Alignment = PenAlignment.Center; p.Color = BackColor.BrightnessTransmit(Theme.BorderBright);
                                            e.Graphics.DrawPath(p, pth);
                                            #endregion
                                            #region Text
                                            var str = v.Name + "\r\n" + "" + (v.Value / sum).ToString("0.0%") + "";
                                            var sz = e.Graphics.MeasureString(str, ft);
                                            var nsz = Math.Max(Convert.ToInt32(sz.Width), Convert.ToInt32(sz.Height));
                                            var rtt = MathTool.MakeRectangle(new Point(Convert.ToInt32(mcp.X), Convert.ToInt32(mcp.Y)), nsz, nsz); rtt.Inflate(1, 1);
                                            Theme.DrawTextShadow(e.Graphics, null, str, ft, v.Color, BackColor, rtt, DvContentAlignment.MiddleCenter);
                                            #endregion

                                            startAngle += sweepAngle;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region CurrentItem
                        if (sel != null)
                        {
                            var ptTitle = _rtGI.Height / 20 / 1.33F * 1.75F;
                            var ptValPer = _rtGI.Height / 20 / 1.33F;

                            var th = _rtGI.Height / 2;
                            var nh = th / 3;

                            var rtTitle = new Rectangle(_rtGI.X, Convert.ToInt32(_rtGI.Y + (th - th / 2) + (nh * 0)), _rtGI.Width, Convert.ToInt32(nh));
                            var rtValue = new Rectangle(_rtGI.X, Convert.ToInt32(_rtGI.Y + (th - th / 2) + (nh * 1)), _rtGI.Width, Convert.ToInt32(nh));
                            var rtRaito = new Rectangle(_rtGI.X, Convert.ToInt32(_rtGI.Y + (th - th / 2) + (nh * 2)), _rtGI.Width, Convert.ToInt32(nh));

                            using (var ft = new Font(Font.FontFamily, ptTitle, FontStyle.Bold))
                            {
                                Theme.DrawTextShadow(e.Graphics, null, sel.Name, ft, ForeColor, BackColor, rtTitle);
                            }
                            using (var ft = new Font(Font.FontFamily, ptValPer, Font.Style))
                            {
                                Theme.DrawTextShadow(e.Graphics, null, sel.Value.ToString(), ft, ForeColor, BackColor, rtValue);
                            }
                            using (var ft = new Font(Font.FontFamily, ptValPer, Font.Style))
                            {
                                Theme.DrawTextShadow(e.Graphics, null, (sel.Value / sum).ToString("0.0%"), ft, ForeColor, BackColor, rtRaito);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region NO DATA
                        var _rtGR = new Rectangle(rtGraph.X, rtGraph.Y, rtGraph.Width, rtGraph.Height); _rtGR.Inflate(-(_rtGR.Width / 8), -(_rtGR.Width / 8));
                        var _rtGI = new Rectangle(rtGraph.X, rtGraph.Y, rtGraph.Width, rtGraph.Height); _rtGI.Inflate(-(_rtGI.Width / 4), -(_rtGI.Height / 4));

                        p.Width = 5;
                        var c = ForeColor.BrightnessTransmit(-0.5);
                        _rtGR.Offset(Theme.ShadowGap, Theme.ShadowGap); p.Color = BackColor.BrightnessTransmit(Theme.OutShadowBright); e.Graphics.DrawEllipse(p, _rtGR);
                        _rtGR.Offset(-Theme.ShadowGap, -Theme.ShadowGap); p.Color = c; e.Graphics.DrawEllipse(p, _rtGR);
                        Theme.DrawTextShadow(e.Graphics, null, "NO DATA", Font, c, BackColor, _rtGR);
                        #endregion
                    }
                }
                else
                {
                    #region NOT SELECTED
                    var _rtGR = new Rectangle(rtGraph.X, rtGraph.Y, rtGraph.Width, rtGraph.Height); _rtGR.Inflate(-(_rtGR.Width / 8), -(_rtGR.Width / 8));
                    var _rtGI = new Rectangle(rtGraph.X, rtGraph.Y, rtGraph.Width, rtGraph.Height); _rtGI.Inflate(-(_rtGI.Width / 4), -(_rtGI.Height / 4));

                    p.Width = 5;
                    var c = ForeColor.BrightnessTransmit(-0.5);
                    _rtGR.Offset(Theme.ShadowGap, Theme.ShadowGap); p.Color = BackColor.BrightnessTransmit(Theme.OutShadowBright); e.Graphics.DrawEllipse(p, _rtGR);
                    _rtGR.Offset(-Theme.ShadowGap, -Theme.ShadowGap); p.Color = c; e.Graphics.DrawEllipse(p, _rtGR);
                    Theme.DrawTextShadow(e.Graphics, null, "NOT SELECTED", Font, c, BackColor, _rtGR);
                    #endregion
                }
                #endregion
            }
            else
            {
                #region Empty
                var c = ForeColor.BrightnessTransmit(-0.5);
                Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-chevron-left") { IconSize = 12 }, null, Font, c, BackColor, rtSelectLeft);
                Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-chevron-right") { IconSize = 12 }, null, Font, c, BackColor, rtSelectRight);

                var _rtGR = new Rectangle(rtGraph.X, rtGraph.Y, rtGraph.Width, rtGraph.Height); _rtGR.Inflate(-(_rtGR.Width / 8), -(_rtGR.Width / 8));
                var _rtGI = new Rectangle(rtGraph.X, rtGraph.Y, rtGraph.Width, rtGraph.Height); _rtGI.Inflate(-(_rtGI.Width / 4), -(_rtGI.Height / 4));

                p.Width = 5;
                _rtGR.Offset(Theme.ShadowGap, Theme.ShadowGap); p.Color = BackColor.BrightnessTransmit(Theme.OutShadowBright); e.Graphics.DrawEllipse(p, _rtGR);
                _rtGR.Offset(-Theme.ShadowGap, -Theme.ShadowGap); p.Color = c; e.Graphics.DrawEllipse(p, _rtGR);
                Theme.DrawTextShadow(e.Graphics, null, "EMPTY", Font, c, BackColor, _rtGR);
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
                        GraphDatas.Add(new GV() { Name = v.Name, Props = dic, Data = v, Color = v.Color });

                    nSelectedIndex = 0;
                    Invalidate();
                }
                else throw new Exception("잘못된 데이터 입니다.");
            }
            else throw new Exception("GraphSeries는 최소 1개 이상이어야 합니다.");
        }
        #endregion
        #endregion
    }

    #region class : CGV
    class CGV
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public Color Color { get; set; }
    }
    #endregion

}
