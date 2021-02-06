
namespace Devinno.Forms.Dialogs
{
    partial class DvColorPickerDialog
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
            this.tbl = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.lblB = new Devinno.Forms.Controls.DvValueLabel();
            this.lblR = new Devinno.Forms.Controls.DvValueLabel();
            this.lblG = new Devinno.Forms.Controls.DvValueLabel();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.lblColor = new Devinno.Forms.Controls.DvLabel();
            this.draw = new Devinno.Forms.Controls.DvControl();
            this.tbl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbl
            // 
            this.tbl.ColumnCount = 3;
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tbl.Controls.Add(this.lblB, 2, 0);
            this.tbl.Controls.Add(this.lblR, 0, 0);
            this.tbl.Controls.Add(this.lblG, 1, 0);
            this.tbl.Controls.Add(this.btnCancel, 2, 1);
            this.tbl.Controls.Add(this.btnOK, 1, 1);
            this.tbl.Controls.Add(this.lblColor, 0, 1);
            this.tbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbl.Location = new System.Drawing.Point(7, 420);
            this.tbl.Name = "tbl";
            this.tbl.RowCount = 2;
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl.Size = new System.Drawing.Size(424, 102);
            this.tbl.TabIndex = 0;
            // 
            // lblB
            // 
            this.lblB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblB.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblB.IconGap = 0;
            this.lblB.IconImage = null;
            this.lblB.IconSize = 10F;
            this.lblB.IconString = null;
            this.lblB.Location = new System.Drawing.Point(285, 3);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(136, 45);
            this.lblB.Style = Devinno.Forms.Controls.LabelStyle.FLAT;
            this.lblB.TabIndex = 3;
            this.lblB.TabStop = false;
            this.lblB.Text = "B";
            this.lblB.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblB.TitleWidth = 60;
            this.lblB.Unit = "";
            this.lblB.UnitWidth = 36;
            this.lblB.UseThemeColor = true;
            this.lblB.Value = null;
            this.lblB.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // lblR
            // 
            this.lblR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblR.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblR.IconGap = 0;
            this.lblR.IconImage = null;
            this.lblR.IconSize = 10F;
            this.lblR.IconString = null;
            this.lblR.Location = new System.Drawing.Point(3, 3);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(135, 45);
            this.lblR.Style = Devinno.Forms.Controls.LabelStyle.FLAT;
            this.lblR.TabIndex = 2;
            this.lblR.TabStop = false;
            this.lblR.Text = "R";
            this.lblR.TitleBoxColor = System.Drawing.Color.Red;
            this.lblR.TitleWidth = 60;
            this.lblR.Unit = "";
            this.lblR.UnitWidth = 36;
            this.lblR.UseThemeColor = true;
            this.lblR.Value = null;
            this.lblR.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // lblG
            // 
            this.lblG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblG.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblG.IconGap = 0;
            this.lblG.IconImage = null;
            this.lblG.IconSize = 10F;
            this.lblG.IconString = null;
            this.lblG.Location = new System.Drawing.Point(144, 3);
            this.lblG.Name = "lblG";
            this.lblG.Size = new System.Drawing.Size(135, 45);
            this.lblG.Style = Devinno.Forms.Controls.LabelStyle.FLAT;
            this.lblG.TabIndex = 1;
            this.lblG.TabStop = false;
            this.lblG.Text = "G";
            this.lblG.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblG.TitleWidth = 60;
            this.lblG.Unit = "";
            this.lblG.UnitWidth = 36;
            this.lblG.UseThemeColor = true;
            this.lblG.Value = null;
            this.lblG.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
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
            this.btnCancel.IconString = null;
            this.btnCancel.Location = new System.Drawing.Point(285, 54);
            this.btnCancel.LongClickTime = 0;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 45);
            this.btnCancel.TabIndex = 0;
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
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Gradient = true;
            this.btnOK.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnOK.IconGap = 0;
            this.btnOK.IconImage = null;
            this.btnOK.IconSize = 10F;
            this.btnOK.IconString = null;
            this.btnOK.Location = new System.Drawing.Point(144, 54);
            this.btnOK.LongClickTime = 0;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(135, 45);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "확인";
            this.btnOK.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOK.UseLongClick = false;
            this.btnOK.UseThemeColor = true;
            // 
            // lblColor
            // 
            this.lblColor.BackgroundDraw = true;
            this.lblColor.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblColor.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblColor.IconGap = 0;
            this.lblColor.IconImage = null;
            this.lblColor.IconSize = 10F;
            this.lblColor.IconString = null;
            this.lblColor.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblColor.Location = new System.Drawing.Point(3, 54);
            this.lblColor.LongClickTime = 0;
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(62, 45);
            this.lblColor.Style = Devinno.Forms.Controls.LabelStyle.FLAT;
            this.lblColor.TabIndex = 4;
            this.lblColor.TabStop = false;
            this.lblColor.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblColor.Unit = "";
            this.lblColor.UnitWidth = 36;
            this.lblColor.UseLongClick = false;
            this.lblColor.UseThemeColor = false;
            // 
            // draw
            // 
            this.draw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.draw.Location = new System.Drawing.Point(7, 60);
            this.draw.Name = "draw";
            this.draw.Size = new System.Drawing.Size(424, 360);
            this.draw.TabIndex = 3;
            this.draw.TabStop = false;
            this.draw.Text = "dvControl1";
            this.draw.UseThemeColor = true;
            // 
            // DvColorPickerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 529);
            this.Controls.Add(this.draw);
            this.Controls.Add(this.tbl);
            this.Fixed = true;
            this.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DvColorPickerDialog";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(7, 60, 7, 7);
            this.Text = "색상 선택";
            this.Title = "색상 선택";
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-palette";
            this.tbl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.DvTableLayoutPanel tbl;
        private Controls.DvValueLabel lblB;
        private Controls.DvValueLabel lblR;
        private Controls.DvValueLabel lblG;
        private Controls.DvButton btnCancel;
        private Controls.DvButton btnOK;
        private Controls.DvControl draw;
        private Controls.DvLabel lblColor;
    }
}