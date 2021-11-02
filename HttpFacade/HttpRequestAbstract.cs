using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpFacade
{
    abstract class HttpRequestAbstract
    {
        private HttpClient _httpClient;
        private HttpResponce _httpResponce;
        public HttpRequestAbstract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public abstract void Request();
        public abstract HttpResponce GetResponce();
    }
}