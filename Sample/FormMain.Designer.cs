
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.blackTheme1 = new Devinno.Forms.Themes.BlackTheme();
            this.dvButton2 = new Devinno.Forms.Controls.DvButton();
            this.dvLabel1 = new Devinno.Forms.Controls.DvLabel();
            this.dvLabel2 = new Devinno.Forms.Controls.DvLabel();
            this.SuspendLayout();
            // 
            // blackTheme1
            // 
            this.blackTheme1.BorderBright = -0.5D;
            this.blackTheme1.Color0 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.blackTheme1.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.blackTheme1.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.blackTheme1.Color3 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.blackTheme1.Color4 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.blackTheme1.Color5 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.blackTheme1.ColumnColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.blackTheme1.Corner = 5;
            this.blackTheme1.DisableAlpha = 180;
            this.blackTheme1.DownBright = -0.25D;
            this.blackTheme1.FrameColor = System.Drawing.Color.Black;
            this.blackTheme1.GradientDarkBright = -0.2D;
            this.blackTheme1.GradientLightBright = 0.2D;
            this.blackTheme1.InBevelBright = 0.4D;
            this.blackTheme1.InShadowBright = -0.3D;
            this.blackTheme1.OutBevelBright = 0.4D;
            this.blackTheme1.OutShadowBright = -0.5D;
            this.blackTheme1.PointColor = System.Drawing.Color.Teal;
            this.blackTheme1.ShadowGap = 1;
            this.blackTheme1.TextOffsetX = 2;
            this.blackTheme1.TextOffsetY = 1;
            this.blackTheme1.TitleBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            // 
            // dvButton2
            // 
            this.dvButton2.BackgroundDraw = true;
            this.dvButton2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.dvButton2.Clickable = true;
            this.dvButton2.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvButton2.Gradient = true;
            this.dvButton2.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.dvButton2.IconImage = null;
            this.dvButton2.IconSize = 18F;
            this.dvButton2.IconString = "fa-layer-group";
            this.dvButton2.Location = new System.Drawing.Point(233, 143);
            this.dvButton2.Name = "dvButton2";
            this.dvButton2.Size = new System.Drawing.Size(128, 108);
            this.dvButton2.TabIndex = 1;
            this.dvButton2.Text = "dvButton2";
            this.dvButton2.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvButton2.UseThemeColor = true;
            // 
            // dvLabel1
            // 
            this.dvLabel1.BackgroundDraw = true;
            this.dvLabel1.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvLabel1.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel1.IconImage = null;
            this.dvLabel1.IconSize = 10F;
            this.dvLabel1.IconString = "far fa-address-card";
            this.dvLabel1.InShadow = true;
            this.dvLabel1.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.dvLabel1.Location = new System.Drawing.Point(502, 173);
            this.dvLabel1.Name = "dvLabel1";
            this.dvLabel1.Size = new System.Drawing.Size(156, 45);
            this.dvLabel1.TabIndex = 2;
            this.dvLabel1.Text = "dvLabel1";
            this.dvLabel1.TextPadding = new System.Windows.Forms.Padding(10);
            this.dvLabel1.UseThemeColor = true;
            // 
            // dvLabel2
            // 
            this.dvLabel2.BackgroundDraw = true;
            this.dvLabel2.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvLabel2.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel2.IconImage = ((System.Drawing.Bitmap)(resources.GetObject("dvLabel2.IconImage")));
            this.dvLabel2.IconSize = 10F;
            this.dvLabel2.IconString = "fa-address-book";
            this.dvLabel2.InShadow = true;
            this.dvLabel2.LabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dvLabel2.Location = new System.Drawing.Point(435, 263);
            this.dvLabel2.Name = "dvLabel2";
            this.dvLabel2.Size = new System.Drawing.Size(156, 88);
            this.dvLabel2.TabIndex = 3;
            this.dvLabel2.Text = "dvLabel2";
            this.dvLabel2.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvLabel2.UseThemeColor = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dvLabel2);
            this.Controls.Add(this.dvLabel1);
            this.Controls.Add(this.dvButton2);
            this.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Theme = this.blackTheme1;
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Themes.BlackTheme blackTheme1;
        private Devinno.Forms.Controls.DvButton dvButton2;
        private Devinno.Forms.Controls.DvLabel dvLabel1;
        private Devinno.Forms.Controls.DvLabel dvLabel2;
    }
}

