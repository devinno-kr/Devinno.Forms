using Devinno.Forms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Dialogs
{
    public partial class DvInputBox : DvForm
    {
        public bool UseEnterKey { get; set; } = false;

        #region Constructor
        public DvInputBox()
        {
            InitializeComponent();

            Fixed = true;

            btnOK.ButtonClick += (o, s) => OK();
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region Method
        #region OK
        void OK()
        {
            if (ValidCheck()) DialogResult = DialogResult.OK;
        }
        #endregion
        #region ValidCheck
        bool ValidCheck()
        {
            bool ret = true;
            foreach (var c in layout.Controls.Cast<Control>().Where(x => x is DvValueInput).Cast<DvValueInput>())
            {
                var tag = c.Tag as InputBoxTag;
                var p = tag.p;
                var info = tag.info;

                if (c.InputStyle == DvInputType.NUMBER)
                {
                    if (p.PropertyType == typeof(byte)) { byte n; c.DrawBorder = !byte.TryParse(c.Value, out n); }
                    else if (p.PropertyType == typeof(ushort)) { ushort n; c.DrawBorder = !ushort.TryParse(c.Value, out n); }
                    else if (p.PropertyType == typeof(uint)) { uint n; c.DrawBorder = !uint.TryParse(c.Value, out n); }
                    else if (p.PropertyType == typeof(ulong)) { ulong n; c.DrawBorder = !ulong.TryParse(c.Value, out n); }
                    else if (p.PropertyType == typeof(sbyte)) { sbyte n; c.DrawBorder = !sbyte.TryParse(c.Value, out n); }
                    else if (p.PropertyType == typeof(short)) { short n; c.DrawBorder = !short.TryParse(c.Value, out n); }
                    else if (p.PropertyType == typeof(int)) { int n; c.DrawBorder = !int.TryParse(c.Value, out n); }
                    else if (p.PropertyType == typeof(long)) { long n; c.DrawBorder = !long.TryParse(c.Value, out n); }
                }
                else if (c.InputStyle == DvInputType.FLOATING)
                {
                    if (p.PropertyType == typeof(float)) { float n; c.DrawBorder = !float.TryParse(c.Value, out n); }
                    else if (p.PropertyType == typeof(double)) { double n; c.DrawBorder = !double.TryParse(c.Value, out n); }
                    else if (p.PropertyType == typeof(decimal)) { decimal n; c.DrawBorder = !decimal.TryParse(c.Value, out n); }
                }
                else if (c.InputStyle == DvInputType.COMBO)
                {
                    c.DrawBorder = c.SelectedIndex == -1;
                }

                ret &= !c.DrawBorder;
            }

            return ret;
        }
        #endregion
        #region ShowInputBox
        public T ShowInputBox<T>(string Title, T value = default(T), Dictionary<string, InputBoxInfo> infos = null)
        {
            #region UI
            var props = typeof(T).GetProperties().Where(x => x.Name != "Id" && x.CanRead && x.CanWrite && !Attribute.IsDefined(x, typeof(InputBoxIgnoreAttribute))).ToList();
            #region DPI Size 
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            pnl.Padding = new Padding(0, m7, 0, 0);
            pnlBtn.Padding = new Padding(m3);
            pnlBtn.Height = Convert.ToInt32(f * 36);
            gpH.Width = gpV.Height = Convert.ToInt32(f * 4);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(300 * f), Convert.ToInt32((40 + 7 + ((3 + 30 + 3) * props.Count) + 4 + 36 + 7) * f));
            #endregion

            #region Column / Row
            layout.ColumnStyles.Clear();
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            layout.ColumnCount = 1;

            layout.RowStyles.Clear();
            for (int row = 0; row < props.Count; row++)
                layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F / (float)props.Count));
            layout.RowCount = props.Count;
            #endregion
            #region Controls
            var ls = layout.Controls.Cast<Control>().ToArray();
            layout.Controls.Clear();

            for (int i = 0; i < props.Count; i++)
            {
                if (i < props.Count)
                {
                    var p = props[i];
                    var info = infos != null && infos.ContainsKey(p.Name) ? infos[p.Name] : null;
                    var c = new DvValueInput() { Name = "in" + p.Name, Dock = DockStyle.Fill, Tag = new InputBoxTag() { p = p, info = info } };
                    c.Margin = new Padding(m3);
                    c.Text = info != null && !string.IsNullOrWhiteSpace(info.Alias) ? info.Alias : p.Name;
                    c.ValueTextChanged += (o, s) => ((DvValueInput)o).DrawBorder = false;
                    c.SelectedIndexChanged += (o, s) => ((DvValueInput)o).DrawBorder = false;
                    c.OriginalTextBox.KeyDown += (o, s) =>
                    {
                        if (UseEnterKey && s.KeyCode == Keys.Enter)
                        {
                            var oc = ((Control)o).Parent;
                            var vls = layout.Controls.Cast<Control>().ToList();
                            var idx = vls.IndexOf(oc);
                            if (idx == vls.Count - 1) OK();
                            else
                            {
                                var cNext = layout.Controls[idx + 1 == layout.Controls.Count ? 0 : idx + 1];
                                if(cNext != null)
                                {
                                    var vc = cNext as DvValueInput;
                                    if (vc.InputStyle != DvInputType.BOOL && vc.InputStyle != DvInputType.COMBO) this.Invoke(new Action(() => vc.OriginalTextBox.Focus()));
                                    else this.Invoke(new Action(() => vc.Focus()));
                                }
                            }
                        }
                    };

                    layout.Controls.Add(c, 0, i);
                }
            }
            
            foreach (var v in ls) v.Dispose();
            #endregion
            #region Props
            int nw = 150;
            var last = layout.Controls.Cast<Control>().OrderBy(x => x.Text.Length).LastOrDefault();
            using (var g = CreateGraphics()) { nw = Convert.ToInt32(Math.Ceiling((g.MeasureString(last.Text, Font).Width) * f)) + 20; }
            foreach (var c in layout.Controls.Cast<Control>().Where(x => x is DvValueInput).Cast<DvValueInput>())
            {
                var tag = c.Tag as InputBoxTag;
                var p = tag.p;
                var info = tag.info;

                c.TitleWidth = nw;
                c.DrawBorder = false;

                if (info != null && info.Items != null)
                {
                    c.InputStyle = DvInputType.COMBO;
                    c.ItemHeight = Convert.ToInt32(30 * f);
                    c.Items.Clear();
                    c.Items.AddRange(info.Items);
                }
                else if (p.PropertyType == typeof(string)) { c.InputStyle = DvInputType.TEXT; c.OriginalTextBox.MaxLength = 100; }
                else if (p.PropertyType == typeof(byte)) { c.InputStyle = DvInputType.NUMBER; c.MinusInput = false; c.OriginalTextBox.MaxLength = 3; }
                else if (p.PropertyType == typeof(ushort)) { c.InputStyle = DvInputType.NUMBER; c.MinusInput = false; c.OriginalTextBox.MaxLength = 5; }
                else if (p.PropertyType == typeof(uint)) { c.InputStyle = DvInputType.NUMBER; c.MinusInput = false; c.OriginalTextBox.MaxLength = 10; }
                else if (p.PropertyType == typeof(ulong)) { c.InputStyle = DvInputType.NUMBER; c.MinusInput = false; c.OriginalTextBox.MaxLength = 19; }
                else if (p.PropertyType == typeof(sbyte)) { c.InputStyle = DvInputType.NUMBER; c.MinusInput = true; c.OriginalTextBox.MaxLength = 4; }
                else if (p.PropertyType == typeof(short)) { c.InputStyle = DvInputType.NUMBER; c.MinusInput = true; c.OriginalTextBox.MaxLength = 6; }
                else if (p.PropertyType == typeof(int)) { c.InputStyle = DvInputType.NUMBER; c.MinusInput = true; c.OriginalTextBox.MaxLength = 11; }
                else if (p.PropertyType == typeof(long)) { c.InputStyle = DvInputType.NUMBER; c.MinusInput = true; c.OriginalTextBox.MaxLength = 20; }
                else if (p.PropertyType == typeof(float)) { c.InputStyle = DvInputType.FLOATING; c.MinusInput = true; c.OriginalTextBox.MaxLength = 10; }
                else if (p.PropertyType == typeof(decimal)) { c.InputStyle = DvInputType.FLOATING; c.MinusInput = true; c.OriginalTextBox.MaxLength = 18; }
                else if (p.PropertyType == typeof(double)) { c.InputStyle = DvInputType.FLOATING; c.MinusInput = true; c.OriginalTextBox.MaxLength = 30; }
                else if (p.PropertyType == typeof(bool)) { c.InputStyle = DvInputType.BOOL; }
                else if (p.PropertyType.IsEnum)
                {
                    c.InputStyle = DvInputType.COMBO;
                    c.ItemHeight = Convert.ToInt32(30 * f);
                    c.Items.Clear();
                    c.Items.AddRange(Enum.GetValues(p.PropertyType).Cast<object>().Select(x => new ComboBoxItem(x.ToString()) { Tag = x }));
                }
            }
            #endregion

            btnOK.Width = btnCancel.Width = Convert.ToInt32(80 * f);
            pnl.Height = Convert.ToInt32(30 * f);
            #endregion
            #region Set
            foreach (var c in layout.Controls.Cast<Control>().Where(x => x is DvValueInput).Cast<DvValueInput>())
            {
                var tag = c.Tag as InputBoxTag;
                var p = tag.p;
                var info = tag.info;
                var val = value == null ? null : p.GetValue(value);

                if (c.InputStyle == DvInputType.NUMBER || c.InputStyle == DvInputType.TEXT || c.InputStyle == DvInputType.FLOATING)
                {
                    c.Value = val == null ? "" : val.ToString();
                }
                else if (c.InputStyle == DvInputType.COMBO)
                {
                    c.SelectedIndex = c.Items.Select(x => x.Text).ToList().IndexOf(val == null ? "" : val.ToString());
                }
                else if (c.InputStyle == DvInputType.BOOL)
                {
                    c.OnOff = val is bool ? (bool)val : false;
                }
            }
            #endregion
            #region Title
            this.Text = this.Title = string.IsNullOrWhiteSpace(Title) ? "항목 입력" : Title;
            Theme = GetCallerFormTheme() ?? Theme;
            #endregion
            #region Focus
            var vf = layout.Controls.Cast<Control>().Where(x => x is DvValueInput).FirstOrDefault();
            if (vf != null)
            {
                var th = new Thread(new ThreadStart(() =>
                {
                    Thread.Sleep(100);
                    if (vf is DvValueInput)
                    {
                        var vc = vf as DvValueInput;
                        if (vc.InputStyle != DvInputType.BOOL && vc.InputStyle != DvInputType.COMBO) this.Invoke(new Action(() => vc.OriginalTextBox.Focus()));
                        else this.Invoke(new Action(() => vc.Focus()));
                    }
                }))
                { IsBackground = true };
                th.Start();
            }
            #endregion

            T ret = default(T);
            if (this.ShowDialog() == DialogResult.OK)
            {
                var v = (T)Activator.CreateInstance(typeof(T));
                #region Value
                foreach (var c in layout.Controls.Cast<Control>().Where(x => x is DvValueInput).Cast<DvValueInput>())
                {
                    var tag = c.Tag as InputBoxTag;
                    var p = tag.p;
                    var info = tag.info;

                    if (c.InputStyle == DvInputType.NUMBER)
                    {
                        if (p.PropertyType == typeof(byte)) p.SetValue(v, Convert.ToByte(c.Value));
                        else if (p.PropertyType == typeof(ushort)) p.SetValue(v, Convert.ToUInt16(c.Value));
                        else if (p.PropertyType == typeof(uint)) p.SetValue(v, Convert.ToUInt32(c.Value));
                        else if (p.PropertyType == typeof(ulong)) p.SetValue(v, Convert.ToUInt64(c.Value));
                        else if (p.PropertyType == typeof(sbyte)) p.SetValue(v, Convert.ToSByte(c.Value));
                        else if (p.PropertyType == typeof(short)) p.SetValue(v, Convert.ToByte(c.Value));
                        else if (p.PropertyType == typeof(int)) p.SetValue(v, Convert.ToInt32(c.Value));
                        else if (p.PropertyType == typeof(long)) p.SetValue(v, Convert.ToInt64(c.Value));
                    }
                    else if (c.InputStyle == DvInputType.FLOATING)
                    {
                        if (p.PropertyType == typeof(float)) p.SetValue(v, Convert.ToSingle(c.Value));
                        else if (p.PropertyType == typeof(double)) p.SetValue(v, Convert.ToDouble(c.Value));
                        else if (p.PropertyType == typeof(decimal)) p.SetValue(v, Convert.ToDecimal(c.Value));
                    }
                    else if (c.InputStyle == DvInputType.TEXT)
                    {
                        p.SetValue(v, c.Value);
                    }
                    else if (c.InputStyle == DvInputType.BOOL)
                    {
                        p.SetValue(v, c.OnOff);
                    }
                    else if (c.InputStyle == DvInputType.COMBO)
                    {
                        var vt = c.Items[c.SelectedIndex].Text;

                        if (p.PropertyType == typeof(byte)) p.SetValue(v, Convert.ToByte(vt));
                        else if (p.PropertyType == typeof(ushort)) p.SetValue(v, Convert.ToUInt16(vt));
                        else if (p.PropertyType == typeof(uint)) p.SetValue(v, Convert.ToUInt32(vt));
                        else if (p.PropertyType == typeof(ulong)) p.SetValue(v, Convert.ToUInt64(vt));
                        else if (p.PropertyType == typeof(sbyte)) p.SetValue(v, Convert.ToSByte(vt));
                        else if (p.PropertyType == typeof(short)) p.SetValue(v, Convert.ToByte(vt));
                        else if (p.PropertyType == typeof(int)) p.SetValue(v, Convert.ToInt32(vt));
                        else if (p.PropertyType == typeof(long)) p.SetValue(v, Convert.ToInt64(vt));
                        else if (p.PropertyType == typeof(float)) p.SetValue(v, Convert.ToSingle(vt));
                        else if (p.PropertyType == typeof(double)) p.SetValue(v, Convert.ToDouble(vt));
                        else if (p.PropertyType == typeof(decimal)) p.SetValue(v, Convert.ToDecimal(vt));
                        else if (p.PropertyType == typeof(string)) p.SetValue(v, vt);
                        else if (p.PropertyType.IsEnum) p.SetValue(v, c.Items[c.SelectedIndex].Tag);
                    }
                }
                #endregion
                ret = v;
            }
            return ret;
        }
        #endregion

        public byte? ShowByte(string Title, string Name = null, byte? value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            byte? ret = null;
            var v = ShowInputBox(Title, value.HasValue ? new ReturnByte() { Value = value.Value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        public short? ShowShort(string Title, string Name = null, short? value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            short? ret = null;
            var v = ShowInputBox(Title, value.HasValue ? new ReturnShort() { Value = value.Value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        public int? ShowInt(string Title, string Name = null, int? value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            int? ret = null;
            var v = ShowInputBox(Title, value.HasValue ? new ReturnInt() { Value = value.Value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        public long? ShowLong(string Title, string Name = null, long? value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            long? ret = null;
            var v = ShowInputBox(Title, value.HasValue ? new ReturnLong() { Value = value.Value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        public ushort? ShowUShort(string Title, string Name = null, ushort? value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            ushort? ret = null;
            var v = ShowInputBox(Title, value.HasValue ? new ReturnUShort() { Value = value.Value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        public uint? ShowUInt(string Title, string Name = null, uint? value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            uint? ret = null;
            var v = ShowInputBox(Title, value.HasValue ? new ReturnUInt() { Value = value.Value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        public ulong? ShowULong(string Title, string Name = null, ulong? value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            ulong? ret = null;
            var v = ShowInputBox(Title, value.HasValue ? new ReturnULong() { Value = value.Value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        public float? ShowFloat(string Title, string Name = null, float? value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            float? ret = null;
            var v = ShowInputBox(Title, value.HasValue ? new ReturnFloat() { Value = value.Value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        public double? ShowDouble(string Title, string Name = null, double? value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            double? ret = null;
            var v = ShowInputBox(Title, value.HasValue ? new ReturnDouble() { Value = value.Value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        public decimal? ShowDecimal(string Title, string Name = null, decimal? value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            decimal? ret = null;
            var v = ShowInputBox<ReturnDecimal>(Title, value.HasValue ? new ReturnDecimal() { Value = value.Value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        public string ShowString(string Title, string Name = null, string value = null)
        {
            Dictionary<string, InputBoxInfo> dic = null;
            if (Name != null)
            {
                dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("Value", new InputBoxInfo() { Alias = Name });
            }

            string ret = null;
            var v = ShowInputBox(Title, value != null ? new ReturnString() { Value = value } : null, dic);
            if (v != null) ret = v.Value;
            return ret;
        }
        #endregion

        class ReturnByte { public byte Value { get; set; } }
        class ReturnShort { public short Value { get; set; } }
        class ReturnInt { public int Value { get; set; } }
        class ReturnLong { public long Value { get; set; } }
        class ReturnUShort { public ushort Value { get; set; } }
        class ReturnUInt { public uint Value { get; set; } }
        class ReturnULong { public ulong Value { get; set; } }
        class ReturnFloat { public float Value { get; set; } }
        class ReturnDouble { public double Value { get; set; } }
        class ReturnDecimal { public decimal Value { get; set; } }
        class ReturnString { public string Value { get; set; } }

    }

    #region attr : InputBoxIgnore
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class InputBoxIgnoreAttribute : Attribute { }
    #endregion
    #region class : InputBoxInfo
    public class InputBoxInfo
    {
        public string Alias { get; set; }
        public List<ComboBoxItem> Items { get; set; }
    }
    #endregion
    #region class : InputBoxTag
    internal class InputBoxTag
    {
        public PropertyInfo p { get; set; }
        public InputBoxInfo info { get; set; }
    }
    #endregion
}
