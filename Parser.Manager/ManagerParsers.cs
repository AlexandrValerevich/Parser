using System.Threading.Tasks;

namespace Parser.Manager
{
    public class ManagerParser
    {
        private readonly ExecutorParserBook _executorParserBook;
        private readonly CurrencyConverter _currencyConverter;

        public ManagerParser()
        {
            _executorParserBook = new ExecutorParserBook();
            _currencyConverter = new CurrencyConverter();
        }
        public async Task<BookInfo[]> ParseAsync(string bookName) => await Task.Run(() => Parse(bookName));

        public BookInfo[] Parse(string bookName)
        {
            BookInfo[] books = _executorParserBook.Parse(bookName);
            _currencyConverter.Convert(ref books);

            return books;
        }

    }
}