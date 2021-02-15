using Devinno.Collections;
using Devinno.Extensions;
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
    public class DvTreeView : DvControl
    {
        #region Const
        const int IndentWidth = 20;
        const int IconSize = 10;
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
        #region RadioColor
        private Color cRadioColor = DvTheme.DefaultTheme.PointColor;
        public Color RadioColor
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
        #region Nodes
        public TreeViewNodeCollection Nodes { get; } = new TreeViewNodeCollection(null);
        #endregion

        #region SelectionMode
        public ItemSelectionMode SelectionMode { get; set; } = ItemSelectionMode.SINGLE;
        #endregion
        #region SelectedItems
        public List<TreeViewNode> SelectedItems { get; } = new List<TreeViewNode>();
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
        private TreeViewNode first = null;
        private int ListCount;
        #endregion

        #region Event
        public event EventHandler<TreeViewNodeMouseEventArgs> NodeDown;
        public event EventHandler<TreeViewNodeMouseEventArgs> NodeUp;
        public event EventHandler<TreeViewNodeMouseEventArgs> NodeClick;
        public event EventHandler<TreeViewNodeMouseEventArgs> NodeDoubleClick;
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

            Size = new Size(300, 200);

            Nodes.Changed += (o, s) => { Invalidate(); };

            scroll.Direction = ScrollDirection.Vertical;
            scroll.ScrollChanged += (o, s) => Invalidate();
            scroll.GetScrollTotal = () =>  GetListCount() * RowHeight;
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
            var RadioColor = UseThemeColor ? Theme.Color4 : this.RadioColor;
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
            #endregion
            #region Draw
            #region Box
            Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtBox, RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
            #endregion
            #region Items
            var mp = this.PointToClient(MousePosition);
            Loop((ls, i, rt, itm, rtv, rti, rtt, rtc) =>
            {
                if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                {
                    if(SelectedItems.Contains(itm))
                    {
                        Theme.DrawBox(e.Graphics, SelectedColor, BoxColor, rtc, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                    }    

                    e.Graphics.SetClip(rtv);
                    if (itm.Nodes.Count > 0)
                    {
                        var ic = Convert.ToInt32(RowHeight / 1.5);
                        var c = BoxColor.BrightnessTransmit(Theme.InShadowBright);
                        var rtb = DrawingTool.MakeRectangleAlign(rti, new Size(24, 24), DvContentAlignment.MiddleCenter);
                        Theme.DrawBox(e.Graphics, c, BoxColor, rtb, RoundType.ELLIPSE, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL);

                        if(!itm.Expands)
                        {
                            rtb.Inflate(-3, -3);
                            Theme.DrawBox(e.Graphics, RadioColor, BoxColor, rtb, RoundType.ELLIPSE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V | BoxDrawOption.IN_BEVEL);
                        }
                    }
                    else
                    {
                        var dot = DrawingTool.MakeRectangleAlign(rti, new Size(5, 5), DvContentAlignment.MiddleCenter);
                        Theme.DrawBox(e.Graphics, RadioColor, BoxColor, dot, RoundType.ELLIPSE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                    }
                    Theme.DrawTextShadow(e.Graphics, itm.Icon, itm.Text, Font, ForeColor, BoxColor, rtt, DvContentAlignment.MiddleLeft);
                    e.Graphics.ResetClip();


                    if (CollisionTool.Check(rtc, mp)) Theme.DrawBorder(e.Graphics, ForeColor.BrightnessTransmit(Theme.BorderBright), BoxColor, 1, rtc, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
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
                bool bSel = false;
                
                if (CollisionTool.Check(Areas["rtBox"], e.Location))
                {
                    downPoint = e.Location;
                    downTime = DateTime.Now;

                    Loop((ls, i, rt, itm, rtv, rti, rtt, rtc) =>
                    {
                        if (itm != null && CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            if (CollisionTool.Check(rtc, e.Location))
                            {
                                NodeDown?.Invoke(this, new TreeViewNodeMouseEventArgs(e.X, e.Y, itm));
                                bSel = true;
                            }
                        }
                    });
                }

                if (!bSel)
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
                    Loop((ls, i, rt, itm, rtv, rti, rtt, rtc) =>
                    {
                        if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom)
                            {
                                if (CollisionTool.Check(rti, e.Location)) { itm.Expands = !itm.Expands; }
                                if (CollisionTool.Check(rtc, e.Location)) NodeUp?.Invoke(this, new TreeViewNodeMouseEventArgs(e.X, e.Y, itm));
                            }
                            else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom)
                            {
                                if (CollisionTool.Check(rti, e.Location)) { itm.Expands = !itm.Expands; }
                                if (CollisionTool.Check(rtc, e.Location)) NodeUp?.Invoke(this, new TreeViewNodeMouseEventArgs(e.X, e.Y, itm));
                            }
                            else
                            {
                                if (CollisionTool.Check(rti, e.Location)) { itm.Expands = !itm.Expands; }
                                if (CollisionTool.Check(rtc, e.Location)) NodeUp?.Invoke(this, new TreeViewNodeMouseEventArgs(e.X, e.Y, itm));
                            }

                            #region Selection
                            if (CollisionTool.Check(rtc, e.Location))
                            {
                                #region Single Selection
                                if (SelectionMode == ItemSelectionMode.SINGLE)
                                {
                                    SelectedItems.Clear();
                                    SelectedItems.Add(itm);
                                    if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                    first = itm;
                                }
                                #endregion
                                #region Multi Selection
                                else if (SelectionMode == ItemSelectionMode.MULTI)
                                {
                                    if ((ModifierKeys & Keys.Control) == Keys.Control)
                                    {
                                        #region Control
                                        if (SelectedItems.Contains(itm))
                                        {
                                            SelectedItems.Remove(itm);
                                            if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                        }
                                        else
                                        {
                                            SelectedItems.Add(itm);
                                            if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                            first = itm;
                                        }
                                        #endregion
                                    }
                                    else if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                                    {
                                        #region Shift
                                        if (first == null)
                                        {
                                            SelectedItems.Add(itm);
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
                                                if (!SelectedItems.Contains(ls[ii]))
                                                {
                                                    SelectedItems.Add(ls[ii]);
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
                                        SelectedItems.Add(itm);
                                        if (SelectedChanged != null) SelectedChanged.Invoke(this, new EventArgs());
                                        first = itm;
                                        #endregion
                                    }
                                }
                                #endregion
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
            Focus();

            if (Areas.ContainsKey("rtScroll") && Areas.ContainsKey("rtBox"))
            {
                var rtBox = Areas["rtBox"];
                var rtScroll = Areas["rtScroll"];

                if (CollisionTool.Check(Areas["rtBox"], e.Location))
                {
                    Loop((ls, i, rt, itm, rtv, rti, rtt, rtc) =>
                    {
                        if (itm != null && CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom)
                            {
                                if (CollisionTool.Check(rtc, e.Location)) NodeClick?.Invoke(this, new TreeViewNodeMouseEventArgs(e.X, e.Y, itm));
                            }
                            else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom)
                            {
                                if (CollisionTool.Check(rtc, e.Location)) NodeClick?.Invoke(this, new TreeViewNodeMouseEventArgs(e.X, e.Y, itm));
                            }
                            else
                            {
                                if (CollisionTool.Check(rtc, e.Location)) NodeClick?.Invoke(this, new TreeViewNodeMouseEventArgs(e.X, e.Y, itm));
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
                    Loop((ls, i, rt, itm, rtv, rti, rtt, rtc) =>
                    {
                        if (itm != null && CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom)
                            {
                                if (CollisionTool.Check(rtc, e.Location)) NodeDoubleClick?.Invoke(this, new TreeViewNodeMouseEventArgs(e.X, e.Y, itm));
                            }
                            else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom)
                            {
                                if (CollisionTool.Check(rtc, e.Location)) NodeDoubleClick?.Invoke(this, new TreeViewNodeMouseEventArgs(e.X, e.Y, itm));
                            }
                            else
                            {
                                if (CollisionTool.Check(rtc, e.Location)) NodeDoubleClick?.Invoke(this, new TreeViewNodeMouseEventArgs(e.X, e.Y, itm));
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
        #region GetTreeNode
        public TreeViewNode GetTreeNode(Point Location)
        {
            TreeViewNode ret = null;
            #region Items
            Loop((ls, i, rt, v, rtv, rti, rtt, rtc) =>
            {
                if (CollisionTool.Check(rtc, Location)) ret = v;
            });
            #endregion
            return ret;
        }
        #endregion
        #region MakeList
        List<TreeViewNode> MakeList()
        {
            List<TreeViewNode> ret = new List<TreeViewNode>();
            for (int i = 0; i < Nodes.Count; i++) MS(Nodes[i], ret);

            ListCount = ret.Count;
            return ret;
        }

        void MS(TreeViewNode nd, List<TreeViewNode> lst)
        {
            lst.Add(nd);
            if (nd.Expands) for (int i = 0; i < nd.Nodes.Count; i++) MS(nd.Nodes[i], lst);
        }
        #endregion
        #region GetListCount
        int GetListCount() => ListCount;
        #endregion
        #region Loop
        private void Loop(Action<List<TreeViewNode>, int, Rectangle, TreeViewNode, Rectangle, Rectangle, Rectangle, Rectangle> Func)
        {
            if (Areas.ContainsKey("rtBox"))
            {
                var sc = scroll.ScrollPosition;
                var spos = Convert.ToInt32(scroll.ScrollPositionWithOffset);

                var rtBox = Areas["rtBox"];
                var si = Convert.ToInt32(Math.Floor((double)(sc - scroll.TouchOffset) / (double)RowHeight));
                var cnt = Convert.ToInt32(Math.Ceiling((double)(rtBox.Height - Math.Min(0, scroll.TouchOffset)) / (double)RowHeight));
                var ei = si + cnt;
                var ls = MakeList();

                using (var g = CreateGraphics())
                {
                    var f = DpiRatio;
                    for (int i = Math.Max(0, si); i < ei + 1 && i < ls.Count; i++)
                    {
                        var itm = ls[i];
                        var rt = new Rectangle(rtBox.Left, spos + rtBox.Top + (RowHeight * i), rtBox.Width, RowHeight);
                        if (CollisionTool.Check(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 2, rt.Height - 2), rtBox))
                        {
                            var idnt = IndentWidth * itm.Depth;
                            var sz = g.MeasureTextIcon(itm.Icon, itm.Text, Font);
                            var w = Convert.ToInt32(sz.Width * f);
                            var h = Convert.ToInt32(sz.Height * f);

                            var rtv = new Rectangle(rt.Left + idnt, rt.Top, rt.Width - idnt, rt.Height);

                            if (rt.Top <= rtBox.Top && rtBox.Top <= rt.Bottom) rtv = new Rectangle(rt.Left + idnt, rtBox.Top, rt.Width - idnt, rt.Bottom - rtBox.Top);
                            else if (rt.Top <= rtBox.Bottom && rtBox.Bottom <= rt.Bottom) rtv = new Rectangle(rt.Left + idnt, rt.Top, rt.Width - idnt, rtBox.Bottom - rt.Top);

                            var rti = new Rectangle(rtv.Left, rt.Top, rt.Height, rt.Height);
                            var rtt = new Rectangle(rtv.Left + rti.Width, rt.Top, rtv.Width - rti.Width, rti.Height);
                            var rtc = DrawingTool.MakeRectangleAlign(rtt, new Size(w, h), DvContentAlignment.MiddleLeft);
                            rtc.Inflate(5, 5);

                            Func(ls, i, rt, itm, rtv, rti, rtt, rtc);
                        }
                    }
                }
            }
        }
        #endregion
        #endregion
    }

    #region class : TreeViewNode
    public class TreeViewNode : ListBoxItem
    {
        #region Properties
        public TreeViewNode Parents { get; internal set; }
        public TreeViewNodeCollection Nodes { get; private set; }
        public bool Expands { get; set; } = true;
        public int Depth { get => (Parents != null ? Parents.Depth + 1 : 0); }
        #endregion
        #region Event
        internal event EventHandler Changed;
        #endregion
        #region Constructor
        public TreeViewNode(string Text) : base(Text)
        {
            Nodes = new TreeViewNodeCollection(this);
            Nodes.Changed += (o, s) =>
            {
                foreach (var v in Nodes) v.Parents = this;
                Changed?.Invoke(this, null);
            };
        }
        public TreeViewNode(string Text, Bitmap Image) : base(Text, Image)
        {
            Nodes = new TreeViewNodeCollection(this);
            Nodes.Changed += (o, s) =>
            {
                foreach (var v in Nodes) v.Parents = this;
                Changed?.Invoke(this, null);
            };
        }
        public TreeViewNode(string Text, string IconString, float Size) : base(Text, IconString, Size)
        {
            Nodes = new TreeViewNodeCollection(this);
            Nodes.Changed += (o, s) =>
            {
                foreach (var v in Nodes) v.Parents = this;
                Changed?.Invoke(this, null);
            };
        }
        public TreeViewNode(string Text, string IconString, float size, int Gap) : base(Text, IconString, size, Gap)
        {
            Nodes = new TreeViewNodeCollection(this);
            Nodes.Changed += (o, s) =>
            {
                foreach (var v in Nodes) v.Parents = this;
                Changed?.Invoke(this, null);
            };
        }
        #endregion
    }
    #endregion
    #region class : TreeViewNodeCollection
    public class TreeViewNodeCollection : EventList<TreeViewNode>
    {
        public TreeViewNode Parent { get; private set; } = null;

        public TreeViewNodeCollection(TreeViewNode node)
        {
            this.Parent = node;
        }
    }
    #endregion
    #region class : TreeViewNodeMouseEventArgs
    public class TreeViewNodeMouseEventArgs : EventArgs
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public TreeViewNode Node { get; private set; }

        public TreeViewNodeMouseEventArgs(int X, int Y, TreeViewNode Node)
        {
            this.X = X;
            this.Y = Y;
            this.Node = Node;
        }
    }
    #endregion
}
