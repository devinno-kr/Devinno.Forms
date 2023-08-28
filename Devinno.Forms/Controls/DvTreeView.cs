using Devinno.Collections;
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
    public class DvTreeView : DvControl
    {
        #region Const
        const int IndentWidth = 20;
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
        #region RadioColor
        private Color? cRadioColor = null;
        public Color? RadioColor
        {
            get => cRadioColor;
            set
            {
                if (cRadioColor != value)
                {
                    cRadioColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region RadioBoxColor
        private Color? cRadioBoxColor = null;
        public Color? RadioBoxColor
        {
            get => cRadioBoxColor;
            set
            {
                if (cRadioBoxColor != value)
                {
                    cRadioBoxColor = value;
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

        #region ItemHeight
        private int nItemHeight = 36;
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

        #region Nodes
        public DvTreeViewNodeCollection Nodes { get; private set; }
        #endregion
        #region SelectedNodes
        public List<DvTreeViewNode> SelectedNodes { get; } = new List<DvTreeViewNode>();
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

        #region Animation
        private bool Animation => GetTheme()?.Animation ?? false;
        #endregion

        #region ScrollBar
        internal bool _IsScrolling => scroll.IsTouchMoving || (scroll.IsTouchScrolling && scroll.TouchOffset != 0);
        internal bool IsScrolling { get; private set; }
        internal bool DrawScroll { get; set; } = true;
        internal bool Scrollable => scroll.ScrollView < scroll.ScrollTotal;
        internal double ScrollPosition
        {
            get => scroll.ScrollPosition;
            set => scroll.ScrollPosition = value;
        }
        #endregion

        #region VisibleDropDown 
        internal bool VisibleDropDown { get; set; } = false;
        #endregion
        #endregion

        #region Member Variable
        private Scroll scroll;
        private Animation ani = new Animation();
        private int mx = 0, my = 0;
        private DvTreeViewNode aniItem = null;
        private List<DvTreeViewNode> ls = new List<DvTreeViewNode>();
        private List<DvTreeViewNode> anils = new List<DvTreeViewNode>();
        private DvTreeViewNode first;

        private Point downPoint;
        private Point movePoint;
        private DateTime downTime;
        private TextBox OriginalTextBox;
        #endregion

        #region Event
        public event EventHandler<DvTreeViewNodeMouseEventArgs> NodeDown;
        public event EventHandler<DvTreeViewNodeMouseEventArgs> NodeUp;
        public event EventHandler<DvTreeViewNodeMouseEventArgs> NodeClicked;
        public event EventHandler<DvTreeViewNodeMouseEventArgs> NodeDoubleClicked;
        public event EventHandler SelectedChanged;
        #endregion

        #region Constructor
        public DvTreeView()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            #region Nodes
            Nodes = new DvTreeViewNodeCollection(null);
            Nodes.Changed += (o, s) =>
            {
                foreach (var v in Nodes)
                {
                    v.Control = this;
                }
            };
            #endregion

            #region Scroll
            scroll = new Scroll() { Direction = ScrollDirection.Vertical, TouchMode = true };
            scroll.GetScrollTotal = () => ls.Count * ItemHeight + (Animation && ani.IsPlaying ?
            (aniItem.Expands ? -ani.Value(AnimationAccel.DCL, anils.Count * ItemHeight, 0)
                             : -ani.Value(AnimationAccel.DCL, 0, anils.Count * ItemHeight))
            : 0);
            scroll.GetScrollTick = () => ItemHeight;
            scroll.GetScrollView = () => (this.Height - 2);
            scroll.GetConstrainIgnore = () => Animation && ani.IsPlaying;
            scroll.ScrollChanged += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            scroll.ScrollEnded += (o, s) => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); };
            #endregion

            #region TextBox
            OriginalTextBox = new TextBox();
            OriginalTextBox.Location = new System.Drawing.Point(0, 0);
            OriginalTextBox.Name = "OriginalTextBox";
            OriginalTextBox.Size = new System.Drawing.Size(60, 28);
            OriginalTextBox.BorderStyle = BorderStyle.None;
            OriginalTextBox.TabIndex = 0;
            OriginalTextBox.TextAlign = HorizontalAlignment.Center;
            OriginalTextBox.Tag = null;
            OriginalTextBox.Visible = false;
            Controls.Add(OriginalTextBox);

            OriginalTextBox.LostFocus += (o, s) => ClearInput();
            OriginalTextBox.TextChanged += (o, s) => FlushInput();
            #endregion
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BoxColor = this.BoxColor ?? Theme.ListBackColor;
            var SelectedColor = this.SelectedColor ?? Theme.PointColor;
            var RadioColor = this.RadioColor ?? Theme.ForeColor;
            var RadioBoxColor = this.RadioBoxColor ?? Theme.CheckBoxColor;
            var BorderColor = Theme.GetBorderColor(BoxColor, BackColor);
            var SelectedBorderColor = Theme.GetBorderColor(SelectedColor, BackColor);
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

                #region Item
                if (BackgroundDraw) Theme.DrawBox(e.Graphics, rtBox, BoxColor, BorderColor, rndBox, Box.ListBox(ShadowGap));

                using (var path = GetPath(Theme, Round, rtContent))
                {
                    e.Graphics.SetClip(path);

                    loop((ls, i, rt, itm, rtRow, rtRadio, rtText) =>
                    {
                        if (CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            var rtBounds = Util.INT(rtText); rtBounds.Inflate(7, -1);
                            var rtRad = Util.INT(rtRadio);

                            #region Radio
                            if (itm.Nodes.Count > 0)
                            {
                                var ns = 4;
                                var ic = Convert.ToInt32(ItemHeight / 1.5);
                                var c = BoxColor.BrightnessTransmit(Theme.DownBrightness);
                                Theme.DrawBox(e.Graphics, rtRad, RadioBoxColor, BorderColor, RoundType.Ellipse, BoxStyle.Border | BoxStyle.Fill | BoxStyle.OutBevel);

                                if (Animation && ani.IsPlaying && aniItem == itm)
                                {
                                    var wh = aniItem.Expands ? ani.Value(AnimationAccel.DCL, RadioSize - (ns * 2), 0) : ani.Value(AnimationAccel.DCL, 0, RadioSize - (ns * 2));
                                    var rtb = Util.MakeRectangleAlign(rtRad, new SizeF(wh, wh), DvContentAlignment.MiddleCenter);
                                    Theme.DrawBox(e.Graphics, rtb, RadioColor, BorderColor, RoundType.Ellipse, BoxStyle.GradientV | BoxStyle.InBevel | BoxStyle.OutShadow);
                                }
                                else
                                {
                                    if (!itm.Expands)
                                    {
                                        var rtb = rtRad;
                                        rtb.Inflate(-ns, -ns);
                                        Theme.DrawBox(e.Graphics, rtb, RadioColor, BorderColor, RoundType.Ellipse, BoxStyle.GradientV | BoxStyle.InBevel | BoxStyle.OutShadow);
                                    }
                                }
                            }
                            else
                            {
                                var dot = Util.MakeRectangleAlign(rtRad, new SizeF(5, 5), DvContentAlignment.MiddleCenter);
                                Theme.DrawBox(e.Graphics, dot, RadioColor, BorderColor, RoundType.Ellipse, BoxStyle.Fill | BoxStyle.OutShadow);
                            }
                            #endregion

                            if (SelectedNodes.Contains(itm))
                                Theme.DrawBox(e.Graphics, rtBounds, SelectedColor, SelectedBorderColor, RoundType.All, Box.ButtonUp_V(true, ShadowGap));

                            if (!_IsScrolling && SelectionMode != ItemSelectionMode.None && CollisionTool.Check(rtBounds, mx, my))
                                Theme.DrawBox(e.Graphics, rtBounds, Util.FromArgb(30, Color.White), Util.FromArgb(90, Color.White), RoundType.All, Box.FlatBox(true));

                            itm.Draw(e.Graphics, Theme, rtText);


                        }
                    });

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
            ClearInput();

            int x = e.X, y = e.Y;

            scroll.TouchMode = GetTheme()?.TouchMode ?? false;

            Areas((rtContent, rtBox, rtScroll) =>
            {
                scroll.MouseDown(x, y, rtScroll);
                if (scroll.TouchMode && CollisionTool.Check(rtBox, x, y)) scroll.TouchDown(x, y);

                IsScrolling = _IsScrolling;

                if (CollisionTool.Check(rtBox, x, y))
                {
                    if (!VisibleDropDown)
                    {
                        loop((ls, i, rt, itm, rtRow, rtRadio, rtText) =>
                        {
                            if (itm != null && CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                            {
                                if (CollisionTool.Check(rtText, x, y))
                                {
                                    NodeDown?.Invoke(this, new DvTreeViewNodeMouseEventArgs(x, y, itm));
                                    itm.MouseDown(rtText, x, y);
                                }
                            }
                        });
                    }
                }

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
                    if (!VisibleDropDown)
                    {
                        loop((ls, i, rt, itm, rtRow, rtRadio, rtText) =>
                        {
                            if (CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                            {
                                if (CollisionTool.Check(rtRadio, x, y))
                                {
                                    itm.Expands = !itm.Expands;

                                    if (Animation)
                                    {
                                        aniItem = itm;
                                        anils.Clear();
                                        MS2(itm, anils);

                                        ani.Stop();
                                        ani.Start(150, "", new Action(() => { if (Created && !IsDisposed && Visible) Invalidate(); }));
                                    }
                                }

                                if (CollisionTool.Check(rtText, x, y) && Math.Abs(downPoint.Y - e.Y) < Scroll.GapSize && (DateTime.Now - downTime).TotalSeconds < Scroll.GapTime)
                                {
                                    NodeUp?.Invoke(this, new DvTreeViewNodeMouseEventArgs(x, y, itm));
                                    var r = itm.MouseUp(rtText, x, y);

                                    if (!r)
                                    {
                                        var v = itm;
                                        #region Single Selection
                                        if (SelectionMode == ItemSelectionMode.Single)
                                        {
                                            SelectedNodes.Clear();
                                            SelectedNodes.Add(v);
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
                                                if (SelectedNodes.Contains(v))
                                                {
                                                    SelectedNodes.Remove(v);
                                                    if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                                }
                                                else
                                                {
                                                    SelectedNodes.Add(v);
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
                                                    SelectedNodes.Add(v);
                                                    if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                                }
                                                else
                                                {
                                                    int idx1 = ls.IndexOf(first);
                                                    int idx2 = i;
                                                    int min = Math.Min(idx1, idx2);
                                                    int max = Math.Max(idx1, idx2);

                                                    bool b = false;
                                                    for (int ii = min; ii <= max; ii++)
                                                    {
                                                        if (!SelectedNodes.Contains(ls[ii]))
                                                        {
                                                            SelectedNodes.Add(ls[ii]);
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
                                                SelectedNodes.Clear();
                                                SelectedNodes.Add(v);
                                                if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                                first = v;
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        });
                    }
                }
            });

            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseWheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            ClearInput();
            Areas((rtContent, rtBox, rtScroll) =>
            {
                if (CollisionTool.Check(rtContent, e.Location))
                {
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
                    if (!VisibleDropDown)
                    {
                        loop((ls, i, rt, itm, rtRow, rtRadio, rtText) =>
                        {
                            if (CollisionTool.Check(rtText, x, y))
                            {
                                NodeClicked?.Invoke(this, new DvTreeViewNodeMouseEventArgs(x, y, itm));
                            }
                        });
                    }
                }
            });

            base.OnMouseClick(e);
        }
        #endregion
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            int x = e.X, y = e.Y;
            var issc = IsScrolling;

            Areas((rtContent, rtBox, rtScroll) =>
            {
                if (CollisionTool.Check(rtBox, x, y) && !IsScrolling)
                {
                    if (!VisibleDropDown)
                    {
                        loop((ls, i, rt, itm, rtRow, rtRadio, rtText) =>
                        {
                            if (CollisionTool.Check(rtText, x, y))
                            {
                                NodeDoubleClicked?.Invoke(this, new DvTreeViewNodeMouseEventArgs(x, y, itm));
                            }
                        });
                    }
                }
            });

            base.OnMouseDoubleClick(e);
        }
        #endregion
        #region OnLostFocus
        protected override void OnLostFocus(EventArgs e)
        {
            ClearInput();
            base.OnLostFocus(e);
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
        private void loop(Action<List<DvTreeViewNode>, int, RectangleF, DvTreeViewNode, RectangleF, RectangleF, RectangleF> Func)
        {
            var vls = new List<DvTreeViewNode>();
            MakeList(vls);
            ls = vls;

            using (var g = CreateGraphics())
            {
                Areas((rtContent, rtBox, rtScroll) =>
                {
                    var sc = scroll.ScrollPosition;
                    var spos = Convert.ToInt32(scroll.ScrollPositionWithOffset);

                    if (Animation && ani.IsPlaying)
                    {
                        if (aniItem != null)
                        {
                            var nh = anils.Count * ItemHeight;
                            var anih = aniItem.Expands ? ani.Value(AnimationAccel.DCL, 0, nh) : ani.Value(AnimationAccel.DCL, nh, 0);

                            var si = Convert.ToInt32(Math.Floor((double)Math.Abs(spos) / (double)ItemHeight));   //0;
                            var ei = ls.Count - 1;

                            var i = Math.Max(0, si);
                            var y = spos + rtBox.Top + (si * ItemHeight);

                            while (i < ei + 1 && i < ls.Count && y <= rtBox.Bottom)
                            {
                                var itm = ls[i];
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
                                            var idnt = IndentWidth * itm.Depth;
                                            var szw = itm.GetWidth();
                                            var w = szw;
                                            var h = ItemHeight;

                                            var SW = 0;
                                            var GP = 10;
                                            var rtv = Util.FromRect(rt.Left + idnt, rt.Top, rt.Width - idnt, rt.Height);
                                            var rtRadio = Util.MakeRectangleAlign(Util.FromRect(rtv.Left, rt.Top, rt.Height, rt.Height), new SizeF(RadioSize, RadioSize), DvContentAlignment.MiddleCenter);
                                            var rtText = Util.FromRect(rtRadio.Right + GP, rt.Top, w + SW, rt.Height);
                                            var rtRow = new RectangleF(rtRadio.Left, rtText.Top, rtText.Right - rtRadio.Left, rtText.Height);
                                            Func(ls, i, rt, itm, rtRow, rtRadio, rtText);
                                        }
                                    }
                                    else
                                    {
                                        var idnt = IndentWidth * itm.Depth;
                                        var szw = itm.GetWidth();
                                        var w = szw;
                                        var h = ItemHeight;

                                        var SW = 0;
                                        var GP = 10;
                                        var rtv = Util.FromRect(rt.Left + idnt, rt.Top, rt.Width - idnt, rt.Height);
                                        var rtRadio = Util.MakeRectangleAlign(Util.FromRect(rtv.Left, rt.Top, rt.Height, rt.Height), new SizeF(RadioSize, RadioSize), DvContentAlignment.MiddleCenter);
                                        var rtText = Util.FromRect(rtRadio.Right + GP, rt.Top, w + SW, rt.Height);
                                        var rtRow = new RectangleF(rtRadio.Left, rtText.Top, rtText.Right - rtRadio.Left, rtText.Height);
                                        Func(ls, i, rt, itm, rtRow, rtRadio, rtText);

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

                        for (int i = Math.Max(0, si); i < ei + 1 && i < ls.Count; i++)
                        {
                            var itm = ls[i];
                            var rt = Util.FromRect(rtBox.Left, spos + rtBox.Top + (ItemHeight * i), rtBox.Width, ItemHeight);
                            if (CollisionTool.Check(Util.FromRect(rt.Left + 1, rt.Top + 1, rt.Width - 2, rt.Height - 2), rtBox))
                            {
                                var idnt = IndentWidth * itm.Depth;
                                var szw = itm.GetWidth();
                                var w = szw;
                                var h = ItemHeight;

                                var SW = 0;
                                var GP = 10;
                                var rtv = Util.FromRect(rt.Left + idnt, rt.Top, rt.Width - idnt, rt.Height);
                                var rtRadio = Util.MakeRectangleAlign(Util.FromRect(rtv.Left, rt.Top, rt.Height, rt.Height), new SizeF(RadioSize, RadioSize), DvContentAlignment.MiddleCenter);
                                var rtText = Util.FromRect(rtRadio.Right + GP, rt.Top, w + SW, rt.Height);
                                var rtRow = new RectangleF(rtRadio.Left, rtText.Top, rtText.Right - rtRadio.Left, rtText.Height);
                                Func(ls, i, rt, itm, rtRow, rtRadio, rtText);
                            }
                        }
                    }
                });
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

        #region GetTreeNode
        public DvTreeViewNode GetTreeNode(int x, int y)
        {
            DvTreeViewNode ret = null;
            loop((ls, i, rt, itm, rtRow, rtRadio, rtText) =>
            {
                if (CollisionTool.Check(rtText, x, y)) ret = itm;
            });
            return ret;
        }
        #endregion
        #region MakeList
        void MakeList(List<DvTreeViewNode> ls)
        {
            ls.Clear();
            for (int i = 0; i < Nodes.Count; i++) MS(Nodes[i], ls);
        }

        void MS(DvTreeViewNode nd, List<DvTreeViewNode> lst)
        {
            if (Animation && ani.IsPlaying && aniItem == nd)
            {
                lst.Add(nd);
                for (int i = 0; i < nd.Nodes.Count; i++) MS(nd.Nodes[i], lst);
            }
            else
            {
                lst.Add(nd);
                if (nd.Expands) for (int i = 0; i < nd.Nodes.Count; i++) MS(nd.Nodes[i], lst);
            }
        }

        void MS2(DvTreeViewNode nd, List<DvTreeViewNode> lst)
        {
            for (int i = 0; i < nd.Nodes.Count; i++) MS(nd.Nodes[i], lst);
        }
        #endregion

        #region Input
        #region SetInput
        internal void SetInput(DvTreeViewNode nd, RectangleF rtValue, string Value)
        {
            var Wnd = FindForm() as DvForm;
            var Theme = GetTheme();
            if (Theme.KeyboardInput)
            {
                OriginalTextBox.BackColor = GetTheme().InputColor;
                OriginalTextBox.ForeColor = ForeColor;
                OriginalTextBox.Text = Value;
                OriginalTextBox.Tag = nd;
                OriginalTextBox.Visible = true;
                OriginalTextBox.Focus();
                AlignInput(rtValue);
            }
            else OriginalTextBox.Visible = false;
        }
        #endregion
        #region ClearInput
        void ClearInput()
        {
            var Wnd = FindForm() as DvForm;
            var Theme = GetTheme();
            if (Theme.KeyboardInput)
            {
                FlushInput();

                OriginalTextBox.Tag = null;
                OriginalTextBox.Visible = false;
            }
            else OriginalTextBox.Visible = false;
        }
        #endregion
        #region FlushInput
        void FlushInput()
        {
            var Wnd = FindForm() as DvForm;
            var Theme = GetTheme();
            if (Theme.KeyboardInput)
            {
                var node = OriginalTextBox.Tag as DvTreeViewNode;
                if (node != null)
                {
                    var s = node.GetType().Name;
                    if (node is DvTreeViewInputTextNode) ((DvTreeViewInputTextNode)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<byte>) ((DvTreeViewInputNumberNode<byte>)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<ushort>) ((DvTreeViewInputNumberNode<ushort>)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<uint>) ((DvTreeViewInputNumberNode<uint>)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<ulong>) ((DvTreeViewInputNumberNode<ulong>)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<sbyte>) ((DvTreeViewInputNumberNode<sbyte>)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<short>) ((DvTreeViewInputNumberNode<short>)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<int>) ((DvTreeViewInputNumberNode<int>)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<long>) ((DvTreeViewInputNumberNode<long>)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<float>) ((DvTreeViewInputNumberNode<float>)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<double>) ((DvTreeViewInputNumberNode<double>)node).TextChange(OriginalTextBox);
                    else if (node is DvTreeViewInputNumberNode<decimal>) ((DvTreeViewInputNumberNode<decimal>)node).TextChange(OriginalTextBox);

                }
                Invalidate();
            }
            else OriginalTextBox.Visible = false;
        }
        #endregion
        #region AlignInput
        void AlignInput(RectangleF rtValue)
        {
            var Wnd = FindForm() as DvForm;
            var Theme = GetTheme();
            if (Theme.KeyboardInput)
            {
                #region Align
                OriginalTextBox.TextAlign = HorizontalAlignment.Center;
                #endregion
                bool bv = this.Enabled && (Wnd == null || (Wnd != null && !Wnd.Block)) && (Theme?.KeyboardInput ?? false);
                if (bv != OriginalTextBox.Visible) OriginalTextBox.Visible = bv;

                var sz = TextRenderer.MeasureText(Text, Font);
                var sz2 = TextRenderer.MeasureText("H", Font);
                var rt = Util.FromRect(rtValue, new Padding(5));
                var rtText = Util.MakeRectangleAlign(rt, new SizeF(rt.Width, Math.Max(Convert.ToInt32(sz2.Height), Convert.ToInt32(sz.Height))), DvContentAlignment.MiddleCenter);

                OriginalTextBox.Bounds = Util.INT(rtText);
            }
            else OriginalTextBox.Visible = false;
        }
        #endregion
        #endregion
        #endregion
    }

    #region class : DvTreeViewLabelNode
    public class DvTreeViewLabelNode : DvTreeViewNode
    {
        #region Constructor
        public DvTreeViewLabelNode(string Text) : base(Text) { }
        public DvTreeViewLabelNode(string Text, string IconString) : base(Text, IconString) { }
        #endregion

        #region Override
        #region Draw
        public override void Draw(Graphics g, DvTheme Theme, RectangleF bounds)
        {
            if(Control != null)
            {
                Theme.DrawTextIcon(g, TextIcon, Control.Font, Control.ForeColor, bounds, DvContentAlignment.MiddleLeft);
            }
            base.Draw(g, Theme, bounds);
        }
        #endregion
        #endregion

        #region Method
        #region GetWidth
        public override float GetWidth()
        {
            float ret = 0F;
            if (Control != null)
            {
                using (var g = Control.CreateGraphics())
                    ret = g.MeasureTextIcon(TextIcon, Control.Font).Width;
            }
            return ret;
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvTreeViewValueLabelNode
    public class DvTreeViewValueLabelNode : DvTreeViewNode
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
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ValueColor
        private Color? cValueColor = null;
        public Color? ValueColor
        {
            get => cValueColor;
            set
            {
                if (cValueColor != value)
                {
                    cValueColor = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion

        #region Unit
        private string strUnit = "";
        public string Unit
        {
            get => strUnit;
            set
            {
                if (strUnit != value)
                {
                    strUnit = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region Value
        private string strValue = "";
        public string Value
        {
            get => strValue;
            set => strValue = value;
        }
        #endregion
        #region Button
        private TextIcon texticonBtn = new TextIcon();

        public DvIcon ButtonIcon => texticonBtn.Icon;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap ButtonIconImage
        {
            get => texticonBtn.IconImage;
            set { if (texticonBtn.IconImage != value) { texticonBtn.IconImage = value; Control?.Invalidate(); } }
        }
        public string ButtonIconString
        {
            get => texticonBtn.IconString;
            set { if (texticonBtn.IconString != value) { texticonBtn.IconString = value; Control?.Invalidate(); } }
        }
        public float ButtonIconSize
        {
            get => texticonBtn.IconSize;
            set { if (texticonBtn.IconSize != value) { texticonBtn.IconSize = value; Control?.Invalidate(); } }
        }
        public int ButtonIconGap
        {
            get => texticonBtn.IconGap;
            set { if (texticonBtn.IconGap != value) { texticonBtn.IconGap = value; Control?.Invalidate(); } }
        }
        public DvTextIconAlignment ButtonIconAlignment
        {
            get => texticonBtn.IconAlignment;
            set { if (texticonBtn.IconAlignment != value) { texticonBtn.IconAlignment = value; Control?.Invalidate(); } }
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Button
        {
            get => texticonBtn.Text;
            set { if (texticonBtn.Text != value) { base.Text = texticonBtn.Text = value; Control?.Invalidate(); } }
        }

        public Padding ButtonTextPadding
        {
            get => texticonBtn.TextPadding;
            set { if (texticonBtn.TextPadding != value) { texticonBtn.TextPadding = value; Control?.Invalidate(); } }
        }
        #endregion

        #region TitleWidth
        private int? nTitleWidth = 70;
        public int? TitleWidth
        {
            get => nTitleWidth;
            set
            {
                if (nTitleWidth != value)
                {
                    nTitleWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ValueWidth
        private int nValueWidth = 100;
        public int ValueWidth
        {
            get => nValueWidth;
            set
            {
                if (nValueWidth != value)
                {
                    nValueWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region UnitWidth
        private int? nUnitWidth = null;
        public int? UnitWidth
        {
            get => nUnitWidth;
            set
            {
                if (nUnitWidth != value)
                {
                    nUnitWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ButtonWidth
        private int? nButtonWidth = null;
        public int? ButtonWidth
        {
            get => nButtonWidth;
            set
            {
                if (nButtonWidth != value)
                {
                    nButtonWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion

        #region ButtonDownState
        protected bool ButtonDownState { get; private set; }
        #endregion
        #endregion

        #region Member Variable
        #endregion

        #region Event
        public event EventHandler ButtonClicked;
        #endregion

        #region Constructor
        public DvTreeViewValueLabelNode(string Text) : base(Text) { }
        public DvTreeViewValueLabelNode(string Text, string IconString) : base(Text, IconString) { }
        #endregion

        #region Override
        #region Draw
        public override void Draw(Graphics g, DvTheme Theme, RectangleF bounds)
        {
            if (Control != null)
            {
                bounds.Inflate(0, -3);
                Areas(bounds, (rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
                {
                    var useT = TitleWidth.HasValue && TitleWidth.Value > 0;
                    var useB = ButtonWidth.HasValue && ButtonWidth.Value > 0;
                    var useU = UnitWidth.HasValue && UnitWidth.Value > 0;

                    #region Color
                    var Font = Control.Font;
                    var ForeColor = Control.ForeColor;
                    var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
                    var ButtonBorderColor = Theme.GetBorderColor(ButtonColor, Control.BoxColor ?? Theme.ListBackColor);
                    var ValueColor = this.ValueColor ?? Theme.LabelColor;
                    var ValueBorderColor = Theme.GetBorderColor(ValueColor, Control.BoxColor ?? Theme.ListBackColor);
                    var ShadowGap = Control.ShadowGap;
                    #endregion
                    #region Round
                    var rndValue = RoundType.L;
                    var rndButton = RoundType.R;

                    if (!ButtonWidth.HasValue)
                    {
                        rndValue = RoundType.All;
                        rndButton = RoundType.Rect;
                    }
                    #endregion

                    #region Title
                    if (useT)
                    {
                        var cT = ForeColor;

                        Theme.DrawTextIcon(g, TextIcon, Control.Font, cT, rtTitle, DvContentAlignment.MiddleLeft);
                    }
                    #endregion
                    #region Button
                    if (useB)
                    {
                        var cV = ButtonDownState ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                        var cB = ButtonDownState ? ButtonBorderColor.BrightnessTransmit(Theme.DownBrightness) : ButtonBorderColor;
                        var cT = ButtonDownState ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;

                        Theme.DrawBox(g, rtButton, cV, cB, rndButton, !ButtonDownState ? Box.ButtonUp_V(true, ShadowGap) : Box.ButtonDown(ShadowGap));
                        if (ButtonDownState) rtButton.Offset(0, 1);
                        Theme.DrawTextIcon(g, texticonBtn, Font, cT, rtButton);
                        if (ButtonDownState) rtButton.Offset(0, -1);
                    }
                    #endregion
                    #region Value
                    {
                        var cV = ValueColor;
                        var cB = ValueBorderColor;
                        var cT = ForeColor;

                        Theme.DrawBox(g, rtValueAll, cV, cB, rndValue, Box.LabelBox(Embossing.FlatConcave, ShadowGap));

                        Theme.DrawText(g, Value, Font, ForeColor, rtValue);
                    }
                    #endregion
                    #region Unit
                    if (useU && !string.IsNullOrWhiteSpace(Unit))
                    {
                        #region Sep
                        var szh = Convert.ToInt32(rtValue.Height / 2);
                        var x = rtUnit.Left;

                        using (var p = new Pen(Color.Black))
                        {
                            var hsv = ValueColor.ToHSV();

                            p.Width = 1;

                            p.Color = Theme.GetInBevelColor(ValueColor);

                            var y1 = (rtValue.Top + (rtValue.Height / 2)) - (szh / 2) + 1;
                            var y2 = (rtValue.Top + (rtValue.Height / 2)) + (szh / 2) + 1;

                            g.DrawLine(p, x + 1F, y1, x + 1F, y2);

                            p.Color = ValueColor.BrightnessTransmit(Theme.BorderBrightness);
                            g.DrawLine(p, x + 0F, y1, x + 0F, y2);
                        }
                        #endregion

                        var isz = Math.Min(rtUnit.Width, rtUnit.Height) / 3;
                        if (FA.Contains(Unit)) Theme.DrawIcon(g, new DvIcon { IconString = Unit, IconSize = isz }, ForeColor, rtUnit);
                        else Theme.DrawText(g, Unit, Font, ForeColor, rtUnit);
                    }
                    #endregion

                });
            }
            base.Draw(g, Theme, bounds);
        }
        #endregion

        #region MouseDown
        public override void MouseDown(RectangleF bounds, int x, int y)
        {
            Areas(bounds, (rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, x, y)) ButtonDownState = true;
            });
            base.MouseDown(bounds, x, y);
        }
        #endregion
        #region MouseUp
        public override bool MouseUp(RectangleF bounds, int x, int y)
        {
            var ret = false;
            Areas(bounds, (rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (ButtonDownState)
                {
                    ButtonDownState = false;
                    ret = true;
                    if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, x, y)) ButtonClicked?.Invoke(this, new EventArgs());
                }
            });

            return ret;
        }
        #endregion
        #endregion

        #region Method
        #region GetWidth
        public override float GetWidth()
        {
            float ret = 0F;
            ret = (TitleWidth ?? 0) + (ValueWidth) + (ButtonWidth ?? 0);
            return ret;
        }
        #endregion
        #region Areas
        public void Areas(RectangleF rtContent, Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var szUnitW = (UnitWidth.HasValue && UnitWidth.Value > 0) ? UnitWidth.Value : 0;
            var szTitleW = (TitleWidth.HasValue && TitleWidth.Value > 0) ? TitleWidth.Value : 0;
            var szButonW = (ButtonWidth.HasValue && ButtonWidth.Value > 0) ? ButtonWidth.Value : 0;

            var rts = Util.DevideSizeH(rtContent, new SizeInfo[] { new SizeInfo(DvSizeMode.Pixel, szTitleW), new SizeInfo(DvSizeMode.Percent, 100F), new SizeInfo(DvSizeMode.Pixel, szUnitW), new SizeInfo(DvSizeMode.Pixel, szButonW) }.ToList());

            var rtTitle = rts[0];
            var rtValue = rts[1];
            var rtUnit = rts[2];
            var rtButton = rts[3];
            var rtValueAll = new RectangleF(rtValue.Left, rtValue.Top, rtUnit.Right - rtValue.Left, rtValue.Height);

            act(rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll);
        }
        #endregion
        #region TextChange
        internal void TextChange(TextBox txt)
        {
            Value = txt.Text;
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvTreeViewInputTextNode
    public class DvTreeViewInputTextNode : DvTreeViewNode
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
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ValueColor
        private Color? cValueColor = null;
        public Color? ValueColor
        {
            get => cValueColor;
            set
            {
                if (cValueColor != value)
                {
                    cValueColor = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion

        #region Unit
        private string strUnit = "";
        public string Unit
        {
            get => strUnit;
            set
            {
                if (strUnit != value)
                {
                    strUnit = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region Value
        private string strValue = "";
        public string Value
        {
            get => strValue;
            set => strValue = value;
        }
        #endregion
        #region Button
        private TextIcon texticonBtn = new TextIcon();

        public DvIcon ButtonIcon => texticonBtn.Icon;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap ButtonIconImage
        {
            get => texticonBtn.IconImage;
            set { if (texticonBtn.IconImage != value) { texticonBtn.IconImage = value; Control?.Invalidate(); } }
        }
        public string ButtonIconString
        {
            get => texticonBtn.IconString;
            set { if (texticonBtn.IconString != value) { texticonBtn.IconString = value; Control?.Invalidate(); } }
        }
        public float ButtonIconSize
        {
            get => texticonBtn.IconSize;
            set { if (texticonBtn.IconSize != value) { texticonBtn.IconSize = value; Control?.Invalidate(); } }
        }
        public int ButtonIconGap
        {
            get => texticonBtn.IconGap;
            set { if (texticonBtn.IconGap != value) { texticonBtn.IconGap = value; Control?.Invalidate(); } }
        }
        public DvTextIconAlignment ButtonIconAlignment
        {
            get => texticonBtn.IconAlignment;
            set { if (texticonBtn.IconAlignment != value) { texticonBtn.IconAlignment = value; Control?.Invalidate(); } }
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Button
        {
            get => texticonBtn.Text;
            set { if (texticonBtn.Text != value) { base.Text = texticonBtn.Text = value; Control?.Invalidate(); } }
        }

        public Padding ButtonTextPadding
        {
            get => texticonBtn.TextPadding;
            set { if (texticonBtn.TextPadding != value) { texticonBtn.TextPadding = value; Control?.Invalidate(); } }
        }
        #endregion
        
        #region TitleWidth
        private int? nTitleWidth = 70;
        public int? TitleWidth
        {
            get => nTitleWidth;
            set
            {
                if (nTitleWidth != value)
                {
                    nTitleWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ValueWidth
        private int nValueWidth = 100;
        public int ValueWidth
        {
            get => nValueWidth;
            set
            {
                if (nValueWidth != value)
                {
                    nValueWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region UnitWidth
        private int? nUnitWidth = null;
        public int? UnitWidth
        {
            get => nUnitWidth;
            set
            {
                if (nUnitWidth != value)
                {
                    nUnitWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ButtonWidth
        private int? nButtonWidth = null;
        public int? ButtonWidth
        {
            get => nButtonWidth;
            set
            {
                if (nButtonWidth != value)
                {
                    nButtonWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion

        #region ButtonDownState
        protected bool ButtonDownState { get; private set; }
        #endregion
        #endregion

        #region Member Variable
        bool bDown = false;
        #endregion

        #region Event
        public event EventHandler ButtonClicked;
        #endregion

        #region Constructor
        public DvTreeViewInputTextNode(string Text) : base(Text) { }
        public DvTreeViewInputTextNode(string Text, string IconString) : base(Text, IconString) { }
        #endregion

        #region Override
        #region Draw
        public override void Draw(Graphics g, DvTheme Theme, RectangleF bounds)
        {
            if (Control != null)
            {
                bounds.Inflate(0, -3);
                Areas(bounds, (rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
                {
                    var useT = TitleWidth.HasValue && TitleWidth.Value > 0;
                    var useB = ButtonWidth.HasValue && ButtonWidth.Value > 0;
                    var useU = UnitWidth.HasValue && UnitWidth.Value > 0;

                    #region Color
                    var Font = Control.Font;
                    var ForeColor = Control.ForeColor;
                    var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
                    var ButtonBorderColor = Theme.GetBorderColor(ButtonColor, Control.BoxColor ?? Theme.ListBackColor);
                    var ValueColor = this.ValueColor ?? Theme.InputColor;
                    var ValueBorderColor = Theme.GetBorderColor(ValueColor, Control.BoxColor ?? Theme.ListBackColor);
                    var ShadowGap = Control.ShadowGap;
                    #endregion
                    #region Round
                    var rndValue = RoundType.L;
                    var rndButton = RoundType.R;

                    if(!ButtonWidth.HasValue)
                    {
                        rndValue = RoundType.All;
                        rndButton = RoundType.Rect;
                    }
                    #endregion

                    #region Title
                    if (useT)
                    {
                        var cT = ForeColor;

                        Theme.DrawTextIcon(g, TextIcon, Control.Font, cT, rtTitle, DvContentAlignment.MiddleLeft);
                    }
                    #endregion
                    #region Button
                    if (useB)
                    {
                        var cV = ButtonDownState ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                        var cB = ButtonDownState ? ButtonBorderColor.BrightnessTransmit(Theme.DownBrightness) : ButtonBorderColor;
                        var cT = ButtonDownState ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;

                        Theme.DrawBox(g, rtButton, cV, cB, rndButton, !ButtonDownState ? Box.ButtonUp_V(true, ShadowGap) : Box.ButtonDown(ShadowGap));
                        if (ButtonDownState) rtButton.Offset(0, 1);
                        Theme.DrawTextIcon(g, texticonBtn, Font, cT, rtButton);
                        if (ButtonDownState) rtButton.Offset(0, -1);
                    }
                    #endregion
                    #region Value
                    {
                        var cV = ValueColor;
                        var cB = ValueBorderColor;
                        var cT = ForeColor;

                        Theme.DrawBox(g, rtValueAll, cV, cB, rndValue, Box.LabelBox(Embossing.FlatConcave, ShadowGap));

                        var rt = Util.INT(rtValue); rt.Offset(1, 0);
                        TextRenderer.DrawText(g, Value, Control.Font, rt, ForeColor);
                    }
                    #endregion
                    #region Unit
                    if (useU && !string.IsNullOrWhiteSpace(Unit))
                    {
                        #region Sep
                        var szh = Convert.ToInt32(rtValue.Height / 2);
                        var x = rtUnit.Left;

                        using (var p = new Pen(Color.Black))
                        {
                            var hsv = ValueColor.ToHSV();

                            p.Width = 1;

                            p.Color = Theme.GetInBevelColor(ValueColor);

                            var y1 = (rtValue.Top + (rtValue.Height / 2)) - (szh / 2) + 1;
                            var y2 = (rtValue.Top + (rtValue.Height / 2)) + (szh / 2) + 1;

                            g.DrawLine(p, x + 1F, y1, x + 1F, y2);

                            p.Color = ValueColor.BrightnessTransmit(Theme.BorderBrightness);
                            g.DrawLine(p, x + 0F, y1, x + 0F, y2);
                        }
                        #endregion

                        var isz = Math.Min(rtUnit.Width, rtUnit.Height) / 3;
                        if (FA.Contains(Unit)) Theme.DrawIcon(g, new DvIcon { IconString = Unit, IconSize = isz }, ForeColor, rtUnit);
                        else Theme.DrawText(g, Unit, Font, ForeColor, rtUnit);
                    }
                    #endregion

                });
            }
            base.Draw(g, Theme, bounds);
        }
        #endregion

        #region MouseDown
        public override void MouseDown(RectangleF bounds, int x, int y)
        {
            Areas(bounds, (rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (CollisionTool.Check(rtValueAll, x, y)) bDown = true;
                if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, x, y)) ButtonDownState = true;
            });
            base.MouseDown(bounds, x, y);
        }
        #endregion
        #region MouseUp
        public override bool MouseUp(RectangleF bounds, int x, int y)
        {
            var ret = false;
                      
            Areas(bounds, (rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (bDown)
                {
                    bDown = false;

                    var Wnd = Control.FindForm() as DvForm;
                    var Theme = Control.GetTheme();
                    if (CollisionTool.Check(rtValue, x, y))
                    {
                        if (Theme.KeyboardInput) Control.SetInput(this, rtValue, this.Value);
                        else
                        {
                            Wnd.Block = true;

                            var ret = DvDialogs.Keyboard.ShowKeyboard(Text ?? "입력", Value);
                            if (ret != null) Value = ret;

                            Wnd.Block = false;
                        }
                    }
                    if (CollisionTool.Check(rtValueAll, x, y)) ret = true;
                }

                if (ButtonDownState)
                {
                    ButtonDownState = false;
                    ret = true;
                    if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, x, y)) ButtonClicked?.Invoke(this, new EventArgs());
                }
            });

            return ret;
        }
        #endregion
        #endregion

        #region Method
        #region GetWidth
        public override float GetWidth()
        {
            float ret = 0F;
            ret = (TitleWidth ?? 0) + (ValueWidth) + (ButtonWidth ?? 0); 
            return ret;
        }
        #endregion
        #region Areas
        public void Areas(RectangleF rtContent, Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var szUnitW = (UnitWidth.HasValue && UnitWidth.Value > 0) ? UnitWidth.Value : 0;
            var szTitleW = (TitleWidth.HasValue && TitleWidth.Value > 0) ? TitleWidth.Value : 0;
            var szButonW = (ButtonWidth.HasValue && ButtonWidth.Value > 0) ? ButtonWidth.Value : 0;

            var rts = Util.DevideSizeH(rtContent, new SizeInfo[] { new SizeInfo(DvSizeMode.Pixel, szTitleW), new SizeInfo(DvSizeMode.Percent, 100F), new SizeInfo(DvSizeMode.Pixel, szUnitW), new SizeInfo(DvSizeMode.Pixel, szButonW) }.ToList());

            var rtTitle = rts[0];
            var rtValue = rts[1];
            var rtUnit = rts[2];
            var rtButton = rts[3];
            var rtValueAll = new RectangleF(rtValue.Left, rtValue.Top, rtUnit.Right - rtValue.Left, rtValue.Height);

            act(rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll);
        }
        #endregion
        #region TextChange
        internal void TextChange(TextBox txt)
        {
            Value = txt.Text;
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvTreeViewInputNumberNode
    public class DvTreeViewInputNumberNode<T> : DvTreeViewNode where T : struct
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
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ValueColor
        private Color? cValueColor = null;
        public Color? ValueColor
        {
            get => cValueColor;
            set
            {
                if (cValueColor != value)
                {
                    cValueColor = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ErrorColor
        private Color? cErrorColor = null;
        public Color? ErrorColor
        {
            get => cErrorColor;
            set
            {
                if (cErrorColor != value)
                {
                    cErrorColor = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion

        #region Unit
        private string strUnit = "";
        public string Unit
        {
            get => strUnit;
            set
            {
                if (strUnit != value)
                {
                    strUnit = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region Button
        private TextIcon texticonBtn = new TextIcon();

        public DvIcon ButtonIcon => texticonBtn.Icon;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap ButtonIconImage
        {
            get => texticonBtn.IconImage;
            set { if (texticonBtn.IconImage != value) { texticonBtn.IconImage = value; Control?.Invalidate(); } }
        }
        public string ButtonIconString
        {
            get => texticonBtn.IconString;
            set { if (texticonBtn.IconString != value) { texticonBtn.IconString = value; Control?.Invalidate(); } }
        }
        public float ButtonIconSize
        {
            get => texticonBtn.IconSize;
            set { if (texticonBtn.IconSize != value) { texticonBtn.IconSize = value; Control?.Invalidate(); } }
        }
        public int ButtonIconGap
        {
            get => texticonBtn.IconGap;
            set { if (texticonBtn.IconGap != value) { texticonBtn.IconGap = value; Control?.Invalidate(); } }
        }
        public DvTextIconAlignment ButtonIconAlignment
        {
            get => texticonBtn.IconAlignment;
            set { if (texticonBtn.IconAlignment != value) { texticonBtn.IconAlignment = value; Control?.Invalidate(); } }
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Button
        {
            get => texticonBtn.Text;
            set { if (texticonBtn.Text != value) { base.Text = texticonBtn.Text = value; Control?.Invalidate(); } }
        }

        public Padding ButtonTextPadding
        {
            get => texticonBtn.TextPadding;
            set { if (texticonBtn.TextPadding != value) { texticonBtn.TextPadding = value; Control?.Invalidate(); } }
        }
        #endregion

        #region TitleWidth
        private int? nTitleWidth = 70;
        public int? TitleWidth
        {
            get => nTitleWidth;
            set
            {
                if (nTitleWidth != value)
                {
                    nTitleWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ValueWidth
        private int nValueWidth = 100;
        public int ValueWidth
        {
            get => nValueWidth;
            set
            {
                if (nValueWidth != value)
                {
                    nValueWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region UnitWidth
        private int? nUnitWidth = null;
        public int? UnitWidth
        {
            get => nUnitWidth;
            set
            {
                if (nUnitWidth != value)
                {
                    nUnitWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ButtonWidth
        private int? nButtonWidth = null;
        public int? ButtonWidth
        {
            get => nButtonWidth;
            set
            {
                if (nButtonWidth != value)
                {
                    nButtonWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion

        #region Value
        public T? Value
        {
            get
            {
                T? ret = null;

                if (typeof(T) == typeof(sbyte))
                {
                    #region short
                    var nMax = (sbyte?)(object)Maximum ?? sbyte.MaxValue;
                    var nMin = (sbyte?)(object)Minimum ?? sbyte.MinValue;
                    sbyte n;
                    if (sbyte.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(short))
                {
                    #region short
                    var nMax = (short?)(object)Maximum ?? short.MaxValue;
                    var nMin = (short?)(object)Minimum ?? short.MinValue;
                    short n;
                    if (short.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(int))
                {
                    #region int
                    var nMax = (int?)(object)Maximum ?? int.MaxValue;
                    var nMin = (int?)(object)Minimum ?? int.MinValue;
                    int n;
                    if (int.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(long))
                {
                    #region long
                    var nMax = (long?)(object)Maximum ?? long.MaxValue;
                    var nMin = (long?)(object)Minimum ?? long.MinValue;
                    long n;
                    if (long.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(byte))
                {
                    #region byte
                    var nMax = (byte?)(object)Maximum ?? byte.MaxValue;
                    var nMin = (byte?)(object)Minimum ?? byte.MinValue;
                    byte n;
                    if (byte.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(ushort))
                {
                    #region ushort
                    var nMax = (ushort?)(object)Maximum ?? ushort.MaxValue;
                    var nMin = (ushort?)(object)Minimum ?? ushort.MinValue;
                    ushort n;
                    if (ushort.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(uint))
                {
                    #region uint
                    var nMax = (uint?)(object)Maximum ?? uint.MaxValue;
                    var nMin = (uint?)(object)Minimum ?? uint.MinValue;
                    uint n;
                    if (uint.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(ulong))
                {
                    #region ulong
                    var nMax = (ulong?)(object)Maximum ?? ulong.MaxValue;
                    var nMin = (ulong?)(object)Minimum ?? ulong.MinValue;
                    ulong n;
                    if (ulong.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(float))
                {
                    #region float
                    var nMax = (float?)(object)Maximum ?? float.MaxValue;
                    var nMin = (float?)(object)Minimum ?? float.MinValue;
                    float n;
                    if (float.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(double))
                {
                    #region double
                    var nMax = (double?)(object)Maximum ?? double.MaxValue;
                    var nMin = (double?)(object)Minimum ?? double.MinValue;
                    double n;
                    if (double.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(decimal))
                {
                    #region decimal
                    var nMax = (decimal?)(object)Maximum ?? decimal.MaxValue;
                    var nMin = (decimal?)(object)Minimum ?? decimal.MinValue;
                    decimal n;
                    if (decimal.TryParse(sVal, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                 
                return ret;
            }
            set
            {
                var v1 = sVal;
                var v2 = value.HasValue ? value.ToString() : "";
                if (v1 != v2)
                {
                    sVal = v2;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region Maximum / Minimum
        public T? Minimum { get; set; } = null;
        public T? Maximum { get; set; } = null;
        #endregion
        #region IsMinusInput
        private bool IsMinusInput
        {
            get
            {
                bool ret = false;
                if (typeof(T) == typeof(sbyte))
                {
                    #region short
                    var nMax = (sbyte?)(object)Maximum ?? sbyte.MaxValue;
                    var nMin = (sbyte?)(object)Minimum ?? sbyte.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(short))
                {
                    #region short
                    var nMax = (short?)(object)Maximum ?? short.MaxValue;
                    var nMin = (short?)(object)Minimum ?? short.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(int))
                {
                    #region int
                    var nMax = (int?)(object)Maximum ?? int.MaxValue;
                    var nMin = (int?)(object)Minimum ?? int.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(long))
                {
                    #region long
                    var nMax = (long?)(object)Maximum ?? long.MaxValue;
                    var nMin = (long?)(object)Minimum ?? long.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(float))
                {
                    #region float
                    var nMax = (float?)(object)Maximum ?? float.MaxValue;
                    var nMin = (float?)(object)Minimum ?? float.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(double))
                {
                    #region double
                    var nMax = (double?)(object)Maximum ?? double.MaxValue;
                    var nMin = (double?)(object)Minimum ?? double.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(decimal))
                {
                    #region decimal
                    var nMax = (decimal?)(object)Maximum ?? decimal.MaxValue;
                    var nMin = (decimal?)(object)Minimum ?? decimal.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                return ret;
            }
        }
        #endregion

        #region Error
        public InputError Error
        {
            get
            {
                var ret = InputError.None;

                if (typeof(T) == typeof(sbyte))
                {
                    #region short
                    sbyte n;
                    var state = sbyte.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((sbyte)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((sbyte)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(short))
                {
                    #region short
                    short n;
                    var state = short.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((short)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((short)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(int))
                {
                    #region int
                    int n;
                    var state = int.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((int)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((int)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(long))
                {
                    #region long
                    long n;
                    var state = long.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((long)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((long)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(byte))
                {
                    #region byte
                    byte n;
                    var state = byte.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((byte)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((byte)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(ushort))
                {
                    #region ushort
                    ushort n;
                    var state = ushort.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((ushort)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((ushort)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(uint))
                {
                    #region uint
                    uint n;
                    var state = uint.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((uint)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((uint)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(ulong))
                {
                    #region float
                    ulong n;
                    var state = ulong.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((ulong)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((ulong)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(float))
                {
                    #region float
                    float n;
                    var state = float.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((float)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((float)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(double))
                {
                    #region double
                    double n;
                    var state = double.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((double)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((double)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(decimal))
                {
                    #region decimal
                    decimal n;
                    var state = decimal.TryParse(sVal, out n);
                    if (!state && string.IsNullOrWhiteSpace(sVal)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((decimal)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((decimal)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }

                return ret;
            }
        }
        #endregion

        #region ButtonDownState
        protected bool ButtonDownState { get; private set; }
        #endregion
        #endregion

        #region Member Variable
        bool bDown = false;
        string sVal = "";
        T? old;
        #endregion

        #region Event
        public event EventHandler ButtonClicked;
        public event EventHandler ValueChanged;
        #endregion

        #region Constructor
        public DvTreeViewInputNumberNode(string Text) : base(Text)
        {
            if (typeof(T) == typeof(sbyte)) { }
            else if (typeof(T) == typeof(short)) { }
            else if (typeof(T) == typeof(int)) { }
            else if (typeof(T) == typeof(long)) { }
            else if (typeof(T) == typeof(byte)) { }
            else if (typeof(T) == typeof(ushort)) { }
            else if (typeof(T) == typeof(uint)) { }
            else if (typeof(T) == typeof(ulong)) { }
            else if (typeof(T) == typeof(float)) { }
            else if (typeof(T) == typeof(double)) { }
            else if (typeof(T) == typeof(decimal)) { }
            else throw new Exception("숫자 자료형이 아닙니다");
        }

        public DvTreeViewInputNumberNode(string Text, string IconString) : base(Text, IconString)
        {
            if (typeof(T) == typeof(sbyte)) { }
            else if (typeof(T) == typeof(short)) { }
            else if (typeof(T) == typeof(int)) { }
            else if (typeof(T) == typeof(long)) { }
            else if (typeof(T) == typeof(byte)) { }
            else if (typeof(T) == typeof(ushort)) { }
            else if (typeof(T) == typeof(uint)) { }
            else if (typeof(T) == typeof(ulong)) { }
            else if (typeof(T) == typeof(float)) { }
            else if (typeof(T) == typeof(double)) { }
            else if (typeof(T) == typeof(decimal)) { }
            else throw new Exception("숫자 자료형이 아닙니다");
        }
        #endregion

        #region Override
        #region Draw
        public override void Draw(Graphics g, DvTheme Theme, RectangleF bounds)
        {
            if (Control != null)
            {
                bounds.Inflate(0, -3);
                Areas(bounds, (rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
                {
                    var useT = TitleWidth.HasValue && TitleWidth.Value > 0;
                    var useB = ButtonWidth.HasValue && ButtonWidth.Value > 0;
                    var useU = UnitWidth.HasValue && UnitWidth.Value > 0;

                    #region Color
                    var Font = Control.Font;
                    var ForeColor = Control.ForeColor;
                    var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
                    var ButtonBorderColor = Theme.GetBorderColor(ButtonColor, Control.BoxColor ?? Theme.ListBackColor);
                    var ValueColor = this.ValueColor ?? Theme.InputColor;
                    var ValueBorderColor = Theme.GetBorderColor(ValueColor, Control.BoxColor ?? Theme.ListBackColor);
                    var ErrorColor = this.ErrorColor ?? Color.FromArgb(220, 0, 0);
                    var ShadowGap = Control.ShadowGap;
                    #endregion
                    #region Round
                    var rndValue = RoundType.L;
                    var rndButton = RoundType.R;

                    if (!ButtonWidth.HasValue)
                    {
                        rndValue = RoundType.All;
                        rndButton = RoundType.Rect;
                    }
                    #endregion

                    #region Title
                    if (useT)
                    {
                        var cT = ForeColor;

                        Theme.DrawTextIcon(g, TextIcon, Control.Font, cT, rtTitle, DvContentAlignment.MiddleLeft);
                    }
                    #endregion
                    #region Button
                    if (useB)
                    {
                        var cV = ButtonDownState ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                        var cB = ButtonDownState ? ButtonBorderColor.BrightnessTransmit(Theme.DownBrightness) : ButtonBorderColor;
                        var cT = ButtonDownState ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;

                        Theme.DrawBox(g, rtButton, cV, cB, rndButton, !ButtonDownState ? Box.ButtonUp_V(true, ShadowGap) : Box.ButtonDown(ShadowGap));
                        if (ButtonDownState) rtButton.Offset(0, 1);
                        Theme.DrawTextIcon(g, texticonBtn, Font, cT, rtButton);
                        if (ButtonDownState) rtButton.Offset(0, -1);
                    }
                    #endregion
                    #region Value
                    {
                        var cV = ValueColor;
                        var cB = ValueBorderColor;
                        var cT = ForeColor;

                        Theme.DrawBox(g, rtValueAll, cV, cB, rndValue, Box.LabelBox(Embossing.FlatConcave, ShadowGap));

                        var rt = Util.INT(rtValue); rt.Offset(1, 0);
                        TextRenderer.DrawText(g, sVal, Control.Font, rt, ForeColor);
                    }
                    #endregion
                    #region Unit
                    if (useU && !string.IsNullOrWhiteSpace(Unit))
                    {
                        #region Sep
                        var szh = Convert.ToInt32(rtValue.Height / 2);
                        var x = rtUnit.Left;

                        using (var p = new Pen(Color.Black))
                        {
                            var hsv = ValueColor.ToHSV();

                            p.Width = 1;

                            p.Color = Theme.GetInBevelColor(ValueColor);

                            var y1 = (rtValue.Top + (rtValue.Height / 2)) - (szh / 2) + 1;
                            var y2 = (rtValue.Top + (rtValue.Height / 2)) + (szh / 2) + 1;

                            g.DrawLine(p, x + 1F, y1, x + 1F, y2);

                            p.Color = ValueColor.BrightnessTransmit(Theme.BorderBrightness);
                            g.DrawLine(p, x + 0F, y1, x + 0F, y2);
                        }
                        #endregion

                        var isz = Math.Min(rtUnit.Width, rtUnit.Height) / 3;
                        if (FA.Contains(Unit)) Theme.DrawIcon(g, new DvIcon { IconString = Unit, IconSize = isz }, ForeColor, rtUnit);
                        else Theme.DrawText(g, Unit, Font, ForeColor, rtUnit);
                    }
                    #endregion
                    #region Error
                    if (Error != InputError.None)
                    {
                        var rt = new RectangleF(rtValue.Left, rtValue.Top, rtValueAll.Width + rtButton.Width, rtValue.Height);
                        Theme.DrawBox(g, rt, ValueColor, ErrorColor, RoundType.All, BoxStyle.Border);
                    }
                    #endregion
                });
            }
            base.Draw(g, Theme, bounds);
        }
        #endregion

        #region MouseDown
        public override void MouseDown(RectangleF bounds, int x, int y)
        {
            Areas(bounds, (rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (CollisionTool.Check(rtValueAll, x, y)) bDown = true;
                if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, x, y)) ButtonDownState = true;
            });
            base.MouseDown(bounds, x, y);
        }
        #endregion
        #region MouseUp
        public override bool MouseUp(RectangleF bounds, int x, int y)
        {
            var ret = false;
            Areas(bounds, (rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (bDown)
                {
                    bDown = false;
                    var Wnd = Control.FindForm() as DvForm;
                    var Theme = Control.GetTheme();
                    if (CollisionTool.Check(rtValue, x, y))
                    {
                        if (Theme.KeyboardInput) Control.SetInput(this, rtValue, sVal);
                        else
                        {
                            Wnd.Block = true;

                            var ret = DvDialogs.Keypad.ShowKeypad<T>(Text ?? "입력", Value);
                            if (ret.HasValue) sVal = ret.Value.ToString();

                            Wnd.Block = false;
                        }
                    }
                    if (CollisionTool.Check(rtValueAll, x, y)) ret = true;
                }

                if (ButtonDownState)
                {
                    ButtonDownState = false;
                    ret = true;
                    if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, x, y)) ButtonClicked?.Invoke(this, new EventArgs());
                }
            });

            return ret;
        }
        #endregion
        #endregion

        #region Method
        #region GetWidth
        public override float GetWidth()
        {
            float ret = 0F;
            ret = (TitleWidth ?? 0) + (ValueWidth) + (ButtonWidth ?? 0);
            return ret;
        }
        #endregion
        #region Areas
        public void Areas(RectangleF rtContent, Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var szUnitW = (UnitWidth.HasValue && UnitWidth.Value > 0) ? UnitWidth.Value : 0;
            var szTitleW = (TitleWidth.HasValue && TitleWidth.Value > 0) ? TitleWidth.Value : 0;
            var szButonW = (ButtonWidth.HasValue && ButtonWidth.Value > 0) ? ButtonWidth.Value : 0;

            var rts = Util.DevideSizeH(rtContent, new SizeInfo[] { new SizeInfo(DvSizeMode.Pixel, szTitleW), new SizeInfo(DvSizeMode.Percent, 100F), new SizeInfo(DvSizeMode.Pixel, szUnitW), new SizeInfo(DvSizeMode.Pixel, szButonW) }.ToList());

            var rtTitle = rts[0];
            var rtValue = rts[1];
            var rtUnit = rts[2];
            var rtButton = rts[3];
            var rtValueAll = new RectangleF(rtValue.Left, rtValue.Top, rtUnit.Right - rtValue.Left, rtValue.Height);

            act(rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll);
        }
        #endregion
        #region TextChange
        internal void TextChange(TextBox txt)
        {
            var textbox = txt;
            var t = typeof(T);
            if (typeof(T) == typeof(float) || typeof(T) == typeof(double) || typeof(T) == typeof(decimal))
            {
                #region Floating
                Int32 selectionStart = textbox.SelectionStart;
                Int32 selectionLength = textbox.SelectionLength;

                String newText = String.Empty;
                bool bComma = false;
                foreach (Char c in textbox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && !bComma && textbox.Text != ".") || (newText.Length == 0 && (c == '-' || c == '+') && IsMinusInput)) newText += c;
                    if (c == '.' && textbox.Text != ".") bComma = true;
                }
                textbox.Text = newText;
                textbox.SelectionStart = selectionStart <= textbox.Text.Length ? selectionStart : textbox.Text.Length;
                #endregion
            }
            else if (typeof(T) == typeof(sbyte) || typeof(T) == typeof(short) || typeof(T) == typeof(int) || typeof(T) == typeof(long) ||
                     typeof(T) == typeof(byte) || typeof(T) == typeof(ushort) || typeof(T) == typeof(uint) || typeof(T) == typeof(ulong))
            {
                #region Number
                Int32 selectionStart = textbox.SelectionStart;
                Int32 selectionLength = textbox.SelectionLength;

                String newText = String.Empty;
                foreach (Char c in textbox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || (newText.Length == 0 && (c == '-' || c == '+') && IsMinusInput)) newText += c;
                }
                textbox.Text = newText;
                textbox.SelectionStart = selectionStart <= textbox.Text.Length ? selectionStart : textbox.Text.Length;
                #endregion
            }

            sVal = txt.Text;

            var v = Value;
            if (!old.Equals(v))
            {
                old = v;
                ValueChanged?.Invoke(this, new EventArgs());
            }

        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvTreeViewInputComboNode
    public class DvTreeViewInputComboNode : DvTreeViewNode
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
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ValueColor
        private Color? cValueColor = null;
        public Color? ValueColor
        {
            get => cValueColor;
            set
            {
                if (cValueColor != value)
                {
                    cValueColor = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion

        #region Button
        private TextIcon texticonBtn = new TextIcon();

        public DvIcon ButtonIcon => texticonBtn.Icon;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap ButtonIconImage
        {
            get => texticonBtn.IconImage;
            set { if (texticonBtn.IconImage != value) { texticonBtn.IconImage = value; Control?.Invalidate(); } }
        }
        public string ButtonIconString
        {
            get => texticonBtn.IconString;
            set { if (texticonBtn.IconString != value) { texticonBtn.IconString = value; Control?.Invalidate(); } }
        }
        public float ButtonIconSize
        {
            get => texticonBtn.IconSize;
            set { if (texticonBtn.IconSize != value) { texticonBtn.IconSize = value; Control?.Invalidate(); } }
        }
        public int ButtonIconGap
        {
            get => texticonBtn.IconGap;
            set { if (texticonBtn.IconGap != value) { texticonBtn.IconGap = value; Control?.Invalidate(); } }
        }
        public DvTextIconAlignment ButtonIconAlignment
        {
            get => texticonBtn.IconAlignment;
            set { if (texticonBtn.IconAlignment != value) { texticonBtn.IconAlignment = value; Control?.Invalidate(); } }
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Button
        {
            get => texticonBtn.Text;
            set { if (texticonBtn.Text != value) { base.Text = texticonBtn.Text = value; Control?.Invalidate(); } }
        }

        public Padding ButtonTextPadding
        {
            get => texticonBtn.TextPadding;
            set { if (texticonBtn.TextPadding != value) { texticonBtn.TextPadding = value; Control?.Invalidate(); } }
        }
        #endregion

        #region TitleWidth
        private int? nTitleWidth = 70;
        public int? TitleWidth
        {
            get => nTitleWidth;
            set
            {
                if (nTitleWidth != value)
                {
                    nTitleWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ValueWidth
        private int nValueWidth = 100;
        public int ValueWidth
        {
            get => nValueWidth;
            set
            {
                if (nValueWidth != value)
                {
                    nValueWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region ButtonWidth
        private int? nButtonWidth = null;
        public int? ButtonWidth
        {
            get => nButtonWidth;
            set
            {
                if (nButtonWidth != value)
                {
                    nButtonWidth = value;
                    Control?.Invalidate();
                }
            }
        }
        #endregion

        #region SelectedIndex
        private int sVal = -1;
        public int SelectedIndex
        {
            get => sVal;
            set
            {
                if (sVal != value)
                {
                    sVal = value;
                    SelectedIndexChanged?.Invoke(this, null);
                    Control?.Invalidate();
                }
            }
        }
        #endregion
        #region Items
        public List<TextIcon> Items { get; private set; } = new List<TextIcon>();
        #endregion
        #region ItemViewCount
        public int ItemViewCount { get; set; } = 8;
        #endregion
        #region ItemHeight
        public int ItemHeight { get; set; } = 30;
        #endregion
        #region ValueText
        private string ValueText => SelectedIndex >= 0 && SelectedIndex < Items.Count ? Items[SelectedIndex].Text : "";
        #endregion

        #region ButtonDownState
        protected bool ButtonDownState { get; private set; }
        #endregion
        #endregion

        #region Member Variable
        bool bDown = false;
        #endregion

        #region Event
        public event EventHandler ButtonClicked;
        public event EventHandler SelectedIndexChanged;
        #endregion

        #region Constructor
        public DvTreeViewInputComboNode(string Text) : base(Text) { }
        public DvTreeViewInputComboNode(string Text, string IconString) : base(Text, IconString) { }
        #endregion

        #region Override
        #region Draw
        public override void Draw(Graphics g, DvTheme Theme, RectangleF bounds)
        {
            if (Control != null)
            {
                bounds.Inflate(0, -3);
                Areas(bounds, (rtContent, rtTitle, rtValue, rtIco, rtText, rtButton) =>
                {
                    var useT = TitleWidth.HasValue && TitleWidth.Value > 0;
                    var useB = ButtonWidth.HasValue && ButtonWidth.Value > 0;

                    #region Color
                    var Font = Control.Font;
                    var ForeColor = Control.ForeColor;
                    var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
                    var ButtonBorderColor = Theme.GetBorderColor(ButtonColor, Control.BoxColor ?? Theme.ListBackColor);
                    var ValueColor = this.ValueColor ?? Theme.InputColor;
                    var ValueBorderColor = Theme.GetBorderColor(ValueColor, Control.BoxColor ?? Theme.ListBackColor);
                    var ShadowGap = Control.ShadowGap;
                    #endregion
                    #region Round
                    var rndValue = RoundType.L;
                    var rndButton = RoundType.R;

                    if (!ButtonWidth.HasValue)
                    {
                        rndValue = RoundType.All;
                        rndButton = RoundType.Rect;
                    }
                    #endregion

                    #region Title
                    if (useT)
                    {
                        var cT = ForeColor;

                        Theme.DrawTextIcon(g, TextIcon, Control.Font, cT, rtTitle, DvContentAlignment.MiddleLeft);
                    }
                    #endregion
                    #region Button
                    if (useB)
                    {
                        var cV = ButtonDownState ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                        var cB = ButtonDownState ? ButtonBorderColor.BrightnessTransmit(Theme.DownBrightness) : ButtonBorderColor;
                        var cT = ButtonDownState ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;

                        Theme.DrawBox(g, rtButton, cV, cB, rndButton, !ButtonDownState ? Box.ButtonUp_V(true, ShadowGap) : Box.ButtonDown(ShadowGap));
                        if (ButtonDownState) rtButton.Offset(0, 1);
                        Theme.DrawTextIcon(g, texticonBtn, Font, cT, rtButton);
                        if (ButtonDownState) rtButton.Offset(0, -1);
                    }
                    #endregion
                    #region Value
                    {
                        var cV = ValueColor;
                        var cB = ValueBorderColor;
                        var cT = ForeColor;

                        #region Box
                        Theme.DrawBox(g, rtValue, cV, cB, rndValue, Box.LabelBox(Embossing.FlatConcave, ShadowGap));
                        #endregion
                        #region Text
                        if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                            Theme.DrawText(g, ValueText, Control.Font, ForeColor, rtText);
                        #endregion
                        #region Icon
                        var nisz = Convert.ToInt32(DrawingTool.PixelToPt(rtIco.Height / 2));
                        Theme.DrawIcon(g, new DvIcon("fa-chevron-down", nisz), ForeColor, rtIco);
                        #endregion
                        #region Unit Sep
                        using (var p = new Pen(Color.Black))
                        {
                            var szh = Convert.ToInt32(rtIco.Height / 2);

                            p.Width = 1;
                            p.Color = ValueColor.BrightnessTransmit(Theme.BorderBrightness);
                            g.DrawLine(p, rtIco.Left + 0F, (rtValue.Top + (rtValue.Height / 2)) - (szh / 2) + 1, rtIco.Left + 0F, (rtValue.Top + (rtValue.Height / 2)) + (szh / 2) + 1);

                            p.Color = Theme.GetInBevelColor(ValueColor);
                            g.DrawLine(p, rtIco.Left + 1F, (rtValue.Top + (rtValue.Height / 2)) - (szh / 2) + 1, rtIco.Left + 1F, (rtValue.Top + (rtValue.Height / 2)) + (szh / 2) + 1);
                        }
                        #endregion
                    }
                    #endregion

                });
            }
            base.Draw(g, Theme, bounds);
        }
        #endregion

        #region MouseDown
        public override void MouseDown(RectangleF bounds, int x, int y)
        {
            Areas(bounds, (rtContent, rtTitle, rtValue, rtIco, rtText, rtButton) =>
            {
                if (CollisionTool.Check(rtValue, x, y)) bDown = true;
                if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, x, y)) ButtonDownState = true;
            });
            base.MouseDown(bounds, x, y);
        }
        #endregion
        #region MouseUp
        public override bool MouseUp(RectangleF bounds, int x, int y)
        {
            var ret = false;
            Areas(bounds, (rtContent, rtTitle, rtValue, rtIco, rtText, rtButton) =>
            {
                if (bDown)
                {
                    bDown = false;
                    if (CollisionTool.Check(rtValue, x, y) && Items != null && Items.Count > 0)
                    {
                        ret = true;

                        OpenDropDown(bounds);
                    }
                }

                if (ButtonDownState)
                {
                    ButtonDownState = false;
                    ret = true;
                    if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, x, y)) ButtonClicked?.Invoke(this, new EventArgs());
                }
            });

            return ret;
        }
        #endregion
        #endregion

        #region Method
        #region GetWidth
        public override float GetWidth()
        {
            float ret = 0F;
            ret = (TitleWidth ?? 0) + (ValueWidth) + (ButtonWidth ?? 0);
            return ret;
        }
        #endregion
        #region Areas
        public void Areas(RectangleF rtContent, Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var szTitleW = (TitleWidth.HasValue && TitleWidth.Value > 0) ? TitleWidth.Value : 0;
            var szButonW = (ButtonWidth.HasValue && ButtonWidth.Value > 0) ? ButtonWidth.Value : 0;

            var rts = Util.DevideSizeH(rtContent, new SizeInfo[] { new SizeInfo(DvSizeMode.Pixel, szTitleW), new SizeInfo(DvSizeMode.Percent, 100F), new SizeInfo(DvSizeMode.Pixel, szButonW) }.ToList());

            var rtTitle = rts[0];
            var rtValue = rts[1];
            var rtButton = rts[2];

            var rtIco = Util.FromRect(rtValue.Right - (rtValue.Height + 10), rtValue.Top, rtValue.Height + 10, rtValue.Height);
            var rtBox = Util.FromRect(rtValue.Left, rtValue.Top, rtValue.Width - rtIco.Width, rtValue.Height);
            var rtText = Util.FromRect(rtBox.Left, rtBox.Top, rtBox.Width, rtBox.Height);


            act(rtContent, rtTitle, rtValue, rtIco, rtText, rtButton);
        }
        #endregion
        #endregion

        #region DropDown
        #region Member Variable
        private bool closedWhileInControl;
        private DropDownContainer dropContainer;
        #endregion

        #region Properties
        #region CanDrop
        protected virtual bool CanDrop
        {
            get
            {
                if (dropContainer != null)
                    return false;

                if (dropContainer == null && closedWhileInControl)
                {
                    closedWhileInControl = false;
                    return false;
                }

                return !closedWhileInControl;
            }
        }
        #endregion
        #region DropState
        public DvDropState DropState { get; private set; }
        #endregion
        #endregion

        #region Method
        #region FreezeDropDown
        internal void FreezeDropDown(bool remainVisible)
        {
            if (dropContainer != null)
            {
                dropContainer.Freeze = true;
                if (!remainVisible)
                    dropContainer.Visible = false;
            }
        }
        #endregion
        #region UnFreezeDropDown
        internal void UnFreezeDropDown()
        {
            if (dropContainer != null)
            {
                dropContainer.Freeze = false;
                if (!dropContainer.Visible)
                    dropContainer.Visible = true;
            }
        }
        #endregion
        #region OpenDropDown
        private void OpenDropDown(RectangleF bounds)
        {
            if (Control != null)
            {
                Control.Move += (o, s) => { if (dropContainer != null) dropContainer.Bounds = GetDropDownBounds(bounds); };

                var vpos = SelectedIndex == -1 ? 0 : SelectedIndex * ItemHeight;
                vpos = (int)MathTool.Constrain(vpos - (ItemHeight * 2), 0, (Items.Count * ItemHeight));

                dropContainer = new DropDownContainer(this);
                dropContainer.Bounds = GetDropDownBounds(bounds);
                dropContainer.DropStateChanged += (o, s) => { DropState = s.DropState; };
                dropContainer.FormClosed += (o, s) =>
                {
                    if (!dropContainer.IsDisposed) dropContainer.Dispose();
                    dropContainer = null;
                    closedWhileInControl = (Control.RectangleToScreen(Control.ClientRectangle).Contains(Cursor.Position));
                    DropState = DvDropState.Closed;
                    Control.Invalidate();

                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        Thread.Sleep(20);
                        Control.VisibleDropDown = false;
                    });
                };
                dropContainer.Shown += (o, s) => Control.VisibleDropDown = true;

                DropState = DvDropState.Dropping;
                dropContainer.VScrollPosition = vpos;
                dropContainer.Show(Control);
                DropState = DvDropState.Dropped;
                Control.Invalidate();
            }
        }
        #endregion
        #region GetDropDownBounds
        private Rectangle GetDropDownBounds(RectangleF bounds)
        {
            if (Control != null)
            {
                RectangleF rtvValue = new RectangleF(0, 0, 0, 0);
                Areas(bounds, (rtContent, rtTitle, rtValue, rtIco, rtText, rtButton) => rtvValue = rtValue);

                int n = Items.Count;
                Point pt = Control.Parent.PointToScreen(new Point(Control.Left + Convert.ToInt32(rtvValue.Left), Control.Top + Convert.ToInt32(rtvValue.Bottom) - 1));
                if (ItemViewCount != -1) n = Items.Count > ItemViewCount ? ItemViewCount : Items.Count;
                Size inflatedDropSize = new Size(Convert.ToInt32(rtvValue.Width), n * ItemHeight + 2);
                Rectangle screenBounds = new Rectangle(pt, inflatedDropSize);
                Rectangle workingArea = Screen.GetWorkingArea(screenBounds);

                if (screenBounds.X < workingArea.X) screenBounds.X = workingArea.X;
                if (screenBounds.Y < workingArea.Y) screenBounds.Y = workingArea.Y;

                if (screenBounds.Right > workingArea.Right && workingArea.Width > screenBounds.Width) screenBounds.X = workingArea.Right - screenBounds.Width;
                if (screenBounds.Bottom > workingArea.Bottom && workingArea.Height > screenBounds.Height) screenBounds.Y = pt.Y - Control.Height - screenBounds.Height + 3;
                return screenBounds;
            }
            else
            {
                return new Rectangle(0, 0, 0, 0);
            }
        }
        #endregion
        #region CloseDropDown
        public void CloseDropDown()
        {
            if (dropContainer != null)
            {
                DropState = DvDropState.Closing;
                dropContainer.Freeze = false;
                dropContainer.Close();
            }
        }
        #endregion
        #region GetDropDownContainerDir
        internal int GetDropDownContainerDir()
        {
            int ret = -1;
            if (Control != null)
            {
                if (DropState == DvDropState.Dropping || DropState == DvDropState.Dropped)
                {
                    var p1 = Control.PointToScreen(new Point(0, 0));
                    var p2 = dropContainer.Location;

                    ret = p1.Y < p2.Y ? 1 : 2;
                }
            }
            return ret;
        }
        #endregion
        #region SetSelectIndexForNotRaiseEvent
        public void SetSelectIndexForNotRaiseEvent(int index)
        {
            sVal = index;
            Control?.Invalidate();
        }
        #endregion
        #endregion

        #region Class
        #region DropWindowEventArgs
        internal class DropWindowEventArgs : EventArgs
        {
            internal DvDropState DropState { get; private set; }
            public DropWindowEventArgs(DvDropState DropState)
            {
                this.DropState = DropState;
            }
        }
        #endregion
        #region DropDownContainer
        public class DropDownContainer : DvForm, IMessageFilter
        {
            #region Properties
            internal bool Freeze { get; set; }
            public DvTreeViewInputComboNode ComboBox { get; private set; }
            public double VScrollPosition
            {
                get => ListBox.ScrollPosition;
                set
                {
                    if (ListBox.ScrollPosition != value)
                    {
                        ListBox.ScrollPosition = value;
                        ListBox.Invalidate();
                    }
                }
            }
            #endregion

            #region Member Variable
            private DvListBox ListBox = new DvListBox();
            #endregion

            #region Event
            internal event EventHandler<DropWindowEventArgs> DropStateChanged;
            #endregion

            #region Constructor
            public DropDownContainer(DvTreeViewInputComboNode c)
            {
                #region Init
                this.BlankForm = true;
                this.DoubleBuffered = true;
                this.StartPosition = FormStartPosition.Manual;
                this.ShowInTaskbar = false;
                this.ControlBox = false;
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.AutoSize = false;
                this.AutoScroll = false;
                this.MinimumSize = new Size(10, 10);
                this.Padding = new Padding(0, 0, 0, 0);

                this.Theme = c.Control.GetTheme();
                #endregion
                #region Set
                var Theme = c.Control.GetTheme();
                this.ComboBox = c;
                this.Font = c.Control.Font;
                Application.AddMessageFilter(this);
                #endregion
                #region ListBox
                ListBox.Dock = DockStyle.Fill;
                ListBox.BoxColor = c.ValueColor ?? Theme.InputColor;
                ListBox.Round = RoundType.Rect;
                ListBox.Items.AddRange(c.Items);
                ListBox.SelectionMode = ItemSelectionMode.Single;
                ListBox.ItemHeight = c.ItemHeight;

                ListBox.ItemClicked += (o, s) =>
                {
                    if (s.Item != null)
                    {
                        if (DropStateChanged != null) DropStateChanged.Invoke(this, new DropWindowEventArgs(DvDropState.Closing));
                        c.SelectedIndex = ListBox.Items.IndexOf(s.Item);
                        this.Close();
                    }
                };

                if (c.SelectedIndex != -1) ListBox.SelectedItems.Add(c.Items[c.SelectedIndex]);

                this.Controls.Add(ListBox);
                #endregion

                #region Color
                var BoxColor = Theme.ListBackColor;
                var ItemColor = c.ValueColor ?? Theme.InputColor;
                var SelectedItemColor = Theme.PointColor;
                
                this.BackColor = ListBox.BackColor = c.Control.BoxColor ?? Theme.ListBackColor;
                this.ForeColor = ListBox.ForeColor = c.Control.ForeColor;
                ListBox.BoxColor = BoxColor;
                ListBox.RowColor = ItemColor;
                ListBox.SelectedColor = SelectedItemColor;
                #endregion
            }
            #endregion

            #region Implements
            #region PreFilterMessage
            public bool PreFilterMessage(ref Message m)
            {
                if (!Freeze && this.Visible && (Form.ActiveForm == null || !Form.ActiveForm.Equals(this)))
                {
                    if (DropStateChanged != null) DropStateChanged.Invoke(this, new DropWindowEventArgs(DvDropState.Closing));
                    this.Close();
                }
                return false;
            }
            #endregion
            #endregion
        }
        #endregion
        #endregion
        #endregion
    }
    #endregion

    #region class : DvTreeViewNode
    public abstract class DvTreeViewNode
    {
        #region Properties
        public DvTreeView Control { get; internal set; }
        public DvTreeViewNode Parents { get; internal set; }
        public DvTreeViewNodeCollection Nodes { get; private set; }
        public bool Expands { get; set; } = true;
        public int Depth { get => (Parents != null ? Parents.Depth + 1 : 0); }

        
        #region Text/Icon
        public TextIcon TextIcon { get; private set; } = new TextIcon();

        public DvIcon Icon => TextIcon.Icon;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap IconImage
        {
            get => TextIcon.IconImage;
            set { if (TextIcon.IconImage != value) { TextIcon.IconImage = value; Control?.Invalidate(); } }
        }
        public string IconString
        {
            get => TextIcon.IconString;
            set { if (TextIcon.IconString != value) { TextIcon.IconString = value; Control?.Invalidate(); } }
        }
        public float IconSize
        {
            get => TextIcon.IconSize;
            set { if (TextIcon.IconSize != value) { TextIcon.IconSize = value; Control?.Invalidate(); } }
        }
        public int IconGap
        {
            get => TextIcon.IconGap;
            set { if (TextIcon.IconGap != value) { TextIcon.IconGap = value; Control?.Invalidate(); } }
        }
        public DvTextIconAlignment IconAlignment
        {
            get => TextIcon.IconAlignment;
            set { if (TextIcon.IconAlignment != value) { TextIcon.IconAlignment = value; Control?.Invalidate(); } }
        }

        public string Text
        {
            get => TextIcon.Text;
            set { if (TextIcon.Text != value) { TextIcon.Text = value; Control?.Invalidate(); } }
        }

        public Padding TextPadding
        {
            get => TextIcon.TextPadding;
            set { if (TextIcon.TextPadding != value) { TextIcon.TextPadding = value; Control?.Invalidate(); } }
        }

        public object Tag { get => TextIcon.Tag; set => TextIcon.Tag = value; }

        #endregion
        #endregion

        #region Event
        internal event EventHandler Changed;
        #endregion

        #region Constructor
        private DvTreeViewNode()
        {
            Nodes = new DvTreeViewNodeCollection(this);
            Nodes.Changed += (o, s) =>
            {
                foreach (var v in Nodes)
                {
                    v.Parents = this;
                    v.Control = Control;
                }
                Changed?.Invoke(this, null);
            };
        }

        public DvTreeViewNode(string Text) : this()
        {
            this.Text = Text;
        }

        public DvTreeViewNode(string Text, string IconString) : this()
        {
            this.Text = Text;
            this.IconString = IconString;
        }
        #endregion

        #region Virtual
        public virtual void Draw(Graphics g, DvTheme Theme, RectangleF bounds)
        {
        }

        public virtual void MouseDown(RectangleF bounds, int x, int y)
        {
        }

        public virtual bool MouseUp(RectangleF bounds, int x, int y)
        {
            return false;
        }
        #endregion

        #region Method
        #region GetWidth
        public abstract float GetWidth();
        #endregion
        #endregion
    }
    #endregion
    #region class : DvTreeViewNodeCollection
    public class DvTreeViewNodeCollection : EventList<DvTreeViewNode>
    {
        public DvTreeViewNode Parent { get; private set; } = null;

        public DvTreeViewNodeCollection(DvTreeViewNode node)
        {
            this.Parent = node;
        }
    }
    #endregion
    #region class : DvTreeViewNodeMouseEventArgs
    public class DvTreeViewNodeMouseEventArgs : EventArgs
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public DvTreeViewNode Node { get; private set; }

        public DvTreeViewNodeMouseEventArgs(int X, int Y, DvTreeViewNode Node)
        {
            this.X = X;
            this.Y = Y;
            this.Node = Node;
        }
    }
    #endregion
}
