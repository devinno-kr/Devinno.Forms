using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Tools
{
    public class IconTool
    {
        public static Icon GetIcon(DvIcon ico, Color color, int width = 24, int height = 24)
        {
            var bmp = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bmp))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                using (var br = new SolidBrush(color))
                {
                    g.DrawIcon(ico, br, new Rectangle(0, 0, width, height), Devinno.Forms.DvContentAlignment.MiddleCenter);
                }
            }

            return Icon.FromHandle(bmp.GetHicon());
        }

        public static void WriteBitmaps(string path, string fa, Color c)
        {
            var szs = new int[] { 16, 24, 32, 48, 64, 128, 256 };

            using (var br = new SolidBrush(c))
            {
                foreach (var sz in szs)
                {
                    var pt = DrawingTool.PixelToPt(sz * 0.9F);
                    using (var bmp = new Bitmap(sz, sz))
                    {
                        using (var g = Graphics.FromImage(bmp))
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                            g.DrawIcon(new DvIcon(fa) { IconSize = pt, RenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit }, br, new RectangleF(0, 0, sz, sz));
                        }
                        bmp.Save(Path.Combine(path, $"_{sz}.png"), System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }
    }
}
