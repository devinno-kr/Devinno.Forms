using Devinno.Extensions;
using Devinno.Forms.Dialogs;
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
    public class DvDateTimePicker : DvControl
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
        private DateTime dtValue = DateTime.Now;
        public DateTime Value
        {
            get => dtValue;
            set
            {
                if (dtValue != value)
                {
                    dtValue = value;
                    Invalidate();
                }
            }
        }
        #endregion
        
        #region DateTimeStyle
        private DvDateTimePickerStyle eDateTimeStyle = DvDateTimePickerStyle.DateTime;
        public DvDateTimePickerStyle DateTimeStyle
        {
            get => eDateTimeStyle;
            set
            {
                if (eDateTimeStyle != value)
                {
                    eDateTimeStyle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        bool bDown = false;
        DvDateTimePickerDialog dlg = new DvDateTimePickerDialog();
        #endregion

        #region Constructor
        public DvDateTimePicker()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 30);
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
                var ico = new DvIcon("far fa-calendar-alt") { IconSize = isz };
                var cv = ButtonColor;
                var ct = ForeColor;
                Theme.DrawBox(e.Graphics, cv, BackColor, rtBtn, RoundType.R, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtBtn.X, rtBtn.Y + 0, rtBtn.Width, rtBtn.Height), DvContentAlignment.MiddleCenter);
            }
            else
            {
                var ico = new DvIcon("far fa-calendar-alt") { IconSize = isz };
                var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                var ct = ForeColor.BrightnessTransmit(Theme.DownBright);
                Theme.DrawBox(e.Graphics, cv, BackColor, rtBtn, RoundType.R, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtBtn.X, rtBtn.Y + 1, rtBtn.Width, rtBtn.Height), DvContentAlignment.MiddleCenter);
            }
            #endregion
            #region Text
            #region String
            string str = "";
            if (DateTimeStyle == DvDateTimePickerStyle.DateTime)
            {
                str = Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (DateTimeStyle == DvDateTimePickerStyle.Time)
            {
                str = Value.ToString("HH:mm:ss");
            }
            else if (DateTimeStyle == DvDateTimePickerStyle.Date)
            {
                str = Value.ToString("yyyy-MM-dd");
            }
            #endregion
            var sz = rtBtn.Height / 2;

            Theme.DrawTextShadow(e.Graphics, null, str, Font, ForeColor, BoxColor, rtTxt, DvContentAlignment.MiddleCenter);
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
                bDown = false;
                if (DateTimeStyle == DvDateTimePickerStyle.DateTime)
                {
                    var frm = this.FindForm() as DvForm;
                    if (frm != null) frm.Block = true;

                    var ret = dlg.ShowDateTimePicker("날짜/시간 입력", Value);
                    if (ret.HasValue) Value = ret.Value;

                    if (frm != null) frm.Block = false;
                }
                else if (DateTimeStyle == DvDateTimePickerStyle.Date)
                {
                    var frm = this.FindForm() as DvForm;
                    if (frm != null) frm.Block = true;

                    var ret = dlg.ShowDatePicker("날짜 입력", Value);
                    if (ret.HasValue) Value = ret.Value;

                    if (frm != null) frm.Block = false;
                }
                else if (DateTimeStyle == DvDateTimePickerStyle.Time)
                {
                    var frm = this.FindForm() as DvForm;
                    if (frm != null) frm.Block = true;

                    var ret = dlg.ShowTimePicker("시간 입력", Value);
                    if (ret.HasValue) Value = ret.Value;

                    if (frm != null) frm.Block = false;
                }
                Invalidate();
            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion
    }

    #region enum : DvDateTimePickerStyle 
    public enum DvDateTimePickerStyle { DateTime, Date, Time }
    #endregion
}
