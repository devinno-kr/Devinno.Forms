
namespace Devinno.Forms.Dialogs
{
    partial class DvInputBox
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
            this.gpV = new Devinno.Forms.Controls.DvControl();
            this.pnlBtn = new Devinno.Forms.Containers.DvContainer();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.gpH = new Devinno.Forms.Controls.DvControl();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.pnl.SuspendLayout();
            this.pnlBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.layout);
            this.pnl.Controls.Add(this.gpV);
            this.pnl.Controls.Add(this.pnlBtn);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(7, 60);
            this.pnl.Name = "pnl";
            this.pnl.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.pnl.Size = new System.Drawing.Size(466, 113);
            this.pnl.TabIndex = 3;
            this.pnl.TabStop = false;
            this.pnl.Text = "dvContainer1";
            this.pnl.UseThemeColor = true;
            // 
            // layout
            // 
            this.layout.ColumnCount = 1;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 7);
            this.layout.Name = "layout";
            this.layout.RowCount = 1;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Size = new System.Drawing.Size(466, 51);
            this.layout.TabIndex = 4;
            // 
            // gpV
            // 
            this.gpV.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpV.Location = new System.Drawing.Point(0, 58);
            this.gpV.Name = "gpV";
            this.gpV.Size = new System.Drawing.Size(466, 4);
            this.gpV.TabIndex = 2;
            this.gpV.TabStop = false;
            this.gpV.Text = "dvControl1";
            this.gpV.UseThemeColor = true;
            // 
            // pnlBtn
            // 
            this.pnlBtn.Controls.Add(this.btnOK);
            this.pnlBtn.Controls.Add(this.gpH);
            this.pnlBtn.Controls.Add(this.btnCancel);
            this.pnlBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBtn.Location = new System.Drawing.Point(0, 62);
            this.pnlBtn.Name = "pnlBtn";
            this.pnlBtn.Padding = new System.Windows.Forms.Padding(3);
            this.pnlBtn.Size = new System.Drawing.Size(466, 51);
            this.pnlBtn.TabIndex = 1;
            this.pnlBtn.TabStop = false;
            this.pnlBtn.Text = "dvContainer1";
            this.pnlBtn.UseThemeColor = true;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundDraw = true;
            this.btnOK.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnOK.Clickable = true;
            this.btnOK.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Gradient = true;
            this.btnOK.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnOK.IconGap = 0;
            this.btnOK.IconImage = null;
            this.btnOK.IconSize = 10F;
            this.btnOK.IconString = null;
            this.btnOK.Location = new System.Drawing.Point(219, 3);
            this.btnOK.LongClickTime = 0;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 45);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "확인";
            this.btnOK.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOK.UseLongClick = false;
            this.btnOK.UseThemeColor = true;
            // 
            // gpH
            // 
            this.gpH.Dock = System.Windows.Forms.DockStyle.Right;
            this.gpH.Location = new System.Drawing.Point(339, 3);
            this.gpH.Name = "gpH";
            this.gpH.Size = new System.Drawing.Size(4, 45);
            this.gpH.TabIndex = 3;
            this.gpH.TabStop = false;
            this.gpH.Text = "dvControl2";
            this.gpH.UseThemeColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundDraw = true;
            this.btnCancel.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCancel.Clickable = true;
            this.btnCancel.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Gradient = true;
            this.btnCancel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCancel.IconGap = 0;
            this.btnCancel.IconImage = null;
            this.btnCancel.IconSize = 10F;
            this.btnCancel.IconString = null;
            this.btnCancel.Location = new System.Drawing.Point(343, 3);
            this.btnCancel.LongClickTime = 0;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 45);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnCancel.UseLongClick = false;
            this.btnCancel.UseThemeColor = true;
            // 
            // DvInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 180);
            this.Controls.Add(this.pnl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DvInputBox";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(7, 60, 7, 7);
            this.Text = "입력";
            this.Title = "입력";
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-clipboard-list";
            this.pnl.ResumeLayout(false);
            this.pnlBtn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.DvContainer pnl;
        private Containers.DvTableLayoutPanel layout;
        private Controls.DvControl gpV;
        private Containers.DvContainer pnlBtn;
        private Controls.DvButton btnOK;
        private Controls.DvControl gpH;
        private Controls.DvButton btnCancel;
    }
}