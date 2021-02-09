using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms
{
    public enum BarGraphMode { STACK, LIST }

    public class GraphSeries
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public Color SeriesColor { get; set; }
    }

    public abstract class GraphData
    {
        public abstract string Name { get; set; }
        public Color Color { get; set; }
    }

    class GV
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Dictionary<string, double> Values
        {
            get
            {
                var ret = new Dictionary<string, double>();
                foreach (var vk in Props.Keys) ret.Add(vk, Convert.ToDouble(Props[vk].GetValue(Data)));
                return ret;
            }
        }

        internal Dictionary<string, PropertyInfo> Props { get; set; }
        internal GraphData Data { get; set; }
    }

    public class GraphSeries2 : GraphSeries
    {
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public bool Visible { get; set; } = true;
    }

    public abstract class TimeGraphData
    {
        public abstract DateTime Time { get; set; }
    }

    class TGV
    {
        public DateTime Time { get; set; }
        public Dictionary<string, double> Values { get; } = new Dictionary<string, double>();
    }

}
