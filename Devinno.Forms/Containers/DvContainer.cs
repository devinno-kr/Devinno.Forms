﻿using Devinno.Forms.Dialogs;
using Devinno.Forms.Themes;
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
    public class DvContainer : ContainerControl
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
        public double DpiRatio => (double)this.LogicalToDeviceUnits(1000) / 1000.0;
        #endregion
        #region Areas
        public Dictionary<string, Rectangle> Areas { get; } = new Dictionary<string, Rectangle>();
        #endregion
        #endregion

        #region Constructor
        public DvContainer()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

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
            if (Theme != null) BlockDraw(e, Theme);
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
        #region LoadAreas
        protected virtual void LoadAreas(Graphics g)
        {
            SetArea("rtContent", GetContentBounds());
        }
        #endregion
        #endregion

        #region Method
        #region GetContentBounds
        public Rectangle GetContentBounds()
        {
            var o = this.FindForm();
            var v = o as DvForm;
            if (v != null && v.Theme != null) return new Rectangle(0, 0, Width - 1 - v.Theme.ShadowGap, Height - 1 - v.Theme.ShadowGap);
            else return new Rectangle(0, 0, Width - 1, Height - 1);
        }
        #endregion
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
        #region BlockDraw
        private void BlockDraw(PaintEventArgs e, DvTheme Theme)
        {
            var Wnd = this.FindForm() as DvForm;
            var bgColor = this.BackColor;
            if (this.BackColor == Color.Transparent)
            {
                if (Parent != null) bgColor = Parent.BackColor;
            }
            if (Wnd != null && Wnd.Block)
            {
                using (var br = new SolidBrush(Color.FromArgb(Theme.DisableAlpha, bgColor)))
                {
                    e.Graphics.FillRectangle(br, new Rectangle(-1, -1, this.Width + 2, this.Height + 2));
                }
            }
        }
        #endregion
        #endregion
    }
}
