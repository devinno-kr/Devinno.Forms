using Devinno.Forms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Dialogs
{
    public partial class DvInputBox : DvForm
    {
        #region Properties
        public int ColumnCount { get; set; } = 1;

        public int ItemWidth { get; set; } = 174;
        public int ItemHeight { get; set; } = 30;

        public int MinWidth { get; set; } = 200;
        public int MinHeight { get; set; } = 100;

        public bool UseEnterKey { get; set; } = false;
        #endregion

        #region Constructor
        public DvInputBox()
        {
            InitializeComponent();

            SetExComposited();

            btnOk.ButtonClick += (o, s) => { if (ValidCheck()) DialogResult = DialogResult.OK; };
            btnCancel.ButtonClick += (o, s) => { DialogResult = DialogResult.Cancel; };
        }
        #endregion

        #region Method
        #region ValidCheck
        bool ValidCheck()
        {
            bool ret = true;

            foreach (var c in tpnl.Controls.Cast<Control>())
            {
                if (c is DvValueInputNumber<byte>) ret &= ((DvValueInputNumber<byte>)c).Error == InputError.None;
                else if (c is DvValueInputNumber<ushort>) ret &= ((DvValueInputNumber<ushort>)c).Error == InputError.None;
                else if (c is DvValueInputNumber<uint>) ret &= ((DvValueInputNumber<uint>)c).Error == InputError.None;
                else if (c is DvValueInputNumber<ulong>) ret &= ((DvValueInputNumber<ulong>)c).Error == InputError.None;
                else if (c is DvValueInputNumber<sbyte>) ret &= ((DvValueInputNumber<sbyte>)c).Error == InputError.None;
                else if (c is DvValueInputNumber<short>) ret &= ((DvValueInputNumber<short>)c).Error == InputError.None;
                else if (c is DvValueInputNumber<int>) ret &= ((DvValueInputNumber<int>)c).Error == InputError.None;
                else if (c is DvValueInputNumber<long>) ret &= ((DvValueInputNumber<long>)c).Error == InputError.None;
                else if (c is DvValueInputNumber<float>) ret &= ((DvValueInputNumber<float>)c).Error == InputError.None;
                else if (c is DvValueInputNumber<double>) ret &= ((DvValueInputNumber<double>)c).Error == InputError.None;
                else if (c is DvValueInputNumber<decimal>) ret &= ((DvValueInputNumber<decimal>)c).Error == InputError.None;
                else if (c is DvValueInputText) ret &= true;
                else if (c is DvValueInputBool) ret &= true;
                else if (c is DvValueInputWheel) ret &= ((DvValueInputWheel)c).SelectedIndex != -1;
            }

            return ret;
        }
        #endregion
        #region CheckProp
        bool CheckProp(PropertyInfo prop, Dictionary<string, InputBoxInfo> infos)
        {
            bool ret = false;
            var p = infos != null ? (infos.ContainsKey(prop.Name) ? infos[prop.Name] : null) : null;

            if (p != null && p.Items != null && p.Items.Count > 0) ret = true;
            if (prop.PropertyType == typeof(sbyte)) ret = true;
            else if (prop.PropertyType == typeof(short)) ret = true;
            else if (prop.PropertyType == typeof(int)) ret = true;
            else if (prop.PropertyType == typeof(long)) ret = true;
            else if (prop.PropertyType == typeof(byte)) ret = true;
            else if (prop.PropertyType == typeof(ushort)) ret = true;
            else if (prop.PropertyType == typeof(uint)) ret = true;
            else if (prop.PropertyType == typeof(ulong)) ret = true;
            else if (prop.PropertyType == typeof(float)) ret = true;
            else if (prop.PropertyType == typeof(double)) ret = true;
            else if (prop.PropertyType == typeof(decimal)) ret = true;
            else if (prop.PropertyType == typeof(string)) ret = true;
            else if (prop.PropertyType == typeof(bool)) ret = true;
            else if (prop.PropertyType.IsEnum) ret = true;

            return ret && prop.CanWrite && prop.CanRead && !Attribute.IsDefined(prop, typeof(InputBoxIgnoreAttribute));
        }
        #endregion
        #region OK
        void OK()
        {
            if (ValidCheck()) DialogResult = DialogResult.OK;
        }
        #endregion

        #region ShowInputBox
        public T? ShowInputBox<T>(string Title, T value, Dictionary<string, InputBoxInfo> infos) where T : class
        {
            Theme = GetCallerFormTheme();

            T? ret = default(T);

            #region Var
            this.Title = this.Text = Title;

            var ps = typeof(T).GetProperties();
            var props = ps.Where(x => CheckProp(x, infos)).ToList();
            var RowCount = Convert.ToInt32(Math.Ceiling((double)props.Count / (double)ColumnCount));
            var csz = 100F / ColumnCount;
            var rsz = 100F / RowCount;

            this.Width = Math.Max(MinWidth, 10 + (ColumnCount * (ItemWidth + 6)) + 10);
            this.Height = Math.Max(MinHeight, TitleHeight + 10 + (RowCount * (ItemHeight + 6)) + 10 + 36 + 10);
            #endregion
            #region Layout
            tpnl.RowStyles.Clear();
            tpnl.ColumnStyles.Clear();

            for (int i = 0; i < ColumnCount; i++) tpnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, csz));
            for (int i = 0; i < RowCount; i++) tpnl.RowStyles.Add(new RowStyle(SizeType.Percent, rsz));
            #endregion
            #region New
            tpnl.Controls.Clear();

            var nstr = props.Select(x => infos.ContainsKey(x.Name) ? infos[x.Name].Title : x.Name).OrderByDescending(x => x.Length).FirstOrDefault();
            var nsz = 0;
            using (var g = CreateGraphics()) nsz = Math.Max(60, Convert.ToInt32(g.MeasureString(nstr, Font).Width + 20));

            foreach (var v in props)
            {
                var p = infos.ContainsKey(v.Name) ? infos[v.Name] : null;
                
                #region Var
                var title = p != null ? p.Title : v.Name;
                var count = tpnl.Controls.Count;
                var col = count % ColumnCount;
                var row = count / ColumnCount;
                var min = p != null ? p.Minimum : null;
                var max = p != null ? p.Maximum : null;
                var unit = p != null ? p.Unit : null;
                #endregion

                if (p != null && p.Items != null && p.Items.Count > 0)
                {
                    #region Selector
                    var c = new DvValueInputWheel { Name = v.Name, Title = title, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    c.Items.AddRange(p.Items);
                    if (value != null)
                    {
                        var val = v.GetValue((object)value);
                        var itm = c.Items.Where(x => val != null && val.Equals(x.Value)).FirstOrDefault();
                        if (itm != null) c.SelectedIndex = c.Items.IndexOf(itm);
                    }
                    #endregion
                }
                else if (v.PropertyType == typeof(sbyte))
                {
                    #region sbyte
                    var c = new DvValueInputNumber<sbyte> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToSByte(min.Value) : null, Maximum = max != null ? Convert.ToSByte(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (sbyte)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(short))
                {
                    #region short
                    var c = new DvValueInputNumber<short> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToInt16(min.Value) : null, Maximum = max != null ? Convert.ToInt16(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (short)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(int))
                {
                    #region int
                    var c = new DvValueInputNumber<int> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToInt32(min.Value) : null, Maximum = max != null ? Convert.ToInt32(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (int)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(long))
                {
                    #region long
                    var c = new DvValueInputNumber<long> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToInt64(min.Value) : null, Maximum = max != null ? Convert.ToInt64(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (long)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(byte))
                {
                    #region byte
                    var c = new DvValueInputNumber<byte> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToByte(min.Value) : null, Maximum = max != null ? Convert.ToByte(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (byte)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(ushort))
                {
                    #region ushort
                    var c = new DvValueInputNumber<ushort> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToUInt16(min.Value) : null, Maximum = max != null ? Convert.ToUInt16(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (ushort)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(uint))
                {
                    #region uint
                    var c = new DvValueInputNumber<uint> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToUInt32(min.Value) : null, Maximum = max != null ? Convert.ToUInt32(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (uint)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(ulong))
                {
                    #region ulong
                    var c = new DvValueInputNumber<ulong> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToUInt64(min.Value) : null, Maximum = max != null ? Convert.ToUInt64(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (ulong)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(float))
                {
                    #region float
                    var c = new DvValueInputNumber<float> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToSingle(min.Value) : null, Maximum = max != null ? Convert.ToSingle(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (float)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(double))
                {
                    #region double
                    var c = new DvValueInputNumber<double> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToDouble(min.Value) : null, Maximum = max != null ? Convert.ToDouble(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (double)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(decimal))
                {
                    #region decimal
                    var c = new DvValueInputNumber<decimal> { Name = v.Name, Title = title, Minimum = min != null ? Convert.ToDecimal(min.Value) : null, Maximum = max != null ? Convert.ToDecimal(max.Value) : null, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (decimal)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(string))
                {
                    #region string
                    var c = new DvValueInputText { Name = v.Name, Title = title, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (string)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType == typeof(bool))
                {
                    #region bool
                    var c = new DvValueInputBool { Name = v.Name, Title = title, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    if (value != null) c.Value = (bool)v.GetValue((object)value);
                    #endregion
                }
                else if (v.PropertyType.IsEnum)
                {
                    #region Enums
                    var c = new DvValueInputWheel { Name = v.Name, Title = title, TitleWidth = nsz, Tag = new InputBoxTag() { p = v, info = p }, Unit = unit, Dock = DockStyle.Fill };
                    tpnl.Controls.Add(c, col, row);
                    c.Items.AddRange(Enum.GetValues(v.PropertyType).Cast<object>().Select(x => new TextIcon() { Text = x.ToString(), Value = x }));

                    if (value != null)
                    {
                        var val = v.GetValue((object)value);
                        var itm = c.Items.Where(x => val != null && val.Equals(x.Value)).FirstOrDefault();
                        if (itm != null) c.SelectedIndex = c.Items.IndexOf(itm);
                    }
                    #endregion
                }
            }
            #endregion

            if (this.ShowDialog() == DialogResult.OK)
            {
                var v = (T)Activator.CreateInstance(typeof(T));

                #region Value
                foreach (var c in tpnl.Controls.Cast<Control>())
                {
                    var tag = c.Tag as InputBoxTag;
                    if (tag != null)
                    {
                        var p = tag.p;
                        var info = tag.info;

                        if (c is DvValueInputNumber<byte>) p.SetValue(v, ((DvValueInputNumber<byte>)c).Value);
                        else if (c is DvValueInputNumber<ushort>) p.SetValue(v, ((DvValueInputNumber<ushort>)c).Value);
                        else if (c is DvValueInputNumber<uint>) p.SetValue(v, ((DvValueInputNumber<uint>)c).Value);
                        else if (c is DvValueInputNumber<ulong>) p.SetValue(v, ((DvValueInputNumber<ulong>)c).Value);
                        else if (c is DvValueInputNumber<sbyte>) p.SetValue(v, ((DvValueInputNumber<sbyte>)c).Value);
                        else if (c is DvValueInputNumber<short>) p.SetValue(v, ((DvValueInputNumber<short>)c).Value);
                        else if (c is DvValueInputNumber<int>) p.SetValue(v, ((DvValueInputNumber<int>)c).Value);
                        else if (c is DvValueInputNumber<long>) p.SetValue(v, ((DvValueInputNumber<long>)c).Value);
                        else if (c is DvValueInputNumber<float>) p.SetValue(v, ((DvValueInputNumber<float>)c).Value);
                        else if (c is DvValueInputNumber<double>) p.SetValue(v, ((DvValueInputNumber<double>)c).Value);
                        else if (c is DvValueInputNumber<decimal>) p.SetValue(v, ((DvValueInputNumber<decimal>)c).Value);
                        else if (c is DvValueInputText) p.SetValue(v, ((DvValueInputText)c).Value);
                        else if (c is DvValueInputBool) p.SetValue(v, ((DvValueInputBool)c).Value);
                        else if (c is DvValueInputWheel)
                        {
                            var vc = c as DvValueInputWheel;
                            var vt = vc.Items[vc.SelectedIndex].Value;
                            p.SetValue(v, vc.Items[vc.SelectedIndex].Value);
                        }
                    }
                }
                #endregion

                ret = v;
            }

            return ret;
        }

        public T? ShowInputBox<T>(string Title) where T : class => ShowInputBox<T>(Title, null, null);
        public T? ShowInputBox<T>(string Title, T value) where T : class => ShowInputBox<T>(Title, value, null);
        public T? ShowInputBox<T>(string Title, Dictionary<string, InputBoxInfo> infos) where T : class => ShowInputBox<T>(Title, null, infos);
        #endregion
        #region Show[Type]
        #region show
        private void show(string Title, Control c, Action actReturn)
        {
            Theme = GetCallerFormTheme();

            #region Var
            this.Title = this.Text = Title;

            this.Width = Math.Max(MinWidth, 10 + (ColumnCount * (ItemWidth + 6)) + 10);
            this.Height = Math.Max(MinHeight, TitleHeight + 10 + (ItemHeight + 6) + 10 + 36 + 10);
            #endregion
            #region Layout
            tpnl.RowStyles.Clear();
            tpnl.ColumnStyles.Clear();

            tpnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tpnl.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            #endregion
            #region New
            tpnl.Controls.Clear();
            tpnl.Controls.Add(c, 0, 0);
            #endregion

            if (UseEnterKey)
            {
                if (c is DvValueInputText) { ((DvValueInputText)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<byte>) { ((DvValueInputNumber<byte>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<ushort>) { ((DvValueInputNumber<ushort>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<uint>) { ((DvValueInputNumber<uint>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<ulong>) { ((DvValueInputNumber<ulong>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<sbyte>) { ((DvValueInputNumber<sbyte>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<short>) { ((DvValueInputNumber<short>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<int>) { ((DvValueInputNumber<int>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<long>) { ((DvValueInputNumber<long>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<float>) { ((DvValueInputNumber<float>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<double>) { ((DvValueInputNumber<double>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }
                else if (c is DvValueInputNumber<decimal>) { ((DvValueInputNumber<decimal>)c).OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) OK(); }; }

            }

            if (this.ShowDialog() == DialogResult.OK)
            {
                if (actReturn != null) actReturn();
            }
        }
        #endregion
        
        #region ShowString
        public string? ShowString(string Title, string? value = null)
        {
            string? ret = null;

            show(Title, new DvValueInputText { Name = "value", Dock = DockStyle.Fill, Value = value },
                () => ret = ((DvValueInputText)tpnl.Controls["value"]).Value);
            
            return ret;
        }
        #endregion
        #region ShowByte
        public byte? ShowByte(string Title, byte? value = null, byte? min = null, byte? max = null, string unit = null)
        {
            byte? ret = null;

            var s = (min.HasValue || max.HasValue ? ("[ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<byte> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<byte>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowUShort
        public ushort? ShowUShort(string Title, ushort? value = null, ushort? min = null, ushort? max = null, string unit = null)
        {
            ushort? ret = null;

            var s = (min.HasValue || max.HasValue ? ("[ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<ushort> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<ushort>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowUInt
        public uint? ShowUInt(string Title, uint? value = null, uint? min = null, uint? max = null, string unit = null)
        {
            uint? ret = null;

            var s = (min.HasValue || max.HasValue ? ("[ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<uint> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<uint>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowULong
        public ulong? ShowULong(string Title, ulong? value = null, ulong? min = null, ulong? max = null, string unit = null)
        {
            ulong? ret = null;

            var s = (min.HasValue || max.HasValue ? ("[ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<ulong> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<ulong>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowSByte
        public sbyte? ShowSByte(string Title, sbyte? value = null, sbyte? min = null, sbyte? max = null, string unit = null)
        {
            sbyte? ret = null;

            var s = (min.HasValue || max.HasValue ? ("[ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<sbyte> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<sbyte>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowShort
        public short? ShowShort(string Title, short? value = null, short? min = null, short? max = null, string unit = null)
        {
            short? ret = null;

            var s = (min.HasValue || max.HasValue ? ("[ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<short> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<short>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowInt
        public int? ShowInt(string Title, int? value = null, int? min = null, int? max = null, string unit = null)
        {
            int? ret = null;

            var s = (min.HasValue || max.HasValue ? ("[ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<int> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<int>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowLong
        public long? ShowLong(string Title, long? value = null, long? min = null, long? max = null, string unit = null)
        {
            long? ret = null;

            var s = (min.HasValue || max.HasValue ? ("[ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<long> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<long>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowFloat
        public float? ShowFloat(string Title, float? value = null, float? min = null, float? max = null, string unit = null)
        {
            float? ret = null;

            var s = (min.HasValue || max.HasValue ? (" [ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<float> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<float>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowDouble
        public double? ShowDouble(string Title, double? value = null, double? min = null, double? max = null, string unit = null)
        {
            double? ret = null;

            var s = (min.HasValue || max.HasValue ? (" [ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<double> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<double>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowDecimal
        public decimal? ShowDecimal(string Title, decimal? value = null, decimal? min = null, decimal? max = null, string unit = null)
        {
            decimal? ret = null;

            var s = (min.HasValue || max.HasValue ? (" [ " + min.ToString() ?? "?") + " ~ " + (max.ToString() ?? "?") + " ]" : "");
            show(Title + s, new DvValueInputNumber<decimal> { Name = "value", Dock = DockStyle.Fill, Value = value, Minimum = min, Maximum = max, Unit = unit, UnitWidth = !string.IsNullOrWhiteSpace(unit) ? 30 : 0 },
                () => ret = ((DvValueInputNumber<decimal>)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #region ShowBool
        public bool? ShowBool(string Title, bool? value = null)
        {
            bool? ret = null;

            show(Title, new DvValueInputBool { Name = "value", Dock = DockStyle.Fill, Value = value.HasValue && value.Value },
                () => ret = ((DvValueInputBool)tpnl.Controls["value"]).Value);

            return ret;
        }
        #endregion
        #endregion
        #endregion
    }

    #region attr : InputBoxIgnore
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class InputBoxIgnoreAttribute : Attribute { }
    #endregion
    #region class : InputBoxInfo
    public class InputBoxInfo
    {
        public decimal? Minimum { get; set; } = null;
        public decimal? Maximum { get; set; } = null;

        public string Title { get; set; }
        public string Unit { get; set; }
        public List<TextIcon> Items { get; set; }
    }
    #endregion
    #region class : InputBoxTag
    internal class InputBoxTag
    {
        public PropertyInfo p { get; set; }
        public InputBoxInfo info { get; set; }
    }
    #endregion
    #region class : EnterKeyDownEventArgs
    public class EnterKeyDownEventArgs : EventArgs
    {
        public Control Control { get; private set; }

        public EnterKeyDownEventArgs(Control c)
        {
            this.Control = c;
        }
    }
    #endregion

}
