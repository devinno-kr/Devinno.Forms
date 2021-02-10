using Devinno.Forms;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class FormMain : DvForm
    {
        Timer tmr = new Timer();
       
        public FormMain()
        {
            InitializeComponent();

            grp.Series.Add(new GraphSeries2() { Name = "Temp", Alias = "온도", SeriesColor = Color.Crimson, Minimum = 0, Maximum = 100 });
            grp.Series.Add(new GraphSeries2() { Name = "Humidity", Alias = "습도", SeriesColor = Color.Teal , Minimum = 0, Maximum = 100 });
            grp.Series.Add(new GraphSeries2() { Name = "Velocity", Alias = "속도", SeriesColor = Color.DodgerBlue, Minimum = 0, Maximum = 100 });

            tmr = new Timer() { Interval = 10 };
            tmr.Tick += (o, s) => { this.Title = "Sample [ " + this.Width + " x " + this.Height + " ]"; };
            tmr.Enabled = true;

            sldTemp.ValueChanged += (o, s) => grp.SetData(new Sens() { Temp = sldTemp.Value, Velocity = sldVelocity.Value, Humidity = sldHumidity.Value });
            sldVelocity.ValueChanged += (o, s) => grp.SetData(new Sens() { Temp = sldTemp.Value, Velocity = sldVelocity.Value, Humidity = sldHumidity.Value });
            sldHumidity.ValueChanged += (o, s) => grp.SetData(new Sens() { Temp = sldTemp.Value, Velocity = sldVelocity.Value, Humidity = sldHumidity.Value });

            grp.XAxisGridDraw = true;
            grp.TouchMode = true;
            grp.Interval = 10;
            grp.MaximumXScale = new TimeSpan(0, 0, 15);
            grp.XScale = new TimeSpan(0, 0, 10);
            grp.XAxisGraduation = new TimeSpan(0, 0, 1);
            grp.Start(new Sens() { Temp = sldTemp.Value, Velocity = sldVelocity.Value, Humidity = sldHumidity.Value });
        }
    }

    class Sens : TimeGraphData
    {
        public override DateTime Time{ get; set; }

        public double Temp { get; set; }
        public double Humidity { get; set; }
        public double Velocity { get; set; }

    }
}
