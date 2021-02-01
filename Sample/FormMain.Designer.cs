
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
            this.blackTheme1 = new Devinno.Forms.Themes.BlackTheme();
            this.dvControl1 = new Devinno.Forms.Controls.DvControl();
            this.dvContainer1 = new Devinno.Forms.Containers.DvContainer();
            this.dvGauge1 = new Devinno.Forms.Controls.DvGauge();
            this.dvContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // blackTheme1
            // 
            this.blackTheme1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.blackTheme1.BevelAlpha = 30;
            this.blackTheme1.BorderBright = -0.5D;
            this.blackTheme1.Color0 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.blackTheme1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.blackTheme1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.blackTheme1.Color3 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.blackTheme1.Color4 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.blackTheme1.Color5 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.blackTheme1.Corner = 5;
            this.blackTheme1.DisableAlpha = 180;
            this.blackTheme1.DownBright = -0.25D;
            this.blackTheme1.ForeColor = System.Drawing.Color.White;
            this.blackTheme1.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.blackTheme1.GradientDarkBright = -0.2D;
            this.blackTheme1.GradientLightBright = 0.2D;
            this.blackTheme1.InBevelBright = 0.4D;
            this.blackTheme1.InShadowBright = -0.3D;
            this.blackTheme1.OutBevelBright = 0.15D;
            this.blackTheme1.OutShadowBright = -0.15D;
            this.blackTheme1.PointColor = System.Drawing.Color.DarkRed;
            this.blackTheme1.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.blackTheme1.ScrollCursorColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.blackTheme1.ShadowAlpha = 60;
            this.blackTheme1.ShadowGap = 2;
            this.blackTheme1.TextOffsetX = 0;
            this.blackTheme1.TextOffsetY = 1;
            // 
            // dvControl1
            // 
            this.dvControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dvControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dvControl1.Location = new System.Drawing.Point(5, 50);
            this.dvControl1.Name = "dvControl1";
            this.dvControl1.Size = new System.Drawing.Size(599, 1);
            this.dvControl1.TabIndex = 2;
            this.dvControl1.TabStop = false;
            this.dvControl1.UseThemeColor = true;
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.dvGauge1);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer1.Location = new System.Drawing.Point(5, 51);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(15);
            this.dvContainer1.Size = new System.Drawing.Size(599, 544);
            this.dvContainer1.TabIndex = 3;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            this.dvContainer1.UseThemeColor = true;
            // 
            // dvGauge1
            // 
            this.dvGauge1.EmptyColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dvGauge1.FillColor = System.Drawing.Color.Red;
            this.dvGauge1.GraduationLarge = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.dvGauge1.GraduationSmall = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.dvGauge1.Location = new System.Drawing.Point(70, 54);
            this.dvGauge1.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.dvGauge1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dvGauge1.Name = "dvGauge1";
            this.dvGauge1.RemarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.dvGauge1.Size = new System.Drawing.Size(482, 431);
            this.dvGauge1.StartAngle = 135;
            this.dvGauge1.SweepAngle = 270;
            this.dvGauge1.TabIndex = 0;
            this.dvGauge1.TabStop = false;
            this.dvGauge1.Text = "dvGauge1";
            this.dvGauge1.UseThemeColor = true;
            this.dvGauge1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(609, 600);
            this.Controls.Add(this.dvContainer1);
            this.Controls.Add(this.dvControl1);
            this.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "FormMain";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(5, 50, 5, 5);
            this.Text = "Sample";
            this.Theme = this.blackTheme1;
            this.Title = "Sample";
            this.TitleBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.TitleFont = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconColor = System.Drawing.Color.Gainsboro;
            this.TitleIconSize = 14F;
            this.TitleIconString = "fab fa-instalod";
            this.UseThemeColor = false;
            this.dvContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Themes.BlackTheme blackTheme1;
        private Devinno.Forms.Controls.DvControl dvControl1;
        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Controls.DvGauge dvGauge1;
    }
}

