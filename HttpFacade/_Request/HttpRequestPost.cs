using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpFacade
{
    public class HttpRequestPost : HttpRequestAbstract
    {
        private HttpContent _httpContent;
        private readonly HttpContent _DefaultHttpContent = new StringContent("");

        public HttpRequestPost(HttpClient httpClient): base(httpClient)
        {
            _httpContent = _DefaultHttpContent;
        }

        public override IHttpResponce Request()
        {
            Task<HttpResponseMessage> taskResponce = _httpClient.PostAsync(Uri, _httpContent);

            HttpResponseMessage responce = taskResponce.Result;
            responce.EnsureSuccessStatusCode();
            IHttpResponce httpResponce = new HttpResponce(responce);

            return httpResponce;
        }
        
        public IHttpResponce Request(string content)
        {
            _httpContent = new StringContent(content);
            IHttpResponce httpResponce = Request();
            _httpContent = _DefaultHttpContent;

            return httpResponce;
        }

    }
}