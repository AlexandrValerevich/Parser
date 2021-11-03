using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpFacade
{
    public abstract class HttpRequestAbstract : IDisposable, IHttpRequest
    {
        protected HttpClient _httpClient;
        protected Uri _Uri => _httpClient.BaseAddress;

        public HttpRequestAbstract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public abstract Task<IHttpResponce> Request();

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}