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
        Timer tmr;
        double n = 0;
        public FormMain()
        {
            InitializeComponent();

            tmr = new Timer();
            tmr.Interval = 10;
            tmr.Tick += (o, s) =>
            {
                n += 0.1;
                dvGauge1.Value = dvMeter1.Value = Math.Abs(n % 200 - 100);
            };
            tmr.Enabled = true;
        }
    }
}
