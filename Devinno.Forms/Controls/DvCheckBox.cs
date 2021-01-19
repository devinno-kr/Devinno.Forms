using Devinno.Forms.Themes;
using Devinno.Tools;
using Devinno.Extensions;
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
    public class DvCheckBox : DvControl
    {
        #region Properties
        #region BoxColor
        private Color cBoxColor = DvTheme.DefaultTheme.Color1;
        public Color BoxColor
        {
            get => cBoxColor;
            set { if (cBoxColor != value) { cBoxColor = value; Invalidate(); } }
        }
        #endregion
        #region CheckColor
        private Color cCheckColor = DvTheme.DefaultTheme.ForeColor;
        public Color CheckColor
        {
            get => cCheckColor;
            set { if (cCheckColor != value) { cCheckColor = value; Invalidate(); } }
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
        public DvCheckBox()
        {
            #region Set Style
            this.SetStyle(ControlStyles.Selectable, true);
            this.UpdateStyles();
            TabStop = true;
            #endregion
            #region Size
            Size = new System.Drawing.Size(120, 36);
            #endregion
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            
            var f = DpiRatio;
            var gap = Convert.ToInt32(5 * f);
            var nsz = Convert.ToInt32(18 * f);
            var npt = MathTool.MakeRectangle(rtContent, new Size(nsz, nsz)); //npt.Offset(0, GetTheme().TextOffsetY);
            
            var rtBox = new Rectangle(rtContent.X, npt.Y, nsz, nsz); 
            var rtText = new Rectangle(rtBox.Right + gap, rtBox.Y, rtContent.Width - gap - rtBox.Width, rtBox.Height);
            var rtCheck = new Rectangle(rtBox.X, rtBox.Y, rtBox.Width, rtBox.Height); rtCheck.Inflate(-Convert.ToInt32(4 * f), -Convert.ToInt32(4 * f));
            
            SetArea("rtBox", rtBox);
            SetArea("rtText", rtText);
            SetArea("rtCheck", rtCheck);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BoxColor = UseThemeColor ? Theme.Color1 : this.BoxColor;
            var CheckColor = UseThemeColor ? Theme.ForeColor : this.CheckColor;
            #endregion
            #region Set
            var f = (float)this.LogicalToDeviceUnits(1000) / 1000F;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            var p = new Pen(Color.Black);
            var br = new SolidBrush(Color.Black);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Draw
            var rtBox = Areas["rtBox"];
            var rtText = Areas["rtText"];
            var rtCheck = Areas["rtCheck"];
            Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtBox, RoundType.NONE, BoxDrawOption.OUT_BEVEL | BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW);
            #region Check
            if (Checked)
            {
                p.Width = Convert.ToInt32(3 * f);
                p.Color = CheckColor;
                e.Graphics.DrawLine(p, rtCheck.X, rtCheck.Y + rtCheck.Height / 2, rtCheck.X + rtCheck.Width / 2, rtCheck.Y + rtCheck.Height);
                e.Graphics.DrawLine(p, rtCheck.X + rtCheck.Width / 2 - 1, rtCheck.Y + rtCheck.Height, rtCheck.X + rtCheck.Width, rtCheck.Y);
                p.Width = 1;
            }
            #endregion
            Theme.DrawTextShadow(e.Graphics, null, Text, Font, ForeColor, BackColor, rtText, DvContentAlignment.MiddleLeft);
            #endregion
            #region Dispose
            p.Dispose();
            br.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var rtBox = Areas["rtBox"];
            var rtText = Areas["rtText"];
            if (CollisionTool.Check(rtBox, e.Location) || CollisionTool.Check(rtText, e.Location))
            {
                Checked = !Checked;
            }

            base.OnMouseDown(e);
        }
        #endregion
        #endregion
    }
}
