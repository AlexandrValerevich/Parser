using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static System.Console;


namespace Parser
{
    public class WildBerriesParser : IParser
    {
        private HttpClient _httpClient;
        private string _product;
        private HttpRequestHeaders Header => _httpClient.DefaultRequestHeaders;
        private string SearchURL => "https://by.wildberries.ru/catalog/0/search.aspx?search=" + _product;

        public WildBerriesParser(string product)
        {
            _httpClient = new HttpClient();
            _product = product.Replace(" ", "+");

            InicializeHeader();
        }


        private void InicializeHeader()
        {
            Header.Host = "by.wildberries.ru";
            Header.Referrer = new Uri(SearchURL);
            Header.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
            Header.UserAgent.Add(new ProductInfoHeaderValue("Chrome", "93.0.4577.82"));
            Header.AcceptLanguage.Add(new StringWithQualityHeaderValue("ru"));
            Header.AcceptLanguage.Add(new StringWithQualityHeaderValue("en", 0.9));
            Header.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            Header.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            Header.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
            Header.Connection.Add("keep-alive");
            Header.Add("X-Requested-With", "XMLHttpRequest");
            Header.Add("Sec-Fetch-Site","same-origin");
            Header.Add("Sec-Fetch-Mode","cors");
            Header.Add("Sec-Fetch-Dect","empty");
            Header.Add("Origin","https://by.wildberries.ru");
            Header.Add("Cookie", "route=8bb719def63907e340640898d77f5b612476fbf1; BasketUID=a6c35f6d-0dce-4255-bb89-feddfadd43ba; ___wbu=8ddb7ad8-782c-4eb9-8900-7aecf5378c6c.1635748716; _gcl_au=1.1.1303374638.1635748718; _wbauid=2167931161635748720; _ga=GA1.2.314026144.1635748721; _gid=GA1.2.2014847738.1635748721; __wbl=cityId%3D1983668%26regionId%3D0%26city%3D%D0%9C%D0%B8%D0%BD%D1%81%D0%BA%26phone%3D80173591900%26latitude%3D53%2C5359%26longitude%3D27%2C34%26src%3D1; __store=119261_122252_122256_121631_122466_122467_122495_122496_122498_122590_122591_122592_123816_123817_123818_123820_123821_123822_124096_124097_124098_121700_117393_117501_507_3158_120762_2737_1699_130744_117986; __region=4_30_70_68_22_66_40_82_1_80_69_48; __pricemargin=1--; __cpns=12_7_3_21; __sppfix=; ncache=119261_122252_122256_121631_122466_122467_122495_122496_122498_122590_122591_122592_123816_123817_123818_123820_123821_123822_124096_124097_124098_121700_117393_117501_507_3158_120762_2737_1699_130744_117986%3B4_30_70_68_22_66_40_82_1_80_69_48%3B1--%3B12_7_3_21%3B; ___wbs=7a2cce68-6abd-4b30-8f97-6480e6ab0979.1635776281; _pk_ses.2.7571=*; _dc_gtm_UA-31472839-1=1; _pk_id.2.7571=734d72735f3307cc.1635748728.4.1635776513.1635776282.");
        }

        public WildBerriesParser UseProxy()
        {
            string addressIP = "127.0.0.1";
            int port = 8888;

            WebProxy proxy = new WebProxy(addressIP, port);
            HttpClient.DefaultProxy = proxy;

            return this;
        }

        public async Task<string> GetXinfoFromWBAsync()
        {
            string result = String.Empty;
            string URI = "https://by.wildberries.ru/user/get-xinfo-v2";
            HttpContent httpConten = new ByteArrayContent(new byte[0]);

            HttpResponseMessage response = await _httpClient.PostAsync(URI, httpConten);
            response.EnsureSuccessStatusCode();
            string responsBoby = await response.Content.ReadAsStringAsync();

            JObject jObject = JObject.Parse(responsBoby);
            result = jObject["xinfo"].ToString();

            return result;
        }

        public async Task<string> GetListProductAsync()
        {
            string result = String.Empty;
            string requestURI = "https://wbxcatalog-sng.wildberries.ru/presets/bucket_100/catalog?" + await GetXinfoFromWBAsync() + "&preset=11091829&";
            Header.Host = "wbxsearch-by.wildberries.ru";

            HttpResponseMessage response = await _httpClient.GetAsync(requestURI);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            WriteLine(responseBody);

            return result;
        }




        


        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
