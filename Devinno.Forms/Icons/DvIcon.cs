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
        public DvIcon(Bitmap ico) => this.IconImage = ico;
        public DvIcon(string iconString) => this.IconString = iconString;
        public DvIcon(string iconString, int Size) : this(iconString) => this.IconSize = Size;
        public DvIcon(string iconString, int Size, int gap) :this(iconString, Size) => this.Gap = gap;
        public DvIcon(string iconString, int Size, DvTextIconAlignment align, int gap) : this(iconString, Size, gap) => this.Alignment = align;
    }
}
