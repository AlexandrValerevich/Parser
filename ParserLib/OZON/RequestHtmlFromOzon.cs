using System.Net;
using System.Net.Http;
using HttpFacade;

namespace Parser.Ozon
{
    static class RequestHtmlFromOzon
    {
        static private string _searchUriPrefix => "https://www.ozon.ru/category/knigi-16500/?text=";
    
        static public string GetResponce(string bookName)
        {
            using IHttpRequest httpRequest = CreateHttpRequest(bookName);
            string responceBody = httpRequest.RequestAsString();

            return responceBody;
        }

        static private IHttpRequest CreateHttpRequest(string bookName)
        {
            string uri = _searchUriPrefix + bookName.Replace(" ", "+");
            HttpRequestBuilder httpRequestBulder = HttpRequestBuilder.Create();

            httpRequestBulder
            .AddUri(uri)
            .AddHeaderHost("www.ozon.ru")
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
            .AddHeaderAcceptLanguage("en", 0.9)
            
            .AddHeaderCookies("__Secure-access-token=3.0.H8FjU6GhSwWjdV-tKm7vRQ.29.l8cMBQAAAABhhsSDG4BhrqN3ZWKgAICQoA..20211106200803.QTYJCk-JD1ieFfQwEPhvtxEV4YH9DFPQQCV4umILMXo; __Secure-refresh-token=3.0.H8FjU6GhSwWjdV-tKm7vRQ.29.l8cMBQAAAABhhsSDG4BhrqN3ZWKgAICQoA..20211106200803.jLiLxIQ0tf-6VU_6TNGIiBYWOu2z9U7NtusD6t29XWM; __Secure-ab-group=29; __Secure-user-id=0; xcid=c77bc4e99acfb313007fea5663d5753c; __Secure-ext_xcid=c77bc4e99acfb313007fea5663d5753c; nlbi_1101384=TQRWZ5/ZAyWuNdYlK8plmQAAAAA7a0Z1jMsoCsRUYnooIZjG; visid_incap_1101384=9jYG8Ox1RpaSzONEOZxI3oLEhmEAAAAAQUIPAAAAAACtyzjReL5iQQiTR5UsDaoK; incap_ses_474_1101384=bXVDPnphvQPfONaA+PyTBoPEhmEAAAAAFSzKaku75n5XAItbIRSHkQ==; _gcl_au=1.1.1663206516.1636222104; _gid=GA1.2.212586798.1636222119; cnt_of_orders=0; isBuyer=0; tmr_lvid=e7f9547623ad83a10f2d767a78dfffef; tmr_lvidTS=1636222121738; __exponea_etc__=ed748d0e-9906-47e8-be70-8fa1d3371b6f; __exponea_time2__=-0.4993417263031006; _fbp=fb.1.1636222125206.1194114138; _ym_uid=1636222172306676513; _ym_d=1636222172; _ym_visorc=w; _ym_isad=2; incap_ses_1524_1101384=eCTqZXo2NVpaGEBO9lUmFYLHhmEAAAAAs6fWojoXFsrw4OG2LqM6Tw==; _ga_JNVTMNXQ6F=GS1.1.1636222104.1.1.1636222857.56; _ga=GA1.2.1139218684.1636222114; tmr_detect=0%7C1636222872171; tmr_reqNum=19");
            
            return httpRequestBulder.Build();
        }
    }
}