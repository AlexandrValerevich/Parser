using System;
using System.Net;
using System.Net.Http;
using HttpFacade;


namespace Parser.WildBarries
{
    class RequestAllThings
    {
        private string _bookName;
        private string _refererUri => "https://by.wildberries.ru/catalog/0/search.aspx?search=" + _bookName;
        private string _sharedKey;
        private string _xinfoFild;
        private string _queriFild;
        private string _requestUri => "https://wbxcatalog-sng.wildberries.ru/" + _sharedKey + "/catalog?" + _xinfoFild +"&"+ _queriFild +"&sort=popular";

        public RequestAllThings(string bookName, string xinfoFild, string queriFild, string shardKey)
        {
            _bookName = bookName.Replace(" ", "+");
            _xinfoFild = xinfoFild;
            _queriFild = queriFild;
            _sharedKey = shardKey;
        }

        public string GetResponce()
        {
            using IHttpRequest httpRequest = CreateHttpRequest();
            string responceBody = httpRequest.RequestAsString();

            return responceBody;
        }

        private IHttpRequest CreateHttpRequest()
        {
            IHttpRequestBulder httpRequestBulder = HttpRequestGetBulder.Create();

            httpRequestBulder
            .AddUri(_requestUri)
            .AddHeaderHost("wbxcatalog-sng.wildberries.ru")
            .AddHeaderConnection("keep-alive")
            .AddHeader("sec-ch-ua", "\"Yandex\";v=\"21\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"")
            .AddHeaderUserAgent("Chrome", "93.0.4577.82")
            .AddHeader("sec-ch-ua-platform", "Windows")
            .AddHeaderOrigin("https://by.wildberries.ru")
            .AddHeader("Sec-Fetch-Site", "same-origin")
            .AddHeader("Sec-Fetch-Mode", "cors")
            .AddHeader("Sec-Fetch-Dest", "empty")
            .AddHeaderReferer(_refererUri)
            .AddHeaderAcceptEncoding("gzip")
            .AddHeaderAcceptEncoding("br")
            .AddHeaderAcceptEncoding("deflate")
            .AddHeaderAcceptLanguage("ru")
            .AddHeaderAcceptLanguage("en", 0.9);

            return httpRequestBulder.Build();
        }
    }
}