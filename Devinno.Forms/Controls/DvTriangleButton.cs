using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using Devinno.Tools;
using Devinno.Forms.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Devinno.Extensions;
using System.Drawing.Drawing2D;
using Thread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;

namespace Devinno.Forms.Controls
{
    public class DvTriangleButton : DvControl
    {
        #region Properties
        #region ButtonColor
        private Color? cButtonColor = null;
        public Color? ButtonColor
        {
            get => cButtonColor;
            set
            {
                if (cButtonColor != value)
                {
                    cButtonColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BoxColor
        private Color? cBoxColor = null;
        public Color? BoxColor
        {
            get => cBoxColor;
            set
            {
                if (cBoxColor != value)
                {
                    cBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Direction
        private DvDirection eDir = DvDirection.Left;
        public DvDirection Direction
        {
            get => eDir; 
            set
            {
                if(eDir != value)
                {
                    eDir = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Corner
        private int? nCorner = null;
        public int? Corner
        {
            get => nCorner;
            set
            {
                if(nCorner != value)
                {
                    nCorner = value;
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
                if (bGradient != value)
                {
                    bGradient = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Clickable
        public bool Clickable { get; set; } = true;
        #endregion
        #region UseKey
        public bool UseKey { get; set; } = false;
        #endregion
        #endregion

        #region Member Variable
        bool bDown = false;
        #endregion

        #region Event
        public event EventHandler ButtonClick;
        #endregion

        #region Constructor
        public DvTriangleButton()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 30);

            #region KeyPress
            KeyPress += (o, s) =>
            {
                if (UseKey && Focused)
                {
                    if (s.KeyChar == '\r' || s.KeyChar == ' ')
                    {
                        var th = new Thread(() =>
                        {
                            this.Invoke(new Action(() =>
                            {
                                bDown = true;
                                Invalidate();

                            }));

                            Thread.Sleep(50);

                            this.Invoke(new Action(() =>
                            {
                                if (bDown)
                                {
                                    bDown = false;
                                    Invalidate();
                                    ButtonClick?.Invoke(this, null);
                                }
                            }));

                        })
                        { IsBackground = true };
                        th.Start();
                    }
                }
            };
            #endregion
        }
        #endregion

        #region Override
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            Areas((rtContent, rtBox, rtButton, ng) =>
            {
                #region Var
                var BoxColor = this.BoxColor ?? Theme.ConcaveBoxColor;
                var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
                var BorderColor = Theme.GetBorderColor(BoxColor, BackColor);
                var ButtonBorderColor = Theme.GetBorderColor(ButtonColor, BoxColor);

                var Corner = this.Corner ?? Theme.Corner;
                
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                #endregion
                #region Init
                var p = new Pen(Color.Black);
                var br = new SolidBrush(Color.Black);
                #endregion

                #region Box
                var ptsBox = GetPolygon(rtBox);
                using (var pth = Util.RoundCorners(ptsBox, Corner))
                {
                    #region Back
                    #region OutBevel
                    switch (Direction)
                    {
                        case DvDirection.Left:
                            {
                                var r = MathTool.GetPointWithAngle(new Point(0, 0), 100, 1.5F);
                                e.Graphics.TranslateTransform(r.X, r.Y);
                                br.Color = Color.FromArgb(Theme.OutBevelAlpha, Color.White);
                                e.Graphics.FillPath(br, pth);
                                e.Graphics.ResetTransform();
                            }
                            break;
                        case DvDirection.Right:
                            {
                                var r = MathTool.GetPointWithAngle(new Point(0, 0), 80, 1.5F);
                                e.Graphics.TranslateTransform(r.X, r.Y);
                                br.Color = Color.FromArgb(Theme.OutBevelAlpha, Color.White);
                                e.Graphics.FillPath(br, pth);
                                e.Graphics.ResetTransform();
                            }
                            break;
                        case DvDirection.Up:
                            {
                                var r = MathTool.GetPointWithAngle(new Point(0, 0), 90, 1.5F);
                                e.Graphics.TranslateTransform(r.X, r.Y);
                                br.Color = Color.FromArgb(Theme.OutBevelAlpha, Color.White);
                                e.Graphics.FillPath(br, pth);
                                e.Graphics.ResetTransform();
                            }
                            break;
                        case DvDirection.Down:
                            {
                                var r = MathTool.GetPointWithAngle(new Point(0, 0), 90, 1.5F);
                                e.Graphics.TranslateTransform(r.X, r.Y);
                                br.Color = Color.FromArgb(Theme.OutBevelAlpha, Color.White);
                                e.Graphics.FillPath(br, pth);
                                e.Graphics.ResetTransform();
                            }
                            break;
                    }
                    #endregion
                    #region Fill
                    br.Color = BoxColor;
                    e.Graphics.FillPath(br, pth);
                    #endregion
                    #region Border
                    p.Color = BorderColor;
                    e.Graphics.DrawPath(p, pth);
                    #endregion
                    #endregion
                }
                #endregion
                #region Button
                var ptsBtn = GetPolygonBtn(rtButton, ng);
                using (var pth = Util.RoundCorners(ptsBtn, Corner / 1.1F))
                {
                    if (!bDown)
                    {
                        #region Up
                        if (!Gradient)
                        {
                            #region Fill
                            br.Color = ButtonColor;
                            e.Graphics.FillPath(br, pth);

                            var rt = rtButton;
                            using (var lgbr = new LinearGradientBrush(new RectangleF(rt.X - 1, rt.Y - 1, rt.Width + 2, rt.Height + 2), Theme.GetInBevelColor(ButtonColor), Color.FromArgb(0, Color.White), 45F))
                            {
                                e.Graphics.SetClip(pth);
                                e.Graphics.TranslateTransform(1, 1);
                                using (var p2 = new Pen(lgbr, 2))
                                {
                                    e.Graphics.DrawPath(p2, pth);
                                }
                                e.Graphics.ResetTransform();
                                e.Graphics.ResetClip();
                            }
                            #endregion
                        }
                        else
                        {
                            #region Gradient
                            #region Var
                            var cS = ButtonColor.BrightnessTransmit(Theme.GradientLight);
                            var cE = ButtonColor.BrightnessTransmit(Theme.GradientDark);
                            var cS2 = Util.FromArgb(Theme.GradientLightAlpha, Color.White);
                            var cE2 = Util.FromArgb(Theme.GradientDarkAlpha, Color.Black);
                            #endregion

                            var L = ptsBtn.Min(x => x.X);
                            var R = ptsBtn.Max(x => x.X);
                            var T = ptsBtn.Min(x => x.Y);
                            var B = ptsBtn.Max(x => x.Y);

                            var rt = rtButton;
                            var ang = 45F;
                            switch (Direction)
                            {
                                case DvDirection.Up: ang = 60; break;
                                case DvDirection.Down: ang = 10; break;
                                case DvDirection.Left: ang = 30F; break;
                                case DvDirection.Right: ang = 80; break;
                            }

                            using (var lgbr = new LinearGradientBrush(new RectangleF(L - 1, T - 1, (R - L) + 2, (B - T) + 2), cS, cE, ang))
                            {
                                e.Graphics.FillPath(lgbr, pth);
                            }

                            using (var path = new GraphicsPath())
                            {
                                var n = Math.Min(rtContent.Width, rtContent.Height) * 0.1F;
                                var rtm = Util.FromRect(rtContent); rtm.Inflate(n, n);
                                path.AddEllipse(rtm);

                                using (var pbr = new PathGradientBrush(path))
                                {
                                    var tcp = Util.CenterPoint(ptsBtn.ToList());
                                    var cp = MathTool.GetPointWithAngle(tcp, 225, rtButton.Width / 4);
                                    pbr.CenterPoint = cp;
                                    pbr.CenterColor = Color.FromArgb(45, Color.White);
                                    pbr.SurroundColors = new Color[] { Color.FromArgb(30, Color.Black) };
                                    e.Graphics.FillPath(pbr, pth);
                                }
                            }

                            var cL = ButtonColor.BrightnessTransmit(1);
                            using (var lgbr = new LinearGradientBrush(new RectangleF(rt.X - 1, rt.Y - 1, rt.Width + 2, rt.Height + 2), Theme.GetInBevelColor(cL), Color.FromArgb(0, cL), 45F))
                            {
                                e.Graphics.SetClip(pth);
                                e.Graphics.TranslateTransform(1, 1);
                                using (var p2 = new Pen(lgbr, 2))
                                {
                                    e.Graphics.DrawPath(p2, pth);
                                }
                                e.Graphics.ResetTransform();
                                e.Graphics.ResetClip();
                            }

                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region Down
                        br.Color = ButtonColor.BrightnessTransmit(Theme.DownBrightness);
                        e.Graphics.FillPath(br, pth);
                        
                        var rt = rtButton;
                        e.Graphics.SetClip(pth);
                        using (var p2 = new Pen(Util.FromArgb(Theme.InShadowAlpha, Color.Black), 5))
                        {
                            e.Graphics.DrawPath(p2, pth);
                        }
                        e.Graphics.ResetClip();
                        #endregion
                    }

                    #region Border
                    p.Width = 2;
                    p.Color = BorderColor;
                    e.Graphics.DrawPath(p, pth);
                    #endregion
                }
                #endregion

                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion
            });
            base.OnThemeDraw(e, Theme);
        }

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtBox, rtButton, ng) =>
            {
                var pts = GetPolygon(rtContent);
                if (CollisionTool.CheckPolygon(pts, new PointF[] { e.Location, e.Location, e.Location }))
                {
                    bDown = true;
                    Invalidate();
                }
            });
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtBox, rtButton, ng) =>
            {
                if (bDown)
                {
                    bDown = false;
                    var pts = GetPolygon(rtContent);
                    if (CollisionTool.CheckPolygon(pts, new PointF[] { e.Location, e.Location, e.Location }))
                    {
                        ButtonClick?.Invoke(this, new EventArgs());
                    }
                    Invalidate();
                }
            });
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        public void Areas(Action<RectangleF, RectangleF, RectangleF, float> act)
        {
            var rtContent = GetContentBounds();
            var cp = MathTool.CenterPoint(rtContent);
            var rtBox = MathTool.MakeRectangle(cp, Math.Min(rtContent.Width, rtContent.Height));
            var rtButton = Util.FromRect(rtBox);
            var wh = Math.Min(rtBox.Width, rtBox.Height) * 0.1F;
            rtButton.Inflate(-wh, -wh);
            act(rtContent, rtBox, rtButton, wh);
        }
        #endregion

        #region GetPolygon
        PointF[] GetPolygon(RectangleF rt)
        {
            var wh = Convert.ToSingle(Math.Min(rt.Width, rt.Height)) / 1.75F;
            var nwh = 0F;

            var ls = new List<PointF>();
            {
                var cp = MathTool.CenterPoint(rt);
                switch (Direction)
                {
                    case DvDirection.Up:
                        {
                            var sang = -90;
                            var p1 = MathTool.GetPointWithAngle(cp, sang + 0, wh);
                            var p2 = MathTool.GetPointWithAngle(cp, sang + 120, wh);
                            var p3 = MathTool.GetPointWithAngle(cp, sang + 240, wh);

                            var my = new PointF[] { p1, p2, p3 }.Max(x => x.Y);
                            nwh = (rt.Bottom - my) / 2F;

                            p1.Y += nwh;
                            p2.Y += nwh;
                            p3.Y += nwh;

                            ls.Add(p1);
                            ls.Add(p2);
                            ls.Add(p3);
                        }
                        break;

                    case DvDirection.Down:
                        {
                            var sang = 90;
                            var p1 = MathTool.GetPointWithAngle(cp, sang + 0, wh);
                            var p2 = MathTool.GetPointWithAngle(cp, sang + 120, wh);
                            var p3 = MathTool.GetPointWithAngle(cp, sang + 240, wh);

                            var my = new PointF[] { p1, p2, p3 }.Min(x => x.Y);
                            nwh = (my - rt.Top) / 2F;

                            p1.Y += -nwh;
                            p2.Y += -nwh;
                            p3.Y += -nwh;

                            ls.Add(p1);
                            ls.Add(p2);
                            ls.Add(p3);
                        }
                        break;

                    case DvDirection.Left:
                        {
                            var sang = 180;
                            var p1 = MathTool.GetPointWithAngle(cp, sang + 0, wh);
                            var p2 = MathTool.GetPointWithAngle(cp, sang + 120, wh);
                            var p3 = MathTool.GetPointWithAngle(cp, sang + 240, wh);

                            var mx = new PointF[] { p1, p2, p3 }.Max(x => x.X);
                            nwh = (rt.Right - mx) / 2F;

                            p1.X += nwh;
                            p2.X += nwh;
                            p3.X += nwh;

                            ls.Add(p1);
                            ls.Add(p2);
                            ls.Add(p3);
                        }
                        break;

                    case DvDirection.Right:
                        {
                            var sang = 0;
                            var p1 = MathTool.GetPointWithAngle(cp, sang + 0, wh);
                            var p2 = MathTool.GetPointWithAngle(cp, sang + 120, wh);
                            var p3 = MathTool.GetPointWithAngle(cp, sang + 240, wh);

                            var mx = new PointF[] { p1, p2, p3 }.Min(x => x.X);
                            nwh = (mx - rt.Left) / 2F;

                            p1.X += -nwh;
                            p2.X += -nwh;
                            p3.X += -nwh;

                            ls.Add(p1);
                            ls.Add(p2);
                            ls.Add(p3);
                        }
                        break;
                }
            }
            return ls.Count == 3 ? ls.ToArray() : null;
        }
        #endregion
        #region GetPolygonBtn
        PointF[] GetPolygonBtn(RectangleF rt, float ng)
        {
            var wh = Convert.ToSingle(Math.Min(rt.Width, rt.Height)) /1.75F;
            var nwh = 0F;

            var ls = new List<PointF>();
            {
                var cp = MathTool.CenterPoint(rt);
                switch (Direction)
                {
                    case DvDirection.Up:
                        {
                            var sang = -90;
                            var p1 = MathTool.GetPointWithAngle(cp, sang + 0, wh);
                            var p2 = MathTool.GetPointWithAngle(cp, sang + 120, wh);
                            var p3 = MathTool.GetPointWithAngle(cp, sang + 240, wh);

                            nwh = ng;
                            p1.Y += nwh;
                            p2.Y += nwh;
                            p3.Y += nwh;

                            ls.Add(p1);
                            ls.Add(p2);
                            ls.Add(p3);
                        }
                        break;

                    case DvDirection.Down:
                        {
                            var sang = 90;
                            var p1 = MathTool.GetPointWithAngle(cp, sang + 0, wh);
                            var p2 = MathTool.GetPointWithAngle(cp, sang + 120, wh);
                            var p3 = MathTool.GetPointWithAngle(cp, sang + 240, wh);

                            nwh = ng;
                            p1.Y += -nwh;
                            p2.Y += -nwh;
                            p3.Y += -nwh;

                            ls.Add(p1);
                            ls.Add(p2);
                            ls.Add(p3);
                        }
                        break;

                    case DvDirection.Left:
                        {
                            var sang = 180;
                            var p1 = MathTool.GetPointWithAngle(cp, sang + 0, wh);
                            var p2 = MathTool.GetPointWithAngle(cp, sang + 120, wh);
                            var p3 = MathTool.GetPointWithAngle(cp, sang + 240, wh);

                            nwh = ng;
                            p1.X += nwh;
                            p2.X += nwh;
                            p3.X += nwh;

                            ls.Add(p1);
                            ls.Add(p2);
                            ls.Add(p3);
                        }
                        break;

                    case DvDirection.Right:
                        {
                            var sang = 0;
                            var p1 = MathTool.GetPointWithAngle(cp, sang + 0, wh);
                            var p2 = MathTool.GetPointWithAngle(cp, sang + 120, wh);
                            var p3 = MathTool.GetPointWithAngle(cp, sang + 240, wh);

                            nwh = ng;
                            p1.X += -nwh;
                            p2.X += -nwh;
                            p3.X += -nwh;

                            ls.Add(p1);
                            ls.Add(p2);
                            ls.Add(p3);
                        }
                        break;
                }
            }
            return ls.Count == 3 ? ls.ToArray() : null;
        }
        #endregion
        #endregion
    }
}
