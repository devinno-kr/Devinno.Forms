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
        public DvKeyboard Keyboard = new DvKeyboard();
        public DvSerialPortSetting dlg = new DvSerialPortSetting();
        public FormMain()
        {
            InitializeComponent();

            btnKeyboard.MouseUp += (o, s) => { var r = dlg.ShowSimpleSerialPortSetting();};
        }
    }
}
