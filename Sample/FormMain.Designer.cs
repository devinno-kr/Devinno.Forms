
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
            this.dvContainer1 = new Devinno.Forms.Containers.DvContainer();
            this.grpH = new Devinno.Forms.Controls.DvBarGraphH();
            this.grpV = new Devinno.Forms.Controls.DvBarGraphV();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dvContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // blackTheme1
            // 
            this.blackTheme1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
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
            this.blackTheme1.OutBevelBright = 0.4D;
            this.blackTheme1.OutShadowBright = -0.5D;
            this.blackTheme1.PointColor = System.Drawing.Color.Teal;
            this.blackTheme1.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.blackTheme1.ScrollCursorColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.blackTheme1.ShadowGap = 1;
            this.blackTheme1.TextOffsetX = 2;
            this.blackTheme1.TextOffsetY = 1;
            // 
            // dvContainer1
            // 
            this.dvContainer1.Controls.Add(this.tableLayoutPanel1);
            this.dvContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvContainer1.Location = new System.Drawing.Point(5, 70);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(20);
            this.dvContainer1.Size = new System.Drawing.Size(1087, 725);
            this.dvContainer1.TabIndex = 0;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            this.dvContainer1.UseThemeColor = true;
            // 
            // grpH
            // 
            this.grpH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpH.FormatString = null;
            this.grpH.Graduation = 10D;
            this.grpH.GraphBackColor = System.Drawing.Color.Transparent;
            this.grpH.GraphMode = Devinno.Forms.Controls.DvBarGraphMode.LIST;
            this.grpH.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.grpH.Location = new System.Drawing.Point(526, 3);
            this.grpH.Maximum = 100D;
            this.grpH.Minimum = 0D;
            this.grpH.Name = "grpH";
            this.grpH.Scrollable = false;
            this.grpH.Size = new System.Drawing.Size(518, 679);
            this.grpH.TabIndex = 1;
            this.grpH.TabStop = false;
            this.grpH.Text = "dvBarGraphh1";
            this.grpH.TouchMode = false;
            this.grpH.UseThemeColor = true;
            this.grpH.ValueDraw = false;
            // 
            // grpV
            // 
            this.grpV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpV.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpV.FormatString = null;
            this.grpV.Graduation = 10D;
            this.grpV.GraphBackColor = System.Drawing.Color.Transparent;
            this.grpV.GraphMode = Devinno.Forms.Controls.DvBarGraphMode.LIST;
            this.grpV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.grpV.Location = new System.Drawing.Point(3, 3);
            this.grpV.Maximum = 100D;
            this.grpV.Minimum = 0D;
            this.grpV.Name = "grpV";
            this.grpV.Scrollable = false;
            this.grpV.Size = new System.Drawing.Size(517, 679);
            this.grpV.TabIndex = 0;
            this.grpV.TabStop = false;
            this.grpV.Text = "dvBarGraphv1";
            this.grpV.TouchMode = false;
            this.grpV.UseThemeColor = true;
            this.grpV.ValueDraw = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.grpV, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpH, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1047, 685);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(1097, 800);
            this.Controls.Add(this.dvContainer1);
            this.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(800, 800);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(5, 70, 5, 5);
            this.Text = "Sample";
            this.Theme = this.blackTheme1;
            this.Title = "Sample";
            this.TitleFont = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconColor = System.Drawing.Color.Gainsboro;
            this.TitleIconSize = 14F;
            this.TitleIconString = "fab fa-instalod";
            this.UseThemeColor = false;
            this.dvContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Themes.BlackTheme blackTheme1;
        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Controls.DvBarGraphV grpV;
        private Devinno.Forms.Controls.DvBarGraphH grpH;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

