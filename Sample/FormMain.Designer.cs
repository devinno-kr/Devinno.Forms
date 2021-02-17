
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
            this.tab = new Devinno.Forms.Containers.DvTabControl();
            this.tpControl = new System.Windows.Forms.TabPage();
            this.dvButton3 = new Devinno.Forms.Controls.DvButton();
            this.dvButton2 = new Devinno.Forms.Controls.DvButton();
            this.dvButton1 = new Devinno.Forms.Controls.DvButton();
            this.tpContainer = new System.Windows.Forms.TabPage();
            this.tpGraph = new System.Windows.Forms.TabPage();
            this.tpDialog = new System.Windows.Forms.TabPage();
            this.dvContainer2 = new Devinno.Forms.Containers.DvContainer();
            this.ms = new Devinno.Forms.Menus.DvMenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tab.SuspendLayout();
            this.tpControl.SuspendLayout();
            this.ms.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tab.Controls.Add(this.tpControl);
            this.tab.Controls.Add(this.tpContainer);
            this.tab.Controls.Add(this.tpGraph);
            this.tab.Controls.Add(this.tpDialog);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.ItemSize = new System.Drawing.Size(120, 200);
            this.tab.Location = new System.Drawing.Point(0, 105);
            this.tab.Multiline = true;
            this.tab.Name = "tab";
            this.tab.PointColor = System.Drawing.Color.Red;
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(1273, 820);
            this.tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tab.TabBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.tab.TabIndex = 1;
            this.tab.TextColor = System.Drawing.Color.White;
            this.tab.UseThemeColor = true;
            // 
            // tpControl
            // 
            this.tpControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.tpControl.Controls.Add(this.dvButton3);
            this.tpControl.Controls.Add(this.dvButton2);
            this.tpControl.Controls.Add(this.dvButton1);
            this.tpControl.Location = new System.Drawing.Point(204, 4);
            this.tpControl.Name = "tpControl";
            this.tpControl.Padding = new System.Windows.Forms.Padding(3);
            this.tpControl.Size = new System.Drawing.Size(1065, 812);
            this.tpControl.TabIndex = 0;
            this.tpControl.Text = "Control";
            // 
            // dvButton3
            // 
            this.dvButton3.BackgroundDraw = true;
            this.dvButton3.ButtonColor = System.Drawing.SystemColors.ActiveBorder;
            this.dvButton3.Clickable = true;
            this.dvButton3.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvButton3.ForeColor = System.Drawing.Color.Black;
            this.dvButton3.Gradient = true;
            this.dvButton3.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvButton3.IconGap = 5;
            this.dvButton3.IconImage = null;
            this.dvButton3.IconSize = 12F;
            this.dvButton3.IconString = "fa-cube";
            this.dvButton3.Location = new System.Drawing.Point(20, 131);
            this.dvButton3.LongClickTime = 0;
            this.dvButton3.Name = "dvButton3";
            this.dvButton3.Size = new System.Drawing.Size(225, 50);
            this.dvButton3.TabIndex = 2;
            this.dvButton3.Text = "BUTTON 3";
            this.dvButton3.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvButton3.UseLongClick = false;
            this.dvButton3.UseThemeColor = false;
            // 
            // dvButton2
            // 
            this.dvButton2.BackgroundDraw = true;
            this.dvButton2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.dvButton2.Clickable = true;
            this.dvButton2.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvButton2.Gradient = true;
            this.dvButton2.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvButton2.IconGap = 5;
            this.dvButton2.IconImage = null;
            this.dvButton2.IconSize = 12F;
            this.dvButton2.IconString = "fa-layer-group";
            this.dvButton2.Location = new System.Drawing.Point(20, 75);
            this.dvButton2.LongClickTime = 0;
            this.dvButton2.Name = "dvButton2";
            this.dvButton2.Size = new System.Drawing.Size(225, 50);
            this.dvButton2.TabIndex = 1;
            this.dvButton2.Text = "BUTTON 2";
            this.dvButton2.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvButton2.UseLongClick = false;
            this.dvButton2.UseThemeColor = true;
            // 
            // dvButton1
            // 
            this.dvButton1.BackgroundDraw = true;
            this.dvButton1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.dvButton1.Clickable = true;
            this.dvButton1.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvButton1.Gradient = true;
            this.dvButton1.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvButton1.IconGap = 0;
            this.dvButton1.IconImage = null;
            this.dvButton1.IconSize = 10F;
            this.dvButton1.IconString = null;
            this.dvButton1.Location = new System.Drawing.Point(20, 19);
            this.dvButton1.LongClickTime = 0;
            this.dvButton1.Name = "dvButton1";
            this.dvButton1.Size = new System.Drawing.Size(225, 50);
            this.dvButton1.TabIndex = 0;
            this.dvButton1.Text = "BUTTON 1";
            this.dvButton1.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvButton1.UseLongClick = false;
            this.dvButton1.UseThemeColor = true;
            // 
            // tpContainer
            // 
            this.tpContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.tpContainer.Location = new System.Drawing.Point(204, 4);
            this.tpContainer.Name = "tpContainer";
            this.tpContainer.Padding = new System.Windows.Forms.Padding(3);
            this.tpContainer.Size = new System.Drawing.Size(1065, 812);
            this.tpContainer.TabIndex = 1;
            this.tpContainer.Text = "Container";
            // 
            // tpGraph
            // 
            this.tpGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.tpGraph.Location = new System.Drawing.Point(204, 4);
            this.tpGraph.Name = "tpGraph";
            this.tpGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tpGraph.Size = new System.Drawing.Size(1065, 812);
            this.tpGraph.TabIndex = 2;
            this.tpGraph.Text = "Graph";
            // 
            // tpDialog
            // 
            this.tpDialog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.tpDialog.Location = new System.Drawing.Point(204, 4);
            this.tpDialog.Name = "tpDialog";
            this.tpDialog.Padding = new System.Windows.Forms.Padding(3);
            this.tpDialog.Size = new System.Drawing.Size(1065, 812);
            this.tpDialog.TabIndex = 3;
            this.tpDialog.Text = "Dialog";
            // 
            // dvContainer2
            // 
            this.dvContainer2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dvContainer2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dvContainer2.Location = new System.Drawing.Point(0, 925);
            this.dvContainer2.Name = "dvContainer2";
            this.dvContainer2.Size = new System.Drawing.Size(1273, 50);
            this.dvContainer2.TabIndex = 3;
            this.dvContainer2.TabStop = false;
            this.dvContainer2.Text = "dvContainer2";
            this.dvContainer2.UseThemeColor = true;
            // 
            // ms
            // 
            this.ms.AutoSize = false;
            this.ms.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ms.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiEdit});
            this.ms.Location = new System.Drawing.Point(0, 60);
            this.ms.Name = "ms";
            this.ms.Size = new System.Drawing.Size(1273, 45);
            this.ms.TabIndex = 4;
            this.ms.Text = "dvMenuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewFile,
            this.toolStripSeparator1,
            this.tsmiOpenFile,
            this.tsmiSaveFile,
            this.toolStripSeparator2,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(83, 41);
            this.tsmiFile.Text = "파일(&F)";
            // 
            // tsmiNewFile
            // 
            this.tsmiNewFile.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.tsmiNewFile.Name = "tsmiNewFile";
            this.tsmiNewFile.Size = new System.Drawing.Size(198, 34);
            this.tsmiNewFile.Text = "새 파일(&N)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiOpenFile
            // 
            this.tsmiOpenFile.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.Size = new System.Drawing.Size(198, 34);
            this.tsmiOpenFile.Text = "열기(&O)";
            // 
            // tsmiSaveFile
            // 
            this.tsmiSaveFile.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.tsmiSaveFile.Name = "tsmiSaveFile";
            this.tsmiSaveFile.Size = new System.Drawing.Size(198, 34);
            this.tsmiSaveFile.Text = "저장(&S)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(198, 34);
            this.tsmiExit.Text = "끝내기(&X)";
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(84, 41);
            this.tsmiEdit.Text = "편집(&E)";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(1273, 975);
            this.Controls.Add(this.tab);
            this.Controls.Add(this.dvContainer2);
            this.Controls.Add(this.ms);
            this.MainMenuStrip = this.ms;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 0, 0);
            this.Text = "Sample";
            this.Title = "Sample";
            this.TitleFont = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconColor = System.Drawing.Color.Gainsboro;
            this.TitleIconSize = 14F;
            this.TitleIconString = "fab fa-instalod";
            this.UseThemeColor = false;
            this.tab.ResumeLayout(false);
            this.tpControl.ResumeLayout(false);
            this.ms.ResumeLayout(false);
            this.ms.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Containers.DvTabControl tab;
        private System.Windows.Forms.TabPage tpControl;
        private System.Windows.Forms.TabPage tpContainer;
        private System.Windows.Forms.TabPage tpGraph;
        private System.Windows.Forms.TabPage tpDialog;
        private Devinno.Forms.Containers.DvContainer dvContainer2;
        private Devinno.Forms.Menus.DvMenuStrip ms;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Devinno.Forms.Controls.DvButton dvButton1;
        private Devinno.Forms.Controls.DvButton dvButton3;
        private Devinno.Forms.Controls.DvButton dvButton2;
    }
}

