using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using static Parser.Json.JsonWorker;

using Newtonsoft.Json.Linq;

namespace Parser.Currency
{
    public class ParserCurrency : IParser<CurrencyInfo>
    {
        private string _currency;

        public ParserCurrency()
        {
            _currency = "RUB";
        }

        public async Task<CurrencyInfo[]> ParseAsync(CurrencyAbbreviation currency) => await Task.Run(() => Parse(currency));

        public CurrencyInfo[] Parse(CurrencyAbbreviation currency) => Parse(currency.ToString());
        
        public async Task<CurrencyInfo[]> ParseAsync(string currency) => await Task.Run(() => Parse(currency));
        
        public CurrencyInfo[] Parse(string currency)
        {   
            _currency = currency;

            var currencyResponcies = GetJsonCurrencies();
            var currencyInfos = new List<CurrencyInfo>();

            foreach (string cur in currencyResponcies)
            {
                
                cur.TryParseToJObject(out JObject jObjectCurrency);

                currencyInfos.Add(new CurrencyInfo()
                {
                    Abbreviation = (string)jObjectCurrency["Cur_Abbreviation"],
                    OfficialRate = Decimal.Parse((string)jObjectCurrency["Cur_OfficialRate"])
                });

            }

            return currencyInfos.ToArray();
        }

        private List<string> GetJsonCurrencies()
        {
            string[] currencies = _currency.Split(", ");
            var currencyResponcies = new List<Task<string>>();
            var resultJson = new List<string>();

            foreach (string cur in currencies)
            {
                Task<string> item = RequestToApiNbrb.GetResponceAsync(cur);
                currencyResponcies.Add(item);
            }

            Task.WaitAll(currencyResponcies.ToArray());

            foreach(Task<string> responce in currencyResponcies)
            {
                resultJson.Add(responce.Result);
            }
            
            return resultJson;
        }
        
    }
}