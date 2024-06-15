using Devinno.Collections;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
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
    public class DvToolBox : DvControl
    {
        #region Const
        const int StartIndent = 10;
        //const int RadioSize = 6;
        #endregion

        #region Properties
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
        #region CategoryColor
        private Color? cCategoryColor = null;
        public Color? CategoryColor
        {
            get => cCategoryColor;
            set
            {
                if (cCategoryColor != value)
                {
                    cCategoryColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Categories
        public EventList<ToolCategoryItem> Categories { get; } = new EventList<ToolCategoryItem>();
        #endregion

        #region RadioSize
        private int nRadioSize = 16;
        public int RadioSize
        {
            get => nRadioSize;
            set
            {
                if (nRadioSize != value)
                {
                    nRadioSize = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ItemHeight
        private int nItemHeight = 30;
        public int ItemHeight
        {
            get => nItemHeight;
            set
            {
                if (nItemHeight != value)
                {
                    nItemHeight = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region CategoryHeight
        private int nCategoryHeight = 30;
        public int CategoryHeight
        {
            get => nCategoryHeight;
            set
            {
                if (nCategoryHeight != value)
                {
                    nCategoryHeight = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Round
        private RoundType? round = null;
        public RoundType? Round
        {
            get => round;
            set
            {
                if (round != value)
                {
                    round = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BackgroundDraw
        private bool bBackgroundDraw = true;
        public bool BackgroundDraw
        {
            get => bBackgroundDraw;
            set
            {
                if (bBackgroundDraw != value)
                {
                    bBackgroundDraw = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region ScrollBar
        internal bool _IsScrolling => scroll.IsTouchMoving || (scroll.IsTouchScrolling && scroll.TouchOffset != 0);
        internal bool IsScrolling { get; private set; }
        internal bool DrawScroll { get; set; } = true;
        internal bool Scrollable => scroll.ScrollView < scroll.ScrollTotal;
        public double ScrollPosition
        {
            get => scroll.ScrollPosition;
            set => scroll.ScrollPosition = value;
        }
        #endregion

        #region Animation
        private bool Animation => GetTheme()?.Animation ?? false;
        #endregion

        public DvContentAlignment ContentAlignment { get; set; } = DvContentAlignment.MiddleCenter;
        public int IndentWidth { get; set; } = 30;
        #endregion

        #region Member Variable
        private Scroll scroll;
        private Animation ani = new Animation();
        private int mx = 0, my = 0;
        private ToolCategoryItem aniItem = null;
        private List<TextIcon> ls = new List<TextIcon>();
        #endregion

        #region Event
        public event EventHandler<ToolItemMouseDownEventArgs> ItemDown;
        public event EventHandler<ToolItemMouseEventArgs> ItemUp;
        public event EventHandler<ToolItemMouseEventArgs> ItemClicked;
        public event EventHandler<ToolItemMouseEventArgs> ItemDoubleClicked;
        #endregion

        #region Constructor
        public DvToolBox()
        {
            scroll = new Scroll() { Direction = ScrollDirection.Vertical, TouchMode = true };
            scroll.GetScrollTotal = () => (ls.Select(x => x is ToolCategoryItem ? CategoryHeight : ItemHeight).Sum()) + (Animation && ani.IsPlaying ?
            (aniItem.Expands ? -ani.Value(AnimationAccel.DCL, aniItem.Items.Count * ItemHeight, 0)
                             : -ani.Value(AnimationAccel.DCL, 0, aniItem.Items.Count * ItemHeight))
            : 0);
            scroll.GetScrollTick = () => ItemHeight;
            scroll.GetScrollView = () => (this.Height - 2);
            scroll.GetConstrainIgnore = () => Animation && ani.IsPlaying;
            scroll.ScrollChanged += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            scroll.ScrollEnded += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };

        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BoxColor = this.BoxColor ?? Theme.ListBackColor;
            var CategoryColor = this.CategoryColor ?? Theme.RowColor;
            var BorderColor = Theme.GetBorderColor(BoxColor, BackColor);
            var CategoryBorderColor = Theme.GetBorderColor(CategoryColor, BackColor);
            var ScrollBorderColor = Theme.GetBorderColor(Theme.ScrollBarColor, BackColor);
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            var p = new Pen(Color.Black);
            var br = new SolidBrush(Color.Black);
            #endregion

            scroll.TouchMode = Theme.TouchMode;

            Areas((rtContent, rtBox, rtScroll) =>
            {
                #region Round
                var rndBox = RoundType.L;
                var rndScroll = RoundType.R;

                if (!BackgroundDraw)
                {
                    rndScroll = RoundType.All;
                }
                else if (!DrawScroll)
                {
                    rndBox = Round == RoundType.Ellipse ? RoundType.Rect : Round;
                    rndScroll = RoundType.Rect;
                }
                else
                {
                    switch (Round)
                    {
                        case RoundType.Ellipse: rndBox = RoundType.Rect; rndScroll = RoundType.Rect; break;
                        case RoundType.Rect: rndBox = RoundType.Rect; rndScroll = RoundType.Rect; break;

                        case RoundType.L: rndBox = RoundType.L; rndScroll = RoundType.Rect; break;
                        case RoundType.R: rndBox = RoundType.Rect; rndScroll = RoundType.R; break;
                        case RoundType.T: rndBox = RoundType.LT; rndScroll = RoundType.RT; break;
                        case RoundType.B: rndBox = RoundType.LB; rndScroll = RoundType.RB; break;

                        case RoundType.LT: rndBox = RoundType.LT; rndScroll = RoundType.Rect; break;
                        case RoundType.RT: rndBox = RoundType.Rect; rndScroll = RoundType.RT; break;
                        case RoundType.LB: rndBox = RoundType.LB; rndScroll = RoundType.Rect; break;
                        case RoundType.RB: rndBox = RoundType.Rect; rndScroll = RoundType.RB; break;

                        case RoundType.All: rndBox = RoundType.L; rndScroll = RoundType.R; break;
                    }
                }
                #endregion

                #region Items
                if (BackgroundDraw) Theme.DrawBox(e.Graphics, rtBox, BoxColor, BorderColor, rndBox, Box.ListBox(ShadowGap));

                using (var path = GetPath(Theme, Round, rtContent))
                {
                    e.Graphics.SetClip(path);

                    #region Items
                    loop((i, rt, itm, rtv, rtRadio, rtText, rtRow) =>
                    {
                        if (CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            var cRow = itm is ToolCategoryItem ? CategoryColor : BoxColor;
                            var idnt = (itm is ToolCategoryItem ? StartIndent : StartIndent + (IndentWidth / 2));

                            if (itm is ToolCategoryItem)
                            {
                                var v = itm as ToolCategoryItem;

                                Theme.DrawBox(e.Graphics, rtv, cRow, BorderColor, RoundType.Rect, BoxStyle.Border | BoxStyle.GradientV);
                                Theme.DrawTextIcon(e.Graphics, v, Font, ForeColor, rtText);

                                #region Radio
                                var wh = 12;
                                var bw = 3;
                                var cp = MathTool.CenterPoint(rtRadio);
                                br.Color = ForeColor;

                                if (Animation && ani.IsPlaying && v == aniItem)
                                {
                                    wh = Math.Abs(ani.Value(AnimationAccel.DCL, -9, 9)) + 3;

                                    var rtH = DrawingTool.GetRoundRectPath(Util.MakeRectangle(cp, wh, bw), bw);
                                    var rtV = DrawingTool.GetRoundRectPath(Util.MakeRectangle(cp, bw, wh), bw);

                                    if (v.Expands)
                                    {
                                        e.Graphics.FillPath(br, rtH);
                                    }
                                    else
                                    {
                                        e.Graphics.FillPath(br, rtV);
                                        e.Graphics.FillPath(br, rtH);
                                    }

                                    rtH.Dispose();
                                    rtV.Dispose();
                                }
                                else
                                {
                                    var rtH = DrawingTool.GetRoundRectPath(Util.MakeRectangle(cp, wh, bw), bw);
                                    var rtV = DrawingTool.GetRoundRectPath(Util.MakeRectangle(cp, bw, wh), bw);

                                    if (v.Expands)
                                    {
                                        e.Graphics.FillPath(br, rtH);
                                    }
                                    else
                                    {
                                        e.Graphics.FillPath(br, rtV);
                                        e.Graphics.FillPath(br, rtH);
                                    }

                                    rtH.Dispose();
                                    rtV.Dispose();
                                }
                                #endregion
                            }
                            else
                            {
                                if (CollisionTool.Check(rtRow, mx, my)) Theme.DrawBox(e.Graphics, rtRow, Util.FromArgb(30, Color.White), BorderColor, RoundType.All, BoxStyle.Border | BoxStyle.Fill | BoxStyle.OutBevel);
                                var rtdot = Util.MakeRectangle(rtRadio, new SizeF(5, 5));
                                Theme.DrawTextIcon(e.Graphics, itm, Font, ForeColor, rtText);
                                Theme.DrawBox(e.Graphics, rtdot, ForeColor, BorderColor, RoundType.Ellipse, BoxStyle.Fill | BoxStyle.OutShadow);
                            }
                        }
                    });
                    #endregion

                    e.Graphics.ResetClip();
                }

                if (BackgroundDraw) Theme.DrawBox(e.Graphics, rtBox, BoxColor, BorderColor, rndBox, Box.Border());
                #endregion

                #region Scroll
                if (DrawScroll)
                {
                    Theme.DrawBox(e.Graphics, rtScroll, Theme.ScrollBarColor, Color.Black, rndScroll, Box.FlatBox(true, true));

                    e.Graphics.SetClip(Util.FromRect(rtScroll.Left, rtScroll.Top + 0, rtScroll.Width, rtScroll.Height - 0));

                    var cCur = Theme.ScrollCursorOffColor;
                    if (scroll.IsScrolling || scroll.IsTouchMoving) cCur = Theme.ScrollCursorOnColor;

                    var rtcur = scroll.GetScrollCursorRect(rtScroll);
                    if (rtcur.HasValue) Theme.DrawBox(e.Graphics, rtcur.Value, cCur, ScrollBorderColor, RoundType.All, BoxStyle.Fill);

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

            Areas((rtContent, rtBox, rtScroll) =>
            {
                scroll.MouseDown(x, y, rtScroll);
                if (scroll.TouchMode && CollisionTool.Check(rtBox, x, y)) scroll.TouchDown(x, y);

                IsScrolling = _IsScrolling;

                if (CollisionTool.Check(rtBox, x, y))
                {
                    loop((i, rt, itm, rtv, rtRadio, rtText, rtRow) =>
                    {
                        var v = itm as ToolItem;
                        if (v != null && CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            if (CollisionTool.Check(rtRow, x, y))
                            {
                                var arg = new ToolItemMouseDownEventArgs(x, y, v);
                                ItemDown?.Invoke(this, arg);

                                if (arg.Drag)
                                {
                                    scroll.MouseUp(x, y);
                                    if (scroll.TouchMode && CollisionTool.Check(rtBox, x, y)) scroll.TouchUp(x, y);

                                    DoDragDrop(v, DragDropEffects.Copy);
                                }
                            }
                        }
                    });
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
            mx = x;
            my = y;

            Areas((rtContent, rtBox, rtScroll) =>
            {
                scroll.MouseMove(x, y, rtScroll);
                if (scroll.TouchMode) scroll.TouchMove(x, y);

                IsScrolling = _IsScrolling;
            });

            Invalidate();
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            Areas((rtContent, rtBox, rtScroll) =>
            {
                IsScrolling = _IsScrolling;

                scroll.MouseUp(x, y);
                if (scroll.TouchMode) scroll.TouchUp(x, y);

                if (CollisionTool.Check(rtBox, x, y))
                {
                    loop((i, rt, itm, rtv, rtRadio, rtText, rtRow) =>
                    {
                        if (CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            var vcat = itm as ToolCategoryItem;
                            var v = itm as ToolItem;

                            if (vcat != null && CollisionTool.Check(rtRadio, x, y))
                            {
                                vcat.Expands = !vcat.Expands;

                                if (Animation)
                                {
                                    aniItem = vcat;

                                    ani.Stop();
                                    ani.Start(150, "", () => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); });
                                }
                            }
                            if (CollisionTool.Check(rtRow, x, y)) ItemUp?.Invoke(this, new ToolItemMouseEventArgs(x, y, v));

                        }
                    });
                }
            });

            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseWheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Areas((rtContent, rtBox, rtScroll) =>
            {
                if (CollisionTool.Check(rtContent, e.Location))
                {
                    ((HandledMouseEventArgs)e).Handled = true;
                    scroll.MouseWheel(e.Delta, rtScroll);
                    Invalidate();
                }
            });
            base.OnMouseWheel(e);
        }
        #endregion
        #region OnMouseClick
        protected override void OnMouseClick(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            Areas((rtContent, rtBox, rtScroll) =>
            {
                if (CollisionTool.Check(rtBox, x, y) && !IsScrolling)
                {
                    loop((i, rt, itm, rtv, rtRadio, rtText, rtRow) =>
                    {
                        var v = itm as ToolItem;
                        if (v != null && CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            if (CollisionTool.Check(rtRow, x, y)) ItemClicked?.Invoke(this, new ToolItemMouseEventArgs(x, y, v));
                        }
                    });
                }
            });

            base.OnMouseClick(e);
        }
        #endregion
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            Areas((rtContent, rtBox, rtScroll) =>
            {
                if (CollisionTool.Check(rtBox, x, y) && !IsScrolling)
                {
                    loop((i, rt, itm, rtv, rtRadio, rtText, rtRow) =>
                    {
                        var v = itm as ToolItem;
                        if (v != null && CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            if (CollisionTool.Check(rtRow, x, y)) ItemDoubleClicked?.Invoke(this, new ToolItemMouseEventArgs(x, y, v));
                        }
                    });
                }
            });

            base.OnMouseDoubleClick(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF> act)
        {
            var GAP = 0;
            var scwh = Convert.ToInt32(Scroll.SC_WH);
            var rtContent = GetContentBounds();
            var rtBox = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width - scwh - GAP, rtContent.Height);
            var rtScroll = Util.FromRect(rtBox.Right + GAP, rtBox.Top, scwh, rtBox.Height);

            act(rtContent, rtBox, rtScroll);
        }
        #endregion
        #region loop
        private void loop(Action<int, RectangleF, TextIcon, RectangleF, RectangleF, RectangleF, RectangleF> Func)
        {
            var vls = new List<TextIcon>();
            MakeList(vls);
            ls = vls;

            Areas((rtContent, rtBox, rtScroll) =>
            {
                var sc = scroll.ScrollPosition;
                var spos = Convert.ToInt32(scroll.ScrollPositionWithOffset);

                using (var g = CreateGraphics())
                {
                    if (Animation && ani.IsPlaying)
                    {
                        if (aniItem != null)
                        {

                            var anils = aniItem.Items.Cast<TextIcon>().ToList();
                            var nh = anils.Select(x => x is ToolCategoryItem ? CategoryHeight : ItemHeight).Sum();
                            var anih = aniItem.Expands ? ani.Value(AnimationAccel.DCL, 0, nh) : ani.Value(AnimationAccel.DCL, nh, 0);

                            var si = Convert.ToInt32(Math.Floor((double)Math.Abs(spos) / (double)ItemHeight));   //0;
                            var ei = ls.Count - 1;

                            var i = Math.Max(0, si);
                            var y = spos + rtBox.Top + (si * ItemHeight);

                            while (i < ei + 1 && i < ls.Count && y <= rtBox.Bottom)
                            {
                                var itm = ls[i];
                                var ItemHeight = itm is ToolCategoryItem ? this.CategoryHeight : this.ItemHeight;
                                var rt = Util.FromRect(rtBox.Left, y, rtBox.Width, ItemHeight);
                                if (CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                                {
                                    if (anils.Contains(itm))
                                    {
                                        var idx = anils.IndexOf(itm);
                                        var bView = false;
                                        if (anih >= idx * ItemHeight + ItemHeight)
                                        {
                                            bView = true;
                                            y += ItemHeight;
                                        }
                                        else if (anih >= idx * ItemHeight && anih < idx * ItemHeight + ItemHeight)
                                        {
                                            //bView = true;
                                            rt = Util.FromRect(rtBox.Left, y, rtBox.Width, anih % ItemHeight);
                                            y += anih % ItemHeight;
                                        }

                                        if (bView)
                                        {
                                            var sz = g.MeasureTextIcon(itm, Font);
                                            var w = Convert.ToInt32(sz.Width);
                                            var h = Convert.ToInt32(sz.Height);

                                            var SW = 10;
                                            var GP = 10;
                                            var rtv = Util.FromRect(rt.Left, rt.Top, rt.Width, rt.Height);

                                            if (itm is ToolCategoryItem)
                                            {
                                                var rtRadio = Util.MakeRectangleAlign(Util.FromRect(rtv.Left, rt.Top, rt.Height, rt.Height), new SizeF(RadioSize, RadioSize), DvContentAlignment.MiddleCenter);
                                                var rtText = Util.FromRect(rtRadio.Right + GP, rt.Top, w + 2, rt.Height);
                                                var rtRow = new RectangleF(rtRadio.Left, rtText.Top, rtText.Right - rtRadio.Left, rtText.Height);

                                                Func(i, rt, itm, rtv, rtRadio, rtText, rtRow);
                                            }
                                            else
                                            {
                                                var ro = 5;
                                                var rtz = Util.FromRect(rtv); rtz.Inflate(-IndentWidth, 0);
                                                var rtRow = Util.MakeRectangleAlign(rtz, new SizeF(ro + GP + w, rtv.Height), ContentAlignment);
                                                var cp = MathTool.CenterPoint(rtRow);
                                                var rtRadio = Util.FromRect(rtRow.Left, cp.Y - (ro / 2), ro, ro);
                                                var rtText = Util.FromRect(rtRadio.Right + GP, rt.Top, w + 2, rt.Height);
                                                rtRow.Inflate(8, 0);

                                                Func(i, rt, itm, rtv, rtRadio, rtText, rtRow);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var sz = g.MeasureTextIcon(itm, Font);
                                        var w = Convert.ToInt32(sz.Width);
                                        var h = Convert.ToInt32(sz.Height);

                                        var SW = 10;
                                        var GP = 10;
                                        var rtv = Util.FromRect(rt.Left, rt.Top, rt.Width, rt.Height);

                                        if (itm is ToolCategoryItem)
                                        {
                                            var rtRadio = Util.MakeRectangleAlign(Util.FromRect(rtv.Left, rt.Top, rt.Height, rt.Height), new SizeF(RadioSize, RadioSize), DvContentAlignment.MiddleCenter);
                                            var rtText = Util.FromRect(rtRadio.Right + GP, rt.Top, w + 2, rt.Height);
                                            var rtRow = new RectangleF(rtRadio.Left, rtText.Top, rtText.Right - rtRadio.Left, rtText.Height);

                                            Func(i, rt, itm, rtv, rtRadio, rtText, rtRow);
                                        }
                                        else
                                        {
                                            var ro = 5;
                                            var rtz = Util.FromRect(rtv); rtz.Inflate(-IndentWidth, 0);
                                            var rtRow = Util.MakeRectangleAlign(rtz, new SizeF(ro + GP + w, rtv.Height), ContentAlignment);
                                            var cp = MathTool.CenterPoint(rtRow);
                                            var rtRadio = Util.FromRect(rtRow.Left, cp.Y - (ro / 2), ro, ro);
                                            var rtText = Util.FromRect(rtRadio.Right + GP, rt.Top, w + 2, rt.Height);
                                            rtRow.Inflate(8, 0);

                                            Func(i, rt, itm, rtv, rtRadio, rtText, rtRow);
                                        }

                                        y += ItemHeight;
                                    }
                                }
                                else y += ItemHeight;
                                i++;
                            }
                        }
                    }
                    else
                    {
                        var si = Convert.ToInt32(Math.Floor((double)(sc - scroll.TouchOffset) / (double)ItemHeight));
                        var cnt = Convert.ToInt32(Math.Ceiling((double)(rtBox.Height - Math.Min(0, scroll.TouchOffset)) / (double)ItemHeight));
                        var ei = si + cnt;

                        var ysum = ls.GetRange(0, Math.Max(0, si)).Sum(x => x is ToolCategoryItem ? this.CategoryHeight : this.ItemHeight);
                        for (int i = Math.Max(0, si); i < ei + 1 && i < ls.Count; i++)
                        {
                            var itm = ls[i];
                            var ItemHeight = itm is ToolCategoryItem ? this.CategoryHeight : this.ItemHeight;
                            var rt = Util.FromRect(rtBox.Left, spos + rtBox.Top + ysum, rtBox.Width, ItemHeight);
                            if (CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                            {
                                var sz = g.MeasureTextIcon(itm, Font);
                                var w = Convert.ToInt32(sz.Width);
                                var h = Convert.ToInt32(sz.Height);

                                var SW = 10;
                                var GP = 10;
                                var rtv = Util.FromRect(rt.Left, rt.Top, rt.Width, rt.Height);

                                if (itm is ToolCategoryItem)
                                {
                                    var rtRadio = Util.MakeRectangleAlign(Util.FromRect(rtv.Left, rt.Top, rt.Height, rt.Height), new SizeF(RadioSize, RadioSize), DvContentAlignment.MiddleCenter);
                                    var rtText = Util.FromRect(rtRadio.Right + GP, rt.Top, w + 2, rt.Height);
                                    var rtRow = new RectangleF(rtRadio.Left, rtText.Top, rtText.Right - rtRadio.Left, rtText.Height);

                                    Func(i, rt, itm, rtv, rtRadio, rtText, rtRow);
                                }
                                else
                                {
                                    var ro = 5;
                                    var rtz = Util.FromRect(rtv); rtz.Inflate(-IndentWidth, 0);
                                    var rtRow = Util.MakeRectangleAlign(rtz, new SizeF(ro + GP + w, rtv.Height), ContentAlignment);
                                    var cp = MathTool.CenterPoint(rtRow);
                                    var rtRadio = Util.FromRect(rtRow.Left, cp.Y - (ro / 2), ro, ro);
                                    var rtText = Util.FromRect(rtRadio.Right + GP, rt.Top, w + 2, rt.Height);
                                    rtRow.Inflate(8, 0);

                                    Func(i, rt, itm, rtv, rtRadio, rtText, rtRow);
                                }
                            }
                            ysum += ItemHeight;
                        }
                    }
                }
            });
        }
        #endregion
        #region GetPath
        GraphicsPath GetPath(DvTheme theme, RoundType round, RectangleF rtContent)
        {
            GraphicsPath ret = null;
            switch (round)
            {
                case RoundType.Ellipse: ret = new GraphicsPath(); ret.AddRectangle(rtContent); break;
                case RoundType.Rect: ret = new GraphicsPath(); ret.AddRectangle(rtContent); break;

                case RoundType.L: ret = DrawingTool.GetRoundRectPathL(rtContent, theme.Corner); break;
                case RoundType.R: ret = DrawingTool.GetRoundRectPathR(rtContent, theme.Corner); break;
                case RoundType.T: ret = DrawingTool.GetRoundRectPathT(rtContent, theme.Corner); break;
                case RoundType.B: ret = DrawingTool.GetRoundRectPathB(rtContent, theme.Corner); break;

                case RoundType.LT: ret = DrawingTool.GetRoundRectPathLT(rtContent, theme.Corner); break;
                case RoundType.RT: ret = DrawingTool.GetRoundRectPathRT(rtContent, theme.Corner); break;
                case RoundType.LB: ret = DrawingTool.GetRoundRectPathLB(rtContent, theme.Corner); break;
                case RoundType.RB: ret = DrawingTool.GetRoundRectPathRB(rtContent, theme.Corner); break;

                case RoundType.All: ret = DrawingTool.GetRoundRectPath(rtContent, theme.Corner); break;
            }
            return ret;
        }
        #endregion

        #region GetToolItem
        public ToolItem GetToolItem(int mx, int my)
        {
            ToolItem ret = null;
            #region Items
            loop((i, rt, v, rtv, rtRadio, rtText, rtRow) =>
            {
                if (v is ToolItem)
                {
                    var itm = (ToolItem)v;
                    if (CollisionTool.Check(rtRow, mx, my))
                    {
                        ret = itm;
                    }
                }
            });
            #endregion
            return ret;
        }
        #endregion
        #region MakeList
        void MakeList(List<TextIcon> ls)
        {
            ls.Clear();
            foreach (var v in Categories)
            {
                if (v.Expands || (Animation && ani.IsPlaying && aniItem == v)) { ls.Add(v); ls.AddRange(v.Items); }
                else ls.Add(v);
            }
        }
        #endregion
        #region GetListCount
        int GetListCount() => ls.Count;
        #endregion
        #endregion
    }

    #region class : ToolCategoryItem
    public class ToolCategoryItem : TextIcon 
    {
        #region Properties
        public bool Expands { get; set; } = true;
        public EventList<ToolItem> Items { get; } = new EventList<ToolItem>();
        #endregion

        #region Event
        internal event EventHandler Changed;
        #endregion

        #region Constructor
        public ToolCategoryItem(string Text)
        {
            this.Text = Text;

            Items.Changed += (o, s) => Changed?.Invoke(this, null);
        }
        public ToolCategoryItem(string Text, string IconString)
        {
            this.Text = Text;
            this.IconString = IconString;

            Items.Changed += (o, s) => Changed?.Invoke(this, null);
        }
        #endregion
    }
    #endregion
    #region class : ToolItem
    public class ToolItem : TextIcon 
    {
        #region Constructor
        public ToolItem(string Text)
        {
            this.Text = Text;
        }
        
        public ToolItem(string Text, string IconString)   
        {
            this.Text = Text;
            this.IconString = IconString;
        }
        #endregion
    }
    #endregion
    #region class : ToolItemMouseEventArgs
    public class ToolItemMouseEventArgs : EventArgs
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public ToolItem Item { get; private set; }

        public ToolItemMouseEventArgs(int X, int Y, ToolItem Item)
        {
            this.X = X;
            this.Y = Y;
            this.Item = Item;
        }
    }

    public class ToolItemMouseDownEventArgs : ToolItemMouseEventArgs
    {
        public bool Drag { get; set; }
        public ToolItemMouseDownEventArgs(int X, int Y, ToolItem Item) : base(X, Y, Item) { }
    }
    #endregion
}
