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
    public class DvLamp : DvControl
    {
        #region Properties
        #region Text
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Text
        {
            get => base.Text;
            set { if (base.Text != value) { base.Text = value; Invalidate(); } }
        }

        private Padding padText = new Padding(0);
        public Padding TextPadding
        {
            get => padText;
            set { if (padText != value) { padText = value; Invalidate(); } }
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

        #region OnLampColor
        private Color? cOnLampColor = null;
        public Color? OnLampColor
        {
            get => cOnLampColor;
            set
            {
                if (cOnLampColor != value)
                {
                    cOnLampColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OffLampColor
        private Color? cOffLampColor = null;
        public Color? OffLampColor
        {
            get => cOffLampColor;
            set
            {
                if (cOffLampColor != value)
                {
                    cOffLampColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LampAlignment
        private DvTextIconAlignment eLampAlignment = DvTextIconAlignment.LeftRight;
        public DvTextIconAlignment LampAlignment
        {
            get => eLampAlignment;
            set
            {
                if (eLampAlignment != value)
                {
                    eLampAlignment = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LampGap
        private int nLampGap = 3;
        public int LampGap
        {
            get => nLampGap;
            set
            {
                if (nLampGap != value)
                {
                    nLampGap = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region LampSize
        private int nLampSize = 20;
        public int LampSize
        {
            get => nLampSize;
            set
            {
                if (nLampSize != value)
                {
                    nLampSize = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OnOff
        private bool bOnOff = false;
        public bool OnOff
        {
            get { return bOnOff; }
            set
            {
                if (bOnOff != value)
                {
                    bOnOff = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvLamp()
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
            var OnLampColor = this.OnLampColor ?? Theme.LampOnColor;
            var OffLampColor = this.OffLampColor ?? Theme.LampOffColor;
            var Corner = Theme.Corner;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent, rtLamp, rtText) =>
            {
                var cON =  OnLampColor;
                var cOFF =  OffLampColor;
                var cT =  ForeColor;
                var cL = Util.FromArgb(Theme.OutBevelAlpha, Color.White);
                 
                Theme.DrawLamp(e.Graphics, rtLamp, BackColor, cON, cOFF, OnOff);
                Theme.DrawText(e.Graphics, Text, Font, cT, rtText, ALIGN(ContentAlignment));
            });

            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        /// <summary>
        /// ( rtContent, rtLamp, rtText )
        /// </summary>
        /// <param name="act"></param>
        public void Areas(Action<RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtBounds = Util.FromRect(rtContent, TextPadding);
            using (var g = CreateGraphics())
            {
                Util.TextIconBounds(g,
                                    rtBounds, ContentAlignment,
                                    Text, Font,
                                    LampGap, new SizeF(LampSize, LampSize), LampAlignment,
                                    (rtLamp, rtText) =>
                                    {
                                        act(rtContent, rtLamp, rtText);
                                    });
            }
        }
        #endregion
        #region ALIGN
        DvContentAlignment ALIGN(DvContentAlignment align)
        {
            var ret = DvContentAlignment.MiddleCenter;
            switch (align)
            {
                case DvContentAlignment.TopLeft:
                case DvContentAlignment.MiddleLeft:
                case DvContentAlignment.BottomLeft:
                    ret = DvContentAlignment.MiddleLeft;
                    break;

                case DvContentAlignment.TopCenter:
                case DvContentAlignment.MiddleCenter:
                case DvContentAlignment.BottomCenter:
                    ret = DvContentAlignment.MiddleCenter;
                    break;

                case DvContentAlignment.TopRight:
                case DvContentAlignment.MiddleRight:
                case DvContentAlignment.BottomRight:
                    ret = DvContentAlignment.MiddleRight;
                    break;
            }
            return ret;
        }
        #endregion
        #endregion
    }
}
