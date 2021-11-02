using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpFacade
{
    class HttpRequestGet : HttpRequestAbstract
    {
        private HttpClient _httpClient;
        private HttpResponce _httpResponce;
        private Uri _Uri => _httpClient.BaseAddress;

        public HttpRequestGet(HttpClient httpClient): base(httpClient)
        {
            
        }

        public override async void Request()
        {
            var responce = await _httpClient.GetAsync(_Uri);
            _httpResponce = new HttpResponce(responce);
        }

        public override HttpResponce GetResponce()
        {
            if(_httpResponce == null)
            {
                //выбрасываем исключение
                throw new Exception("Запрос еще не поступал");
            }       

            return _httpResponce;
        }
    }
}