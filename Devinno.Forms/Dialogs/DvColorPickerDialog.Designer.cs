
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
            this.pnl = new Devinno.Forms.Containers.DvContainer();
            this.tbl = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.lblColor = new Devinno.Forms.Controls.DvLabel();
            this.inR = new Devinno.Forms.Controls.DvValueInput();
            this.inG = new Devinno.Forms.Controls.DvValueInput();
            this.inB = new Devinno.Forms.Controls.DvValueInput();
            this.draw = new Devinno.Forms.Controls.DvControl();
            this.pnl.SuspendLayout();
            this.tbl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.tbl);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(7, 60);
            this.pnl.Name = "pnl";
            this.pnl.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.pnl.Size = new System.Drawing.Size(436, 433);
            this.pnl.TabIndex = 0;
            this.pnl.TabStop = false;
            this.pnl.Text = "dvContainer1";
            this.pnl.UseThemeColor = true;
            // 
            // tbl
            // 
            this.tbl.ColumnCount = 3;
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tbl.Controls.Add(this.btnCancel, 2, 2);
            this.tbl.Controls.Add(this.btnOK, 1, 2);
            this.tbl.Controls.Add(this.lblColor, 0, 2);
            this.tbl.Controls.Add(this.inR, 0, 1);
            this.tbl.Controls.Add(this.inG, 1, 1);
            this.tbl.Controls.Add(this.inB, 2, 1);
            this.tbl.Controls.Add(this.draw, 0, 0);
            this.tbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl.Location = new System.Drawing.Point(3, 10);
            this.tbl.Name = "tbl";
            this.tbl.RowCount = 3;
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tbl.Size = new System.Drawing.Size(430, 420);
            this.tbl.TabIndex = 1;
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
            this.btnCancel.Location = new System.Drawing.Point(289, 372);
            this.btnCancel.LongClickTime = 0;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(138, 45);
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
            this.btnOK.Location = new System.Drawing.Point(146, 372);
            this.btnOK.LongClickTime = 0;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(137, 45);
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
            this.lblColor.Location = new System.Drawing.Point(3, 372);
            this.lblColor.LongClickTime = 0;
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(62, 45);
            this.lblColor.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblColor.TabIndex = 4;
            this.lblColor.TabStop = false;
            this.lblColor.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblColor.Unit = "";
            this.lblColor.UnitWidth = 36;
            this.lblColor.UseLongClick = false;
            this.lblColor.UseThemeColor = false;
            // 
            // inR
            // 
            this.inR.BorderColor = System.Drawing.Color.Red;
            this.inR.ButtonWidth = 60;
            this.inR.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.inR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inR.DrawBorder = false;
            this.inR.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inR.IconGap = 0;
            this.inR.IconImage = null;
            this.inR.IconSize = 10F;
            this.inR.IconString = null;
            this.inR.InputStyle = Devinno.Forms.Controls.DvInputType.NUMBER;
            this.inR.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.inR.ItemHeight = 30;
            this.inR.ItemPadding = new System.Windows.Forms.Padding(0);
            this.inR.Location = new System.Drawing.Point(3, 322);
            this.inR.MaximumViewCount = 10;
            this.inR.MinusInput = false;
            this.inR.Name = "inR";
            this.inR.OffText = "OFF";
            this.inR.OnOff = false;
            this.inR.OnText = "ON";
            this.inR.SelectedIndex = -1;
            this.inR.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.inR.Size = new System.Drawing.Size(137, 44);
            this.inR.TabIndex = 5;
            this.inR.Text = "R";
            this.inR.TextPadding = new System.Windows.Forms.Padding(0);
            this.inR.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.inR.TitleWidth = 60;
            this.inR.TouchMode = false;
            this.inR.Unit = "";
            this.inR.UnitWidth = 36;
            this.inR.UseThemeColor = true;
            this.inR.Value = "";
            this.inR.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // inG
            // 
            this.inG.BorderColor = System.Drawing.Color.Red;
            this.inG.ButtonWidth = 60;
            this.inG.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.inG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inG.DrawBorder = false;
            this.inG.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inG.IconGap = 0;
            this.inG.IconImage = null;
            this.inG.IconSize = 10F;
            this.inG.IconString = null;
            this.inG.InputStyle = Devinno.Forms.Controls.DvInputType.NUMBER;
            this.inG.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.inG.ItemHeight = 30;
            this.inG.ItemPadding = new System.Windows.Forms.Padding(0);
            this.inG.Location = new System.Drawing.Point(146, 322);
            this.inG.MaximumViewCount = 10;
            this.inG.MinusInput = false;
            this.inG.Name = "inG";
            this.inG.OffText = "OFF";
            this.inG.OnOff = false;
            this.inG.OnText = "ON";
            this.inG.SelectedIndex = -1;
            this.inG.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.inG.Size = new System.Drawing.Size(137, 44);
            this.inG.TabIndex = 6;
            this.inG.Text = "G";
            this.inG.TextPadding = new System.Windows.Forms.Padding(0);
            this.inG.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.inG.TitleWidth = 60;
            this.inG.TouchMode = false;
            this.inG.Unit = "";
            this.inG.UnitWidth = 36;
            this.inG.UseThemeColor = true;
            this.inG.Value = "";
            this.inG.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // inB
            // 
            this.inB.BorderColor = System.Drawing.Color.Red;
            this.inB.ButtonWidth = 60;
            this.inB.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.inB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inB.DrawBorder = false;
            this.inB.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inB.IconGap = 0;
            this.inB.IconImage = null;
            this.inB.IconSize = 10F;
            this.inB.IconString = null;
            this.inB.InputStyle = Devinno.Forms.Controls.DvInputType.NUMBER;
            this.inB.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.inB.ItemHeight = 30;
            this.inB.ItemPadding = new System.Windows.Forms.Padding(0);
            this.inB.Location = new System.Drawing.Point(289, 322);
            this.inB.MaximumViewCount = 10;
            this.inB.MinusInput = false;
            this.inB.Name = "inB";
            this.inB.OffText = "OFF";
            this.inB.OnOff = false;
            this.inB.OnText = "ON";
            this.inB.SelectedIndex = -1;
            this.inB.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.inB.Size = new System.Drawing.Size(138, 44);
            this.inB.TabIndex = 7;
            this.inB.Text = "B";
            this.inB.TextPadding = new System.Windows.Forms.Padding(0);
            this.inB.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.inB.TitleWidth = 60;
            this.inB.TouchMode = false;
            this.inB.Unit = "";
            this.inB.UnitWidth = 36;
            this.inB.UseThemeColor = true;
            this.inB.Value = "";
            this.inB.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // draw
            // 
            this.tbl.SetColumnSpan(this.draw, 3);
            this.draw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.draw.Location = new System.Drawing.Point(3, 3);
            this.draw.Name = "draw";
            this.draw.Size = new System.Drawing.Size(424, 313);
            this.draw.TabIndex = 8;
            this.draw.TabStop = false;
            this.draw.Text = "dvControl1";
            this.draw.UseThemeColor = true;
            // 
            // DvColorPickerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 500);
            this.Controls.Add(this.pnl);
            this.Fixed = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DvColorPickerDialog";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(7, 60, 7, 7);
            this.Text = "색상 선택";
            this.Title = "색상 선택";
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-palette";
            this.pnl.ResumeLayout(false);
            this.tbl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Containers.DvContainer pnl;
        private Containers.DvTableLayoutPanel tbl;
        private Controls.DvButton btnCancel;
        private Controls.DvButton btnOK;
        private Controls.DvLabel lblColor;
        private Controls.DvValueInput inR;
        private Controls.DvValueInput inG;
        private Controls.DvValueInput inB;
        private Controls.DvControl draw;
    }
}