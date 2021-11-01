using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Parser
{
    public class WildBerriesParser : IDisposable
    {
        private HttpClient _httpClient;
        private string _product;
        private string SearchURL => "https://by.wildberries.ru/catalog/0/search.aspx?search=" + _product;
        private HttpRequestHeaders Header => _httpClient.DefaultRequestHeaders;

        public WildBerriesParser(string product)
        {
            _httpClient = default;
            _product = product.Replace(" ", "+");

            InicializeHeader();
        }
        private void InicializeHeader()
        {
            Header.Host = "by.wildberries.ru";
            Header.Referrer = new Uri(SearchURL);
            Header.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
            Header.UserAgent.Add(new ProductInfoHeaderValue("Chrome", "93.0.4577.82"));
            Header.AcceptLanguage.Add(new StringWithQualityHeaderValue("ru, en", 0.9));
            Header.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip, deflate, br"));
        }

        private string GetXinfoFromWB()
        {
            string pathQuery = "https://by.wildberries.ru/user/get-xinfo-v2";

            var postResponce = 

            return "";
        }


        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
