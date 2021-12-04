
namespace Devinno.Forms.Dialogs
{
    partial class DvMessageBox
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
            this.btnOk = new Devinno.Forms.Controls.DvButton();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.btnYes = new Devinno.Forms.Controls.DvButton();
            this.btnNo = new Devinno.Forms.Controls.DvButton();
            this.lblMessage = new Devinno.Forms.Controls.DvLabel();
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
            this.pnl.Size = new System.Drawing.Size(252, 99);
            this.pnl.TabIndex = 100;
            this.pnl.TabStop = false;
            this.pnl.Text = "dvContainer1";
            this.pnl.UseThemeColor = true;
            // 
            // layout
            // 
            this.layout.ColumnCount = 8;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.layout.Controls.Add(this.btnOk, 1, 1);
            this.layout.Controls.Add(this.btnCancel, 2, 1);
            this.layout.Controls.Add(this.btnYes, 3, 1);
            this.layout.Controls.Add(this.btnNo, 4, 1);
            this.layout.Controls.Add(this.lblMessage, 0, 0);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(3, 10);
            this.layout.Margin = new System.Windows.Forms.Padding(0);
            this.layout.Name = "layout";
            this.layout.RowCount = 2;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.layout.Size = new System.Drawing.Size(246, 86);
            this.layout.TabIndex = 101;
            // 
            // btnOk
            // 
            this.btnOk.BackgroundDraw = true;
            this.btnOk.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnOk.Clickable = true;
            this.btnOk.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOk.Gradient = true;
            this.btnOk.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnOk.IconGap = 0;
            this.btnOk.IconImage = null;
            this.btnOk.IconSize = 10F;
            this.btnOk.IconString = "fa-check";
            this.btnOk.Location = new System.Drawing.Point(37, 53);
            this.btnOk.LongClickTime = 0;
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(23, 30);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "확인";
            this.btnOk.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOk.UseKey = false;
            this.btnOk.UseLongClick = false;
            this.btnOk.UseThemeColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundDraw = true;
            this.btnCancel.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCancel.Clickable = true;
            this.btnCancel.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Gradient = true;
            this.btnCancel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCancel.IconGap = 0;
            this.btnCancel.IconImage = null;
            this.btnCancel.IconSize = 10F;
            this.btnCancel.IconString = "fa-times";
            this.btnCancel.Location = new System.Drawing.Point(66, 53);
            this.btnCancel.LongClickTime = 0;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(23, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnCancel.UseKey = false;
            this.btnCancel.UseLongClick = false;
            this.btnCancel.UseThemeColor = true;
            // 
            // btnYes
            // 
            this.btnYes.BackgroundDraw = true;
            this.btnYes.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnYes.Clickable = true;
            this.btnYes.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnYes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnYes.Gradient = true;
            this.btnYes.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnYes.IconGap = 0;
            this.btnYes.IconImage = null;
            this.btnYes.IconSize = 10F;
            this.btnYes.IconString = "far fa-check-circle";
            this.btnYes.Location = new System.Drawing.Point(95, 53);
            this.btnYes.LongClickTime = 0;
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(23, 30);
            this.btnYes.TabIndex = 3;
            this.btnYes.Text = "예";
            this.btnYes.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnYes.UseKey = false;
            this.btnYes.UseLongClick = false;
            this.btnYes.UseThemeColor = true;
            // 
            // btnNo
            // 
            this.btnNo.BackgroundDraw = true;
            this.btnNo.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnNo.Clickable = true;
            this.btnNo.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNo.Gradient = true;
            this.btnNo.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnNo.IconGap = 0;
            this.btnNo.IconImage = null;
            this.btnNo.IconSize = 10F;
            this.btnNo.IconString = "far fa-times-circle";
            this.btnNo.Location = new System.Drawing.Point(124, 53);
            this.btnNo.LongClickTime = 0;
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(23, 30);
            this.btnNo.TabIndex = 4;
            this.btnNo.Text = "아니요";
            this.btnNo.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnNo.UseKey = false;
            this.btnNo.UseLongClick = false;
            this.btnNo.UseThemeColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.BackgroundDraw = false;
            this.layout.SetColumnSpan(this.lblMessage, 8);
            this.lblMessage.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblMessage.IconGap = 0;
            this.lblMessage.IconImage = null;
            this.lblMessage.IconSize = 10F;
            this.lblMessage.IconString = null;
            this.lblMessage.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblMessage.Location = new System.Drawing.Point(3, 3);
            this.lblMessage.LongClickTime = 0;
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(240, 44);
            this.lblMessage.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblMessage.TabIndex = 103;
            this.lblMessage.TabStop = false;
            this.lblMessage.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblMessage.Unit = "";
            this.lblMessage.UnitWidth = 36;
            this.lblMessage.UseLongClick = false;
            this.lblMessage.UseThemeColor = true;
            // 
            // DvMessageBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(266, 166);
            this.Controls.Add(this.pnl);
            this.Fixed = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DvMessageBox";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(7, 60, 7, 7);
            this.Text = "";
            this.Title = "";
            this.TitleFont = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-comment-dots";
            this.pnl.ResumeLayout(false);
            this.layout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.DvContainer pnl;
        private Containers.DvTableLayoutPanel layout;
        private Controls.DvLabel lblMessage;
        private Controls.DvButton btnOk;
        private Controls.DvButton btnCancel;
        private Controls.DvButton btnYes;
        private Controls.DvButton btnNo;
    }
}