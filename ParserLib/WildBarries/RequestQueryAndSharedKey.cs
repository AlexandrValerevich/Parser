using System;
using System.Net;
using System.Net.Http;

using HttpFacade;
using Newtonsoft.Json.Linq;

namespace Parser.WildBarries
{
    static class RequestQueryAndSharedKeyFild
    {
        static private string _refererUriPrefix => "https://by.wildberries.ru/catalog/0/search.aspx?search=";
        static private string _searchUriPrefix => "https://wbxsearch-by.wildberries.ru/exactmatch/common?query=";

        static public string GetResponce(string bookName)
        {
            using IHttpRequest httpRequest = CreateHttpRequest(bookName);
            string responceBody = httpRequest.RequestAsString();

            return responceBody;
        }

        static private IHttpRequest CreateHttpRequest(string bookName)
        {
            string book = bookName.Replace(" ", "+");
            string refererUri = _refererUriPrefix + book;
            string searchUri = _searchUriPrefix + book;

            IHttpRequestBulder httpRequestBulder = HttpRequestGetBulder.Create();

            httpRequestBulder
            .AddUri(searchUri)
            .AddHeaderHost("wbxsearch-by.wildberries.ru")
            .AddHeaderConnection("keep-alive")
            .AddHeader("sec-ch-ua", "\"Yandex\";v=\"21\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"")
            .AddHeaderUserAgent("Chrome", "93.0.4577.82")
            .AddHeader("sec-ch-ua-platform", "Windows")
            .AddHeaderOrigin("https://by.wildberries.ru")
            .AddHeader("Sec-Fetch-Site", "same-origin")
            .AddHeader("Sec-Fetch-Mode", "cors")
            .AddHeader("Sec-Fetch-Dest", "empty")
            .AddHeaderReferer(refererUri)
            .AddHeaderAcceptEncoding("gzip")
            .AddHeaderAcceptEncoding("br")
            .AddHeaderAcceptEncoding("deflate")
            .AddHeaderAcceptLanguage("ru")
            .AddHeaderAcceptLanguage("en", 0.9);

            return httpRequestBulder.Build();
        }

    }
}