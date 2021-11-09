using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpFacade
{
    public abstract class HttpRequestAbstract : IDisposable, IHttpRequest
    {
        protected HttpClient _httpClient;
        protected Uri _Uri => _httpClient.BaseAddress;

        protected HttpRequestAbstract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public abstract IHttpResponce Request();

        public async Task<IHttpResponce> RequestAsync()
        {    
            return await Task.Run(() => Request()); 
        }

        public string RequestAsString()
        {
            IHttpResponce httpResponce = Request();
            string responceBody = httpResponce.ReadAsString();

            return responceBody;
        }

        public async Task<string> RequestAsStringAsync()
        {
            return await Task.Run(() => RequestAsString());
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}