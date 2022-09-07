
namespace Devinno.Forms.Dialogs
{
    partial class DvKeypad
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
            this.container = new Devinno.Forms.Containers.DvContainer();
            this.tpnl = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.container.SuspendLayout();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.Controls.Add(this.tpnl);
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 40);
            this.container.Name = "container";
            this.container.Padding = new System.Windows.Forms.Padding(10);
            this.container.ShadowGap = 1;
            this.container.Size = new System.Drawing.Size(309, 337);
            this.container.TabIndex = 0;
            this.container.TabStop = false;
            this.container.Text = "dvContainer1";
            // 
            // tpnl
            // 
            this.tpnl.ColumnCount = 4;
            this.tpnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpnl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tpnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpnl.Location = new System.Drawing.Point(10, 10);
            this.tpnl.Name = "tpnl";
            this.tpnl.RowCount = 5;
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tpnl.Size = new System.Drawing.Size(289, 317);
            this.tpnl.TabIndex = 0;
            // 
            // DvKeypad
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(309, 377);
            this.Controls.Add(this.container);
            this.Fixed = true;
            this.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DvKeypad";
            this.Text = "키패드";
            this.Title = "키패드";
            this.TitleFont = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-grip-vertical";
            this.container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.DvContainer container;
        private Containers.DvTableLayoutPanel tpnl;
    }
}