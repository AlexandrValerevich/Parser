using System;
using System.Linq;
using Parser.Currency;

namespace Parser.Client
{
    class CurrencyConverter
    {
        private ExecutorParserCurrency _executorParserCurrency;
        private CurrencyInfo[] _currencies;
        private CurrencyInfo RUB => _currencies.First(x => x.Abbreviation == "RUB");
        private CurrencyInfo EUR => _currencies.First(x => x.Abbreviation == "EUR");
        private CurrencyInfo USD => _currencies.First(x => x.Abbreviation == "USD");
        private DateTime? _lastDayOfParseCurrencies;

        public CurrencyConverter()
        {
            _executorParserCurrency = new ExecutorParserCurrency();
            _currencies = UpdateCurrency;
            _lastDayOfParseCurrencies = DateTime.Now;
        }

        public void Convert(ref BookInfo[] books)
        {
            CheckActucalCurrency();
            ConvertCurrency(ref books);
        }

        private void CheckActucalCurrency()
        {
            if(!isActualDate)
            {
                _currencies = UpdateCurrency;
                _lastDayOfParseCurrencies = DateTime.Now;
            }
        }

        private bool isActualDate => _lastDayOfParseCurrencies?.Day == DateTime.Now.Day;

        private CurrencyInfo[] UpdateCurrency => _executorParserCurrency.Parse(
                    CurrencyAbbreviation.RUB |
                    CurrencyAbbreviation.USD |
                    CurrencyAbbreviation.EUR);

        private void ConvertCurrency(ref BookInfo[] books)
        {
            for (var i = 0; i < books.Length; i++)
            {
                if(books[i].Currency == "RUB")
                {
                    ConvertCurrencyRub(ref books[i]);
                }
            }
        }

        private void ConvertCurrencyRub(ref BookInfo book)
        {
            book.Price *= RUB.OfficialRate / 100;
            book.Price = RoundPrice(book.Price);
            book.Currency = "BYN";
        }

        private void ConvertCurrencyUsd(ref BookInfo book)
        {
            book.Price *= USD.OfficialRate;
            book.Price = RoundPrice(book.Price);
            book.Currency = "BYN";
        }

        private void ConvertCurrencyEur(ref BookInfo book)
        {
            book.Price *= EUR.OfficialRate;
            book.Price = RoundPrice(book.Price);
            book.Currency = "BYN";
        }

        private decimal RoundPrice(decimal price) => Math.Round(price, 2);
    }
}