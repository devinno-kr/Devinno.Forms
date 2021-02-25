using Devinno.Collections;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
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
    public class DvContentGrid : DvControl
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
        #region PageSelectorColor
        private Color cPageSelectorColor = DvTheme.DefaultTheme.Color5;
        public Color PageSelectorColor
        {
            get => cPageSelectorColor;
            set
            {
                if (cPageSelectorColor != value)
                {
                    cPageSelectorColor = value;
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

        #region Pages
        public EventList<DvContentGridPage> Pages { get; } = new EventList<DvContentGridPage>();
        #endregion
        #region CurrentPageIndex
        public int CurrentPageIndex
        {
            get => swipe.CurrentPage;
            set  
            {
                swipe.CurrentPage = value;
                Invalidate();
            }
        }
        #endregion
        #region CurrentPage
        public DvContentGridPage CurrentPage
        {
            get => (CurrentPageIndex >= 0 && CurrentPageIndex < Pages.Count ? Pages[CurrentPageIndex] : null);
            set => CurrentPageIndex = value == null ? -1 : Pages.IndexOf(value);
        }
        #endregion

        #region TouchMode
        public bool TouchMode
        {
            get => swipe.TouchMode;
            set
            {
                if (swipe.TouchMode != value)
                {
                    swipe.TouchMode = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Gap
        public int Gap { get; set; } = 3;
        #endregion
        #region AutoArrange
        public bool AutoArrange { get; set; } = true;
        #endregion
        #region TouchAreaSize
        public int TouchAreaSize { get; set; } = 60;
        #endregion
        #region Selectable
        public bool Selectable { get; set; } = false;
        #endregion

        #endregion

        #region Member Variable
        private Swipe swipe = new Swipe();
        private Point? downPoint = null;
        private Point? movePoint = null;
        #endregion

        #region Event
        public event EventHandler SelectedChanged;
        #endregion

        #region Constructor
        public DvContentGrid()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(300, 200);

            swipe.GetPageCount = new Func<int>(() => Pages.Count);
            swipe.GetPageWidth = new Func<int>(() => Areas.ContainsKey("rtBox") ? Areas["rtBox"].Width : 0);
            swipe.ScrollChanged += (o, s) => this.Invoke(new Action(Invalidate));
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var gp = Convert.ToInt32(10 * DpiRatio);
            var twh = TouchMode && Selectable ? TouchAreaSize : 0;
            var pw = Convert.ToInt32(30 * DpiRatio);
            var ph = Convert.ToInt32(20 * DpiRatio);
            var rtContent = Areas["rtContent"];

            var rtBox = new Rectangle(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height - twh - ph - (gp * 2));
            var rtTouchArea = new Rectangle(rtBox.Left, rtBox.Bottom + gp, rtBox.Width, twh);
            var rtPageNaviBar = new Rectangle(rtBox.Left, rtTouchArea.Bottom + gp, rtBox.Width, ph);
            var rtPageNavi = DrawingTool.MakeRectangleAlign(rtPageNaviBar, new Size(pw * Pages.Count, ph), DvContentAlignment.MiddleCenter);
            SetArea("rtBox", rtBox);
            SetArea("rtPageNavi", rtPageNavi);
            for (int i = 0; i < Pages.Count; i++) SetArea("rtPageNavi" + i, new Rectangle(rtPageNavi.X + (i * pw), rtPageNavi.Y, pw, ph));
            SetArea("rtTouchArea", rtTouchArea);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var SelectedColor = UseThemeColor ? Theme.PointColor : this.SelectedColor;
            var PageSelectorColor = UseThemeColor ? Theme.Color5 : this.PageSelectorColor;
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
            var rtTouchArea = Areas["rtTouchArea"];
            var rtPageNavi = Areas["rtPageNavi"];
            #endregion
            #region Draw
            #region Page
            var spos = Convert.ToInt32(swipe.ScrollPositionWithOffset);
            if (Pages.Count > 1)
            {
                var ci = CurrentPageIndex;
                var pi = CurrentPageIndex - 1 >= 0 ? CurrentPageIndex - 1 : Pages.Count - 1;
                var ni = CurrentPageIndex + 1 < Pages.Count ? CurrentPageIndex + 1 : 0;

                if (pi >= 0 && pi < Pages.Count) Pages[pi].Draw(e, Theme, this, new Rectangle(rtBox.X + (rtBox.Width * -1) + spos, rtBox.Y, rtBox.Width, rtBox.Height), rtBox);
                if (ci >= 0 && ci < Pages.Count) Pages[ci].Draw(e, Theme, this, new Rectangle(rtBox.X + (rtBox.Width * 0) + spos, rtBox.Y, rtBox.Width, rtBox.Height), rtBox);
                if (ni >= 0 && ni < Pages.Count) Pages[ni].Draw(e, Theme, this, new Rectangle(rtBox.X + (rtBox.Width * 1) + spos, rtBox.Y, rtBox.Width, rtBox.Height), rtBox);
            }
            #endregion
            #region TouchArea
            if (TouchMode && Selectable)
            {
                var rtTA = new Rectangle(rtTouchArea.X, rtTouchArea.Y, rtTouchArea.Width, rtTouchArea.Height);

                br.Color = Color.FromArgb(30, Color.Black);
                e.Graphics.FillRoundRectangle(br, rtTA, Theme.Corner);

                p.Color = Color.FromArgb(30, Color.White); p.Width = 1;
                e.Graphics.DrawRoundRectangle(p, rtTA, Theme.Corner);

                using (var ft = new Font(Font.FontFamily, 7, FontStyle.Bold))
                {
                    Theme.DrawText(e.Graphics, null, "TOUCH\r\nAREA", ft, p.Color, rtTA);
                }
            }
            #endregion
            #region Page Navigation
            for (int i = 0; i < Pages.Count; i++)
            {
                var rtNavi = Areas["rtPageNavi" + i];
                var wh = Convert.ToInt32(rtNavi.Height * 0.5);
                var rt = DrawingTool.MakeRectangleAlign(rtNavi, new Size(wh, wh), DvContentAlignment.MiddleCenter);
                Theme.DrawBorder(e.Graphics, PageSelectorColor, BackColor, 2, rt, RoundType.ELLIPSE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);

                if (i == CurrentPageIndex)
                {
                    rt.Inflate(-3, -3);
                    Theme.DrawBox(e.Graphics, PageSelectorColor, BackColor, rt, RoundType.ELLIPSE, BoxDrawOption.OUT_SHADOW);
                }
            }
            #endregion
            #region Drag
            if (Selectable)
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
            CurrentPageIndex += (e.Delta / -120);
            base.OnMouseWheel(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            if (Areas.ContainsKey("rtPageNavi") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtPageNavi = Areas["rtPageNavi"];
                var rtTouchArea = Areas["rtTouchArea"];

                if (!swipe.IsPageChanging && CurrentPage != null)
                {
                    var spos = Convert.ToInt32(swipe.ScrollPositionWithOffset);
                    CurrentPage.MouseDown(e, this, new Rectangle(rtBox.X + (rtBox.Width * 0) + spos, rtBox.Y, rtBox.Width, rtBox.Height), rtBox);
                }

                if (Selectable)
                {
                    if (swipe.TouchMode && CollisionTool.Check(rtTouchArea, e.Location)) swipe.TouchDown(e);
                    if (!swipe.IsPageChanging && CollisionTool.Check(rtBox, e.Location))
                    {
                        downPoint = e.Location;
                        movePoint = null;
                    }
                }
                else
                {
                    if (swipe.TouchMode && CollisionTool.Check(rtBox, e.Location)) swipe.TouchDown(e);

                }

                #region Page Navigation
                for (int i = 0; i < Pages.Count; i++)
                {
                    var rtNavi = Areas["rtPageNavi" + i];
                    if (CollisionTool.Check(rtNavi, e.Location)) CurrentPageIndex = i;
                }
                #endregion
            }
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtPageNavi") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtPageNavi = Areas["rtPageNavi"];

                if (!swipe.IsPageChanging && CurrentPage != null)
                {
                    var spos = Convert.ToInt32(swipe.ScrollPositionWithOffset);
                    CurrentPage.MouseMove(e, this, new Rectangle(rtBox.X + (rtBox.Width * 0) + spos, rtBox.Y, rtBox.Width, rtBox.Height), rtBox);
                }

                if (swipe.TouchMode) swipe.TouchMove(e);
                if (swipe.TouchMode && swipe.IsTouchScrolling) Invalidate();

                if (Selectable && downPoint.HasValue) movePoint = e.Location;
            }
            Invalidate();
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtPageNavi") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtPageNavi = Areas["rtPageNavi"];

                if (!swipe.IsPageChanging && CurrentPage != null)
                {
                    var spos = Convert.ToInt32(swipe.ScrollPositionWithOffset);
                    CurrentPage.MouseUp(e, this, new Rectangle(rtBox.X + (rtBox.Width * 0) + spos, rtBox.Y, rtBox.Width, rtBox.Height), rtBox, downPoint, movePoint);
                }

                if (swipe.TouchMode) swipe.TouchUp(e);

                downPoint = movePoint = null;
            }
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtPageNavi") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtPageNavi = Areas["rtPageNavi"];

                if (!swipe.IsPageChanging && CurrentPage != null)
                {
                    var spos = Convert.ToInt32(swipe.ScrollPositionWithOffset);
                    CurrentPage.MouseDoubleClick(e, this, new Rectangle(rtBox.X + (rtBox.Width * 0) + spos, rtBox.Y, rtBox.Width, rtBox.Height), rtBox);
                }

                if (swipe.TouchMode) swipe.TouchUp(e);
            }
            Invalidate();
            base.OnMouseDoubleClick(e);
        }
        #endregion
        #endregion

        #region Method
        internal void InvokeSelectedChanged() => SelectedChanged?.Invoke(this, null);
        #endregion
    }

    #region class : DvContentGridPage
    public class DvContentGridPage
    {
        #region Properties
        public string PageName { get; set; }
        public List<DvContent> Items { get; } = new List<DvContent>();
        #endregion
        #region Member Variable
        private Size szprev;
        List<DvContent> vls = new List<DvContent>();
        #endregion

        #region Draw
        internal void Draw(PaintEventArgs e, DvTheme Theme, DvContentGrid c, Rectangle rtPage, Rectangle rtBox)
        {
            #region Color
            var SelectedColor = c.UseThemeColor ? Theme.PointColor : c.SelectedColor;
            var PageSelectorColor = c.UseThemeColor ? Theme.Color5 : c.PageSelectorColor;
            #endregion

            Loop(c, rtPage, rtBox, (ls, i, rt, itm) =>
            {
                itm.Draw(e.Graphics, Theme, rt);

                if (itm.Visible)
                {
                    if (c.SelectedItems.Contains(itm))
                    {
                        var rtv = new Rectangle(rt.X, rt.Y, rt.Width, rt.Height); rtv.Inflate(c.Gap, c.Gap);
                        Theme.DrawBorder(e.Graphics, SelectedColor, c.BackColor, 3, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                    }
                    itm.Draw(e.Graphics, Theme, rt);
                }
            });
        }
        #endregion
        #region MouseDown
        internal void MouseDown(MouseEventArgs e, DvContentGrid c, Rectangle rtPage, Rectangle rtBox)
        {
            Loop(c, rtPage, rtBox, (ls, i, rt, itm) =>
            {
                if (itm.Enabled && itm.Visible && itm.Collision(rt, e.Location)) itm.MouseDown(rt, e.Location);
            });
        }
        #endregion
        #region MouseMove
        internal void MouseMove(MouseEventArgs e, DvContentGrid c, Rectangle rtPage, Rectangle rtBox)
        {
            Loop(c, rtPage, rtBox, (ls, i, rt, itm) =>
            {
                if (itm.Enabled && itm.Visible) itm.MouseMove(rt, e.Location);
            });
        }
        #endregion
        #region MouseUp
        internal void MouseUp(MouseEventArgs e, DvContentGrid c, Rectangle rtPage, Rectangle rtBox, Point? downPoint, Point? movePoint)
        {
            Loop(c, rtPage, rtBox, (ls, i, rt, itm) =>
            {
                if (itm.Enabled && itm.Visible) itm.MouseUp(rt, e.Location);
            });

            if (c.Selectable && downPoint.HasValue)
            {
                if (Math.Abs(downPoint.Value.X - e.X) >= 5 || Math.Abs(downPoint.Value.Y - e.Y) >= 5)
                {
                    var rtdrag = MathTool.MakeRectangle(e.Location, downPoint.Value);
                    #region SelectedClear
                    if ((DvContentGrid.ModifierKeys & Keys.Shift) != Keys.Shift) { c.SelectedItems.Clear(); c.Invalidate(); }
                    #endregion
                    #region Selected
                    bool bChange = false;
                    Loop(c, rtPage, rtBox, (ls, i, rt, itm) =>
                    {
                        var v = itm;
                        if (v.Enabled && v.Collision(rt, rtdrag))
                        {
                            bChange = true;
                            c.SelectedItems.Add(v);
                        }
                    });
                    if (bChange) { c.InvokeSelectedChanged(); c.Invalidate(); }
                    #endregion
                }
                else
                {
                    #region SelectedClear
                    if ((DvContentGrid.ModifierKeys & Keys.Shift) != Keys.Shift) { c.SelectedItems.Clear(); c.Invalidate(); }
                    #endregion
                    #region Selected
                    bool bChange = false;
                    Loop(c, rtPage, rtBox, (ls, i, rt, itm) =>
                    {
                        var v = itm;
                        if (v.Enabled && v.Collision(rt, e.Location))
                        {
                            bChange = true;
                            c.SelectedItems.Add(v);
                        }
                    });
                    if (bChange) { c.InvokeSelectedChanged(); c.Invalidate(); }
                    #endregion
                }
                downPoint = movePoint = null;
            }
        }
        #endregion
        #region MouseDoubleClick
        internal void MouseDoubleClick(MouseEventArgs e, DvContentGrid c, Rectangle rtPage, Rectangle rtBox)
        {
            Loop(c, rtPage, rtBox, (ls, i, rt, itm) =>
            {
                if (itm.Enabled && itm.Visible) itm.MouseDoubleClick(rt, e.Location);
            });
        }
        #endregion

        #region Arrange
        private void Arrange(DvContentGrid c, Rectangle rtPage, Rectangle rtBox)
        {
            var ls = this.Items.Where(x => x.Visible).ToList();

            if (vls.Count != ls.Count || szprev.Width != c.Size.Width || szprev.Height != c.Size.Height)
            {
                szprev = c.Size;

                var cw = (int)Math.Floor((double)(rtBox.Width) / (double)c.ContentSize.Width);
                int ir = 0, ic = 0;
                vls.Clear();
                for (int i = 0; i < ls.Count; i++)
                {
                    var itm = ls[i];

                    itm.ColIndex = ic;
                    itm.RowIndex = ir;

                    vls.Add(itm);

                    var vitm = i + 1 < ls.Count ? ls[i + 1] : null;
                    do
                    {
                        ic++;
                        if (vitm != null && ic + vitm.ColSpan > cw) { ir++; ic = 0; }

                    } while (vitm != null && vls.Where(x => x.Check(ic, ir, vitm.ColSpan, vitm.RowSpan)).Count() > 0);
                }
            }
        }

        private void NoArrange(DvContentGrid c, Rectangle rtPage, Rectangle rtBox)
        {
            var ls = this.Items.Where(x => x.Visible).ToList();

            if (vls.Count != ls.Count || szprev.Width != c.Size.Width || szprev.Height != c.Size.Height)
            {
                szprev = c.Size;
                vls.Clear();
                vls.AddRange(ls);
            }
        }
        #endregion
        #region Loop
        private void Loop(DvContentGrid c, Rectangle rtPage, Rectangle rtBox, Action<List<DvContent>, int, Rectangle, DvContent> Func)
        {
            var CWRH = c.ContentSize.Height;
            var MaxWH = rtBox.Height;

            int cw = (int)Math.Floor((double)(rtBox.Width) / (double)c.ContentSize.Width);
            int ch = (int)Math.Floor((double)(rtBox.Height) / (double)c.ContentSize.Height);

            if (c.AutoArrange) Arrange(c, rtPage, rtBox);
            else NoArrange(c, rtPage, rtBox);

            var ls = vls.Where(itm => CollisionTool.Check(rtBox, new Rectangle(rtBox.X + itm.Bounds.X + rtPage.X, rtBox.Y + itm.Bounds.Y, itm.Bounds.Width, itm.Bounds.Height))).ToList();
            foreach (var itm in ls)
            {
                var i = vls.IndexOf(itm);
                var rt = new Rectangle(rtBox.X + itm.Bounds.X + rtPage.X, rtBox.Y + itm.Bounds.Y, itm.Bounds.Width, itm.Bounds.Height);
                rt.Inflate(-c.Gap, -c.Gap);
                if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                {
                    Func(vls, i, rt, itm);
                }
            }
        }
        #endregion
    }
    #endregion
}
