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
    public partial class DvColorBox : DvForm
    {
        Timer tmr = new Timer() { Interval = 10 };
        public DvColorBox()
        {
            InitializeComponent();

            tmr.Tick += (o, s) =>
            {
                byte r = 0, g = 0, b = 0;
                if (byte.TryParse(txtR.Text, out r) && byte.TryParse(txtG.Text, out g) && byte.TryParse(txtB.Text, out b))
                {
                    lblColor.LabelColor = Color.FromArgb(r, g, b);
                }
            };
            tmr.Enabled = true;

            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
            btnOK.ButtonClick += (o, s) =>
            {
                byte r = 0, g = 0, b = 0;
                if (byte.TryParse(txtR.Text, out r) && byte.TryParse(txtG.Text, out g) && byte.TryParse(txtB.Text, out b))
                    DialogResult = DialogResult.OK;
            };
        }

        public Color? ShowColorBox(Color? color = null)
        {
            Color? ret = null;

            Theme = GetCallerFormTheme() ?? Theme;

            #region Set
            if (color.HasValue)
            {
                txtR.Text = color.Value.R.ToString();
                txtG.Text = color.Value.G.ToString();
                txtB.Text = color.Value.B.ToString();
            }
            else
            {
                txtR.Text = txtG.Text = txtB.Text = "255";
            }
            #endregion

            if (this.ShowDialog() == DialogResult.OK)
            {
                byte r = 0, g = 0, b = 0;
                if (byte.TryParse(txtR.Text, out r) && byte.TryParse(txtG.Text, out g) && byte.TryParse(txtB.Text, out b))
                    ret = Color.FromArgb(r, g, b);
            }

            return ret;
        }

    }
}
