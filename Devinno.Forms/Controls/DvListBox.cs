using Devinno.Collections;
using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
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
        private Color cBoxColor = DvTheme.DefaultTheme.Color1;
        public Color BoxColor
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
        #region ItemColor
        private Color cItemColor = DvTheme.DefaultTheme.Color2;
        public Color ItemColor
        {
            get => cItemColor;
            set
            {
                if (cItemColor != value)
                {
                    cItemColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region SelectedItemColor
        private Color cSelectedItemColor = DvTheme.DefaultTheme.PointColor;
        public Color SelectedItemColor
        {
            get => cSelectedItemColor;
            set
            {
                if (cSelectedItemColor != value)
                {
                    cSelectedItemColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region RowHeight
        private int nRowHeight = 30;
        public int RowHeight
        {
            get => nRowHeight;
            set
            {
                if (nRowHeight != value)
                {
                    nRowHeight = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region SelectedItems
        public List<ListBoxItem> SelectedItems { get; } = new List<ListBoxItem>();
        #endregion
        #region Items
        public EventList<ListBoxItem> Items { get; } = new EventList<ListBoxItem>();
        #endregion
        #region ItemPadding
        private Padding padItem = new Padding(0, 0, 0, 0);
        public Padding ItemPadding
        {
            get => padItem;
            set
            {
                if (padItem != value)
                {
                    padItem = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ContentAlignment
        private DvContentAlignment eContentAlignment = DvContentAlignment.MiddleCenter;
        public DvContentAlignment ContentAlignment
        {
            get { return eContentAlignment; }
            set { if (eContentAlignment != value) { eContentAlignment = value; Invalidate(); } }
        }
        #endregion

        #region SelectionMode
        public ItemSelectionMode SelectionMode { get; set; } = ItemSelectionMode.SINGLE;
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

        #region ScrollPosition
        public int ScrollPosition { get => scroll.ScrollPosition; set => scroll.ScrollPosition = value; }
        #endregion

        #region RectMode
        internal bool RectMode { get; set; } = false;
        #endregion
        #endregion

        #region Event
        public event EventHandler SelectedChanged;
        public event EventHandler<ListBoxItemClickedEventArgs> ItemClicked;
        public event EventHandler<ListBoxItemClickedEventArgs> ItemDoubleClicked;
        #endregion

        #region Member Variable
        private Scroll scroll = new Scroll();
        private ListBoxItem first;
        private Point downPoint;
        private DateTime downTime;
        #endregion

        #region Constructor
        public DvListBox()
        {
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            Size = new Size(150, 150);

            Items.Changed += (o, s) => Invalidate();

            scroll.Direction = ScrollDirection.Vertical;
            scroll.ScrollChanged += (o, s) => Invalidate();
            scroll.GetScrollTotal = () => Items.Count * RowHeight;
            scroll.GetScrollTick = () => RowHeight;
            scroll.GetScrollView = () => Areas.ContainsKey("rtBox") ? Areas["rtBox"].Height : 0;
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var scwh = Convert.ToInt32(Scroll.SC_WH * DpiRatio);
            var rtContent = Areas["rtContent"];
            var rtBox = new Rectangle(rtContent.Left, rtContent.Top, rtContent.Width - scwh, rtContent.Height);
            var rtScroll = new Rectangle(rtBox.Right, rtBox.Top, scwh, rtBox.Height);
            SetArea("rtBox", rtBox);
            SetArea("rtScroll", rtScroll);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BoxColor = UseThemeColor ? Theme.Color2 : this.BoxColor;
            var ItemColor = UseThemeColor ? Theme.Color3 : this.ItemColor;
            var SelectedItemColor = UseThemeColor ? Theme.PointColor : this.SelectedItemColor;
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
            #endregion
            #region Draw
            using (var pp = DrawingTool.GetRoundRectPathLT(rtBox, Theme.Corner))
            {
                if (!RectMode) e.Graphics.SetClip(pp);
                Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtBox, RectMode ? RoundType.NONE : RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);

                Loop((i, rt, itm) =>
                {
                    if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                    {
                        var cRow = SelectedItems.Contains(itm) ? SelectedItemColor : ItemColor;
                        if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom)
                        {
                            #region Top
                            var rtv = new Rectangle(rt.Left, rtBox.Top, rt.Width, rt.Bottom - rtBox.Top);
                            Theme.DrawBox(e.Graphics, cRow, BackColor, rtv, RectMode ? RoundType.NONE : RoundType.LT, BoxDrawOption.BORDER);

                            rtv.Inflate(-1, -1);
                            if (!RectMode)
                            {
                                using (var pth = new GraphicsPath())
                                {
                                    var rtk = new Rectangle(rtv.Left, rtv.Top, Theme.Corner * 2, Theme.Corner * 2);

                                    pth.AddLine(rtv.Right, rtv.Top, rtv.Left + Theme.Corner, rtv.Top);
                                    pth.AddArc(rtk, 270, -90);
                                    pth.AddLine(rtv.Left, rtv.Top + Theme.Corner, rtv.Left, rtv.Bottom);

                                    p.Width = 1;
                                    p.Color = cRow.BrightnessTransmit(Theme.InBevelBright);
                                    e.Graphics.DrawPath(p, pth);
                                }
                            }
                            else
                            {
                                p.Width = 1;
                                p.Color = cRow.BrightnessTransmit(Theme.InBevelBright);
                                e.Graphics.DrawLine(p, rtv.Left, rtv.Top, rtv.Right, rtv.Top);
                                e.Graphics.DrawLine(p, rtv.Left, rtv.Top, rtv.Left, rtv.Bottom);

                            }

                            Theme.DrawTextShadow(e.Graphics, itm.Icon, itm.Text, Font, ForeColor, cRow, rt, DvContentAlignment.MiddleCenter);
                            #endregion
                        }
                        else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom)
                        {
                            #region Bottom
                            var rtv = new Rectangle(rt.Left, rt.Top, rt.Width, rtBox.Bottom - rt.Top);
                            Theme.DrawBox(e.Graphics, cRow, BackColor, rtv, RectMode ? RoundType.NONE : RoundType.LB, BoxDrawOption.BORDER);

                            rtv.Inflate(-1, -1);
                            if (!RectMode)
                            {
                                using (var pth = new GraphicsPath())
                                {
                                    var rtk = new Rectangle(rtv.Left, rtv.Bottom - (Theme.Corner * 2), Theme.Corner * 2, Theme.Corner * 2);

                                    pth.AddLine(rtv.Right, rtv.Top, rtv.Left, rtv.Top);
                                    pth.AddLine(rtv.Left, rtv.Top, rtv.Left, rtv.Bottom - Theme.Corner);
                                    pth.AddArc(rtk, 180, -60);

                                    p.Width = 1;
                                    p.Color = cRow.BrightnessTransmit(Theme.InBevelBright);
                                    e.Graphics.DrawPath(p, pth);
                                }
                            }
                            else
                            {
                                p.Width = 1;
                                p.Color = cRow.BrightnessTransmit(Theme.InBevelBright);
                                e.Graphics.DrawLine(p, rtv.Left, rtv.Top, rtv.Right, rtv.Top);
                                e.Graphics.DrawLine(p, rtv.Left, rtv.Top, rtv.Left, rtv.Bottom);

                            }

                            Theme.DrawTextShadow(e.Graphics, itm.Icon, itm.Text, Font, ForeColor, cRow, rt, DvContentAlignment.MiddleCenter);
                            #endregion
                        }
                        else
                        {
                            #region Else 
                            var rtv = new Rectangle(rt.Left, rt.Top, rt.Width, rt.Height);
                            Theme.DrawBox(e.Graphics, cRow, BackColor, rtv, RectMode ? RoundType.NONE : RoundType.NONE, BoxDrawOption.BORDER);

                            rtv.Inflate(-1, -1);
                            p.Width = 1;
                            p.Color = cRow.BrightnessTransmit(Theme.InBevelBright);
                            e.Graphics.DrawLine(p, rtv.Left, rtv.Top, rtv.Right, rtv.Top);
                            e.Graphics.DrawLine(p, rtv.Left, rtv.Top, rtv.Left, rtv.Bottom);

                            Theme.DrawTextShadow(e.Graphics, itm.Icon, itm.Text, Font, ForeColor, cRow, rt, DvContentAlignment.MiddleCenter);
                            #endregion
                        }
                    }
                });

                if (!RectMode) e.Graphics.ResetClip();

                var border = BackColor.BrightnessTransmit(Theme.BorderBright);
                Theme.DrawBorder(e.Graphics, border, BackColor, 1, rtBox, RectMode ? RoundType.NONE : RoundType.L, BoxDrawOption.BORDER);
            }

            #region Scroll
            br.Color = Theme.ScrollBarColor;
            Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScroll, RectMode ? RoundType.NONE : RoundType.R, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);

            var cCur = scroll.IsScrolling ? Theme.ScrollCursorColor.BrightnessTransmit(0.3) : Theme.ScrollCursorColor;
            var rtcur = scroll.GetScrollCursorRect(rtScroll);
            if (rtcur.HasValue) Theme.DrawBox(e.Graphics, cCur, Theme.ScrollBarColor, rtcur.Value, RoundType.ALL, BoxDrawOption.BORDER);
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

            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                scroll.MouseDown(e, Areas["rtScroll"]);
                if (scroll.TouchMode && CollisionTool.Check(Areas["rtBox"], e.Location)) scroll.TouchDown(e);

                if (CollisionTool.Check(Areas["rtBox"], e.Location))
                {
                    downPoint = e.Location;
                    downTime = DateTime.Now;
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

                scroll.MouseUP(e);
                if (scroll.TouchMode && CollisionTool.Check(rtBox, e.Location)) scroll.TouchUp(e);

                if (CollisionTool.Check(rtBox, e.Location) && Math.Abs(downPoint.Y - e.Y) < Scroll.GapSize && (DateTime.Now - downTime).TotalSeconds < Scroll.GapTime )
                {
                    Loop((i, rtITM, v) =>
                    {
                        if (CollisionTool.Check(rtITM, e.Location))
                        {
                            #region Single Selection
                            if (SelectionMode == ItemSelectionMode.SINGLE)
                            {
                                #region Select
                                SelectedItems.Clear();
                                SelectedItems.Add(v);
                                if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                first = v;
                                #endregion
                            }
                            #endregion
                            #region Multi Selection
                            else if (SelectionMode == ItemSelectionMode.MULTI)
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
                    });
                }
            }
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion

        #region OnMouseClick
        protected override void OnMouseClick(MouseEventArgs e)
        {
            this.Focus();
            if (SelectionMode != ItemSelectionMode.NONE)
            {
                Loop((i, rt, itm) =>
                {
                    if (Math.Abs(downPoint.Y - e.Y) < Scroll.GapSize && (DateTime.Now - downTime).TotalSeconds < Scroll.GapTime && CollisionTool.Check(rt, e.Location))
                        ItemClicked?.Invoke(this, new ListBoxItemClickedEventArgs(itm));
                });
                Invalidate();
            }
            base.OnMouseClick(e);
        }
        #endregion
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            this.Focus();
            if (SelectionMode != ItemSelectionMode.NONE)
            {
                Loop((i, rt, itm) =>
                {
                    if (Math.Abs(downPoint.Y - e.Y) < Scroll.GapSize && (DateTime.Now - downTime).TotalSeconds < Scroll.GapTime && CollisionTool.Check(rt, e.Location))
                        ItemDoubleClicked?.Invoke(this, new ListBoxItemClickedEventArgs(itm));
                });
                Invalidate();
            }
            base.OnMouseDoubleClick(e);
        }
        #endregion
        #endregion

        #region Method
        #region Loop
        private void Loop(Action<int, Rectangle, ListBoxItem> act)
        {
            if (Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                for (int i = 0; i < Items.Count; i++)
                {
                    var itm = Items[i];
                    var rt = new Rectangle(rtBox.Left, scroll.ScrollPositionWithOffset + rtBox.Top + (RowHeight * i), rtBox.Width, RowHeight);
                    if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox)) act(i, rt, itm);
                }
            }
        }
        #endregion
        #endregion
    }

    #region class : ListBoxItem
    public class ListBoxItem
    {
        private DvIcon ico = new DvIcon();

        public string Text { get; set; } = null;
        public object Tag { get; set; } = null;
        public DvIcon Icon { get => ico; }
        public int IconGap { get => ico.Gap; set => ico.Gap = value; }

        public ListBoxItem(string Text) { this.Text = Text; }
        public ListBoxItem(string Text, Bitmap img) : this(Text) { ico.IconImage = img; }
        public ListBoxItem(string Text, string IconString, float size) : this(Text) { ico.IconString = IconString; ico.IconSize = size; }
    }
    #endregion
    #region class : ListBoxItemClickedEventArgs
    public class ListBoxItemClickedEventArgs : EventArgs
    {
        public ListBoxItem Item { get; private set; }

        public ListBoxItemClickedEventArgs(ListBoxItem Item) { this.Item = Item; }
    }
    #endregion
    #region enum : ItemSelectionMode 
    public enum ItemSelectionMode { NONE, SINGLE, MULTI }
    #endregion
}
