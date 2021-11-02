using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;


namespace HttpFacade
{
    public class HttpResponce : IDisposable, IHttpResponce
    {
        private HttpResponseMessage _httpResponceMassage;

        private HttpContent _Content => _Content;

        public HttpResponce(HttpResponseMessage httpResponceMassage)
        {
            _httpResponceMassage = httpResponceMassage;
        }

        public IHttpResponce EnsureSuccessStatusCode()
        {
            _httpResponceMassage.EnsureSuccessStatusCode();
            return this;
        }

        public async Task<byte[]> ReadAsByteArrayAsync()
        {
            byte[] responce = await  _Content.ReadAsByteArrayAsync();
            return responce;
        }

        public Stream ReadAsStream()
        {
            Stream responce = _Content.ReadAsStream();
            return responce;
        }

        public async Task<string> ReadAsStringAsync()
        {
            string responce = await _Content.ReadAsStringAsync();
            return responce;
        }

        public void Dispose()
        {
            _httpResponceMassage.Dispose();
        }
    }
}