using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvLabel : DvControl
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
        #region ContentAlignment
        private DvContentAlignment eContentAlignment = DvContentAlignment.MiddleCenter;
        [Category("- 모양")]
        public DvContentAlignment ContentAlignment
        {
            get { return eContentAlignment; }
            set { if (eContentAlignment != value) { eContentAlignment = value; Invalidate(); } }
        }
        #endregion
        #region BackgroundDraw
        private bool bBackgroundDraw = true;
        [Category("- 모양")]
        public bool BackgroundDraw
        {
            get => bBackgroundDraw;
            set
            {
                if (bBackgroundDraw != value)
                {
                    bBackgroundDraw = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LabelColor
        private Color cLabelColor = DvTheme.DefaultTheme.Color2;
        [Category("- 색상")]
        public Color LabelColor
        {
            get => cLabelColor;
            set
            {
                if (cLabelColor != value)
                {
                    cLabelColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Text
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Category("- 모양")]
        public override string Text
        {
            get => base.Text;
            set { if (base.Text != value) { base.Text = value; Invalidate(); } }
        }
        #endregion
        #region TextPadding
        private Padding padText = new Padding(0, 0, 0, 0);
        [Category("- 모양")]
        public Padding TextPadding
        {
            get => padText;
            set
            {
                if (padText != value)
                {
                    padText = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region InShadow
        private bool bInShadow = true;
        public bool InShadow
        {
            get => bInShadow;
            set
            {
                if (bInShadow != value)
                {
                    bInShadow = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvLabel()
        {
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            Size = new Size(80, 36);

            TabStop = true;
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var LabelColor = UseThemeColor ? Theme.Color2 : this.LabelColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = GetContentBounds();
            var rtText = new Rectangle(rtContent.X + TextPadding.Left, rtContent.Y + TextPadding.Top, rtContent.Width - (TextPadding.Left + TextPadding.Right), rtContent.Height - (TextPadding.Top + TextPadding.Bottom));
            #endregion
            #region Init
            var p = new Pen(LabelColor, 1);
            var br = new SolidBrush(LabelColor);
            #endregion
            #region Draw
            if (BackgroundDraw) Theme.DrawBox(e.Graphics, LabelColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | (InShadow ? BoxDrawOption.IN_SHADOW : BoxDrawOption.NONE) | BoxDrawOption.OUT_BEVEL);
            Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor, BackgroundDraw ? LabelColor : BackColor, new Rectangle(rtText.X, rtText.Y + 0, rtText.Width, rtText.Height), ContentAlignment);
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion

            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion
    }
}
