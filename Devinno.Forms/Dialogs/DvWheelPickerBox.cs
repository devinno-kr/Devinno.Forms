﻿using Devinno.Forms.Controls;
using Devinno.Forms.Extensions;
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
    public partial class DvWheelPickerBox : DvForm
    {
        #region Properties
        public int ButtonHeight { get; set; } = 30;
        public int ItemViewCount { get; set; } = 5;
        public int ItemHeight { get => wheelPicker.ItemHeight; set => wheelPicker.ItemHeight = value; }

        public int MinWidth { get; set; } = 200;
        public int MinHeight { get; set; } = 140;

        public DvButton ButtonOK => btnOK;
        public DvButton ButtonCancel => btnCancel;
        #endregion

        #region Constructor
        public DvWheelPickerBox()
        {
            InitializeComponent();

            btnOK.ButtonClick += (o, s) => DialogResult = DialogResult.OK;
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;

            SetExComposited();
        }
        #endregion

        #region Method
        #region ShowWheelPickerBox
        public int? ShowWheelPickerBox(string Title, int SelectedIndex, List<TextIcon> Items)
        {
            Theme = GetCallerFormTheme();

            int? ret = null;
            this.Title = this.Text = Title;

            #region Size
            tpnl.RowStyles[2].Height = ButtonHeight + 6;

            int w = 0;
            using(var g = CreateGraphics())
            {
                var v = wheelPicker.Items.OrderByDescending(x => x.Text).FirstOrDefault();

                if (v != null) w = Convert.ToInt32(g.MeasureTextIcon(v, Font).Width);
            }

            this.Width = Math.Max(MinWidth, w + 20 + 40);
            this.Height = Math.Max(MinHeight, 40 + 20 + 36 + (wheelPicker.ItemHeight * ItemViewCount));
            Padding = (BlankForm && FormBorderStyle != FormBorderStyle.None) ? new Padding(0) : new Padding(0, 40, 0, 0);

            #endregion

            #region Set
            wheelPicker.Items.Clear();
            wheelPicker.Items.AddRange(Items);
            wheelPicker.SelectedIndex = SelectedIndex == -1 ? 0 : SelectedIndex;
            #endregion

            if (this.ShowDialog()== DialogResult.OK)
            {
                ret = wheelPicker.SelectedIndex;
            }

            return ret;
        }
        #endregion
        #endregion
    }
}
