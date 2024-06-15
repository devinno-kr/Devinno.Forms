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
    public class DvListBox : DvControl
    {
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
        #region SelectedColor
        private Color? cSelectedColor = null;
        public Color? SelectedColor
        {
            get => cSelectedColor;
            set
            {
                if (cSelectedColor != value)
                {
                    cSelectedColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region RowColor
        private Color? cRowColor = null;
        public Color? RowColor
        {
            get => cRowColor;
            set
            {
                if (cRowColor != value)
                {
                    cRowColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region ItemPadding
        private Padding padItem = new Padding(0);
        public Padding ItemPadding
        {
            get => padItem;
            set
            {
                if(padItem != value)
                {
                    padItem = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ItemAlignment
        private DvContentAlignment eItemAlignment = DvContentAlignment.MiddleCenter;
        public DvContentAlignment ItemAlignment
        {
            get => eItemAlignment;
            set
            {
                if (eItemAlignment != value)
                {
                    eItemAlignment = value;
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

        #region Items
        public List<TextIcon> Items { get; } = new List<TextIcon>();
        #endregion
        #region SelectedItems
        public List<TextIcon> SelectedItems { get; } = new List<TextIcon>();
        #endregion
        #region SelectionMode
        public ItemSelectionMode SelectionMode { get; set; } = ItemSelectionMode.Single;
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
        internal bool IsScrolling => scroll.IsTouchMoving || (scroll.IsTouchScrolling && scroll.TouchOffset != 0);
        internal bool DrawScroll { get; set; } = true;
        internal bool Scrollable => scroll.ScrollView < scroll.ScrollTotal;
        public double ScrollPosition
        {
            get => scroll.ScrollPosition;
            set => scroll.ScrollPosition = value;
        }
        #endregion
        #endregion

        #region Member Variable
        private Scroll scroll;
        private TextIcon first;
        private Point downPoint;
        private Point movePoint;
        private DateTime downTime;
        #endregion

        #region Event 
        public event EventHandler SelectedChanged;
        public event EventHandler<ItemClickedEventArgs> ItemClicked;
        public event EventHandler<ItemClickedEventArgs> ItemDoubleClicked;
        #endregion

        #region Constructor
        public DvListBox()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 100);

            scroll = new Scroll() { Direction = ScrollDirection.Vertical, TouchMode = true };
            scroll.GetScrollTotal = () => Items.Count * ItemHeight;
            scroll.GetScrollTick = () => ItemHeight;
            scroll.GetScrollView = () => this.Height - 2;
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
            var SelectedItemColor = this.SelectedColor ?? Theme.PointColor;
            var RowColor = this.RowColor ?? Theme.RowColor;
            var BorderColor = Theme.GetBorderColor(BoxColor, BackColor);
            var SelectedBorderColor = Theme.GetBorderColor(SelectedItemColor, BackColor);
            var ScrollBorderColor = Theme.GetBorderColor(Theme.ScrollBarColor, BackColor);
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

                    loop(rtBox, (i, rt, itm) =>
                    {
                        if (SelectedItems.Contains(itm))
                            Theme.DrawBox(e.Graphics, rt, SelectedItemColor, SelectedBorderColor, BackgroundDraw ? RoundType.Rect : RoundType.All, Box.ButtonUp_V(true, ShadowGap));
                        else
                        {
                            Theme.DrawBox(e.Graphics, rt, RowColor, BorderColor, RoundType.Rect, Box.FlatBox(true));
                            
                            var pts = new PointF[] { new PointF(rt.Right - 1, rt.Top + 1), new PointF(rt.Left + 1, rt.Top + 1) };
                            p.Color = Theme.GetInBevelColor(RowColor);
                            e.Graphics.DrawLines(p, pts);
                        }
                        
                        Theme.DrawTextIcon(e.Graphics, itm, Font, ForeColor, rt);
                    });

                    e.Graphics.ResetClip();
                }

                if (BackgroundDraw) Theme.DrawBox(e.Graphics, rtBox, BoxColor, BorderColor, rndBox, Box.Border());
                #endregion

                #region Scroll
                if (DrawScroll)
                {
                    Theme.DrawBox(e.Graphics, rtScroll, Theme.ScrollBarColor, ScrollBorderColor, rndScroll, Box.FlatBox(true));

                    e.Graphics.SetClip(Util.FromRect(rtScroll.Left, rtScroll.Top + 0, rtScroll.Width, rtScroll.Height - 0));

                    var cCur = Theme.ScrollCursorOffColor;
                    if (scroll.IsScrolling || scroll.IsTouchMoving) cCur = Theme.ScrollCursorOnColor;

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

            Areas((rtContent, rtBox, rtScroll) =>
            {
                scroll.MouseDown(x, y, rtScroll);
                if (scroll.TouchMode && CollisionTool.Check(rtBox, x, y)) scroll.TouchDown(x, y);

                if (CollisionTool.Check(rtBox, x, y))
                {
                    downPoint = e.Location;
                    downTime = DateTime.Now;
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
            Areas((rtContent, rtBox, rtScroll) =>
            {
                scroll.MouseMove(x, y, rtScroll);
                if (scroll.TouchMode) scroll.TouchMove(x, y);
            });

            movePoint = e.Location;
            if (scroll.IsScrolling || scroll.IsTouchMoving || scroll.IsTouchScrolling) Invalidate();
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            Areas((rtContent, rtBox, rtScroll) =>
            {
                scroll.MouseUp(x, y);
                if (scroll.TouchMode) scroll.TouchUp(x, y);

                if (CollisionTool.Check(rtBox, e.Location) && Math.Abs(downPoint.Y - e.Y) < Scroll.GapSize && (DateTime.Now - downTime).TotalSeconds < Scroll.GapTime)
                {
                    loop(rtBox, (i, rt, v) =>
                    {
                        if (CollisionTool.Check(rt, x, y))
                        {
                            #region Single Selection
                            if (SelectionMode == ItemSelectionMode.Single)
                            {
                                SelectedItems.Clear();
                                SelectedItems.Add(v);
                                if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                first = v;
                            }
                            #endregion
                            #region Multi Selection
                            else if (SelectionMode == ItemSelectionMode.Multi)
                            {
                                if ((ModifierKeys & Keys.Control) == Keys.Control)
                                {
                                    #region Control
                                    if (SelectedItems.Contains(v))
                                    {
                                        SelectedItems.Remove(v);
                                        if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                    }
                                    else
                                    {
                                        SelectedItems.Add(v);
                                        if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                        first = v;
                                    }
                                    #endregion
                                }
                                else if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                                {
                                    #region Shift
                                    if (first == null)
                                    {
                                        SelectedItems.Add(v);
                                        if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                    }
                                    else
                                    {
                                        int idx1 = Items.IndexOf(first);
                                        int idx2 = i;
                                        int min = Math.Min(idx1, idx2);
                                        int max = Math.Max(idx1, idx2);

                                        bool b = false;
                                        for (int ii = min; ii <= max; ii++)
                                        {
                                            if (!SelectedItems.Contains(Items[ii]))
                                            {
                                                SelectedItems.Add(Items[ii]);
                                                b = true;
                                            }
                                        }
                                        if (b && SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region Select
                                    SelectedItems.Clear();
                                    SelectedItems.Add(v);
                                    if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                    first = v;
                                    #endregion
                                }
                            }
                            #endregion
                        }

                        if (MathTool.GetDistance(downPoint, e.Location) < 10 && (DateTime.Now - downTime).TotalSeconds < Scroll.GapTime && CollisionTool.Check(rt, e.Location))
                            ItemClicked?.Invoke(this, new ItemClickedEventArgs(v));
                    });
                }
            });
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseClick
        protected override void OnMouseClick(MouseEventArgs e)
        {
            /*
            int x = e.X, y = e.Y;
            Areas((rtContent, rtBox, rtScroll) =>
            {
                if (SelectionMode != ItemSelectionMode.None)
                {
                    loop(rtBox, (i, rt, itm) =>
                    {
                        if (Math.Abs(downPoint.Y - e.Y) < Scroll.GapSize && (DateTime.Now - downTime).TotalSeconds < Scroll.GapTime && CollisionTool.Check(rt, e.Location))
                            ItemClicked?.Invoke(this, new ItemClickedEventArgs(itm));
                    });
                    Invalidate();
                }
            });
            Invalidate();
            */
            base.OnMouseClick(e);
        }
        #endregion
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            Areas((rtContent, rtBox, rtScroll) =>
            {
                if (SelectionMode != ItemSelectionMode.None)
                {
                    loop(rtBox, (i, rt, itm) =>
                    {
                        if (Math.Abs(downPoint.Y - e.Y) < Scroll.GapSize && (DateTime.Now - downTime).TotalSeconds < Scroll.GapTime && CollisionTool.Check(rt, e.Location))
                            ItemDoubleClicked?.Invoke(this, new ItemClickedEventArgs(itm));
                    });
                    Invalidate();
                }
            });
            Invalidate();
            base.OnMouseDoubleClick(e);
        }
        #endregion
        #region OnMouseWheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Areas((rtContent, rtBox, rtScroll) =>
            {
                if (CollisionTool.Check(rtContent, e.Location))
                {
                    if (scroll.ScrollVisible) ((HandledMouseEventArgs)e).Handled = true;
                    scroll.MouseWheel(e.Delta, rtScroll);
                    Invalidate();
                }
            });
            base.OnMouseWheel(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF> act)
        {
            var GAP = 0;
            var scwh = DrawScroll ? Convert.ToInt32(Scroll.SC_WH) : 0;
            var rtContent = GetContentBounds();
            var rtBox = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width - scwh - GAP, rtContent.Height);
            var rtScroll = Util.FromRect(rtBox.Right + GAP, rtBox.Top, scwh, rtBox.Height);

            act(rtContent, rtBox, (rtScroll));
        }
        #endregion
        #region loop
        private void loop(RectangleF rtBox, Action<int, RectangleF, TextIcon> act)
        {
            if (!IsDisposed)
            {
                var sc = scroll.ScrollPosition;
                var spos = Convert.ToInt32(scroll.ScrollPositionWithOffset);

                var si = Convert.ToInt32(Math.Floor((double)(sc - scroll.TouchOffset) / (double)ItemHeight));
                var cnt = Convert.ToInt32(Math.Ceiling((double)(rtBox.Height - Math.Min(0, scroll.TouchOffset)) / (double)ItemHeight));
                var ei = si + cnt;

                using (var g = CreateGraphics())
                {
                    for (int i = Math.Max(0, si); i < ei + 1 && i < Items.Count; i++)
                    {
                        var itm = Items[i];
                        var rt = Util.FromRect(rtBox.Left, spos + rtBox.Top + (ItemHeight * i), rtBox.Width, ItemHeight);
                        if (!BackgroundDraw)
                        {
                            var sz = g.MeasureTextIcon(itm, Font);
                            rt = Util.MakeRectangle(rt, new SizeF(sz.Width + 20, rt.Height));
                        }
                        if (CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox)) act(i, rt, itm);
                    }
                }
            }
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
        #endregion
    }


}
