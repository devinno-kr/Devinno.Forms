﻿using Devinno.Forms.Dialogs;
using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvControl : Control
    {
        #region Properties
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

        #endregion

        #region Constructor
        public DvControl()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            DoubleBuffered = true;

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
            LoadAreas(e.Graphics);

            var Theme = GetTheme();
            if (Theme != null) OnThemeDraw(e, Theme);

            base.OnPaint(e);

            if (Theme != null) OnThemeEnableDraw(e, Theme);
            if (Theme != null) OnThemeBlockDraw(e, Theme);
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
            }
        }
        #endregion
       
        #region LoadAreas
        protected virtual void LoadAreas(Graphics g)
        {
            SetArea("rtContent", GetContentBounds());
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
        public Rectangle GetContentBounds()
        {
            var o = this.FindForm();
            var v = o as DvForm;
            if (v != null && v.Theme != null) return new Rectangle(0, 0, Width - 1 - v.Theme.ShadowGap, Height - 1 - v.Theme.ShadowGap);
            else return new Rectangle(0, 0, Width - 2, Height - 2);
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
}