using Devinno.Forms.Controls;
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
    public partial class DvMessageBox : DvForm
    {
        #region Properties
        #region ButtonHeight
        public int ButtonHeight { get; set; } = 30;
        #endregion

        public int MinWidth { get; set; } = 240;
        public int MinHeight { get; set; } = 150;
        #endregion

        #region Member Variable
        DvButton btnOK;
        DvButton btnCancel;
        DvButton btnYes;
        DvButton btnNo;
        DvLabel lbl;
        #endregion

        #region Constructor
        public DvMessageBox()
        {
            InitializeComponent();

            lbl = new DvLabel() { Name = nameof(lbl), BackgroundDraw = false, ContentAlignment = DvContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            btnOK = new DvButton { Name = nameof(btnOK), Text = "확인", Dock = DockStyle.Fill };
            btnCancel = new DvButton { Name = nameof(btnCancel), Text = "취소", Dock = DockStyle.Fill };
            btnYes = new DvButton { Name = nameof(btnYes), Text = "예", Dock = DockStyle.Fill };
            btnNo = new DvButton { Name = nameof(btnNo), Text = "아니요", Dock = DockStyle.Fill };

            btnOK.MouseClick += (o, s) => DialogResult = DialogResult.OK;
            btnCancel.MouseClick += (o, s) => DialogResult = DialogResult.Cancel;
            btnYes.MouseClick += (o, s) => DialogResult = DialogResult.Yes;
            btnNo.MouseClick += (o, s) => DialogResult = DialogResult.No;

            SetExComposited();
        }
        #endregion

        #region Method
        #region show
        DialogResult show(string Title, string Message, Action act)
        {
            Theme = GetCallerFormTheme();

            SizeF sz;
            using (var g = CreateGraphics()) sz = g.MeasureString(Message, Font);
            var btnSZ = Convert.ToInt32(ButtonHeight + 6);
            var gapW = layout.Padding.Left + layout.Padding.Right + lbl.Margin.Left + lbl.Margin.Right;
            var gapH = layout.Padding.Top + layout.Padding.Bottom + lbl.Margin.Top + lbl.Margin.Bottom;
            

            tpnl.RowStyles[1].Height = btnSZ;
            Width = Math.Max(gapW + Convert.ToInt32(sz.Width), MinWidth);
            Height = Math.Max(TitleHeight + gapH + btnSZ + Convert.ToInt32(sz.Height), MinHeight);

            this.Title = this.Text = Title;
            lbl.Text = Message;

            act();

            return this.ShowDialog();
        }
        #endregion
        #region ShowMessageBoxOk
        public DialogResult ShowMessageBoxOk(string Title, string Message)
        {
            return show(Title, Message, () =>
            {
                tpnl.Controls.Clear();
                tpnl.Controls.Add(lbl, 0, 0, 8, 1);
                tpnl.Controls.Add(btnOK, 2, 1, 4, 1);
            });
        }
        #endregion
        #region ShowMessageBoxYesNo
        public DialogResult ShowMessageBoxYesNo(string Title, string Message )
        {
            return show(Title, Message, () =>
            {
                tpnl.Controls.Clear();
                tpnl.Controls.Add(lbl, 0, 0, 8, 1);
                tpnl.Controls.Add(btnYes, 1, 1, 3, 1);
                tpnl.Controls.Add(btnNo, 4, 1, 3, 1);
            });
        }
        #endregion
        #region ShowMessageBoxOkCancel
        public DialogResult ShowMessageBoxOkCancel(string Title, string Message )
        {
            return show(Title, Message, () =>
            {
                tpnl.Controls.Clear();
                tpnl.Controls.Add(lbl, 0, 0, 8, 1);
                tpnl.Controls.Add(btnOK, 1, 1, 3, 1);
                tpnl.Controls.Add(btnCancel, 4, 1, 3, 1);
            });
        }
        #endregion
        #region ShowMessageBoxYesNoCancel
        public DialogResult ShowMessageBoxYesNoCancel(string Title, string Message )
        {
            return show(Title, Message, () =>
            {
                tpnl.Controls.Clear();
                tpnl.Controls.Add(lbl, 0, 0, 8, 1);
                tpnl.Controls.Add(btnYes, 1, 1, 2, 1);
                tpnl.Controls.Add(btnNo, 3, 1, 2, 1);
                tpnl.Controls.Add(btnCancel, 5, 1, 2, 1);
            });
        }
        #endregion
        #endregion
    }
}
