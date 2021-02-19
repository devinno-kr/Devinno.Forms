using Devinno.Collections;
using Devinno.Extensions;
using Devinno.Forms.Extensions;
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
    public class DvToolBox : DvControl
    {
        #region Const
        const int IndentWidth = 30;
        const int StartIndent = 10;
        const int IconSize = 6;
        #endregion

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
        #region CategoryColor
        private Color cCategoryColor = DvTheme.DefaultTheme.Color0;
        public Color CategoryColor
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
        #region Categories
        public EventList<ToolCategoryItem> Categories { get; } = new EventList<ToolCategoryItem>();
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
        #endregion

        #region Member Variable
        private Scroll scroll = new Scroll();
        private Point downPoint;
        private DateTime downTime;
        private MouseEventArgs evdown;
        private List<ListBoxItem> ls = new List<ListBoxItem>();
        #endregion

        #region Event
        public event EventHandler<ToolItemMouseDownEventArgs> ToolItemDown;
        public event EventHandler<ToolItemMouseEventArgs> ToolItemUp;
        public event EventHandler<ToolItemMouseEventArgs> ToolItemClick;
        public event EventHandler<ToolItemMouseEventArgs> ToolItemDoubleClick;
        #endregion

        #region Constructor
        public DvToolBox()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(300, 200);
            
            scroll.Direction = ScrollDirection.Vertical;
            scroll.ScrollChanged += (o, s) => this.Invoke(new Action(() => Invalidate()));
            scroll.GetScrollTotal = () => ls.Count * RowHeight;
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
            var CategoryColor = UseThemeColor ? Theme.Color1 : this.CategoryColor;
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
            #region Box
            Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtBox, RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
            #endregion
            #region Items
            var mp = this.PointToClient(MousePosition);
            Loop((i, rt, itm, rtv, rti, rtt, rtc) =>
            {
                if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                {
                    var cRow = itm is ToolCategoryItem ? CategoryColor : BoxColor;
                    var idnt = (itm is ToolCategoryItem ? StartIndent : StartIndent + (IndentWidth / 2));
                    if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom)
                    {
                        #region Top
                        e.Graphics.SetClip(rtv);
                        if (itm is ToolCategoryItem)
                        {
                            var v = itm as ToolCategoryItem;

                            Theme.DrawBox(e.Graphics, cRow, BackColor, rtv, RoundType.LT, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.GRADIENT_V);

                            rtv.Inflate(-1, -1);
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

                            Theme.DrawTextShadow(e.Graphics, new Icons.DvIcon(v.Expands ? "fa-minus" : "fa-plus", IconSize), null, Font, ForeColor, CategoryColor, rti);
                        }
                        Theme.DrawTextShadow(e.Graphics, itm.Icon, itm.Text, Font, ForeColor, cRow, rtt, DvContentAlignment.MiddleLeft);
                        e.Graphics.ResetClip();
                        #endregion
                    }
                    else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom)
                    {
                        #region Bottom
                        e.Graphics.SetClip(rtv);
                        if (itm is ToolCategoryItem)
                        {
                            var v = itm as ToolCategoryItem;

                            Theme.DrawBox(e.Graphics, cRow, BackColor, rtv, RoundType.LB, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.GRADIENT_V);
                            rtv.Inflate(-1, -1);
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
                            Theme.DrawTextShadow(e.Graphics, new Icons.DvIcon(v.Expands ? "fa-minus" : "fa-plus", IconSize), null, Font, ForeColor, CategoryColor, rti);
                        }
                        Theme.DrawTextShadow(e.Graphics, itm.Icon, itm.Text, Font, ForeColor, cRow, rtt, DvContentAlignment.MiddleLeft);
                        e.Graphics.ResetClip();
                        #endregion
                    }
                    else
                    {
                        #region Else 
                        e.Graphics.SetClip(rtv);
                        if (itm is ToolCategoryItem)
                        {
                            var v = itm as ToolCategoryItem;

                            Theme.DrawBox(e.Graphics, cRow, BackColor, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.GRADIENT_V);

                            rtv.Inflate(-1, -1);
                            p.Width = 1;
                            p.Color = cRow.BrightnessTransmit(Theme.InBevelBright);
                            e.Graphics.DrawLine(p, rtv.Left, rtv.Top, rtv.Right, rtv.Top);
                            e.Graphics.DrawLine(p, rtv.Left, rtv.Top, rtv.Left, rtv.Bottom);

                            Theme.DrawTextShadow(e.Graphics, new Icons.DvIcon(v.Expands ? "fa-minus" : "fa-plus", IconSize), null, Font, ForeColor, CategoryColor, rti);
                        }
                        Theme.DrawTextShadow(e.Graphics, itm.Icon, itm.Text, Font, ForeColor, cRow, rtt, DvContentAlignment.MiddleLeft);
                        e.Graphics.ResetClip();
                        #endregion
                    }


                    if (itm is ToolItem && CollisionTool.Check(rtc, mp))
                    {
                        Theme.DrawBorder(e.Graphics, ForeColor.BrightnessTransmit(Theme.BorderBright), BoxColor, 1, rtc, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                    }
                }
            });
            #endregion
            #region Scroll
            br.Color = Theme.ScrollBarColor;
            Theme.DrawBox(e.Graphics, Theme.ScrollBarColor, BackColor, rtScroll, RoundType.R, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);

            var cCur = Theme.ScrollCursorColor;
            if (scroll.IsScrolling) cCur = Theme.ScrollCursorColor.BrightnessTransmit(0.3);
            else if (scroll.IsTouchMoving) cCur = Theme.PointColor.BrightnessTransmit(0.3);

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
            evdown = e;
            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtScroll = Areas["rtScroll"];

                scroll.MouseDown(e, Areas["rtScroll"]);
                if (scroll.TouchMode && CollisionTool.Check(Areas["rtBox"], e.Location)) scroll.TouchDown(e);

                if (CollisionTool.Check(Areas["rtBox"], e.Location))
                {
                    downPoint = e.Location;
                    downTime = DateTime.Now;
                    
                    Loop((i, rt, itm, rtv, rti, rtt, rtc) =>
                    {
                        var v = itm as ToolItem;
                        if (v != null && CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            var idnt = (itm is ToolCategoryItem ? StartIndent : StartIndent + (IndentWidth / 2));
                            if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom)
                            {
                                #region Top
                                if (CollisionTool.Check(rtc, e.Location))
                                {
                                    var arg = new ToolItemMouseDownEventArgs(e.X, e.Y, v);
                                    ToolItemDown?.Invoke(this, arg);

                                    if(arg.Drag)
                                    {
                                        DoDragDrop(v, DragDropEffects.Copy);
                                        scroll.MouseUp(e);
                                        if (scroll.TouchMode && CollisionTool.Check(rtBox, e.Location)) scroll.TouchUp(e);
                                    }
                                }
                                #endregion
                            }
                            else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom)
                            {
                                #region Bottom
                                if (CollisionTool.Check(rtc, e.Location))
                                {
                                    var arg = new ToolItemMouseDownEventArgs(e.X, e.Y, v);
                                    ToolItemDown?.Invoke(this, arg);

                                    if (arg.Drag)
                                    {
                                        DoDragDrop(v, DragDropEffects.Copy);
                                        scroll.MouseUp(e);
                                        if (scroll.TouchMode && CollisionTool.Check(rtBox, e.Location)) scroll.TouchUp(e);
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region Else 
                                if (CollisionTool.Check(rtc, e.Location))
                                {
                                    var arg = new ToolItemMouseDownEventArgs(e.X, e.Y, v);
                                    ToolItemDown?.Invoke(this, arg);

                                    if (arg.Drag)
                                    {
                                        DoDragDrop(v, DragDropEffects.Copy);
                                        scroll.MouseUp(e);
                                        if (scroll.TouchMode && CollisionTool.Check(rtBox, e.Location)) scroll.TouchUp(e);
                                    }
                                }
                                #endregion
                            }
                        }
                    });
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

                if (CollisionTool.Check(rtBox, e.Location) && Math.Abs(downPoint.Y - e.Y) < Scroll.GapSize && (DateTime.Now - downTime).TotalSeconds < Scroll.GapTime)
                {
                    Loop((i, rt, itm, rtv, rti, rtt, rtc) =>
                    {
                        var vcat = itm as ToolCategoryItem;
                        var v = itm as ToolItem;

                        if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom)
                            {
                                if (vcat != null && CollisionTool.Check(rti, e.Location)) vcat.Expands = !vcat.Expands;
                                if (CollisionTool.Check(rtc, e.Location)) ToolItemUp?.Invoke(this, new ToolItemMouseEventArgs(e.X, e.Y, v));
                            }
                            else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom)
                            {
                                if (vcat != null && CollisionTool.Check(rti, e.Location)) vcat.Expands = !vcat.Expands;
                                if (CollisionTool.Check(rtc, e.Location)) ToolItemUp?.Invoke(this, new ToolItemMouseEventArgs(e.X, e.Y, v));
                            }
                            else
                            {
                                if (vcat != null && CollisionTool.Check(rti, e.Location)) vcat.Expands = !vcat.Expands;
                                if (CollisionTool.Check(rtc, e.Location)) ToolItemUp?.Invoke(this, new ToolItemMouseEventArgs(e.X, e.Y, v));
                            }
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
            Focus();

            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtScroll = Areas["rtScroll"];

                if (CollisionTool.Check(Areas["rtBox"], e.Location))
                {
                    Loop((i, rt, itm, rtv, rti, rtt, rtc) =>
                    {
                        var v = itm as ToolItem;
                        if (v != null && CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            var idnt = (itm is ToolCategoryItem ? StartIndent : StartIndent + (IndentWidth / 2));
                            if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom)
                            {
                                if (CollisionTool.Check(rtc, e.Location)) ToolItemClick?.Invoke(this, new ToolItemMouseEventArgs(e.X, e.Y, v));
                            }
                            else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom)
                            {
                                if (CollisionTool.Check(rtc, e.Location)) ToolItemClick?.Invoke(this, new ToolItemMouseEventArgs(e.X, e.Y, v));
                            }
                            else
                            {
                                if (CollisionTool.Check(rtc, e.Location)) ToolItemClick?.Invoke(this, new ToolItemMouseEventArgs(e.X, e.Y, v));
                            }
                        }
                    });
                }
            }
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            Focus();

            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtScroll = Areas["rtScroll"];

                if (CollisionTool.Check(Areas["rtBox"], e.Location))
                {
                    Loop((i, rt, itm, rtv, rti, rtt, rtc) =>
                    {
                        var v = itm as ToolItem;
                        if (v != null && CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom)
                            {
                                if (CollisionTool.Check(rtc, e.Location)) ToolItemDoubleClick?.Invoke(this, new ToolItemMouseEventArgs(e.X, e.Y, v));
                            }
                            else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom)
                            {
                                if (CollisionTool.Check(rtc, e.Location)) ToolItemDoubleClick?.Invoke(this, new ToolItemMouseEventArgs(e.X, e.Y, v));
                            }
                            else
                            {
                                if (CollisionTool.Check(rtc, e.Location)) ToolItemDoubleClick?.Invoke(this, new ToolItemMouseEventArgs(e.X, e.Y, v));
                            }
                        }
                    });
                }
            }
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #endregion

        #region Method
        #region GetToolItem
        public ToolItem GetToolItem(int mx, int my)
        {
            ToolItem ret = null;
            #region Items
            Loop((i, rt, v, rtv, rti, rtt, rtc) =>
            {
                #region Category
                if (v is ToolItem)
                {
                    var itm = (ToolItem)v;
                    if (CollisionTool.Check(rtc, mx, my))
                    {
                        ret = itm;
                    }
                }
                #endregion
            });
            #endregion
            return ret;
        }
        #endregion
        #region MakeList
        void MakeList()
        {
            ls.Clear();
            foreach (var v in Categories)
            {
                if (v.Expands) { ls.Add(v); ls.AddRange(v.Items); }
                else ls.Add(v);
            }
        }
        #endregion
        #region GetListCount
        int GetListCount() => ls.Count;
        #endregion
        #region Loop
        private void Loop(Action<int, Rectangle, ListBoxItem, Rectangle, Rectangle, Rectangle, Rectangle> Func)
        {
            if (Areas.ContainsKey("rtBox"))
            {
                var sc = scroll.ScrollPosition;
                var spos = Convert.ToInt32(scroll.ScrollPositionWithOffset);

                var rtBox = Areas["rtBox"];
                var si = Convert.ToInt32(Math.Floor((double)(sc - scroll.TouchOffset) / (double)RowHeight));
                var cnt = Convert.ToInt32(Math.Ceiling((double)(rtBox.Height - Math.Min(0, scroll.TouchOffset)) / (double)RowHeight));
                var ei = si + cnt;

                if (Categories.Select(x => x.Expands ? x.Items.Count + 1 : 1).Sum() != ls.Count) MakeList();

                using (var g = CreateGraphics())
                {
                    var f = DpiRatio;
                    for (int i = Math.Max(0, si); i < ei + 1 && i < ls.Count; i++)
                    {
                        var itm = ls[i];
                        var rt = new Rectangle(rtBox.Left, spos + rtBox.Top + (RowHeight * i), rtBox.Width, RowHeight);
                        if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            var idnt = (itm is ToolCategoryItem ? StartIndent : StartIndent + (IndentWidth / 2));
                            var sz = g.MeasureTextIcon(itm.Icon, itm.Text, Font);
                            var w = Convert.ToInt32(sz.Width * f);
                            var h = Convert.ToInt32(sz.Height * f);

                            var rtv = new Rectangle(rt.Left, rt.Top, rt.Width, rt.Height);
                            
                            if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom)              rtv = new Rectangle(rt.Left, rtBox.Top, rt.Width, rt.Bottom - rtBox.Top);
                            else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom)   rtv = new Rectangle(rt.Left, rt.Top, rt.Width, rtBox.Bottom - rt.Top);

                            var rti = new Rectangle(rt.Left, rt.Top, itm is ToolCategoryItem ? rt.Height : idnt, rt.Height);
                            var rtt = itm is ToolCategoryItem ? new Rectangle(rt.Left + rti.Width, rt.Top, rt.Width - rti.Width, rti.Height)
                                                              : new Rectangle(rt.Left + idnt, rt.Top, rt.Width - idnt, rti.Height);

                            var rtc = DrawingTool.MakeRectangleAlign(rtt, new Size(w, h), DvContentAlignment.MiddleLeft);
                            rtc.Inflate(5, 5);

                            Func(i, rt, itm, rtv, rti, rtt, rtc);
                        }
                    }
                }
            }
        }
        #endregion
        #region GetDragItem
        public ToolItem GetDragItem(IDataObject o) => o.GetData(typeof(ToolItem)) as ToolItem;
        #endregion
        #endregion
    }

    #region class : ToolCategoryItem
    public class ToolCategoryItem : ListBoxItem
    {
        #region Properties
        public bool Expands { get; set; } = true;
        public EventList<ToolItem> Items { get; } = new EventList<ToolItem>();
        #endregion
        #region Constructor
        public ToolCategoryItem(string Text) : base(Text) => Items.Changed += (o, s) => Changed?.Invoke(this, null);
        public ToolCategoryItem(string Text, Bitmap Image) : base(Text, Image) => Items.Changed += (o, s) => Changed?.Invoke(this, null);
        public ToolCategoryItem(string Text, string IconString, float Size) : base(Text, IconString, Size) => Items.Changed += (o, s) => Changed?.Invoke(this, null);
        public ToolCategoryItem(string Text, string IconString, float size, int Gap) : base(Text, IconString, size, Gap) => Items.Changed += (o, s) => Changed?.Invoke(this, null);
        #endregion
        #region Event
        internal event EventHandler Changed;
        #endregion
    }
    #endregion
    #region class : ToolItem
    public class ToolItem : ListBoxItem
    {
        #region Constructor
        public ToolItem(string Text) : base(Text) { }
        public ToolItem(string Text, Bitmap Image) : base(Text, Image) { }
        public ToolItem(string Text, string IconString, float Size) : base(Text,IconString, Size) { }
        public ToolItem(string Text, string IconString, float size, int Gap) : base(Text, IconString, size, Gap) { }
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
