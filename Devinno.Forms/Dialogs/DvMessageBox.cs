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

            Fixed = true;

        }

        public DialogResult ShowMessageBoxOk(string Title, string Message)
        {
            this.Text = this.Title = Title;
            lblMessage.Text = Message;

            #region DPI Size
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            int szh = 100;
            int szw = 320;
            using (var g = CreateGraphics()) { var sz =  g.MeasureString(Message, Font); szw = Math.Max(Convert.ToInt32(sz.Width + 40), szw); szh = Math.Max(Convert.ToInt32(sz.Height + 40), szh); }

            foreach (var c in layout.Controls.Cast<Control>()) c.Margin = new Padding(m3);
            pnl.Padding = new Padding(m3, m10, m3, m3);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(szw * f), Convert.ToInt32((40 + 10 + (szh) + 10) * f));
            layout.RowStyles[1].SizeType = SizeType.Absolute;
            layout.RowStyles[1].Height = Convert.ToInt32(36 * f);
            #endregion

            layout.Controls.Clear();
            layout.Controls.Add(lblMessage, 0, 0); layout.SetColumnSpan(lblMessage, 8);
            layout.Controls.Add(btnOk, 1, 1); layout.SetColumnSpan(btnOk, 6);
            return this.ShowDialog();
        }

        public DialogResult ShowMessageBoxOkCancel(string Title, string Message)
        {
            this.Text = this.Title = Title;
            lblMessage.Text = Message;

            #region DPI Size
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            int szh = 100;
            int szw = 320;
            using (var g = CreateGraphics()) { var sz = g.MeasureString(Message, Font); szw = Math.Max(Convert.ToInt32(sz.Width + 40), szw); szh = Math.Max(Convert.ToInt32(sz.Height + 40), szh); }

            foreach (var c in layout.Controls.Cast<Control>()) c.Margin = new Padding(m3);
            pnl.Padding = new Padding(m3, m10, m3, m3);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(szw * f), Convert.ToInt32((40 + 10 + (szh) + 10) * f));
            layout.RowStyles[1].SizeType = SizeType.Absolute;
            layout.RowStyles[1].Height = Convert.ToInt32(36 * f);
            #endregion

            layout.Controls.Clear();
            layout.Controls.Add(lblMessage, 0, 0); layout.SetColumnSpan(lblMessage, 8);
            layout.Controls.Add(btnOk, 1, 1); layout.SetColumnSpan(btnOk, 3);
            layout.Controls.Add(btnCancel, 4, 1); layout.SetColumnSpan(btnCancel, 3);
            return this.ShowDialog();
        }

        public DialogResult ShowMessageBoxYesNo(string Title, string Message)
        {
            this.Text = this.Title = Title;
            lblMessage.Text = Message;

            #region DPI Size
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            int szh = 100;
            int szw = 320;
            using (var g = CreateGraphics()) { var sz = g.MeasureString(Message, Font); szw = Math.Max(Convert.ToInt32(sz.Width + 40), szw); szh = Math.Max(Convert.ToInt32(sz.Height + 40), szh); }

            foreach (var c in layout.Controls.Cast<Control>()) c.Margin = new Padding(m3);
            pnl.Padding = new Padding(m3, m10, m3, m3);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(szw * f), Convert.ToInt32((40 + 10 + (szh) + 10) * f));
            layout.RowStyles[1].SizeType = SizeType.Absolute;
            layout.RowStyles[1].Height = Convert.ToInt32(36 * f);
            #endregion


            layout.Controls.Clear();
            layout.Controls.Add(lblMessage, 0, 0); layout.SetColumnSpan(lblMessage, 8);
            layout.Controls.Add(btnYes, 1, 1); layout.SetColumnSpan(btnYes, 3);
            layout.Controls.Add(btnNo, 4, 1); layout.SetColumnSpan(btnNo, 3);
            return this.ShowDialog();
        }

        public DialogResult ShowMessageBoxYesNoCancel(string Title, string Message)
        {
            this.Text = this.Title = Title;
            lblMessage.Text = Message;

            #region DPI Size
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            int szh = 100;
            int szw = 320;
            using (var g = CreateGraphics()) { var sz = g.MeasureString(Message, Font); szw = Math.Max(Convert.ToInt32(sz.Width + 40), szw); szh = Math.Max(Convert.ToInt32(sz.Height + 80), szh); }

            foreach (var c in layout.Controls.Cast<Control>()) c.Margin = new Padding(m3);
            pnl.Padding = new Padding(0, m7, 0, 0);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(szw * f), Convert.ToInt32((40 + 10 + (szh) + 10) * f));
            layout.RowStyles[1].SizeType = SizeType.Absolute;
            layout.RowStyles[1].Height = Convert.ToInt32(36 * f);
            #endregion

            layout.Controls.Clear();
            layout.Controls.Add(lblMessage, 0, 0);      layout.SetColumnSpan(lblMessage, 8);
            layout.Controls.Add(btnYes, 1, 1);          layout.SetColumnSpan(btnYes, 2);
            layout.Controls.Add(btnNo, 3, 1);           layout.SetColumnSpan(btnNo, 2);
            layout.Controls.Add(btnCancel, 5, 1);       layout.SetColumnSpan(btnCancel, 2);
            return this.ShowDialog();
        }

    }
}
