using System;

namespace Parser.Client
{
    class ManagerParser
    {
        private static Object locker = new Object();
        private ExecutorParserBook _executorParserBook;
        private CurrencyConverter _currencyConverter;

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