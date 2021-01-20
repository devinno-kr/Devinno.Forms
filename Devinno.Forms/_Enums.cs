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
        IN_BEVEL = 2, IN_BEVEL_LT = 4, OUT_BEVEL = 8, OUT_BEVEL_RB = 16,
        IN_SHADOW = 32, OUT_SHADOW = 64,
        GRADIENT_V = 128, GRADIENT_V_REVERSE = 256,
        GRADIENT_H = 512, GRADIENT_H_REVERSE = 1024,
        GRADIENT_LT = 2048, GRADIENT_RT = 4096, GRADIENT_LB = 8192, GRADIENT_RB = 16384,
    }
}
