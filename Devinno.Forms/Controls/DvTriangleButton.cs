using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public enum TriangleDirection { UP, DOWN, LEFT, RIGHT }
    
    [Description("버튼")]
    public class DvTriangleButton : DvControl
    {
        #region Properties
 
        #region ButtonColor
        private Color cButtonColor = DvTheme.DefaultTheme.Color3;
        [Category("- 색상")]
        public Color ButtonColor
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
        #region Text
        private new string Text
        {
            get; set;
        }
        #endregion
        #region Direction
        private TriangleDirection eDirection = TriangleDirection.UP;
        [Category("- 방향")]
        public TriangleDirection Direction
        {
            get => eDirection;
            set
            {
                if (eDirection != value)
                {
                    eDirection = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Gradient
        private bool bGradient = true;
        [Category("- 색상")]
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
        [Category("- 기능")]
        public bool Clickable { get; set; } = true;
        #endregion
        #region Corner
        private int nCorner = 5;
        public int Corner
        {
            get => nCorner;
            set
            {
                if (nCorner != value)
                {
                    nCorner = value;
                    Invalidate();
                }
            }
        }
        #endregion

        public bool UseLongClick { get => click.UseLongClick; set => click.UseLongClick = value; }
        public int LongClickTime { get => click.LongClickTime; set => click.LongClickTime = value; }

        public bool UseKey { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ButtonDownState => bDown;
        #endregion

        #region Event
        public event EventHandler LongClick;
        public event EventHandler ButtonClick;
        public event EventHandler ButtonDown;
        public event EventHandler ButtonUp;
        #endregion

        #region Member Variable
        private bool bDown = false;
        private LongClick click = new LongClick();
        private bool bKeyClick = false;
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

            click.Reset = new Action(() => { this.Invoke(new Action(() => { bDown = false; Invalidate(); })); });
            click.GenLongClick = new Action(() => { this.Invoke(new Action(() => LongClick?.Invoke(this, null))); });

            KeyPress += (o, s) =>
            {
                if (UseKey && Focused)
                {
                    if (s.KeyChar == '\r' || s.KeyChar == ' ')
                    {
                        var th = new Thread(new ThreadStart(() =>
                        {
                            this.Invoke(new Action(() =>
                            {
                                bDown = true;
                                ButtonDown?.Invoke(this, null); Invalidate();
                                Invalidate();

                            }));

                            Thread.Sleep(50);

                            this.Invoke(new Action(() =>
                            {
                                ButtonUp?.Invoke(this, null);
                                if (bDown)
                                {
                                    bDown = false;
                                    Invalidate();
                                    ButtonClick?.Invoke(this, null);
                                }
                            }));

                        }))
                        { IsBackground = true };
                        th.Start();
                    }
                }

            };
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);
            var rtContent = Areas["rtContent"];
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var ButtonColor = UseThemeColor ? Theme.Color3 : this.ButtonColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtin = new Rectangle(rtContent.X + 1, rtContent.Y + 1, rtContent.Width, rtContent.Height);
            var rtsh = new Rectangle(rtContent.X + Theme.ShadowGap, rtContent.Y + Theme.ShadowGap, rtContent.Width, rtContent.Height);
            var rtin2 = new Rectangle(rtContent.X + 1, rtContent.Y + 1, rtContent.Width - 2, rtContent.Height - 2);
            #endregion
            #region Init
            var p = new Pen(ButtonColor, 1);
            var br = new SolidBrush(ButtonColor);
            #endregion
            #region Draw
            var pts = GetPolygon(rtContent);
            var pts2 = GetPolygon(rtin);
            var pts3 = GetPolygon(rtsh);
            var pts4 = GetPolygon(rtin2);
            if (pts != null && rtContent.Width>=2 && rtContent.Height >=2)
            {
                if (pts.Length == 3)
                {
                    if (!bDown)
                    {
                        var cv = ButtonColor;
                        var cb = BackColor.BrightnessTransmit(Theme.BorderBright);
                        var cbv1 = cv.BrightnessTransmit(Theme.InBevelBright);
                        var cbv2 = Color.Transparent;
                        using (var pth = DrawingTool.RoundCorners(pts, Corner))
                        {
                            #region Shadow
                            using (var pth3 = DrawingTool.RoundCorners(pts3, Corner))
                            {
                                br.Color = BackColor.BrightnessTransmit(Theme.OutShadowBright);
                                e.Graphics.FillPath(br, pth3);
                            }
                            #endregion
                            #region Fill
                            if (Gradient)
                            {
                                var c1 = cv.BrightnessTransmit(Theme.GradientLightBright);
                                var c2 = cv.BrightnessTransmit(Theme.GradientDarkBright);
                                using (var lgbr = new LinearGradientBrush(new Rectangle(rtContent.X - 1, rtContent.Y - 1, rtContent.Width + 2, rtContent.Height + 2), c1, c2, 45))
                                {
                                    e.Graphics.FillPath(lgbr, pth);
                                }
                            }
                            else
                            {
                                br.Color = cv; 
                                e.Graphics.FillPath(br, pth);
                            }
                            #endregion
                            #region Bevel
                            e.Graphics.SetClip(pth);
                            using (var pth2 = DrawingTool.RoundCorners(pts2, Theme.Corner))
                            {
                                p.Color = cbv1; e.Graphics.DrawPath(p, pth2);
                            }
                            e.Graphics.ResetClip();
                            #endregion
                            #region Border
                            p.Color = cb; e.Graphics.DrawPath(p, pth);
                            #endregion
                        }
                    }
                    else
                    {
                        var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                        var cb = BackColor.BrightnessTransmit(Theme.BorderBright);
                        using (var pth = DrawingTool.RoundCorners(pts, Corner))
                        {
                            #region Fill
                            if (Gradient)
                            {
                                var c1 = cv.BrightnessTransmit(Theme.GradientLightBright);
                                var c2 = cv.BrightnessTransmit(Theme.GradientDarkBright);
                                using (var lgbr = new LinearGradientBrush(new Rectangle(rtContent.X - 1, rtContent.Y - 1, rtContent.Width + 2, rtContent.Height + 2), c2, c1, 45))
                                {
                                    e.Graphics.FillPath(lgbr, pth);
                                }
                            }
                            else
                            {
                                br.Color = cv;
                                e.Graphics.FillPath(br, pth);
                            }
                            #endregion
                            #region Bevel
                            e.Graphics.SetClip(pth);
                            using (var pth4 = DrawingTool.RoundCorners(pts4, Corner))
                            {
                                p.Color = cv.BrightnessTransmit(Theme.InShadowBright);
                                p.Width = 3;
                                e.Graphics.DrawPath(p, pth4);
                            }
                            e.Graphics.ResetClip();
                            #endregion
                            #region Border
                            p.Width = 1;
                            p.Color = cb; 
                            e.Graphics.DrawPath(p, pth);
                            #endregion
                        }
                    }

                    if (UseKey && Focused && !bDown)
                    {
                        var cv = ButtonColor.BrightnessTransmit(0.25);
                    }
                }
            }
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
            var rtContent = Areas["rtContent"];
            var pts = GetPolygon(rtContent);
            if (Clickable && CollisionTool.CheckPolygon(pts, new PointF[] { e.Location }))
            {
                Focus();

                bDown = true;
                ButtonDown?.Invoke(this, null);
                Invalidate();

                click.MouseDown(e);
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Clickable)
            {
                click.MouseUp(e);
                ButtonUp?.Invoke(this, null);
                if (bDown)
                {
                    bDown = false;
                    Invalidate();
                    ButtonClick?.Invoke(this, null);
                }
            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region GetPolygon
        PointF[] GetPolygon(Rectangle rt)
        {
            var ls = new List<PointF>();
            {
                var cp = MathTool.CenterPoint(rt);
                switch (Direction)
                {
                    case TriangleDirection.UP:
                        ls.Add(new PointF(cp.X, rt.Top));
                        ls.Add(new PointF(rt.Left, rt.Bottom));
                        ls.Add(new PointF(rt.Right, rt.Bottom));
                        break;

                    case TriangleDirection.DOWN:
                        ls.Add(new PointF(cp.X, rt.Bottom));
                        ls.Add(new PointF(rt.Left, rt.Top));
                        ls.Add(new PointF(rt.Right, rt.Top));
                        break;

                    case TriangleDirection.LEFT:
                        ls.Add(new PointF(rt.Left, cp.Y));
                        ls.Add(new PointF(rt.Right, rt.Top));
                        ls.Add(new PointF(rt.Right, rt.Bottom));
                        break;

                    case TriangleDirection.RIGHT:
                        ls.Add(new PointF(rt.Right, cp.Y));
                        ls.Add(new PointF(rt.Left, rt.Top));
                        ls.Add(new PointF(rt.Left, rt.Bottom));
                        break;
                }
            }
            return ls.Count == 3 ? ls.ToArray() : null;
        }
        #endregion
    }
}
