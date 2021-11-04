using System;
using System.Net;
using System.Net.Http;

using HttpFacade;
using Newtonsoft.Json.Linq;

namespace Parser.WildBarries
{
    class RequestBook
    {
        private string _bookName;
        private string _SearchUri => "https://by.wildberries.ru/catalog/0/search.aspx?search=" + _bookName;
        private string _sharedKey;
        private string _xinfoFild;
        private string _queriFild;
        private string _RequestUri => "https://wbxcatalog-sng.wildberries.ru/" + _sharedKey + "/catalog?" + _xinfoFild +"&"+ _queriFild +"&sort=popular";

        public RequestBook(string bookName, string xinfoFild, string queriFild, string shardKey)
        {
            _bookName = bookName.Replace(" ", "+");
            _xinfoFild = xinfoFild;
            _queriFild = queriFild;
            _sharedKey = shardKey;
        }

        public string GetResponceBody()
        {
            IHttpResponce httpResponce = GetResponce();
            string textRecponce = httpResponce.ReadAsString();

            return textRecponce;
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
            .AddUri(_RequestUri)
            .AddHeaderHost("wbxcatalog-sng.wildberries.ru")
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
    }
}