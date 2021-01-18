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
            grpH.ValueDraw = true;
            grpH.GraphMode = DvBarGraphMode.STACK;
            grpH.Series.Add(new DvGraphSeries() { Name = "Math", Alias = "수학", SeriesColor = Color.Red });
            grpH.Series.Add(new DvGraphSeries() { Name = "Science", Alias = "과학", SeriesColor = Color.Green });
            grpH.Series.Add(new DvGraphSeries() { Name = "Programming", Alias = "코딩", SeriesColor = Color.Blue });
            grpH.MouseUp += (o, s) =>
            {
                var ls = new List<BGraphValue>();
                var rnd = new Random();
                int ngp = 30;
                int A = rnd.Next(0, 100), B = rnd.Next(0, 100), C = rnd.Next(0, 100);
                for (int i = 1; i <= 12; i++)
                {
                    A = (int)MathTool.Constrain(A + rnd.Next(-ngp, ngp), 0, 100);
                    B = (int)MathTool.Constrain(B + rnd.Next(-ngp, ngp), 0, 100);
                    C = (int)MathTool.Constrain(C + rnd.Next(-ngp, ngp), 0, 100);

                    ls.Add(new BGraphValue() { Name = i.ToString("0월"), Math = A, Science = B, Programming = C });
                }
                grpH.SetDataSource<BGraphValue>(ls);
            };
        }

        #region class : BGraphValue
        public class BGraphValue : GraphData
        {
            public override string Name { get; set; }
            public double Math { get; set; }
            public double Science { get; set; }
            public double Programming { get; set; }
        }
        #endregion
    }
}
