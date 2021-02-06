using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Icons
{
    public class DvIcon
    {
        public Bitmap IconImage { get; set; } = null;

        public string IconString { get; set; } = null;
        public float IconSize { get; set; } = 10;

        public DvTextIconAlignment Alignment { get; set; } = DvTextIconAlignment.LeftRight;
        public int Gap { get; set; } = 0;

        internal bool Shadow { get; set; }

        public DvIcon() { }
        public DvIcon(string iconString) => this.IconString = iconString;
    }
}
