using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

using Parser.WildBarries;
using Parser.Labirint;
using Parser.OZ;
using Parser.Ozon;
using Parser.Currency;


namespace Parser.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var parserList = new List<IParser<BookInfo>>();

            string bookName = "Angular";

            // Console.WriteLine("Введите книгу:");
            // bookName = Console.ReadLine();

            InitializeList(parserList);

            Stopwatch sw = Stopwatch.StartNew();

            BookInfo[] bookInfo = ExecuteAllParserWithAsync(parserList, bookName);
            CurrencyInfo[] currencyInfo = GetCurrency();
            
            ConverBookPriceToBLR(ref bookInfo, currencyInfo);

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
            ConvertToJsonAndWriteToFile(bookInfo); 
        }

        

        private static void InitializeList(List<IParser<BookInfo>> parserList)
        {
            parserList.Add(new WildBerriesParser());
            parserList.Add(new OZParser());
            parserList.Add(new LabirintParser());
            parserList.Add(new OzonParser());
        }

        private static BookInfo[] ExecuteAllParser(List<IParser<BookInfo>> parserList, string bookName)
        {
            var bookInfo = new List<BookInfo>();
            parserList.ForEach(x => bookInfo.AddRange(x.Parse(bookName)));

            return bookInfo.ToArray();
        }

        private static CurrencyInfo[] GetCurrency()
        {
            ParserCurrency parserCurrency = new ParserCurrency();
            CurrencyInfo[] currencies = parserCurrency.Parse(CurrencyAbbreviation.USD | CurrencyAbbreviation.RUB);

            return currencies;
        }

        private static void ConverBookPriceToBLR(ref BookInfo[] bookInfo, CurrencyInfo[] currencyInfo)
        {
            CurrencyInfo rus = currencyInfo.First(x => x.Abbreviation == "RUB");

            for (var i = 0; i < bookInfo.Length; i++)
            {
                if(bookInfo[i].Currency == "RUB")
                {
                    bookInfo[i].Price *= rus.OfficialRate / 100;
                    bookInfo[i].Price = Math.Round(bookInfo[i].Price, 2);
                    bookInfo[i].Currency = "BYN";
                }
            }
        }

        private static BookInfo[] ExecuteAllParserWithAsync(List<IParser<BookInfo>> parserList, string bookName)
        {
            var tasks = new List<Task<BookInfo[]>>();
            var bookInfo = new List<BookInfo>();

            parserList.ForEach(x => tasks.Add(x.ParseAsync(bookName)));
            tasks.ForEach(x => bookInfo.AddRange(x.Result));

            return bookInfo.ToArray();
        }
       
        private static void ConvertToJsonAndWriteToFile(BookInfo[] bookInfo)
        {
            string jsonBookInfo = BookInfoAdapterToJson.Convert(bookInfo);
            
            using StreamWriter sw = new StreamWriter("books.json");
            sw.Write(jsonBookInfo);
            sw.Close(); 
        }
    }
}
