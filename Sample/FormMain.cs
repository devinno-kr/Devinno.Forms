using Devinno.Forms.Dialogs;
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

            var v = typeof(Devinno.Forms.Icons.IconFAEditor).AssemblyQualifiedName;
        }

    }
}
