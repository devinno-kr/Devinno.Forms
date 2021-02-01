using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Containers
{
    public class DvGroupBox : DvContainer
    {
        #region Properties
        #region Icon
        private DvIcon ico = new DvIcon();

        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        [Category("- 아이콘")]
        public Bitmap IconImage
        {
            get => ico.IconImage;
            set { if (ico.IconImage != value) { ico.IconImage = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public string IconString
        {
            get => ico.IconString;
            set { if (ico.IconString != value) { ico.IconString = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public int IconGap
        {
            get => ico.Gap;
            set { if (ico.Gap != value) { ico.Gap = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public DvTextIconAlignment IconAlignment
        {
            get => ico.Alignment;
            set { if (ico.Alignment != value) { ico.Alignment = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public float IconSize
        {
            get => ico.IconSize;
            set { if (ico.IconSize != value) { ico.IconSize = value; Invalidate(); } }
        }
        #endregion
        #region Text
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Text
        {
            get => base.Text; set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BorderWidth
        private int nBorderWidth = 1;
        public int BorderWidth
        {
            get => nBorderWidth;
            set
            {
                if (nBorderWidth != value)
                {
                    nBorderWidth = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BorderColor
        private Color cBorderColor = DvTheme.DefaultTheme.Color0;
        public Color BorderColor
        {
            get => cBorderColor;
            set
            {
                if (cBorderColor != value)
                {
                    cBorderColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvGroupBox()
        {
            Size = new Size(200, 150);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BorderColor = UseThemeColor ? Theme.Color0 : this.BorderColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtTitle = Areas["rtTitle"];
            var rtPanel = Areas["rtPanel"];
            #endregion
            #region Init
            var p = new Pen(Color.Black, 2);
            var br = new SolidBrush(Color.Black);
            #endregion
            #region Draw
            e.Graphics.Clear(Parent.BackColor);

            if (BackColor.GetBrightness() > BorderColor.GetBrightness())
                Theme.DrawBorder(e.Graphics, BorderColor, BackColor, BorderWidth, rtPanel, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT | BoxDrawOption.OUT_BEVEL_RB);
            else
                Theme.DrawBorder(e.Graphics, BorderColor, BackColor, BorderWidth, rtPanel, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);

            if (!string.IsNullOrWhiteSpace(Text))
            {
                br.Color = BackColor; e.Graphics.FillRectangle(br, rtTitle);
                Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor, BackColor, rtTitle, DvContentAlignment.MiddleCenter);
            }
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);
            var rtContent = Areas["rtContent"];
            var sz = g.MeasureTextIcon(ico, Text, Font);

            int nh = Convert.ToInt32(sz.Height);
            int nh2 = (nh / 2);
            var rtPanel = new Rectangle(rtContent.X, rtContent.Y + nh2, rtContent.Width, rtContent.Height - nh2); rtPanel.Inflate(-BorderWidth / 2, -BorderWidth / 2);
            var rtTitle = new Rectangle(rtPanel.X + 10, rtPanel.Y - nh2, Convert.ToInt32(sz.Width + 20), nh);

            SetArea("rtTitle", rtTitle);
            SetArea("rtPanel", rtPanel);
        }
        #endregion
        #endregion'
    }
}
