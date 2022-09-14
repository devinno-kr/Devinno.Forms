using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvWheelPicker : DvControl
    {
        #region Const
        const float GapSize = 10;
        const float GapTime = 1;
        const double decelerationRate = 0.996;
        const int ThreadInterval = 10;
        #endregion

        #region Properties
        public List<TextIcon> Items { get; private set; } = new List<TextIcon>();
        public int ItemHeight { get; set; } = 30;

        #region SelectedIndex
        private int nSelectedIndex = -1;
        public int SelectedIndex
        {
            get => nSelectedIndex;
            set
            {
                if (nSelectedIndex != value)
                {
                    nSelectedIndex = value;
                    SelectedIndexChanged?.Invoke(this, null);
                }
            }
        }
        #endregion
        #region Animation
        private bool Animation => GetTheme()?.Animation ?? false;
        #endregion

        private double TouchOffset => tcDown != null ? (tcDown.MovePoint.Y - tcDown.DownPoint.Y) : 0;
        private double ScrollPosition { get; set; }
        private double ScrollPositionWithOffset => ScrollPosition + TouchOffset;
        #endregion

        #region Member Varaible
        private TCDI tcDown = null;

        private double initPos;
        private double initVel;
        private double destPos;
        private double destTime;
        private double dCoeff = 1000.0 * Math.Log(decelerationRate);
        private double threshold = 0.1;
        private bool isScroll = false;
        private bool bU, bD;

        private Animation ani = new Animation();
        #endregion

        #region Event
        public event EventHandler SelectedIndexChanged;
        #endregion

        #region Constructor
        public DvWheelPicker()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 90);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var Corner = Theme.Corner;
            var BorderColor = ForeColor.BrightnessTransmit(Theme.BorderBrightness);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            Pen p = new Pen(Color.Black);
            #endregion

            if (Theme?.TouchMode ?? false)
            {
                Areas((rtContent, rtView, rtCenter) =>
                {
                    #region Box
                    p.Width = 1;
                    p.Color = BorderColor;

                    e.Graphics.DrawRoundRectangle(p, rtCenter, Corner);
                    #endregion
                    #region Draw
                    if (Items.Count > 0)
                    {
                        var vcnt = Convert.ToInt32(Math.Ceiling((rtCenter.Top - rtView.Top) / (float)ItemHeight));
                        var si = SelectedIndex - vcnt - (ScrollPositionWithOffset < 0 ? Math.Ceiling(ScrollPositionWithOffset / ItemHeight) : Math.Floor(ScrollPositionWithOffset / ItemHeight));
                        var sy = rtCenter.Top - (vcnt * ItemHeight);
                        var vo = ScrollPositionWithOffset % ItemHeight;

                        var ao = 0F;
                        if (ani.IsPlaying)
                        {
                            if (ani.Variable.Substring(0, 1) == "E") ao = ani.Value(AnimationAccel.DCL, Convert.ToSingle(ani.Variable.Substring(1)), 0);
                            if (ani.Variable.Substring(0, 1) == "B") ao = ani.Value(AnimationAccel.DCL, Convert.ToSingle(ani.Variable.Substring(1)), 0);
                        }

                        int i = 0;
                        while (sy < rtView.Bottom)
                        {
                            var bounds = Util.FromRect(rtView.Left, Convert.ToSingle(sy + vo + ao), rtView.Width, ItemHeight);
                            var bnd_cp = MathTool.CenterPoint(bounds);
                            var rtc_cp = MathTool.CenterPoint(rtCenter);
                            var c = Color.FromArgb(Convert.ToByte(MathTool.Constrain(MathTool.Map(Math.Abs(bnd_cp.Y - rtc_cp.Y), 0, rtView.Height / 2F, 255, 0), 0, 255)), ForeColor);
                            var v = Items[Index(si)];

                            Theme.DrawTextIcon(e.Graphics, v, Font, c, bounds);

                            sy += ItemHeight;
                            si++;
                            i++;
                        }
                    }
                    #endregion
                });
            }
            else
            {
                Areas2((rtContent, rtView, rtCenter, rtU, rtD) =>
                {
                    #region Button
                    var ih = Convert.ToInt16(DrawingTool.PixelToPt(ItemHeight / 2F));
                    if (bU) rtU.Offset(0, 1);
                    if (bD) rtD.Offset(0, 1);

                    var cU = bU ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;
                    var cD = bD ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;

                    Theme.DrawIcon(e.Graphics, new DvIcon("fa-chevron-up", ih), cU, rtU);
                    Theme.DrawIcon(e.Graphics, new DvIcon("fa-chevron-down", ih), cD, rtD);
                    #endregion
                    #region Box
                    p.Width = 1;
                    p.Color = BorderColor;

                    e.Graphics.DrawRoundRectangle(p, rtCenter, Corner);
                    #endregion
                    #region Draw
                    e.Graphics.SetClip(rtView);

                    if (Items.Count > 0)
                    {
                        var vcnt = Convert.ToInt32(Math.Ceiling((rtCenter.Top - rtView.Top) / (float)ItemHeight));
                        var vs = Convert.ToDouble(ani.IsPlaying ? ani.Value(AnimationAccel.DCL, 0, ani.Variable == "Up" ? 30 : -30) : 0);
                        var si = SelectedIndex - vcnt - (ani.IsPlaying ? (ani.Variable == "Up" ? -1 : 1) : 0);
                        var sy = rtCenter.Top - (vcnt * ItemHeight);
                        var vo = vs;

                        int i = 0;
                        while (sy < rtView.Bottom)
                        {
                            var bounds = Util.FromRect(rtView.Left, Convert.ToSingle(sy + vo), rtView.Width, ItemHeight);
                            var bnd_cp = MathTool.CenterPoint(bounds);
                            var rtc_cp = MathTool.CenterPoint(rtCenter);
                            var c = Color.FromArgb(Convert.ToByte(MathTool.Constrain(MathTool.Map(Math.Abs(bnd_cp.Y - rtc_cp.Y), 0, rtView.Height / 2F, 255, 0), 0, 255)), ForeColor);
                            var v = Items[Index(si)];

                            Theme.DrawTextIcon(e.Graphics, v, Font, c, bounds);

                            sy += ItemHeight;
                            si++;
                            i++;
                        }
                    }

                    e.Graphics.ResetClip();
                    #endregion
                });
            }

            #region Dispose
            p.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            float x = e.X, y = e.Y;

            if (GetTheme()?.TouchMode ?? false)
            {
                #region Touch
                Areas((rtContent, rtView, rtCenter) =>
                {
                    tcDown = new TCDI() { DownPoint = new PointF(x, y), MovePoint = new PointF(x, y), DownTime = DateTime.Now };
                    tcDown.List.Add(new TCMI() { Time = DateTime.Now, Point = new PointF(x, y) });
                    isScroll = false;
                    Thread.Sleep(15);
                    Invalidate();
                });
                #endregion
            }
            else
            {
                #region Button
                Areas2((rtContent, rtView, rtCenter, rtU, rtD) =>
                {
                    if(CollisionTool.Check(rtU, e.Location))
                    {
                        bU = true;
                    }

                    if (CollisionTool.Check(rtD, e.Location))
                    {
                        bD = true;
                    }

                    Invalidate();
                });
                #endregion
            }

            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            float x = e.X, y = e.Y;
           
            if (GetTheme()?.TouchMode ?? false)
            {
                #region Touch
                Areas((rtContent, rtView, rtCenter) =>
                {

                    if (tcDown != null && MathTool.GetDistance(tcDown.DownPoint, new PointF(x, y)) >= GapSize)
                    {
                        tcDown.MovePoint = new PointF(x, y);
                        tcDown.List.Add(new TCMI() { Time = DateTime.Now, Point = new PointF(x, y) });
                        Invalidate();
                    }
                });
                #endregion
            }

            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            float x = e.X, y = e.Y;

            if (GetTheme()?.TouchMode ?? false)
            {
                #region Touch
                Areas((rtContent, rtView, rtCenter) =>
                {
                    if (Items.Count > 0)
                    {
                        if (tcDown != null)
                        {
                            var sp = tcDown.List.Where(x => (DateTime.Now - x.Time).TotalMilliseconds < Scroll.GestureTime).FirstOrDefault();
                            if (sp == null) sp = tcDown.List.Count > 0 ? tcDown.List.Last() : new TCMI() { Time = tcDown.DownTime, Point = tcDown.DownPoint };

                            initPos = TouchOffset;
                            initVel = ((y - sp.Point.Y)) / ((double)(DateTime.Now - sp.Time).TotalMilliseconds / 1000.0);
                            destPos = initPos - initVel / dCoeff;
                            destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;

                            if ((DateTime.Now - sp.Time).TotalSeconds <= 0.25 && (Math.Abs(initPos - destPos) > GapSize && destTime > GapTime))
                            {
                                #region Thread
                                if (!isScroll)
                                    ThreadPool.QueueUserWorkItem((o) =>
                                    {
                                        isScroll = true;
                                        var stime = DateTime.Now;
                                        var time = 0.0;

                                        while (isScroll && time < destTime * 1000 && Convert.ToInt64(ScrollPosition) != Convert.ToInt64(destPos))
                                        {
                                            time = (DateTime.Now - stime).TotalMilliseconds;
                                            var oldV = ScrollPosition;
                                            var newV = (initPos + (Math.Pow(decelerationRate, time) - 1) / dCoeff * initVel);
                                            ScrollPosition = newV;
                                            Thread.Sleep(10);

                                            this.Invoke(new Action(() => Invalidate()));
                                        }
                                        isScroll = false;

                                        var vv = ScrollPositionWithOffset;
                                        var ti = SelectedIndex - Math.Round(vv / ItemHeight);
                                        ScrollPosition = 0;
                                        SelectedIndex = Index(ti);

                                        {
                                            var off = (vv % ItemHeight);
                                            if (off > ItemHeight / 2F) off = ItemHeight - off;
                                            else if (off < -ItemHeight / 2F) off = off + ItemHeight;

                                            ani.Stop();
                                            ani.Start(Math.Abs(off * 5), "E" + off.ToString(), () => { this.Invoke(new Action(() => Invalidate())); });
                                        }

                                    });
                                #endregion
                            }
                            else
                            {
                                if (Math.Abs(tcDown.DownPoint.Y - y) < 5 && (DateTime.Now - tcDown.DownTime).TotalMilliseconds < 300)
                                {
                                    #region Ani
                                    if (CollisionTool.Check(rtView, x, y))
                                    {
                                        var oy = y - rtCenter.Top;
                                        var ni = Convert.ToInt32(oy < 0 ? Math.Floor(oy / ItemHeight) : Math.Floor(oy / ItemHeight));

                                        SelectedIndex = Index(SelectedIndex + ni);

                                        if (Animation && !ani.IsPlaying)
                                        {
                                            ani.Stop();
                                            ani.Start(Math.Abs(ItemHeight * ni) * 5, "B" + (ItemHeight * ni), () => { this.Invoke(new Action(() => Invalidate())); });
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region Set
                                    var vv = ScrollPositionWithOffset;
                                    var ti = SelectedIndex - Math.Round(vv / ItemHeight);
                                    ScrollPosition = 0;
                                    SelectedIndex = Index(ti);
                                    #endregion
                                }
                            }

                            tcDown = null;
                        }
                    }
                    Invalidate();

                });
                #endregion
            }
            else
            {
                #region Button
                Areas2((rtContent, rtView, rtCenter, rtU, rtD) =>
                {
                    if (bU)
                    {
                        bU = false;
                        if (CollisionTool.Check(rtU, e.Location))
                        {
                            ani.Stop();
                            if (!ani.IsPlaying)
                            {
                                if (Items.Count > 0)
                                {
                                    SelectedIndex--;
                                    if (SelectedIndex < 0) SelectedIndex = Items.Count - 1;
                                }
                                else SelectedIndex = -1;
                                ani.Start(250, "Up", () => this.Invoke(new Action(() => Invalidate())));
                            }
                        }
                    }

                    if (bD)
                    {
                        bD = false;
                        if (CollisionTool.Check(rtD, e.Location))
                        {
                            ani.Stop();
                            if (!ani.IsPlaying)
                            {
                                if (Items.Count > 0)
                                {
                                    SelectedIndex++;
                                    if (SelectedIndex >= Items.Count) SelectedIndex = 0;
                                }
                                else SelectedIndex = -1;
                                ani.Start(250, "Down", () => this.Invoke(new Action(() => Invalidate())));
                            }
                        }
                    }

                    Invalidate();
                });
                #endregion
            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Index
        int Index(double i)
        {
            var idx = Convert.ToInt32(i) % Items.Count;

            if (idx >= Items.Count) idx -= Items.Count;
            if (idx < 0) idx += Items.Count;

            return idx;
        }
        #endregion
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtView = rtContent;
            var rtCenter = Util.MakeRectangle(rtView, new SizeF(rtContent.Width, ItemHeight));

            var viewCount = Convert.ToInt32(Math.Ceiling(rtContent.Height / ItemHeight));

            act(rtContent, rtView, rtCenter);
        }

        void Areas2(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtU = Util.MakeRectangleAlign(rtContent, new SizeF(rtContent.Width, ItemHeight), DvContentAlignment.TopCenter);
            var rtD = Util.MakeRectangleAlign(rtContent, new SizeF(rtContent.Width, ItemHeight), DvContentAlignment.BottomCenter);
            var rtView = new RectangleF(rtContent.X, rtU.Bottom, rtContent.Width, rtD.Top - rtU.Bottom);
            var rtCenter = Util.MakeRectangle(rtView, new SizeF(rtContent.Width, ItemHeight));

            var viewCount = Convert.ToInt32(Math.Ceiling(rtContent.Height / ItemHeight));
           
            act(rtContent, rtView, rtCenter, rtU, rtD);
        }
        #endregion
        #endregion
    }
}
