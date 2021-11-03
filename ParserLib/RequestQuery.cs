using System;
using System.Net;
using System.Net.Http;

using HttpFacade;
using Newtonsoft.Json.Linq;

namespace Parser.WildBarries
{
    class RequestQueryFild
    {
        private string _bookName;
        private string _SearchUri => "https://by.wildberries.ru/catalog/0/search.aspx?search=" + _bookName;

        public RequestQueryFild(string bookName)
        {
            _bookName = bookName.Replace(" ", "+");
        }

        public string GetFormatedQuery()
        {
            IHttpResponce httpResponce = GetResponce();

            string textRecponce = httpResponce.ReadAsString();
            string presets = ParseQueryString(textRecponce);

            

            return presets;
        }

        private IHttpResponce GetResponce()
        {
            using HttpClient httpClient = CreateHttpClient();
            HttpRequestGet httpRequestPost = new HttpRequestGet(httpClient);
            IHttpResponce httpResponce = httpRequestPost.Request();

            return httpResponce;
        }

        private HttpClient CreateHttpClient()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };

            HttpClientBulder httpClientBulder = HttpClientBulder.Create(httpClientHandler);

            httpClientBulder
            .AddUri("https://wbxsearch-by.wildberries.ru/exactmatch/common?query=" + _bookName)
            .AddHeaderHost("wbxsearch-by.wildberries.ru")
            .AddHeaderConnection("keep-alive")
            .AddHeader("sec-ch-ua", "\"Yandex\";v=\"21\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"")
            .AddHeaderUserAgent("Chrome", "93.0.4577.82")
            .AddHeader("sec-ch-ua-platform", "Windows")
            .AddHeaderOrigin("https://by.wildberries.ru")
            .AddHeader("Sec-Fetch-Site", "same-origin")
            .AddHeader("Sec-Fetch-Mode", "cors")
            .AddHeader("Sec-Fetch-Dest", "empty")
            .AddHeaderReferer(_SearchUri)
            .AddHeaderAcceptEncoding("gzip")
            .AddHeaderAcceptEncoding("br")
            .AddHeaderAcceptEncoding("deflate")
            .AddHeaderAcceptLanguage("ru")
            .AddHeaderAcceptLanguage("en", 0.9);

            return httpClientBulder.Build();
        }

        private string ParseQueryString(string textRecponce)
        {
            JObject jObject = JObject.Parse(textRecponce);
            string preset = jObject["query"].ToString();

            return preset;
        }

    }
}