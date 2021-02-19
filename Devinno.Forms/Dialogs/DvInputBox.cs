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
        #region Constructor
        public DvInputBox()
        {
            InitializeComponent();

            btnOK.ButtonClick += (o, s) => { if (ValidCheck()) DialogResult = DialogResult.OK; };
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region Method
        #region ValidCheck
        bool ValidCheck()
        {
            bool ret = true;
            foreach (var c in layout.Controls.Cast<Control>().Where(x => x is DvValueInput).Cast<DvValueInput>())
            {
                var tag = c.Tag as InputBoxTag;
                var p = tag.p;
                var info = tag.info;

                if(c.InputStyle == DvInputType.NUMBER)
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
                else if(c.InputStyle == DvInputType.COMBO)
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
            var f = DpiRatio;
            var props = typeof(T).GetProperties().Where(x => x.Name != "Id" && x.CanRead && x.CanWrite && !Attribute.IsDefined(x, typeof(InputBoxIgnoreAttribute))).ToList();
            this.Size = new Size(450, 180 + Convert.ToInt32(((30 * DpiRatio)) * props.Count));

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
                    c.Text = info != null && !string.IsNullOrWhiteSpace(info.Alias) ? info.Alias : p.Name;
                    c.ValueTextChanged += (o, s) => ((DvValueInput)o).DrawBorder = false;
                    c.SelectedIndexChanged += (o, s) => ((DvValueInput)o).DrawBorder = false;

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

            this.Text = this.Title = string.IsNullOrWhiteSpace(Title) ? "항목 입력" : Title;
            Theme = GetCallerFormTheme() ?? Theme;

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
        #endregion
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
