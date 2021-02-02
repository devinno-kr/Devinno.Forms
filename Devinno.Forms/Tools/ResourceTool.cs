using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Tools
{
    internal class ResourceTool
    {
        internal static Bitmap volumemask { get; private set; }
        internal static Bitmap saturation { get; private set; }
        internal static Bitmap circlegrad { get; private set; }
        static ResourceTool()
        {
            volumemask = new Bitmap(Properties.Resources.tmi);
            saturation = new Bitmap(Properties.Resources.Saturation);
            circlegrad = new Bitmap(Properties.Resources.gb);
        }
    }
}
