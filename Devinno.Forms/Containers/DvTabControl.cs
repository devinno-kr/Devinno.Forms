using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Containers
{
    public class DvTabControl : TabControl, IMessageFilter
    {
        #region Properties
        #region TabIcons
        public Dictionary<string, DvIcon> TabIcons { get; } = new Dictionary<string, DvIcon>();
        #endregion
        #region TabBackColor
        private Color? cTabBackColor = null;
        public Color? TabBackColor
        {
            get { return cTabBackColor; }
            set { if (cTabBackColor != value) { cTabBackColor = value; Invalidate(); } }
        }
        #endregion
        #region TaColor
        private Color? cTabColor = null;
        public Color? TabColor
        {
            get { return cTabColor; }
            set { if (cTabColor != value) { cTabColor = value; Invalidate(); } }
        }
        #endregion
        #region PointColor
        private Color? cPointColor = null;
        public Color? PointColor
        {
            get { return cPointColor; }
            set { if (cPointColor != value) { cPointColor = value; Invalidate(); } }
        }
        #endregion
        #region DrawBorder
        private bool bDrawBorder = true;
        public bool DrawBoarder
        {
            get => bDrawBorder;
            set
            {
                if(bDrawBorder != value)
                {
                    bDrawBorder = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Buttons
        public List<DvTabButton> Buttons { get; } = new List<DvTabButton>();
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
        #endregion

        #region Event
        #region ButtonClicked
        public event EventHandler<TabButtonClickedEventArgs> ButtonClicked;
        #endregion
        #endregion

        #region Interop
        [DllImport("user32")]
        private static extern bool ClientToScreen(IntPtr windowHandle, ref Point screenPoint);
        #endregion

        #region Member Variable
        List<_TPI> tps = new List<_TPI>();
        #endregion

        #region Constructor
        public DvTabControl()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(120, 30);

            Application.AddMessageFilter(this);
        }
        #endregion

        #region Override
        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
           
            e.Graphics.Clear(Parent.BackColor);

            var Theme = GetTheme();
            if (Theme != null) OnThemeDraw(e, Theme);

            base.OnPaint(e);

            if (Theme != null) OnThemeEnableDraw(e, Theme);
            if (Theme != null) OnThemeBlockDraw(e, Theme);
        }
        #endregion
        #region OnEnabledChanged
        protected override void OnEnabledChanged(EventArgs e)
        {
            Invalidate();
            base.OnEnabledChanged(e);
        }
        #endregion
        #region OnThemeDraw
        protected virtual void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Initialize
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            var p = new Pen(Color.White);
            var br = new SolidBrush(Color.White);
            #endregion
            #region Set
            var PointColor = this.PointColor ?? Theme.PointColor;
            var TabColor = this.TabColor ?? Theme.TabPageColor;
            var TabBackColor = this.TabBackColor ?? Theme.TabBackColor;
            var TabBorderColor = Theme.GetBorderColor(TabColor, BackColor);
            var TabBackBorderColor = Theme.GetBorderColor(TabBackColor, BackColor);
            var PointBorderColor = Theme.GetBorderColor(PointColor, BackColor);
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;
            #endregion

            Areas((rtContent, rtPage, rtNavi) =>
            {
                #region Round
                RoundType eNavi = RoundType.Rect;
                RoundType ePage = RoundType.Rect;

                switch (Round)
                {
                    #region All
                    case RoundType.All:
                        switch (Alignment)
                        {
                            case TabAlignment.Left: eNavi = RoundType.L; ePage = RoundType.R; break;
                            case TabAlignment.Top: eNavi = RoundType.T; ePage = RoundType.B; break;
                            case TabAlignment.Right: eNavi = RoundType.R; ePage = RoundType.L; break;
                            case TabAlignment.Bottom: eNavi = RoundType.B; ePage = RoundType.T; break;
                        }
                        break;
                    #endregion

                    #region L
                    case RoundType.L:
                        switch (Alignment)
                        {
                            case TabAlignment.Left: eNavi = RoundType.L; ePage = RoundType.Rect; break;
                            case TabAlignment.Top: eNavi = RoundType.LT; ePage = RoundType.LB; break;
                            case TabAlignment.Right: eNavi = RoundType.Rect; ePage = RoundType.L; break;
                            case TabAlignment.Bottom: eNavi = RoundType.LB; ePage = RoundType.LT; break;
                        }
                        break;
                    #endregion
                    #region R
                    case RoundType.R:
                        switch (Alignment)
                        {
                            case TabAlignment.Left: eNavi = RoundType.Rect; ePage = RoundType.R; break;
                            case TabAlignment.Top: eNavi = RoundType.RT; ePage = RoundType.RB; break;
                            case TabAlignment.Right: eNavi = RoundType.R; ePage = RoundType.Rect; break;
                            case TabAlignment.Bottom: eNavi = RoundType.RB; ePage = RoundType.RT; break;
                        }
                        break;
                    #endregion
                    #region T
                    case RoundType.T:
                        switch (Alignment)
                        {
                            case TabAlignment.Left: eNavi = RoundType.LT; ePage = RoundType.RT; break;
                            case TabAlignment.Top: eNavi = RoundType.T; ePage = RoundType.Rect; break;
                            case TabAlignment.Right: eNavi = RoundType.RT; ePage = RoundType.LT; break;
                            case TabAlignment.Bottom: eNavi = RoundType.Rect; ePage = RoundType.T; break;
                        }
                        break;
                    #endregion
                    #region B
                    case RoundType.B:
                        switch (Alignment)
                        {
                            case TabAlignment.Left: eNavi = RoundType.LB; ePage = RoundType.RB; break;
                            case TabAlignment.Top: eNavi = RoundType.Rect; ePage = RoundType.B; break;
                            case TabAlignment.Right: eNavi = RoundType.RB; ePage = RoundType.LB; break;
                            case TabAlignment.Bottom: eNavi = RoundType.B; ePage = RoundType.Rect; break;
                        }
                        break;
                    #endregion

                    #region LT
                    case RoundType.LT:
                        switch (Alignment)
                        {
                            case TabAlignment.Left: eNavi = RoundType.LT; ePage = RoundType.Rect; break;
                            case TabAlignment.Top: eNavi = RoundType.LT; ePage = RoundType.Rect; break;
                            case TabAlignment.Right: eNavi = RoundType.Rect; ePage = RoundType.LT; break;
                            case TabAlignment.Bottom: eNavi = RoundType.Rect; ePage = RoundType.LT; break;
                        }
                        break;
                    #endregion
                    #region RT
                    case RoundType.RT:
                        switch (Alignment)
                        {
                            case TabAlignment.Left: eNavi = RoundType.Rect; ePage = RoundType.RT; break;
                            case TabAlignment.Top: eNavi = RoundType.RT; ePage = RoundType.Rect; break;
                            case TabAlignment.Right: eNavi = RoundType.RT; ePage = RoundType.Rect; break;
                            case TabAlignment.Bottom: eNavi = RoundType.Rect; ePage = RoundType.RT; break;
                        }
                        break;
                    #endregion
                    #region LB
                    case RoundType.LB:
                        switch (Alignment)
                        {
                            case TabAlignment.Left: eNavi = RoundType.LB; ePage = RoundType.Rect; break;
                            case TabAlignment.Top: eNavi = RoundType.Rect; ePage = RoundType.LB; break;
                            case TabAlignment.Right: eNavi = RoundType.Rect; ePage = RoundType.LB; break;
                            case TabAlignment.Bottom: eNavi = RoundType.LB; ePage = RoundType.Rect; break;
                        }
                        break;
                    #endregion
                    #region RB
                    case RoundType.RB:
                        switch (Alignment)
                        {
                            case TabAlignment.Left: eNavi = RoundType.Rect; ePage = RoundType.RB; break;
                            case TabAlignment.Top: eNavi = RoundType.Rect; ePage = RoundType.RB; break;
                            case TabAlignment.Right: eNavi = RoundType.RB; ePage = RoundType.Rect; break;
                            case TabAlignment.Bottom: eNavi = RoundType.RB; ePage = RoundType.Rect; break;
                        }
                        break;
                        #endregion
                }
                #endregion
                #region Background
                {
                    Theme.DrawBox(e.Graphics, rtNavi, TabBackColor, (!DrawBoarder ? TabBackColor : TabBackBorderColor), eNavi, Box.FlatBox(true, true), Corner);
                }
                #endregion

                for (int i = 0; i < TabPages.Count; i++)
                {
                    #region Var
                    var tab = TabPages[i];
                    var tabicon = TabIcons.ContainsKey(tab.Name) ? TabIcons[tab.Name] : null;
                    var ti = new TextIcon { Text = tab.Text };
                    if(tabicon != null)
                    {
                        ti.IconAlignment = tabicon.Alignment;
                        ti.IconGap = tabicon.Gap;
                        ti.IconSize = tabicon.IconSize;
                        ti.IconString = tabicon.IconString;
                        ti.IconImage = tabicon.IconImage;
                    }
                    #endregion

                    #region Tab
                    var cT = i == SelectedIndex ? ForeColor : Color.FromArgb(60, ForeColor);
                    var rtTab = GetTabRect(i);
                    switch (Alignment)
                    {
                        case TabAlignment.Left: rtTab = new Rectangle(rtTab.X, rtTab.Y, Convert.ToInt32(rtNavi.Width), rtTab.Height); break;
                        case TabAlignment.Top: rtTab = new Rectangle(rtTab.X, rtTab.Y, rtTab.Width, Convert.ToInt32(rtNavi.Height)); break;
                        case TabAlignment.Right: rtTab = new Rectangle(rtTab.X + 5, rtTab.Y, Convert.ToInt32(rtNavi.Width), rtTab.Height); break;
                        case TabAlignment.Bottom: rtTab = new Rectangle(rtTab.X, rtTab.Y + 5, rtTab.Width, Convert.ToInt32(rtNavi.Height)); break;
                    }

                    //Theme.DrawTextIcon(e.Graphics, ti, Font, cT, rtTab);
                    if (ti != null)
                    {
                        br.Color = cT;

                        if (FA.Contains(ti.IconString) || ti.IconImage != null)
                        {
                            TextIconBounds(e.Graphics, rtTab, DvContentAlignment.MiddleCenter, ti.Text, Font, ti.IconGap, new SizeF(ti.IconSize, ti.IconSize), ti.IconAlignment,
                            (rtIco, rtText) =>
                            {
                                e.Graphics.DrawIcon(ti.Icon, br, rtIco);
                                using (var strfrm = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter })
                                {
                                    var sz = e.Graphics.MeasureString(ti.Text, Font);
                                    var vrt = Util.MakeRectangleAlign(rtText, new SizeF(rtText.Width, sz.Height), DvContentAlignment.MiddleCenter);
                                    e.Graphics.DrawString(ti.Text, Font, br, vrt, strfrm);
                                }
                            });
                        }
                        else
                        {
                            using (var strfrm = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter })
                            {
                                var sz = e.Graphics.MeasureString(ti.Text, Font);
                                var vrt = Util.MakeRectangleAlign(rtTab, new SizeF(rtTab.Width, sz.Height), DvContentAlignment.MiddleCenter);
                                e.Graphics.DrawString(ti.Text, Font, br, vrt, strfrm);
                            }
                        }
                    }
                    #endregion

                    #region Point
                    if (i == SelectedIndex)
                    {
                        var rtCur = new RectangleF(0, 0, 0, 0);
                        switch (Alignment)
                        {
                            case TabAlignment.Left: rtCur = new RectangleF(rtNavi.Right - 5, rtTab.Y, 5, rtTab.Height); break;
                            case TabAlignment.Top: rtCur = new RectangleF(rtTab.X, rtNavi.Bottom - 5, rtTab.Width, 5); break;
                            case TabAlignment.Right: rtCur = new RectangleF(rtNavi.Left, rtTab.Y, 5, rtTab.Height); break;
                            case TabAlignment.Bottom: rtCur = new RectangleF(rtTab.X, rtNavi.Top, rtTab.Width, 5); break;
                        }

                        Theme.DrawBox(e.Graphics, rtCur, PointColor, PointBorderColor, RoundType.All, Box.ButtonUp_Flat(1));
                    }
                    #endregion
                }

                AreasButtons(rtNavi, (btn, rt) =>
                {
                    if (btn.Icon != null)
                    {
                        br.Color = !btn.bDown ? ForeColor : Color.FromArgb(60, ForeColor);
                        e.Graphics.DrawIcon(btn.Icon, br, rt);
                    }
                });

                Theme.DrawBox(e.Graphics, rtPage, TabColor, (!DrawBoarder ? TabColor : TabBorderColor), ePage, Box.FlatBox(true,true));

            });

            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
        }
        #endregion
        #region OnThemeEnableDraw
        protected virtual void OnThemeEnableDraw(PaintEventArgs e, DvTheme Theme)
        {
            var bgColor = this.BackColor;
            if (this.BackColor == Color.Transparent)
            {
                if (Parent != null) bgColor = Parent.BackColor;
            }
            if (!Enabled)
            {
                using (var br = new SolidBrush(Color.FromArgb(Theme.DisableAlpha, bgColor)))
                {
                    e.Graphics.FillRectangle(br, new Rectangle(-1, -1, this.Width + 2, this.Height + 2));
                }
            }
        }
        #endregion
        #region OnThemeBlockDraw
        protected virtual void OnThemeBlockDraw(PaintEventArgs e, DvTheme Theme)
        {
            var wnd = this.FindForm() as DvForm;
            if (wnd != null)
            {
                if (wnd.Block)
                {
                    using (var br = new SolidBrush(Color.FromArgb(DvForm.BlockAlpha, Color.Black)))
                    {
                        e.Graphics.FillRectangle(br, new Rectangle(-1, -1, this.Width + 2, this.Height + 2));
                    }
                }
            }
        }
        #endregion
        #region OnControlAdded
        protected override void OnControlAdded(ControlEventArgs e)
        {
            var c = e.Control as TabPage;
            if (c != null)
            {
                c.Paint += (o, s) =>
                {
                    var wnd = this.FindForm() as DvForm;
                    if (wnd != null && wnd.Block)
                        s.Graphics.Clear(ColorTool.MixColorAlpha(c.BackColor, Color.Black, DvForm.BlockAlpha));

                };
                Util.SetDoubleBuffered(c);

                var Theme = GetTheme();
                if (Theme == null) Theme = DvTheme.DefaultTheme;
                c.BackColor = TabColor ?? Theme.TabPageColor;
            }

            base.OnControlAdded(e);
        }
        #endregion

        #region PreFilterMessage
        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0201) //WM_LBUTTONDOWN
            {
                var v = m.LParam.ToInt32();
                int X = (v & 0x0000FFFF), Y = (int)((v & 0xFFFF0000) >> 16);
                var p = new Point(X, Y);

                var sp = ClientToScreen(m.HWnd, ref p);

                Areas((rtContent, rtPage, rtNavi) =>
                {
                    AreasButtons(rtNavi, (btn, rt) =>
                    {
                        var srt = this.RectangleToScreen(Util.INT(rt));
                        if (CollisionTool.Check(srt, new Point(Convert.ToInt32(p.X), Convert.ToInt32(p.Y))))
                        {
                            btn.bDown = true;
                        }
                    });
                });
                Invalidate();
            }
            else if (m.Msg == 0x202) //WM_LBUTTONUP
            {
                var v = m.LParam.ToInt32();
                int X = (v & 0x0000FFFF), Y = (int)((v & 0xFFFF0000) >> 16);
                var p = new Point(X, Y);

                var sp = ClientToScreen(m.HWnd, ref p);

                Areas((rtContent, rtPage, rtNavi) =>
                {
                    AreasButtons(rtNavi, (btn, rt) =>
                    {
                        if (btn.bDown)
                        {
                            btn.bDown = false;

                            var srt = this.RectangleToScreen(Util.INT(rt));
                            if (CollisionTool.Check(srt, new Point(Convert.ToInt32(p.X), Convert.ToInt32(p.Y))))
                            {
                                ButtonClicked?.Invoke(this, new TabButtonClickedEventArgs(btn));
                            }
                        }
                    });
                });
                Invalidate();
            }

            return false;
        }
        #endregion
        #endregion

        #region Method
        #region GetTheme
        public DvTheme GetTheme()
        {
            DvTheme ret = null;
            try
            {
                var o = this.FindForm();
                if (o != null)
                {
                    var pi = o.GetType().GetProperty("Theme");
                    var thm = pi.GetValue(o);
                    ret = thm as DvTheme;
                }
            }
            catch (Exception) { }
            return ret;
        }
        #endregion

        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF > act)
        {
            var rtContent = Util.FromRect(0, 0, this.Width - 1, this.Height - 1);
            var rtPage = rtContent;
            var rtNavi = new RectangleF();

            var ih = TabPages.Count > 0 ? (SizeMode == TabSizeMode.Fixed ? ItemSize.Height : GetTabRect(0).Height) : 10;

            switch (Alignment)
            {
                case TabAlignment.Left:
                    rtNavi = Util.FromRect(rtContent.Left, rtContent.Top, ih - 1, rtContent.Height);
                    rtPage = Util.FromRect(rtNavi.Right, rtContent.Top, rtContent.Width - ih, rtContent.Height);
                    break;
                case TabAlignment.Top:
                    rtNavi = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, ih - 1);
                    rtPage = Util.FromRect(rtContent.Left, rtNavi.Bottom, rtContent.Width, rtContent.Height - ih);
                    break;
                case TabAlignment.Right:
                    rtNavi = Util.FromRect(rtContent.Right - ih + 1, rtContent.Top, ih - 1, rtContent.Height);
                    rtPage = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width - ih, rtContent.Height);
                    break;
                case TabAlignment.Bottom:
                    rtNavi = Util.FromRect(rtContent.Left, rtContent.Bottom - ih + 1, rtContent.Width, ih - 1);
                    rtPage = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height - ih);
                    break;
            }

            act(rtContent, rtPage, rtNavi);
        }
        #endregion
        #region AreasButtons
        void AreasButtons(RectangleF rtNavi, Action<DvTabButton, RectangleF> act)
        {
            if (Buttons.Count > 0)
            {
                for (int i = 0; i < Buttons.Count; i++)
                {
                    var btn = Buttons[i];
                    switch (Alignment)
                    {
                        case TabAlignment.Top:
                            {
                                var wh = rtNavi.Height;
                                var x = rtNavi.Right - (wh * Buttons.Count) + (wh * i);
                                var y = rtNavi.Y;
                                var rt = new RectangleF(x, y, wh, wh);
                                act(btn, rt);
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region TextIconBounds
        void TextIconBounds(Graphics g, RectangleF bounds, DvContentAlignment align, string text, Font font, float iconGap, SizeF iconSize, DvTextIconAlignment iconAlign, Action<RectangleF, RectangleF> act)
        {
            var gap = string.IsNullOrWhiteSpace(text) ? 0F : iconGap;
            var szTX = g.MeasureString(text, font);
            var szFA = iconSize;
            var szv = g.MeasureTextIcon(iconAlign, szFA, gap, text, font);
            if (szv.Width > bounds.Width - 10)
            {
                var gp = szv.Width - (bounds.Width - 10);
                szTX.Width -= gp;
                szv.Width -= gp;
            }
            var rt = Util.MakeRectangleAlign(bounds, szv, align);

            if (iconAlign == DvTextIconAlignment.LeftRight)
            {
                var rtFA = new RectangleF(rt.X, Util.CenterY(rt, szFA), szFA.Width, szFA.Height);
                var rtTX = new RectangleF(rt.Right - szTX.Width, Util.CenterY(rt, szTX), szTX.Width, szTX.Height);

                act(rtFA, rtTX);
            }
            else
            {
                var rtFA = new RectangleF(Util.CenterX(rt, szFA), rt.Y, szFA.Width, szFA.Height);
                var rtTX = new RectangleF(Util.CenterX(rt, szTX), rt.Bottom - szTX.Height, szTX.Width, szTX.Height);

                act(rtFA, rtTX);
            }
        }
        #endregion
        #endregion
    }

    #region _TPI
    internal class _TPI
    {
        public TabPage Page { get; set; }
        public Color BackColor { get; set; }
    }
    #endregion
    #region DvTabButton
    public class DvTabButton
    {
        public string Name { get; set; }
        public DvIcon Icon { get; set; }
        internal bool bDown { get; set; }
    }
    #endregion
    #region TabButtonClickedEventArgs 
    public class TabButtonClickedEventArgs : EventArgs
    {
        public DvTabButton Button { get; private set; }
        public TabButtonClickedEventArgs(DvTabButton btn) => this.Button = btn;
    }
    #endregion
}
