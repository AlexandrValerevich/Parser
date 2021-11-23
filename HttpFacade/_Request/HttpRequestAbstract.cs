using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpFacade
{
    public abstract class HttpRequestAbstract : IHttpRequest
    {
        protected HttpClient _httpClient;
        protected Uri _Uri => _httpClient.BaseAddress;
        
        public abstract IHttpResponce Request();

        protected HttpRequestAbstract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponce> RequestAsync() => await Task.Run(() => Request()); 
        
        public async Task<string> RequestAsStringAsync() => await Task.Run(() => RequestAsString());
        
        public string RequestAsString()
        {
            IHttpResponce httpResponce = Request();
            string responceBody = httpResponce.ReadAsString();

            return responceBody;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}