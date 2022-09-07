using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Extensions
{
    public static class ControlCollectionExtension
    {
        public static void Add(this TableLayoutControlCollection v, Control c, int column, int row, int columnSpan, int rowSpan)
        {
            v.Add(c, column, row);
            v.Container.SetColumnSpan(c, columnSpan);
            v.Container.SetRowSpan(c, rowSpan);
        }
    }
}
