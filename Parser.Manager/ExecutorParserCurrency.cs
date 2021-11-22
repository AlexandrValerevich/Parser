using System.Threading.Tasks;
using Parser.Currency;

namespace Parser.Manager
{
    class ExecutorParserCurrency
    {
        public ExecutorParserCurrency() {}

        public async Task<CurrencyInfo[]> ParseAsync(CurrencyAbbreviation currency) =>
            await Task<BookInfo[]>.Run(() => Parse(currency));

        public CurrencyInfo[] Parse(CurrencyAbbreviation currency)
        {
            ParserCurrency parserCurrency = new ParserCurrency();
            CurrencyInfo[] currencies = parserCurrency.Parse(currency);

            return currencies;
        }
    }
}