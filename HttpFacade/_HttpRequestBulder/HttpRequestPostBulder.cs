using System;
using System.Net.Http;

namespace HttpFacade
{
    public class HttpRequestPostBulder : HttpRequestBulderAbstract
    {
        public static HttpRequestPostBulder Create()
        {
            return new HttpRequestPostBulder();
        }

        private HttpRequestPostBulder() {}

        public override IHttpRequest Build()
        {
            HttpClient httpClient = _httpClientBulder.Build();
            return new HttpRequestPost(httpClient);
        }
    }
}