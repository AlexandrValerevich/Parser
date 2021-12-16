using HttpFacade;

namespace Parser.Labirint
{
    static class RequestHtmlFromLabirint
    {
        private static readonly string s_searchUriPrefix = "https://www.labirint.ru/search/";
        private static readonly string s_searchUriPostfix = "/?stype=0&available=1&wait=1&preorder=1&paperbooks=1";

        public static string GetResponce(string bookName)
        {
            using IHttpRequest httpRequest = CreateHttpRequest(bookName);
            string responceBody = httpRequest.RequestAsString();

            return responceBody;
        }

        private static IHttpRequest CreateHttpRequest(string bookName)
        {
            string uri = UriBuild(bookName);

            IHttpRequestBulder httpRequestBulder = HttpRequestGetBulder.Create();

            httpRequestBulder
            .AddUri(uri)
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

            return httpRequestBulder.Build();
        }

        private static string UriBuild(string bookName) => s_searchUriPrefix + bookName.Replace(" ", "+") + s_searchUriPostfix;
    }
}