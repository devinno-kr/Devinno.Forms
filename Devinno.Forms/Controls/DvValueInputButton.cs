using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
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
    public class DvValueInputButton : DvControl
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
        #region Value
        public string Value
        {
            get { return OriginalTextBox.Text; }
            set { if (OriginalTextBox.Text != value) { OriginalTextBox.Text = value; Invalidate(); } }
        }
        #endregion
        #region TitleWidth
        int nTitleWidth = 60;
        public int TitleWidth
        {
            get { return nTitleWidth; }
            set { if (nTitleWidth != value) { nTitleWidth = value; Invalidate(); } }
        }
        #endregion
        #region TitleBoxColor
        private Color cTitleColor = DvTheme.DefaultTheme.Color3;
        public Color TitleBoxColor
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
        #region ValueBoxColor
        private Color cValueColor = DvTheme.DefaultTheme.Color1;
        public Color ValueBoxColor
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
        #region BorderColor
        private Color cBorderColor = Color.Red;
        public Color BorderColor
        {
            get => cBorderColor;
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
        #region ItemColor
        private Color cItemColor = DvTheme.DefaultTheme.Color2;
        public Color ItemColor
        {
            get => cItemColor;
            set
            {
                if (cItemColor != value)
                {
                    cItemColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region SelectedItemColor
        private Color cSelectedItemColor = DvTheme.DefaultTheme.PointColor;
        public Color SelectedItemColor
        {
            get => cSelectedItemColor;
            set
            {
                if (cSelectedItemColor != value)
                {
                    cSelectedItemColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Unit
        private string strUnit = "";
        public string Unit
        {
            get => strUnit;
            set
            {
                if (strUnit != value)
                {
                    strUnit = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region UnitWidth
        private int nUnitWidth = 36;
        public int UnitWidth
        {
            get => nUnitWidth;
            set
            {
                if (nUnitWidth != value)
                {
                    nUnitWidth = value;
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
        #region InputStyle
        private DvInputType eInputStyle = DvInputType.TEXT;
        public DvInputType InputStyle
        {
            get
            {
                return eInputStyle;
            }
            set
            {
                if (eInputStyle != value)
                {
                    eInputStyle = value;
                    OriginalTextBox.Visible = (value != DvInputType.COMBO && value != DvInputType.BOOL);
                    Invalidate();
                }
            }
        }
        #endregion
        #region MinusInput
        private bool bMinusInput = false;
        public bool MinusInput
        {
            get => bMinusInput;
            set => bMinusInput = value;
        }
        #endregion
        #region OriginalTextBox
        public TextBox OriginalTextBox { get; private set; }
        #endregion
        #region TextPadding
        private Padding padText = new Padding(0, 0, 0, 0);
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

        #region OnOFf
        private bool bOnOff = false;
        public bool OnOff
        {
            get => bOnOff;
            set
            {
                if (bOnOff != value)
                {
                    bOnOff = value;
                    OnOffChanged?.Invoke(this, null);
                    Invalidate();
                }
            }

        }
        #endregion

        #region TouchMode
        private bool bTouchMode = false;
        public bool TouchMode
        {
            get => bTouchMode;
            set
            {
                if (bTouchMode != value)
                {
                    bTouchMode = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region MaximumViewCount
        int nMaximumViewCount = 10;
        public int MaximumViewCount
        {
            get { return nMaximumViewCount; }
            set { nMaximumViewCount = value; }
        }
        #endregion
        #region ItemHeight
        private int nItemHeight = 30;
        public int ItemHeight
        {
            get => nItemHeight;
            set
            {
                if (nItemHeight != value)
                {
                    nItemHeight = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Items
        public List<ComboBoxItem> Items { get; } = new List<ComboBoxItem>();
        #endregion
        #region ItemPadding
        private Padding padItem = new Padding(0, 0, 0, 0);
        public Padding ItemPadding
        {
            get => padItem;
            set
            {
                if (padItem != value)
                {
                    padItem = value;
                    Invalidate();
                }
            }
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
        #region SelectedIndex
        private int nSelectIndex = -1;
        public int SelectedIndex
        {
            get { return nSelectIndex; }
            set
            {
                if (nSelectIndex != value)
                {
                    int old = nSelectIndex;

                    nSelectIndex = value;
                    if (Items == null || Items.Count == 0) nSelectIndex = -1;
                    else if (nSelectIndex < 0 && nSelectIndex >= Items.Count) nSelectIndex = -1;

                    if (SelectedIndexChanged != null && old != nSelectIndex)
                    {
                        SelectedIndexChanged.Invoke(this, null);
                    }
                    Invalidate();
                }
            }
        }
        #endregion
        #region ComboButtonWidth
        int nComboButtonWidth = 60;
        public int ComboButtonWidth
        {
            get { return nComboButtonWidth; }
            set { if (nComboButtonWidth != value) { nComboButtonWidth = value; Invalidate(); } }
        }
        #endregion
        #region OnText
        private string strOnText = "ON";
        public string OnText
        {
            get => strOnText;
            set { if (strOnText != value) { strOnText = value; Invalidate(); } }
        }
        #endregion
        #region OffText
        private string strOffText = "OFF";
        public string OffText
        {
            get => strOffText;
            set { if (strOffText != value) { strOffText = value; Invalidate(); } }
        }
        #endregion
        #region DrawBorder
        private bool bDrawBorder = false;
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

        #region ButtonIcon
        private DvIcon icobtn = new DvIcon();

        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        [Category("- 아이콘")]
        public Bitmap ButtonIconImage
        {
            get => icobtn.IconImage;
            set { if (icobtn.IconImage != value) { icobtn.IconImage = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public string ButtonIconString
        {
            get => icobtn.IconString;
            set { if (icobtn.IconString != value) { icobtn.IconString = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public int ButtonIconGap
        {
            get => icobtn.Gap;
            set { if (icobtn.Gap != value) { icobtn.Gap = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public DvTextIconAlignment ButtonIconAlignment
        {
            get => icobtn.Alignment;
            set { if (icobtn.Alignment != value) { icobtn.Alignment = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public float ButtonIconSize
        {
            get => icobtn.IconSize;
            set { if (icobtn.IconSize != value) { icobtn.IconSize = value; Invalidate(); } }
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
        #region ButtonText
        string strButtonText = "Button";
        public string ButtonText
        {
            get { return strButtonText; }
            set { if (strButtonText != value) { strButtonText = value; Invalidate(); } }
        }
        #endregion

        public bool UseLongClick { get => click.UseLongClick; set => click.UseLongClick = value; }
        public int LongClickTime { get => click.LongClickTime; set => click.LongClickTime = value; }
        #endregion

        #region Member Variable
        bool bDownOn = false, bDownOff = false;
        bool bDownCombo = false;

        bool bDown = false;
        private LongClick click = new LongClick();
        #endregion

        #region Constructor
        public DvValueInputButton()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            #region TextBox
            OriginalTextBox = new TextBox();
            OriginalTextBox.Location = new System.Drawing.Point(0, 0);
            OriginalTextBox.Name = "OriginalTextBox";
            OriginalTextBox.Size = new System.Drawing.Size(60, 28);
            OriginalTextBox.BorderStyle = BorderStyle.None;
            OriginalTextBox.TabIndex = 0;
            OriginalTextBox.TextAlign = HorizontalAlignment.Center;
            Controls.Add(OriginalTextBox);

            OriginalTextBox.TextChanged += (o, s) => { TextChange(); };
            OriginalTextBox.GotFocus += (o, s) => { Invalidate(); };
            OriginalTextBox.LostFocus += (o, s) => { Invalidate(); };
            #endregion

            Size = new Size(150, 30);
        }
        #endregion

        #region Event
        public event EventHandler SelectedIndexChanged;
        public event EventHandler OnOffChanged;
        public event EventHandler ValueTextChanged;
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);
            var rtContent = Areas["rtContent"];

            var rtTitle = new Rectangle(rtContent.X, rtContent.Y, TitleWidth, rtContent.Height);
            SetArea("rtTitle", rtTitle);

            var rtValueAll = new Rectangle(rtContent.X + TitleWidth, rtContent.Y, rtContent.Width - TitleWidth - ButtonWidth, rtContent.Height);
            SetArea("rtValueAll", rtValueAll);

            var rtButton = new Rectangle(rtContent.Right - ButtonWidth, rtContent.Y, ButtonWidth, rtContent.Height);
            SetArea("rtButton", rtButton);

            var szUnitW = 0;
            if (!string.IsNullOrWhiteSpace(Unit)) szUnitW = UnitWidth;
            var rtUnit = new Rectangle(rtValueAll.Right - szUnitW, rtValueAll.Y, szUnitW, rtValueAll.Height);
            SetArea("rtUnit", rtUnit);

            var rtValue = new Rectangle(rtValueAll.X, rtValueAll.Y, rtValueAll.Width - szUnitW, rtValueAll.Height);
            SetArea("rtValue", rtValue);

            var rtOn = new Rectangle(rtValueAll.X, rtValueAll.Y, rtValueAll.Width / 2, rtValueAll.Height);
            SetArea("rtOn", rtOn);

            var rtOff = new Rectangle(rtOn.Right, rtValueAll.Y, rtContent.Right - rtOn.Right, rtValueAll.Height);
            SetArea("rtOff", rtOff);

            var rtIco = new Rectangle(rtValueAll.Right - ButtonWidth, rtValueAll.Y, ButtonWidth, rtValueAll.Height);
            SetArea("rtIco", rtIco);

            var rtComboValue = new Rectangle(rtValue.X, rtValue.Y, rtValue.Width - rtIco.Width, rtValue.Height);
            SetArea("rtComboValue", rtComboValue);
        }
        #endregion

        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var ValueColor = UseThemeColor ? Theme.Color1 : this.ValueBoxColor;
            var ItemColor = UseThemeColor ? Theme.Color2 : this.ItemColor;
            var SelectedItemColor = UseThemeColor ? Theme.PointColor : this.SelectedItemColor;
            var TitleColor = UseThemeColor ? Theme.Color3 : this.TitleBoxColor;
            #endregion
            #region Set
            var Wnd = this.FindForm() as DvForm;

            if (OriginalTextBox.ForeColor != ForeColor) OriginalTextBox.ForeColor = ForeColor;
            if (OriginalTextBox.ForeColor != ValueBoxColor) OriginalTextBox.BackColor = ValueBoxColor;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtValue = Areas["rtValue"];
            var rtValueAll = Areas["rtValueAll"];
            var rtTitle = Areas["rtTitle"];
            var rtUnit = Areas["rtUnit"];
            var rtOff = Areas["rtOff"];
            var rtOn = Areas["rtOn"];
            var rtIco = Areas["rtIco"];
            var rtComboValue = Areas["rtComboValue"];
            var rtButton = Areas["rtButton"];
            #endregion
            #region Init
            var p = new Pen(ValueColor, 1);
            var br = new SolidBrush(ValueColor);
            #endregion
            #region Draw
            switch (InputStyle)
            {
                #region NUMBER / TEXT / HEX / FLOATING / ENG_NUM 
                case DvInputType.NUMBER:
                case DvInputType.ENG_NUM:
                case DvInputType.FLOATING:
                case DvInputType.TEXT:
                case DvInputType.HEX:
                    {
                        Center(e.Graphics, Wnd);
                        #region TitleBox
                        Theme.DrawBox(e.Graphics, TitleColor, BackColor, rtTitle, RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
                        #endregion
                        #region ValueBox
                        var Style = DvLabelStyle.FlatConcave;
                        switch (Style)
                        {
                            case DvLabelStyle.FlatConcave:
                                Theme.DrawBox(e.Graphics, ValueColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL);
                                break;
                            case DvLabelStyle.FlatConvex:
                                Theme.DrawBox(e.Graphics, ValueColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                                break;
                            case DvLabelStyle.Concave:
                                Theme.DrawBox(e.Graphics, ValueColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL | BoxDrawOption.IN_SHADOW);
                                break;
                            case DvLabelStyle.Convex:
                                Theme.DrawBox(e.Graphics, ValueColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW | BoxDrawOption.IN_BEVEL_LT);
                                break;
                        }
                        #endregion
                        #region Text
                        Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor, TitleColor, rtTitle);
                        Theme.DrawTextShadow(e.Graphics, null, Value, Font, ForeColor, ValueColor, rtValue);
                        #endregion
                        #region Unit
                        if (UnitWidth > 0 && !string.IsNullOrWhiteSpace(Unit))
                        {
                            #region Unit Sep
                            var szh = Convert.ToInt32(rtUnit.Height / 2);

                            p.Width = 1;

                            p.Color = ValueColor.BrightnessTransmit(-Theme.BorderBright);
                            e.Graphics.DrawLine(p, rtUnit.X + 1, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtUnit.X + 1, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);

                            p.Color = ValueColor.BrightnessTransmit(Theme.BorderBright);
                            e.Graphics.DrawLine(p, rtUnit.X, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtUnit.X, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);
                            #endregion
                            Theme.DrawTextShadow(e.Graphics, null, Unit, Font, ForeColor, ValueColor, rtUnit);
                        }
                        #endregion
                    }
                    break;
                #endregion
                #region COMBO
                case DvInputType.COMBO:
                    {
                        #region TitleBox
                        Theme.DrawBox(e.Graphics, TitleColor, BackColor, rtTitle, RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);

                        Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor, TitleColor, rtTitle);
                        #endregion
                        #region ValueBox
                        if (DropState == DvDropState.Dropped || DropState == DvDropState.Dropping)
                        {
                            var vrt = this.RectangleToScreen(rtContent);
                            if (dropContainer != null && dropContainer.Top <= vrt.Top)
                                Theme.DrawBox(e.Graphics, ItemColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_BEVEL);
                            else
                                Theme.DrawBox(e.Graphics, ItemColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_BEVEL);
                        }
                        else Theme.DrawBox(e.Graphics, ItemColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);

                        if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                        {
                            var v = Items[nSelectIndex];
                            Theme.DrawTextShadow(e.Graphics, v.Icon, v.Text, Font, ForeColor, ItemColor, rtComboValue, ContentAlignment);
                        }
                        #endregion
                        #region Icon
                        var nisz = rtIco.Height / 4;
                        if (DropState == DvDropState.Dropped || DropState == DvDropState.Dropping)
                        {
                            Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-chevron-up") { IconSize = nisz, Gap = 0 }, "", Font, ForeColor, ItemColor, rtIco);
                        }
                        else
                        {
                            Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-chevron-down") { IconSize = nisz, Gap = 0 }, "", Font, ForeColor, ItemColor, rtIco);
                        }
                        #endregion
                        #region Seperate
                        var szh = Convert.ToInt32(rtIco.Height / 2);

                        p.Width = 1;

                        p.Color = ItemColor.BrightnessTransmit(Theme.OutBevelBright);
                        e.Graphics.DrawLine(p, rtIco.X, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtIco.X, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);

                        p.Color = ItemColor.BrightnessTransmit(Theme.BorderBright);
                        e.Graphics.DrawLine(p, rtIco.X - 1, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtIco.X - 1, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);
                        #endregion
                    }
                    break;
                #endregion
                #region BOOL
                case DvInputType.BOOL:
                    {
                        #region TitleBox
                        Theme.DrawBox(e.Graphics, TitleColor, BackColor, rtTitle, RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
                        Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor, TitleColor, rtTitle);
                        #endregion
                        #region ValueBox
                        var cOnBG = OnOff ? SelectedItemColor : ItemColor;
                        var cOffBG = !OnOff ? SelectedItemColor : ItemColor;
                        var cOnTxt = OnOff ? ForeColor : ForeColor.BrightnessTransmit(Theme.BorderBright);
                        var cOffTxt = !OnOff ? ForeColor : ForeColor.BrightnessTransmit(Theme.BorderBright);

                        if (!bDownOn)
                        {
                            Theme.DrawBox(e.Graphics, cOnBG, BackColor, rtOn, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
                            Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-check") { IconSize = 12, Gap = 5 }, OnText, Font, cOnTxt, cOnBG, rtOn);

                        }
                        else
                        {
                            Theme.DrawBox(e.Graphics, cOnBG.BrightnessTransmit(Theme.DownBright), BackColor, rtOn, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.OUT_BEVEL);
                            Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-check") { IconSize = 12, Gap = 5 }, OnText, Font, cOnTxt.BrightnessTransmit(Theme.DownBright), cOnBG, new Rectangle(rtOn.X, rtOn.Y + Theme.ShadowGap, rtOn.Width, rtOn.Height));
                        }

                        if (!bDownOff)
                        {
                            Theme.DrawBox(e.Graphics, cOffBG, BackColor, rtOff, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);
                            Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-times") { IconSize = 12, Gap = 5 }, OffText, Font, cOffTxt, cOffBG, rtOff);
                        }
                        else
                        {
                            Theme.DrawBox(e.Graphics, cOffBG.BrightnessTransmit(Theme.DownBright), BackColor, rtOff, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.OUT_BEVEL);
                            Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-times") { IconSize = 12, Gap = 5 }, OffText, Font, cOffTxt.BrightnessTransmit(Theme.DownBright), cOffBG, new Rectangle(rtOff.X, rtOff.Y + Theme.ShadowGap, rtOff.Width, rtOff.Height));
                        }
                        #endregion
                    }
                    break;
                #endregion
                default: break;
            }

            if (!bDown)
            {
                var cv = ButtonColor;
                Theme.DrawBox(e.Graphics, cv, BackColor, rtButton, RoundType.R, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                Theme.DrawTextShadow(e.Graphics, icobtn, ButtonText, Font, ForeColor, cv, new Rectangle(rtButton.X, rtButton.Y + 0, rtButton.Width, rtButton.Height), DvContentAlignment.MiddleCenter);
            }
            else
            {
                var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                Theme.DrawBox(e.Graphics, cv, BackColor, rtButton, RoundType.R, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                Theme.DrawTextShadow(e.Graphics, icobtn, ButtonText, Font, ForeColor.BrightnessTransmit(Theme.DownBright), cv, new Rectangle(rtButton.X, rtButton.Y + 1, rtButton.Width, rtButton.Height), DvContentAlignment.MiddleCenter);
            }


            if (DrawBorder)
            {
                Theme.DrawBorder(e.Graphics, BorderColor, BackColor, 1, rtContent, RoundType.ALL, BoxDrawOption.BORDER);
            }
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnGotFocus
        protected override void OnGotFocus(EventArgs e)
        {
            OriginalTextBox.Focus();
            Invalidate();
            base.OnGotFocus(e);
        }
        #endregion
        #region OnResize
        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }
        #endregion
        #region OnSizeChanged
        protected override void OnSizeChanged(EventArgs e)
        {
            Invalidate();
            base.OnSizeChanged(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Areas.Count > 1)
            {
                this.Focus();
                OriginalTextBox.Focus();

                if (InputStyle == DvInputType.BOOL)
                {
                    if (CollisionTool.Check(Areas["rtOff"], e.Location)) bDownOff = true;
                    if (CollisionTool.Check(Areas["rtOn"], e.Location)) bDownOn = true;
                }
                else if (InputStyle == DvInputType.COMBO)
                {
                    if (CollisionTool.Check(Areas["rtValue"], e.Location)) bDownCombo = true;
                }

                Invalidate();
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Areas.Count > 1)
            {
                if (InputStyle == DvInputType.BOOL)
                {
                    if (bDownOn)
                    {
                        if (CollisionTool.Check(Areas["rtOn"], e.Location)) OnOff = true;
                        bDownOn = false;
                    }

                    if (bDownOff)
                    {
                        if (CollisionTool.Check(Areas["rtOff"], e.Location)) OnOff = false;
                        bDownOff = false;
                    }
                }
                else if (InputStyle == DvInputType.COMBO)
                {
                    if (bDownCombo)
                    {
                        if (CollisionTool.Check(Areas["rtValue"], e.Location) && Items != null && Items.Count > 0) OpenDropDown();
                        bDownCombo = false;
                    }
                }
                Invalidate();
            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region TextChange
        void TextChange()
        {
            var textbox = OriginalTextBox;
            if (InputStyle == DvInputType.NUMBER)
            {
                #region Number
                Int32 selectionStart = textbox.SelectionStart;
                Int32 selectionLength = textbox.SelectionLength;

                String newText = String.Empty;
                foreach (Char c in textbox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || (newText.Length == 0 && (c == '-' || c == '+') && MinusInput)) newText += c;
                }
                textbox.Text = newText;
                textbox.SelectionStart = selectionStart <= textbox.Text.Length ? selectionStart : textbox.Text.Length;
                #endregion
            }
            else if (InputStyle == DvInputType.FLOATING)
            {
                #region Floating
                Int32 selectionStart = textbox.SelectionStart;
                Int32 selectionLength = textbox.SelectionLength;

                String newText = String.Empty;
                bool bComma = false;
                foreach (Char c in textbox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && !bComma && textbox.Text != ".") || (newText.Length == 0 && (c == '-' || c == '+') && MinusInput)) newText += c;
                    if (c == '.' && textbox.Text != ".") bComma = true;
                }
                textbox.Text = newText;
                textbox.SelectionStart = selectionStart <= textbox.Text.Length ? selectionStart : textbox.Text.Length;
                #endregion
            }
            else if (InputStyle == DvInputType.HEX)
            {
                #region Hex
                Int32 selectionStart = textbox.SelectionStart;
                Int32 selectionLength = textbox.SelectionLength;

                String newText = String.Empty;
                foreach (Char c in textbox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || ((int)Char.ToUpper(c) >= (int)'A' && (int)Char.ToUpper(c) <= (int)'F')) newText += c;
                }
                textbox.Text = newText;
                textbox.SelectionStart = selectionStart <= textbox.Text.Length ? selectionStart : textbox.Text.Length;
                #endregion
            }
            else if (InputStyle == DvInputType.ENG_NUM)
            {
                #region Eng / Num
                Int32 selectionStart = textbox.SelectionStart;
                Int32 selectionLength = textbox.SelectionLength;

                String newText = String.Empty;
                foreach (Char c in textbox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || ((int)Char.ToUpper(c) >= (int)'A' && (int)Char.ToUpper(c) <= (int)'Z')) newText += c;
                }
                textbox.Text = newText;
                textbox.SelectionStart = selectionStart <= textbox.Text.Length ? selectionStart : textbox.Text.Length;
                #endregion
            }

            ValueTextChanged?.Invoke(this, null);
        }
        #endregion
        #region Center
        private void Center(Graphics g, DvForm Wnd)
        {
            if (Areas.Count > 1)
            {
                #region Bounds
                var szUnitW = 0;
                if (!string.IsNullOrWhiteSpace(Unit)) szUnitW = UnitWidth;

                var rtContent = Areas["rtContent"];
                var rtValueAll = Areas["rtValueAll"];
                var rtUnit = Areas["rtUnit"];
                var rtValue = Areas["rtValue"];
                #endregion
                #region Font
                if (DesignMode)
                {
                    OriginalTextBox.Font = this.Font;
                }
                else
                {
                    var ftsz = Convert.ToSingle(this.Font.Size * DpiRatio);
                    if (OriginalTextBox.Font.Name != this.Font.Name || OriginalTextBox.Font.Style != this.Font.Style || OriginalTextBox.Font.Size != ftsz)
                    {
                        //var old = OriginalTextBox.Font;
                        OriginalTextBox.Font = new Font(this.Font.FontFamily, ftsz, this.Font.Style);
                        //old.Dispose();
                    }
                }
                #endregion
                #region Set
                OriginalTextBox.Size = rtValue.Size;
                var rttb = MathTool.MakeRectangle(rtValue, OriginalTextBox.Size);
                OriginalTextBox.Location = new Point(rttb.X + 0, rttb.Y + 0);
                OriginalTextBox.Visible = this.Enabled && (Wnd == null || (Wnd != null && !Wnd.Block));
                #endregion
            }
        }
        #endregion
        #endregion

        #region DropDown
        #region Member Variable
        private bool closedWhileInControl;
        private DropDownContainer dropContainer;
        #endregion

        #region Properties
        #region CanDrop
        protected virtual bool CanDrop
        {
            get
            {
                if (dropContainer != null)
                    return false;

                if (dropContainer == null && closedWhileInControl)
                {
                    closedWhileInControl = false;
                    return false;
                }

                return !closedWhileInControl;
            }
        }
        #endregion
        #region DropState
        public DvDropState DropState { get; private set; }
        #endregion
        #endregion

        #region Method
        #region FreezeDropDown
        internal void FreezeDropDown(bool remainVisible)
        {
            if (dropContainer != null)
            {
                dropContainer.Freeze = true;
                if (!remainVisible)
                    dropContainer.Visible = false;
            }
        }
        #endregion
        #region UnFreezeDropDown
        internal void UnFreezeDropDown()
        {
            if (dropContainer != null)
            {
                dropContainer.Freeze = false;
                if (!dropContainer.Visible)
                    dropContainer.Visible = true;
            }
        }
        #endregion
        #region OpenDropDown
        private void OpenDropDown()
        {
            this.Move += (o, s) => { if (dropContainer != null) dropContainer.Bounds = GetDropDownBounds(); };

            var vpos = SelectedIndex == -1 ? 0 : SelectedIndex * ItemHeight;
            vpos = (int)MathTool.Constrain(vpos - (ItemHeight * 2), 0, (Items.Count * ItemHeight));

            dropContainer = new DropDownContainer(this);
            dropContainer.Bounds = GetDropDownBounds();
            dropContainer.DropStateChanged += (o, s) => { DropState = s.DropState; };
            dropContainer.FormClosed += (o, s) =>
            {
                if (!dropContainer.IsDisposed) dropContainer.Dispose();
                dropContainer = null;
                closedWhileInControl = (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position));
                DropState = DvDropState.Closed;
                this.Invalidate();
            };
            DropState = DvDropState.Dropping;
            dropContainer.VScrollPosition = vpos;
            dropContainer.Show(this);
            DropState = DvDropState.Dropped;
            this.Invalidate();
        }
        #endregion
        #region GetDropDownBounds
        private Rectangle GetDropDownBounds()
        {
            int n = Items.Count;
            if (MaximumViewCount != -1) n = Items.Count > MaximumViewCount ? MaximumViewCount : Items.Count;

            Point pt = this.Parent.PointToScreen(new Point(this.Left, this.Bottom - 2));
            Size inflatedDropSize = new Size(this.Width, n * ItemHeight + 2);

            if (Areas.Count > 1)
            {
                var rtValue = Areas["rtValue"];
                pt = this.Parent.PointToScreen(new Point(this.Left + rtValue.Left, this.Bottom - 2));
                inflatedDropSize = new Size(rtValue.Width, n * ItemHeight + 2);
            }

            Rectangle screenBounds = new Rectangle(pt, inflatedDropSize);
            Rectangle workingArea = Screen.GetWorkingArea(screenBounds);

            if (screenBounds.X < workingArea.X) screenBounds.X = workingArea.X;
            if (screenBounds.Y < workingArea.Y) screenBounds.Y = workingArea.Y;

            if (screenBounds.Right > workingArea.Right && workingArea.Width > screenBounds.Width) screenBounds.X = workingArea.Right - screenBounds.Width;
            if (screenBounds.Bottom > workingArea.Bottom && workingArea.Height > screenBounds.Height) screenBounds.Y = pt.Y - this.Height - screenBounds.Height + 3;
            return screenBounds;
        }
        #endregion
        #region CloseDropDown
        public void CloseDropDown()
        {
            if (dropContainer != null)
            {
                DropState = DvDropState.Closing;
                dropContainer.Freeze = false;
                dropContainer.Close();
            }
        }
        #endregion
        #region GetDropDownContainerDir
        internal int GetDropDownContainerDir()
        {
            int ret = -1;
            if (DropState == DvDropState.Dropping || DropState == DvDropState.Dropped)
            {
                var p1 = this.PointToScreen(new Point(0, 0));
                var p2 = dropContainer.Location;

                ret = p1.Y < p2.Y ? 1 : 2;
            }
            return ret;
        }
        #endregion
        #region SetSelectIndexForNotRaiseEvent
        public void SetSelectIndexForNotRaiseEvent(int index)
        {
            nSelectIndex = index;
            Invalidate();
        }
        #endregion
        #endregion

        #region Class
        #region DropWindowEventArgs
        internal class DropWindowEventArgs : EventArgs
        {
            internal DvDropState DropState { get; private set; }
            public DropWindowEventArgs(DvDropState DropState)
            {
                this.DropState = DropState;
            }
        }
        #endregion
        #region DropDownContainer
        public class DropDownContainer : DvForm, IMessageFilter
        {
            #region Properties
            internal bool Freeze { get; set; }
            public DvValueInputButton ValueInput { get; private set; }
            public long VScrollPosition
            {
                get => ListBox.ScrollPosition;
                set
                {
                    if (ListBox.ScrollPosition != value)
                    {
                        ListBox.ScrollPosition = value;
                        ListBox.Invalidate();
                    }
                }
            }
            #endregion

            #region Member Variable
            private DvListBox ListBox = new DvListBox();
            #endregion

            #region Event
            internal event EventHandler<DropWindowEventArgs> DropStateChanged;
            #endregion

            public DropDownContainer(DvValueInputButton c)
            {
                #region Init
                this.BlankForm = true;
                this.DoubleBuffered = true;
                this.StartPosition = FormStartPosition.Manual;
                this.ShowInTaskbar = false;
                this.ControlBox = false;
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.AutoSize = false;
                this.AutoScroll = false;
                this.MinimumSize = new Size(10, 10);
                this.Padding = new Padding(0, 0, 0, 0);

                this.Theme = c.GetTheme();
                #endregion
                #region Set
                this.ValueInput = c;
                this.Font = c.Font;
                this.BackColor = c.BackColor;
                Application.AddMessageFilter(this);
                #endregion
                #region ListBox
                ListBox.Dock = DockStyle.Fill;
                ListBox.ForeColor = c.ForeColor;
                ListBox.BackColor = c.BackColor;
                ListBox.BoxColor = c.ItemColor;
                ListBox.RectMode = true;
                ListBox.Items.AddRange(c.Items);
                ListBox.SelectionMode = ItemSelectionMode.SINGLE;
                //ListBox.Corner = 0;
                ListBox.RowHeight = c.ItemHeight;
                ListBox.TouchMode = c.TouchMode;
                ListBox.ItemClicked += (o, s) =>
                {
                    if (s.Item != null)
                    {
                        if (DropStateChanged != null) DropStateChanged.Invoke(this, new DropWindowEventArgs(DvDropState.Closing));
                        c.SelectedIndex = ListBox.Items.IndexOf(s.Item);
                        this.Close();
                    }
                };

                if (c.SelectedIndex != -1) ListBox.SelectedItems.Add(c.Items[c.SelectedIndex]);

                this.Controls.Add(ListBox);
                #endregion

                #region Color
                var Theme = c.GetTheme();
                var BoxColor = c.UseThemeColor ? Theme.Color1 : c.ValueBoxColor;
                var ItemColor = c.UseThemeColor ? Theme.Color2 : c.ItemColor;
                var SelectedItemColor = c.UseThemeColor ? Theme.PointColor : c.SelectedItemColor;
                #endregion
                this.BackColor = ListBox.BackColor = c.BackColor;
                this.ForeColor = ListBox.ForeColor = c.ForeColor;
                ListBox.UseThemeColor = false;
                ListBox.BoxColor = BoxColor;
                ListBox.ItemColor = ItemColor;
                ListBox.SelectedItemColor = SelectedItemColor;
            }

            #region Implements
            #region PreFilterMessage
            public bool PreFilterMessage(ref Message m)
            {
                if (!Freeze && this.Visible && (Form.ActiveForm == null || !Form.ActiveForm.Equals(this)))
                {
                    if (DropStateChanged != null) DropStateChanged.Invoke(this, new DropWindowEventArgs(DvDropState.Closing));
                    this.Close();
                }
                return false;
            }
            #endregion
            #endregion
        }
        #endregion
        #endregion
        #endregion
    }
}
