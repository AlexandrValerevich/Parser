using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpFacade
{
    public class HttpRequestPost : HttpRequestAbstract
    {
        public HttpRequestPost(HttpClient httpClient): base(httpClient)
        {
            
        }

        public override IHttpResponce Request()
        {
            HttpContent httpConten = new ByteArrayContent(new byte[0]);
            Task<HttpResponseMessage> taskResponce = _httpClient.PostAsync(_Uri, httpConten);

            HttpResponseMessage responce = taskResponce.Result;
            IHttpResponce httpResponce = new HttpResponce(responce);

            return httpResponce;
        }
    }
}