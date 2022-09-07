using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Tools
{
    public class ColorTool2
    {
        #region Member Variable
        static Dictionary<Color, List<string>> dic = new Dictionary<Color, List<string>>();
        #endregion

        #region Constructor
        static ColorTool2()
        {
            var vals = typeof(Color).GetFields();
            foreach (var v in vals)
            {
                var color = (Color)v.GetValue(null);
                var name = v.Name;
                if (!dic.ContainsKey(color)) dic.Add(color, new List<string>());
                dic[color].Add(name);
            }
        }
        #endregion

        #region GetName
        public static string GetName(Color c, ColorCodeType code)
        {
            var ret = "";
            if (dic.ContainsKey(c)) ret = dic[c].First();
            else
            {
                if (code == ColorCodeType.ARGB) ret = c.A.ToString() + "," + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString();
                else if (code == ColorCodeType.RGB) ret = c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString();
                else if (code == ColorCodeType.CodeARGB) ret = "#" + c.A.ToString("X2") + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
                else if (code == ColorCodeType.CodeRGB) ret = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
            return ret;
        }
        #endregion
    }
}
