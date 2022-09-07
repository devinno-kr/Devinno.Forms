using Devinno.Extensions;
using Devinno.Forms.Controls;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Dialogs
{
    public partial class DvKeypad : DvForm
    {
        #region Properties
        public int ButtonIconSize { get; set; } = 14;
        #endregion

        #region Member Variable
        DvButton btn0;
        DvButton btn1;
        DvButton btn2;
        DvButton btn3;
        DvButton btn4;
        DvButton btn5;
        DvButton btn6;
        DvButton btn7;
        DvButton btn8;
        DvButton btn9;
        DvButton btnDot;
        DvButton btnBack;
        DvButton btnClear;
        DvButton btnSign;
        DvButton btnEnter;
        DvLabel lbl;

        string sval = "";
        string svalOrigin = "";
        int mode = 0;
        Type valueType;

        byte? minU8 = null, maxU8 = null;
        ushort? minU16 = null, maxU16 = null;
        uint? minU32 = null, maxU32 = null;
        ulong? minU64 = null, maxU64 = null;
        sbyte? minI8 = null, maxI8 = null;
        short? minI16 = null, maxI16 = null;
        int? minI32 = null, maxI32 = null;
        long? minI64 = null, maxI64 = null;
        float? minF1 = null, maxF1 = null;
        double? minF2 = null, maxF2 = null;
        decimal? minF3 = null, maxF3 = null;
        #endregion

        #region Constructor
        public DvKeypad()
        {
            InitializeComponent();

            #region New
            var gr = false;
            lbl = new DvLabel() { Name = nameof(lbl), Dock = DockStyle.Fill };
            btn0 = new DvButton() { Name = nameof(btn0), Text = "0", Gradient = gr, Dock = DockStyle.Fill };
            btn1 = new DvButton() { Name = nameof(btn1), Text = "1", Gradient = gr, Dock = DockStyle.Fill };
            btn2 = new DvButton() { Name = nameof(btn2), Text = "2", Gradient = gr, Dock = DockStyle.Fill };
            btn3 = new DvButton() { Name = nameof(btn3), Text = "3", Gradient = gr, Dock = DockStyle.Fill };
            btn4 = new DvButton() { Name = nameof(btn4), Text = "4", Gradient = gr, Dock = DockStyle.Fill };
            btn5 = new DvButton() { Name = nameof(btn5), Text = "5", Gradient = gr, Dock = DockStyle.Fill };
            btn6 = new DvButton() { Name = nameof(btn6), Text = "6", Gradient = gr, Dock = DockStyle.Fill };
            btn7 = new DvButton() { Name = nameof(btn7), Text = "7", Gradient = gr, Dock = DockStyle.Fill };
            btn8 = new DvButton() { Name = nameof(btn8), Text = "8", Gradient = gr, Dock = DockStyle.Fill };
            btn9 = new DvButton() { Name = nameof(btn9), Text = "9", Gradient = gr, Dock = DockStyle.Fill };
            btnDot = new DvButton { Name = nameof(btnDot), Text = ".", Gradient = gr, Dock = DockStyle.Fill };
            btnBack = new DvButton { Name = nameof(btnBack), IconString = "fa-delete-left", Text = "", IconSize = ButtonIconSize, Gradient = gr, Dock = DockStyle.Fill };
            btnClear = new DvButton { Name = nameof(btnClear), IconString = "fa-eraser", Text = "", IconSize = ButtonIconSize, Gradient = gr, Dock = DockStyle.Fill };
            btnSign = new DvButton { Name = nameof(btnSign), IconString = "fa-plus-minus", Text = "", IconSize = ButtonIconSize, Gradient = gr, Dock = DockStyle.Fill };
            btnEnter = new DvButton { Name = nameof(btnEnter), Text = "", IconSize = ButtonIconSize, Gradient = gr, Dock = DockStyle.Fill };
            #endregion

            #region Enter.Draw
            btnEnter.ThemeDraw += (o, s) =>
            {
                var rt = btnEnter.GetContentBounds();
                var cp = MathTool.CenterPoint(rt);
                s.Graphics.TranslateTransform(cp.X, cp.Y);
                s.Graphics.RotateTransform(90);

                Theme.DrawIcon(s.Graphics, new DvIcon("fa-arrow-turn-down", ButtonIconSize), ForeColor, MathTool.MakeRectangle(new Point(0, 0), rt.Width, rt.Height));

                s.Graphics.ResetTransform();
            };
            #endregion
            #region Enter / Clear
            btnClear.MouseClick += (o, s) => { sval = ""; SetText(); };
            btnEnter.MouseClick += (o, s) =>
            {
                if (sval.Length != lbl.Text.Length && sval.Length == 0)
                {
                    sval = svalOrigin;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    decimal v1;
                    long v2;
                    ulong v3;

                    if (mode == 0 && ulong.TryParse(sval, out v3)) DialogResult = DialogResult.OK;
                    else if (mode == 1 && long.TryParse(sval, out v2)) DialogResult = DialogResult.OK;
                    else if (mode == 2 && decimal.TryParse(sval, out v1)) DialogResult = DialogResult.OK;
                    else if (mode == 3 && long.TryParse(sval, out v2)) DialogResult = DialogResult.OK;
                    else if (mode == 4 && long.TryParse(sval, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out v2)) DialogResult = DialogResult.OK;
                }
            };
            #endregion
            #region 0~9
            btn0.MouseClick += (o, s) => { sval += "0"; SetText(); };
            btn1.MouseClick += (o, s) => { sval += "1"; SetText(); };
            btn2.MouseClick += (o, s) => { sval += "2"; SetText(); };
            btn3.MouseClick += (o, s) => { sval += "3"; SetText(); };
            btn4.MouseClick += (o, s) => { sval += "4"; SetText(); };
            btn5.MouseClick += (o, s) => { sval += "5"; SetText(); };
            btn6.MouseClick += (o, s) => { sval += "6"; SetText(); };
            btn7.MouseClick += (o, s) => { sval += "7"; SetText(); };
            btn8.MouseClick += (o, s) => { sval += "8"; SetText(); };
            btn9.MouseClick += (o, s) => { sval += "9"; SetText(); };
            #endregion
            #region Dot
            btnDot.MouseClick += (o, s) =>
            {
                if (sval.IndexOf('.') == -1)
                {
                    if (sval.Length == 0) sval += "0";
                    sval += ".";
                    SetText();
                }
            };
            #endregion
            #region Sign
            btnSign.MouseClick += (o, s) => {
                decimal n = 0;
                if (decimal.TryParse(sval, out n))
                {
                    if (n >= 0 && sval.Substring(0, 1) != "-")
                    {
                        sval = sval.Insert(0, "-");
                        SetText();
                    }
                    else if (n <= 0 && sval.Substring(0, 1) == "-")
                    {
                        sval = sval.Substring(1);
                        SetText();
                    }
                }
            };
            #endregion
            #region Back
            btnBack.MouseClick += (o, s) =>
            {
                if (sval.Length > 0) sval = sval.Substring(0, sval.Length - 1);
                SetText();
            };
            #endregion

            SetExComposited();
        }
        #endregion

        #region Method
        #region SetText
        void SetText()
        {
            if (mode == 4)
            {
                long n = 0;
                if (sval.Length > 0 && long.TryParse(sval, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out n))
                    lbl.Text = sval = n.ToString("X");
                else
                    lbl.Text = sval = "";
            }
            else if (mode == 3)
            {
                lbl.Text = new string(sval.Select(x => '*').ToArray());
            }
            else if (mode == 2)
            {
                decimal n = 0;
                if (sval.Length > 0 && decimal.TryParse(sval, out n))
                {
                    if (valueType == typeof(float)) { if (minF1.HasValue && maxF1.HasValue) n = Constrain(n, Convert.ToDecimal(minF1.Value), Convert.ToDecimal(maxF1.Value)); }
                    else if (valueType == typeof(double)) { if (minF2.HasValue && maxF2.HasValue) n = Constrain(n, Convert.ToDecimal(minF2.Value), Convert.ToDecimal(maxF2.Value)); }
                    else if (valueType == typeof(decimal)) { if (minF3.HasValue && maxF3.HasValue) n = Constrain(n, minF3.Value, maxF3.Value); }

                    if (sval.Last() == '.') sval = lbl.Text = n.ToString() + ".";
                    else sval = lbl.Text = n.ToString();
                }
                else sval = lbl.Text = "";
            }
            else if (mode == 1)
            {
                long n = 0;
                if (sval.Length > 0 && long.TryParse(sval, out n))
                {
                    if (valueType == typeof(sbyte)) { if (minI8.HasValue && maxI8.HasValue) n = Constrain(n, minI8.Value, maxI8.Value); }
                    else if (valueType == typeof(short)) { if (minI16.HasValue && maxI16.HasValue) n = Constrain(n, minI16.Value, maxI16.Value); }
                    else if (valueType == typeof(int)) { if (minI32.HasValue && maxI32.HasValue) n = Constrain(n, minI32.Value, maxI32.Value); }
                    else if (valueType == typeof(long)) { if (minI64.HasValue && maxI64.HasValue) n = Constrain(n, minI64.Value, maxI64.Value); }

                    sval = lbl.Text = n.ToString();
                }
                else sval = lbl.Text = "";
            }
            else if (mode == 0)
            {
                ulong n = 0;
                if (sval.Length > 0 && ulong.TryParse(sval, out n))
                {
                    if (valueType == typeof(byte)) { if (minU8.HasValue && maxU8.HasValue) n = Constrain(n, minU8.Value, maxU8.Value); }
                    else if (valueType == typeof(ushort)) { if (minU16.HasValue && maxU16.HasValue) n = Constrain(n, minU16.Value, maxU16.Value); }
                    else if (valueType == typeof(uint)) { if (minU32.HasValue && maxU32.HasValue) n = Constrain(n, minU32.Value, maxU32.Value); }
                    else if (valueType == typeof(ulong)) { if (minU64.HasValue && maxU64.HasValue) n = Constrain(n, minU64.Value, maxU64.Value); }

                    sval = lbl.Text = n.ToString();
                }
                else sval = lbl.Text = "";
            }
        }
        #endregion

        #region show
        void show(string Title, Action act1, Action<DialogResult> act2)
        {
            Theme = GetCallerFormTheme();

            foreach (var c in tpnl.Controls)
            {
                if (c is DvButton) ((DvButton)c).IconSize = ButtonIconSize;
                if (c is DvLabel) ((DvLabel)c).IconSize = ButtonIconSize;
                if (c is DvToggleButton) ((DvToggleButton)c).IconSize = ButtonIconSize;
            }

            {
                var c = Theme.ButtonColor.BrightnessTransmit(Theme.KeySpecialButtonBrightness);
                btnEnter.ButtonColor = c;
                btnBack.ButtonColor = c;
                btnClear.ButtonColor = c;
                btnSign.ButtonColor = c;
            }

            var s = "";
            if (valueType == typeof(byte) && minU8.HasValue && maxU8.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minU8.Value, maxU8.Value);
            else if (valueType == typeof(ushort) && minU16.HasValue && maxU16.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minU16.Value, maxU16.Value);
            else if (valueType == typeof(uint) && minU32.HasValue && maxU32.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minU32.Value, maxU32.Value);
            else if (valueType == typeof(ulong) && minU64.HasValue && maxU64.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minU64.Value, maxU64.Value);
            else if (valueType == typeof(sbyte) && minI8.HasValue && maxI8.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minI8.Value, maxI8.Value);
            else if (valueType == typeof(short) && minI16.HasValue && maxI16.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minI16.Value, maxI16.Value);
            else if (valueType == typeof(int) && minI32.HasValue && maxI32.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minI32.Value, maxI32.Value);
            else if (valueType == typeof(long) && minI64.HasValue && maxI64.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minI64.Value, maxI64.Value);
            else if (valueType == typeof(float) && minF1.HasValue && maxF1.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minF1.Value, maxF1.Value);
            else if (valueType == typeof(double) && minF2.HasValue && maxF2.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minF2.Value, maxF2.Value);
            else if (valueType == typeof(decimal) && minF3.HasValue && maxF3.HasValue) s = string.Format(" [ {0} ~ {1} ] ", minF3.Value, maxF3.Value);

            this.Title = Title + s;
            this.Text = Title;
            act1();

            act2(this.ShowDialog());
        }
        #endregion
        #region ShowKeypad
        public T? ShowKeypad<T>(string Title, T? value, T? min, T? max) where T : struct
        {
            TitleIconString = "fa-grip-vertical";

            T? ret = null;

            this.Width = Math.Max(240, Width);
            this.Height = Math.Max(320, Height);

            minI8 = null; minI16 = null; minI32 = null; minI64 = null;
            minU8 = null; minU16 = null; minU32 = null; minU64 = null;
            minF1 = null; minF2 = null; minF3 = null;

            maxI8 = null; maxI16 = null; maxI32 = null; maxI64 = null;
            maxU8 = null; maxU16 = null; maxU32 = null; maxU64 = null;
            maxF1 = null; maxF2 = null; maxF3 = null;

            var m = -1;
            if (typeof(T) == typeof(sbyte)) { valueType = typeof(sbyte); m = 1; minI8 = (sbyte?)(object)min; maxI8 = (sbyte?)(object)max; }
            else if (typeof(T) == typeof(short)) { valueType = typeof(short); m = 1; minI16 = (short?)(object)min; maxI16 = (short?)(object)max; }
            else if (typeof(T) == typeof(int)) { valueType = typeof(int); m = 1; minI32 = (int?)(object)min; maxI32 = (int?)(object)max; }
            else if (typeof(T) == typeof(long)) { valueType = typeof(long); m = 1; minI64 = (long?)(object)min; maxI64 = (long?)(object)max; }
            else if (typeof(T) == typeof(byte)) { valueType = typeof(byte); m = 0; minU8 = (byte?)(object)min; maxU8 = (byte?)(object)max; }
            else if (typeof(T) == typeof(ushort)) { valueType = typeof(ushort); m = 0; minU16 = (ushort?)(object)min; maxU16 = (ushort?)(object)max; }
            else if (typeof(T) == typeof(uint)) { valueType = typeof(uint); m = 0; minU32 = (uint?)(object)min; maxU32 = (uint?)(object)max; }
            else if (typeof(T) == typeof(ulong)) { valueType = typeof(ulong); m = 0; minU64 = (ulong?)(object)min; maxU64 = (ulong?)(object)max; }
            else if (typeof(T) == typeof(float)) { valueType = typeof(float); m = 2; minF1 = (float?)(object)min; maxF1 = (float?)(object)max; }
            else if (typeof(T) == typeof(double)) { valueType = typeof(double); m = 2; minF2 = (double?)(object)min; maxF2 = (double?)(object)max; }
            else if (typeof(T) == typeof(decimal)) { valueType = typeof(decimal); m = 2; minF3 = (decimal?)(object)min; maxF3 = (decimal?)(object)max; }
            else { valueType = null; m = -1; throw new Exception("숫자 자료형이 아닙니다"); }

            if (m != -1)
            {
                show(Title, () =>
                {
                    if (m == 0)
                    {
                        mode = 0;
                        sval = "";
                        svalOrigin = value.HasValue ? value.Value.ToString() : "";
                        lbl.Text = value.HasValue ? value.Value.ToString() : "";

                        #region Controls
                        tpnl.Controls.Clear();

                        tpnl.Controls.Add(lbl, 0, 0, 4, 1);
                        tpnl.Controls.Add(btn7, 0, 1);
                        tpnl.Controls.Add(btn8, 1, 1);
                        tpnl.Controls.Add(btn9, 2, 1);
                        tpnl.Controls.Add(btnBack, 3, 1);
                        tpnl.Controls.Add(btn4, 0, 2);
                        tpnl.Controls.Add(btn5, 1, 2);
                        tpnl.Controls.Add(btn6, 2, 2);
                        tpnl.Controls.Add(btnClear, 3, 2);
                        tpnl.Controls.Add(btn1, 0, 3);
                        tpnl.Controls.Add(btn2, 1, 3);
                        tpnl.Controls.Add(btn3, 2, 3);
                        tpnl.Controls.Add(btnEnter, 3, 3, 1, 2);
                        tpnl.Controls.Add(btn0, 0, 4, 3, 1);
                        #endregion
                    }
                    else if (m == 1)
                    {
                        mode = 1;
                        sval = "";
                        svalOrigin = value.HasValue ? value.Value.ToString() : "";
                        lbl.Text = value.HasValue ? value.Value.ToString() : "";

                        if (min.HasValue && max.HasValue && Convert.ToInt64((object)min.Value) >= 0 && Convert.ToInt64((object)max.Value) >= 0)
                        {
                            #region Controls
                            tpnl.Controls.Clear();

                            tpnl.Controls.Add(lbl, 0, 0, 4, 1);
                            tpnl.Controls.Add(btn7, 0, 1);
                            tpnl.Controls.Add(btn8, 1, 1);
                            tpnl.Controls.Add(btn9, 2, 1);
                            tpnl.Controls.Add(btnBack, 3, 1);
                            tpnl.Controls.Add(btn4, 0, 2);
                            tpnl.Controls.Add(btn5, 1, 2);
                            tpnl.Controls.Add(btn6, 2, 2);
                            tpnl.Controls.Add(btnClear, 3, 2);
                            tpnl.Controls.Add(btn1, 0, 3);
                            tpnl.Controls.Add(btn2, 1, 3);
                            tpnl.Controls.Add(btn3, 2, 3);
                            tpnl.Controls.Add(btnEnter, 3, 3, 1, 2);
                            tpnl.Controls.Add(btn0, 0, 4, 3, 1);
                            #endregion
                        }
                        else
                        {
                            #region Controls
                            tpnl.Controls.Clear();

                            tpnl.Controls.Add(lbl, 0, 0, 4, 1);
                            tpnl.Controls.Add(btn7, 0, 1);
                            tpnl.Controls.Add(btn8, 1, 1);
                            tpnl.Controls.Add(btn9, 2, 1);
                            tpnl.Controls.Add(btnBack, 3, 1);
                            tpnl.Controls.Add(btn4, 0, 2);
                            tpnl.Controls.Add(btn5, 1, 2);
                            tpnl.Controls.Add(btn6, 2, 2);
                            tpnl.Controls.Add(btnClear, 3, 2);
                            tpnl.Controls.Add(btn1, 0, 3);
                            tpnl.Controls.Add(btn2, 1, 3);
                            tpnl.Controls.Add(btn3, 2, 3);
                            tpnl.Controls.Add(btnSign, 3, 3);
                            tpnl.Controls.Add(btn0, 0, 4, 3, 1);
                            tpnl.Controls.Add(btnEnter, 3, 4);
                            #endregion
                        }
                    }
                    else if (m == 2)
                    {
                        mode = 2;
                        sval = "";
                        svalOrigin = value.HasValue ? value.Value.ToString() : "";
                        lbl.Text = value.HasValue ? value.Value.ToString() : "";

                        if (min.HasValue && max.HasValue && Convert.ToDecimal((object)min.Value) >= 0 && Convert.ToDecimal((object)max.Value) >= 0)
                        {
                            #region Controls
                            tpnl.Controls.Clear();

                            tpnl.Controls.Add(lbl, 0, 0, 4, 1);
                            tpnl.Controls.Add(btn7, 0, 1);
                            tpnl.Controls.Add(btn8, 1, 1);
                            tpnl.Controls.Add(btn9, 2, 1);
                            tpnl.Controls.Add(btnBack, 3, 1);
                            tpnl.Controls.Add(btn4, 0, 2);
                            tpnl.Controls.Add(btn5, 1, 2);
                            tpnl.Controls.Add(btn6, 2, 2);
                            tpnl.Controls.Add(btnClear, 3, 2);
                            tpnl.Controls.Add(btn1, 0, 3);
                            tpnl.Controls.Add(btn2, 1, 3);
                            tpnl.Controls.Add(btn3, 2, 3);
                            tpnl.Controls.Add(btnEnter, 3, 3, 1, 2);
                            tpnl.Controls.Add(btn0, 0, 4, 2, 1);
                            tpnl.Controls.Add(btnDot, 2, 4);
                            #endregion
                        }
                        else
                        {
                            #region Controls
                            tpnl.Controls.Clear();

                            tpnl.Controls.Add(lbl, 0, 0, 4, 1);
                            tpnl.Controls.Add(btn7, 0, 1);
                            tpnl.Controls.Add(btn8, 1, 1);
                            tpnl.Controls.Add(btn9, 2, 1);
                            tpnl.Controls.Add(btnBack, 3, 1);
                            tpnl.Controls.Add(btn4, 0, 2);
                            tpnl.Controls.Add(btn5, 1, 2);
                            tpnl.Controls.Add(btn6, 2, 2);
                            tpnl.Controls.Add(btnClear, 3, 2);
                            tpnl.Controls.Add(btn1, 0, 3);
                            tpnl.Controls.Add(btn2, 1, 3);
                            tpnl.Controls.Add(btn3, 2, 3);
                            tpnl.Controls.Add(btnSign, 3, 3);
                            tpnl.Controls.Add(btn0, 0, 4, 2, 1);
                            tpnl.Controls.Add(btnDot, 2, 4);
                            tpnl.Controls.Add(btnEnter, 3, 4);
                            #endregion
                        }
                    }

                }, (result) =>
                {
                    if (result == DialogResult.OK)
                    {
                        if (valueType == typeof(sbyte)) ret = ((T)(object)Convert.ToSByte(Constrain(Convert.ToInt64(sval), minI8 ?? sbyte.MinValue, maxI8 ?? sbyte.MaxValue)));
                        else if (valueType == typeof(short)) ret = ((T)(object)Convert.ToInt16(Constrain(Convert.ToInt64(sval), minI16 ?? short.MinValue, maxI16 ?? short.MaxValue)));
                        else if (valueType == typeof(int)) ret = ((T)(object)Convert.ToInt32(Constrain(Convert.ToInt64(sval), minI32 ?? int.MinValue, maxI32 ?? int.MaxValue)));
                        else if (valueType == typeof(long)) ret = ((T)(object)Convert.ToInt64(Constrain(Convert.ToInt64(sval), minI64 ?? long.MinValue, maxI64 ?? long.MaxValue)));
                        else if (valueType == typeof(byte)) ret = ((T)(object)Convert.ToByte(Constrain(Convert.ToUInt64(sval), minU8 ?? byte.MinValue, maxU8 ?? byte.MaxValue)));
                        else if (valueType == typeof(ushort)) ret = ((T)(object)Convert.ToUInt16(Constrain(Convert.ToUInt64(sval), minU16 ?? ushort.MinValue, maxU16 ?? ushort.MaxValue)));
                        else if (valueType == typeof(uint)) ret = ((T)(object)Convert.ToUInt32(Constrain(Convert.ToUInt64(sval), minU32 ?? uint.MinValue, maxU32 ?? uint.MaxValue)));
                        else if (valueType == typeof(ulong)) ret = ((T)(object)Convert.ToUInt64(Constrain(Convert.ToUInt64(sval), minU64 ?? ulong.MinValue, maxU64 ?? ulong.MaxValue)));
                        else if (valueType == typeof(float)) ret = ((T)(object)Convert.ToSingle(Constrain(Convert.ToSingle(sval), minF1 ?? float.MinValue, maxF1 ?? float.MaxValue)));
                        else if (valueType == typeof(double)) ret = ((T)(object)Convert.ToDouble(Constrain(Convert.ToDouble(sval), minF2 ?? double.MinValue, maxF2 ?? double.MaxValue)));
                        else if (valueType == typeof(decimal)) ret = ((T)(object)Convert.ToDecimal(Constrain(Convert.ToDecimal(sval), minF3 ?? decimal.MinValue, maxF3 ?? decimal.MaxValue)));
                    }
                    else ret = null;
                });
            }

            return ret;
        }

        public T? ShowKeypad<T>(string Title) where T : struct => ShowKeypad<T>(Title, null, null, null);
        public T? ShowKeypad<T>(string Title, T? value) where T : struct => ShowKeypad<T>(Title, value, null, null);
        #endregion
        #region ShowPassword
        public string? ShowPassword(string Title, string value)
        {
            TitleIconString = "fa-key";

            string? ret = null;
            this.Width = Math.Max(240, Width);
            this.Height = Math.Max(320, Height);

            show(Title, () =>
            {
                mode = 3;
                sval = "";
                svalOrigin = value ?? "";
                lbl.Text = value != null ? string.Concat(value.Select(x => "*").ToArray()) : "";

                #region Controls
                tpnl.Controls.Clear();

                tpnl.Controls.Add(lbl, 0, 0); tpnl.SetColumnSpan(lbl, 4);
                tpnl.Controls.Add(btn7, 0, 1);
                tpnl.Controls.Add(btn8, 1, 1);
                tpnl.Controls.Add(btn9, 2, 1);
                tpnl.Controls.Add(btnBack, 3, 1);
                tpnl.Controls.Add(btn4, 0, 2);
                tpnl.Controls.Add(btn5, 1, 2);
                tpnl.Controls.Add(btn6, 2, 2);
                tpnl.Controls.Add(btnClear, 3, 2);
                tpnl.Controls.Add(btn1, 0, 3);
                tpnl.Controls.Add(btn2, 1, 3);
                tpnl.Controls.Add(btn3, 2, 3);
                tpnl.Controls.Add(btnEnter, 3, 3); tpnl.SetRowSpan(btnEnter, 2);
                tpnl.Controls.Add(btn0, 0, 4); tpnl.SetColumnSpan(btn0, 3);
                #endregion

            }, (result) =>
            {
                if (result == DialogResult.OK) ret = (sval);
                else ret = (null);
            });

            return ret;
        }

        public string? ShowPassword(string Title) => ShowPassword(Title, null);
        #endregion

        #region Constrain
        /// <summary>
        /// 제한값 구하기
        /// </summary>
        /// <param name="val">현재값</param>
        /// <param name="min">최소값</param>
        /// <param name="max">최대값</param>
        /// <returns>제한값</returns>


        byte Constrain(byte val, byte min, byte max)
        {
            byte ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }

        short Constrain(short val, short min, short max)
        {
            short ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }

        static int Constrain(int val, int min, int max)
        {
            int ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }

        long Constrain(long val, long min, long max)
        {
            long ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }

        sbyte Constrain(sbyte val, sbyte min, sbyte max)
        {
            sbyte ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }

        ushort Constrain(ushort val, ushort min, ushort max)
        {
            ushort ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }

        uint Constrain(uint val, uint min, uint max)
        {
            uint ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }

        ulong Constrain(ulong val, ulong min, ulong max)
        {
            ulong ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }


        /// <summary>
        /// 제한값 구하기
        /// </summary>
        /// <param name="val">현재값</param>
        /// <param name="min">최소값</param>
        /// <param name="max">최대값</param>
        /// <returns>제한값</returns>
        double Constrain(double val, double min, double max)
        {
            double ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }

        float Constrain(float val, float min, float max)
        {
            float ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }

        decimal Constrain(decimal val, decimal min, decimal max)
        {
            decimal ret = val;
            if (ret < min) ret = min;
            if (ret > max) ret = max;
            if (min > max) ret = min;
            return ret;
        }
        #endregion
        #endregion
    }
}
