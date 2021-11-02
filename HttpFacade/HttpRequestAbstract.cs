using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpFacade
{
    abstract class HttpRequestAbstract
    {
        private HttpClient _httpClient;
        public HttpRequestAbstract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public abstract void Request();
        public abstract void GetResponce();
    }
}