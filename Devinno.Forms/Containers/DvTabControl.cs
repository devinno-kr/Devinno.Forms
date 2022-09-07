using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Containers
{
    public class DvTabControl : TabControl
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
                    Theme.DrawBox(e.Graphics, rtNavi, TabBackColor, TabBackBorderColor, eNavi, Box.FlatBox(true, true), Corner);
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
                        case TabAlignment.Left: rtTab = new Rectangle(rtTab.X, rtTab.Y, rtTab.Width - 5, rtTab.Height); break;
                        case TabAlignment.Top: rtTab = new Rectangle(rtTab.X, rtTab.Y, rtTab.Width, rtTab.Height - 5); break;
                        case TabAlignment.Right: rtTab = new Rectangle(rtTab.X + 5, rtTab.Y, rtTab.Width - 5, rtTab.Height); break;
                        case TabAlignment.Bottom: rtTab = new Rectangle(rtTab.X, rtTab.Y + 5, rtTab.Width, rtTab.Height - 5); break;
                    }


                    Theme.DrawTextIcon(e.Graphics, ti, Font, cT, rtTab);
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

                Theme.DrawBox(e.Graphics, rtPage, TabColor, TabBorderColor, ePage, Box.FlatBox(true,true));

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
 

            switch (Alignment)
            {
                case TabAlignment.Left:
                    rtNavi = Util.FromRect(rtContent.Left, rtContent.Top, ItemSize.Height - 1, rtContent.Height);
                    rtPage = Util.FromRect(rtNavi.Right, rtContent.Top, rtContent.Width - ItemSize.Height, rtContent.Height);
                    break;
                case TabAlignment.Top:
                    rtNavi = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, ItemSize.Height - 1);
                    rtPage = Util.FromRect(rtContent.Left, rtNavi.Bottom, rtContent.Width, rtContent.Height - ItemSize.Height);
                    break;
                case TabAlignment.Right:
                    rtNavi = Util.FromRect(rtContent.Right - ItemSize.Height + 1, rtContent.Top, ItemSize.Height - 1, rtContent.Height);
                    rtPage = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width - ItemSize.Height, rtContent.Height);
                    break;
                case TabAlignment.Bottom:
                    rtNavi = Util.FromRect(rtContent.Left, rtContent.Bottom - ItemSize.Height + 1, rtContent.Width, ItemSize.Height - 1);
                    rtPage = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height - ItemSize.Height);
                    break;
            }
             

            act(rtContent, rtPage, rtNavi);
        }
        #endregion
        #endregion
    }

    internal class _TPI
    {
        public TabPage Page { get; set; }
        public Color BackColor { get; set; }
    }
}
