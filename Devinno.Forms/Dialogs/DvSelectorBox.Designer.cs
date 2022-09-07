
namespace Devinno.Forms.Dialogs
{
    partial class DvSelectorBox
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
            this.dvControl1 = new Devinno.Forms.Controls.DvControl();
            this.tpnlBox = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.btnOk = new Devinno.Forms.Controls.DvButton();
            this.dvContainer1.SuspendLayout();
            this.tpnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.tpnl);
            this.dvContainer1.Controls.Add(this.dvControl1);
            this.dvContainer1.Controls.Add(this.tpnlBox);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer1.Location = new System.Drawing.Point(0, 40);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(10);
            this.dvContainer1.ShadowGap = 1;
            this.dvContainer1.Size = new System.Drawing.Size(200, 80);
            this.dvContainer1.TabIndex = 1;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            // 
            // tpnl
            // 
            this.tpnl.ColumnCount = 1;
            this.tpnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpnl.Location = new System.Drawing.Point(10, 10);
            this.tpnl.Name = "tpnl";
            this.tpnl.RowCount = 1;
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnl.Size = new System.Drawing.Size(180, 14);
            this.tpnl.TabIndex = 3;
            // 
            // dvControl1
            // 
            this.dvControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dvControl1.Location = new System.Drawing.Point(10, 24);
            this.dvControl1.Name = "dvControl1";
            this.dvControl1.ShadowGap = 1;
            this.dvControl1.Size = new System.Drawing.Size(180, 10);
            this.dvControl1.TabIndex = 2;
            this.dvControl1.TabStop = false;
            this.dvControl1.Text = "dvControl1";
            // 
            // tpnlBox
            // 
            this.tpnlBox.ColumnCount = 2;
            this.tpnlBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlBox.Controls.Add(this.btnCancel, 1, 0);
            this.tpnlBox.Controls.Add(this.btnOk, 0, 0);
            this.tpnlBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tpnlBox.Location = new System.Drawing.Point(10, 34);
            this.tpnlBox.Name = "tpnlBox";
            this.tpnlBox.RowCount = 1;
            this.tpnlBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tpnlBox.Size = new System.Drawing.Size(180, 36);
            this.tpnlBox.TabIndex = 1;
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
            this.btnCancel.Location = new System.Drawing.Point(93, 3);
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
            this.btnOk.Location = new System.Drawing.Point(3, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Round = null;
            this.btnOk.ShadowGap = 1;
            this.btnOk.Size = new System.Drawing.Size(84, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "확인";
            this.btnOk.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOk.UseKey = false;
            // 
            // DvSelectorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 120);
            this.Controls.Add(this.dvContainer1);
            this.Fixed = true;
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 120);
            this.Name = "DvSelectorBox";
            this.Text = "항목 선택";
            this.Title = "항목 선택";
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-check";
            this.dvContainer1.ResumeLayout(false);
            this.tpnlBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.DvContainer dvContainer1;
        private Containers.DvTableLayoutPanel tpnl;
        private Controls.DvControl dvControl1;
        private Containers.DvTableLayoutPanel tpnlBox;
        private Controls.DvButton btnCancel;
        private Controls.DvButton btnOk;
    }
}