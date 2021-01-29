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
    public partial class DvMessageBox : DvForm
    {
        public DvMessageBox()
        {
            InitializeComponent();

            btnOk.ButtonClick += (o, s) => DialogResult = DialogResult.OK;
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
            btnYes.ButtonClick += (o, s) => DialogResult = DialogResult.Yes;
            btnNo.ButtonClick += (o, s) => DialogResult = DialogResult.No;
        }

        public DialogResult ShowMessageBoxOk(string Title, string Message)
        {
            this.Text = this.Title = Title;
            lblMessage.Text = Message;

            layout.Controls.Clear();
            layout.Controls.Add(lblMessage, 0, 0);
            layout.SetColumnSpan(lblMessage, 13);
            layout.Controls.Add(btnOk, 3, 2);
            layout.SetColumnSpan(btnOk, 7);
            return this.ShowDialog();
        }

        public DialogResult ShowMessageBoxOkCancel(string Title, string Message)
        {
            this.Text = this.Title = Title;
            lblMessage.Text = Message;

            layout.Controls.Clear();
            layout.Controls.Add(lblMessage, 0, 0);
            layout.SetColumnSpan(lblMessage, 13);
            layout.Controls.Add(btnOk, 1, 2);
            layout.SetColumnSpan(btnOk, 5);
            layout.Controls.Add(btnCancel, 7, 2);
            layout.SetColumnSpan(btnCancel, 5);
            return this.ShowDialog();
        }

        public DialogResult ShowMessageBoxYesNo(string Title, string Message)
        {
            this.Text = this.Title = Title;
            lblMessage.Text = Message;

            layout.Controls.Clear();
            layout.Controls.Add(lblMessage, 0, 0);
            layout.SetColumnSpan(lblMessage, 13);
            layout.Controls.Add(btnYes, 1, 2);
            layout.SetColumnSpan(btnYes, 5);
            layout.Controls.Add(btnNo, 7, 2);
            layout.SetColumnSpan(btnNo, 5);
            return this.ShowDialog();
        }

        public DialogResult ShowMessageBoxYesNoCancel(string Title, string Message)
        {
            this.Text = this.Title = Title;
            lblMessage.Text = Message;

            layout.Controls.Clear();
            layout.Controls.Add(lblMessage, 0, 0);
            layout.SetColumnSpan(lblMessage, 13);
            layout.Controls.Add(btnYes, 1, 2);
            layout.SetColumnSpan(btnYes, 3);
            layout.Controls.Add(btnNo, 5, 2);
            layout.SetColumnSpan(btnNo, 3);
            layout.Controls.Add(btnCancel, 9, 2);
            layout.SetColumnSpan(btnCancel, 3);
            return this.ShowDialog();
        }

    }
}
