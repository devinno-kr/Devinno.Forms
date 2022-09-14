using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
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
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvTextBox : DvControl
    {
        #region Properties
        #region TextBoxColor
        private Color? cTextBoxColor = null;
        public Color? TextBoxColor
        {
            get => cTextBoxColor;
            set
            {
                if (cTextBoxColor != value)
                {
                    cTextBoxColor = value;
                    OriginalTextBox.BackColor = cTextBoxColor ?? GetTheme().InputColor;
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
        #region BorderColor
        private Color? cBorderColor = null;
        public Color? BorderColor
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

        #region InputType
        private DvTextBoxType eInputType = DvTextBoxType.Text;
        public DvTextBoxType InputType
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
        #region Text
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Text
        {
            get => OriginalTextBox.Text;
            set => OriginalTextBox.Text = value;
        }
        #endregion
        #region TextPadding
        private Padding padText = new Padding(5, 5, 5, 5);
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
        #region Style(Emboss)
        private Embossing eEmboss = Embossing.FlatConcave;
        public Embossing Style
        {
            get => eEmboss;
            set
            {
                if (eEmboss != value)
                {
                    eEmboss = value;
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

        #region ContentAlignment
        private DvContentAlignment eContentAlignment = DvContentAlignment.MiddleCenter;
        public DvContentAlignment ContentAlignment
        {
            get { return eContentAlignment; }
            set
            {
                if (eContentAlignment != value)
                {
                    eContentAlignment = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region OriginalTextBox
        public TextBox OriginalTextBox { get; private set; }
        #endregion
        #region MaxLength
        public int MaxLength
        {
            get => OriginalTextBox.MaxLength;
            set => OriginalTextBox.MaxLength = value;
        }
        #endregion
        #region MultiLine
        public bool MultiLine
        {
            get => OriginalTextBox.Multiline;
            set => OriginalTextBox.Multiline = value;
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
        #endregion

        #region Member Variable
        RectangleF bounds = new RectangleF(0, 0, 0, 0);
        #endregion

        #region Event
        public new event EventHandler TextChanged;
        #endregion

        #region Constructor
        public DvTextBox()
        {
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

            OriginalTextBox.TextChanged += (o, s) => TextChange(s);
            #endregion
        }
        #endregion

        #region Override
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

        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            Areas((rtContent, rtText, rtUnit) =>
            {
                #region Var
                var TextBoxColor = this.TextBoxColor ?? Theme.InputColor;
                var BorderColor = this.BorderColor ?? Theme.GetBorderColor(TextBoxColor, BackColor);
                var Round = this.Round ?? RoundType.All;
                #endregion
                #region Set
                var Wnd = this.FindForm() as DvForm;

                if (OriginalTextBox.ForeColor != ForeColor) OriginalTextBox.ForeColor = ForeColor;
                if (OriginalTextBox.BackColor != TextBoxColor) OriginalTextBox.BackColor = TextBoxColor;

                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                #endregion
                #region Init
                var p = new Pen(TextBoxColor, 1);
                var br = new SolidBrush(TextBoxColor);
                #endregion
                #region Draw
                Align(e.Graphics, Wnd);

                Theme.DrawBox(e.Graphics, rtContent, TextBoxColor, BorderColor, Round, Box.LabelBox(Style, ShadowGap));

                #region Enabled Text
                if (!Enabled || (Wnd != null && Wnd.Block) || !Theme.KeyboardInput)
                {
                    var rt = OriginalTextBox.Bounds; rt.Offset(1, 0);

                    var flag = TextFormatFlags.Default;
                    switch(ContentAlignment)
                    {
                        case DvContentAlignment.TopLeft:        flag |= TextFormatFlags.Left;             flag |= TextFormatFlags.Top; break;
                        case DvContentAlignment.TopCenter:      flag |= TextFormatFlags.HorizontalCenter; flag |= TextFormatFlags.Top; break;
                        case DvContentAlignment.TopRight:       flag |= TextFormatFlags.Right;            flag |= TextFormatFlags.Top; break;

                        case DvContentAlignment.MiddleLeft:     flag |= TextFormatFlags.Left;             flag |= TextFormatFlags.VerticalCenter; break;
                        case DvContentAlignment.MiddleCenter:   flag |= TextFormatFlags.HorizontalCenter; flag |= TextFormatFlags.VerticalCenter; break;
                        case DvContentAlignment.MiddleRight:    flag |= TextFormatFlags.Right;            flag |= TextFormatFlags.VerticalCenter; break;

                        case DvContentAlignment.BottomLeft:     flag |= TextFormatFlags.Left;             flag |= TextFormatFlags.Bottom; break;
                        case DvContentAlignment.BottomCenter:   flag |= TextFormatFlags.HorizontalCenter; flag |= TextFormatFlags.Bottom; break;
                        case DvContentAlignment.BottomRight:    flag |= TextFormatFlags.Right;            flag |= TextFormatFlags.Bottom; break;

                    }

                    TextRenderer.DrawText(e.Graphics, OriginalTextBox.Text, this.Font, rt, ForeColor, flag);
                }
                #endregion
                #region Unit
                if (UnitWidth.HasValue && UnitWidth.Value > 0 && !string.IsNullOrWhiteSpace(Unit))
                {
                    var szh = Convert.ToInt32(rtUnit.Height / 2);

                    p.Width = 1;

                    p.Color = Util.FromArgb(Theme.OutBevelAlpha, Color.White);
                    e.Graphics.DrawLine(p, rtUnit.X + 1, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtUnit.X + 1, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);

                    p.Color = Util.FromArgb(Theme.OutShadowAlpha, Color.Black);
                    e.Graphics.DrawLine(p, rtUnit.X, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtUnit.X, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);

                    //TextRenderer.DrawText(e.Graphics, Unit, Font, rtUnit, ForeColor);
                    Theme.DrawText(e.Graphics, Unit, Font, ForeColor, rtUnit);
                }
                #endregion
                #endregion
                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion
            });
            base.OnThemeDraw(e, Theme);
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

                var ret = DvDialogs.Keyboard.ShowKeyboard("입력", Text);
                if (ret != null) OriginalTextBox.Text = ret;

                Wnd.Block = false;
            }
            base.OnMouseDown(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        /// <summary>
        /// ( rtContent, rtText, rtUnit )
        /// </summary>
        /// <param name="act">( rtContent, rtText, rtUnit )</param>
        public void Areas(Action<RectangleF, RectangleF, RectangleF> act)
        {
            var szUnitW = (UnitWidth.HasValue && UnitWidth.Value > 0) ? UnitWidth.Value : 0;

            var rtContent = GetContentBounds();
            var rtTextAll = new RectangleF(rtContent.Left, rtContent.Top, rtContent.Width - szUnitW, rtContent.Height);
            var rtTextArea = Util.FromRect(rtTextAll, TextPadding);
            var rtUnit = Util.FromRect(rtTextAll.Right, rtTextAll.Top, szUnitW, rtTextAll.Height);

            var sz = TextRenderer.MeasureText(Text, Font);
            var sz2 = TextRenderer.MeasureText("H", Font);
            var rtText = Util.MakeRectangleAlign(rtTextArea, new SizeF(rtTextArea.Width - rtUnit.Width, Math.Max(Convert.ToInt32(sz2.Height), Convert.ToInt32(sz.Height))), ContentAlignment);

            act(rtContent, rtText, rtUnit);
        }
        #endregion
        #region Align
        private void Align(Graphics g, DvForm Wnd)
        {
            #region Align
            switch (ContentAlignment)
            {
                case DvContentAlignment.TopLeft:
                case DvContentAlignment.MiddleLeft:
                case DvContentAlignment.BottomLeft:
                    if (OriginalTextBox.TextAlign != HorizontalAlignment.Left)
                        OriginalTextBox.TextAlign = HorizontalAlignment.Left;
                    break;

                case DvContentAlignment.TopCenter:
                case DvContentAlignment.MiddleCenter:
                case DvContentAlignment.BottomCenter:
                    if (OriginalTextBox.TextAlign != HorizontalAlignment.Center)
                        OriginalTextBox.TextAlign = HorizontalAlignment.Center;
                    break;

                case DvContentAlignment.TopRight:
                case DvContentAlignment.MiddleRight:
                case DvContentAlignment.BottomRight:
                    if (OriginalTextBox.TextAlign != HorizontalAlignment.Right)
                        OriginalTextBox.TextAlign = HorizontalAlignment.Right;
                    break;
            }
            #endregion
            Areas((rtContent, rtText, rtUnit) =>
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
            if (InputType == DvTextBoxType.Number)
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
            else if (InputType == DvTextBoxType.Floating)
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
            else if (InputType == DvTextBoxType.Hex)
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

            TextChanged?.Invoke(this, null);
        }
        #endregion
        #endregion
    }
}
