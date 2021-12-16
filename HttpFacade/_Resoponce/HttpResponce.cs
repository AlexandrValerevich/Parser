using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace HttpFacade
{
    public class HttpResponce : IDisposable, IHttpResponce
    {
        private readonly HttpResponseMessage _httpResponceMassage;

        private HttpContent Content => _httpResponceMassage.Content;

        public HttpResponce(HttpResponseMessage httpResponceMassage)
        {
            _httpResponceMassage = httpResponceMassage;
        }

        public IHttpResponce EnsureSuccessStatusCode()
        {
            _httpResponceMassage.EnsureSuccessStatusCode();
            return this;
        }

        public async Task<byte[]> ReadAsByteArrayAsync() => await Content.ReadAsByteArrayAsync();

        public Stream ReadAsStream() => Content.ReadAsStream();

        public async Task<string> ReadAsStringAsync() => await Content.ReadAsStringAsync();

        public string ReadAsString()
        {
            Task<string> taskResponce = Content.ReadAsStringAsync();
            taskResponce.Wait();

            string responce = taskResponce.Result;

            return responce;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}