
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
            this.btnKeyboard = new Devinno.Forms.Controls.DvButton();
            this.SuspendLayout();
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.BackgroundDraw = true;
            this.btnKeyboard.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnKeyboard.Clickable = true;
            this.btnKeyboard.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnKeyboard.Gradient = true;
            this.btnKeyboard.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnKeyboard.IconGap = 3;
            this.btnKeyboard.IconImage = null;
            this.btnKeyboard.IconSize = 12F;
            this.btnKeyboard.IconString = "fa-keyboard";
            this.btnKeyboard.Location = new System.Drawing.Point(29, 93);
            this.btnKeyboard.LongClickTime = 0;
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(225, 45);
            this.btnKeyboard.TabIndex = 0;
            this.btnKeyboard.Text = "키보드";
            this.btnKeyboard.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnKeyboard.UseLongClick = false;
            this.btnKeyboard.UseThemeColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(737, 690);
            this.Controls.Add(this.btnKeyboard);
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
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Controls.DvButton btnKeyboard;
    }
}

