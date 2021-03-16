using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvColorPicker : DvControl
    {
        #region Properties
        #region ButtonColor
        private Color cButtonColor = DvTheme.DefaultTheme.Color3;
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
        #region ButtonWidth
        int nButtonWidth = 60;
        public int ButtonWidth
        {
            get { return nButtonWidth; }
            set { if (nButtonWidth != value) { nButtonWidth = value; Invalidate(); } }
        }
        #endregion
        #region BoxColor
        private Color cBoxColor = DvTheme.DefaultTheme.Color2;
        public Color BoxColor
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
        #region Value
        private Color cValue = Color.White;
        public Color Value
        {
            get => cValue;
            set
            {
                if (cValue != value)
                {
                    cValue = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region TextStyle
        private DvColorTextStyle eTextStyle = DvColorTextStyle.TEXT;
        public DvColorTextStyle TextStyle
        {
            get => eTextStyle;
            set
            {
                if (eTextStyle != value)
                {
                    eTextStyle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        bool bDown = false;
        DvColorPickerDialog dlg = new DvColorPickerDialog();
        Dictionary<int, List<string>> dicKnown = new Dictionary<int, List<string>>();
        #endregion

        #region Constructor
        public DvColorPicker()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 30);

            foreach (KnownColor kc in Enum.GetValues(typeof(KnownColor)))
            {
                Color known = Color.FromKnownColor(kc);
                if (!known.IsSystemColor)
                {
                    int k = known.ToArgb();
                    if (!dicKnown.ContainsKey(k)) dicKnown.Add(k, new List<string>());
                    dicKnown[k].Add(known.Name);
                }
            }
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            var rtButton = new Rectangle(rtContent.Right - ButtonWidth, rtContent.Y, ButtonWidth, rtContent.Height);
            var rtValue = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width - ButtonWidth, rtContent.Height);
            SetArea("rtButton", rtButton);
            SetArea("rtValue", rtValue);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var ButtonColor = UseThemeColor ? Theme.Color3 : this.ButtonColor;
            var ValueBoxColor = UseThemeColor ? Theme.Color2 : this.ButtonColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtTxt = Areas["rtValue"];
            var rtBtn = Areas["rtButton"];
            #endregion
            #region Init
            var p = new Pen(ButtonColor, 1);
            var br = new SolidBrush(ButtonColor);
            #endregion
            #region Draw
            int isz = rtBtn.Height / 4;
            #region Back
            Theme.DrawBox(e.Graphics, ValueBoxColor, BackColor, rtTxt, RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
            #endregion
            #region Button
            if (!bDown)
            {
                var ico = new DvIcon("fa-palette") { IconSize = isz };
                var cv = ButtonColor;
                var ct = ForeColor;
                Theme.DrawBox(e.Graphics, cv, BackColor, rtBtn, RoundType.R, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtBtn.X, rtBtn.Y + 0, rtBtn.Width, rtBtn.Height), DvContentAlignment.MiddleCenter);
            }
            else
            {
                var ico = new DvIcon("fa-palette") { IconSize = isz };
                var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                var ct = ForeColor.BrightnessTransmit(Theme.DownBright);
                Theme.DrawBox(e.Graphics, cv, BackColor, rtBtn, RoundType.R, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtBtn.X, rtBtn.Y + 1, rtBtn.Width, rtBtn.Height), DvContentAlignment.MiddleCenter);
            }
            #endregion
            #region Text
            #region String
            string str = "";
            if (TextStyle == DvColorTextStyle.ARGB)
            {
                str = "(" + Value.A + "," + Value.R + "," + Value.G + "," + Value.B + ")";
            }
            else if (TextStyle == DvColorTextStyle.TEXT)
            {
                var k = Value.ToArgb();
                if (dicKnown.ContainsKey(k)) str = dicKnown[k].First();
                else str = Value.Name.ToUpper();
            }
            else if (TextStyle == DvColorTextStyle.ALL)
            {
                string nm = "";
                var k = Value.ToArgb();
                if (dicKnown.ContainsKey(k)) nm = dicKnown[k].First();
                else nm = Value.Name.ToUpper();

                str = nm + " (" + Value.A + "," + Value.R + "," + Value.G + "," + Value.B + ")";
            }
            #endregion
            var sz = rtBtn.Height / 2;

            using (var bmp = new Bitmap(sz+1, sz+1))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

                    br.Color = Value; 
                    g.FillRectangle(br, new Rectangle(1, 1, sz - 1, sz - 1));

                    p.Width = 1;
                    p.Color = Color.Black;
                    g.DrawRectangle(p, new Rectangle(1, 1, sz - 1, sz - 1));
                }

                var ico = new DvIcon() { IconImage = bmp, Gap = 10 };
                var h = e.Graphics.MeasureTextIcon(ico, str, Font).Height / 2;
                rtTxt.Inflate(-Convert.ToInt32(h), 0);
                Theme.DrawTextShadow(e.Graphics, ico, str, Font, ForeColor, BoxColor, rtTxt, DvContentAlignment.MiddleLeft);
            }
            #endregion
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
            Focus();
            if (Areas.ContainsKey("rtButton") && CollisionTool.Check(Areas["rtButton"], e.Location))
            {
                bDown = true;
                Invalidate();
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (bDown)
            {
                var frm = this.FindForm() as DvForm;
                if (frm != null) frm.Block = true;

                bDown = false;
                var ret = dlg.ShowColorPicker("색상 선택", Value);
                if (ret.HasValue) Value = ret.Value;
                Invalidate();

                if (frm != null) frm.Block = false;

            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion
    }
    #region enum : DvColorTextStyle 
    public enum DvColorTextStyle { ARGB, TEXT, ALL }
    #endregion
}
