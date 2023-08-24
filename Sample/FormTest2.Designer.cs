
namespace Sample
{
    partial class FormTest2
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
            this.dvStepGauge1 = new Devinno.Forms.Controls.DvStepGauge();
            this.SuspendLayout();
            // 
            // dvStepGauge1
            // 
            this.dvStepGauge1.ButtonColor = null;
            this.dvStepGauge1.ButtonStyle = Devinno.Forms.DvStepButtonStyle.LeftRight;
            this.dvStepGauge1.Gap = 7;
            this.dvStepGauge1.Location = new System.Drawing.Point(274, 185);
            this.dvStepGauge1.Name = "dvStepGauge1";
            this.dvStepGauge1.OffColor = null;
            this.dvStepGauge1.OnColor = null;
            this.dvStepGauge1.ShadowGap = 1;
            this.dvStepGauge1.Size = new System.Drawing.Size(269, 35);
            this.dvStepGauge1.Step = 0;
            this.dvStepGauge1.StepCount = 7;
            this.dvStepGauge1.TabIndex = 0;
            this.dvStepGauge1.Text = "dvStepGauge1";
            this.dvStepGauge1.UseButton = true;
            // 
            // FormTest2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dvStepGauge1);
            this.Name = "FormTest2";
            this.Text = "FormTest2";
            this.Title = "FormTest2";
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Controls.DvStepGauge dvStepGauge1;
    }
}