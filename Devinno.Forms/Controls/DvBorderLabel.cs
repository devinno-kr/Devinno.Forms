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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvBorderLabel : DvControl
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

        #region BorderColor
        private Color cBorderColor = DvTheme.DefaultTheme.Color3;
        public Color BorderColor
        {
            get { return cBorderColor; }
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
        #region BorderWidth
        private int nBorderWidth = 3;
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
        #region Corner
        private int nCorner = DvTheme.DefaultTheme.Corner;
        public int Corner
        {
            get => nCorner;
            set
            {
                if (nCorner != value)
                {
                    nCorner = value;
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
        #region DrawBorder
        private bool bDrawBorder = true;
        public bool DrawBorder
        {
            get => bDrawBorder;
            set
            {
                if (bDrawBorder != value)
                {
                    bDrawBorder = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvBorderLabel()
        {
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            Size = new Size(80, 36);

            TabStop = true;
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            var rtBorder = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height); rtBorder.Inflate(-(BorderWidth / 2), -(BorderWidth / 2));

            var rtText = new Rectangle(rtContent.X + TextPadding.Left, rtContent.Y + TextPadding.Top, rtContent.Width - (TextPadding.Left + TextPadding.Right), rtContent.Height - (TextPadding.Top + TextPadding.Bottom));
            SetArea("rtText", rtText);
            SetArea("rtBorder", rtBorder);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BorderColor = UseThemeColor ? Theme.Color3 : this.BorderColor;
            var LabelColor = UseThemeColor ? Theme.Color2 : this.LabelColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtBorder = Areas["rtBorder"];
            var rtText = Areas["rtText"];
            #endregion
            #region Init
            var p = new Pen(BorderColor, 2);
            var br = new SolidBrush(BorderColor);
            #endregion
            #region Draw
            if (DrawBorder)
            {
                #region Border Shadow
                rtBorder.Inflate(-(BorderWidth / 2), -(BorderWidth / 2));

                p.Width = BorderWidth;

                p.Color = BackColor.BrightnessTransmit(Theme.OutShadowBright);
                rtBorder.Offset(Theme.ShadowGap, Theme.ShadowGap); e.Graphics.DrawRoundRectangle(p, rtBorder, Corner);
                #endregion
                #region Fill
                if (LabelColor != Color.Transparent)
                {
                    br.Color = LabelColor;
                    e.Graphics.FillRoundRectangle(br, rtBorder, Corner);
                }
                #endregion
                #region Border
                p.Color = BorderColor;
                rtBorder.Offset(-Theme.ShadowGap, -Theme.ShadowGap); e.Graphics.DrawRoundRectangle(p, rtBorder, Corner);
                #endregion
            }
            Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor, BackColor, new Rectangle(rtText.X, rtText.Y + 0, rtText.Width, rtText.Height), ContentAlignment);
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
