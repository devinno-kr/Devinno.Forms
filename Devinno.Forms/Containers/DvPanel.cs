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

namespace Devinno.Forms.Containers
{
    public class DvPanel : DvContainer
    {
        #region Properties
        #region Icon
        private DvIcon ico = new DvIcon();

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DvIcon Icon => ico;

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
        #region TitleFont
        private Font ftTitleFont = new Font("나눔고딕", 9, FontStyle.Bold);
        public Font TitleFont
        {
            get => ftTitleFont;
            set
            {
                if (ftTitleFont != value)
                {
                    ftTitleFont = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TitleHeight
        private int nTitleHeight = 30;
        public int TitleHeight
        {
            get => nTitleHeight;
            set
            {
                if (nTitleHeight != value)
                {
                    nTitleHeight = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TitleColor
        private Color cTitleColor = DvTheme.DefaultTheme.Color2;
        public Color TitleColor
        {
            get => cTitleColor;
            set
            {
                if (cTitleColor != value)
                {
                    cTitleColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region DrawTitle
        private bool bDrawTitle = true;
        public bool DrawTitle
        {
            get => bDrawTitle;
            set
            {
                if(bDrawTitle != value)
                {
                    bDrawTitle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Style
        private DvPanelStyle eStyle = DvPanelStyle.A;
        public DvPanelStyle Style
        {
            get => eStyle;
            set
            {
                if(eStyle != value)
                {
                    eStyle = value;
                    Invalidate();
                }
            }
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
        #endregion

        #region Constructor
        public DvPanel()
        {
            this.Padding = new System.Windows.Forms.Padding(7, 7 + TitleHeight, 7, 7);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            if (Style == DvPanelStyle.A)
            {
                var rtTitle = new Rectangle(rtContent.X + 1, rtContent.Y, rtContent.Width - 2, TitleHeight);
                var rtPanel = new Rectangle(rtContent.X, rtTitle.Bottom, rtContent.Width, rtContent.Height - rtTitle.Bottom);
                var rtText = new Rectangle(rtTitle.X + TextPadding.Left, rtTitle.Y + TextPadding.Top, rtTitle.Width - (TextPadding.Left + TextPadding.Right), rtTitle.Height - (TextPadding.Top + TextPadding.Bottom));
                SetArea("rtTitle", rtTitle);
                SetArea("rtPanel", rtPanel);
                SetArea("rtText", rtText);
            }
            else
            {
                var rtTitle = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, TitleHeight);
                var rtPanel = rtContent;
                var rtText = new Rectangle(rtTitle.X + TextPadding.Left, rtTitle.Y + TextPadding.Top, rtTitle.Width - (TextPadding.Left + TextPadding.Right), rtTitle.Height - (TextPadding.Top + TextPadding.Bottom));
                SetArea("rtTitle", rtTitle);
                SetArea("rtPanel", rtPanel);
                SetArea("rtText", rtText);
            }
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var TitleColor = UseThemeColor ? Theme.Color2 : this.TitleColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtPanel = Areas["rtPanel"];
            var rtTitle = Areas["rtTitle"];
            var rtText = Areas["rtText"];
            #endregion
            #region Init
            var p = new Pen(TitleColor, 2);
            var br = new SolidBrush(TitleColor);
            #endregion
            #region Draw
            e.Graphics.Clear(Parent.BackColor);
            if (Style == DvPanelStyle.A)
            {
                if (DrawTitle)
                {
                    Theme.DrawBox(e.Graphics, TitleColor, Parent.BackColor, rtTitle, RoundType.T, BoxDrawOption.BORDER | BoxDrawOption.GRADIENT_V | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
                    Theme.DrawTextShadow(e.Graphics, ico, Text, TitleFont, ForeColor, TitleColor, rtText, DvContentAlignment.MiddleLeft);
                }
                Theme.DrawBox(e.Graphics, BackColor, Parent.BackColor, rtPanel, (DrawTitle ? RoundType.B : RoundType.ALL), BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
            }
            else
            {
                Theme.DrawBox(e.Graphics, BackColor, Parent.BackColor, rtPanel, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT | BoxDrawOption.OUT_SHADOW);

                if (DrawTitle)
                {
                    Theme.DrawTextShadow(e.Graphics, ico, Text, TitleFont, ForeColor, TitleColor, rtText, DvContentAlignment.MiddleLeft);
                }
            }
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

    public enum DvPanelStyle { A, B }
}
