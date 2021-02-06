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
        DvColorPickerDialog dv = new DvColorPickerDialog();
        public FormMain()
        {
            InitializeComponent();
            
            ooR.OnOffChanged += (o, s) => lmpR.OnOff = ooR.OnOff;
            ooG.OnOffChanged += (o, s) => lmpG.OnOff = ooG.OnOff;
            ooB.OnOffChanged += (o, s) => lmpB.OnOff = ooB.OnOff;

            dvButton1.ButtonClick += (o, s) => dv.ShowColorPicker(Color.DarkSlateGray);
        }
    }
}
