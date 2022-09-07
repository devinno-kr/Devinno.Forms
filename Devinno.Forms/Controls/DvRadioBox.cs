using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using Devinno.Tools;
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
    public class DvRadioBox : DvControl
    {
        #region Properties
        #region RadioColor
        private Color? cRadioColor = null;
        public Color? RadioColor
        {
            get => cRadioColor;
            set
            {
                if (cRadioColor != value)
                {
                    cRadioColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BoxColor
        private Color? cBoxColor = null;
        public Color? BoxColor
        {
            get => cBoxColor;
            set
            {
                if (cBoxColor != value)
                {
                    cBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BoxSize
        private int nBoxSize = 20;
        public int BoxSize
        {
            get => nBoxSize;
            set
            {
                if (nBoxSize != value)
                {
                    nBoxSize = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Text
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Text
        {
            get => base.Text;
            set { if (base.Text != value) { base.Text = value; Invalidate(); } }
        }
        #endregion
        #region Checked
        private bool bChecked = false;
        public bool Checked
        {
            get { return bChecked; }
            set
            {
                if (bChecked != value)
                {
                    bChecked = value;
                    if (bChecked && this.Parent != null)
                    {
                        foreach (var c in this.Parent.Controls)
                        {
                            var cr = c as DvRadioBox;
                            if (cr != null && !this.Equals(cr)) cr.Checked = false;
                        }
                    }
                    CheckedChanged?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Event
        public event EventHandler CheckedChanged;
        #endregion

        #region Constructor
        public DvRadioBox()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion
            #region Size
            Size = new System.Drawing.Size(150, 30);
            #endregion
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BoxColor = this.BoxColor ?? Theme.CheckBoxColor;
            var RadioColor = this.RadioColor ?? Theme.ForeColor;
            var BorderColor = Theme.GetBorderColor(BoxColor, BackColor);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent, rtBox, rtRadio, rtText) =>
            {
                Theme.DrawBox(e.Graphics, rtBox, BoxColor, BorderColor, RoundType.Ellipse, Box.BackBox(ShadowGap));
                #region Check
                if (Checked)
                {
                    using (var br = new SolidBrush(RadioColor))
                    {
                        br.Color = RadioColor;
                        e.Graphics.FillEllipse(br, rtRadio);
                    }
                }
                #endregion
                Theme.DrawText(e.Graphics, Text, Font, ForeColor, rtText, DvContentAlignment.MiddleLeft);

            });
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtBox, rtRadio, rtText) =>
            {
                if (CollisionTool.Check(rtBox, e.Location) || CollisionTool.Check(rtText, e.Location))
                {
                    Checked = true;
                    Focus();
                    Invalidate();
                }
            });
            base.OnMouseDown(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        /// <summary>
        /// ( rtContent, rtBox, rtRadio, rtText )
        /// </summary>
        /// <param name="act"></param>
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var INF = BoxSize / 4;
            var GAP = 8;

            var rtContent = GetContentBounds();
            var rtBox = Util.MakeRectangleAlign(rtContent, new SizeF(BoxSize, BoxSize), DvContentAlignment.MiddleLeft); rtBox.Offset(0, 0);
            var rtText = Util.FromRect(rtBox.Right + GAP, rtContent.Top, rtContent.Width - (GAP + rtBox.Width), rtContent.Height);
            var rtRadio = Util.FromRect(rtBox.Left, rtBox.Top, rtBox.Width, rtBox.Height); rtRadio.Inflate(-INF, -INF);

            act(rtContent, rtBox, rtRadio, rtText);
        }
        #endregion
        #endregion
    }
}
