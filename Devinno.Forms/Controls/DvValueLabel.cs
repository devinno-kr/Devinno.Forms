using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
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
    #region abstract class : DvValueLabel
    public abstract class DvValueLabel : DvControl
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
        private bool bValueDown = false;
        #endregion

        #region Event
        public event EventHandler ValueClicked;
        public event EventHandler ValueDoubleClicked;
        public event EventHandler ButtonClicked;
        #endregion

        #region Constructor
        public DvValueLabel()
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
            var ValueColor = this.ValueColor ?? Theme.LabelColor;
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
                        if (useT && useB)   { rndTitle = RoundType.L; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useT)      { rndTitle = RoundType.L; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useB)      { rndTitle = RoundType.Rect; rndValue = RoundType.L; rndButton = RoundType.Rect; }
                        else                { rndTitle = RoundType.Rect; rndValue = RoundType.L; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.R:
                        if (useT && useB)   { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.R; }
                        else if (useT)      { rndTitle = RoundType.Rect; rndValue = RoundType.R; rndButton = RoundType.Rect; }
                        else if (useB)      { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.R; }
                        else                { rndTitle = RoundType.Rect; rndValue = RoundType.R; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.T:
                        if (useT && useB)   { rndTitle = RoundType.LT; rndValue = RoundType.Rect; rndButton = RoundType.RT; }
                        else if (useT)      { rndTitle = RoundType.LT; rndValue = RoundType.RT; rndButton = RoundType.Rect; }
                        else if (useB)      { rndTitle = RoundType.Rect; rndValue = RoundType.LT; rndButton = RoundType.RT; }
                        else                { rndTitle = RoundType.Rect; rndValue = RoundType.T; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.B:
                        if (useT && useB)   { rndTitle = RoundType.LB; rndValue = RoundType.Rect; rndButton = RoundType.RB; }
                        else if (useT)      { rndTitle = RoundType.LB; rndValue = RoundType.RB; rndButton = RoundType.Rect; }
                        else if (useB)      { rndTitle = RoundType.Rect; rndValue = RoundType.LB; rndButton = RoundType.RB; }
                        else                { rndTitle = RoundType.Rect; rndValue = RoundType.B; rndButton = RoundType.Rect; }
                        break;
                    #endregion

                    #region LT / RT / LB / RB
                    case RoundType.LT:
                        if (useT && useB)   { rndTitle = RoundType.LT; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useT)      { rndTitle = RoundType.LT; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useB)      { rndTitle = RoundType.Rect; rndValue = RoundType.LT; rndButton = RoundType.Rect; }
                        else                { rndTitle = RoundType.Rect; rndValue = RoundType.LT; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.RT:
                        if (useT && useB)   { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.RT; }
                        else if (useT)      { rndTitle = RoundType.Rect; rndValue = RoundType.RT; rndButton = RoundType.Rect; }
                        else if (useB)      { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.RT; }
                        else                { rndTitle = RoundType.Rect; rndValue = RoundType.RT; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.LB:
                        if (useT && useB)   { rndTitle = RoundType.LB; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useT)      { rndTitle = RoundType.LB; rndValue = RoundType.Rect; rndButton = RoundType.Rect; }
                        else if (useB)      { rndTitle = RoundType.Rect; rndValue = RoundType.LB; rndButton = RoundType.Rect; }
                        else                { rndTitle = RoundType.Rect; rndValue = RoundType.LB; rndButton = RoundType.Rect; }
                        break;
                    case RoundType.RB:
                        if (useT && useB)   { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.RB; }
                        else if (useT)      { rndTitle = RoundType.Rect; rndValue = RoundType.RB; rndButton = RoundType.Rect; }
                        else if (useB)      { rndTitle = RoundType.Rect; rndValue = RoundType.Rect; rndButton = RoundType.RB; }
                        else                { rndTitle = RoundType.Rect; rndValue = RoundType.RB; rndButton = RoundType.Rect; }
                        break;
                    #endregion

                    #region All
                    case RoundType.All:
                        if (useT && useB)   { rndTitle = RoundType.L; rndValue = RoundType.Rect; rndButton = RoundType.R; }
                        else if (useT)      { rndTitle = RoundType.L; rndValue = RoundType.R; rndButton = RoundType.Rect; }
                        else if (useB)      { rndTitle = RoundType.Rect; rndValue = RoundType.L; rndButton = RoundType.R; }
                        else                { rndTitle = RoundType.Rect; rndValue = RoundType.All; rndButton = RoundType.Rect; }
                        break;
                        #endregion
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
                    
                    Theme.DrawBox(g, rtButton, cV, cB, rndButton, !ButtonDownState ? Box.ButtonUp_V(true, ShadowGap) : Box.ButtonDown(ShadowGap));

                    if (ButtonDownState) rtButton.Offset(0, 1);
                    Theme.DrawTextIcon(g, texticonBtn, Font, cT, rtButton);
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
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (bValueDown)
                {
                    bValueDown = false;
                    if (CollisionTool.Check(rtValueAll, e.Location)) ValueClicked?.Invoke(this, new EventArgs());
                }
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
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            Areas((rtContent, rtTitle, rtValue, rtUnit, rtButton, rtValueAll) =>
            {
                if (CollisionTool.Check(rtValueAll, e.Location)) ValueDoubleClicked?.Invoke(this, new EventArgs());
            });

            Invalidate();
            base.OnMouseDoubleClick(e);
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

    #region DvValueLabelText
    public class DvValueLabelText : DvValueLabel
    {
        #region Properties
        #region Value
        private string sValue = "";
        public string Value
        {
            get => sValue;
            set
            {
                if(sValue != value)
                {
                    sValue = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion
        #region Override
        #region DrawValue
        public override void DrawValue(Graphics g, DvTheme theme, RectangleF rtValue)
        {
            if (!string.IsNullOrWhiteSpace(Value))
                theme.DrawText(g, Value, Font, ForeColor, rtValue);
        }
        #endregion
        #endregion
    }
    #endregion

    #region DvValueLabelNumber
    public class DvValueLabelNumber<T> : DvValueLabel where T : struct
    {
        #region Properties
        #region Value
        private T nValue = default(T);
        public T Value
        {
            get => nValue;
            set
            {
                if (!nValue.Equals(value))
                {
                    nValue = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region FormatString
        public string FormatString { get; set; } = null;
        #endregion
        #region ValueText
        private string ValueText
        {
            get
            {
                var ret = "";
                if (typeof(T) == typeof(sbyte)) ret = FormatString != null ? ((sbyte)(object)Value).ToString(FormatString) : Value.ToString();
                else if (typeof(T) == typeof(short)) ret = FormatString != null ? ((short)(object)Value).ToString(FormatString) : Value.ToString();
                else if (typeof(T) == typeof(int)) ret = FormatString != null ? ((int)(object)Value).ToString(FormatString) : Value.ToString();
                else if (typeof(T) == typeof(long)) ret = FormatString != null ? ((long)(object)Value).ToString(FormatString) : Value.ToString();
                else if (typeof(T) == typeof(byte)) ret = FormatString != null ? ((byte)(object)Value).ToString(FormatString) : Value.ToString();
                else if (typeof(T) == typeof(ushort)) ret = FormatString != null ? ((ushort)(object)Value).ToString(FormatString) : Value.ToString();
                else if (typeof(T) == typeof(uint)) ret = FormatString != null ? ((uint)(object)Value).ToString(FormatString) : Value.ToString();
                else if (typeof(T) == typeof(ulong)) ret = FormatString != null ? ((ulong)(object)Value).ToString(FormatString) : Value.ToString();
                else if (typeof(T) == typeof(float)) ret = FormatString != null ? ((float)(object)Value).ToString(FormatString) : Value.ToString();
                else if (typeof(T) == typeof(double)) ret = FormatString != null ? ((double)(object)Value).ToString(FormatString) : Value.ToString();
                else if (typeof(T) == typeof(decimal)) ret = FormatString != null ? ((decimal)(object)Value).ToString(FormatString) : Value.ToString();

                return ret;
            }
        }
        #endregion
        #endregion
        #region Constructor
        public DvValueLabelNumber()
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
        }
        #endregion
        #region Override
        #region DrawValue
        public override void DrawValue(Graphics g, DvTheme theme, RectangleF rtValue)
        {
            theme.DrawText(g, ValueText, Font, ForeColor, rtValue);
        }
        #endregion
        #endregion
    }

    public class DvValueLabelSByte : DvValueLabelNumber<sbyte> { }
    public class DvValueLabelShort : DvValueLabelNumber<short> { }
    public class DvValueLabelInt : DvValueLabelNumber<int> { }
    public class DvValueLabelLong : DvValueLabelNumber<long> { }

    public class DvValueLabelByte : DvValueLabelNumber<byte> { }
    public class DvValueLabelUShort : DvValueLabelNumber<ushort> { }
    public class DvValueLabelUInt : DvValueLabelNumber<uint> { }
    public class DvValueLabelULong : DvValueLabelNumber<ulong> { }

    public class DvValueLabelFloat : DvValueLabelNumber<float> { }
    public class DvValueLabelDouble : DvValueLabelNumber<double> { }
    public class DvValueLabelDecimal : DvValueLabelNumber<decimal> { }

    #endregion

    #region DvValueLabelBoolean
    public class DvValueLabelBoolean: DvValueLabel
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

        #region Override
        #region DrawValue
        public override void DrawValue(Graphics g, DvTheme Theme, RectangleF rtValue)
        {
            Areas2(rtValue, (rtOn, rtOff, cOn, cOff) =>
            {
                var rt = Util.FromRect(rtValue, new Padding(5));
                g.SetClip(rt);
                #region Var
                var ValueColor = this.ValueColor ?? Theme.LabelColor;
                #endregion
                #region Text
                if (Animation && ani.IsPlaying)
                {
                    Theme.DrawText(g,  On, Font, cOn, rtOn);
                    Theme.DrawText(g, Off, Font, cOff, rtOff);
                }
                else
                {
                    Theme.DrawText(g, Value ? On : Off, Font, ForeColor, rtValue);
                }
                #endregion
                g.ResetClip();
            });
        }
        #endregion
        #endregion

        #region Method
        #region Areas2
        void Areas2(RectangleF rtValue, Action<RectangleF, RectangleF, Color, Color> act)
        {
            RectangleF rtOn = rtValue;
            RectangleF rtOff = rtValue;
            Color cOn = ForeColor;
            Color cOff = ForeColor;
            if(Animation && ani.IsPlaying)
            {
                if (Value)
                {
                    rtOff.Offset(ani.Value(AnimationAccel.DCL, 0, rtValue.Width), 0);
                    rtOn.Offset(ani.Value(AnimationAccel.DCL, -rtValue.Width, 0), 0);

                    cOff = ani.Value(AnimationAccel.DCL, ForeColor, Color.FromArgb(30, ForeColor));
                    cOn = ani.Value(AnimationAccel.DCL, Color.FromArgb(30, ForeColor), ForeColor);
                }
                else
                {
                    rtOff.Offset(ani.Value(AnimationAccel.DCL, rtValue.Width, 0), 0);
                    rtOn.Offset(ani.Value(AnimationAccel.DCL, 0, -rtValue.Width), 0);

                    cOn = ani.Value(AnimationAccel.DCL, ForeColor, Color.FromArgb(30, ForeColor));
                    cOff = ani.Value(AnimationAccel.DCL, Color.FromArgb(30, ForeColor), ForeColor);
                }

                act(rtOn, rtOff, cOn, cOff);
            }
            else
            {
                if (Value)
                {
                    rtOff.Offset(-rtValue.Width, 0);
                    cOff = Color.FromArgb(30, ForeColor);
                }
                else
                {
                    rtOn.Offset(rtValue.Width, 0);
                    cOn = Color.FromArgb(30, ForeColor);
                }
                act(rtOn, rtOff, cOn, cOff);
            }
            
        }
        #endregion
        #endregion
    }
    #endregion
}
