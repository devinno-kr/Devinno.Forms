
namespace Devinno.Forms.Dialogs
{
    partial class DvDateTimePickerDialog
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
            this.tbl = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.lblDay = new Devinno.Forms.Controls.DvValueLabel();
            this.calendar = new Devinno.Forms.Controls.DvCalendar();
            this.btnCancel = new Devinno.Forms.Controls.DvButton();
            this.btnOK = new Devinno.Forms.Controls.DvButton();
            this.lblMonth = new Devinno.Forms.Controls.DvValueLabel();
            this.lblYear = new Devinno.Forms.Controls.DvValueLabel();
            this.inSec = new Devinno.Forms.Controls.DvValueInput();
            this.inHour = new Devinno.Forms.Controls.DvValueInput();
            this.inMin = new Devinno.Forms.Controls.DvValueInput();
            this.dvContainer1.SuspendLayout();
            this.tbl.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.tbl);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer1.Location = new System.Drawing.Point(7, 60);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.dvContainer1.Size = new System.Drawing.Size(536, 513);
            this.dvContainer1.TabIndex = 0;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            this.dvContainer1.UseThemeColor = true;
            // 
            // tbl
            // 
            this.tbl.ColumnCount = 3;
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tbl.Controls.Add(this.lblDay, 2, 1);
            this.tbl.Controls.Add(this.calendar, 0, 0);
            this.tbl.Controls.Add(this.btnCancel, 2, 3);
            this.tbl.Controls.Add(this.btnOK, 1, 3);
            this.tbl.Controls.Add(this.lblMonth, 1, 1);
            this.tbl.Controls.Add(this.lblYear, 0, 1);
            this.tbl.Controls.Add(this.inSec, 2, 2);
            this.tbl.Controls.Add(this.inHour, 0, 2);
            this.tbl.Controls.Add(this.inMin, 1, 2);
            this.tbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl.Location = new System.Drawing.Point(3, 10);
            this.tbl.Name = "tbl";
            this.tbl.RowCount = 4;
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tbl.Size = new System.Drawing.Size(530, 500);
            this.tbl.TabIndex = 17;
            // 
            // lblDay
            // 
            this.lblDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDay.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblDay.IconGap = 0;
            this.lblDay.IconImage = null;
            this.lblDay.IconSize = 10F;
            this.lblDay.IconString = null;
            this.lblDay.Location = new System.Drawing.Point(355, 353);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(172, 44);
            this.lblDay.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblDay.TabIndex = 11;
            this.lblDay.TabStop = false;
            this.lblDay.Text = "일";
            this.lblDay.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblDay.TitleWidth = 50;
            this.lblDay.Unit = "";
            this.lblDay.UnitWidth = 36;
            this.lblDay.UseThemeColor = true;
            this.lblDay.Value = null;
            this.lblDay.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // calendar
            // 
            this.tbl.SetColumnSpan(this.calendar, 3);
            this.calendar.DaysBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendar.Location = new System.Drawing.Point(3, 3);
            this.calendar.MonthlyBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.calendar.MultiSelect = false;
            this.calendar.Name = "calendar";
            this.calendar.NoneSelect = false;
            this.calendar.SelectColor = System.Drawing.Color.Cyan;
            this.calendar.Size = new System.Drawing.Size(524, 344);
            this.calendar.TabIndex = 8;
            this.calendar.Text = "dvCalendar1";
            this.calendar.UseThemeColor = true;
            this.calendar.WeeklyBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
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
            this.btnCancel.Location = new System.Drawing.Point(355, 453);
            this.btnCancel.LongClickTime = 0;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(172, 44);
            this.btnCancel.TabIndex = 15;
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
            this.btnOK.Location = new System.Drawing.Point(179, 453);
            this.btnOK.LongClickTime = 0;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(170, 44);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "확인";
            this.btnOK.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnOK.UseLongClick = false;
            this.btnOK.UseThemeColor = true;
            // 
            // lblMonth
            // 
            this.lblMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMonth.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblMonth.IconGap = 0;
            this.lblMonth.IconImage = null;
            this.lblMonth.IconSize = 10F;
            this.lblMonth.IconString = null;
            this.lblMonth.Location = new System.Drawing.Point(179, 353);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(170, 44);
            this.lblMonth.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblMonth.TabIndex = 10;
            this.lblMonth.TabStop = false;
            this.lblMonth.Text = "월";
            this.lblMonth.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblMonth.TitleWidth = 50;
            this.lblMonth.Unit = "";
            this.lblMonth.UnitWidth = 36;
            this.lblMonth.UseThemeColor = true;
            this.lblMonth.Value = null;
            this.lblMonth.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // lblYear
            // 
            this.lblYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYear.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblYear.IconGap = 0;
            this.lblYear.IconImage = null;
            this.lblYear.IconSize = 10F;
            this.lblYear.IconString = null;
            this.lblYear.Location = new System.Drawing.Point(3, 353);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(170, 44);
            this.lblYear.Style = Devinno.Forms.Controls.DvLabelStyle.FlatConvex;
            this.lblYear.TabIndex = 9;
            this.lblYear.TabStop = false;
            this.lblYear.Text = "년";
            this.lblYear.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblYear.TitleWidth = 50;
            this.lblYear.Unit = "";
            this.lblYear.UnitWidth = 36;
            this.lblYear.UseThemeColor = true;
            this.lblYear.Value = null;
            this.lblYear.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // inSec
            // 
            this.inSec.ButtonWidth = 45;
            this.inSec.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.inSec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inSec.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inSec.IconGap = 0;
            this.inSec.IconImage = null;
            this.inSec.IconSize = 10F;
            this.inSec.IconString = null;
            this.inSec.InputStyle = Devinno.Forms.Controls.DvInputType.COMBO;
            this.inSec.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.inSec.ItemHeight = 30;
            this.inSec.ItemPadding = new System.Windows.Forms.Padding(0);
            this.inSec.Location = new System.Drawing.Point(355, 403);
            this.inSec.MaximumViewCount = 10;
            this.inSec.MinusInput = false;
            this.inSec.Name = "inSec";
            this.inSec.OffText = "OFF";
            this.inSec.OnOff = false;
            this.inSec.OnText = "ON";
            this.inSec.SelectedIndex = -1;
            this.inSec.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.inSec.Size = new System.Drawing.Size(172, 44);
            this.inSec.TabIndex = 14;
            this.inSec.Tag = "";
            this.inSec.Text = "초";
            this.inSec.TextPadding = new System.Windows.Forms.Padding(0);
            this.inSec.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.inSec.TitleWidth = 50;
            this.inSec.TouchMode = false;
            this.inSec.Unit = "";
            this.inSec.UnitWidth = 36;
            this.inSec.UseThemeColor = true;
            this.inSec.Value = "";
            this.inSec.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // inHour
            // 
            this.inHour.ButtonWidth = 45;
            this.inHour.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.inHour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inHour.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inHour.IconGap = 0;
            this.inHour.IconImage = null;
            this.inHour.IconSize = 10F;
            this.inHour.IconString = null;
            this.inHour.InputStyle = Devinno.Forms.Controls.DvInputType.COMBO;
            this.inHour.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.inHour.ItemHeight = 30;
            this.inHour.ItemPadding = new System.Windows.Forms.Padding(0);
            this.inHour.Location = new System.Drawing.Point(3, 403);
            this.inHour.MaximumViewCount = 10;
            this.inHour.MinusInput = false;
            this.inHour.Name = "inHour";
            this.inHour.OffText = "OFF";
            this.inHour.OnOff = false;
            this.inHour.OnText = "ON";
            this.inHour.SelectedIndex = -1;
            this.inHour.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.inHour.Size = new System.Drawing.Size(170, 44);
            this.inHour.TabIndex = 12;
            this.inHour.Text = "시";
            this.inHour.TextPadding = new System.Windows.Forms.Padding(0);
            this.inHour.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.inHour.TitleWidth = 50;
            this.inHour.TouchMode = false;
            this.inHour.Unit = "";
            this.inHour.UnitWidth = 36;
            this.inHour.UseThemeColor = true;
            this.inHour.Value = "";
            this.inHour.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // inMin
            // 
            this.inMin.ButtonWidth = 45;
            this.inMin.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.inMin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inMin.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inMin.IconGap = 0;
            this.inMin.IconImage = null;
            this.inMin.IconSize = 10F;
            this.inMin.IconString = null;
            this.inMin.InputStyle = Devinno.Forms.Controls.DvInputType.COMBO;
            this.inMin.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.inMin.ItemHeight = 30;
            this.inMin.ItemPadding = new System.Windows.Forms.Padding(0);
            this.inMin.Location = new System.Drawing.Point(179, 403);
            this.inMin.MaximumViewCount = 10;
            this.inMin.MinusInput = false;
            this.inMin.Name = "inMin";
            this.inMin.OffText = "OFF";
            this.inMin.OnOff = false;
            this.inMin.OnText = "ON";
            this.inMin.SelectedIndex = -1;
            this.inMin.SelectedItemColor = System.Drawing.Color.DarkRed;
            this.inMin.Size = new System.Drawing.Size(170, 44);
            this.inMin.TabIndex = 13;
            this.inMin.Text = "분";
            this.inMin.TextPadding = new System.Windows.Forms.Padding(0);
            this.inMin.TitleBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.inMin.TitleWidth = 50;
            this.inMin.TouchMode = false;
            this.inMin.Unit = "";
            this.inMin.UnitWidth = 36;
            this.inMin.UseThemeColor = true;
            this.inMin.Value = "";
            this.inMin.ValueBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // DvDateTimePickerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 580);
            this.Controls.Add(this.dvContainer1);
            this.Fixed = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DvDateTimePickerDialog";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(7, 60, 7, 7);
            this.Text = "날짜 / 시간 입력";
            this.Title = "날짜 / 시간 입력";
            this.TitleIconSize = 14F;
            this.TitleIconString = "far fa-calendar-alt";
            this.dvContainer1.ResumeLayout(false);
            this.tbl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.DvContainer dvContainer1;
        private Controls.DvButton btnOK;
        private Controls.DvButton btnCancel;
        private Controls.DvValueInput inSec;
        private Controls.DvValueInput inMin;
        private Controls.DvValueInput inHour;
        private Controls.DvValueLabel lblDay;
        private Controls.DvValueLabel lblMonth;
        private Controls.DvValueLabel lblYear;
        private Controls.DvCalendar calendar;
        private Containers.DvTableLayoutPanel tbl;
    }
}