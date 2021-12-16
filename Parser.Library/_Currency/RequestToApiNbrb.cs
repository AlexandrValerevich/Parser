using System;
using System.Threading.Tasks;
using HttpFacade;

namespace Parser.Currency
{
    static class RequestToApiNbrb
    {
        public static async Task<string> GetResponceAsync(string currency) => await Task.Run(() => GetResponce(currency));

        public static string GetResponce(string currency)
        {
            using IHttpRequest httpRequest = CreateHttpRequest(currency);
            string responceBody = httpRequest.RequestAsString();

            return responceBody;
        }

        private static IHttpRequest CreateHttpRequest(string currency)
        {
            IHttpRequestBulder httpRequestBulder = HttpRequestGetBulder.Create();

            var uri = new Uri("https://www.nbrb.by/api/exrates/rates/" + currency + "?parammode=2");
            httpRequestBulder.AddUri(uri);

            return httpRequestBulder.Build();
        }
    }
}