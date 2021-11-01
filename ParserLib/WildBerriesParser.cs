using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Parser
{
    public class WildBerriesParser : IDisposable
    {
        private HttpClient _httpClient;
        private HttpClientHandler _httpClientHandler;
        private HttpRequestHeaders _httpRequestHeader;
        private string _product;
        private string SearchURL => "https://by.wildberries.ru/catalog/0/search.aspx?search=" + _product;

        public WildBerriesParser(string product)
        {
            _httpClient = default;
            _httpClientHandler = default;
            _httpRequestHeader = default;
            
            _product = product.Replace(" ", "+");
        }

        private string GetXinfoFromWB()
        {
            _httpRequestHeader
            return "";
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
