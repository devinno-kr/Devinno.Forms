using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
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
    public class DvBoxPanel : DvContainer
    {
        #region Properties
        #region Text / Icon
        private TextIcon texticon = new TextIcon();

        public DvIcon Icon => texticon.Icon;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap IconImage
        {
            get => texticon.IconImage;
            set { if (texticon.IconImage != value) { texticon.IconImage = value; Invalidate(); } }
        }
        public string IconString
        {
            get => texticon.IconString;
            set { if (texticon.IconString != value) { texticon.IconString = value; Invalidate(); } }
        }
        public float IconSize
        {
            get => texticon.IconSize;
            set { if (texticon.IconSize != value) { texticon.IconSize = value; Invalidate(); } }
        }
        public int IconGap
        {
            get => texticon.IconGap;
            set { if (texticon.IconGap != value) { texticon.IconGap = value; Invalidate(); } }
        }
        public DvTextIconAlignment IconAlignment
        {
            get => texticon.IconAlignment;
            set { if (texticon.IconAlignment != value) { texticon.IconAlignment = value; Invalidate(); } }
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Text
        {
            get => texticon.Text;
            set { if (texticon.Text != value) { base.Text = texticon.Text = value; Invalidate(); } }
        }

        public Padding TextPadding
        {
            get => texticon.TextPadding;
            set { if (texticon.TextPadding != value) { texticon.TextPadding = value; Invalidate(); } }
        }
        #endregion
        #region DrawTitle
        private bool bDrawTitle = true;
        public bool DrawTitle
        {
            get => bDrawTitle;
            set
            {
                if (bDrawTitle != value)
                {
                    bDrawTitle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Border
        private bool bBorder = true;
        public bool Border
        {
            get => bBorder;
            set
            {
                if (bBorder != value)
                {
                    bBorder = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Corner
        private int? nCorner = null;
        public int? Corner
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
        #region Round
        private RoundType? round = null;
        public RoundType? Round
        {
            get => round;
            set
            {
                if (round != value)
                {
                    round = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region PanelColor
        private Color? cPanelColor = null;
        public Color? PanelColor
        {
            get => cPanelColor;
            set
            {
                if (cPanelColor != value)
                {
                    cPanelColor = value;
                    if (cPanelColor.HasValue) BackColor = cPanelColor.Value;
                    else BackColor = cPanelColor ?? GetTheme().PanelColor;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Contstructor
        public DvBoxPanel()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, false);
            UpdateStyles();

            TabStop = false;
            #endregion

            Size = new Size(150, 100);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BackColor = Parent.BackColor;
            var PanelColor = this.PanelColor ?? Theme.PanelColor;
            var BorderColor = Theme.GetBorderColor(PanelColor, BackColor);
            var Corner = this.Corner ?? Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            this.BackColor = PanelColor;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            Pen p = new Pen(Color.Black);
            #endregion

            e.Graphics.Clear(BackColor);

            Areas((rtContent, rtText) =>
            {
                Theme.DrawBox(e.Graphics, rtContent, PanelColor, BorderColor, Round, Box.FlatBox(Border, true));
                Theme.DrawTextIcon(e.Graphics, texticon, Font, ForeColor, rtText, DvContentAlignment.TopLeft);
            });

            #region Dispose
            p.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtText = Util.FromRect(rtContent.Left + 10, rtContent.Top + 10, rtContent.Width - 10, rtContent.Height - 10);

            act(rtContent, rtText);
        }
        #endregion
        #endregion
    }
}
