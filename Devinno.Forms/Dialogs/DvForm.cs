using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Dialogs
{
    public partial class DvForm : Form
    {
        #region Properties
        #region Theme
        private DvTheme thm = DvTheme.DefaultTheme;
        public DvTheme Theme
        {
            get { return thm; }
            set
            {
                if (thm != value) thm = value;
            }
        }
        #endregion
        #endregion

        public DvForm()
        {
            InitializeComponent();
        }
    }
}
