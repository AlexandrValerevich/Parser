using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpFacade
{
    class HttpRequestGet : HttpRequestAbstract
    {
        private HttpClient _httpClient;
        public HttpRequestGet(HttpClient httpClient): base(httpClient) {}
        public void Request()
        {
            
        }
        public void GetResponce()
        {

        }
    }
}