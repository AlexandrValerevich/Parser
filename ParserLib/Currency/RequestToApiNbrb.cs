using System;
using System.Threading.Tasks;
using HttpFacade;

namespace Parser.Currency
{
    static class RequestToApiNbrb
    {
        static public async Task<string> GetResponceAsync(string currency) => await Task.Run(() => GetResponce(currency));

        static public string GetResponce(string currency)
        {
            using IHttpRequest httpRequest = CreateHttpRequest(currency);
            string responceBody = httpRequest.RequestAsString();

            return responceBody;
        }

        private static IHttpRequest CreateHttpRequest(string currency)
        {
            IHttpRequestBulder httpRequestBulder = HttpRequestGetBulder.Create();

            Uri uri = new System.Uri("https://www.nbrb.by/api/exrates/rates/" + currency + "?parammode=2");
            httpRequestBulder.AddUri(uri);

            return httpRequestBulder.Build();
        }
    }
}