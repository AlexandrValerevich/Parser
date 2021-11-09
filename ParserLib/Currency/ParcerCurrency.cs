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

        public ParserCurrency(CurrencyAbbreviation currencyName)
        {
            _currencyName = currencyName;
       }

        public async Task<CurrencyInfo[]> ParseAsync()
        {
            return await Task.Run(() => Parse());
        }

        public CurrencyInfo[] Parse()
        {
            string currencys = RequestToApiNbrb.GetCurrencyJson(_currencyName);
            CurrencyInfo[] currencyInfo = ConvertToCurrenctInfo(currencys);

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