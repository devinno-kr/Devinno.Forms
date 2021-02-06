using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms
{
    public enum DvContentAlignment
    {
        TopLeft, TopCenter, TopRight,
        MiddleLeft, MiddleCenter, MiddleRight,
        BottomLeft, BottomCenter, BottomRight
    }

    public enum DvTextIconAlignment
    {
        LeftRight, TopBottom
    }

    public enum RoundType
    {
        NONE, ALL,
        L, R, T, B,
        LT, LB, RT, RB,
        ELLIPSE,
        FULL_HORIZON
    }

    public enum BoxDrawOption : int
    {
        NONE = 0,
        BORDER = 1,
        IN_BEVEL = 2, IN_BEVEL_LT = 4, IN_BEVEL2 = 8, IN_BEVEL_LT2 = 16,
        OUT_BEVEL = 32, OUT_BEVEL_RB = 64,
        IN_SHADOW = 128, OUT_SHADOW = 256,
        GRADIENT_V = 512, GRADIENT_V_REVERSE = 1024,
        GRADIENT_H = 2048, GRADIENT_H_REVERSE = 4096,
        GRADIENT_LT = 8192, GRADIENT_RT = 16384, GRADIENT_LB = 32768, GRADIENT_RB = 65536,
    }
}
