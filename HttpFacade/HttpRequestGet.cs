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

        public override IHttpResponce Request()
        {
            Task<HttpResponseMessage> taskResponce = _httpClient.GetAsync(_Uri);
            taskResponce.Wait();

            HttpResponseMessage responce = taskResponce.Result;
            IHttpResponce httpResponce = new HttpResponce(responce);

            return httpResponce; 
        }

        public override async Task<IHttpResponce> RequestAsync()
        {
            HttpResponseMessage responce = await _httpClient.GetAsync(_Uri);
            IHttpResponce httpResponce = new HttpResponce(responce);

            return httpResponce; 
        }
    }
}