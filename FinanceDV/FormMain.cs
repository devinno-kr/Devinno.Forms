using Devinno.Communications.Restful;
using Devinno.Data;
using Devinno.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceDV
{
    public partial class FormMain : DvForm
    {
        HttpClient client;

        public FormMain()
        {
            InitializeComponent();

            client = DaumFinance.CreateClient();

            var result = DaumFinance.GetCategories(client, "KOSDAQ");
        }
    }

   
}
