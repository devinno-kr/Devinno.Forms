using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Menus
{
    public class DvContextMenuStrip : ContextMenuStrip
    {
        #region Member Variable
        private string CurrentThemeName = null;
        private DvTheme RefTheme = null;
        #endregion

        #region Constructor
        public DvContextMenuStrip()
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
        protected override void OnPaint(PaintEventArgs e) { SetRenderer(); base.OnPaint(e); }
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
            }
        }
        #endregion
        #region GetTheme
        public DvTheme GetTheme()
        {
            DvTheme ret = null;
            try
            {
                if (RefTheme == null)
                {
                    if (SourceControl != null)
                    {
                        var o = SourceControl.FindForm();
                        if (o != null)
                        {
                            var pi = o.GetType().GetProperty("Theme");
                            var thm = pi.GetValue(o);
                            ret = thm as DvTheme;
                        }
                    }
                }
                else ret = RefTheme;
            }
            catch (Exception) { }
            return ret;
        }
        #endregion
        #region SetReferenceTheme
        public void SetReferenceTheme(DvTheme Theme)
        {
            RefTheme = Theme;
        }
        #endregion
        #endregion
    }
}
