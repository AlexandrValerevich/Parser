using HttpFacade;


namespace Parser.WildBarries
{
    static class RequestAllThings
    {
        static private string _bookName;
        static private string _sharedKey;
        static private string _xinfoFild;
        static private string _queriFild;
        static private string _refererUri => "https://by.wildberries.ru/catalog/0/search.aspx?search=" + _bookName;
        static private string _requestUri => "https://wbxcatalog-sng.wildberries.ru/" + _sharedKey + "/catalog?" + _xinfoFild +"&"+ _queriFild +"&sort=popular";

        static public string GetResponce(string bookName)
        {
            InitializeField(bookName);

            using IHttpRequest httpRequest = CreateHttpRequest();
            string responceBody = httpRequest.RequestAsString();

            return responceBody;
        }

        static private void InitializeField(string bookName)
        {
            _bookName = bookName.Replace(" ", "+");
            _xinfoFild = Xinfo.ParseXinfo(_bookName);
            (_queriFild, _sharedKey) = QueryAndSharedKey.ParseQueryAndSharedKey(bookName);
        }

        static private IHttpRequest CreateHttpRequest()
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