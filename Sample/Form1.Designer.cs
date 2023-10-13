
namespace Sample
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ivCanvas1 = new Devinno.Forms.ImageCanvas.IvCanvas();
            this.ivPage1 = new Devinno.Forms.ImageCanvas.IvPage();
            this.ivButton3 = new Devinno.Forms.ImageCanvas.IvButton();
            this.ivButton2 = new Devinno.Forms.ImageCanvas.IvButton();
            this.ivButton1 = new Devinno.Forms.ImageCanvas.IvButton();
            this.ivPage2 = new Devinno.Forms.ImageCanvas.IvPage();
            this.ivPage3 = new Devinno.Forms.ImageCanvas.IvPage();
            this.ivCanvas1.SuspendLayout();
            this.ivPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ivCanvas1
            // 
            this.ivCanvas1.Controls.Add(this.ivPage1);
            this.ivCanvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ivCanvas1.Location = new System.Drawing.Point(0, 0);
            this.ivCanvas1.Name = "ivCanvas1";
            this.ivCanvas1.Pages = new Devinno.Forms.ImageCanvas.IvPage[] {
        this.ivPage1,
        this.ivPage2,
        this.ivPage3};
            this.ivCanvas1.SelectPage = this.ivPage1;
            this.ivCanvas1.ShadowGap = 1;
            this.ivCanvas1.Size = new System.Drawing.Size(1024, 768);
            this.ivCanvas1.TabIndex = 0;
            this.ivCanvas1.TabStop = false;
            this.ivCanvas1.Text = "ivCanvas1";
            // 
            // ivPage1
            // 
            this.ivPage1.Controls.Add(this.ivButton3);
            this.ivPage1.Controls.Add(this.ivButton2);
            this.ivPage1.Controls.Add(this.ivButton1);
            this.ivPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ivPage1.Location = new System.Drawing.Point(0, 0);
            this.ivPage1.Name = "ivPage1";
            this.ivPage1.OffImage = global::Sample.Properties.Resources._2_off_;
            this.ivPage1.OnImage = global::Sample.Properties.Resources._2_on_;
            this.ivPage1.OnOff = false;
            this.ivPage1.ShadowGap = 1;
            this.ivPage1.Size = new System.Drawing.Size(1024, 768);
            this.ivPage1.TabIndex = 0;
            this.ivPage1.TabStop = false;
            this.ivPage1.Text = "ivPage1";
            // 
            // ivButton3
            // 
            this.ivButton3.Location = new System.Drawing.Point(3, 278);
            this.ivButton3.Name = "ivButton3";
            this.ivButton3.Size = new System.Drawing.Size(143, 97);
            this.ivButton3.TabIndex = 2;
            this.ivButton3.Text = "x";
            this.ivButton3.Click += new System.EventHandler(this.ivButton3_Click);
            // 
            // ivButton2
            // 
            this.ivButton2.Location = new System.Drawing.Point(3, 178);
            this.ivButton2.Name = "ivButton2";
            this.ivButton2.Size = new System.Drawing.Size(143, 96);
            this.ivButton2.TabIndex = 1;
            this.ivButton2.Text = "x";
            // 
            // ivButton1
            // 
            this.ivButton1.Location = new System.Drawing.Point(3, 80);
            this.ivButton1.Name = "ivButton1";
            this.ivButton1.Size = new System.Drawing.Size(143, 99);
            this.ivButton1.TabIndex = 0;
            this.ivButton1.Text = "x";
            // 
            // ivPage2
            // 
            this.ivPage2.Location = new System.Drawing.Point(0, 0);
            this.ivPage2.Name = "ivPage2";
            this.ivPage2.OffImage = null;
            this.ivPage2.OnImage = null;
            this.ivPage2.OnOff = false;
            this.ivPage2.ShadowGap = 1;
            this.ivPage2.Size = new System.Drawing.Size(0, 0);
            this.ivPage2.TabIndex = 0;
            this.ivPage2.TabStop = false;
            this.ivPage2.Text = "ivPage2";
            // 
            // ivPage3
            // 
            this.ivPage3.Location = new System.Drawing.Point(0, 0);
            this.ivPage3.Name = "ivPage3";
            this.ivPage3.OffImage = null;
            this.ivPage3.OnImage = null;
            this.ivPage3.OnOff = false;
            this.ivPage3.ShadowGap = 1;
            this.ivPage3.Size = new System.Drawing.Size(0, 0);
            this.ivPage3.TabIndex = 0;
            this.ivPage3.TabStop = false;
            this.ivPage3.Text = "ivPage3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BlankForm = true;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.ivCanvas1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Text = "Form1";
            this.Title = "Form1";
            this.TitleHeight = 0;
            this.ivCanvas1.ResumeLayout(false);
            this.ivPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.ImageCanvas.IvCanvas ivCanvas1;
        private Devinno.Forms.ImageCanvas.IvPage ivPage1;
        private Devinno.Forms.ImageCanvas.IvPage ivPage2;
        private Devinno.Forms.ImageCanvas.IvPage ivPage3;
        private Devinno.Forms.ImageCanvas.IvButton ivButton3;
        private Devinno.Forms.ImageCanvas.IvButton ivButton2;
        private Devinno.Forms.ImageCanvas.IvButton ivButton1;
    }
}