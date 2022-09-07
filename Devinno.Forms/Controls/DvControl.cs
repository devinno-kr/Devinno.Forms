using Devinno.Forms.Dialogs;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvControl : Control
    {
        #region Event
        public event EventHandler<ThemeDrawEventArgs> ThemeDraw;
        #endregion

        #region Properties
        #region ShadowGap
        private int nShadowGap = 1;
        public int ShadowGap
        {
            get => nShadowGap;
            set
            {
                if (nShadowGap != value)
                {
                    nShadowGap = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvControl()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.UpdateStyles();

            DoubleBuffered = true;
            ResizeRedraw = true;

            this.TabStop = false;
        }
        #endregion

        #region Override
        #region OnEnabledChanged
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }
        #endregion
        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            var Theme = GetTheme();
            if (Theme != null) OnThemeDraw(e, Theme);

            base.OnPaint(e);

            if (Theme != null) OnThemeEnableDraw(e, Theme);
            if (Theme != null) OnThemeBlockDraw(e, Theme);
        }
        #endregion
        #region OnThemeDraw
        protected virtual void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            ThemeDraw?.Invoke(this, new ThemeDrawEventArgs(e.Graphics, e.ClipRectangle, Theme));
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
        #region OnGotFocus
        protected override void OnGotFocus(EventArgs e)
        {
            Invalidate();
            base.OnGotFocus(e);
        }
        #endregion
        #region OnLostFocus
        protected override void OnLostFocus(EventArgs e)
        {
            Invalidate();
            base.OnLostFocus(e);
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
                    if (pi != null)
                    {
                        var thm = pi.GetValue(o);
                        ret = thm as DvTheme;
                    }
                }
            }
            catch (Exception) { }
            return ret;
        }
        #endregion
        #region GetParentBackColor
        public Color GetParentBackColor(Control c)
        {
            if (c == null) return Color.Transparent;
            if (c.BackColor != Color.Transparent) return c.BackColor;
            else return GetParentBackColor(c.Parent);
        }
        #endregion
        #region GetContentBounds
        public RectangleF GetContentBounds() => new RectangleF(0, 0, this.Width - 1 - ShadowGap, this.Height - 1 - ShadowGap);
        #endregion
        #endregion
    }
}
