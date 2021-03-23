using Devinno.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinanceDV
{
    public class DaumFinance
    {
        public const string KOSPI = "KOSPI";
        public const string KOSDAQ = "KOSDAQ";

        #region GetCategories
        public static List<FinCategory> GetCategories(HttpClient client, string market = KOSPI)
        {
            List<FinCategory> ret = null;
            var url = string.Format("http://finance.daum.net/api/quotes/sectors?fieldName=&order=&perPage=&market={0}&page=&changes=UPPER_LIMIT%2CRISE%2CEVEN%2CFALL%2CLOWER_LIMIT", market);
            var res = client.GetAsync(url).Result;
            if (res != null && res.StatusCode == HttpStatusCode.OK)
            {
                var content = res.Content.ReadAsStringAsync().Result;
                var rdata = Serialize.JsonDeserialize<PackCategory>(content);
                ret = rdata.data;
            }
            return ret;
        }
        #endregion
        #region CreateClient
        public static HttpClient CreateClient()
        {
            var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            client.DefaultRequestHeaders.Add("Host", "finance.daum.net");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.67 Safari/537.36");
            client.DefaultRequestHeaders.Add("Referer", "http://finance.daum.net/domestic/all_quotes");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            client.DefaultRequestHeaders.Add("Accept-Language", "ko-KR,ko;q=0.9,en-US;q=0.8,en;q=0.7");
            return client;
        }
        #endregion
    }

    #region PackCategory
    public class PackCategory
    {
        public List<FinCategory> data { get; set; }
    }
    #endregion
    #region FinCategory
    public class FinCategory
    {
        public string symbolCode { get; set; }
        public string code { get; set; }
        public string sectorCode { get; set; }
        public string sectorName { get; set; }
        public DateTime date { get; set; }
        public string market { get; set; }
        public string change { get; set; }
        public decimal changePrice { get; set; }
        public decimal changeRate { get; set; }
        public decimal tradePrice { get; set; }
        public decimal prevClosingPrice { get; set; }
        public long accTradeVolume { get; set; }
        public long accTradePrice { get; set; }
        public List<FinStock> includedStocks { get; set; } = new List<FinStock>();
        public double institutionStraightPurchasePrice { get; set; }
        public double foreignStraightPurchasePrice { get; set; }
        public string chartSlideImage { get; set; }
    }
    #endregion
    #region FinStock
    public class FinStock
    {
        public string name { get; set; }
        public string code { get; set; }
        public string symbolCode { get; set; }
        public decimal tradePrice { get; set; }
        public string change { get; set; }
        public decimal changePrice { get; set; }
        public decimal changeRate { get; set; }
        public long accTradeVolume { get; set; }
        public long accTradePrice { get; set; }
        public double marketCap { get; set; }
        public decimal foreignRatio { get; set; }
    }
    #endregion
}
