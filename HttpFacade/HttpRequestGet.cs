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
        {}

        public override async Task<IHttpResponce> Request()
        {
            HttpResponseMessage responce = await _httpClient.GetAsync(_Uri);
            var httpResponce = new HttpResponce(responce);

            return httpResponce; 
        }

    }
}