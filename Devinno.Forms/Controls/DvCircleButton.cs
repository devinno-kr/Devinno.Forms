using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
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
using ThreadPool = System.Threading.ThreadPool;
using Thread = System.Threading.Thread;
namespace Devinno.Forms.Controls
{
    public class DvCircleButton : DvControl
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
        #region ButtonColor
        private Color? cButtonColor = null;
        public Color? ButtonColor
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
        #region ButtonBackColor
        private Color? cButtonBackColor = null;
        public Color? ButtonBackColor
        {
            get => cButtonBackColor;
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

        #region Gradient
        private bool bGradient = true;
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
        public bool Clickable { get; set; } = true;
        #endregion
        #region UseKey
        public bool UseKey { get; set; } = false;
        #endregion
        #endregion

        #region Member Variable
        private bool bDown = false;
        #endregion

        #region Event
        public event EventHandler ButtonClick;
        #endregion

        #region Constructor
        public DvCircleButton()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 30);

            #region KeyPress
            KeyPress += (o, s) =>
            {
                if (UseKey && Focused)
                {
                    if (s.KeyChar == '\r' || s.KeyChar == ' ')
                    {
                        ThreadPool.QueueUserWorkItem((o) =>
                        {
                            this.Invoke(new Action(() =>
                            {
                                bDown = true;
                                Invalidate();

                            }));

                            Thread.Sleep(50);

                            this.Invoke(new Action(() =>
                            {
                                if (bDown)
                                {
                                    bDown = false;
                                    Invalidate();
                                    ButtonClick?.Invoke(this, null);
                                }
                            }));

                        });
                    }
                }
            };
            #endregion
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
            var ButtonBackColor = this.ButtonColor ?? Theme.ConcaveBoxColor;
            var BorderColor = Theme.GetBorderColor(ButtonColor, BackColor);
            var BorderBackColor = Theme.GetBorderColor(ButtonBackColor, BackColor);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            var p = new Pen(ButtonColor, 1);
            var br = new SolidBrush(ButtonColor);
            #endregion

            Areas((rtContent, rtCircleBack, rtCircle, rtText) =>
            {
                if (rtContent.Width > 1 && rtContent.Height > 1)
                {
                    Theme.DrawBox(e.Graphics, rtCircleBack, ButtonBackColor, BorderBackColor, RoundType.Ellipse, Box.BackBox(ShadowGap));

                    if(!bDown)
                    {
                        #region Up
                        var cV = ButtonColor;
                        var cB = ButtonBackColor;
                        var cT = ForeColor;

                        if (BackgroundDraw)
                        {
                            Theme.DrawBox(e.Graphics, rtCircle, cV, cB, RoundType.Ellipse, Box.Style((Gradient ? Fill.GradientLT : Fill.Fill ), Embossing.Convex, ShadowGap, true));

                            if (Gradient)
                            {
                                rtCircle.Inflate(-1, -1);
                                using (var pth = new GraphicsPath())
                                {
                                    pth.AddEllipse(rtCircle);
                                    using (var pbr = new PathGradientBrush(pth))
                                    {
                                        pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtCircle.Left, rtCircle.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rtCircle.Top, rtCircle.Bottom)));
                                        pbr.CenterColor = Color.FromArgb(45, Color.White);
                                        pbr.SurroundColors = new Color[] { Color.FromArgb(30, Color.Black) };

                                        e.Graphics.FillEllipse(pbr, rtCircle);
                                    }
                                }
                            }
                        }
                        Theme.DrawTextIcon(e.Graphics, texticon, Font, cT, rtText);
                        #endregion
                    }
                    else
                    {
                        #region Down
                        var cV = ButtonColor.BrightnessTransmit(Theme.DownBrightness);
                        var cB= ButtonBackColor.BrightnessTransmit(Theme.DownBrightness);
                        var cT = ForeColor.BrightnessTransmit(Theme.DownBrightness);
                        if (BackgroundDraw)
                        {
                            Theme.DrawBox(e.Graphics, rtCircle, cV, cB, RoundType.Ellipse, Box.BackBox(ShadowGap));
                        }

                        rtText.Offset(0, 1);
                        Theme.DrawTextIcon(e.Graphics, texticon, Font, cT, rtText);
                        #endregion
                    }
                }
            });

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
                Areas((rtContent, rtCircleBack, rtCircle, rtText) =>
                {
                    if (CollisionTool.CheckEllipse(rtCircle, e.Location))
                    {
                        Focus();

                        bDown = true;
                        Invalidate();
                    }
                });
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Clickable)
            {
                Areas((rtContent, rtCircleBack, rtCircle, rtText) =>
                {
                    if (bDown)
                    {
                        bDown = false;
                        Invalidate();
                        if (CollisionTool.CheckEllipse(rtCircle, e.Location)) ButtonClick?.Invoke(this, null);
                    }
                });
            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        /// <summary>
        /// ( rtContent, rtCircleBack, rtCircle, rtText )
        /// </summary>
        /// <param name="act"></param>
        public void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtText = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height - 1);
            var wh = Convert.ToInt32(Math.Min(rtContent.Width, rtContent.Height));
            //var ng = Math.Min(rtContent.Height * 0.1F, 9F);
            var ng = rtContent.Height * 0.1F;
            var rtCircleBack = Util.MakeRectangle(rtContent, new SizeF(wh, wh));
            var rtCircle = Util.FromRect(rtCircleBack); rtCircle.Inflate(-ng, -ng);

            act(rtContent, rtCircleBack, rtCircle, rtText);
        }
        #endregion
        #endregion
    }
}
