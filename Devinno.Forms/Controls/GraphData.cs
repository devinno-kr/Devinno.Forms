using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Controls
{
    public enum DvBarGraphMode { STACK, LIST }

    public class DvGraphSeries
    {
        public string Name { get; set; }
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
