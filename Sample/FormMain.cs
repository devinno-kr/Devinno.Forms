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
            grpH.BarSize = grpV.BarSize = 24;
            grpH.GraphBackColor = grpV.GraphBackColor = Color.FromArgb(30, 30, 30);
            grpH.ValueDraw = grpV.ValueDraw = true;
            grpH.Scrollable = grpV.Scrollable = true;
            grpH.TouchMode = grpV.TouchMode = true;
            grpH.GraphMode = grpV.GraphMode = DvBarGraphMode.STACK;
            grpH.Graduation = grpV.Graduation = grpV.GraphMode == DvBarGraphMode.STACK ? 50 : 10;
            
            grpV.Series.Add(new DvGraphSeries() { Name = "Math", Alias = "수학", SeriesColor = Color.FromArgb(192,0,0) });
            grpV.Series.Add(new DvGraphSeries() { Name = "Science", Alias = "과학", SeriesColor = Color.Green });
            grpV.Series.Add(new DvGraphSeries() { Name = "Programming", Alias = "코딩", SeriesColor = Color.DarkBlue });

            grpH.Series.Add(new DvGraphSeries() { Name = "Math", Alias = "수학", SeriesColor = Color.FromArgb(192, 0, 0) });
            grpH.Series.Add(new DvGraphSeries() { Name = "Science", Alias = "과학", SeriesColor = Color.Green });
            grpH.Series.Add(new DvGraphSeries() { Name = "Programming", Alias = "코딩", SeriesColor = Color.DarkBlue });

            //grpH.MouseUp += (o, s) => Gen();
            Gen();
        }

        void Gen()
        {
            var ls = new List<BGraphValue>();
            var rnd = new Random();
            int ngp = 30;
            int A = rnd.Next(50, 100), B = rnd.Next(50, 100), C = rnd.Next(50, 100);
            for (int y = 15; y < 21; y++)
            {
                for (int i = 1; i <= 12; i++)
                {
                    A = (int)MathTool.Constrain(A + rnd.Next(-ngp, ngp), 30, 100);
                    B = (int)MathTool.Constrain(B + rnd.Next(-ngp, ngp), 30, 100);
                    C = (int)MathTool.Constrain(C + rnd.Next(-ngp, ngp), 30, 100);

                    ls.Add(new BGraphValue() { Name = y.ToString("0년") + "\r\n" + i.ToString("0월"), Math = A, Science = B, Programming = C });
                }
            }
            grpV.SetDataSource<BGraphValue>(ls);
            grpH.SetDataSource<BGraphValue>(ls);
        }
    }

    public class BGraphValue : GraphData
    {
        public override string Name { get; set; }
        public double Math { get; set; }
        public double Science { get; set; }
        public double Programming { get; set; }
    }
}
