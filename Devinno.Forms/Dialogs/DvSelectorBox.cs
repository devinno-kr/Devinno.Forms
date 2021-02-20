using Devinno.Forms.Controls;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Dialogs
{
    public partial class DvSelectorBox : DvForm
    {
        #region Properties
        private SelectorBoxMode Mode { get; set; } = SelectorBoxMode.Selector;
        #endregion

        #region Constructor
        public DvSelectorBox()
        {
            InitializeComponent();
            btnOK.ButtonClick += (o, s) =>
            {
                switch (Mode)
                {
                    case SelectorBoxMode.ComboBox:
                        if (layout.Controls.Count == 1 && layout.Controls[0] is DvComboBox && ((DvComboBox)layout.Controls[0]).SelectedIndex != -1) DialogResult = DialogResult.OK;
                        break;

                    case SelectorBoxMode.Selector:
                        if (layout.Controls.Count == 1 && layout.Controls[0] is DvSelector && ((DvSelector)layout.Controls[0]).SelectedIndex != -1) DialogResult = DialogResult.OK;
                        break;

                    case SelectorBoxMode.RadioBox:
                        if (layout.Controls.Count > 0)
                        {
                            var ls = layout.Controls.Cast<Control>().Where(x => x is DvRadioBox).Select(x => (DvRadioBox)x).ToList();
                            if (ls.Where(x => x.Checked).Count() > 0) DialogResult = DialogResult.OK;
                        }
                        break;

                    case SelectorBoxMode.CheckBox:
                        if (layout.Controls.Count > 0) DialogResult = DialogResult.OK;
                        break;
                }
            };
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;

            Fixed = true;

        }
        #endregion

        #region Method
        #region ShowSelector
        public TextIconItem ShowSelector(string Title, List<TextIconItem> List)
        {
            #region DPI Size 
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            var w = 264;
            var gpw = 20;
            using (var g = CreateGraphics())
            {
                var szw = Convert.ToInt32(List.Select(x => g.MeasureTextIcon(x.Icon, x.Text, Font).Width).Max() + gpw);
                var sz = g.MeasureIcon(new DvIcon("fa-chevron-right") { IconSize = Convert.ToInt32(Font.Size * 1.33) });

                w = Math.Max(w, szw + Convert.ToInt32(sz.Width * 4));
            }

            pnl.Padding = new Padding(0, m7, 0, 0);
            pnlBtn.Padding = new Padding(m3);
            pnlBtn.Height = Convert.ToInt32(f * 36);
            gpH.Width = gpV.Height = Convert.ToInt32(f * 4);
            btnOK.Width = btnCancel.Width = Convert.ToInt32(80 * f);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(w * f), Convert.ToInt32((40 + 7 + ((3 + 30 + 3)) + 4 + 36 + 7) * f));
            #endregion
            #region UI
            var c = new DvSelector() { Name = "selector", Dock = DockStyle.Fill, UseAnimation = true, BackgroundDraw = false };
            c.Items.AddRange(List);
            c.Margin = new Padding(m3);
            c.SelectedIndex = c.Items.Count > 0 ? 0 : -1;

            layout.ColumnStyles.Clear();
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            layout.ColumnCount = 1;

            layout.RowStyles.Clear();
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            layout.RowCount = 1;

            var ls = layout.Controls.Cast<Control>().ToArray();
            layout.Controls.Clear();
            layout.Controls.Add(c, 0, 0);
            foreach (var v in ls) v.Dispose();
            #endregion
            #region Set
            Mode = SelectorBoxMode.Selector;
            #endregion

            this.Text = this.Title = string.IsNullOrWhiteSpace(Title) ? "항목 선택" : Title;
            Theme = GetCallerFormTheme() ?? Theme;

            TextIconItem ret = null;
            if (this.ShowDialog() == DialogResult.OK)
            {
                var ctrl = layout.Controls[0] as DvSelector;
                ret = ctrl.Items[c.SelectedIndex] as TextIconItem;
            }

            return ret;
        }
        #endregion
        #region ShowComboBox
        public TextIconItem ShowComboBox(string Title, List<TextIconItem> List)
        {
            #region DPI Size 
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            var w = 264;
            var gpw = 20;
            using (var g = CreateGraphics())
            {
                var szw = Convert.ToInt32(List.Select(x => g.MeasureTextIcon(x.Icon, x.Text, Font).Width).Max() + gpw + 60);
                w = Math.Max(w, szw);
            }

            pnl.Padding = new Padding(0, m7, 0, 0);
            pnlBtn.Padding = new Padding(m3);
            pnlBtn.Height = Convert.ToInt32(f * 36);
            gpH.Width = gpV.Height = Convert.ToInt32(f * 4);
            btnOK.Width = btnCancel.Width = Convert.ToInt32(80 * f);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(w * f), Convert.ToInt32((40 + 7 + ((3 + 30 + 3)) + 4 + 36 + 7) * f));
            #endregion
            #region UI

            var c = new DvComboBox() { Name = "selector", Dock = DockStyle.Fill };
            c.Items.AddRange(List.Select(x => (x.IconImage != null ? new ComboBoxItem(x.Text, x.IconImage) { Tag = x } : new ComboBoxItem(x.Text, x.IconString, x.IconSize, x.IconGap) { Tag = x })));
            c.Margin = new Padding(m3);
            c.SelectedIndex = c.Items.Count > 0 ? 0 : -1;
            c.ItemHeight = Convert.ToInt32(30 * DpiRatio);

            layout.ColumnStyles.Clear();
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            layout.ColumnCount = 1;

            layout.RowStyles.Clear();
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            layout.RowCount = 1;

            var ls = layout.Controls.Cast<Control>().ToArray();
            layout.Controls.Clear();
            layout.Controls.Add(c, 0, 0);
            foreach (var v in ls) v.Dispose();
            #endregion
            #region Set
            Mode = SelectorBoxMode.ComboBox;
            #endregion

            this.Text = this.Title = string.IsNullOrWhiteSpace(Title) ? "항목 선택" : Title;
            Theme = GetCallerFormTheme() ?? Theme;

            TextIconItem ret = null;
            if (this.ShowDialog() == DialogResult.OK)
            {
                var ctrl = layout.Controls[0] as DvComboBox;
                ret = ctrl.Items[c.SelectedIndex].Tag as TextIconItem;
            }

            return ret;
        }
        #endregion
        #region ShowRadioBox
        public TextIconItem ShowRadioBox(string Title, List<TextIconItem> List, int ColumnSize = 1)
        {
            var RowSize = Convert.ToInt32(Math.Ceiling((double)List.Count / (double)ColumnSize));

            #region DPI Size 
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            var w = 264;
            var gpw = 20;
            using (var g = CreateGraphics())
            {
                var szw = Convert.ToInt32(List.Select(x => g.MeasureTextIcon(x.Icon, x.Text, Font).Width).Max() + gpw + (18 + 5)) * ColumnSize;
                w = Math.Max(w, szw);
            }

            pnl.Padding = new Padding(0, m7, 0, 0);
            pnlBtn.Padding = new Padding(m3);
            pnlBtn.Height = Convert.ToInt32(f * 36);
            gpH.Width = gpV.Height = Convert.ToInt32(f * 4);
            btnOK.Width = btnCancel.Width = Convert.ToInt32(80 * f);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(w * f), Convert.ToInt32((40 + 7 + ((3 + 30 + 3) *RowSize) + 4 + 36 + 7) * f));
            #endregion
            #region UI
            int nw = 150;
            var last = List.OrderBy(x => x.Text.Length).LastOrDefault();
            using (var g = CreateGraphics()) { nw = Convert.ToInt32(Math.Ceiling((g.MeasureString(last.Text, Font).Width + 18 + 5 + 10) * f)) + 6; }

            layout.ColumnStyles.Clear();
            for (int col = 0; col < ColumnSize; col++)
                layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F / (float)ColumnSize));
            layout.ColumnCount = ColumnSize;

            layout.RowStyles.Clear();
            for (int row = 0; row < RowSize; row++)
                layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F / (float)RowSize));
            layout.RowCount = RowSize;

            var ls = layout.Controls.Cast<Control>().ToArray();
            layout.Controls.Clear();
            for (int row = 0, i = 0; row < RowSize; row++)
            {
                for (int col = 0; col < ColumnSize; col++, i++)
                {
                    if (i < List.Count)
                    {
                        var c = new DvRadioBox() { Name = "rad" + col + "_" + row, Dock = DockStyle.Fill, Checked = row == 0 && col == 0, Text = List[i].Text, Tag = List[i] };
                        layout.Controls.Add(c, col, row);
                    }
                }
            }
            foreach (var v in ls) v.Dispose();
            #endregion
            #region Set
            Mode = SelectorBoxMode.RadioBox;
            #endregion

            TextIconItem ret = null;

            if (this.ShowDialog() == DialogResult.OK)
            {
                var s = layout.Controls.Cast<Control>().Where(x => x is DvRadioBox && ((DvRadioBox)x).Checked).FirstOrDefault() as DvRadioBox;
                if (s != null) ret = s.Tag as TextIconItem;
            }

            return ret;
        }
        #endregion
        #region ShwoCheckBox
        public List<TextIconItem> ShowCheckBox(string Title, List<TextIconItem> List, int ColumnSize = 1)
        {
            var RowSize = Convert.ToInt32(Math.Ceiling((double)List.Count / (double)ColumnSize));

            #region DPI Size 
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            var w = 264;
            var gpw = 20;
            using (var g = CreateGraphics())
            {
                var szw = Convert.ToInt32(List.Select(x => g.MeasureTextIcon(x.Icon, x.Text, Font).Width).Max() + gpw + (18 + 5)) * ColumnSize;
                w = Math.Max(w, szw);
            }

            pnl.Padding = new Padding(0, m7, 0, 0);
            pnlBtn.Padding = new Padding(m3);
            pnlBtn.Height = Convert.ToInt32(f * 36);
            gpH.Width = gpV.Height = Convert.ToInt32(f * 4);
            btnOK.Width = btnCancel.Width = Convert.ToInt32(80 * f);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(w * f), Convert.ToInt32((40 + 7 + ((3 + 30 + 3) * RowSize) + 4 + 36 + 7) * f));
            #endregion
            #region UI
            int nw = 150;
            var last = List.OrderBy(x => x.Text.Length).LastOrDefault();
            using (var g = CreateGraphics()) { nw = Convert.ToInt32(Math.Ceiling((g.MeasureString(last.Text, Font).Width + 18 + 5 + 10) * f)) + 6; }

            layout.ColumnStyles.Clear();
            for (int col = 0; col < ColumnSize; col++)
                layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F / (float)ColumnSize));
            layout.ColumnCount = ColumnSize;

            layout.RowStyles.Clear();
            for (int row = 0; row < RowSize; row++)
                layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F / (float)RowSize));
            layout.RowCount = RowSize;

            var ls = layout.Controls.Cast<Control>().ToArray();
            layout.Controls.Clear();
            for (int row = 0, i = 0; row < RowSize; row++)
            {
                for (int col = 0; col < ColumnSize; col++, i++)
                {
                    if (i < List.Count)
                    {
                        var c = new DvCheckBox() { Name = "rad" + col + "_" + row, Dock = DockStyle.Fill, Text = List[i].Text, Tag = List[i] };
                        layout.Controls.Add(c, col, row);
                    }
                }
            }
            foreach (var v in ls) v.Dispose();
            #endregion
            #region Set
            Mode = SelectorBoxMode.CheckBox;
            #endregion

            List<TextIconItem> ret = null;

            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = layout.Controls.Cast<Control>().Where(x => x is DvCheckBox && ((DvCheckBox)x).Checked).Select(x => x.Tag as TextIconItem).ToList();
            }

            return ret;
        }
        #endregion
        #endregion
    }

    #region enum : SelectorBoxMode
    internal enum SelectorBoxMode { Selector, ComboBox, RadioBox, CheckBox }
    #endregion
}
