using System;
using System.Net;
using System.Net.Http;

using HttpFacade;


namespace Parser.OZ
{
    static class RequestHtmlFromOz
    {
        static private string _searchUriPrefix => "https://oz.by/search/?c=1101523&q=";
        static private string _refererUriPrefix => "https://oz.by/search/?q=";

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
            string uri = _searchUriPrefix + book;       

            IHttpRequestBulder httpRequestBulder = HttpRequestGetBulder.Create();

            httpRequestBulder
            .AddUri(uri)
            .AddHeaderHost("oz.by")
            .AddHeaderConnection("keep-alive")
            .AddHeader("sec-ch-ua", "\"Yandex\";v=\"21\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"")
            .AddHeader("sec-ch-ua-platform", "Windows")
            .AddHeader("Upgrade-Insecure-Requests", "1")
            .AddHeaderUserAgent("Chrome", "93.0.4577.82")
            .AddHeader("Sec-Fetch-Site", "same-origin")
            .AddHeader("Sec-Fetch-Mode", "navigate")
            .AddHeader("Sec-Fetch-Dest", "document")
            .AddHeaderReferer(refererUri)
            .AddHeaderAcceptEncoding("gzip")
            .AddHeaderAcceptEncoding("br")
            .AddHeaderAcceptEncoding("deflate")
            .AddHeaderAcceptLanguage("ru")
            .AddHeaderAcceptLanguage("en", 0.9)
            .AddHeaderCookies("SID_de664c25=6e95631653c94dd4861092b53215bd52; SID_970e2927=1f15f12897712521bd9b57108222de82; cl_today=611; CATALOG_MENU_STATE=0000000000000000000000000000000000000000000000000000100000110001; PHPSESSID=81c3r980j3a4pacd3cu2fpin45; _goods_limit_Desktop=96; search_goods_limit_Desktop=96; search_viewtype_Desktop=grid; screen=a%3A2%3A%7Bs%3A5%3A%22width%22%3Bs%3A4%3A%221536%22%3Bs%3A6%3A%22height%22%3Bs%3A3%3A%22864%22%3B%7D; _fbp=fb.1.1636193405990.456769587; _ym_uid=1636193409256156790; _ym_d=1636193409");

            return httpRequestBulder.Build();
        }
    }
}