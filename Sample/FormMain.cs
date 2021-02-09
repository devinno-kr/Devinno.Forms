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
        public FormMain()
        {
            InitializeComponent();

            grp.Series.Add(new GraphSeries2() { Name = "Temp", Alias = "온도", SeriesColor = Color.Crimson, Minimum = -20, Maximum = 200 });
            grp.Series.Add(new GraphSeries2() { Name = "Humidity", Alias = "습도", SeriesColor = Color.DodgerBlue, Minimum = 0, Maximum = 100 });
            grp.Series.Add(new GraphSeries2() { Name = "Velocity", Alias = "속도", SeriesColor = Color.Teal, Minimum = 0, Maximum = 50 });

            var t = 30d;
            var h = 50d;
            var v = 20d;
            var rnd = new Random();

            var ls = new List<Sens>();
            for (DateTime i = new DateTime(2021, 2, 10, 15, 0, 0); i <= new DateTime(2021, 2, 10, 20, 0, 0); i += TimeSpan.FromSeconds(10))
            {
                t = MathTool.Constrain(t + (rnd.Next() % 2 == 0 ? 1 : -1), -20, 200);
                h = MathTool.Constrain(h + (rnd.Next() % 2 == 0 ? 1 : -1), 0, 100);
                v = MathTool.Constrain(v + (rnd.Next() % 2 == 0 ? 0.5 : -0.5), 0, 50);

                ls.Add(new Sens() { Time = i, Temp = t, Humidity = h, Velocity = v });
            }
            //grp.Scrollable = grp.TouchMode= true;
            //grp.PointDraw = true;
            //grp.Gradient = true;
            //grp.ValueDraw = true;
            grp.TouchMode = true;
            grp.SetDataSource<Sens>(ls);
        }
    }

    class Sens : TimeGraphData
    {
        public override DateTime Time { get; set; }

        public double Temp { get; set; }
        public double Humidity { get; set; }
        public double Velocity { get; set; }
    }
}
