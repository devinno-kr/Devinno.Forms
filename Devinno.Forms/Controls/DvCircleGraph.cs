using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
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
    public class DvCircleGraph : DvControl
    {
        #region Const
        float GPM = 10F;
        #endregion

        #region Properties
        #region Series
        public List<GraphSeries> Series { get; } = new List<GraphSeries>();
        #endregion
        #region IconSize
        private int nIconSize = 12;
        public int IconSize
        {
            get => nIconSize;
            set
            {
                if(nIconSize != value)
                {
                    nIconSize = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Gradient
        private bool bGradient = true;
        public bool Gradient
        {
            get => bGradient;
            set
            {
                if(bGradient != value)
                {
                    bGradient = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ValueFont
        private Font ftValue = new Font("나눔고딕", 14, FontStyle.Regular);
        public Font ValueFont
        {
            get => ftValue;
            set
            {
                if (ftValue != value)
                {
                    ftValue = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        private List<GV> GraphDatas = new List<GV>();
        private int nSelectedIndex = -1;
        private bool bLeftSel = false;
        private bool bRightSel = false;
        private PointF mpt;

        private DateTime prev = DateTime.Now;
        #endregion

        #region Constructor
        public DvCircleGraph()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 150);

        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BorderColor = Theme.GetBorderColor(ForeColor, BackColor);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init 
            var p = new Pen(Color.Black);
            var br = new SolidBrush(Color.Black);
            #endregion

            Areas((rtContent, rtGraph, rtSelectLabel, rtSelectLeft, rtSelectRight, GP, CH) =>
            {
                if(Series.Count > 0)
                {
                    #region Selector
                    if (Series.Count > 1)
                    {
                        if (bLeftSel) rtSelectLeft.Offset(0, 1);
                        if (bRightSel) rtSelectRight.Offset(0, 1);

                        Theme.DrawIcon(e.Graphics, new DvIcon("fa-chevron-left", IconSize), ForeColor, Util.INT(rtSelectLeft));
                        Theme.DrawIcon(e.Graphics, new DvIcon("fa-chevron-right", IconSize), ForeColor, Util.INT(rtSelectRight));
                    }
                    if (nSelectedIndex >= 0 && nSelectedIndex < Series.Count)
                        Theme.DrawText(e.Graphics, Series[nSelectedIndex].Alias, Font, ForeColor, rtSelectLabel);
                    #endregion

                    #region Graph
                    if(nSelectedIndex >= 0 && nSelectedIndex < Series.Count)
                    {
                        if (GraphDatas.Count > 0)
                        {
                            #region Var
                            var ls = GraphDatas.Select(x => new CGV() { Name = x.Name, Value = x.Values[Series[nSelectedIndex].Name], Color = x.Color });
                            var cp = MathTool.CenterPoint(rtGraph);

                            var _rtGR = Util.FromRect(rtGraph.Left, rtGraph.Top, rtGraph.Width, rtGraph.Height); _rtGR.Inflate(-(_rtGR.Width / 10), -(_rtGR.Width / 10));
                            var _rtGI = Util.FromRect(rtGraph.Left, rtGraph.Top, rtGraph.Width, rtGraph.Height); _rtGI.Inflate(-(_rtGI.Width / 3), -(_rtGI.Height / 3));

                            var startAngle = 315F;
                            var sum = ls.Sum(x => x.Value);
                            var vv = (_rtGR.Width - _rtGI.Width) / 2;
                            var pt = Convert.ToSingle(MathTool.Constrain(vv / 6 / 1.33, 6, 14));
                            #endregion
                            #region Draw
                            CGV sel = null;
                            {
                                foreach (var v in ls)
                                {
                                    if (v.Value > 0)
                                    {
                                        using (var path = new GraphicsPath())
                                        {
                                            #region Var
                                            var rtGR = Util.FromRect(_rtGR.Left, _rtGR.Top, _rtGR.Width, _rtGR.Height);
                                            var rtGI = Util.FromRect(_rtGI.Left, _rtGI.Top, _rtGI.Width, _rtGI.Height);

                                            var sweepAngle = Convert.ToSingle(MathTool.Map(v.Value, 0, sum, 0, 360));
                                            var dist = Convert.ToSingle(rtGR.Width / 2F + (rtGR.Width / 12));
                                            var mcp = MathTool.GetPointWithAngle(cp, Convert.ToSingle(MathTool.Map(5, 0, 10, startAngle, startAngle + sweepAngle)), dist);
                                            var ang = MathTool.GetAngle(cp, mpt);
                                            var bSel = CollisionTool.CheckCircle(_rtGR, mpt) && !CollisionTool.CheckCircle(_rtGI, mpt) && MathTool.CompareAngle(ang, startAngle, startAngle + sweepAngle);
                                            var gpoff = bSel ? 15F : 7F;
                                            var ptOff = MathTool.GetPointWithAngle(cp, Convert.ToSingle(startAngle + sweepAngle / 2.0), gpoff);

                                            if (bSel) sel = v;
                                            #endregion
                                            #region Offset
                                            rtGI.Offset(Convert.ToSingle(ptOff.X - cp.X), Convert.ToSingle(ptOff.Y - cp.Y));
                                            rtGR.Offset(Convert.ToSingle(ptOff.X - cp.X), Convert.ToSingle(ptOff.Y - cp.Y));
                                            mcp.X += Convert.ToSingle(ptOff.X - cp.X);
                                            mcp.Y += Convert.ToSingle(ptOff.Y - cp.Y);
                                            #endregion
                                            #region Path
                                            path.AddArc(rtGI, startAngle, sweepAngle);
                                            path.AddArc(rtGR, startAngle + sweepAngle, -sweepAngle);
                                            path.CloseAllFigures();
                                            #endregion
                                            #region Fill
                                            e.Graphics.TranslateTransform(2, 2);
                                            br.Color = Color.FromArgb(Theme.OutShadowAlpha, Color.Black);
                                            e.Graphics.FillPath(br, path);
                                            e.Graphics.ResetTransform();

                                            br.Color = v.Color;
                                            e.Graphics.FillPath(br, path);
                                            #endregion
                                            #region Gredient
                                            if (Gradient)
                                            {
                                                using (var path2 = new GraphicsPath())
                                                {
                                                    path2.AddEllipse(rtGR);

                                                    using (var pbr = new PathGradientBrush(path2))
                                                    {

                                                        var giw = rtGI.Width / 2F;
                                                        var grw = rtGR.Width / 2F;
                                                        var va1 = giw / grw;
                                                        var vam = ((grw - giw) / 20F) / giw;
                                                        var cs = new Color[] { Util.FromArgb(90, Color.Black), Util.FromArgb(90, Color.Black), Util.FromArgb(20, Color.Black), Util.FromArgb(50, Color.White), Util.FromArgb(0, Color.White), Util.FromArgb(30, Color.Black) };
                                                        var ps = new float[] { 0F, 0F + va1, 0F + va1 + vam, 0.01F + va1 + vam, 0.05F + va1 + vam, 1F };


                                                        if (bSel)
                                                        {
                                                            giw = rtGI.Width / 2F - gpoff;
                                                            grw = rtGR.Width / 2F - gpoff;
                                                            va1 = giw / grw;
                                                            vam = ((grw - giw) / 20F) / giw;

                                                            var va2 = gpoff / ((grw + gpoff) / 2f);
                                                            ps = new float[] { 0F, 0F + va1, 0F + va1 + vam, 0.01F + va1 + vam, 0.05F + va1 + vam, 1F };
                                                        }

                                                        ColorBlend cb = new ColorBlend();
                                                        cb.Positions = ps;
                                                        cb.Colors = cs.Reverse().ToArray();

                                                        pbr.CenterPoint = cp;
                                                        pbr.CenterColor = v.Color;
                                                        pbr.SurroundColors = new Color[] { v.Color };
                                                        pbr.InterpolationColors = cb;

                                                        e.Graphics.FillPath(pbr, path);

                                                        p.Color = Theme.GetBorderColor(v.Color, BackColor);
                                                    }
                                                }
                                            }
                                            #endregion
                                            #region Text
                                            e.Graphics.SetClip(path);
                                            var str = v.Name + "\r\n" + v.Value + "\r\n" + (v.Value / sum).ToString("0.0%") + "";
                                            var lcp = MathTool.GetPointWithAngle(cp, Convert.ToSingle(MathTool.Map(5, 0, 10, startAngle, startAngle + sweepAngle)), Convert.ToSingle(MathTool.Map(5, 0, 10, _rtGI.Width / 2F, _rtGR.Width / 2F)) + gpoff);
                                            var szText = e.Graphics.MeasureString(str, Font);
                                            var rtPath = MathTool.MakeRectangle(lcp, szText.Width + 2, szText.Height + 2);
                                            Theme.DrawText(e.Graphics, str, Font, ForeColor, rtPath);
                                            e.Graphics.ResetClip();
                                            #endregion
                                            #region Center Text


                                            #endregion


                                            startAngle += sweepAngle;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region CurrentItem
                            if (sel != null)
                            {
                                var ptTitle = Convert.ToInt32(_rtGI.Height / 15F);
                                var ptValPer = Convert.ToInt32(_rtGI.Height / 15F);

                                var th = _rtGI.Height / 2;
                                var nh = th / 3;

                                var rtTitle = Util.FromRect(_rtGI.Left, Convert.ToInt32(_rtGI.Top + (th - th / 2) + (nh * 0)), _rtGI.Width, Convert.ToInt32(nh));
                                var rtValue = Util.FromRect(_rtGI.Left, Convert.ToInt32(_rtGI.Top + (th - th / 2) + (nh * 1)), _rtGI.Width, Convert.ToInt32(nh));
                                var rtRaito = Util.FromRect(_rtGI.Left, Convert.ToInt32(_rtGI.Top + (th - th / 2) + (nh * 2)), _rtGI.Width, Convert.ToInt32(nh));

                                Theme.DrawText(e.Graphics, sel.Name, ValueFont, ForeColor, rtTitle);
                                Theme.DrawText(e.Graphics, sel.Value.ToString(), Font, ForeColor, rtValue);
                                Theme.DrawText(e.Graphics, (sel.Value / sum).ToString("0.0%"), Font, ForeColor, rtRaito);
                            }
                            #endregion
                        }
                        else
                        {
                            #region NOT SELECTED
                            var _rtGR = Util.FromRect(rtGraph.Left, rtGraph.Top, rtGraph.Width, rtGraph.Height); _rtGR.Inflate(-(_rtGR.Width / 8), -(_rtGR.Width / 8));
                            var _rtGI = Util.FromRect(rtGraph.Left, rtGraph.Top, rtGraph.Width, rtGraph.Height); _rtGI.Inflate(-(_rtGI.Width / 4), -(_rtGI.Height / 4));

                            p.Width = 5;
                            var c = ForeColor.BrightnessTransmit(-0.5F);
                            e.Graphics.DrawEllipse(p, _rtGR);
                            Theme.DrawText(e.Graphics, "NO DATA", Font, c, rtGraph);
                            #endregion
                        }
                    }
                    else
                    {
                        #region NOT SELECTED
                        var _rtGR = Util.FromRect(rtGraph.Left, rtGraph.Top, rtGraph.Width, rtGraph.Height); _rtGR.Inflate(-(_rtGR.Width / 8), -(_rtGR.Width / 8));
                        var _rtGI = Util.FromRect(rtGraph.Left, rtGraph.Top, rtGraph.Width, rtGraph.Height); _rtGI.Inflate(-(_rtGI.Width / 4), -(_rtGI.Height / 4));

                        p.Width = 5;
                        var c = ForeColor.BrightnessTransmit(-0.5F);
                        e.Graphics.DrawEllipse(p, _rtGR);
                        Theme.DrawText(e.Graphics, "NOT SELECTED", Font, c, rtGraph);
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region Empty
                    var c = ForeColor.BrightnessTransmit(-0.5F);
                    Theme.DrawIcon(e.Graphics, new DvIcon("fa-chevron-left", IconSize), c, Util.INT(rtSelectLeft));
                    Theme.DrawIcon(e.Graphics, new DvIcon("fa-chevron-right", IconSize), c, Util.INT(rtSelectRight));

                    var _rtGR = Util.FromRect(rtGraph.Left, rtGraph.Top, rtGraph.Width, rtGraph.Height); _rtGR.Inflate(-(_rtGR.Width / 8), -(_rtGR.Width / 8));
                    var _rtGI = Util.FromRect(rtGraph.Left, rtGraph.Top, rtGraph.Width, rtGraph.Height); _rtGI.Inflate(-(_rtGI.Width / 4), -(_rtGI.Height / 4));

                    p.Width = 5;
                    p.Color = c;
                    e.Graphics.DrawEllipse(p, _rtGR);
                    Theme.DrawText(e.Graphics, "EMPTY", Font, c, rtGraph);
                    #endregion
                }

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
            Areas((rtContent, rtGraph, rtSelectLabel, rtSelectLeft, rtSelectRight, GP, CH) =>
            {
                if (Series.Count > 1)
                {
                    if (CollisionTool.Check(rtSelectLeft, x, y))
                    {
                        bLeftSel = true;
                        if (nSelectedIndex - 1 < 0) nSelectedIndex = Series.Count - 1;
                        else nSelectedIndex--;
                    }
                    if (CollisionTool.Check(rtSelectRight, x, y))
                    {
                        bRightSel = true;
                        if (nSelectedIndex + 1 >= Series.Count) nSelectedIndex = 0;
                        else nSelectedIndex++;
                    }
                }
            });

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
            int x = e.X, y = e.Y;
            mpt.X = x;
            mpt.Y = y;
            Invalidate();
            base.OnMouseMove(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, float, float> act)
        {
            using (var g = CreateGraphics())
            {
                var szCHSZ = g.MeasureIcon(new DvIcon("fa-chevron-left", IconSize));

                var CH = szCHSZ.Height;
                var GP = 10F;
                var SelectorAreaHeight = Convert.ToInt32(CH * 2);

                var rtContent = GetContentBounds();
                var rtSelector = Util.FromRect(rtContent.Left, rtContent.Bottom - SelectorAreaHeight, rtContent.Width, SelectorAreaHeight);

                #region var lsSelectLabel;
                var lsSelectLabel = new List<float>();
                foreach (var ser in Series)
                {
                    var sz = g.MeasureString(ser.Alias, Font);
                    lsSelectLabel.Add(sz.Width);
                }
                #endregion
                var wSelectLabel = Series.Count > 0 ? Convert.ToInt32(lsSelectLabel.Max(x => x + (GP * 2))) : 0;

                var rtSelectLabel = Util.MakeRectangleAlign(rtSelector, new SizeF(wSelectLabel, rtSelector.Height), DvContentAlignment.MiddleCenter);
                var rtSelectLeft = Util.FromRect(rtSelectLabel.Left - rtSelectLabel.Height, rtSelectLabel.Top, rtSelectLabel.Height, rtSelectLabel.Height);
                var rtSelectRight = Util.FromRect(rtSelectLabel.Right, rtSelectLabel.Top, rtSelectLabel.Height, rtSelectLabel.Height);

                var rt = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height - rtSelector.Height);
                var wh = Math.Min(rt.Width, rt.Height) - (GP * 2);
                var rtGraph = Util.MakeRectangleAlign(rt, new SizeF(wh, wh), DvContentAlignment.MiddleCenter);

                act(rtContent, rtGraph, rtSelectLabel, rtSelectLeft, rtSelectRight, GP, CH);
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
}
