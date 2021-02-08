
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
            this.lblMessage = new Devinno.Forms.Controls.DvLabel();
            this.btnOk = new Devinno.Forms.Controls.DvButton();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.btnYes = new Devinno.Forms.Controls.DvButton();
            this.btnNo = new Devinno.Forms.Controls.DvButton();
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
            this.pnl.Size = new System.Drawing.Size(586, 233);
            this.pnl.TabIndex = 0;
            this.pnl.TabStop = false;
            this.pnl.Text = "dvContainer1";
            this.pnl.UseThemeColor = true;
            // 
            // layout
            // 
            this.layout.ColumnCount = 13;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layout.Controls.Add(this.lblMessage, 0, 0);
            this.layout.Controls.Add(this.btnOk, 1, 2);
            this.layout.Controls.Add(this.btnCancel, 3, 2);
            this.layout.Controls.Add(this.btnYes, 5, 2);
            this.layout.Controls.Add(this.btnNo, 7, 2);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(3, 10);
            this.layout.Margin = new System.Windows.Forms.Padding(0);
            this.layout.Name = "layout";
            this.layout.RowCount = 3;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.Size = new System.Drawing.Size(580, 220);
            this.layout.TabIndex = 0;
            // 
            // lblMessage
            // 
            this.lblMessage.BackgroundDraw = false;
            this.layout.SetColumnSpan(this.lblMessage, 13);
            this.lblMessage.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblMessage.IconGap = 0;
            this.lblMessage.IconImage = null;
            this.lblMessage.IconSize = 10F;
            this.lblMessage.IconString = null;
            this.lblMessage.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.LongClickTime = 0;
            this.lblMessage.Margin = new System.Windows.Forms.Padding(0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(580, 147);
            this.lblMessage.Style = Devinno.Forms.Controls.LabelStyle.Convex;
            this.lblMessage.TabIndex = 0;
            this.lblMessage.TabStop = false;
            this.lblMessage.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblMessage.Unit = "";
            this.lblMessage.UnitWidth = 36;
            this.lblMessage.UseLongClick = false;
            this.lblMessage.UseThemeColor = true;
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
            this.btnOk.Location = new System.Drawing.Point(106, 157);
            this.btnOk.LongClickTime = 0;
            this.btnOk.Margin = new System.Windows.Forms.Padding(0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(53, 63);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "확인";
            this.btnOk.TextPadding = new System.Windows.Forms.Padding(0);
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
            this.btnCancel.Location = new System.Drawing.Point(169, 157);
            this.btnCancel.LongClickTime = 0;
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(53, 63);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextPadding = new System.Windows.Forms.Padding(0);
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
            this.btnYes.Location = new System.Drawing.Point(232, 157);
            this.btnYes.LongClickTime = 0;
            this.btnYes.Margin = new System.Windows.Forms.Padding(0);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(53, 63);
            this.btnYes.TabIndex = 4;
            this.btnYes.Text = "예";
            this.btnYes.TextPadding = new System.Windows.Forms.Padding(0);
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
            this.btnNo.Location = new System.Drawing.Point(295, 157);
            this.btnNo.LongClickTime = 0;
            this.btnNo.Margin = new System.Windows.Forms.Padding(0);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(53, 63);
            this.btnNo.TabIndex = 5;
            this.btnNo.Text = "아니요";
            this.btnNo.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnNo.UseLongClick = false;
            this.btnNo.UseThemeColor = true;
            // 
            // DvMessageBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(600, 300);
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
        private Containers.DvContainer dvContainer1;
        private Controls.DvLabel lblMessage;
        private Controls.DvButton btnOk;
        private Controls.DvButton btnCancel;
        private Controls.DvButton btnYes;
        private Controls.DvButton btnNo;
    }
}