﻿using Devinno.Forms.Dialogs;
using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Menus
{
    public class DvMenuStrip : MenuStrip
    {
        #region Member Variable
        private string CurrentThemeName = null;
        #endregion

        #region Constructor
        public DvMenuStrip()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            this.ForeColor = Color.FromArgb(180, 180, 180);
        }
        #endregion

        #region Override
        protected override void OnHandleCreated(EventArgs e) { SetRenderer(); base.OnHandleCreated(e); }
        protected override void OnCreateControl() { SetRenderer(); base.OnCreateControl(); }
        protected override void OnParentChanged(EventArgs e) { SetRenderer(); base.OnParentChanged(e); }
        protected override void OnPaint(PaintEventArgs e)
        {
            SetRenderer();
            var wnd2 = this.FindForm() as DvForm;
            if (wnd2 != null)
            {
                var thm = GetTheme();

                if (wnd2.Block)
                {
                    e.Graphics.Clear(ColorTool.MixColorAlpha(BackColor, Color.Black, DvForm.BlockAlpha));
                    this.ForeColor = ColorTool.MixColorAlpha(thm.MenuColorTable.TextColor, Color.Black, DvForm.BlockAlpha);
                }
                else this.ForeColor = thm.MenuColorTable.TextColor;
            }
            base.OnPaint(e);
        }
        #endregion

        #region Method
        #region SetRenderer
        private void SetRenderer()
        {
            var v = GetTheme();
            if (v != null && CurrentThemeName != v.ThemeName)
            {
                Renderer = new DvToolStripProfessionalRenderer(v.MenuColorTable);
                CurrentThemeName = v.ThemeName;
                this.BackColor = v.MenuColorTable.MenuStripColor;
                this.ForeColor = v.MenuColorTable.TextColor;
            }
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
        #endregion
    }
}
