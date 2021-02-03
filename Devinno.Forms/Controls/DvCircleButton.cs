using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Tools;
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

namespace Devinno.Forms.Controls
{
    public class DvCircleButton : DvControl
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
        #region ButtonBackColor
        private Color cButtonBackColor = DvTheme.DefaultTheme.Color1;
        public Color ButtonBackColor
        {
            get { return cButtonBackColor; }
            set
            {
                if (cButtonBackColor != value)
                {
                    cButtonBackColor = value;
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

        public bool UseLongClick { get => click.UseLongClick; set => click.UseLongClick = value; }
        public int LongClickTime { get => click.LongClickTime; set => click.LongClickTime = value; }
        #endregion

        #region Member Variable
        private bool bDown = false;
        private LongClick click = new LongClick();
        #endregion

        #region Event
        public event EventHandler LongClick;
        #endregion

        #region Constructor
        public DvCircleButton()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion
            #region Size
            this.Size = new Size(80, 80);
            #endregion

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

            var wh = Math.Min(rtContent.Width, rtContent.Height);
            var ng = Math.Min(wh / 8, 9);
            var rtButtonBack = DrawingTool.MakeRectangleAlign(rtContent, new Size(wh, wh), DvContentAlignment.MiddleCenter);
            var rtButton = new Rectangle(rtButtonBack.X, rtButtonBack.Y, rtButtonBack.Width, rtButtonBack.Height); rtButton.Inflate(-ng, -ng);
            var rtText = new Rectangle(rtContent.X + TextPadding.Left, rtContent.Y + TextPadding.Top, rtContent.Width - (TextPadding.Left + TextPadding.Right), rtContent.Height - (TextPadding.Top + TextPadding.Bottom));

            SetArea("rtText", rtText);
            SetArea("rtButtonBack", rtButtonBack);
            SetArea("rtButton", rtButton);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var ButtonColor = UseThemeColor ? Theme.Color3 : this.ButtonColor;
            var ButtonBackColor = UseThemeColor ? Theme.Color1 : this.ButtonBackColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtButtonBack = Areas["rtButtonBack"];
            var rtButton = Areas["rtButton"];
            var rtText = Areas["rtText"];
            #endregion
            #region Init
            var p = new Pen(ButtonColor, 1);
            var br = new SolidBrush(ButtonColor);
            #endregion
            
            if (rtContent.Width > 1 && rtContent.Height > 1)
            {
                #region Draw
                Theme.DrawBox(e.Graphics, ButtonBackColor, BackColor, rtButtonBack, RoundType.ELLIPSE, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL | BoxDrawOption.IN_SHADOW);

                if (!bDown)
                {
                    var cv = ButtonColor;
                    if (BackgroundDraw)
                    {
                        Theme.DrawBox(e.Graphics, cv, ButtonBackColor, rtButton, RoundType.ELLIPSE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT | BoxDrawOption.OUT_SHADOW | (Gradient ? BoxDrawOption.GRADIENT_LT : BoxDrawOption.NONE));

                        using (var pth = new GraphicsPath())
                        {
                            pth.AddEllipse(rtButton);
                            using (var pbr = new PathGradientBrush(pth))
                            {
                                pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtButton.Left, rtButton.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtButton.Top, rtButton.Bottom)));
                                pbr.CenterColor = Color.FromArgb(30, Color.White);
                                pbr.SurroundColors = new Color[] { Color.FromArgb(30, Color.Black) };

                                e.Graphics.FillEllipse(pbr, rtButton);
                            }
                            Theme.DrawBorder(e.Graphics, cv.BrightnessTransmit(Theme.BorderBright), cv, 1, rtButton, RoundType.ELLIPSE, BoxDrawOption.BORDER);
                        }
                    }
                    Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor, BackgroundDraw ? cv : BackColor, new Rectangle(rtText.X, rtText.Y + 0, rtText.Width, rtText.Height), DvContentAlignment.MiddleCenter);
                }
                else
                {
                    var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                    if (BackgroundDraw)
                    {
                        Theme.DrawBox(e.Graphics, cv, ButtonBackColor, rtButton, RoundType.ELLIPSE, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | (Gradient ? BoxDrawOption.GRADIENT_LT : BoxDrawOption.NONE));
                    }
                    Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor.BrightnessTransmit(Theme.DownBright), BackgroundDraw ? cv : BackColor, new Rectangle(rtText.X, rtText.Y + 1, rtText.Width, rtText.Height), DvContentAlignment.MiddleCenter);
                }


                #endregion
            }
            
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
                Focus();

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
            click.MouseUp(e);
            bDown = false; Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion
    }
}
