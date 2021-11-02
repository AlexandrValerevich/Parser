using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpFacade
{
    public abstract class HttpRequestAbstract : IDisposable, IHttpRequest
    {
        protected HttpClient _httpClient;
        protected HttpResponce _httpResponce;
        protected Uri _Uri => _httpClient.BaseAddress;

        public HttpRequestAbstract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public abstract void Request();

        public abstract IHttpResponce GetResponce();

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}