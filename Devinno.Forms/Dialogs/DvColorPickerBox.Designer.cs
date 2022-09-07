
namespace Devinno.Forms.Dialogs
{
    partial class DvColorPickerBox
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
            this.layout = new Devinno.Forms.Containers.DvContainer();
            this.dvTableLayoutPanel1 = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.color = new Devinno.Forms.Controls.DvControl();
            this.hue = new Devinno.Forms.Controls.DvControl();
            this.lbl = new Devinno.Forms.Controls.DvLabel();
            this.inR = new Devinno.Forms.Controls.DvValueInputInt();
            this.inG = new Devinno.Forms.Controls.DvValueInputInt();
            this.inB = new Devinno.Forms.Controls.DvValueInputInt();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.layout.SuspendLayout();
            this.dvTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.dvTableLayoutPanel1);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 40);
            this.layout.Name = "layout";
            this.layout.Padding = new System.Windows.Forms.Padding(10);
            this.layout.ShadowGap = 1;
            this.layout.Size = new System.Drawing.Size(422, 282);
            this.layout.TabIndex = 0;
            this.layout.TabStop = false;
            this.layout.Text = "dvContainer1";
            // 
            // dvTableLayoutPanel1
            // 
            this.dvTableLayoutPanel1.ColumnCount = 5;
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 262F));
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.dvTableLayoutPanel1.Controls.Add(this.color, 0, 0);
            this.dvTableLayoutPanel1.Controls.Add(this.hue, 2, 0);
            this.dvTableLayoutPanel1.Controls.Add(this.lbl, 4, 0);
            this.dvTableLayoutPanel1.Controls.Add(this.inR, 4, 1);
            this.dvTableLayoutPanel1.Controls.Add(this.inG, 4, 2);
            this.dvTableLayoutPanel1.Controls.Add(this.inB, 4, 3);
            this.dvTableLayoutPanel1.Controls.Add(this.btnOK, 4, 5);
            this.dvTableLayoutPanel1.Controls.Add(this.btnCancel, 4, 6);
            this.dvTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvTableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.dvTableLayoutPanel1.Name = "dvTableLayoutPanel1";
            this.dvTableLayoutPanel1.RowCount = 7;
            this.dvTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.dvTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.dvTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.dvTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.dvTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dvTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.dvTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.dvTableLayoutPanel1.Size = new System.Drawing.Size(402, 262);
            this.dvTableLayoutPanel1.TabIndex = 0;
            // 
            // color
            // 
            this.color.Dock = System.Windows.Forms.DockStyle.Fill;
            this.color.Location = new System.Drawing.Point(3, 3);
            this.color.Name = "color";
            this.dvTableLayoutPanel1.SetRowSpan(this.color, 7);
            this.color.ShadowGap = 1;
            this.color.Size = new System.Drawing.Size(256, 256);
            this.color.TabIndex = 0;
            this.color.TabStop = false;
            this.color.Text = "dvControl1";
            // 
            // hue
            // 
            this.hue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hue.Location = new System.Drawing.Point(272, 3);
            this.hue.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.hue.Name = "hue";
            this.dvTableLayoutPanel1.SetRowSpan(this.hue, 7);
            this.hue.ShadowGap = 1;
            this.hue.Size = new System.Drawing.Size(20, 256);
            this.hue.TabIndex = 1;
            this.hue.TabStop = false;
            this.hue.Text = "dvControl1";
            // 
            // lbl
            // 
            this.lbl.BackgroundDraw = true;
            this.lbl.BorderColor = null;
            this.lbl.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lbl.IconGap = 0;
            this.lbl.IconImage = null;
            this.lbl.IconSize = 12F;
            this.lbl.IconString = null;
            this.lbl.LabelColor = null;
            this.lbl.Location = new System.Drawing.Point(305, 3);
            this.lbl.Name = "lbl";
            this.lbl.Round = null;
            this.lbl.ShadowGap = 1;
            this.lbl.Size = new System.Drawing.Size(94, 30);
            this.lbl.Style = Devinno.Forms.Embossing.FlatConvex;
            this.lbl.TabIndex = 2;
            this.lbl.TabStop = false;
            this.lbl.Text = null;
            this.lbl.TextPadding = new System.Windows.Forms.Padding(0);
            this.lbl.Unit = "";
            this.lbl.UnitWidth = null;
            // 
            // inR
            // 
            this.inR.Button = null;
            this.inR.ButtonColor = null;
            this.inR.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inR.ButtonIconGap = 0;
            this.inR.ButtonIconImage = null;
            this.inR.ButtonIconSize = 12F;
            this.inR.ButtonIconString = null;
            this.inR.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inR.ButtonWidth = null;
            this.inR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inR.ErrorColor = null;
            this.inR.Location = new System.Drawing.Point(305, 39);
            this.inR.Maximum = 255;
            this.inR.Minimum = 0;
            this.inR.Name = "inR";
            this.inR.Round = null;
            this.inR.ShadowGap = 1;
            this.inR.Size = new System.Drawing.Size(94, 30);
            this.inR.TabIndex = 3;
            this.inR.Text = "R";
            this.inR.Title = "R";
            this.inR.TitleColor = null;
            this.inR.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inR.TitleIconGap = 0;
            this.inR.TitleIconImage = null;
            this.inR.TitleIconSize = 12F;
            this.inR.TitleIconString = null;
            this.inR.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inR.TitleWidth = 30;
            this.inR.Unit = "";
            this.inR.UnitWidth = null;
            this.inR.Value = 0;
            this.inR.ValueColor = null;
            // 
            // inG
            // 
            this.inG.Button = null;
            this.inG.ButtonColor = null;
            this.inG.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inG.ButtonIconGap = 0;
            this.inG.ButtonIconImage = null;
            this.inG.ButtonIconSize = 12F;
            this.inG.ButtonIconString = null;
            this.inG.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inG.ButtonWidth = null;
            this.inG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inG.ErrorColor = null;
            this.inG.Location = new System.Drawing.Point(305, 75);
            this.inG.Maximum = 255;
            this.inG.Minimum = 0;
            this.inG.Name = "inG";
            this.inG.Round = null;
            this.inG.ShadowGap = 1;
            this.inG.Size = new System.Drawing.Size(94, 30);
            this.inG.TabIndex = 4;
            this.inG.Text = "G";
            this.inG.Title = "G";
            this.inG.TitleColor = null;
            this.inG.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inG.TitleIconGap = 0;
            this.inG.TitleIconImage = null;
            this.inG.TitleIconSize = 12F;
            this.inG.TitleIconString = null;
            this.inG.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inG.TitleWidth = 30;
            this.inG.Unit = "";
            this.inG.UnitWidth = null;
            this.inG.Value = 0;
            this.inG.ValueColor = null;
            // 
            // inB
            // 
            this.inB.Button = null;
            this.inB.ButtonColor = null;
            this.inB.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inB.ButtonIconGap = 0;
            this.inB.ButtonIconImage = null;
            this.inB.ButtonIconSize = 12F;
            this.inB.ButtonIconString = null;
            this.inB.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inB.ButtonWidth = null;
            this.inB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inB.ErrorColor = null;
            this.inB.Location = new System.Drawing.Point(305, 111);
            this.inB.Maximum = 255;
            this.inB.Minimum = 0;
            this.inB.Name = "inB";
            this.inB.Round = null;
            this.inB.ShadowGap = 1;
            this.inB.Size = new System.Drawing.Size(94, 30);
            this.inB.TabIndex = 5;
            this.inB.Text = "B";
            this.inB.Title = "B";
            this.inB.TitleColor = null;
            this.inB.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inB.TitleIconGap = 0;
            this.inB.TitleIconImage = null;
            this.inB.TitleIconSize = 12F;
            this.inB.TitleIconString = null;
            this.inB.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inB.TitleWidth = 30;
            this.inB.Unit = "";
            this.inB.UnitWidth = null;
            this.inB.Value = 0;
            this.inB.ValueColor = null;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundDraw = true;
            this.btnOK.ButtonColor = null;
            this.btnOK.Clickable = true;
            this.btnOK.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnOK.Gradient = true;
            this.btnOK.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnOK.IconGap = 0;
            this.btnOK.IconImage = null;
            this.btnOK.IconSize = 12F;
            this.btnOK.IconString = null;
            this.btnOK.Location = new System.Drawing.Point(305, 193);
            this.btnOK.Name = "btnOK";
            this.btnOK.Round = null;
            this.btnOK.ShadowGap = 1;
            this.btnOK.Size = new System.Drawing.Size(94, 30);
            this.btnOK.TabIndex = 6;
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
            this.btnCancel.Gradient = true;
            this.btnCancel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCancel.IconGap = 0;
            this.btnCancel.IconImage = null;
            this.btnCancel.IconSize = 12F;
            this.btnCancel.IconString = null;
            this.btnCancel.Location = new System.Drawing.Point(305, 229);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Round = null;
            this.btnCancel.ShadowGap = 1;
            this.btnCancel.Size = new System.Drawing.Size(94, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnCancel.UseKey = false;
            // 
            // DvColorPickerBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(422, 322);
            this.Controls.Add(this.layout);
            this.Fixed = true;
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "DvColorPickerBox";
            this.Text = "색상 선택";
            this.Title = "색상 선택";
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-palette";
            this.layout.ResumeLayout(false);
            this.dvTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.DvContainer layout;
        private Containers.DvTableLayoutPanel dvTableLayoutPanel1;
        private Controls.DvControl color;
        private Controls.DvControl hue;
        private Controls.DvLabel lbl;
        private Controls.DvValueInputInt inR;
        private Controls.DvValueInputInt inG;
        private Controls.DvValueInputInt inB;
        private Controls.DvButton btnOK;
        private Controls.DvButton btnCancel;
    }
}