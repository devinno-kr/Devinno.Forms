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
    #region abstract class : DvValueInput
    public abstract class DvValueInput : DvControl
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
        #region Button
        private TextIcon texticonBtn = new TextIcon();

        public DvIcon ButtonIcon => texticonBtn.Icon;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap ButtonIconImage
        {
            get => texticonBtn.IconImage;
            set { if (texticonBtn.IconImage != value) { texticonBtn.IconImage = value; Invalidate(); } }
        }
        public string ButtonIconString
        {
            get => texticonBtn.IconString;
            set { if (texticonBtn.IconString != value) { texticonBtn.IconString = value; Invalidate(); } }
        }
        public float ButtonIconSize
        {
            get => texticonBtn.IconSize;
            set { if (texticonBtn.IconSize != value) { texticonBtn.IconSize = value; Invalidate(); } }
        }
        public int ButtonIconGap
        {
            get => texticonBtn.IconGap;
            set { if (texticonBtn.IconGap != value) { texticonBtn.IconGap = value; Invalidate(); } }
        }
        public DvTextIconAlignment ButtonIconAlignment
        {
            get => texticonBtn.IconAlignment;
            set { if (texticonBtn.IconAlignment != value) { texticonBtn.IconAlignment = value; Invalidate(); } }
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Button
        {
            get => texticonBtn.Text;
            set { if (texticonBtn.Text != value) { base.Text = texticonBtn.Text = value; Invalidate(); } }
        }

        public Padding ButtonTextPadding
        {
            get => texticonBtn.TextPadding;
            set { if (texticonBtn.TextPadding != value) { texticonBtn.TextPadding = value; Invalidate(); } }
        }
        #endregion
        #region ButtonWidth
        private int? nButtonWidth = null;
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
        #region TitleGradient
        private bool bTitleGradient = false;
        public bool TitleGradient
        {
            get => bTitleGradient;
            set
            {
                if (bTitleGradient != value)
                {
                    bTitleGradient = value;
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
        private int? nUnitWidth = null;
        public int? UnitWidth
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

        #region ButtonDownState
        protected bool ButtonDownState { get; private set; }
        #endregion
        #endregion

        #region Member Variable
        #endregion

        #region Event
        public event EventHandler ButtonClicked;
        #endregion

        #region Constructor
        public DvValueInput()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 30);
        }
        #endregion

        #region Abstract
        public abstract void DrawValue(Graphics g, DvTheme theme, RectangleF rtValue);
        public virtual void DrawValueBackground(Graphics g, DvTheme theme, RectangleF rtValueAll, RoundType rndValue, Color cV, Color cB, Color cT)
        {
            theme.DrawBox(g, rtValueAll, cV, cB, rndValue, Box.LabelBox(Embossing.FlatConcave, ShadowGap));
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

            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                var g = e.Graphics;

                var useT = TitleWidth.HasValue && TitleWidth.Value > 0;
                var useB = ButtonWidth.HasValue && ButtonWidth.Value > 0;
                var useU = UnitWidth.HasValue && UnitWidth.Value > 0;

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
                
                #region Title
                if (useT)
                {
                    var cV = TitleColor;
                    var cB = TitleBorderColor;
                    var cT = ForeColor;

                    Theme.DrawBox(g, rtTitle, TitleColor, TitleBorderColor, rndTitle, Box.ButtonUp_V(TitleGradient, ShadowGap));
                    Theme.DrawTextIcon(g, texticonTitle, Font, cT, rtTitle);
                }
                #endregion
                #region Button
                if (useB)
                {
                    var cV = ButtonDownState ? ButtonColor.BrightnessTransmit(Theme.DownBrightness) : ButtonColor;
                    var cB = ButtonDownState ? ButtonBorderColor.BrightnessTransmit(Theme.DownBrightness) : ButtonBorderColor;
                    var cT = ButtonDownState ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;

                    Theme.DrawBox(g, rtButton, cV, cB, rndButton, !ButtonDownState ? Box.ButtonUp_V(true, ShadowGap) : Box.ButtonDown(ShadowGap));

                    if (ButtonDownState) rtButton.Offset(0, 1);
                    Theme.DrawTextIcon(g, texticonBtn, Font, cT, rtButton);
                }
                #endregion
                #region Value
                {
                    var cV = ValueColor;
                    var cB = ValueBorderColor;
                    var cT = ForeColor;

                    DrawValueBackground(g, Theme, rtValueAll, rndValue, cV, cB, cT);

                    DrawValue(g, Theme, rtValue);
                }
                #endregion
                #region Unit
                if (useU && !string.IsNullOrWhiteSpace(Unit))
                {
                    #region Sep
                    var szh = Convert.ToInt32(rtValue.Height / 2);
                    var x = rtUnit.Left;

                    using (var p = new Pen(Color.Black))
                    {
                        var hsv = ValueColor.ToHSV();

                        p.Width = 1;

                        p.Color = Theme.GetInBevelColor(ValueColor);

                        var y1 = (rtValue.Top + (rtValue.Height / 2)) - (szh / 2) + 1;
                        var y2 = (rtValue.Top + (rtValue.Height / 2)) + (szh / 2) + 1;

                        g.DrawLine(p, x + 1F, y1, x + 1F, y2);

                        p.Color = ValueColor.BrightnessTransmit(Theme.BorderBrightness);
                        g.DrawLine(p, x + 0F, y1, x + 0F, y2);
                    }
                    #endregion

                    var isz = Math.Min(rtUnit.Width, rtUnit.Height) / 3;
                    if (FA.Contains(Unit)) Theme.DrawIcon(g, new DvIcon { IconString = Unit, IconSize = isz }, ForeColor, rtUnit);
                    else Theme.DrawText(g, Unit, Font, ForeColor, rtUnit);
                }
                #endregion
            });

            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, e.Location)) ButtonDownState = true;
            });

            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (ButtonDownState)
                {
                    ButtonDownState = false;
                    if (ButtonWidth.HasValue && CollisionTool.Check(rtButton, e.Location)) ButtonClicked?.Invoke(this, new EventArgs());
                }
            });

            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        public void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();

            var szUnitW = (UnitWidth.HasValue && UnitWidth.Value > 0) ? UnitWidth.Value : 0;
            var szTitleW = (TitleWidth.HasValue && TitleWidth.Value > 0) ? TitleWidth.Value : 0;
            var szButonW = (ButtonWidth.HasValue && ButtonWidth.Value > 0) ? ButtonWidth.Value : 0;

            var rts = Util.DevideSizeH(rtContent, new SizeInfo[] { new SizeInfo(DvSizeMode.Pixel, szTitleW), new SizeInfo(DvSizeMode.Percent, 100F), new SizeInfo(DvSizeMode.Pixel, szUnitW), new SizeInfo(DvSizeMode.Pixel, szButonW) }.ToList());

            var rtTitle = rts[0];
            var rtValue = rts[1];
            var rtUnit = rts[2];
            var rtButton = rts[3];
            var rtValueAll = new RectangleF(rtValue.Left, rtValue.Top, rtUnit.Right - rtValue.Left, rtValue.Height);

            act(rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll);
        }
        #endregion
        #endregion
    }
    #endregion

    #region DvValueInputText
    public class DvValueInputText : DvValueInput
    {
        #region Properties
        #region Value
        public string Value
        {
            get => OriginalTextBox.Text;
            set => OriginalTextBox.Text = value;
        }
        #endregion

        #region OriginalTextBox
        public TextBox OriginalTextBox { get; private set; }
        #endregion
        #endregion

        #region Member Variable
        RectangleF bounds;
        bool bFirst = true;
        #endregion

        #region Event
        public event EventHandler ValueChanged;
        #endregion

        #region Constructor
        public DvValueInputText() : base()
        {
            #region TextBox
            OriginalTextBox = new TextBox();
            OriginalTextBox.Location = new System.Drawing.Point(0, 0);
            OriginalTextBox.Name = "OriginalTextBox";
            OriginalTextBox.Size = new System.Drawing.Size(60, 28);
            OriginalTextBox.BorderStyle = BorderStyle.None;
            OriginalTextBox.TabIndex = 0;
            OriginalTextBox.TextAlign = HorizontalAlignment.Center;
            Controls.Add(OriginalTextBox);

            OriginalTextBox.TextChanged += (o, s) => TextChange(s);
            #endregion
        }
        #endregion

        #region Override
        #region DrawValue
        public override void DrawValue(Graphics g, DvTheme Theme, RectangleF rtValue)
        {
            if (bFirst)
            {
                DwmTool.SetDarkMode(OriginalTextBox.Handle, Theme.Brightness == ThemeBrightness.Dark);
                bFirst = false;
            }

            #region Set
            var ValueColor = this.ValueColor ?? Theme.InputColor;

            var Wnd = this.FindForm() as DvForm;

            if (OriginalTextBox.ForeColor != ForeColor) OriginalTextBox.ForeColor = ForeColor;
            if (OriginalTextBox.BackColor != ValueColor) OriginalTextBox.BackColor = ValueColor;

            Align(g, Wnd);
            #endregion
            #region Enabled Text
            if (!Enabled || (Wnd != null && Wnd.Block) || !Theme.KeyboardInput)
            {
                var rt = OriginalTextBox.Bounds; rt.Offset(1, 0);
                TextRenderer.DrawText(g, OriginalTextBox.Text, this.Font, rt, ForeColor);
            }
            #endregion
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var Wnd = FindForm() as DvForm;
            var Theme = GetTheme();
            if (Theme.KeyboardInput) OriginalTextBox.Focus();
            else
            {
                Wnd.Block = true;

                var ret = DvDialogs.Keyboard.ShowKeyboard(Title ?? "입력", Value);
                if (ret != null) OriginalTextBox.Text = ret;

                Wnd.Block = false;
            }
            base.OnMouseDown(e);
        }
        #endregion
        #endregion

        #region Method
        #region Align
        private void Align(Graphics g, DvForm Wnd)
        {
            #region Align
            OriginalTextBox.TextAlign = HorizontalAlignment.Center;
            #endregion
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                #region Font
                if (DesignMode)
                {
                    OriginalTextBox.Font = this.Font;
                }
                else
                {
                    var f = (float)OriginalTextBox.LogicalToDeviceUnits(1000) / 1000F;
                    var ftsz = this.Font.Size * f;
                    if (OriginalTextBox.Font.Name != this.Font.Name || OriginalTextBox.Font.Style != this.Font.Style || OriginalTextBox.Font.Size != ftsz)
                    {
                        OriginalTextBox.Font = new Font(this.Font.FontFamily, ftsz, this.Font.Style);
                    }
                }
                #endregion
                #region Set
                var Theme = GetTheme();
                bool bv = this.Enabled && (Wnd == null || (Wnd != null && !Wnd.Block)) && (Theme?.KeyboardInput ?? false);
                if (bv != OriginalTextBox.Visible) OriginalTextBox.Visible = bv;

                var sz = TextRenderer.MeasureText(Text, Font);
                var sz2 = TextRenderer.MeasureText("H", Font);
                var rt = Util.FromRect(rtValue, new Padding(5));
                var rtText = Util.MakeRectangleAlign(rt, new SizeF(rt.Width, Math.Max(Convert.ToInt32(sz2.Height), Convert.ToInt32(sz.Height))), DvContentAlignment.MiddleCenter);

                if (bounds != rtText)
                {
                    bounds = rtText;
                    OriginalTextBox.Bounds = Util.INT(rtText);
                }
                #endregion
            });
        }
        #endregion
        #region TextChange
        void TextChange(EventArgs a)
        {
            using (var g = CreateGraphics())
            {
                var Wnd = this.FindForm() as DvForm;
                Align(g, Wnd);
            }

            var textbox = OriginalTextBox;
            ValueChanged?.Invoke(this, a);
        }
        #endregion
        #endregion
    }
    #endregion

    #region DvValueInputNumber<T>
    public class DvValueInputNumber<T> : DvValueInput where T : struct
    {
        #region Properties
        #region Value
        public T? Value
        {
            get
            {
                T? ret = null;
              
                if (typeof(T) == typeof(sbyte))
                {
                    #region short
                    var nMax = (sbyte?)(object)Maximum ?? sbyte.MaxValue;
                    var nMin = (sbyte?)(object)Minimum ?? sbyte.MinValue;
                    sbyte n;
                    if (sbyte.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(short))
                {
                    #region short
                    var nMax = (short?)(object)Maximum ?? short.MaxValue;
                    var nMin = (short?)(object)Minimum ?? short.MinValue;
                    short n;
                    if (short.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(int))
                {
                    #region int
                    var nMax = (int?)(object)Maximum ?? int.MaxValue;
                    var nMin = (int?)(object)Minimum ?? int.MinValue;
                    int n;
                    if (int.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(long))
                {
                    #region long
                    var nMax = (long?)(object)Maximum ?? long.MaxValue;
                    var nMin = (long?)(object)Minimum ?? long.MinValue;
                    long n;
                    if (long.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(byte))
                {
                    #region byte
                    var nMax = (byte?)(object)Maximum ?? byte.MaxValue;
                    var nMin = (byte?)(object)Minimum ?? byte.MinValue;
                    byte n;
                    if (byte.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(ushort))
                {
                    #region ushort
                    var nMax = (ushort?)(object)Maximum ?? ushort.MaxValue;
                    var nMin = (ushort?)(object)Minimum ?? ushort.MinValue;
                    ushort n;
                    if (ushort.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(uint))
                {
                    #region uint
                    var nMax = (uint?)(object)Maximum ?? uint.MaxValue;
                    var nMin = (uint?)(object)Minimum ?? uint.MinValue;
                    uint n;
                    if (uint.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(ulong))
                {
                    #region ulong
                    var nMax = (ulong?)(object)Maximum ?? ulong.MaxValue;
                    var nMin = (ulong?)(object)Minimum ?? ulong.MinValue;
                    ulong n;
                    if (ulong.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(float))
                {
                    #region float
                    var nMax = (float?)(object)Maximum ?? float.MaxValue;
                    var nMin = (float?)(object)Minimum ?? float.MinValue;
                    float n;
                    if (float.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(double))
                {
                    #region double
                    var nMax = (double?)(object)Maximum ?? double.MaxValue;
                    var nMin = (double?)(object)Minimum ?? double.MinValue;
                    double n;
                    if (double.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }
                else if (typeof(T) == typeof(decimal))
                {
                    #region decimal
                    var nMax = (decimal?)(object)Maximum ?? decimal.MaxValue;
                    var nMin = (decimal?)(object)Minimum ?? decimal.MinValue;
                    decimal n;
                    if (decimal.TryParse(OriginalTextBox.Text, out n) && n >= nMin && n <= nMax) ret = (T)(object)n;
                    #endregion
                }

                return ret;
            }
            set
            {
                var v1 = OriginalTextBox.Text;
                var v2 = value.HasValue ? value.ToString() : "";
                if (v1 != v2)
                {
                    OriginalTextBox.Text = v2;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Maximum / Minimum
        public T? Minimum { get; set; } = null;
        public T? Maximum { get; set; } = null;
        #endregion
        #region IsMinusInput
        private bool IsMinusInput
        {
            get
            {
                bool ret = false;
                if (typeof(T) == typeof(sbyte))
                {
                    #region short
                    var nMax = (sbyte?)(object)Maximum ?? sbyte.MaxValue;
                    var nMin = (sbyte?)(object)Minimum ?? sbyte.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(short))
                {
                    #region short
                    var nMax = (short?)(object)Maximum ?? short.MaxValue;
                    var nMin = (short?)(object)Minimum ?? short.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(int))
                {
                    #region int
                    var nMax = (int?)(object)Maximum ?? int.MaxValue;
                    var nMin = (int?)(object)Minimum ?? int.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(long))
                {
                    #region long
                    var nMax = (long?)(object)Maximum ?? long.MaxValue;
                    var nMin = (long?)(object)Minimum ?? long.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(float))
                {
                    #region float
                    var nMax = (float?)(object)Maximum ?? float.MaxValue;
                    var nMin = (float?)(object)Minimum ?? float.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(double))
                {
                    #region double
                    var nMax = (double?)(object)Maximum ?? double.MaxValue;
                    var nMin = (double?)(object)Minimum ?? double.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                else if (typeof(T) == typeof(decimal))
                {
                    #region decimal
                    var nMax = (decimal?)(object)Maximum ?? decimal.MaxValue;
                    var nMin = (decimal?)(object)Minimum ?? decimal.MinValue;
                    ret = nMax < 0 || nMin < 0;
                    #endregion
                }
                return ret;
            }
        }
        #endregion

        #region Error
        public InputError Error
        {
            get
            {
                var ret = InputError.None;

                if (typeof(T) == typeof(sbyte))
                {
                    #region short
                    sbyte n;
                    var state = sbyte.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((sbyte)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((sbyte)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(short))
                {
                    #region short
                    short n;
                    var state = short.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((short)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((short)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(int))
                {
                    #region int
                    int n;
                    var state = int.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((int)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((int)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(long))
                {
                    #region long
                    long n;
                    var state = long.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((long)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((long)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(byte))
                {
                    #region byte
                    byte n;
                    var state = byte.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((byte)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((byte)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(ushort))
                {
                    #region ushort
                    ushort n;
                    var state = ushort.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((ushort)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((ushort)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(uint))
                {
                    #region uint
                    uint n;
                    var state = uint.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((uint)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((uint)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(ulong))
                {
                    #region float
                    ulong n;
                    var state = ulong.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((ulong)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((ulong)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(float))
                {
                    #region float
                    float n;
                    var state = float.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((float)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((float)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(double))
                {
                    #region double
                    double n;
                    var state = double.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((double)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((double)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }
                else if (typeof(T) == typeof(decimal))
                {
                    #region decimal
                    decimal n;
                    var state = decimal.TryParse(OriginalTextBox.Text, out n);
                    if (!state && string.IsNullOrWhiteSpace(OriginalTextBox.Text)) ret = InputError.Empty;
                    else if (state && Maximum.HasValue && n > ((decimal)(object)Maximum.Value)) ret = InputError.RangeOver;
                    else if (state && Minimum.HasValue && n < ((decimal)(object)Minimum.Value)) ret = InputError.RangeOver;
                    else if (!state) ret = InputError.Unknown;
                    #endregion
                }

                return ret;
            }
        }
        #endregion
        #region ErrorColor
        private Color? cErrorColor = null;
        public Color? ErrorColor
        {
            get => cErrorColor;
            set
            {
                if (cErrorColor != value)
                {
                    cErrorColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region OriginalTextBox
        public TextBox OriginalTextBox { get; private set; }
        #endregion
        #endregion

        #region Member Variable
        RectangleF bounds;
        T? old;
        bool bFirst = true;
        #endregion

        #region Event
        public event EventHandler ValueChanged;
        #endregion

        #region Constructor
        public DvValueInputNumber() : base()
        {
            if (typeof(T) == typeof(sbyte)) { }
            else if (typeof(T) == typeof(short)) { }
            else if (typeof(T) == typeof(int)) { }
            else if (typeof(T) == typeof(long)) { }
            else if (typeof(T) == typeof(byte)) { }
            else if (typeof(T) == typeof(ushort)) { }
            else if (typeof(T) == typeof(uint)) { }
            else if (typeof(T) == typeof(ulong)) { }
            else if (typeof(T) == typeof(float)) { }
            else if (typeof(T) == typeof(double)) { }
            else if (typeof(T) == typeof(decimal)) { }
            else throw new Exception("숫자 자료형이 아닙니다");

            #region TextBox
            OriginalTextBox = new TextBox();
            OriginalTextBox.Location = new System.Drawing.Point(0, 0);
            OriginalTextBox.Name = "OriginalTextBox";
            OriginalTextBox.Size = new System.Drawing.Size(60, 28);
            OriginalTextBox.BorderStyle = BorderStyle.None;
            OriginalTextBox.TabIndex = 0;
            OriginalTextBox.TextAlign = HorizontalAlignment.Center;
            Controls.Add(OriginalTextBox);

            OriginalTextBox.TextChanged += (o, s) => TextChange(s);
            #endregion

            OriginalTextBox.Text = "0";
            old = null;
        }
        #endregion

        #region Override
        #region DrawValue
        public override void DrawValue(Graphics g, DvTheme Theme, RectangleF rtValue)
        {
            if (bFirst)
            {
                DwmTool.SetDarkMode(OriginalTextBox.Handle, Theme.Brightness == ThemeBrightness.Dark);
                bFirst = false;
            }

            #region Set
            var ValueColor = this.ValueColor ?? Theme.InputColor;
            var ErrorColor = this.ErrorColor ?? Color.FromArgb(220, 0, 0);
            var Round = this.Round ?? RoundType.All;
            var Wnd = this.FindForm() as DvForm;

            if (OriginalTextBox.ForeColor != ForeColor) OriginalTextBox.ForeColor = ForeColor;
            if (OriginalTextBox.BackColor != ValueColor) OriginalTextBox.BackColor = ValueColor;

            Align(g, Wnd);
            #endregion
            #region Enabled Text
            if (!Enabled || (Wnd != null && Wnd.Block) || !Theme.KeyboardInput)
            {
                var rt = OriginalTextBox.Bounds; rt.Offset(1, 0);
                TextRenderer.DrawText(g, OriginalTextBox.Text, this.Font, rt, ForeColor);
            }
            #endregion
            #region Valid
            if (Error != InputError.None)
            {
                Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
                {
                    Theme.DrawBox(g, rtContent, ValueColor, ErrorColor, Round, BoxStyle.Border);
                });
            }
            #endregion
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var Wnd = FindForm() as DvForm;
            var Theme = GetTheme();
            if (Theme.KeyboardInput) OriginalTextBox.Focus();
            else
            {
                Wnd.Block = true;

                var ret = DvDialogs.Keypad.ShowKeypad<T>(Title ?? "입력", Value);
                if (ret.HasValue) OriginalTextBox.Text = ret.Value.ToString();

                Wnd.Block = false;
            }
            base.OnMouseDown(e);
        }
        #endregion
        #endregion

        #region Method
        #region Align
        private void Align(Graphics g, DvForm Wnd)
        {
            #region Align
            OriginalTextBox.TextAlign = HorizontalAlignment.Center;
            #endregion
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                #region Font
                if (DesignMode)
                {
                    OriginalTextBox.Font = this.Font;
                }
                else
                {
                    var f = (float)OriginalTextBox.LogicalToDeviceUnits(1000) / 1000F;
                    var ftsz = this.Font.Size * f;
                    if (OriginalTextBox.Font.Name != this.Font.Name || OriginalTextBox.Font.Style != this.Font.Style || OriginalTextBox.Font.Size != ftsz)
                    {
                        OriginalTextBox.Font = new Font(this.Font.FontFamily, ftsz, this.Font.Style);
                    }
                }
                #endregion
                #region Set
                var Theme = GetTheme();
                bool bv = this.Enabled && (Wnd == null || (Wnd != null && !Wnd.Block)) && (Theme?.KeyboardInput ?? false);
                if (bv != OriginalTextBox.Visible) OriginalTextBox.Visible = bv;

                var sz = TextRenderer.MeasureText(Text, Font);
                var sz2 = TextRenderer.MeasureText("H", Font);
                var rt = Util.FromRect(rtValue, new Padding(5));
                var rtText = Util.MakeRectangleAlign(rt, new SizeF(rt.Width, Math.Max(Convert.ToInt32(sz2.Height), Convert.ToInt32(sz.Height))), DvContentAlignment.MiddleCenter);

                if (bounds != rtText)
                {
                    bounds = rtText;
                    OriginalTextBox.Bounds = Util.INT(rtText);
                }
                #endregion
            });
        }
        #endregion
        #region TextChange
        void TextChange(EventArgs a)
        {
            using (var g = CreateGraphics())
            {
                var Wnd = this.FindForm() as DvForm;
                Align(g, Wnd);
            }

            var textbox = OriginalTextBox;
            var t = typeof(T);
            if (typeof(T) == typeof(float) || typeof(T) == typeof(double) || typeof(T) == typeof(decimal))
            {
                #region Floating
                Int32 selectionStart = textbox.SelectionStart;
                Int32 selectionLength = textbox.SelectionLength;

                String newText = String.Empty;
                bool bComma = false;
                foreach (Char c in textbox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && !bComma && textbox.Text != ".") || (newText.Length == 0 && (c == '-' || c == '+') && IsMinusInput)) newText += c;
                    if (c == '.' && textbox.Text != ".") bComma = true;
                }
                textbox.Text = newText;
                textbox.SelectionStart = selectionStart <= textbox.Text.Length ? selectionStart : textbox.Text.Length;
                #endregion
            }
            else if (typeof(T) == typeof(sbyte) || typeof(T) == typeof(short) || typeof(T) == typeof(int) || typeof(T) == typeof(long) ||
                     typeof(T) == typeof(byte) || typeof(T) == typeof(ushort) || typeof(T) == typeof(uint) || typeof(T) == typeof(ulong))
            {
                #region Number
                Int32 selectionStart = textbox.SelectionStart;
                Int32 selectionLength = textbox.SelectionLength;

                String newText = String.Empty;
                foreach (Char c in textbox.Text.ToCharArray())
                {
                    if (Char.IsDigit(c) || Char.IsControl(c) || (newText.Length == 0 && (c == '-' || c == '+') && IsMinusInput)) newText += c;
                }
                textbox.Text = newText;
                textbox.SelectionStart = selectionStart <= textbox.Text.Length ? selectionStart : textbox.Text.Length;
                #endregion
            }

            var v = Value;
            if (!old.Equals(v))
            {
                old = v;
                ValueChanged?.Invoke(this, a);
            }

            Invalidate();
        }
        #endregion
        #endregion
    }

    public class DvValueInputSByte : DvValueInputNumber<sbyte> { }
    public class DvValueInputShort : DvValueInputNumber<short> { }
    public class DvValueInputInt : DvValueInputNumber<int> { }
    public class DvValueInputLong : DvValueInputNumber<long> { }

    public class DvValueInputByte : DvValueInputNumber<byte> { }
    public class DvValueInputUShort : DvValueInputNumber<ushort> { }
    public class DvValueInputUInt : DvValueInputNumber<uint> { }
    public class DvValueInputULong : DvValueInputNumber<ulong> { }

    public class DvValueInputFloat : DvValueInputNumber<float> { }
    public class DvValueInputDouble : DvValueInputNumber<double> { }
    public class DvValueInputDecimal : DvValueInputNumber<decimal> { }
    #endregion

    #region DvValueInputBool
    public class DvValueInputBool : DvValueInput
    {
        #region Properties
        #region Value
        private bool bValue = false;
        public bool Value
        {
            get => bValue;
            set
            {
                if (bValue != value)
                {
                    bValue = value;
                    if (Animation && !ani.IsPlaying)
                    {
                        ani.Stop();
                        ani.Start(200, bValue ? "On" : "Off", () => { if (Created && !IsDisposed && Visible) this.Invoke(new Action(() => Invalidate())); });
                    }
                    ValueChanged?.Invoke(this, new EventArgs());
                    Invalidate();
                }
            }
        }
        #endregion
        #region On
        private string sOn = "ON";
        public string On
        {
            get => sOn;
            set { if (sOn != value) { sOn = value; Invalidate(); } }
        }
        #endregion
        #region Off
        private string sOff = "OFF";
        public string Off
        {
            get => sOff;
            set { if (sOff != value) { sOff = value; Invalidate(); } }
        }
        #endregion

        #region Animation
        private bool Animation => GetTheme()?.Animation ?? false;
        #endregion
        #endregion

        #region Member Variable
        Animation ani = new Animation();
        #endregion

        #region Event
        public event EventHandler ValueChanged;
        #endregion

        #region Override
        #region DrawValue
        public override void DrawValue(Graphics g, DvTheme Theme, RectangleF rtValue)
        {
            Areas2(rtValue, (rtOn, rtOff) =>
            {
                #region Var
                var CG = 2;
                var ValueColor = this.ValueColor ?? Theme.LabelColor;
                var cL = ForeColor;
                var cD = Util.FromArgb(75, cL);
                var cOn = Value ? cL : cD;
                var cOff = Value ? cD : cL;

                var isOn = Value ? 12 : 0;
                var isOff = Value ? 0 : 12;
                var igOn = Value ? CG : 0;
                var igOff = Value ? 0 : CG;
                #endregion
                #region Animation
                if (Animation && ani.IsPlaying)
                {
                    if (Value)
                    {
                        cOn = ani.Value(AnimationAccel.DCL, cD, cL);
                        cOff = ani.Value(AnimationAccel.DCL, cL, cD);
                        isOn = ani.Value(AnimationAccel.DCL, 0, 12);
                        isOff = ani.Value(AnimationAccel.DCL, 12, 0);
                        igOn = ani.Value(AnimationAccel.DCL, 0, CG);
                        igOff = ani.Value(AnimationAccel.DCL, CG, 0);
                    }
                    else
                    {
                        cOn = ani.Value(AnimationAccel.DCL, cL, cD);
                        cOff = ani.Value(AnimationAccel.DCL, cD, cL);
                        isOff = ani.Value(AnimationAccel.DCL, 0, 12);
                        isOn = ani.Value(AnimationAccel.DCL, 12, 0);
                        igOff = ani.Value(AnimationAccel.DCL, 0, CG);
                        igOn = ani.Value(AnimationAccel.DCL, CG, 0);
                    }
                }
                #endregion
                #region Text
                var tiOn = new TextIcon { Text = On, IconGap = igOn, IconSize = isOn, IconString = "fa-check", IconAlignment = DvTextIconAlignment.LeftRight };
                var tiOff = new TextIcon { Text = Off, IconGap = igOff, IconSize = isOff, IconString = "fa-check", IconAlignment = DvTextIconAlignment.LeftRight };

                Theme.DrawTextIcon(g, tiOn, Font, cOn, rtOn);
                Theme.DrawTextIcon(g, tiOff, Font, cOff, rtOff);
                #endregion
                #region Sep
                {
                    var szh = Convert.ToInt32(rtValue.Height / 2);
                    var x = Convert.ToInt32(rtValue.Left + rtValue.Width / 2);

                    using (var p = new Pen(Color.Black))
                    {
                        p.Width = 1;

                        p.Color = Theme.GetInBevelColor(ValueColor);

                        var y1 = (rtValue.Top + (rtValue.Height / 2)) - (szh / 2) + 1;
                        var y2 = (rtValue.Top + (rtValue.Height / 2)) + (szh / 2) + 1;

                        g.DrawLine(p, x + 1F, y1, x + 1F, y2);

                        p.Color = ValueColor.BrightnessTransmit(Theme.BorderBrightness);
                        g.DrawLine(p, x + 0F, y1, x + 0F, y2);
                    }
                }
                #endregion
            });
        }
        #endregion

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                Areas2(rtValue, (rtOn, rtOff) =>
                {
                    if (CollisionTool.Check(rtOn, e.Location)) Value = true;
                    if (CollisionTool.Check(rtOff, e.Location)) Value = false;
                });
            });
            base.OnMouseDown(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas2
        void Areas2(RectangleF rtValue, Action<RectangleF, RectangleF> act)
        {
            var w = rtValue.Width / 2F;
            var rtOn = Util.FromRect(rtValue.Left, rtValue.Top, w, rtValue.Height);
            var rtOff = Util.FromRect(rtValue.Left + w, rtValue.Top, w, rtValue.Height);
            act(rtOn, rtOff);
        }
        #endregion
        #endregion
    }
    #endregion

    #region DvValueInputWheel
    public class DvValueInputWheel : DvValueInput
    {
        #region Properties
        #region SelectedIndex
        private int sVal = -1;
        public int SelectedIndex
        {
            get => sVal;
            set
            {
                if (sVal != value)
                {
                    sVal = value;
                    SelectedIndexChanged?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion

        public List<TextIcon> Items { get; private set; } = new List<TextIcon>();
        public int ItemViewCount { get; set; } = 5;
        public int ItemHeight { get; set; } = 30;
        private string ValueText => SelectedIndex >= 0 && SelectedIndex < Items.Count ? Items[SelectedIndex].Text : "";
        #endregion

        #region Member Variable
        bool bDown = false;
        DvWheelPickerBox box => DvDialogs.WheelBox;
        #endregion

        #region Event
        public event EventHandler SelectedIndexChanged;
        #endregion

        #region Override
        #region DrawValue
        public override void DrawValue(Graphics g, DvTheme Theme, RectangleF rtValue)
        {
            #region Var
            var ValueColor = this.ValueColor ?? Theme.LabelColor;
            #endregion

            #region Text
            Theme.DrawText(g, ValueText, Font, ForeColor, rtValue);
            #endregion
        }
        #endregion

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (CollisionTool.Check(rtValue, e.Location)) bDown = true;
            });
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (bDown)
                {
                    bDown = false;
                    if (CollisionTool.Check(rtValue, e.Location))
                    {
                        var dlg = FindForm() as DvForm;

                        if (dlg != null) dlg.Block = true;
                        var sel = box.ShowWheelPickerBox(Title ?? "항목 선택", SelectedIndex, Items);
                        if (dlg != null) dlg.Block = false;

                        if (sel.HasValue) SelectedIndex = sel.Value;
                    }
                }
            });
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion
    }
    #endregion

    #region DvValueInputCombo
    public class DvValueInputCombo : DvValueInput
    {
        #region Properties
        #region SelectedIndex
        private int sVal = -1;
        public int SelectedIndex
        {
            get => sVal;
            set
            {
                if (sVal != value)
                {
                    sVal = value;
                    SelectedIndexChanged?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion

        public List<TextIcon> Items { get; private set; } = new List<TextIcon>();
        public int ItemViewCount { get; set; } = 8;
        public int ItemHeight { get; set; } = 30;
        private string ValueText => SelectedIndex >= 0 && SelectedIndex < Items.Count ? Items[SelectedIndex].Text : "";
        #endregion

        #region Member Variable
        bool bDown = false;
        #endregion

        #region Event
        public event EventHandler SelectedIndexChanged;
        #endregion

        #region Override
        #region DrawValue
        public override void DrawValue(Graphics g, DvTheme Theme, RectangleF rtValue)
        {
            #region Var
            var ValueColor = this.ValueColor ?? Theme.LabelColor;
            #endregion

            #region Text
            Areas2(rtValue, (rtIco, rtText) =>
            {
                #region Item
                if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                    Theme.DrawTextIcon(g, Items[SelectedIndex], Font, ForeColor, rtText);
                #endregion
                #region Icon
                var nisz = Convert.ToInt32(DrawingTool.PixelToPt(rtIco.Height / 2));
                Theme.DrawIcon(g, new DvIcon("fa-chevron-down", nisz), ForeColor, rtIco);
                #endregion
                #region Unit Sep
                using(var p = new Pen(Color.Black))
                {
                    var szh = Convert.ToInt32(rtIco.Height / 2);

                    p.Width = 1;
                    p.Color = ValueColor.BrightnessTransmit(Theme.BorderBrightness);
                    g.DrawLine(p, rtIco.Left + 0F, (rtValue.Top + (rtValue.Height / 2)) - (szh / 2) + 1, rtIco.Left + 0F, (rtValue.Top + (rtValue.Height / 2)) + (szh / 2) + 1);

                    p.Color = Theme.GetInBevelColor(ValueColor);
                    g.DrawLine(p, rtIco.Left + 1F, (rtValue.Top + (rtValue.Height / 2)) - (szh / 2) + 1, rtIco.Left + 1F, (rtValue.Top + (rtValue.Height / 2)) + (szh / 2) + 1);
                }
                #endregion
            });
            #endregion
        }
        #endregion

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (CollisionTool.Check(rtValue, e.Location)) bDown = true;
            });
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (bDown)
                {
                    bDown = false;
                    if (CollisionTool.Check(rtValue, e.Location) && Items != null && Items.Count > 0) OpenDropDown();

                }
            });
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        void Areas2(RectangleF rtValue, Action<RectangleF, RectangleF> act)
        {
            var rtContent = rtValue;
            var rtIco = Util.FromRect(rtContent.Right - (rtContent.Height + 10), rtContent.Top, rtContent.Height + 10, rtContent.Height);
            var rtBox = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width - rtIco.Width, rtContent.Height);
            var rtText = Util.FromRect(rtBox.Left, rtBox.Top, rtBox.Width, rtBox.Height);

            act(rtIco, rtText);
        }
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
            RectangleF rtvValue = new RectangleF(0, 0, 0, 0);
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) => rtvValue = rtValue);
                
            int n = Items.Count;
            Point pt = this.Parent.PointToScreen(new Point(this.Left+Convert.ToInt32(rtvValue.Left), this.Bottom - 1));
            if (ItemViewCount != -1) n = Items.Count > ItemViewCount ? ItemViewCount : Items.Count;
            Size inflatedDropSize = new Size(Convert.ToInt32(rtvValue.Width), n * ItemHeight + 2);
            Rectangle screenBounds = new Rectangle(pt, inflatedDropSize);
            Rectangle workingArea = Screen.GetWorkingArea(screenBounds);

            if (screenBounds.X < workingArea.X) screenBounds.X = workingArea.X;
            if (screenBounds.Y < workingArea.Y) screenBounds.Y = workingArea.Y;

            if (screenBounds.Right > workingArea.Right && workingArea.Width > screenBounds.Width) screenBounds.X = workingArea.Right - screenBounds.Width;
            if (screenBounds.Bottom > workingArea.Bottom && workingArea.Height > screenBounds.Height) screenBounds.Y = pt.Y - this.Height - screenBounds.Height + 3;
            return screenBounds;

            /*
            RectangleF rtvValue = new RectangleF(0, 0, 0, 0);
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) => rtvValue = rtValue);
                
            int n = Items.Count;
            Point pt = this.Parent.PointToScreen(new Point(Convert.ToInt32(rtvValue.Left), Convert.ToInt32(rtvValue.Bottom) - 1));
            if (ItemViewCount != -1) n = Items.Count > ItemViewCount ? ItemViewCount : Items.Count;
            Size inflatedDropSize = new Size(Convert.ToInt32(rtvValue.Width), n * ItemHeight + 2);
            Rectangle screenBounds = new Rectangle(pt, inflatedDropSize);
            Rectangle workingArea = Screen.GetWorkingArea(screenBounds);

            if (screenBounds.X < workingArea.X) screenBounds.X = workingArea.X;
            if (screenBounds.Y < workingArea.Y) screenBounds.Y = workingArea.Y;

            if (screenBounds.Right > workingArea.Right && workingArea.Width > screenBounds.Width) screenBounds.X = workingArea.Right - screenBounds.Width;
            if (screenBounds.Bottom > workingArea.Bottom && workingArea.Height > screenBounds.Height) screenBounds.Y = pt.Y - this.Height - screenBounds.Height + 3;
            return screenBounds;
            */
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
            sVal = index;
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
            public DvValueInputCombo ComboBox { get; private set; }
            public double VScrollPosition
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

            #region Contstructor
            public DropDownContainer(DvValueInputCombo c)
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
                this.ComboBox = c;
                this.Font = c.Font;
                this.BackColor = c.BackColor;
                Application.AddMessageFilter(this);
                #endregion
                #region ListBox
                ListBox.Dock = DockStyle.Fill;
                ListBox.ForeColor = c.ForeColor;
                ListBox.BackColor = c.BackColor;
                ListBox.BoxColor = c.ValueColor;
                ListBox.Round = RoundType.Rect;
                ListBox.Items.AddRange(c.Items);
                ListBox.SelectionMode = ItemSelectionMode.Single;
                ListBox.ItemHeight = c.ItemHeight;

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
                var BoxColor = Theme.ListBackColor;
                var ItemColor = c.ValueColor ?? Theme.InputColor;
                var SelectedItemColor = Theme.PointColor;
              
                this.BackColor = ListBox.BackColor = c.BackColor;
                this.ForeColor = ListBox.ForeColor = c.ForeColor;
                ListBox.BoxColor = BoxColor;
                ListBox.RowColor = ItemColor;
                ListBox.SelectedColor = SelectedItemColor;
                #endregion
            }
            #endregion

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
    #endregion

    #region enum : InputError
    public enum InputError
    {
        None,
        Empty,
        RangeOver,
        Unknown,
    }
    #endregion
}
