using System.Threading.Tasks;
using Parser.Currency;

namespace Parser.Manager
{
    class ExecutorParserCurrency
    {
        public ExecutorParserCurrency() {}

        public static async Task<CurrencyInfo[]> ParseAsync(CurrencyAbbreviation currency) =>
            await Task<BookInfo[]>.Run(() => Parse(currency));

        public static CurrencyInfo[] Parse(CurrencyAbbreviation currency)
        {
            var parserCurrency = new ParserCurrency();
            CurrencyInfo[] currencies = parserCurrency.Parse(currency);

            return currencies;
        }
    }
}