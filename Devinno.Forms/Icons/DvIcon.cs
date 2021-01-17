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

        public IconFA IconFA { get; set; } = IconFA._None;
        public StyleFA StyleFA { get; set; } = StyleFA.Solid;
        public float IconFASize { get; set; } = 10;

        public DvTextIconAlignment Alignment { get; set; } = DvTextIconAlignment.LeftRight;
        public int Gap { get; set; } = 0;
        
        public DvIcon() { }
        public DvIcon(IconFA icon, StyleFA style)
        {
            this.IconFA = icon;
            this.StyleFA = style;
        }
    }
}
