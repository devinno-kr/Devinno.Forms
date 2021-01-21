using Devinno.Forms;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class FormMain : DvForm
    {
        public FormMain()
        {
            InitializeComponent();

            dvButton1.MouseUp += (o, s) => dvTablessControl1.SelectedIndex = 0;
            dvButton2.MouseUp += (o, s) => dvTablessControl1.SelectedIndex = 1;
            dvButton3.MouseUp += (o, s) => dvTablessControl1.SelectedIndex = 2;

            dvButton7.MouseUp += (o, s) =>
            {
                dvTablessControl1.Enabled = !dvTablessControl1.Enabled;
                
                //Block = true;
                //MessageBox.Show("TEST");
                //Block = false;
            };

            dvButton8.MouseUp += (o, s) =>
            {

                Block = true;
                MessageBox.Show("TEST");
                Block = false;
            };
        }
    }
}
