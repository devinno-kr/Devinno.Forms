using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Containers
{
    public class DvBorderPanel : DvContainer
    {
        #region Properties
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
        #region DrawBackground
        private bool bDrawBackground = false;
        public bool DrawBackground
        {
            get => bDrawBackground;
            set
            {
                if (bDrawBackground != value)
                {
                    bDrawBackground = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #endregion

        #region Constructor
        public DvBorderPanel()
        {
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            Size = new Size(300, 200);

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

            SetArea("rtBorder", rtBorder);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BorderColor = UseThemeColor ? Theme.Color3 : this.BorderColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtBorder = Areas["rtBorder"];
            #endregion
            #region Init
            var p = new Pen(BorderColor, 2);
            var br = new SolidBrush(BorderColor);
            #endregion
            #region Draw
            if (DrawBorder)
            {
                e.Graphics.Clear(Parent.BackColor);
                br.Color = BackColor;     e.Graphics.FillRoundRectangle(br, rtBorder, Corner);
                #region Border Shadow
                //rtBorder.Inflate(-1, -1);

                p.Width = BorderWidth;

                p.Color = Parent.BackColor.BrightnessTransmit(Theme.OutShadowBright);
                rtBorder.Offset(Theme.ShadowGap, Theme.ShadowGap); 
                e.Graphics.DrawRoundRectangle(p, rtBorder, Corner);
                rtBorder.Offset(-Theme.ShadowGap, -Theme.ShadowGap);
                #endregion
                if (DrawBackground)
                {
                    #region Fill
                    if (BackColor != Color.Transparent)
                    {
                        br.Color = BackColor;
                        e.Graphics.FillRoundRectangle(br, rtBorder, Corner);
                    }
                    #endregion
                }
                #region Border
                p.Color = BorderColor;
                e.Graphics.DrawRoundRectangle(p, rtBorder, Corner);
                #endregion
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
}
