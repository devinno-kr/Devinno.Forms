using Devinno.Extensions;
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
    public class DvValueLabelButton : DvControl
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
        #region Value
        string strValue = null;
        public string Value
        {
            get => strValue;
            set
            {
                if (strValue != value)
                {
                    strValue = value;
                    Invalidate();
                }
            }
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
        private Color cValueColor = DvTheme.DefaultTheme.Color2;
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
        #region Style
        private LabelStyle eStyle = LabelStyle.FlatConvex;
        public LabelStyle Style
        {
            get => eStyle;
            set
            {
                if (eStyle != value)
                {
                    eStyle = value;
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
        bool bDown = false;
        private LongClick click = new LongClick();
        #endregion

        #region Event
        public event EventHandler LongClick;
        public event EventHandler ButtonClick;
        public event EventHandler ButtonDown;
        public event EventHandler ButtonUp;
        #endregion

        #region Constructor
        public DvValueLabelButton()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 30);

            click.Reset = new Action(() => { this.Invoke(new Action(() => { bDown = false; Invalidate(); })); });
            click.GenLongClick = new Action(() => { this.Invoke(new Action(() => LongClick?.Invoke(this, null))); });
        }
        #endregion

        #region Override
        #region LoadArea
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
        }
        #endregion

        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var ValueColor = UseThemeColor ? Theme.Color2 : this.ValueBoxColor;
            var TitleColor = UseThemeColor ? Theme.Color3 : this.TitleBoxColor;
            var ButtonColor = UseThemeColor ? Theme.Color3 : this.ButtonColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtValue = Areas["rtValue"];
            var rtValueAll = Areas["rtValueAll"];
            var rtTitle = Areas["rtTitle"];
            var rtUnit = Areas["rtUnit"];
            var rtButton = Areas["rtButton"];
            #endregion
            #region Init
            var p = new Pen(ValueColor, 1);
            var br = new SolidBrush(ValueColor);
            #endregion
            #region Draw
            Theme.DrawBox(e.Graphics, TitleColor, BackColor, rtTitle, RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);

            switch (Style)
            {
                case LabelStyle.FlatConcave:
                    Theme.DrawBox(e.Graphics, ValueColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL);
                    break;
                case LabelStyle.FlatConvex:
                    Theme.DrawBox(e.Graphics, ValueColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                    break;
                case LabelStyle.Concave:
                    Theme.DrawBox(e.Graphics, ValueColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL | BoxDrawOption.IN_SHADOW);
                    break;
                case LabelStyle.Convex:
                    Theme.DrawBox(e.Graphics, ValueColor, BackColor, rtValueAll, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW | BoxDrawOption.IN_BEVEL_LT);
                    break;
            }

            Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor, TitleColor, rtTitle);
            Theme.DrawTextShadow(e.Graphics, null, Value, Font, ForeColor, ValueColor, rtValue);

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

            if (UnitWidth > 0 && !string.IsNullOrWhiteSpace(Unit))
            {
                #region Unit Sep
                var szh = Convert.ToInt32(rtUnit.Height / 2);

                p.Width = 1;

                p.Color = ValueColor.BrightnessTransmit(Theme.OutBevelBright);
                e.Graphics.DrawLine(p, rtUnit.X + 1, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtUnit.X + 1, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);

                p.Color = ValueColor.BrightnessTransmit(Theme.BorderBright);
                e.Graphics.DrawLine(p, rtUnit.X, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtUnit.X, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);
                #endregion
                Theme.DrawTextShadow(e.Graphics, null, Unit, Font, ForeColor, ValueColor, rtUnit);
            }
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
                ButtonDown?.Invoke(this, null);
                Invalidate();

                click.MouseDown(e);
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            click.MouseUp(e);
            if (Areas.ContainsKey("rtButton") && CollisionTool.Check(Areas["rtButton"], e.Location)) ButtonUp?.Invoke(this, null);
            if (bDown)
            {
                bDown = false;
                Invalidate();
                if (Areas.ContainsKey("rtButton") && CollisionTool.Check(Areas["rtButton"], e.Location)) ButtonClick?.Invoke(this, null);
            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion
    }
}
