using Devinno.Collections;
using Devinno.Forms.Containers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.ImageCanvas
{
    public class IvCanvas : DvContainer
    {
        #region Properties
        #region Pages
        private EventList<IvPage> lsPages = new EventList<IvPage>();
        public IvPage[] Pages
        {
            get => lsPages.ToArray();
            set
            {
                lsPages.Clear();
                lsPages.AddRange(value);
                Invalidate();
            }
        }
        #endregion
        #region SelectPage
        private IvPage sel = null;
        public IvPage SelectPage
        {
            get => sel;
            set
            {
                Controls.Clear();
                if (Pages.Contains(value))
                {
                    sel = value;
                    sel.Dock = System.Windows.Forms.DockStyle.Fill;
                    Controls.Add(sel);
                }
                else sel = null;

                this.Refresh();
            }
        }
        #endregion
        #endregion

        #region Constructor
        public IvCanvas()
        {
            Padding = new System.Windows.Forms.Padding(0);
        }
        #endregion

        #region Add/Remove
        public void RemovePage(IvPage page)
        {
            if (lsPages.Count > 0 && lsPages.Contains(page))
            {
                lsPages.Remove(page);
                this.Refresh();
            }
        }
        public void AddPage(IvPage page)
        {
            lsPages.Add(page);
            this.Refresh();
        }
        #endregion 
    }

    #region Class
    public class IvPage : DvContainer
    {
        #region OnImage
        private Bitmap bmOn = null;
        [EditorAttribute(typeof(System.Drawing.Design.ImageEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Bitmap OnImage { get => bmOn; set { if (bmOn != value) { bmOn = value; Invalidate(); } } }
        #endregion
        #region OffImage
        private Bitmap bmOff = null;
        [EditorAttribute(typeof(System.Drawing.Design.ImageEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Bitmap OffImage { get => bmOff; set { if (bmOff != value) { bmOff = value; Invalidate(); } } }
        #endregion
        #region OnOff
        private bool bOnOff = false;
        public bool OnOff
        {
            get => bOnOff;
            set
            {
                if (bOnOff != value)
                {
                    bOnOff = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Override
        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode)
            {
                if (OnOff && OnImage != null) e.Graphics.DrawImage(OnImage, new Rectangle(0, 0, Width, Height));
                if (!OnOff && OffImage != null) e.Graphics.DrawImage(OffImage, new Rectangle(0, 0, Width, Height));
            }
            else
            {
                if (OffImage != null) e.Graphics.DrawImage(OffImage, new Rectangle(0, 0, Width, Height));
            }

            base.OnPaint(e);
        }
        #endregion
    }
    #endregion
}
