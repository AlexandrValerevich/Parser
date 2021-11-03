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

        public override async Task<IHttpResponce> Request()
        {
            //HttpClient.DefaultProxy = new WebProxy("127.0.0.1", 8888);

            HttpContent httpConten = new ByteArrayContent(new byte[0]);
            HttpResponseMessage responce = await _httpClient.PostAsync(_Uri, httpConten);

            var httpResponce = new HttpResponce(responce);

            return httpResponce;
        }
    }
}