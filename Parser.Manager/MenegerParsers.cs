using System;

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

        public BookInfo[] Parse(string bookName)
        {
            BookInfo[] books = _executorParserBook.Parse(bookName);
            _currencyConverter.Convert(ref books);

            return books;
        }

        // public static void WriteToFileAsJson(BookInfo[] bookInfo)
        // {
        //     string jsonBookInfo = BookInfoAdapterToJson.Convert(bookInfo);

        //     using StreamWriter sw = new StreamWriter("books.json");
        //     sw.Write(jsonBookInfo);
        //     sw.Close(); 
        // }
    }
}