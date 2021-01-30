
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
            this.dvCalendar1 = new Devinno.Forms.Controls.DvCalendar();
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
            // dvCalendar1
            // 
            this.dvCalendar1.DaysBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dvCalendar1.Location = new System.Drawing.Point(81, 124);
            this.dvCalendar1.MonthlyBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dvCalendar1.MultiSelect = false;
            this.dvCalendar1.Name = "dvCalendar1";
            this.dvCalendar1.NoneSelect = false;
            this.dvCalendar1.SelectColor = System.Drawing.Color.Cyan;
            this.dvCalendar1.Size = new System.Drawing.Size(447, 363);
            this.dvCalendar1.TabIndex = 0;
            this.dvCalendar1.TabStop = false;
            this.dvCalendar1.Text = "dvCalendar1";
            this.dvCalendar1.UseThemeColor = true;
            this.dvCalendar1.WeeklyBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(609, 600);
            this.Controls.Add(this.dvCalendar1);
            this.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.FrameColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "FormMain";
            this.NoFrame = true;
            this.Padding = new System.Windows.Forms.Padding(10, 50, 10, 10);
            this.Text = "Sample";
            this.Theme = this.blackTheme1;
            this.Title = "Sample";
            this.TitleBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.TitleFont = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconColor = System.Drawing.Color.Gainsboro;
            this.TitleIconSize = 14F;
            this.TitleIconString = "fab fa-instalod";
            this.UseThemeColor = false;
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Themes.BlackTheme blackTheme1;
        private Devinno.Forms.Controls.DvCalendar dvCalendar1;
    }
}

