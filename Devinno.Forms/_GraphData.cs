using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        public bool Visible { get; set; } = true;
    }

    public abstract class GraphData
    {
        public abstract string Name { get; set; }
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

    class GV
    {
        public string Name { get; set; }
        public Dictionary<string, double> Values { get; } = new Dictionary<string, double>();
    }
}
