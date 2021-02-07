﻿using Devinno.Forms.Themes;
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
    public partial class DvKeypad : DvForm
    {
        #region Member Variable
        string sval = "";
        string svalOrigin = "";
        int mode = 0;
        #endregion

        public DvKeypad()
        {
            InitializeComponent();

            #region Enter / Clear
            btnClear.MouseUp += (o, s) => { sval = ""; SetText(); };
            btnEnter.MouseUp += (o, s) =>
            {
                if (sval.Length != lbl.Text.Length && sval.Length == 0)
                {
                    sval = svalOrigin;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    decimal v1;
                    int v2, v3;

                    if (mode == 0 && int.TryParse(sval, out v2)) DialogResult = DialogResult.OK;
                    else if (mode == 1 && decimal.TryParse(sval, out v1)) DialogResult = DialogResult.OK;
                    else if (mode == 2 && int.TryParse(sval, out v3)) DialogResult = DialogResult.OK;
                }
            };
            #endregion
            #region 0~9
            btn0.MouseUp += (o, s) => { sval += "0"; SetText(); };
            btn1.MouseUp += (o, s) => { sval += "1"; SetText(); };
            btn2.MouseUp += (o, s) => { sval += "2"; SetText(); };
            btn3.MouseUp += (o, s) => { sval += "3"; SetText(); };
            btn4.MouseUp += (o, s) => { sval += "4"; SetText(); };
            btn5.MouseUp += (o, s) => { sval += "5"; SetText(); };
            btn6.MouseUp += (o, s) => { sval += "6"; SetText(); };
            btn7.MouseUp += (o, s) => { sval += "7"; SetText(); };
            btn8.MouseUp += (o, s) => { sval += "8"; SetText(); };
            btn9.MouseUp += (o, s) => { sval += "9"; SetText(); };
            #endregion
            #region Dot
            btnDot.MouseUp += (o, s) =>
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
            btnSign.MouseUp += (o, s) => {
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
            btnBack.MouseUp += (o, s) =>
            {
                if (sval.Length > 0) sval = sval.Substring(0, sval.Length - 1);
                SetText();
            };
            #endregion
        }

        #region Method
        #region SetText
        void SetText()
        {
            if (mode == 2)
            {
                lbl.Text = new string(sval.Select(x => '*').ToArray());
            }
            else if (mode == 1)
            {
                decimal n = 0;
                if (sval.Length > 0 && decimal.TryParse(sval, out n))
                {
                    if (sval.Last() == '.') lbl.Text = n.ToString() + ".";
                    else lbl.Text = n.ToString();
                }
                else lbl.Text = sval = "";
            }
            else if (mode == 0)
            {
                int n = 0;
                if (sval.Length > 0 && int.TryParse(sval, out n)) lbl.Text = n.ToString();
                else lbl.Text = sval = "";
            }
        }
        #endregion
        #region ShowKeypad
        public int? ShowKeypad(int? value = null)
        {
            #region UI
            layout.Controls.Clear();
            Add(lbl, 0, 7, 0, 1);
            Add(btn7, 0, 1, 2, 1); Add(btn8, 2, 1, 2, 1); Add(btn9, 4, 1, 2, 1); Add(btnBack, 6, 1, 2, 1);
            Add(btn4, 0, 1, 4, 1); Add(btn5, 2, 1, 4, 1); Add(btn6, 4, 1, 4, 1); Add(btnClear, 6, 1, 4, 1);
            Add(btn1, 0, 1, 6, 1); Add(btn2, 2, 1, 6, 1); Add(btn3, 4, 1, 6, 1); Add(btnEnter, 6, 1, 6, 3);
            Add(btn0, 0, 5, 8, 1);                                               
            #endregion
            #region Set
            mode = 0;
            sval = "";
            lbl.Text = value.HasValue ? value.Value.ToString() : "";
            svalOrigin = value.HasValue ? value.Value.ToString() : "";
            #endregion

            Theme = GetCallerFormTheme() ?? Theme;

            int? ret = null;
            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = Convert.ToInt32(sval);
            }
            return ret;
        }
        #endregion
        #region ShowKeypadEx
        public decimal? ShowKeypadEx(decimal? value = null)
        {
            #region UI
            layout.Controls.Clear();
            Add(lbl, 0, 7, 0, 1);
            Add(btn7, 0, 1, 2, 1);  Add(btn8, 2, 1, 2, 1);  Add(btn9, 4, 1, 2, 1);      Add(btnBack, 6, 1, 2, 1);
            Add(btn4, 0, 1, 4, 1);  Add(btn5, 2, 1, 4, 1);  Add(btn6, 4, 1, 4, 1);      Add(btnSign, 6, 1, 4, 1);
            Add(btn1, 0, 1, 6, 1);  Add(btn2, 2, 1, 6, 1);  Add(btn3, 4, 1, 6, 1);      Add(btnClear, 6, 1, 6, 1);
            Add(btn0, 0, 3, 8, 1);                          Add(btnDot, 4, 1, 8, 1);    Add(btnEnter, 6, 1, 8, 1);
            #endregion
            #region Set
            mode = 1;
            sval = "";
            lbl.Text = value.HasValue ? value.Value.ToString() : "";
            svalOrigin = value.HasValue ? value.Value.ToString() : "";
            #endregion

            Theme = GetCallerFormTheme() ?? Theme;

            decimal? ret = null;
            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = Convert.ToDecimal(sval);
            }
            return ret;
        }
        #endregion
        #region ShowPassword
        public string ShowPassword(int? value = null)
        {
            #region UI
            layout.Controls.Clear();
            Add(lbl, 0, 7, 0, 1);
            Add(btn7, 0, 1, 2, 1); Add(btn8, 2, 1, 2, 1); Add(btn9, 4, 1, 2, 1); Add(btnBack, 6, 1, 2, 1);
            Add(btn4, 0, 1, 4, 1); Add(btn5, 2, 1, 4, 1); Add(btn6, 4, 1, 4, 1); Add(btnClear, 6, 1, 4, 1);
            Add(btn1, 0, 1, 6, 1); Add(btn2, 2, 1, 6, 1); Add(btn3, 4, 1, 6, 1); Add(btnEnter, 6, 1, 6, 3);
            Add(btn0, 0, 5, 8, 1);
            #endregion
            #region Set
            mode = 2;
            sval = "";
            lbl.Text = value.HasValue ? string.Concat(value.Value.ToString().Select(x => "●").ToArray()) : "";
            svalOrigin = value.HasValue ? value.Value.ToString() : "";
            #endregion

            Theme = GetCallerFormTheme() ?? Theme;

            string ret = null;
            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = sval;
            }
            return ret;
        }
        #endregion
        #region Add
        void Add(Control c, int column, int columnspan, int row, int rowspan)
        {
            layout.Controls.Add(c);
            layout.SetCellPosition(c, new TableLayoutPanelCellPosition(column, row));
            layout.SetColumnSpan(c, columnspan);
            layout.SetRowSpan(c, rowspan);
        }
        #endregion
        #endregion

    }
}
