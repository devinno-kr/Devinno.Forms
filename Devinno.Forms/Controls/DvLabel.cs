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

namespace Devinno.Forms.Controls
{
    public class DvLabel : DvControl
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
        #region ContentAlignment
        private DvContentAlignment eContentAlignment = DvContentAlignment.MiddleCenter;
        public DvContentAlignment ContentAlignment
        {
            get { return eContentAlignment; }
            set { if (eContentAlignment != value) { eContentAlignment = value; Invalidate(); } }
        }
        #endregion
        #region BackgroundDraw
        private bool bBackgroundDraw = true;
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
        private Color? cLabelColor = null;
        public Color? LabelColor
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

        #region Style(Emboss)
        private Embossing eEmboss = Embossing.FlatConcave;
        public Embossing Style
        {
            get => eEmboss;
            set
            {
                if (eEmboss != value)
                {
                    eEmboss = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Unit
        private string strUnit = "";
        public string Unit
        {
            get => strUnit;
            set
            {
                if (strUnit != value)
                {
                    strUnit = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region UnitWidth
        private int? nUnitWidth = null;
        public int? UnitWidth
        {
            get => nUnitWidth;
            set
            {
                if (nUnitWidth != value)
                {
                    nUnitWidth = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvLabel()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, false);
            UpdateStyles();

            TabStop = false;
            #endregion

            Size = new Size(150, 30);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var LabelColor = this.LabelColor ?? Theme.LabelColor;
            var BorderColor = this.BorderColor ?? Theme.GetBorderColor(LabelColor, BackColor);
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            
            Areas((rtContent, rtText, rtUnit) =>
            {
                if (BackgroundDraw) Theme.DrawBox(e.Graphics, rtContent, LabelColor, BorderColor, Round, Box.LabelBox(Style, ShadowGap), Corner);

                Theme.DrawTextIcon(e.Graphics, texticon, Font, ForeColor, rtText, ContentAlignment);

                #region Unit
                if (UnitWidth.HasValue && UnitWidth.Value > 0 && !string.IsNullOrWhiteSpace(Unit))
                {
                    if (BackgroundDraw)
                    {
                        var szh = Convert.ToInt32(rtUnit.Height / 2);

                        using (var p = new Pen(Color.Black))
                        {
                            p.Width = 1;

                            p.Color = Util.FromArgb(Theme.OutBevelAlpha, Color.White);
                            e.Graphics.DrawLine(p, rtUnit.X + 1, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtUnit.X + 1, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);

                            p.Color = Util.FromArgb(Theme.OutShadowAlpha, Color.Black);
                            e.Graphics.DrawLine(p, rtUnit.X, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtUnit.X, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);
                        }
                    }

                    Theme.DrawText(e.Graphics, Unit, Font, ForeColor, rtUnit);
                }
                #endregion

            });
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion
        
        #region Method
        #region Areas
        public void Areas(Action<RectangleF, RectangleF, RectangleF> act)
        {
            var szUnitW = (UnitWidth.HasValue && UnitWidth.Value > 0) ? UnitWidth.Value : 0;

            var rtContent = GetContentBounds();
            var rtTextAll = new RectangleF(rtContent.Left, rtContent.Top, rtContent.Width - szUnitW, rtContent.Height);
            var rtTextArea = Util.FromRect(rtTextAll, TextPadding);
            var rtUnit = Util.FromRect(rtTextAll.Right, rtTextAll.Top, szUnitW, rtTextAll.Height);
            var rtText = Util.FromRect(rtTextArea.Left, rtTextArea.Top, rtTextArea.Width, rtTextArea.Height);

            act(rtContent, rtText, rtUnit);
        }
        #endregion
        #endregion
    }
}
