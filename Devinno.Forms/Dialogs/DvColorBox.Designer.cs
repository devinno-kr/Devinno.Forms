
namespace Devinno.Forms.Dialogs
{
    partial class DvColorBox
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
            this.txtR = new Devinno.Forms.Controls.DvTextBox();
            this.lblG = new Devinno.Forms.Controls.DvLabel();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.txtG = new Devinno.Forms.Controls.DvTextBox();
            this.txtB = new Devinno.Forms.Controls.DvTextBox();
            this.lblB = new Devinno.Forms.Controls.DvLabel();
            this.lblR = new Devinno.Forms.Controls.DvLabel();
            this.lblColor = new Devinno.Forms.Controls.DvLabel();
            this.SuspendLayout();
            // 
            // txtR
            // 
            this.txtR.InputType = Devinno.Forms.Controls.DvInputType.TEXT;
            this.txtR.Location = new System.Drawing.Point(10, 102);
            this.txtR.MaxLength = 32767;
            this.txtR.MinusInput = false;
            this.txtR.MultiLine = false;
            this.txtR.Name = "txtR";
            this.txtR.Size = new System.Drawing.Size(107, 43);
            this.txtR.Style = Devinno.Forms.Controls.LabelStyle.FlatConcave;
            this.txtR.TabIndex = 19;
            this.txtR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtR.TextBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtR.TextPadding = new System.Windows.Forms.Padding(0);
            this.txtR.Unit = "";
            this.txtR.UnitWidth = 36;
            this.txtR.UseThemeColor = true;
            // 
            // lblG
            // 
            this.lblG.BackgroundDraw = false;
            this.lblG.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblG.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblG.IconGap = 0;
            this.lblG.IconImage = null;
            this.lblG.IconSize = 10F;
            this.lblG.IconString = null;
            this.lblG.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblG.Location = new System.Drawing.Point(123, 71);
            this.lblG.LongClickTime = 0;
            this.lblG.Name = "lblG";
            this.lblG.Size = new System.Drawing.Size(107, 25);
            this.lblG.Style = Devinno.Forms.Controls.LabelStyle.FlatConvex;
            this.lblG.TabIndex = 17;
            this.lblG.TabStop = false;
            this.lblG.Text = "G";
            this.lblG.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblG.Unit = "";
            this.lblG.UnitWidth = 36;
            this.lblG.UseLongClick = false;
            this.lblG.UseThemeColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundDraw = true;
            this.btnCancel.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnCancel.Clickable = true;
            this.btnCancel.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnCancel.Gradient = true;
            this.btnCancel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCancel.IconGap = 0;
            this.btnCancel.IconImage = null;
            this.btnCancel.IconSize = 10F;
            this.btnCancel.IconString = null;
            this.btnCancel.Location = new System.Drawing.Point(236, 151);
            this.btnCancel.LongClickTime = 0;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 45);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnCancel.UseLongClick = false;
            this.btnCancel.UseThemeColor = true;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundDraw = true;
            this.btnOK.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnOK.Clickable = true;
            this.btnOK.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnOK.Gradient = true;
            this.btnOK.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnOK.IconGap = 0;
            this.btnOK.IconImage = null;
            this.btnOK.IconSize = 10F;
            this.btnOK.IconString = null;
            this.btnOK.Location = new System.Drawing.Point(123, 151);
            this.btnOK.LongClickTime = 0;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 45);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "확인";
            this.btnOK.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOK.UseLongClick = false;
            this.btnOK.UseThemeColor = true;
            // 
            // txtG
            // 
            this.txtG.InputType = Devinno.Forms.Controls.DvInputType.TEXT;
            this.txtG.Location = new System.Drawing.Point(123, 102);
            this.txtG.MaxLength = 32767;
            this.txtG.MinusInput = false;
            this.txtG.MultiLine = false;
            this.txtG.Name = "txtG";
            this.txtG.Size = new System.Drawing.Size(107, 43);
            this.txtG.Style = Devinno.Forms.Controls.LabelStyle.FlatConcave;
            this.txtG.TabIndex = 14;
            this.txtG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtG.TextBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtG.TextPadding = new System.Windows.Forms.Padding(0);
            this.txtG.Unit = "";
            this.txtG.UnitWidth = 36;
            this.txtG.UseThemeColor = true;
            // 
            // txtB
            // 
            this.txtB.InputType = Devinno.Forms.Controls.DvInputType.TEXT;
            this.txtB.Location = new System.Drawing.Point(236, 102);
            this.txtB.MaxLength = 32767;
            this.txtB.MinusInput = false;
            this.txtB.MultiLine = false;
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(108, 43);
            this.txtB.Style = Devinno.Forms.Controls.LabelStyle.FlatConcave;
            this.txtB.TabIndex = 15;
            this.txtB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtB.TextBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtB.TextPadding = new System.Windows.Forms.Padding(0);
            this.txtB.Unit = "";
            this.txtB.UnitWidth = 36;
            this.txtB.UseThemeColor = true;
            // 
            // lblB
            // 
            this.lblB.BackgroundDraw = false;
            this.lblB.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblB.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblB.IconGap = 0;
            this.lblB.IconImage = null;
            this.lblB.IconSize = 10F;
            this.lblB.IconString = null;
            this.lblB.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblB.Location = new System.Drawing.Point(236, 71);
            this.lblB.LongClickTime = 0;
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(108, 25);
            this.lblB.Style = Devinno.Forms.Controls.LabelStyle.FlatConvex;
            this.lblB.TabIndex = 16;
            this.lblB.TabStop = false;
            this.lblB.Text = "B";
            this.lblB.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblB.Unit = "";
            this.lblB.UnitWidth = 36;
            this.lblB.UseLongClick = false;
            this.lblB.UseThemeColor = true;
            // 
            // lblR
            // 
            this.lblR.BackgroundDraw = false;
            this.lblR.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblR.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblR.IconGap = 0;
            this.lblR.IconImage = null;
            this.lblR.IconSize = 10F;
            this.lblR.IconString = null;
            this.lblR.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblR.Location = new System.Drawing.Point(10, 71);
            this.lblR.LongClickTime = 0;
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(107, 25);
            this.lblR.Style = Devinno.Forms.Controls.LabelStyle.FlatConvex;
            this.lblR.TabIndex = 18;
            this.lblR.TabStop = false;
            this.lblR.Text = "R";
            this.lblR.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblR.Unit = "";
            this.lblR.UnitWidth = 36;
            this.lblR.UseLongClick = false;
            this.lblR.UseThemeColor = true;
            // 
            // lblColor
            // 
            this.lblColor.BackgroundDraw = true;
            this.lblColor.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblColor.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblColor.IconGap = 0;
            this.lblColor.IconImage = null;
            this.lblColor.IconSize = 10F;
            this.lblColor.IconString = null;
            this.lblColor.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblColor.Location = new System.Drawing.Point(10, 151);
            this.lblColor.LongClickTime = 0;
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(62, 45);
            this.lblColor.Style = Devinno.Forms.Controls.LabelStyle.Convex;
            this.lblColor.TabIndex = 20;
            this.lblColor.TabStop = false;
            this.lblColor.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblColor.Unit = "";
            this.lblColor.UnitWidth = 36;
            this.lblColor.UseLongClick = false;
            this.lblColor.UseThemeColor = false;
            // 
            // DvColorBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(354, 206);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.txtR);
            this.Controls.Add(this.lblG);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtG);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblR);
            this.Fixed = true;
            this.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DvColorBox";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(7, 60, 7, 7);
            this.Text = "색상 입력";
            this.Title = "색상 입력";
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-eye-dropper";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DvTextBox txtR;
        private Controls.DvLabel lblG;
        private Controls.DvButton btnCancel;
        private Controls.DvButton btnOK;
        private Controls.DvTextBox txtG;
        private Controls.DvTextBox txtB;
        private Controls.DvLabel lblB;
        private Controls.DvLabel lblR;
        private Controls.DvLabel lblColor;
    }
}