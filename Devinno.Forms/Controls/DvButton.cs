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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvButton : DvControl
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
                if(bBackgroundDraw != value)
                {
                    bBackgroundDraw = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ButtonColor
        private Color cButtonColor = DvTheme.DefaultTheme.Color3;
        [Category("- 색상")]
        public Color ButtonColor 
        {
            get => cButtonColor;
            set
            {
                if (cButtonColor != value)
                {
                    cButtonColor = value; 
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

        #region Gradient
        private bool bGradient = true;
        [Category("- 색상")]
        public bool Gradient
        {
            get => bGradient;
            set
            {
                if (bGradient != value)
                {
                    bGradient = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Clickable
        [Category("- 기능")]
        public bool Clickable { get; set; } = true;
        #endregion

        public bool UseLongClick { get => click.UseLongClick; set=> click.UseLongClick = value; }
        public int LongClickTime { get => click.LongClickTime; set => click.LongClickTime = value; } 
        #endregion

        #region Event
        public event EventHandler LongClick;
        public event EventHandler ButtonClick;
        #endregion

        #region Member Variable
        private bool bDown = false;
        private LongClick click = new LongClick();
        #endregion

        #region Constructor
        public DvButton()
        {
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            Size = new Size(80, 36);

            TabStop = true;

            click.Reset = new Action(() => { this.Invoke(new Action(() => { bDown = false; Invalidate(); })); });
            click.GenLongClick = new Action(() => { this.Invoke(new Action(() => LongClick?.Invoke(this, null))); });
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);
            var rtContent = Areas["rtContent"];
            var rtText = new Rectangle(rtContent.X + TextPadding.Left, rtContent.Y + TextPadding.Top, rtContent.Width - (TextPadding.Left + TextPadding.Right), rtContent.Height - (TextPadding.Top + TextPadding.Bottom));
            SetArea("rtText", rtText);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var ButtonColor = UseThemeColor ? Theme.Color3 : this.ButtonColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtText = Areas["rtText"];
            #endregion
            #region Init
            var p = new Pen(ButtonColor, 1);
            var br = new SolidBrush(ButtonColor);
            #endregion
            #region Draw
            if (!bDown)
            {
                var cv = ButtonColor;
                if (BackgroundDraw) Theme.DrawBox(e.Graphics, cv, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | (Gradient ? BoxDrawOption.GRADIENT_V : BoxDrawOption.NONE));
                Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor, BackgroundDraw ? cv : BackColor, new Rectangle(rtText.X, rtText.Y + 0, rtText.Width, rtText.Height), DvContentAlignment.MiddleCenter);
            }
            else
            {
                var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                if (BackgroundDraw) Theme.DrawBox(e.Graphics, cv, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | (Gradient ? BoxDrawOption.GRADIENT_V_REVERSE : BoxDrawOption.NONE));
                Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor.BrightnessTransmit(Theme.DownBright), BackgroundDraw ? cv : BackColor, new Rectangle(rtText.X, rtText.Y + 1, rtText.Width, rtText.Height), DvContentAlignment.MiddleCenter);
            }
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion

            base.OnThemeDraw(e, Theme);
        }
        #endregion

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Clickable)
            {
                bDown = true; 
                Invalidate();

                click.MouseDown(e);
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (bDown) ButtonClick?.Invoke(this, null);
            click.MouseUp(e);
            bDown = false; Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion
    }
}
