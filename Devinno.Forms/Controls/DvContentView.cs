using Devinno.Collections;
using Devinno.Extensions;
using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvContentView : DvControl
    {
        #region Properties
        #region SelectedColor
        private Color cSelectedColor = DvTheme.DefaultTheme.PointColor;
        public Color SelectedColor
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

        #region ContentSize
        private Size szContent = new Size(100, 100);
        public Size ContentSize
        {
            get => szContent; 
            set
            {
                if (szContent != value) { szContent = value; Invalidate(); }
            }
        }
        #endregion

        #region SelectedItems
        public EventList<DvContent> SelectedItems { get; } = new EventList<DvContent>();
        #endregion
        #region Items
        public EventList<DvContent> Items { get; } = new EventList<DvContent>();
        #endregion

        #region SelectionMode
        public ItemSelectionMode SelectionMode { get; set; } = ItemSelectionMode.SINGLE;
        #endregion
        #region DragSelect
        public bool DragSelect { get; set; } = true;
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
        #region ScrollDirection
        public ScrollDirection ScrollDirection
        {
            get => scroll.Direction;
            set
            {
                if (scroll.Direction != value)
                {
                    scroll.Direction = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Gap
        public int Gap { get; set; } = 3;
        #endregion
        #endregion

        #region Member Variable
        private Scroll scroll = new Scroll();
        private List<CI> vls = new List<CI>();
        private Size szprev;
        private MouseEventArgs evdown;
        private Point? downPoint = null;
        private Point? movePoint = null;
        #endregion

        #region Event
        public event EventHandler SelectedChanged;
        #endregion

        #region Constructor
        public DvContentView()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(300, 200);

            scroll.Direction = ScrollDirection;
            scroll.ScrollChanged += (o, s) => this.Invoke(new Action(() => Invalidate()));
            scroll.GetScrollTotal = () => ScrollDirection == ScrollDirection.Vertical ? (vls.Count > 0 ? vls.Max(x => x.Bounds.Bottom) : 0) : (vls.Count > 0 ? vls.Max(x => x.Bounds.Right) : 0);
            scroll.GetScrollTick = () => ScrollDirection == ScrollDirection.Vertical? ContentSize.Height : ContentSize.Width;
            scroll.GetScrollView = () => ScrollDirection == ScrollDirection.Vertical ? (Areas.ContainsKey("rtView") ? Areas["rtView"].Height : 0) : (Areas.ContainsKey("rtView") ? Areas["rtView"].Width : 0);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var scwh = Convert.ToInt32(Scroll.SC_WH * DpiRatio);
            var rtContent = Areas["rtContent"];
            if (ScrollDirection == ScrollDirection.Vertical)
            {
                var rtBox = new Rectangle(rtContent.Left, rtContent.Top, rtContent.Width - scwh, rtContent.Height);
                var rtScroll = new Rectangle(rtBox.Right, rtBox.Top, scwh, rtBox.Height);
                SetArea("rtBox", rtBox);
                SetArea("rtScroll", rtScroll);
            }
            else
            {
                var rtBox = new Rectangle(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height - scwh);
                var rtScroll = new Rectangle(rtBox.Left, rtBox.Bottom, rtBox.Width, scwh );
                SetArea("rtBox", rtBox);
                SetArea("rtScroll", rtScroll);
            }

            var rtb = Areas["rtBox"];
            var rtc = new Rectangle(rtb.X, rtb.Y, rtb.Width, rtb.Height);
            int cw = (int)Math.Floor((double)(rtc.Width) / (double)ContentSize.Width);
            int ch = (int)Math.Floor((double)(rtc.Height) / (double)ContentSize.Height);
            var rtView = MathTool.MakeRectangle(rtc, new Size(cw * ContentSize.Width, ch * ContentSize.Height));
            SetArea("rtView", rtView);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var SelectedColor = UseThemeColor ? Theme.PointColor : this.SelectedColor;
            #endregion
            #region Set
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            #endregion
            #region Init
            var p = new Pen(Color.Black, 2);
            var br = new SolidBrush(Color.Black);
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtBox = Areas["rtBox"];
            var rtScroll = Areas["rtScroll"];
            var rtView = Areas["rtView"];
            #endregion
            #region Draw
            #region Items
            e.Graphics.SetClip(rtView);
            Loop((ls, i, rt, itm) =>
            {
                if (itm.Item.Visible)
                {
                    if (SelectedItems.Contains(itm.Item))
                    {
                        var rtv = new Rectangle(rt.X, rt.Y, rt.Width, rt.Height); rtv.Inflate(Gap, Gap);
                        br.Color = Color.FromArgb(180, Color.Black);
                        e.Graphics.FillRectangle(br, rtv);
                        Theme.DrawBorder(e.Graphics, SelectedColor, BackColor, 3, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                    }
                    itm.Item.Draw(e.Graphics, Theme, rt);
                }
            });
            e.Graphics.ResetClip();
            #endregion
            #region Scroll
            br.Color = Theme.ScrollBarColor;

            Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScroll, RoundType.ALL);
            Theme.DrawBorder(e.Graphics, BackColor.BrightnessTransmit(-Theme.BorderBright), BackColor, 1, rtScroll, RoundType.ALL, BoxDrawOption.BORDER);

            var cCur = Theme.ScrollCursorColor;
            if (scroll.IsScrolling) cCur = Theme.ScrollCursorColor.BrightnessTransmit(0.3);
            else if (scroll.IsTouchMoving) cCur = Theme.PointColor.BrightnessTransmit(0.3);

            var rtcur = scroll.GetScrollCursorRect(rtScroll);
            if (rtcur.HasValue) Theme.DrawBox(e.Graphics, cCur, Theme.ScrollBarColor, rtcur.Value, RoundType.ALL, BoxDrawOption.BORDER);
            #endregion
            #region Drag
            if (DragSelect)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                if (downPoint.HasValue && movePoint.HasValue)
                {
                    var rt = MathTool.MakeRectangle(downPoint.Value, movePoint.Value);
                    p.Width = 1; p.Color = Color.Black; p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    e.Graphics.DrawRectangle(p, rt);
                    p.Width = 1; p.Color = Color.White; p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawRectangle(p, rt);
                }
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
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

        #region OnMouseWheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            scroll.MouseWheel(e);
            Invalidate();
            base.OnMouseWheel(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            evdown = e;
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtScroll = Areas["rtScroll"];

                var sel = false;
                Loop((ls, i, rt, itm) =>
                {
                    if(itm.Item != null && CollisionTool.Check(rt, e.Location))
                    {
                        sel = true;
                    }
                });

                if (DragSelect)
                {
                    if (!sel)
                    {
                        scroll.MouseDown(e, Areas["rtScroll"]);
                        if (scroll.TouchMode && CollisionTool.Check(Areas["rtBox"], e.Location)) scroll.TouchDown(e);
                    }
                    else
                    {
                        downPoint = e.Location;
                        movePoint = null;
                    }
                }
                else
                {
                    scroll.MouseDown(e, Areas["rtScroll"]);
                    if (scroll.TouchMode && CollisionTool.Check(Areas["rtBox"], e.Location)) scroll.TouchDown(e);
                }
            }
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                scroll.MouseMove(e, Areas["rtScroll"]);
                if (scroll.TouchMode) scroll.TouchMove(e);
                if (scroll.IsScrolling) Invalidate();
                if (scroll.TouchMode && scroll.IsTouchScrolling) Invalidate();

                if (DragSelect && downPoint.HasValue) movePoint = e.Location;

                Invalidate();
            }
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtScroll = Areas["rtScroll"];

                scroll.MouseUp(e);
                if (scroll.TouchMode && CollisionTool.Check(rtBox, e.Location)) scroll.TouchUp(e);
            }

            if(downPoint.HasValue)
            {
                if (DragSelect)
                {
                    if (Math.Abs(downPoint.Value.X - e.X) >= 5 || Math.Abs(downPoint.Value.Y - e.Y) >= 5)
                    {
                        var rtdrag = MathTool.MakeRectangle(e.Location, downPoint.Value);
                        #region SelectedClear
                        if ((ModifierKeys & Keys.Shift) != Keys.Shift) { SelectedItems.Clear(); Invalidate(); }
                        #endregion
                        #region Selected
                        bool bChange = false;
                        Loop((ls, i, rt, itm) =>
                        {
                            var v = itm.Item;
                            if (v.Enabled && v.Collision(rt, rtdrag))
                            {
                                bChange = true;
                                SelectedItems.Add(v);
                            }
                        });
                        if (bChange) { SelectedChanged?.Invoke(this, null); Invalidate(); }
                        #endregion
                    }
                    else
                    {
                        #region SelectedClear
                        if ((ModifierKeys & Keys.Shift) != Keys.Shift) { SelectedItems.Clear(); Invalidate(); }
                        #endregion
                        #region Selected
                        bool bChange = false;
                        Loop((ls, i, rt, itm) =>
                        {
                            var v = itm.Item;
                            if (v.Enabled && v.Collision(rt, e.Location))
                            {
                                bChange = true;
                                SelectedItems.Add(v);
                            }
                        });
                        if (bChange) { SelectedChanged?.Invoke(this, null); Invalidate(); }
                        #endregion
                    }
                }
                downPoint = movePoint = null;
            }

            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region MakeList
        void MakeList()
        {
            if (Areas.Count > 1)
            {
                var ls = this.Items.Where(x => x.Visible).ToList();
                var rtView = Areas["rtView"];
                var cw = (int)Math.Floor((double)(rtView.Width) / (double)ContentSize.Width);
                var ch = (int)Math.Floor((double)(rtView.Height) / (double)ContentSize.Height);

                if (vls.Count != ls.Count || szprev.Width != this.Size.Width || szprev.Height != this.Size.Height)
                {
                    szprev = this.Size;

                    if (ScrollDirection == ScrollDirection.Vertical)
                    {
                        var colcnt = cw;
                        vls.Clear();
                        foreach (var v in ls)
                        {
                            var itm = new CI() { Item = v, ColSpan = v.ColSpan, RowSpan = v.RowSpan };

                            var last = vls.LastOrDefault();
                            if (last == null) { itm.ColIndex = 0; itm.RowIndex = 0; }
                            else
                            {
                                var rls = vls.Where(x => last.RowIndex >= x.RowIndex && last.RowIndex < x.RowIndex + x.RowSpan && last.ColIndex + last.ColSpan >= x.ColIndex && last.ColIndex + last.ColSpan < x.ColIndex + x.ColSpan);
                                itm.ColIndex = (rls.Count() > 0 ? rls.Max(x => x.ColIndex + x.ColSpan) : last.ColIndex + last.ColSpan);
                                itm.RowIndex = last.RowIndex;

                                if (itm.ColIndex + itm.ColSpan - 1 >= colcnt)
                                {
                                    itm.RowIndex = last.RowIndex + 1;
                                    var cls = vls.Where(x => last.RowIndex + 1 >= x.RowIndex && last.RowIndex + 1 < x.RowIndex + x.RowSpan && 0 >= x.ColIndex && 0 < x.ColIndex + x.ColSpan);
                                    itm.ColIndex = (cls.Count() > 0 ? cls.Max(x => x.ColIndex + x.ColSpan) : 0);
                                }
                            }

                            int vx = (itm.ColIndex * ContentSize.Width);
                            int vy = (itm.RowIndex * ContentSize.Height);
                            itm.Bounds = new Rectangle(vx, vy, ContentSize.Width * v.ColSpan, ContentSize.Height * v.RowSpan);

                            vls.Add(itm);
                        }
                    }
                    else
                    {
                        var rowcnt = ch;
                        vls.Clear();
                        foreach (var v in ls)
                        {
                            var itm = new CI() { Item = v, ColSpan = v.ColSpan, RowSpan = v.RowSpan };

                            var last = vls.LastOrDefault();
                            if (last == null) { itm.ColIndex = 0; itm.RowIndex = 0; }
                            else
                            {
                                var rls = vls.Where(x => last.ColIndex >= x.ColIndex && last.ColIndex < x.ColIndex + x.ColSpan && last.RowIndex + last.RowSpan >= x.RowIndex && last.RowIndex + last.RowSpan < x.RowIndex + x.RowSpan);
                                itm.RowIndex = (rls.Count() > 0 ? rls.Max(x => x.RowIndex + x.RowSpan) : last.RowIndex + last.RowSpan);
                                itm.ColIndex = last.ColIndex;

                                if (itm.RowIndex + itm.RowSpan - 1 >= rowcnt)
                                {
                                    itm.ColIndex = last.ColIndex + 1;
                                    var cls = vls.Where(x => last.ColIndex + 1 >= x.ColIndex && last.ColIndex + 1 < x.ColIndex + x.ColSpan && 0 >= x.RowIndex && 0 < x.RowIndex + x.RowSpan);
                                    itm.RowIndex = (cls.Count() > 0 ? cls.Max(x => x.RowIndex + x.RowSpan) : 0);
                                }
                            }

                            int vx = (itm.ColIndex * ContentSize.Width);
                            int vy = (itm.RowIndex * ContentSize.Height);
                            itm.Bounds = new Rectangle(vx, vy, ContentSize.Width * v.ColSpan, ContentSize.Height * v.RowSpan);

                            vls.Add(itm);
                        }
                    }
                }
            }
        }
        #endregion

        #region Loop
        private void Loop(Action<List<CI>, int, Rectangle, CI> Func)
        {
            if (Areas.ContainsKey("rtView"))
            {
                var rtView = Areas["rtView"];
                var sc = scroll.ScrollPosition;
                var spos = Convert.ToInt32(scroll.ScrollPositionWithOffset);

                var CWRH = ScrollDirection == ScrollDirection.Vertical ? ContentSize.Height : ContentSize.Width;
                var MaxWH = ScrollDirection == ScrollDirection.Vertical ? rtView.Height : rtView.Width;

                int cw = (int)Math.Floor((double)(rtView.Width) / (double)ContentSize.Width);
                int ch = (int)Math.Floor((double)(rtView.Height) / (double)ContentSize.Height);

                MakeList();

                var ls = vls.Where(itm => CollisionTool.Check(rtView, new Rectangle(rtView.X + itm.Bounds.X + (ScrollDirection == ScrollDirection.Horizon ? spos : 0), rtView.Y + itm.Bounds.Y + (ScrollDirection == ScrollDirection.Vertical ? spos : 0), itm.Bounds.Width, itm.Bounds.Height))).ToList();
                foreach(var itm in ls)
                {
                    var i = vls.IndexOf(itm);
                    var rt = new Rectangle(rtView.X + itm.Bounds.X + (ScrollDirection == ScrollDirection.Horizon ? spos : 0), rtView.Y + itm.Bounds.Y + (ScrollDirection == ScrollDirection.Vertical ? spos : 0), itm.Bounds.Width, itm.Bounds.Height);
                    rt.Inflate(-Gap, -Gap);
                    if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtView))
                    {
                        Func(vls, i, rt, itm);
                    }
                }
            }
        }
        #endregion
        #endregion
    }


    #region class : CI
    internal class CI
    {
        internal int RowIndex { get; set; }
        internal int ColIndex { get; set; }
        internal int RowSpan { get; set; }
        internal int ColSpan { get; set; }
        internal Rectangle Bounds { get; set; }
        internal DvContent Item { get; set; }
    }
    #endregion
    #region class : DvContent
    public abstract class DvContent
    {
        #region Properties
        #region RowSpan
        private int nRowSpan = 1;
        public int RowSpan { get => nRowSpan; set { var n = value; if (n < 1) n = 1; nRowSpan = n; } }
        #endregion
        #region ColSpan
        private int nColSpan = 1;
        public int ColSpan { get => nColSpan; set { var n = value; if (n < 1) n = 1; nColSpan = n; } }
        #endregion
        #region Control
        public DvContentView Control { get; private set; }
        #endregion
        #region Selected
        public bool Selected
        {
            get { return (Control != null ? Control.SelectedItems.Contains(this) : false); }
            set
            {
                if (Control != null)
                {
                    if (value && !Control.SelectedItems.Contains(this)) { Control.SelectedItems.Add(this); Control.Invalidate(); }
                    if (!value && Control.SelectedItems.Contains(this)) { Control.SelectedItems.Remove(this); Control.Invalidate(); }
                }
            }
        }
        #endregion
        #region Visible
        private bool bVisible = true;
        public bool Visible { get => bVisible; set { if (bVisible != value) { bVisible = value; Control?.Invalidate(); } } }
        #endregion
        #region Enabled
        private bool bEnabled = true;
        public bool Enabled { get => bEnabled; set { if (bEnabled != value) { bEnabled = value; Control?.Invalidate(); } } }
        #endregion
        #region Tag
        public object Tag { get; set; }
        #endregion
        #endregion
        #region Constructor
        public DvContent(DvContentView Control) { this.Control = Control; }
        #endregion
        #region Abstract
        public abstract Rectangle GetBounds(Rectangle Bounds);
        #endregion
        #region Virtual
        public virtual void Draw(Graphics g, DvTheme Theme, Rectangle Bounds) { }
        public virtual void MouseDown(Rectangle Bounds, Point p) { }
        public virtual void MouseUp(Rectangle Bounds, Point p) { }
        public virtual void MouseMove(Rectangle Bounds, Point p) { }
        public virtual void MouseDoubleClick(Rectangle Bounds, Point p) { }
        public virtual bool Collision(Rectangle Bounds, Point p) { return CollisionTool.Check(GetBounds(Bounds), p); }
        public virtual bool Collision(Rectangle Bounds, Rectangle Bounds2) { return CollisionTool.Check(GetBounds(Bounds), Bounds2); }
        #endregion
    }
    #endregion
}
