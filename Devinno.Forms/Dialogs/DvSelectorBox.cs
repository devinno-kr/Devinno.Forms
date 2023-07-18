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
        enum SelectorBoxMode { Selector, ComboBox, RadioBox, CheckBox }
        private SelectorBoxMode Mode { get; set; } = SelectorBoxMode.Selector;

        public int ColumnCount { get; set; } = 1;

        public int ItemWidth { get; set; } = 174;
        public int ItemHeight { get; set; } = 30;

        public int MinWidth { get; set; } = 200;
        public int MinHeight { get; set; } = 100;

        public DvButton ButtonOK => btnOk;
        public DvButton ButtonCancel => btnCancel;
        #endregion

        #region Constructor
        public DvSelectorBox()
        {
            InitializeComponent();

            #region btnOk.ButtonClick 
            btnOk.ButtonClick += (o, s) =>
            {
                switch (Mode)
                {
                    case SelectorBoxMode.ComboBox:
                        if (tpnl.Controls.Count == 1 && tpnl.Controls[0] is DvComboBox && ((DvComboBox)tpnl.Controls[0]).SelectedIndex != -1) DialogResult = DialogResult.OK;
                        break;

                    case SelectorBoxMode.Selector:
                        if (tpnl.Controls.Count == 1 && tpnl.Controls[0] is DvSelector && ((DvSelector)tpnl.Controls[0]).SelectedIndex != -1) DialogResult = DialogResult.OK;
                        break;

                    case SelectorBoxMode.RadioBox:
                        if (tpnl.Controls.Count > 0)
                        {
                            var ls = tpnl.Controls.Cast<Control>().Where(x => x is DvRadioBox).Select(x => (DvRadioBox)x).ToList();
                            if (ls.Where(x => x.Checked).Count() > 0) DialogResult = DialogResult.OK;
                        }
                        break;

                    case SelectorBoxMode.CheckBox:
                        if (tpnl.Controls.Count > 0) DialogResult = DialogResult.OK;
                        break;
                }
            };
            #endregion
            #region btnCancel.ButtonClick
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
            #endregion

            SetExComposited();
        }
        #endregion

        #region Method
        #region show
        void show(string Title, List<TextIcon> List, Action<int, int> actSet, Action actReturn)
        {
            Theme = GetCallerFormTheme();

            #region Var
            this.Title = this.Text = Title;

            var RowCount = 1;
            var ColumnCount = 1;

            this.Width = Math.Max(MinWidth, 10 + (ColumnCount * (ItemWidth + 6)) + 10);
            this.Height = Math.Max(MinHeight, TitleHeight + 10 + (RowCount * (ItemHeight + 6)) + 10 + 36 + 10);
            #endregion
            #region Layout
            tpnl.RowStyles.Clear();
            tpnl.ColumnStyles.Clear();

            tpnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tpnl.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            #endregion
            #region New
            tpnl.Controls.Clear();
            if (actSet != null) actSet(ColumnCount, RowCount);
            #endregion

            if (this.ShowDialog() == DialogResult.OK)
            {
                if (actReturn != null) actReturn();
            }
        }
        #endregion
        #region show2
        void show2(string Title, List<TextIcon> List, Action<int,int> actSet, Action actReturn)
        {
            Theme = GetCallerFormTheme();

            #region Var
            this.Title = this.Text = Title;

            var RowCount = Convert.ToInt32(Math.Ceiling((double)List.Count / (double)ColumnCount));
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
            if (actSet != null) actSet(ColumnCount, RowCount);
            #endregion

            if (this.ShowDialog() == DialogResult.OK)
            {
                if (actReturn != null) actReturn();
            }
        }
        #endregion

        #region ShowSelector
        public TextIcon ShowSelector(string Title, List<TextIcon> List, TextIcon sel = null)
        {
            TextIcon ret = null;

            Mode = SelectorBoxMode.Selector;
            show(Title, List,
                (ColumnCount, RowCount) =>
                {
                    tpnl.ColumnCount = ColumnCount;
                    tpnl.RowCount = RowCount;

                    var c = new DvSelector { Name = "value", Dock = DockStyle.Fill };
                    c.Items.AddRange(List);
                    tpnl.Controls.Add(c, 0, 0, 1, 1);

                    if (sel != null) c.SelectedIndex = List.IndexOf(sel);
                    else c.SelectedIndex = -1;
                },
                () =>
                {
                    var c = ((DvSelector)tpnl.Controls["value"]);
                    if (c.SelectedIndex >= 0 && c.SelectedIndex < c.Items.Count) ret = c.Items[c.SelectedIndex];
                });

            return ret;
        }
        #endregion
        #region ShowComboBox
        public TextIcon ShowComboBox(string Title, List<TextIcon> List, TextIcon sel = null)
        {
            TextIcon ret = null;

            Mode = SelectorBoxMode.ComboBox;
            show(Title, List,
                (ColumnCount, RowCount) =>
                {
                    tpnl.ColumnCount = ColumnCount;
                    tpnl.RowCount = RowCount;

                    var c = new DvComboBox { Name = "value", Dock = DockStyle.Fill };
                    c.Items.AddRange(List);
                    tpnl.Controls.Add(c, 0, 0, 1, 1);

                    if (sel != null) c.SelectedIndex = List.IndexOf(sel);
                    else c.SelectedIndex = -1;
                },
                () =>
                {
                    var c = ((DvComboBox)tpnl.Controls["value"]);
                    if (c.SelectedIndex >= 0 && c.SelectedIndex < c.Items.Count) ret = c.Items[c.SelectedIndex];
                });

            return ret;
        }
        #endregion
        #region ShowRadioBox
        public TextIcon ShowRadioBox(string Title, List<TextIcon> List, TextIcon sel = null)
        {
            TextIcon ret = null;

            Mode = SelectorBoxMode.RadioBox;
            show2(Title, List,
                (ColumnCount, RowCount) =>
                {
                    tpnl.ColumnCount = ColumnCount;
                    tpnl.RowCount = RowCount;

                    var si = sel != null ? List.IndexOf(sel) : 0;

                    for (int row = 0, i = 0; row < RowCount; row++)
                    {
                        for (int col = 0; col < ColumnCount; col++, i++)
                        {
                            if (i < List.Count)
                            {
                                var c = new DvRadioBox()
                                {
                                    Name = "rad" + col + "_" + row,
                                    Dock = DockStyle.Fill,
                                    Checked = i == si,
                                    Text = List[i].Text,
                                    Tag = List[i]
                                };
                                tpnl.Controls.Add(c, col, row);
                            }
                        }
                    }
                },
                () =>
                {
                    var s = tpnl.Controls.Cast<Control>().Where(x => x is DvRadioBox && ((DvRadioBox)x).Checked).FirstOrDefault() as DvRadioBox;
                    if (s != null) ret = s.Tag as TextIcon;
                });

            return ret;
        }
        #endregion
        #region ShwoCheckBox
        public List<TextIcon> ShowCheckBox(string Title, List<TextIcon> List, List<TextIcon> sels = null)
        {
            List<TextIcon> ret = null;

            Mode = SelectorBoxMode.CheckBox;
            show2(Title, List,
                (ColumnCount, RowCount) =>
                {
                    tpnl.ColumnCount = ColumnCount;
                    tpnl.RowCount = RowCount;

                    for (int row = 0, i = 0; row < RowCount; row++)
                    {
                        for (int col = 0; col < ColumnCount; col++, i++)
                        {
                            if (i < List.Count)
                            {
                                var c = new DvCheckBox()
                                {
                                    Name = "chk" + col + "_" + row,
                                    Dock = DockStyle.Fill,
                                    Text = List[i].Text,
                                    Tag = List[i],
                                    Checked = sels != null ? sels.Contains(List[i]) : false,
                                };
                                tpnl.Controls.Add(c, col, row);
                            }
                        }
                    }
                },
                () =>
                {
                    ret = tpnl.Controls.Cast<Control>().Where(x => x is DvCheckBox && ((DvCheckBox)x).Checked).Select(x => x.Tag as TextIcon).ToList();
                });

            return ret;
        }
        #endregion
        #endregion
    }
}
