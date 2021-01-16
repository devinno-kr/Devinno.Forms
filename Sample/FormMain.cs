using Devinno.Forms.Enums;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Tools;
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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            for (int i = 0; i < 9; i++)
            {
                var icon = new DvIcon(IconFA.Cube, StyleFA.Solid) { IconSize = 12, Gap = 0, Alignment = DvTextIconAlignment.LeftRight };

                var sz = e.Graphics.MessureTextIconFA(icon, "Test" + i, Font);
                var rt = new Rectangle(50, 50, 300, 300);
                var rtv = DrawingTool.MakeRectangleAlign(rt, sz, (DvContentAlignment)i);

                e.Graphics.DrawTextIconFA(icon, "Test" + i, Font, Brushes.Black, rt, (DvContentAlignment)i, 0, -2);
                e.Graphics.DrawRectangle(Pens.Red, rtv);
            }
            base.OnPaint(e);
        }
    }
}
