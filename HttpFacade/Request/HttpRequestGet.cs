using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpFacade
{
    public class HttpRequestGet : HttpRequestAbstract
    {
        public HttpRequestGet(HttpClient httpClient): base(httpClient)
        {

        }

        public override IHttpResponce Request()
        {
            Task<HttpResponseMessage> taskResponce = _httpClient.GetAsync(_Uri);

            HttpResponseMessage responce = taskResponce.Result;
            IHttpResponce httpResponce = new HttpResponce(responce);

            return httpResponce; 
        }

    }
}