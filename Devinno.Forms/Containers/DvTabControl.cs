using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        #region TabBarColor
        private Color cTabBarColor = DvTheme.DefaultTheme.Color2;
        public Color TabBarColor
        {
            get { return cTabBarColor; }
            set { if (cTabBarColor != value) { cTabBarColor = value; Invalidate(); } }
        }
        #endregion
        #region PointColor
        private Color cPointColor = DvTheme.DefaultTheme.PointColor;
        public Color PointColor
        {
            get { return cPointColor; }
            set { if (cPointColor != value) { cPointColor = value; Invalidate(); } }
        }
        #endregion
        #region TextColor
        private Color cTextColor = DvTheme.DefaultTheme.ForeColor;
        public Color TextColor
        {
            get { return cTextColor; }
            set { if (cTextColor != value) { cTextColor = value; Invalidate(); } }
        }
        #endregion

        #region UseThemeColor
        private bool bUseThemeColor = true;
        [Category("- 색상")]
        public bool UseThemeColor
        {
            get => bUseThemeColor;
            set
            {
                if (bUseThemeColor != value)
                {
                    bUseThemeColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region DpiRatio
#if NET5_0
        public double DpiRatio => (double)this.LogicalToDeviceUnits(1000) / 1000.0;
#else
        public double DpiRatio => 1D;
#endif
        #endregion
        #region Areas
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dictionary<string, Rectangle> Areas { get; } = new Dictionary<string, Rectangle>();
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
            LoadAreas(e.Graphics);

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

                foreach (var v in TabPages.Cast<TabPage>().Where(x => !tps.Select(x2 => x2.Page).Contains(x)))
                {
                    tps.Add(new _TPI() { Page = v, BackColor = v.BackColor });
                    v.Paint += (o, s) =>
                    {
                        var wnd2 = this.FindForm() as DvForm;
                        if (wnd2 != null && wnd2.Block)
                            s.Graphics.Clear(ColorTool.MixColorAlpha(Parent.BackColor, Color.Black, DvForm.BlockAlpha));
                    };
                }
            }
        }
        #endregion

        #region LoadAreas
        protected virtual void LoadAreas(Graphics g)
        {
        }
        #endregion
        #region OnThemeDraw
        protected virtual void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var TabBarColor = UseThemeColor ? Theme.Color2 : this.TabBarColor;
            var PointColor = UseThemeColor ? Theme.PointColor : this.PointColor;
            var TextColor = UseThemeColor ? Theme.ForeColor : this.TextColor;
            #endregion
            #region Initialize
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            var p = new Pen(Color.White);
            var br = new SolidBrush(Color.White);
            #endregion
            #region Draw
            var f = DpiRatio;
            var cBorder = SelectedTab != null ? SelectedTab.BackColor.BrightnessTransmit(Theme.BorderBright) : Theme.BackColor;
            var cSelectedTab = TabBarColor.BrightnessTransmit(0.3);
            var cNoSelectText = TabBarColor.BrightnessTransmit(0.6);
            var nPTSZ = Convert.ToInt32(4 * f);
           
            #region Left
            if (Alignment == TabAlignment.Left)
            {
                var rtTabBar = new Rectangle(0, 0, ItemSize.Height, Height);
                if(SelectedTab != null)     e.Graphics.Clear(SelectedTab.BackColor);
                br.Color = TabBarColor;     e.Graphics.FillRectangle(br, rtTabBar);

                for (int i = 0; i < TabPages.Count; i++)
                {
                    var tab = TabPages[i];
                    var tabicon = TabIcons.ContainsKey(tab.Name) ? TabIcons[tab.Name] : null;
                    var rtOrg = GetTabRect(i);
                    var rtbg = new Rectangle(rtTabBar.X, rtOrg.Y, rtTabBar.Width, rtOrg.Height); rtbg.Inflate(0, -10);
                    if (i == SelectedIndex)
                    {
                        var rtpoint = new Rectangle(rtbg.Right - nPTSZ, rtbg.Top, nPTSZ, rtbg.Height);
                        br.Color = cSelectedTab;    e.Graphics.FillRectangle(br, rtbg);
                        br.Color = PointColor;      e.Graphics.FillRectangle(br, rtpoint);

                        p.Color = Color.FromArgb(20, Color.White);
                        e.Graphics.DrawLine(p, rtbg.Left, rtbg.Top, rtbg.Right - 1, rtbg.Top);
                        e.Graphics.DrawLine(p, rtbg.Left, rtbg.Bottom - 1, rtbg.Right - 1, rtbg.Bottom-1);
                    }
                    Theme.DrawTextShadow(e.Graphics, tabicon, tab.Text, Font, i == SelectedIndex ? TextColor : cNoSelectText, i == SelectedIndex ? cSelectedTab : TabBarColor, rtbg);
                }
            }
            #endregion
            #region Right
            else if (Alignment == TabAlignment.Right)
            {
                var rtTabBar = new Rectangle(Width - ItemSize.Height - 4, 0, ItemSize.Height + 4, Height);
                if (SelectedTab != null) e.Graphics.Clear(SelectedTab.BackColor);
                br.Color = TabBarColor; e.Graphics.FillRectangle(br, rtTabBar);
                
                for (int i = 0; i < TabPages.Count; i++)
                {
                    var tab = TabPages[i];
                    var tabicon = TabIcons.ContainsKey(tab.Name) ? TabIcons[tab.Name] : null;
                    var rtOrg = GetTabRect(i);
                    var rtbg = new Rectangle(rtTabBar.X, rtOrg.Y, rtTabBar.Width, rtOrg.Height); rtbg.Inflate(0, -10);
                    if (i == SelectedIndex)
                    {
                        var rtpoint = new Rectangle(rtbg.X, rtbg.Y, nPTSZ, rtbg.Height); 
                        br.Color = cSelectedTab; e.Graphics.FillRectangle(br, rtbg);
                        br.Color = PointColor; e.Graphics.FillRectangle(br, rtpoint);

                        p.Color = Color.FromArgb(20, Color.White);
                        e.Graphics.DrawLine(p, rtbg.Left, rtbg.Top, rtbg.Right - 1, rtbg.Top);
                        e.Graphics.DrawLine(p, rtbg.Left, rtbg.Bottom - 1, rtbg.Right - 1, rtbg.Bottom - 1);
                    }
                    Theme.DrawTextShadow(e.Graphics, tabicon, tab.Text, Font, i == SelectedIndex ? TextColor : cNoSelectText, i == SelectedIndex ? cSelectedTab : TabBarColor, rtbg);
                }
            }
            #endregion
            #region Top
            else if (Alignment == TabAlignment.Top)
            {
                var rtTabBar = new Rectangle(0, 0, Width, ItemSize.Height + 4);
                if (SelectedTab != null) e.Graphics.Clear(SelectedTab.BackColor);
                br.Color = TabBarColor; e.Graphics.FillRectangle(br, rtTabBar);

                for (int i = 0; i < TabPages.Count; i++)
                {
                    var tab = TabPages[i];
                    var tabicon = TabIcons.ContainsKey(tab.Name) ? TabIcons[tab.Name] : null;
                    var rtOrg = GetTabRect(i);
                    var rtbg = new Rectangle(rtOrg.X, rtTabBar.Y, rtOrg.Width, rtTabBar.Height); rtbg.Inflate(-10, 0);
                    if (i == SelectedIndex)
                    {
                        var rtpoint = new Rectangle(rtbg.X, rtbg.Bottom-nPTSZ, rtbg.Width, nPTSZ);
                        br.Color = cSelectedTab; e.Graphics.FillRectangle(br, rtbg);
                        br.Color = PointColor; e.Graphics.FillRectangle(br, rtpoint);

                        p.Color = Color.FromArgb(20, Color.White);
                        e.Graphics.DrawLine(p, rtbg.Left, rtbg.Top, rtbg.Left, rtbg.Bottom - 1);
                        e.Graphics.DrawLine(p, rtbg.Right - 1, rtbg.Top, rtbg.Right - 1, rtbg.Bottom - 1);
                    }
                    Theme.DrawTextShadow(e.Graphics, tabicon, tab.Text, Font, i == SelectedIndex ? TextColor : cNoSelectText, i == SelectedIndex ? cSelectedTab : TabBarColor, rtbg);
                }
            }
            #endregion
            #region Bottom
            else if (Alignment == TabAlignment.Bottom)
            {
                var rtTabBar = new Rectangle(0, Height - ItemSize.Height - 4, Width, ItemSize.Height + 4);
                if (SelectedTab != null) e.Graphics.Clear(SelectedTab.BackColor);
                br.Color = TabBarColor; e.Graphics.FillRectangle(br, rtTabBar);

                for (int i = 0; i < TabPages.Count; i++)
                {
                    var tab = TabPages[i];
                    var tabicon = TabIcons.ContainsKey(tab.Name) ? TabIcons[tab.Name] : null;
                    var rtOrg = GetTabRect(i);
                    var rtbg = new Rectangle(rtOrg.X, rtTabBar.Y, rtOrg.Width, rtTabBar.Height); rtbg.Inflate(-10, 0);
                    if (i == SelectedIndex)
                    {
                        var rtpoint = new Rectangle(rtbg.X, rtbg.Y, rtbg.Width, nPTSZ);
                        br.Color = cSelectedTab; e.Graphics.FillRectangle(br, rtbg);
                        br.Color = PointColor; e.Graphics.FillRectangle(br, rtpoint);

                        p.Color = Color.FromArgb(20, Color.White);
                        e.Graphics.DrawLine(p, rtbg.Left, rtbg.Top, rtbg.Left, rtbg.Bottom - 1);
                        e.Graphics.DrawLine(p, rtbg.Right - 1, rtbg.Top, rtbg.Right - 1, rtbg.Bottom - 1);
                    }
                    Theme.DrawTextShadow(e.Graphics, tabicon, tab.Text, Font, i == SelectedIndex ? TextColor : cNoSelectText, i == SelectedIndex ? cSelectedTab : TabBarColor, rtbg);
                }
            }
            #endregion
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
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
                var pi = o.GetType().GetProperty("Theme");
                var thm = pi.GetValue(o);
                ret = thm as DvTheme;
            }
            catch (Exception) { }
            return ret;
        }
        #endregion
        #region SetArea
        protected void SetArea(string key, Rectangle rt)
        {
            if (!Areas.ContainsKey(key)) Areas.Add(key, rt);
            else Areas[key] = rt;
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
