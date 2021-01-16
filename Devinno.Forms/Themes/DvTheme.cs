using Devinno.Forms.Icons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Themes
{
    public abstract class DvTheme : Component
    {
        #region Properties
        public static DvTheme DefaultTheme { get; set; }

        public abstract string ThemeName { get; }

        public abstract Color Color0 { get; set; }
        public abstract Color Color1 { get; set; }
        public abstract Color Color2 { get; set; }
        public abstract Color Color3 { get; set; }
        public abstract Color Color4 { get; set; }
        public abstract Color Color5 { get; set; }
        public abstract Color PointColor { get; set; }
        public abstract Color FrameColor { get; set; }
        public abstract Color TitleBarColor { get; set; }
        public abstract Color ColumnColor { get; set; }

        public abstract int Corner { get; set; }
        public abstract int TextOffsetX { get; set; }
        public abstract int TextOffsetY { get; set; }
        public abstract int ShadowGap { get; set; }

        public abstract double GradientBrightLight { get; set; }
        public abstract double GradientBrightDark { get; set; }

        public abstract int EnableAlpha { get; set; }
        #endregion

        #region Method
        public abstract void DrawBox(Graphics g, Brush br, Rectangle bounds, RoundType round = RoundType.ALL, BoxDrawOption option = BoxDrawOption.NONE);
        public abstract void DrawBorder(Graphics g, Brush br, Rectangle bounds, RoundType round = RoundType.ALL);

        public abstract void DrawText(Graphics g, DvIcon icon, string Text, Font ft, Brush br, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter);
        public abstract void DrawTextShadow(Graphics g, DvIcon icon, string Text, Font ft, Brush br, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter);
        public abstract void DrawTextBevel(Graphics g, DvIcon icon, string Text, Font ft, Brush br, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter);
        #endregion
    }
}
