
namespace Devinno.Forms.Dialogs
{
    partial class DvWheelPickerBox
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
            this.wheelPicker = new Devinno.Forms.Controls.DvWheelPicker();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.dvContainer1.SuspendLayout();
            this.tpnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.tpnl);
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
            this.tpnl.ColumnCount = 2;
            this.tpnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tpnl.Controls.Add(this.wheelPicker, 0, 0);
            this.tpnl.Controls.Add(this.btnOK, 0, 2);
            this.tpnl.Controls.Add(this.btnCancel, 1, 2);
            this.tpnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpnl.Location = new System.Drawing.Point(10, 10);
            this.tpnl.Name = "tpnl";
            this.tpnl.RowCount = 3;
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tpnl.Size = new System.Drawing.Size(280, 240);
            this.tpnl.TabIndex = 0;
            // 
            // wheelPicker
            // 
            this.tpnl.SetColumnSpan(this.wheelPicker, 2);
            this.wheelPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wheelPicker.ItemHeight = 30;
            this.wheelPicker.Location = new System.Drawing.Point(3, 3);
            this.wheelPicker.Name = "wheelPicker";
            this.wheelPicker.SelectedIndex = -1;
            this.wheelPicker.ShadowGap = 1;
            this.wheelPicker.Size = new System.Drawing.Size(274, 188);
            this.wheelPicker.TabIndex = 1;
            this.wheelPicker.Text = "dvWheelPicker1";
            this.wheelPicker.TouchMode = true;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundDraw = true;
            this.btnOK.ButtonColor = null;
            this.btnOK.Clickable = true;
            this.btnOK.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Gradient = true;
            this.btnOK.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnOK.IconGap = 0;
            this.btnOK.IconImage = null;
            this.btnOK.IconSize = 12F;
            this.btnOK.IconString = null;
            this.btnOK.Location = new System.Drawing.Point(3, 207);
            this.btnOK.Name = "btnOK";
            this.btnOK.Round = null;
            this.btnOK.ShadowGap = 1;
            this.btnOK.Size = new System.Drawing.Size(134, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "확인";
            this.btnOK.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOK.UseKey = false;
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
            this.btnCancel.Location = new System.Drawing.Point(143, 207);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Round = null;
            this.btnCancel.ShadowGap = 1;
            this.btnCancel.Size = new System.Drawing.Size(134, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnCancel.UseKey = false;
            // 
            // DvWheelPickerBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.dvContainer1);
            this.Fixed = true;
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 140);
            this.Name = "DvWheelPickerBox";
            this.Text = "";
            this.Title = "";
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-check";
            this.dvContainer1.ResumeLayout(false);
            this.tpnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.DvContainer dvContainer1;
        private Containers.DvTableLayoutPanel tpnl;
        private Controls.DvButton btnOK;
        private Controls.DvButton btnCancel;
        private Controls.DvWheelPicker wheelPicker;
    }
}