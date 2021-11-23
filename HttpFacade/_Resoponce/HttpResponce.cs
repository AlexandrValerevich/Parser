using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace HttpFacade
{
    public class HttpResponce : IDisposable, IHttpResponce
    {
        private HttpResponseMessage _httpResponceMassage;

        private HttpContent _сontent => _httpResponceMassage.Content;

        public HttpResponce(HttpResponseMessage httpResponceMassage)
        {
            _httpResponceMassage = httpResponceMassage;
        }

        public IHttpResponce EnsureSuccessStatusCode()
        {
            _httpResponceMassage.EnsureSuccessStatusCode();
            return this;
        }

        public async Task<byte[]> ReadAsByteArrayAsync() => await _сontent.ReadAsByteArrayAsync();

        public Stream ReadAsStream() => _сontent.ReadAsStream();

        public async Task<string> ReadAsStringAsync() => await _сontent.ReadAsStringAsync();

        public string ReadAsString()
        {
            Task<string> taskResponce = _сontent.ReadAsStringAsync();
            taskResponce.Wait();

            string responce = taskResponce.Result;

            return responce;
        }

        public void Dispose()
        {
            _httpResponceMassage.Dispose();
        }
    }
}