
namespace Devinno.Forms.Dialogs
{
    partial class DvSerialPortSettingBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dvContainer1 = new Devinno.Forms.Containers.DvContainer();
            this.tpnl = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.inStopBit = new Devinno.Forms.Controls.DvValueInputCombo();
            this.inParityBit = new Devinno.Forms.Controls.DvValueInputCombo();
            this.inDataBit = new Devinno.Forms.Controls.DvValueInputCombo();
            this.inBaudrate = new Devinno.Forms.Controls.DvValueInputCombo();
            this.inPort = new Devinno.Forms.Controls.DvValueInputCombo();
            this.tpnlBox = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.btnOk = new Devinno.Forms.Controls.DvButton();
            this.dvContainer1.SuspendLayout();
            this.tpnl.SuspendLayout();
            this.tpnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.tpnl);
            this.dvContainer1.Controls.Add(this.tpnlBox);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer1.Location = new System.Drawing.Point(0, 40);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(10);
            this.dvContainer1.ShadowGap = 1;
            this.dvContainer1.Size = new System.Drawing.Size(300, 260);
            this.dvContainer1.TabIndex = 0;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            // 
            // tpnl
            // 
            this.tpnl.ColumnCount = 1;
            this.tpnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpnl.Controls.Add(this.inStopBit, 0, 4);
            this.tpnl.Controls.Add(this.inParityBit, 0, 3);
            this.tpnl.Controls.Add(this.inDataBit, 0, 2);
            this.tpnl.Controls.Add(this.inBaudrate, 0, 1);
            this.tpnl.Controls.Add(this.inPort, 0, 0);
            this.tpnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpnl.Location = new System.Drawing.Point(10, 10);
            this.tpnl.Name = "tpnl";
            this.tpnl.RowCount = 6;
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpnl.Size = new System.Drawing.Size(280, 204);
            this.tpnl.TabIndex = 4;
            // 
            // inStopBit
            // 
            this.inStopBit.Button = null;
            this.inStopBit.ButtonColor = null;
            this.inStopBit.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inStopBit.ButtonIconGap = 0;
            this.inStopBit.ButtonIconImage = null;
            this.inStopBit.ButtonIconSize = 12F;
            this.inStopBit.ButtonIconString = null;
            this.inStopBit.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inStopBit.ButtonWidth = null;
            this.inStopBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inStopBit.ItemHeight = 30;
            this.inStopBit.ItemViewCount = 8;
            this.inStopBit.Location = new System.Drawing.Point(3, 159);
            this.inStopBit.Name = "inStopBit";
            this.inStopBit.Round = null;
            this.inStopBit.SelectedIndex = -1;
            this.inStopBit.ShadowGap = 1;
            this.inStopBit.Size = new System.Drawing.Size(274, 33);
            this.inStopBit.TabIndex = 4;
            this.inStopBit.Text = "정지 비트";
            this.inStopBit.Title = "정지 비트";
            this.inStopBit.TitleColor = null;
            this.inStopBit.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inStopBit.TitleIconGap = 0;
            this.inStopBit.TitleIconImage = null;
            this.inStopBit.TitleIconSize = 12F;
            this.inStopBit.TitleIconString = "";
            this.inStopBit.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inStopBit.TitleWidth = 100;
            this.inStopBit.Unit = "";
            this.inStopBit.UnitWidth = null;
            this.inStopBit.ValueColor = null;
            // 
            // inParityBit
            // 
            this.inParityBit.Button = null;
            this.inParityBit.ButtonColor = null;
            this.inParityBit.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inParityBit.ButtonIconGap = 0;
            this.inParityBit.ButtonIconImage = null;
            this.inParityBit.ButtonIconSize = 12F;
            this.inParityBit.ButtonIconString = null;
            this.inParityBit.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inParityBit.ButtonWidth = null;
            this.inParityBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inParityBit.ItemHeight = 30;
            this.inParityBit.ItemViewCount = 8;
            this.inParityBit.Location = new System.Drawing.Point(3, 120);
            this.inParityBit.Name = "inParityBit";
            this.inParityBit.Round = null;
            this.inParityBit.SelectedIndex = -1;
            this.inParityBit.ShadowGap = 1;
            this.inParityBit.Size = new System.Drawing.Size(274, 33);
            this.inParityBit.TabIndex = 3;
            this.inParityBit.Text = "패리티비트";
            this.inParityBit.Title = "패리티비트";
            this.inParityBit.TitleColor = null;
            this.inParityBit.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inParityBit.TitleIconGap = 0;
            this.inParityBit.TitleIconImage = null;
            this.inParityBit.TitleIconSize = 12F;
            this.inParityBit.TitleIconString = "";
            this.inParityBit.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inParityBit.TitleWidth = 100;
            this.inParityBit.Unit = "";
            this.inParityBit.UnitWidth = null;
            this.inParityBit.ValueColor = null;
            // 
            // inDataBit
            // 
            this.inDataBit.Button = null;
            this.inDataBit.ButtonColor = null;
            this.inDataBit.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inDataBit.ButtonIconGap = 0;
            this.inDataBit.ButtonIconImage = null;
            this.inDataBit.ButtonIconSize = 12F;
            this.inDataBit.ButtonIconString = null;
            this.inDataBit.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inDataBit.ButtonWidth = null;
            this.inDataBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inDataBit.ItemHeight = 30;
            this.inDataBit.ItemViewCount = 8;
            this.inDataBit.Location = new System.Drawing.Point(3, 81);
            this.inDataBit.Name = "inDataBit";
            this.inDataBit.Round = null;
            this.inDataBit.SelectedIndex = -1;
            this.inDataBit.ShadowGap = 1;
            this.inDataBit.Size = new System.Drawing.Size(274, 33);
            this.inDataBit.TabIndex = 2;
            this.inDataBit.Text = "데이터 비트";
            this.inDataBit.Title = "데이터 비트";
            this.inDataBit.TitleColor = null;
            this.inDataBit.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inDataBit.TitleIconGap = 0;
            this.inDataBit.TitleIconImage = null;
            this.inDataBit.TitleIconSize = 12F;
            this.inDataBit.TitleIconString = "";
            this.inDataBit.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inDataBit.TitleWidth = 100;
            this.inDataBit.Unit = "";
            this.inDataBit.UnitWidth = null;
            this.inDataBit.ValueColor = null;
            // 
            // inBaudrate
            // 
            this.inBaudrate.Button = null;
            this.inBaudrate.ButtonColor = null;
            this.inBaudrate.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inBaudrate.ButtonIconGap = 0;
            this.inBaudrate.ButtonIconImage = null;
            this.inBaudrate.ButtonIconSize = 12F;
            this.inBaudrate.ButtonIconString = null;
            this.inBaudrate.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inBaudrate.ButtonWidth = null;
            this.inBaudrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inBaudrate.ItemHeight = 30;
            this.inBaudrate.ItemViewCount = 8;
            this.inBaudrate.Location = new System.Drawing.Point(3, 42);
            this.inBaudrate.Name = "inBaudrate";
            this.inBaudrate.Round = null;
            this.inBaudrate.SelectedIndex = -1;
            this.inBaudrate.ShadowGap = 1;
            this.inBaudrate.Size = new System.Drawing.Size(274, 33);
            this.inBaudrate.TabIndex = 1;
            this.inBaudrate.Text = "통신 속도";
            this.inBaudrate.Title = "통신 속도";
            this.inBaudrate.TitleColor = null;
            this.inBaudrate.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inBaudrate.TitleIconGap = 0;
            this.inBaudrate.TitleIconImage = null;
            this.inBaudrate.TitleIconSize = 12F;
            this.inBaudrate.TitleIconString = "";
            this.inBaudrate.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inBaudrate.TitleWidth = 100;
            this.inBaudrate.Unit = "";
            this.inBaudrate.UnitWidth = null;
            this.inBaudrate.ValueColor = null;
            // 
            // inPort
            // 
            this.inPort.Button = null;
            this.inPort.ButtonColor = null;
            this.inPort.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inPort.ButtonIconGap = 0;
            this.inPort.ButtonIconImage = null;
            this.inPort.ButtonIconSize = 12F;
            this.inPort.ButtonIconString = null;
            this.inPort.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inPort.ButtonWidth = null;
            this.inPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inPort.ItemHeight = 30;
            this.inPort.ItemViewCount = 8;
            this.inPort.Location = new System.Drawing.Point(3, 3);
            this.inPort.Name = "inPort";
            this.inPort.Round = null;
            this.inPort.SelectedIndex = -1;
            this.inPort.ShadowGap = 1;
            this.inPort.Size = new System.Drawing.Size(274, 33);
            this.inPort.TabIndex = 0;
            this.inPort.Text = "통신 포트";
            this.inPort.Title = "통신 포트";
            this.inPort.TitleColor = null;
            this.inPort.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inPort.TitleIconGap = 0;
            this.inPort.TitleIconImage = null;
            this.inPort.TitleIconSize = 12F;
            this.inPort.TitleIconString = "";
            this.inPort.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inPort.TitleWidth = 100;
            this.inPort.Unit = "";
            this.inPort.UnitWidth = null;
            this.inPort.ValueColor = null;
            // 
            // tpnlBox
            // 
            this.tpnlBox.ColumnCount = 3;
            this.tpnlBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tpnlBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlBox.Controls.Add(this.btnCancel, 2, 0);
            this.tpnlBox.Controls.Add(this.btnOk, 1, 0);
            this.tpnlBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tpnlBox.Location = new System.Drawing.Point(10, 214);
            this.tpnlBox.Name = "tpnlBox";
            this.tpnlBox.RowCount = 1;
            this.tpnlBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpnlBox.Size = new System.Drawing.Size(280, 36);
            this.tpnlBox.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundDraw = true;
            this.btnCancel.ButtonColor = null;
            this.btnCancel.Clickable = true;
            this.btnCancel.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Gradient = true;
            this.btnCancel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCancel.IconGap = 0;
            this.btnCancel.IconImage = null;
            this.btnCancel.IconSize = 12F;
            this.btnCancel.IconString = null;
            this.btnCancel.Location = new System.Drawing.Point(193, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Round = null;
            this.btnCancel.ShadowGap = 1;
            this.btnCancel.Size = new System.Drawing.Size(84, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnCancel.UseKey = false;
            // 
            // btnOk
            // 
            this.btnOk.BackgroundDraw = true;
            this.btnOk.ButtonColor = null;
            this.btnOk.Clickable = true;
            this.btnOk.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOk.Gradient = true;
            this.btnOk.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnOk.IconGap = 0;
            this.btnOk.IconImage = null;
            this.btnOk.IconSize = 12F;
            this.btnOk.IconString = null;
            this.btnOk.Location = new System.Drawing.Point(103, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Round = null;
            this.btnOk.ShadowGap = 1;
            this.btnOk.Size = new System.Drawing.Size(84, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "확인";
            this.btnOk.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOk.UseKey = false;
            // 
            // DvSerialPortSettingBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.dvContainer1);
            this.Fixed = true;
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 120);
            this.Name = "DvSerialPortSettingBox";
            this.Text = "포트 설정";
            this.Title = "포트 설정";
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-plug";
            this.dvContainer1.ResumeLayout(false);
            this.tpnl.ResumeLayout(false);
            this.tpnlBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.DvContainer dvContainer1;
        private Containers.DvTableLayoutPanel tpnl;
        private Containers.DvTableLayoutPanel tpnlBox;
        private Controls.DvButton btnCancel;
        private Controls.DvButton btnOk;
        private Controls.DvValueInputCombo inStopBit;
        private Controls.DvValueInputCombo inParityBit;
        private Controls.DvValueInputCombo inDataBit;
        private Controls.DvValueInputCombo inBaudrate;
        private Controls.DvValueInputCombo inPort;
    }
}