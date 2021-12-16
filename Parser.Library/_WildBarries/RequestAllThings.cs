using System.Threading.Tasks;
using HttpFacade;


namespace Parser.WildBarries
{
    static class RequestAllThings
    {
        private static readonly string s_refererUriPrefix = "https://by.wildberries.ru/catalog/0/search.aspx?search=";
        private static readonly string s_requestUriHeadPart = "https://wbxcatalog-sng.wildberries.ru/";
        private static readonly string s_requestUriMiddlePart = "/catalog?";
        private static readonly string s_requestUriTailPart = "&sort=popular";
        private static readonly char s_separator = '&';

        public static string GetResponce(string bookName)
        {
            bookName = bookName.Replace(" ", "+");

            using IHttpRequest httpRequest = CreateHttpRequest(bookName);
            string responceBody = httpRequest.RequestAsString();

            return responceBody;
        }

        private static IHttpRequest CreateHttpRequest(string bookName)
        {
            IHttpRequestBulder httpRequestBulder = HttpRequestGetBulder.Create();

            string requestUri = RequestUri(bookName);
            string refererUri = RefererUri(bookName);

            httpRequestBulder
            .AddUri(requestUri)
            .AddHeaderHost("wbxcatalog-sng.wildberries.ru")
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

        private static string RequestUri(string bookName)
        {
            Task<string> xinfo = Xinfo.ParseXinfoAsync(bookName);
            Task<(string, string)> queryAndSharedKey = QueryAndSharedKey.ParseQueryAndSharedKeyAsync(bookName);

            string xinfoFild = xinfo.Result;
            (string queriFild, string sharedKeyField) = queryAndSharedKey.Result;

            return s_requestUriHeadPart
                + sharedKeyField
                + s_requestUriMiddlePart
                + xinfoFild
                + s_separator
                + queriFild
                + s_requestUriTailPart;
        }

        private static string RefererUri(string bookName) => s_refererUriPrefix + bookName;
    }
}