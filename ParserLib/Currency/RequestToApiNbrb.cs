using System.Net.Http;
using HttpFacade;

namespace Parser.Currency
{
    static class RequestToApiNbrb
    {
        public static string GetCurrencyJson(CurrencyAbbreviation currencyAbbreviation)
        {
            string[] currencys = currencyAbbreviation.ToString().Split(", ");
            string[] responces = GetArrayCurrency(currencys);

            string result = ConcatToJsonArray(responces);
            
            return result;
        }

        private static string ConcatToJsonArray(string[] responces)
        {
            string joinResponce = string.Join("," , responces);
            string jsonArrayResponce = "[" + joinResponce + "]";
            
            return jsonArrayResponce;
        }

        private static string[] GetArrayCurrency(string[] currencys)
        {
            string[] responces = new string[currencys.Length];

            for (var i = 0; i < currencys.Length; i++)
            {
                responces[i] = GetOneCurrency(currencys[i]);
            }

            return responces;
        }

        private static string GetOneCurrency(string currency)
        {
            IHttpResponce httpResponce = GetResponce(currency);
            string responceBody = httpResponce.ReadAsString();

            return responceBody;
        }

        private static IHttpResponce GetResponce(string currency)
        {
            using HttpClient httpClient = CreateHttpClient(currency);

            var httpRequestGet = new HttpRequestGet(httpClient);
            IHttpResponce httpResponce = httpRequestGet.Request();

            return httpResponce;
        }

        private static HttpClient CreateHttpClient(string currency)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new System.Uri("https://www.nbrb.by/api/exrates/rates/" + currency + "?parammode=2");

            return httpClient;
        }
    }
}