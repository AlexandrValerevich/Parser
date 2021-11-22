using System.Threading.Tasks;
using HttpFacade;

namespace Parser.WildBarries
{
    static class RequestQueryAndSharedKeyFild
    {
        private static readonly string s_refererUriPrefix = "https://by.wildberries.ru/catalog/0/search.aspx?search=";
        private static readonly string s_searchUriPrefix = "https://wbxsearch-by.wildberries.ru/exactmatch/common?query=";

        public static async Task<string> GetResponceAsync(string bookName) => await Task.Run(() => GetResponce(bookName));

        public static string GetResponce(string bookName)
        {
            using IHttpRequest httpRequest = CreateHttpRequest(bookName);
            string responceBody = httpRequest.RequestAsString();

            return responceBody;
        }

        private static IHttpRequest CreateHttpRequest(string bookName)
        {
            string book = bookName.Replace(" ", "+");
            string refererUri = s_refererUriPrefix + book;
            string searchUri = s_searchUriPrefix + book;

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