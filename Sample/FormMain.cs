using Devinno.Forms;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
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

            tab.TabIcons.Add("tpControl", new DvIcon("fa-cube", 18, DvTextIconAlignment.TopBottom, 5));
            tab.TabIcons.Add("tpContainer", new DvIcon("fa-layer-group", 18, DvTextIconAlignment.TopBottom, 5));
            tab.TabIcons.Add("tpGraph", new DvIcon("fa-chart-line", 18, DvTextIconAlignment.TopBottom, 5));
            tab.TabIcons.Add("tpDialog", new DvIcon("far fa-window-maximize", 18, DvTextIconAlignment.TopBottom, 5));
        }
    }
}
