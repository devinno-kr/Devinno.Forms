
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
            this.grp = new Devinno.Forms.Controls.DvTimeGraph();
            this.dvContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvControl1
            // 
            this.dvControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dvControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dvControl1.Location = new System.Drawing.Point(7, 60);
            this.dvControl1.Margin = new System.Windows.Forms.Padding(5);
            this.dvControl1.Name = "dvControl1";
            this.dvControl1.Size = new System.Drawing.Size(749, 1);
            this.dvControl1.TabIndex = 2;
            this.dvControl1.TabStop = false;
            this.dvControl1.UseThemeColor = true;
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.grp);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer1.Location = new System.Drawing.Point(7, 61);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(10);
            this.dvContainer1.Size = new System.Drawing.Size(749, 636);
            this.dvContainer1.TabIndex = 3;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            this.dvContainer1.UseThemeColor = true;
            // 
            // grp
            // 
            this.grp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp.Font = new System.Drawing.Font("나눔고딕", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grp.FormatString = null;
            this.grp.GraphBackColor = System.Drawing.Color.Transparent;
            this.grp.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.grp.Location = new System.Drawing.Point(10, 10);
            this.grp.Name = "grp";
            this.grp.Size = new System.Drawing.Size(729, 616);
            this.grp.TabIndex = 0;
            this.grp.Text = "dvTimeGraph1";
            this.grp.TouchMode = false;
            this.grp.UseThemeColor = true;
            this.grp.XAxisGraduation = System.TimeSpan.Parse("00:10:00");
            this.grp.XScale = System.TimeSpan.Parse("01:00:00");
            this.grp.YAxisGraduationCount = 10;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(763, 704);
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
            this.ResumeLayout(false);

        }

        #endregion
        private Devinno.Forms.Controls.DvControl dvControl1;
        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Controls.DvTimeGraph grp;
    }
}

