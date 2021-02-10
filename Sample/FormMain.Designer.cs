
namespace Sample
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dvControl1 = new Devinno.Forms.Controls.DvControl();
            this.dvContainer1 = new Devinno.Forms.Containers.DvContainer();
            this.dvTableLayoutPanel1 = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.grp = new Devinno.Forms.Controls.DvTrendGraph();
            this.sldTemp = new Devinno.Forms.Controls.DvSliderV();
            this.sldHumidity = new Devinno.Forms.Controls.DvSliderV();
            this.sldVelocity = new Devinno.Forms.Controls.DvSliderV();
            this.dvContainer1.SuspendLayout();
            this.dvTableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvControl1
            // 
            this.dvControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dvControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dvControl1.Location = new System.Drawing.Point(7, 60);
            this.dvControl1.Margin = new System.Windows.Forms.Padding(5);
            this.dvControl1.Name = "dvControl1";
            this.dvControl1.Size = new System.Drawing.Size(1227, 1);
            this.dvControl1.TabIndex = 2;
            this.dvControl1.TabStop = false;
            this.dvControl1.UseThemeColor = true;
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.dvTableLayoutPanel1);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer1.Location = new System.Drawing.Point(7, 61);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(10);
            this.dvContainer1.Size = new System.Drawing.Size(1227, 636);
            this.dvContainer1.TabIndex = 3;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            this.dvContainer1.UseThemeColor = true;
            // 
            // dvTableLayoutPanel1
            // 
            this.dvTableLayoutPanel1.ColumnCount = 4;
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.dvTableLayoutPanel1.Controls.Add(this.grp, 0, 0);
            this.dvTableLayoutPanel1.Controls.Add(this.sldTemp, 1, 0);
            this.dvTableLayoutPanel1.Controls.Add(this.sldHumidity, 2, 0);
            this.dvTableLayoutPanel1.Controls.Add(this.sldVelocity, 3, 0);
            this.dvTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvTableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.dvTableLayoutPanel1.Name = "dvTableLayoutPanel1";
            this.dvTableLayoutPanel1.RowCount = 1;
            this.dvTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dvTableLayoutPanel1.Size = new System.Drawing.Size(1207, 616);
            this.dvTableLayoutPanel1.TabIndex = 1;
            // 
            // grp
            // 
            this.grp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp.Font = new System.Drawing.Font("나눔고딕", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grp.GraphBackColor = System.Drawing.Color.Transparent;
            this.grp.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.grp.Interval = 10;
            this.grp.Location = new System.Drawing.Point(3, 3);
            this.grp.MaximumXScale = System.TimeSpan.Parse("1.00:00:00");
            this.grp.Name = "grp";
            this.grp.Size = new System.Drawing.Size(991, 610);
            this.grp.TabIndex = 0;
            this.grp.Text = "dvTrendGraph1";
            this.grp.TimeFormatString = null;
            this.grp.TouchMode = false;
            this.grp.UseThemeColor = true;
            this.grp.ValueFormatString = null;
            this.grp.XAxisGraduation = System.TimeSpan.Parse("00:00:10");
            this.grp.XAxisGridDraw = false;
            this.grp.XScale = System.TimeSpan.Parse("00:01:00");
            this.grp.YAxisGraduationCount = 10;
            this.grp.YAxisGridDraw = true;
            // 
            // sldTemp
            // 
            this.sldTemp.BarColor = System.Drawing.Color.Crimson;
            this.sldTemp.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.sldTemp.CursorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.sldTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sldTemp.DrawText = false;
            this.sldTemp.FormatString = null;
            this.sldTemp.Location = new System.Drawing.Point(1000, 3);
            this.sldTemp.Maximum = 100D;
            this.sldTemp.Minimum = 0D;
            this.sldTemp.Name = "sldTemp";
            this.sldTemp.Reverse = false;
            this.sldTemp.Size = new System.Drawing.Size(64, 610);
            this.sldTemp.TabIndex = 1;
            this.sldTemp.Text = "dvSliderv1";
            this.sldTemp.Tick = 0D;
            this.sldTemp.UseThemeColor = false;
            this.sldTemp.Value = 0D;
            // 
            // sldHumidity
            // 
            this.sldHumidity.BarColor = System.Drawing.Color.Teal;
            this.sldHumidity.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.sldHumidity.CursorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.sldHumidity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sldHumidity.DrawText = false;
            this.sldHumidity.FormatString = null;
            this.sldHumidity.Location = new System.Drawing.Point(1070, 3);
            this.sldHumidity.Maximum = 100D;
            this.sldHumidity.Minimum = 0D;
            this.sldHumidity.Name = "sldHumidity";
            this.sldHumidity.Reverse = false;
            this.sldHumidity.Size = new System.Drawing.Size(64, 610);
            this.sldHumidity.TabIndex = 2;
            this.sldHumidity.Text = "dvSliderv2";
            this.sldHumidity.Tick = 0D;
            this.sldHumidity.UseThemeColor = false;
            this.sldHumidity.Value = 0D;
            // 
            // sldVelocity
            // 
            this.sldVelocity.BarColor = System.Drawing.Color.DodgerBlue;
            this.sldVelocity.BoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.sldVelocity.CursorColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.sldVelocity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sldVelocity.DrawText = false;
            this.sldVelocity.FormatString = null;
            this.sldVelocity.Location = new System.Drawing.Point(1140, 3);
            this.sldVelocity.Maximum = 100D;
            this.sldVelocity.Minimum = 0D;
            this.sldVelocity.Name = "sldVelocity";
            this.sldVelocity.Reverse = false;
            this.sldVelocity.Size = new System.Drawing.Size(64, 610);
            this.sldVelocity.TabIndex = 3;
            this.sldVelocity.Text = "dvSliderv3";
            this.sldVelocity.Tick = 0D;
            this.sldVelocity.UseThemeColor = false;
            this.sldVelocity.Value = 0D;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(1241, 704);
            this.Controls.Add(this.dvContainer1);
            this.Controls.Add(this.dvControl1);
            this.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "FormMain";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(7, 60, 7, 7);
            this.Text = "Sample";
            this.Title = "Sample";
            this.TitleFont = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconColor = System.Drawing.Color.Gainsboro;
            this.TitleIconSize = 14F;
            this.TitleIconString = "fab fa-instalod";
            this.UseThemeColor = false;
            this.dvContainer1.ResumeLayout(false);
            this.dvTableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Devinno.Forms.Controls.DvControl dvControl1;
        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Controls.DvTrendGraph grp;
        private Devinno.Forms.Containers.DvTableLayoutPanel dvTableLayoutPanel1;
        private Devinno.Forms.Controls.DvSliderV sldTemp;
        private Devinno.Forms.Controls.DvSliderV sldHumidity;
        private Devinno.Forms.Controls.DvSliderV sldVelocity;
    }
}

