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

            grpC.Series.Add(new GraphSeries() { Name = "Programming", Alias = "프로그래밍", SeriesColor = Color.Red });
            grpC.Series.Add(new GraphSeries() { Name = "Algorithm", Alias = "알고리즘", SeriesColor = Color.Green });
            grpC.Series.Add(new GraphSeries() { Name = "DataSturcture", Alias = "자료구조", SeriesColor = Color.Blue });

            var rnd = new Random();
            var ls = new List<BGraphValue>();
            int ngp = 30;
            int A = rnd.Next(0, 100), B = rnd.Next(0, 100), C = rnd.Next(0, 100);
            
            for (int i = 1; i <= 12; i++)
            {
                A = (int)MathTool.Constrain(A + rnd.Next(-ngp, ngp), 0, 100);
                B = (int)MathTool.Constrain(B + rnd.Next(-ngp, ngp), 0, 100);
                C = (int)MathTool.Constrain(C + rnd.Next(-ngp, ngp), 0, 100);

                ls.Add(new BGraphValue() { Name = i.ToString("0월"), Programming = A, Algorithm = B, DataSturcture = C, Color = Color.FromArgb(rnd.Next(0, 192), rnd.Next(0, 192), rnd.Next(0, 192)) });
            }

            grpC.SetDataSource<BGraphValue>(ls);
        }
    }

    public class BGraphValue : GraphData
    {
        public override string Name { get; set; }
        
        public double Programming { get; set; }
        public double Algorithm { get; set; }
        public double DataSturcture { get; set; }
    }
}
