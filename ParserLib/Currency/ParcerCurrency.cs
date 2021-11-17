using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Parser;

using Newtonsoft.Json.Linq;

namespace Parser.Currency
{
    public class ParserCurrency : IParser<CurrencyInfo>
    {
        private CurrencyAbbreviation _currencyName;

        public ParserCurrency()
        {
            _currencyName = CurrencyAbbreviation.RUB;
        }

        public async Task<CurrencyInfo[]> ParseAsync(string currencyName)
        {
            return await Task.Run(() => Parse(currencyName));
        }

        public async Task<CurrencyInfo[]> ParseAsync(CurrencyAbbreviation currencyName)
        {
            return await Task.Run(() => Parse(currencyName));
        }

        public CurrencyInfo[] Parse(string currencyName)
        {        
            string currencys = RequestToApiNbrb.GetCurrencyJson(currencyName);
            CurrencyInfo[] currencyInfo = ConvertToCurrenctInfo(currencys);

            return currencyInfo;
        }

        public CurrencyInfo[] Parse(CurrencyAbbreviation currencyName)
        {   
            CurrencyInfo[] currencyInfo = Parse(currencyName.ToString());
            return currencyInfo;
        }

        private CurrencyInfo[] ConvertToCurrenctInfo(string currencys)
        {
            JArray curs = JArray.Parse(currencys);

            var filtredCurrencysField = from cur in curs
                                        select new CurrencyInfo()
                                        {
                                            Abbreviation = (string)cur["Cur_Abbreviation"],
                                            OfficialRate = Decimal.Parse((string)cur["Cur_OfficialRate"])
                                        };

            return filtredCurrencysField.ToArray();
        }
        
    }
}