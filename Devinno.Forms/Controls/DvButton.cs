﻿using Devinno.Extensions;
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
using ThreadPool = System.Threading.ThreadPool;
using Thread = System.Threading.Thread;

namespace Devinno.Forms.Controls
{
    public class DvButton : DvControl
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
            
        #region Round
        private RoundType? round = null;
        public RoundType? Round
        {
            get => round;
            set
            {
                if(round != value)
                {
                    round = value;
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
        #region DownState 
        public bool DownState => bDown;
        #endregion
        #endregion

        #region Member Variable
        private bool bDown = false;
        #endregion

        #region Event
        public event EventHandler ButtonClick;
        #endregion

        #region Constructor
        public DvButton()
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
            var BorderColor = Theme.GetBorderColor(ButtonColor, BackColor);
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent, rtText) =>
            {
                var cB = bDown ? BorderColor.BrightnessTransmit(Theme.DownBrightness) : BorderColor;
                var cF = bDown ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                var cT = bDown ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;
                var cL = Util.FromArgb(Theme.OutBevelAlpha, Color.White);

                if (BackgroundDraw)
                {
                    if (!bDown)
                    {
                        Theme.DrawBox(e.Graphics, rtContent, cF, cB, Round, Box.ButtonUp_V(Gradient, ShadowGap), Corner);
                    }
                    else Theme.DrawBox(e.Graphics, rtContent, cF, cB, Round, Box.ButtonDown(ShadowGap), Corner);
                }

                if (bDown) rtText.Offset(0, 1);

                Theme.DrawTextIcon(e.Graphics, texticon, Font, cT, rtText, ContentAlignment);
            });

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
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Clickable)
            {
                if (bDown)
                {
                    bDown = false;
                    Invalidate();
                    ButtonClick?.Invoke(this, null);
                }
            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        /// <summary>
        /// ( rtContent, rtText )
        /// </summary>
        /// <param name="act"></param>
        public void Areas(Action<RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtText = Util.FromRect(rtContent, TextPadding);

            act(rtContent, rtText);
        }
        #endregion
        #endregion
    }
}
