using Devinno.Forms.Extensions;
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
    public class DvGroupBox : DvContainer
    {
        #region Properties
        #region Text / Icon
        private TextIcon texticon = new TextIcon();

        public DvIcon Icon => texticon.Icon;
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

        #region Corner
        private int? nCorner = null;
        public int? Corner
        {
            get => nCorner;
            set
            {
                if(nCorner != value)
                {
                    nCorner = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BorderThickness
        private BorderThickness eBorderThickness = BorderThickness.Thin;
        public BorderThickness BorderThickness
        {
            get => eBorderThickness;
            set
            {
                if (eBorderThickness != value)
                {
                    eBorderThickness = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BorderColor
        private Color? cBorderColor = null;
        public Color? BorderColor
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
        #region BorderWidth
        private int BorderWidth
        {
            get
            {
                var ret = 1;
                switch (eBorderThickness)
                {
                    case BorderThickness.Thin: ret = 1; break;
                    case BorderThickness.Normal: ret = 2; break;
                    case BorderThickness.Bold: ret = 3; break;
                }
                return ret;
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvGroupBox()
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
            var BorderColor = this.BorderColor ?? Theme.GroupBoxColor;
            var Corner = this.Corner ?? Theme.Corner;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            Pen p = new Pen(Color.Black);
            SolidBrush br = new SolidBrush(Color.Black);
            #endregion

            Areas((rtContent, rtPanel, rtText) =>
            {
                e.Graphics.Clear(Parent.BackColor);
                #region Back
                Theme.DrawBox(e.Graphics, rtContent, BackColor, BackColor, RoundType.All, Box.FlatBox());
                #endregion
                #region Border
                p.Width = BorderWidth;
                
                var rt = rtPanel; rt.Offset(1, 1);
                p.Color = Color.FromArgb(Theme.OutBevelAlpha, Color.White);
                e.Graphics.DrawRoundRectangle(p, rt, Corner);

                p.Color = BorderColor;
                e.Graphics.DrawRoundRectangle(p, rtPanel, Corner);
                #endregion
                #region Text
                {
                    if(!string.IsNullOrWhiteSpace(Text))
                    {
                        br.Color = Parent.BackColor;
                        e.Graphics.FillRectangle(br, rtText);

                        Theme.DrawTextIcon(e.Graphics, texticon, Font, ForeColor, rtText, DvContentAlignment.MiddleCenter);
                    }
                }
                #endregion
            });

            #region Dispose
            p.Dispose();
            br.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF> act)
        {
            using (var g = CreateGraphics())
            {
                var sz = g.MeasureTextIcon(texticon, Font);

                var rtContent = GetContentBounds();
                var rtPanel = rtContent; rtPanel.Inflate(-BorderWidth / 2F, -BorderWidth / 2F);
                var rtText = Util.FromRect(10, 0, sz.Width + 20, sz.Height);

                var gp = Convert.ToInt32(sz.Height / 2);
                rtPanel.Y = gp;
                rtPanel.Height -= gp;
                act(rtContent, rtPanel, rtText);
            }
        }
        #endregion
        #endregion
    }
}
