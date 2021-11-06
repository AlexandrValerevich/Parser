using System.Net;
using System.Net.Http;
using HttpFacade;

namespace Parser.Labirint
{
    class RequestHtmlFromLabirint
    {
         private string _bookName;
        private string _SearchUri => "https://www.labirint.ru/search/" + _bookName + "/?stype=0&available=1&wait=1&preorder=1&paperbooks=1";
        public RequestHtmlFromLabirint(string bookName)
        {
            _bookName = bookName;
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
            var httpRequestGet = new HttpRequestPost(httpClient);
            IHttpResponce httpResponce = httpRequestGet.Request();

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
            .AddUri(_SearchUri)
            .AddHeaderHost("www.labirint.ru")
            .AddHeaderConnection("keep-alive")
            .AddHeader("sec-ch-ua", "\"Yandex\";v=\"21\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"")
            .AddHeader("sec-ch-ua-platform", "Windows")
            .AddHeader("Upgrade-Insecure-Requests", "1")

            .AddHeaderUserAgent("Chrome", "93.0.4577.82")
            .AddHeader("Sec-Fetch-Site", "none")
            .AddHeader("Sec-Fetch-Mode", "navigate")
            .AddHeader("Sec-Fetch-Dest", "document")

            .AddHeaderAcceptEncoding("gzip")
            .AddHeaderAcceptEncoding("br")
            .AddHeaderAcceptEncoding("deflate")
            .AddHeaderAcceptLanguage("ru")
            .AddHeaderAcceptLanguage("en", 0.9);
            
            return httpClientBulder.Build();
        } 
    }
}