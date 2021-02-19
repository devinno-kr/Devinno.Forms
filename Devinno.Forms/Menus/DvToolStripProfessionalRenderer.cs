using Devinno.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Menus
{
    public class DvToolStripProfessionalRenderer : ToolStripProfessionalRenderer
    {
        public DvToolStripProfessionalRenderer(ProfessionalColorTable c) : base(c)
        {
        }
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            ProfessionalColorTable colorTable = this.ColorTable;
            if (colorTable != null)
            {
                e.ArrowColor = colorTable.MenuItemBorder;
            }
            base.OnRenderArrow(e);
        }
    }
}
