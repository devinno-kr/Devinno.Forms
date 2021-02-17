
namespace Devinno.Forms.Dialogs
{
    partial class DvSerialPortSetting
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
            this.pnl = new Devinno.Forms.Containers.DvContainer();
            this.layout = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.cmbStopBit = new Devinno.Forms.Controls.DvComboBox();
            this.cmbParity = new Devinno.Forms.Controls.DvComboBox();
            this.cmbDataBit = new Devinno.Forms.Controls.DvComboBox();
            this.cmbBaudrate = new Devinno.Forms.Controls.DvComboBox();
            this.lblPort = new Devinno.Forms.Controls.DvLabel();
            this.lblBaudrate = new Devinno.Forms.Controls.DvLabel();
            this.lblDataBit = new Devinno.Forms.Controls.DvLabel();
            this.lblParity = new Devinno.Forms.Controls.DvLabel();
            this.lblStopBit = new Devinno.Forms.Controls.DvLabel();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.cmbPort = new Devinno.Forms.Controls.DvComboBox();
            this.pnl.SuspendLayout();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.layout);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(7, 60);
            this.pnl.Name = "pnl";
            this.pnl.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.pnl.Size = new System.Drawing.Size(386, 329);
            this.pnl.TabIndex = 2;
            this.pnl.TabStop = false;
            this.pnl.Text = "dvContainer1";
            this.pnl.UseThemeColor = true;
            // 
            // layout
            // 
            this.layout.ColumnCount = 5;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.layout.Controls.Add(this.cmbStopBit, 1, 4);
            this.layout.Controls.Add(this.cmbParity, 1, 3);
            this.layout.Controls.Add(this.cmbDataBit, 1, 2);
            this.layout.Controls.Add(this.cmbBaudrate, 1, 1);
            this.layout.Controls.Add(this.lblPort, 0, 0);
            this.layout.Controls.Add(this.lblBaudrate, 0, 1);
            this.layout.Controls.Add(this.lblDataBit, 0, 2);
            this.layout.Controls.Add(this.lblParity, 0, 3);
            this.layout.Controls.Add(this.lblStopBit, 0, 4);
            this.layout.Controls.Add(this.btnOK, 1, 6);
            this.layout.Controls.Add(this.btnCancel, 3, 6);
            this.layout.Controls.Add(this.cmbPort, 1, 0);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(3, 10);
            this.layout.Name = "layout";
            this.layout.RowCount = 7;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.65277F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66945F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66945F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66945F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66945F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66945F));
            this.layout.Size = new System.Drawing.Size(380, 316);
            this.layout.TabIndex = 0;
            // 
            // cmbStopBit
            // 
            this.cmbStopBit.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbStopBit.ButtonWidth = 60;
            this.layout.SetColumnSpan(this.cmbStopBit, 4);
            this.cmbStopBit.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.cmbStopBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbStopBit.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbStopBit.ItemHeight = 30;
            this.cmbStopBit.ItemPadding = new System.Windows.Forms.Padding(0);
            this.cmbStopBit.Location = new System.Drawing.Point(117, 206);
            this.cmbStopBit.MaximumViewCount = 10;
            this.cmbStopBit.Name = "cmbStopBit";
            this.cmbStopBit.SelectedIndex = -1;
            this.cmbStopBit.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.cmbStopBit.Size = new System.Drawing.Size(260, 45);
            this.cmbStopBit.TabIndex = 11;
            this.cmbStopBit.Text = "dvComboBox5";
            this.cmbStopBit.TouchMode = false;
            this.cmbStopBit.UseThemeColor = true;
            // 
            // cmbParity
            // 
            this.cmbParity.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbParity.ButtonWidth = 60;
            this.layout.SetColumnSpan(this.cmbParity, 4);
            this.cmbParity.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.cmbParity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbParity.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbParity.ItemHeight = 30;
            this.cmbParity.ItemPadding = new System.Windows.Forms.Padding(0);
            this.cmbParity.Location = new System.Drawing.Point(117, 155);
            this.cmbParity.MaximumViewCount = 10;
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.SelectedIndex = -1;
            this.cmbParity.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.cmbParity.Size = new System.Drawing.Size(260, 45);
            this.cmbParity.TabIndex = 10;
            this.cmbParity.Text = "dvComboBox4";
            this.cmbParity.TouchMode = false;
            this.cmbParity.UseThemeColor = true;
            // 
            // cmbDataBit
            // 
            this.cmbDataBit.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDataBit.ButtonWidth = 60;
            this.layout.SetColumnSpan(this.cmbDataBit, 4);
            this.cmbDataBit.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.cmbDataBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDataBit.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDataBit.ItemHeight = 30;
            this.cmbDataBit.ItemPadding = new System.Windows.Forms.Padding(0);
            this.cmbDataBit.Location = new System.Drawing.Point(117, 104);
            this.cmbDataBit.MaximumViewCount = 10;
            this.cmbDataBit.Name = "cmbDataBit";
            this.cmbDataBit.SelectedIndex = -1;
            this.cmbDataBit.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.cmbDataBit.Size = new System.Drawing.Size(260, 45);
            this.cmbDataBit.TabIndex = 9;
            this.cmbDataBit.Text = "dvComboBox3";
            this.cmbDataBit.TouchMode = false;
            this.cmbDataBit.UseThemeColor = true;
            // 
            // cmbBaudrate
            // 
            this.cmbBaudrate.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBaudrate.ButtonWidth = 60;
            this.layout.SetColumnSpan(this.cmbBaudrate, 4);
            this.cmbBaudrate.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.cmbBaudrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBaudrate.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBaudrate.ItemHeight = 30;
            this.cmbBaudrate.ItemPadding = new System.Windows.Forms.Padding(0);
            this.cmbBaudrate.Location = new System.Drawing.Point(117, 53);
            this.cmbBaudrate.MaximumViewCount = 10;
            this.cmbBaudrate.Name = "cmbBaudrate";
            this.cmbBaudrate.SelectedIndex = -1;
            this.cmbBaudrate.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.cmbBaudrate.Size = new System.Drawing.Size(260, 45);
            this.cmbBaudrate.TabIndex = 8;
            this.cmbBaudrate.Text = "dvComboBox2";
            this.cmbBaudrate.TouchMode = false;
            this.cmbBaudrate.UseThemeColor = true;
            // 
            // lblPort
            // 
            this.lblPort.BackgroundDraw = false;
            this.lblPort.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPort.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblPort.IconGap = 0;
            this.lblPort.IconImage = null;
            this.lblPort.IconSize = 10F;
            this.lblPort.IconString = null;
            this.lblPort.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblPort.Location = new System.Drawing.Point(3, 3);
            this.lblPort.LongClickTime = 0;
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(108, 44);
            this.lblPort.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblPort.TabIndex = 0;
            this.lblPort.TabStop = false;
            this.lblPort.Text = "통신 포트";
            this.lblPort.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblPort.Unit = "";
            this.lblPort.UnitWidth = 36;
            this.lblPort.UseLongClick = false;
            this.lblPort.UseThemeColor = true;
            // 
            // lblBaudrate
            // 
            this.lblBaudrate.BackgroundDraw = false;
            this.lblBaudrate.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblBaudrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBaudrate.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblBaudrate.IconGap = 0;
            this.lblBaudrate.IconImage = null;
            this.lblBaudrate.IconSize = 10F;
            this.lblBaudrate.IconString = null;
            this.lblBaudrate.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblBaudrate.Location = new System.Drawing.Point(3, 53);
            this.lblBaudrate.LongClickTime = 0;
            this.lblBaudrate.Name = "lblBaudrate";
            this.lblBaudrate.Size = new System.Drawing.Size(108, 45);
            this.lblBaudrate.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblBaudrate.TabIndex = 1;
            this.lblBaudrate.TabStop = false;
            this.lblBaudrate.Text = "통신 속도";
            this.lblBaudrate.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblBaudrate.Unit = "";
            this.lblBaudrate.UnitWidth = 36;
            this.lblBaudrate.UseLongClick = false;
            this.lblBaudrate.UseThemeColor = true;
            // 
            // lblDataBit
            // 
            this.lblDataBit.BackgroundDraw = false;
            this.lblDataBit.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblDataBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDataBit.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblDataBit.IconGap = 0;
            this.lblDataBit.IconImage = null;
            this.lblDataBit.IconSize = 10F;
            this.lblDataBit.IconString = null;
            this.lblDataBit.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblDataBit.Location = new System.Drawing.Point(3, 104);
            this.lblDataBit.LongClickTime = 0;
            this.lblDataBit.Name = "lblDataBit";
            this.lblDataBit.Size = new System.Drawing.Size(108, 45);
            this.lblDataBit.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblDataBit.TabIndex = 2;
            this.lblDataBit.TabStop = false;
            this.lblDataBit.Text = "데이터 비트";
            this.lblDataBit.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblDataBit.Unit = "";
            this.lblDataBit.UnitWidth = 36;
            this.lblDataBit.UseLongClick = false;
            this.lblDataBit.UseThemeColor = true;
            // 
            // lblParity
            // 
            this.lblParity.BackgroundDraw = false;
            this.lblParity.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblParity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblParity.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblParity.IconGap = 0;
            this.lblParity.IconImage = null;
            this.lblParity.IconSize = 10F;
            this.lblParity.IconString = null;
            this.lblParity.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblParity.Location = new System.Drawing.Point(3, 155);
            this.lblParity.LongClickTime = 0;
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(108, 45);
            this.lblParity.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblParity.TabIndex = 3;
            this.lblParity.TabStop = false;
            this.lblParity.Text = "패리티 비트";
            this.lblParity.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblParity.Unit = "";
            this.lblParity.UnitWidth = 36;
            this.lblParity.UseLongClick = false;
            this.lblParity.UseThemeColor = true;
            // 
            // lblStopBit
            // 
            this.lblStopBit.BackgroundDraw = false;
            this.lblStopBit.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblStopBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStopBit.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblStopBit.IconGap = 0;
            this.lblStopBit.IconImage = null;
            this.lblStopBit.IconSize = 10F;
            this.lblStopBit.IconString = null;
            this.lblStopBit.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblStopBit.Location = new System.Drawing.Point(3, 206);
            this.lblStopBit.LongClickTime = 0;
            this.lblStopBit.Name = "lblStopBit";
            this.lblStopBit.Size = new System.Drawing.Size(108, 45);
            this.lblStopBit.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblStopBit.TabIndex = 4;
            this.lblStopBit.TabStop = false;
            this.lblStopBit.Text = "정지 비트";
            this.lblStopBit.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblStopBit.Unit = "";
            this.lblStopBit.UnitWidth = 36;
            this.lblStopBit.UseLongClick = false;
            this.lblStopBit.UseThemeColor = true;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundDraw = true;
            this.btnOK.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnOK.Clickable = true;
            this.layout.SetColumnSpan(this.btnOK, 2);
            this.btnOK.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Gradient = true;
            this.btnOK.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnOK.IconGap = 0;
            this.btnOK.IconImage = null;
            this.btnOK.IconSize = 10F;
            this.btnOK.IconString = null;
            this.btnOK.Location = new System.Drawing.Point(117, 267);
            this.btnOK.LongClickTime = 0;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(126, 46);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "확인";
            this.btnOK.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOK.UseLongClick = false;
            this.btnOK.UseThemeColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundDraw = true;
            this.btnCancel.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCancel.Clickable = true;
            this.layout.SetColumnSpan(this.btnCancel, 2);
            this.btnCancel.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Gradient = true;
            this.btnCancel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCancel.IconGap = 0;
            this.btnCancel.IconImage = null;
            this.btnCancel.IconSize = 10F;
            this.btnCancel.IconString = null;
            this.btnCancel.Location = new System.Drawing.Point(249, 267);
            this.btnCancel.LongClickTime = 0;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 46);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnCancel.UseLongClick = false;
            this.btnCancel.UseThemeColor = true;
            // 
            // cmbPort
            // 
            this.cmbPort.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbPort.ButtonWidth = 60;
            this.layout.SetColumnSpan(this.cmbPort, 4);
            this.cmbPort.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.cmbPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPort.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbPort.ItemHeight = 30;
            this.cmbPort.ItemPadding = new System.Windows.Forms.Padding(0);
            this.cmbPort.Location = new System.Drawing.Point(117, 3);
            this.cmbPort.MaximumViewCount = 10;
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.SelectedIndex = -1;
            this.cmbPort.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.cmbPort.Size = new System.Drawing.Size(260, 44);
            this.cmbPort.TabIndex = 7;
            this.cmbPort.Text = "dvComboBox1";
            this.cmbPort.TouchMode = false;
            this.cmbPort.UseThemeColor = true;
            // 
            // DvSerialPortSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 396);
            this.Controls.Add(this.pnl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DvSerialPortSetting";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(7, 60, 7, 7);
            this.Text = "포트설정";
            this.Title = "포트설정";
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-link";
            this.pnl.ResumeLayout(false);
            this.layout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.DvContainer pnl;
        private Containers.DvTableLayoutPanel layout;
        private Controls.DvLabel lblBaudrate;
        private Controls.DvLabel lblPort;
        private Controls.DvComboBox cmbStopBit;
        private Controls.DvComboBox cmbParity;
        private Controls.DvComboBox cmbDataBit;
        private Controls.DvComboBox cmbBaudrate;
        private Controls.DvLabel lblDataBit;
        private Controls.DvLabel lblParity;
        private Controls.DvLabel lblStopBit;
        private Controls.DvButton btnOK;
        private Controls.DvButton btnCancel;
        private Controls.DvComboBox cmbPort;
    }
}