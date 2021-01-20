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
        DateTime n1, n2, n3;
        public FormMain()
        {
            InitializeComponent();

            dvButton1.UseLongClick= true;
            dvButton1.LongClickTime = 1000;
            dvButton1.MouseDown += (o, s) => n1 = DateTime.Now;
            dvButton1.LongClick += (o, s) => this.BeginInvoke(new Action(() => MessageBox.Show((DateTime.Now - n1).TotalMilliseconds.ToString())));

            dvLabel1.UseLongClick = true;
            dvLabel1.LongClickTime = 1000;
            dvLabel1.MouseDown += (o, s) => n2 = DateTime.Now;
            dvLabel1.LongClick += (o, s) => this.BeginInvoke(new Action(() => MessageBox.Show((DateTime.Now - n2).TotalMilliseconds.ToString())));

            dvCircleButton1.UseLongClick = true;
            dvCircleButton1.LongClickTime = 1000;
            dvCircleButton1.MouseDown += (o, s) => n3 = DateTime.Now;
            dvCircleButton1.LongClick += (o, s) => this.BeginInvoke(new Action(() => MessageBox.Show((DateTime.Now - n3).TotalMilliseconds.ToString())));

        }
    }
}
