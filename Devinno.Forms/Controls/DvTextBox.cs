using Devinno.Extensions;
using Devinno.Forms.Containers;
using Devinno.Forms.Dialogs;
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
    public enum DvInputType { NUMBER, FLOATING, TEXT, HEX, ENG_NUM, COMBO, BOOL };

    public class DvTextBox : DvControl
    {
        #region Properties
        #region TextBoxColor
        private Color cTextBoxColor = DvTheme.DefaultTheme.Color1;
        public Color TextBoxColor
        {
            get => cTextBoxColor;
            set
            {
                if (cTextBoxColor != value)
                {
                    OriginalTextBox.BackColor = cTextBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ForeColor
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                if (base.ForeColor != value)
                {
                    OriginalTextBox.ForeColor = base.ForeColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region OriginalTextBox
        public TextBox OriginalTextBox { get; private set; }
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

        #region InputType
        private DvInputType eInputType = DvInputType.TEXT;
        public DvInputType InputType
        {
            get => eInputType;
            set
            {
                if (eInputType != value)
                {
                    eInputType = value;
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

        #region MaxLength
        public int MaxLength
        {
            get => OriginalTextBox.MaxLength;
            set => OriginalTextBox.MaxLength = value;
        }
        #endregion
        #region TextAlign
        public HorizontalAlignment TextAlign
        {
            get => OriginalTextBox.TextAlign;
            set => OriginalTextBox.TextAlign = value;
        }
        #endregion
        #region MultiLine
        public bool MultiLine
        {
            get => OriginalTextBox.Multiline;
            set => OriginalTextBox.Multiline = value;
        }
        #endregion

        #region Text
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Text
        {
            get => OriginalTextBox.Text;
            set => OriginalTextBox.Text = value;
        }
        #endregion
  
        #region Font
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
            }
        }
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
        #region Style
        private LabelStyle eStyle = LabelStyle.FlatConcave;
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
        #endregion

        #region Constructor
        public DvTextBox() 
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            this.Size = new Size(150, 30);

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
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var szUnitW = 0;
            if (!string.IsNullOrWhiteSpace(Unit)) szUnitW = UnitWidth;

            var rtContent = Areas["rtContent"];
            var rtTextAll = new Rectangle(TextPadding.Left, TextPadding.Top, rtContent.Width - (TextPadding.Left + TextPadding.Right), rtContent.Height - (TextPadding.Top + TextPadding.Bottom));
            var rtUnit = new Rectangle(rtTextAll.Right - szUnitW, rtTextAll.Top, szUnitW, rtTextAll.Height);
            var rtText = new Rectangle(rtTextAll.Left, rtTextAll.Top, rtTextAll.Width - rtUnit.Width, rtTextAll.Height); rtText.Inflate(-1, 0);
            SetArea("rtText", rtText);
            SetArea("rtUnit", rtUnit);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            if (Areas.Count > 1)
            {
                #region Color
                var BoxColor = UseThemeColor ? Theme.Color1 : TextBoxColor;
                #endregion
                #region Set
                var Wnd = this.FindForm() as DvForm;

                if (OriginalTextBox.ForeColor != ForeColor) OriginalTextBox.ForeColor = ForeColor;
                if (OriginalTextBox.ForeColor != BoxColor) OriginalTextBox.BackColor = BoxColor;

                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                #endregion
                #region Bounds
                var rtContent = Areas["rtContent"];
                var rtUnit = Areas["rtUnit"];
                var rtText = Areas["rtText"];
                #endregion
                #region Init
                var p = new Pen(BoxColor, 2);
                var br = new SolidBrush(BoxColor);
                #endregion
                #region Draw
                Center(e.Graphics, Wnd);

                switch (Style)
                {
                    case LabelStyle.FlatConcave:
                        Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL);
                        break;
                    case LabelStyle.FlatConvex:
                        Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                        break;
                    case LabelStyle.Concave:
                        Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL | BoxDrawOption.IN_SHADOW);
                        break;
                    case LabelStyle.Convex:
                        Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW | BoxDrawOption.IN_BEVEL_LT);
                        break;
                }

                TextRenderer.DrawText(e.Graphics, Unit, Font, rtUnit, ForeColor);
                #region Enabled Text
                if (!Enabled || (Wnd != null && Wnd.Block))
                {
                    var rt = OriginalTextBox.Bounds; rt.Offset(1, 0);
                    TextRenderer.DrawText(e.Graphics, OriginalTextBox.Text, this.Font, rt, ForeColor);
                }
                #endregion

                #region Unit
                if (UnitWidth > 0 && !string.IsNullOrWhiteSpace(Unit))
                {
                    #region Unit Sep
                    var szh = Convert.ToInt32(rtUnit.Height / 2);

                    p.Width = 1;

                    p.Color = TextBoxColor.BrightnessTransmit(-Theme.BorderBright);
                    e.Graphics.DrawLine(p, rtUnit.X + 1, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtUnit.X + 1, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);

                    p.Color = TextBoxColor.BrightnessTransmit(Theme.BorderBright);
                    e.Graphics.DrawLine(p, rtUnit.X, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtUnit.X, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);
                    #endregion
                    Theme.DrawTextShadow(e.Graphics, null, Unit, Font, ForeColor, TextBoxColor, rtUnit);
                }
                #endregion
                #endregion
                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion
            }
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
            this.Focus();
            OriginalTextBox.Focus();
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #endregion

        #region Method
        #region TextChange
        void TextChange()
        {
            var textbox = OriginalTextBox;
            if (InputType == DvInputType.NUMBER)
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
            else if (InputType == DvInputType.FLOATING)
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
            else if (InputType == DvInputType.HEX)
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
            else if (InputType == DvInputType.ENG_NUM)
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
        }
        #endregion
        #region Center
        private void Center(Graphics g, DvForm Wnd)
        {
            if (Areas.Count > 1)
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
                        //var old = OriginalTextBox.Font;
                        OriginalTextBox.Font = new Font(this.Font.FontFamily, ftsz, this.Font.Style);
                        //old.Dispose();
                    }
                }
                #endregion
                #region Bounds
                var rtUnit = Areas["rtUnit"];
                var rtText = Areas["rtText"];
                #endregion
                #region Set
                OriginalTextBox.Size = rtText.Size;
                var rttb = MathTool.MakeRectangle(rtText, OriginalTextBox.Size);
                OriginalTextBox.Location = new Point(rttb.X + 0, rttb.Y + 0);
                OriginalTextBox.Visible = this.Enabled && (Wnd == null || (Wnd != null && !Wnd.Block));
                #endregion
            }
        }
        #endregion
        #endregion
    }
}
