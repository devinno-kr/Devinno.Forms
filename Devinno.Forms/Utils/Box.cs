using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Utils
{
    public class Box
    {
        public static BoxStyle ButtonDown(int ShadowGap) => Box.Style(Fill.Fill, Embossing.Concave, ShadowGap, true);
        public static BoxStyle ButtonUp_Flat(int ShadowGap) => Box.Style(Fill.Fill, Embossing.Convex, ShadowGap, true);
        public static BoxStyle ButtonUp_V(bool Gradient, int ShadowGap) => Gradient ? Box.Style(Fill.GradientV, Embossing.Convex, ShadowGap, true) : ButtonUp_Flat(ShadowGap);
        public static BoxStyle ButtonUp_H(bool Gradient, int ShadowGap) => Gradient ? Box.Style(Fill.GradientH, Embossing.Convex, ShadowGap, true) : ButtonUp_Flat(ShadowGap);
        public static BoxStyle ButtonUp_LT(bool Gradient, int ShadowGap) => Gradient ? Box.Style(Fill.GradientLT, Embossing.Convex, ShadowGap, true) : ButtonUp_Flat(ShadowGap);

        public static BoxStyle FlatBox() => FlatBox(false, false);
        public static BoxStyle FlatBox(bool Border) => FlatBox(Border, false);
        public static BoxStyle FlatBox(bool Border, bool Shadow) => BoxStyle.Fill | (Border ? BoxStyle.Border : BoxStyle.None) | (Shadow ? BoxStyle.OutShadow : BoxStyle.None);

        public static BoxStyle LabelBox(Embossing Style, int ShadowGap) => Box.Style(Fill.Fill, Style, ShadowGap, true);
        public static BoxStyle BackBox(int ShadowGap) => Box.Style(Fill.Fill, Embossing.Concave, ShadowGap, true);
        
        public static BoxStyle ListBox(int ShadowGap) => BoxStyle.Fill | BoxStyle.OutShadow;
        public static BoxStyle Border() => BoxStyle.Border;

        public static BoxStyle Concave(int ShadowGap) => Style(Fill.Fill, Embossing.Concave, ShadowGap, true);
        public static BoxStyle Convex(int ShadowGap) => Style(Fill.Fill, Embossing.Convex, ShadowGap, true);
        public static BoxStyle FlatConcave(int ShadowGap) => Style(Fill.Fill, Embossing.FlatConcave, ShadowGap, true);
        public static BoxStyle FlatConvex(int ShadowGap) => Style(Fill.Fill, Embossing.FlatConvex, ShadowGap, true);
        #region BoxStyle
        #region Style
        public static BoxStyle Style(Fill fill, Embossing volume, int ShadowGap, bool border)
        {
            var ret = BoxStyle.None;
            ret |= EmbossingStyle(volume, ShadowGap);
            ret |= FillStyle(fill);
            if (border) ret |= BoxStyle.Border;
            return ret;
        }
        #endregion
        #region EmbossingStyle
        public static BoxStyle EmbossingStyle(Embossing volume, int ShadowGap)
        {
            var ret = BoxStyle.None;
            switch (volume)
            {
                case Embossing.FlatConcave: ret = (ShadowGap == 0 ? BoxStyle.None : BoxStyle.OutBevel); break;
                case Embossing.FlatConvex: ret = (ShadowGap == 0 ? BoxStyle.None : BoxStyle.OutShadow); break;

                case Embossing.Concave: ret = BoxStyle.InShadow | (ShadowGap == 0 ? BoxStyle.None : BoxStyle.OutBevel); break;
                case Embossing.Convex: ret = BoxStyle.InBevel | (ShadowGap == 0 ? BoxStyle.None : BoxStyle.OutShadow); break;
            }
            return ret;
        }
        #endregion
        #region FillStyle
        public static BoxStyle FillStyle(Fill fill)
        {
            var ret = BoxStyle.None;
            switch (fill)
            {
                case Fill.Fill: ret |= BoxStyle.Fill; break;
                case Fill.GradientV: ret |= BoxStyle.GradientV; break;
                case Fill.GradientVR: ret |= BoxStyle.GradientV_R; break;
                case Fill.GradientH: ret |= BoxStyle.GradientH; break;
                case Fill.GradientHR: ret |= BoxStyle.GradientH_R; break;
                case Fill.GradientLT: ret |= BoxStyle.GradientLT; break;
                case Fill.GradientRB: ret |= BoxStyle.GradientLT_R; break;
                case Fill.GradientRT: ret |= BoxStyle.GradientRT; break;
                case Fill.GradientLB: ret |= BoxStyle.GradientRT_R; break;
            }
            return ret;
        }
        #endregion
        #endregion
    }
}
