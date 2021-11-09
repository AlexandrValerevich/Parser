using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Parser.WildBarries;
using Parser.Labirint;
using Parser.OZ;
using Parser.Ozon;
using Parser.Currency;
using Parser;


namespace Parser.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var parserList = new List<IParser<BookInfo>>();

            string bookName = "Разработка требований";

            Console.WriteLine("Введите книгу:");
            bookName = Console.ReadLine();

            InitializeList(parserList, bookName);

            BookInfo[] bookInfo = ExecuteAllParserWithAsync(parserList);
            CurrencyInfo[] currencyInfo = GetCurrency();

            ConverBookPriceToBLR(ref bookInfo, currencyInfo);
            ConvertToJsonAndWriteToFile(bookInfo); 
        }

        

        private static void InitializeList(List<IParser<BookInfo>> parserList, string bookName)
        {
            parserList.Add(new WildBerriesParser(bookName));
            parserList.Add(new OZParser(bookName));
            parserList.Add(new LabirintParser(bookName));
            parserList.Add(new OzonParser(bookName));
        }

        private static BookInfo[] ExecuteAllParser(List<IParser<BookInfo>> parserList)
        {
            var bookInfo = new List<BookInfo>();
            parserList.ForEach(x => bookInfo.AddRange(x.Parse()));

            return bookInfo.ToArray();
        }

        private static CurrencyInfo[] GetCurrency()
        {
            IParser<CurrencyInfo> parserCurrency = new ParserCurrency(CurrencyAbbreviation.USD | CurrencyAbbreviation.RUB);
            CurrencyInfo[] currencies = parserCurrency.Parse();

            return currencies;
        }

        private static void ConverBookPriceToBLR(ref BookInfo[] bookInfo, CurrencyInfo[] currencyInfo)
        {
            CurrencyInfo rus = currencyInfo.First(x => x.Abbreviation == "RUB");

            for (var i = 0; i < bookInfo.Length; i++)
            {
                if(bookInfo[i].Currency == "RU")
                {
                    bookInfo[i].Price *= rus.OfficialRate / 100;
                    bookInfo[i].Price = Math.Round(bookInfo[i].Price, 2);
                    bookInfo[i].Currency = "BY";
                }
            }
        }

        private static BookInfo[] ExecuteAllParserWithAsync(List<IParser<BookInfo>> parserList)
        {
            var tasks = new List<Task<BookInfo[]>>();
            var bookInfo = new List<BookInfo>();

            parserList.ForEach(x => tasks.Add(x.ParseAsync()));
            tasks.ForEach(x => bookInfo.AddRange(x.Result));

            return bookInfo.ToArray();
        }
       
        private static void ConvertToJsonAndWriteToFile(BookInfo[] bookInfo)
        {
            string jsonBookInfo = BookInfoAdapterToJson.Convert(bookInfo);
            using StreamWriter sw = new StreamWriter("books.json");
            sw.Write(jsonBookInfo); 
        }
    }
}
