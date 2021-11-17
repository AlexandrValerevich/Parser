using System;
using System.Net.Http;

namespace HttpFacade
{
    public class HttpRequestGetBulder : HttpRequestBulderAbstract
    {
        public static HttpRequestGetBulder Create()
        {
            return new HttpRequestGetBulder();
        }

        private HttpRequestGetBulder() {}

        public override IHttpRequest Build()
        {
            HttpClient httpClient = _httpClientBulder.Build();
            return new HttpRequestGet(httpClient);
        }

    }
}