using Devinno.Collections;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Devinno.Forms.Containers
{
    public class DvTablessControl : TabControl
    {
        #region Properties
        #region SelectedTab
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new TabPage SelectedTab
        {
            get => base.SelectedTab;
            set => base.SelectedTab = value;
        }
        #endregion
        #endregion

        #region Member Variable
        List<string> cons = new List<string>();
        List<TabPage> tps = new List<TabPage>();
        #endregion

        #region Constructor
        public DvTablessControl()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            this.Appearance = TabAppearance.FlatButtons;
            this.ItemSize = new Size(0, 1);
            this.SizeMode = TabSizeMode.Fixed;

            if (!this.DesignMode) this.Multiline = true;
        }
        #endregion

        #region Override
        #region WndProc
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !this.DesignMode)
                m.Result = new IntPtr(1);
            else
                base.WndProc(ref m);
        }
        #endregion
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
        protected virtual void OnThemeDraw(PaintEventArgs e, DvTheme Theme) { }
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

                foreach (var v in TabPages.Cast<TabPage>().Where(x => !tps.Contains(x)))
                {
                    tps.Add(v);
                    v.Paint += (o, s) =>
                    {
                        var wnd2 = this.FindForm() as DvForm;
                        if (wnd2 != null && wnd2.Block)
                            s.Graphics.Clear(ColorTool.MixColorAlpha(v.BackColor, Color.Black, DvForm.BlockAlpha));
                    };
                }
            }
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
        #endregion
    }
}
