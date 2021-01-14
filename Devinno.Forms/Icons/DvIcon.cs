using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devinno.Forms.Enums;

namespace Devinno.Forms.Icons
{
    public class DvIcon
    {
        public IconFA IconFA { get; set; }
        public StyleFA StyleFA { get; set; }
        public DvTextIconAlignment Alignment { get; set; } = DvTextIconAlignment.LeftRight;
        public int IconOffsetX { get; set; }
        public int IconOffsetY { get; set; }
        public int Gap { get; set; } = 0;
        public float IconSize { get; set; } = 8;

        public DvIcon() { }
        public DvIcon(IconFA icon, StyleFA style)
        {
            this.IconFA = icon;
            this.StyleFA = style;
        }
    }
}
