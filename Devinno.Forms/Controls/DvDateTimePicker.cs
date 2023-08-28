using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
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
    public class DvDateTimePicker : DvControl
    {
        #region Properties
        #region ValueColor
        private Color? cValueColor = null;
        public Color? ValueColor
        {
            get => cValueColor;
            set
            {
                if (cValueColor != value)
                {
                    cValueColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region SelectedValue
        private DateTime dtTime = DateTime.Now;
        public DateTime SelectedValue
        {
            get => dtTime;
            set
            {
                if (dtTime != value)
                {
                    dtTime = value;
                    Invalidate();
                    SelectedValueChanged?.Invoke(this, null);
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
        #region ButtonWidth
        private int? nButtonWidth = 30;
        public int? ButtonWidth
        {
            get => nButtonWidth;
            set
            {
                if (nButtonWidth != value)
                {
                    nButtonWidth = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region TitleColor
        private Color? cTitleColor = null;
        public Color? TitleColor
        {
            get => cTitleColor;
            set
            {
                if (cTitleColor != value)
                {
                    cTitleColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Title
        private TextIcon texticonTitle = new TextIcon();

        public DvIcon TitleIcon => texticonTitle.Icon;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap TitleIconImage
        {
            get => texticonTitle.IconImage;
            set { if (texticonTitle.IconImage != value) { texticonTitle.IconImage = value; Invalidate(); } }
        }
        public string TitleIconString
        {
            get => texticonTitle.IconString;
            set { if (texticonTitle.IconString != value) { texticonTitle.IconString = value; Invalidate(); } }
        }
        public float TitleIconSize
        {
            get => texticonTitle.IconSize;
            set { if (texticonTitle.IconSize != value) { texticonTitle.IconSize = value; Invalidate(); } }
        }
        public int TitleIconGap
        {
            get => texticonTitle.IconGap;
            set { if (texticonTitle.IconGap != value) { texticonTitle.IconGap = value; Invalidate(); } }
        }
        public DvTextIconAlignment TitleIconAlignment
        {
            get => texticonTitle.IconAlignment;
            set { if (texticonTitle.IconAlignment != value) { texticonTitle.IconAlignment = value; Invalidate(); } }
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Title
        {
            get => texticonTitle.Text;
            set { if (texticonTitle.Text != value) { base.Text = texticonTitle.Text = value; Invalidate(); } }
        }

        public Padding TitleTextPadding
        {
            get => texticonTitle.TextPadding;
            set { if (texticonTitle.TextPadding != value) { texticonTitle.TextPadding = value; Invalidate(); } }
        }
        #endregion
        #region TitleWidth
        private int? nTitleWidth = null;
        public int? TitleWidth
        {
            get => nTitleWidth;
            set
            {
                if (nTitleWidth != value)
                {
                    nTitleWidth = value;
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
                if (round != value)
                {
                    round = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region DateTimeType
        private DateTimePickerType eDateTimeType = DateTimePickerType.DateTime;
        public DateTimePickerType DateTimeType
        {
            get => eDateTimeType;
            set
            {
                if (eDateTimeType != value)
                {
                    eDateTimeType = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region ButtonDownState
        protected bool ButtonDownState { get; private set; }
        #endregion
        #endregion

        #region Member Variable
        private bool bValueDown = false;
        private DvDateTimePickerBox dlg => DvDialogs.DateTimeBox;
        #endregion

        #region Event
        public event EventHandler SelectedValueChanged;
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
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var TitleColor = this.TitleColor ?? Theme.ButtonColor;
            var ValueColor = this.ValueColor ?? Theme.InputColor;
            var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
            var TitleBorderColor = Theme.GetBorderColor(TitleColor, BackColor);
            var ValueBorderColor = Theme.GetBorderColor(ValueColor, BackColor);
            var ButtonBorderColor = Theme.GetBorderColor(ButtonColor, BackColor);
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent, rtTitle, rtValue, rtButton, rtValueAll) =>
            {
                var g = e.Graphics;

                var useT = TitleWidth.HasValue && TitleWidth.Value > 0;
                var useB = ButtonWidth.HasValue && ButtonWidth.Value > 0;

                #region Round
                var rndTitle = RoundType.Rect;
                var rndValue = RoundType.Rect;
                var rndButton = RoundType.Rect;

                switch (Round)
                {
                    #region Ellipse / Rect
                    case RoundType.Ellipse:
                    case RoundType.Rect:
                        rndTitle = rndValue = rndButton = RoundType.Rect;
                        break;
                    #endregion

                    #region L / T / R / B
                    case RoundType.L:
                        if (useT && useB) { rndTitle = RoundType.L; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useT) { rndTitle = RoundType.L; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useB) { rndTitle = RoundType.Rect; rndValue = RoundType.L; rndButton = RoundType.Rect; }
                        else { rndTitle = RoundType.Rect; rndValue = RoundType.L; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.R:
                        if (useT && useB) { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.R; }
                        else if (useT) { rndTitle = RoundType.Rect; rndValue = RoundType.R; rndButton = RoundType.Rect; }
                        else if (useB) { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.R; }
                        else { rndTitle = RoundType.Rect; rndValue = RoundType.R; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.T:
                        if (useT && useB) { rndTitle = RoundType.LT; rndValue = RoundType.Rect; rndButton = RoundType.RT; }
                        else if (useT) { rndTitle = RoundType.LT; rndValue = RoundType.RT; rndButton = RoundType.Rect; }
                        else if (useB) { rndTitle = RoundType.Rect; rndValue = RoundType.LT; rndButton = RoundType.RT; }
                        else { rndTitle = RoundType.Rect; rndValue = RoundType.T; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.B:
                        if (useT && useB) { rndTitle = RoundType.LB; rndValue = RoundType.Rect; rndButton = RoundType.RB; }
                        else if (useT) { rndTitle = RoundType.LB; rndValue = RoundType.RB; rndButton = RoundType.Rect; }
                        else if (useB) { rndTitle = RoundType.Rect; rndValue = RoundType.LB; rndButton = RoundType.RB; }
                        else { rndTitle = RoundType.Rect; rndValue = RoundType.B; rndButton = RoundType.Rect; }
                        break;
                    #endregion

                    #region LT / RT / LB / RB
                    case RoundType.LT:
                        if (useT && useB) { rndTitle = RoundType.LT; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useT) { rndTitle = RoundType.LT; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useB) { rndTitle = RoundType.Rect; rndValue = RoundType.LT; rndButton = RoundType.Rect; }
                        else { rndTitle = RoundType.Rect; rndValue = RoundType.LT; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.RT:
                        if (useT && useB) { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.RT; }
                        else if (useT) { rndTitle = RoundType.Rect; rndValue = RoundType.RT; rndButton = RoundType.Rect; }
                        else if (useB) { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.RT; }
                        else { rndTitle = RoundType.Rect; rndValue = RoundType.RT; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.LB:
                        if (useT && useB) { rndTitle = RoundType.LB; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useT) { rndTitle = RoundType.LB; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useB) { rndTitle = RoundType.Rect; rndValue = RoundType.LB; rndButton = RoundType.Rect; }
                        else { rndTitle = RoundType.Rect; rndValue = RoundType.LB; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.RB:
                        if (useT && useB) { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.RB; }
                        else if (useT) { rndTitle = RoundType.Rect; rndValue = RoundType.RB; rndButton = RoundType.Rect; }
                        else if (useB) { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.RB; }
                        else { rndTitle = RoundType.Rect; rndValue = RoundType.RB; rndButton = RoundType.Rect; }
                        break;
                    #endregion

                    #region All
                    case RoundType.All:
                        if (useT && useB) { rndTitle = RoundType.L; rndValue = RoundType.Rect; rndButton = RoundType.R; }
                        else if (useT) { rndTitle = RoundType.L; rndValue = RoundType.R; rndButton = RoundType.Rect; }
                        else if (useB) { rndTitle = RoundType.Rect; rndValue = RoundType.L; rndButton = RoundType.R; }
                        else { rndTitle = RoundType.Rect; rndValue = RoundType.All; rndButton = RoundType.Rect; }
                        break;
                        #endregion
                }
                #endregion
                #region Value
                {
                    var cV = ValueColor;
                    var cB = ValueBorderColor;
                    var cT = ForeColor;

                    var s = "";
                    switch (DateTimeType)
                    {
                        case DateTimePickerType.Date: s = dtTime.ToString("yyyy.MM.dd"); break;
                        case DateTimePickerType.Time: s = dtTime.ToString("HH:mm:ss"); break;
                        case DateTimePickerType.DateTime: s = dtTime.ToString("yyyy.MM.dd HH:mm:ss"); break;
                    }

                    Theme.DrawBox(g, rtValueAll, cV, cB, rndValue, Box.LabelBox(Embossing.FlatConcave, ShadowGap));

                    e.Graphics.SetClip(rtValue);

                    Theme.DrawText(e.Graphics, s, Font, cT, rtValue);
                   
                    e.Graphics.ResetClip();
                }
                #endregion
                #region Title
                if (useT)
                {
                    var cV = TitleColor;
                    var cB = TitleBorderColor;
                    var cT = ForeColor;

                    Theme.DrawBox(g, rtTitle, TitleColor, TitleBorderColor, rndTitle, Box.ButtonUp_Flat(ShadowGap));
                    Theme.DrawTextIcon(g, texticonTitle, Font, cT, rtTitle);
                }
                #endregion
                #region Button
                if (useB)
                {
                    var cV = ButtonDownState ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                    var cB = ButtonDownState ? ButtonBorderColor.BrightnessTransmit(Theme.DownBrightness) : ButtonBorderColor;
                    var cT = ButtonDownState ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;

                    if (ButtonDownState) rtButton.Offset(0, 1);

                    var ico = "fa-calendar-check";
                    if (DateTimeType == DateTimePickerType.Time) ico = "fa-clock";

                    Theme.DrawBox(g, rtButton, cV, cB, rndButton, !ButtonDownState ? Box.ButtonUp_V(true, ShadowGap) : Box.ButtonDown(ShadowGap));
                    Theme.DrawIcon(g, new DvIcon(ico, 12), cT, rtButton);
                }
                #endregion
            });

            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtTitle, rtValue, rtButton, rtValueAll) =>
            {
                if (CollisionTool.Check(rtValueAll, e.Location)) bValueDown = true;
                if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, e.Location)) ButtonDownState = true;
            });

            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtTitle, rtValue, rtButton, rtValueAll) =>
            {
                if (bValueDown)
                {
                    bValueDown = false;
                }

                if (ButtonDownState)
                {
                    ButtonDownState = false;
                    if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, e.Location))
                    {
                        var frm = FindForm() as DvForm;

                        if (frm != null) frm.Block = true;

                        switch (DateTimeType)
                        {
                            case DateTimePickerType.DateTime:
                                {
                                    var c = dlg.ShowDateTimePicker(Title ?? "날짜/시간 선택", SelectedValue);
                                    if (c.HasValue) SelectedValue = c.Value;
                                }
                                break;

                            case DateTimePickerType.Date:
                                {
                                    var c = dlg.ShowDatePicker(Title ?? "날짜 선택", SelectedValue);
                                    if (c.HasValue) SelectedValue = c.Value;
                                }
                                break;

                            case DateTimePickerType.Time:
                                {
                                    var c = dlg.ShowTimePicker(Title ?? "시간 선택", SelectedValue);
                                    if (c.HasValue) SelectedValue = c.Value;
                                }
                                break;
                        }
                        if (frm != null) frm.Block = false;
                    }
                }
            });

            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        public void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();

            var szTitleW = (TitleWidth.HasValue && TitleWidth.Value > 0) ? TitleWidth.Value : 0;
            var szButonW = (ButtonWidth.HasValue && ButtonWidth.Value > 0) ? ButtonWidth.Value : 0;

            var rts = Util.DevideSizeH(rtContent, new SizeInfo[] { new SizeInfo(DvSizeMode.Pixel, szTitleW), new SizeInfo(DvSizeMode.Percent, 100F), new SizeInfo(DvSizeMode.Pixel, szButonW) }.ToList());

            var rtTitle = rts[0];
            var rtValue = rts[1];
            var rtButton = rts[2];
            var rtValueAll = new RectangleF(rtValue.Left, rtValue.Top, rtButton.Left - rtValue.Left, rtValue.Height);

            act(rtContent, rtTitle, rtValue, rtButton, rtValueAll);
        }
        #endregion
        #endregion
    }
}
