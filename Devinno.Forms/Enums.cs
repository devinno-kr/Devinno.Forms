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
        GRADIENT = 128, GRADIENT_REVERSE = 256,
    }
}
