using Devinno.Forms.Dialogs;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Devinno.Forms.Containers
{
    public class DvScrollablePanel : DvContainer
    {
        #region Interop
        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        static extern Int32 SetWindowTheme(IntPtr hWnd, string subAppName, string subIdList);
        #endregion

        #region Member Variable
        bool bFirst = true;
        #endregion

        #region Constructor
        public DvScrollablePanel()
        {
            AutoScroll = true;
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            if (bFirst)
            {
                if (Theme.Brightness == ThemeBrightness.Dark) 
                    SetWindowTheme(Handle, "DarkMode_Explorer", null);
                
                bFirst = false;
            }
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnVisibleChanged
        protected override void OnVisibleChanged(EventArgs e)
        {
            var wnd = FindForm() as DvForm;
            if (wnd != null) DwmTool.SetTheme(this.Handle, wnd.Theme.Brightness == ThemeBrightness.Dark);
            base.OnVisibleChanged(e);
        }
        #endregion
        #endregion
    }
}
