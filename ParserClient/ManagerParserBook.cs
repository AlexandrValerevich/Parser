using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

using Parser.WildBarries;
using Parser.Labirint;
using Parser.OZ;
using Parser.Ozon;
using Parser.Currency;

namespace Parser.Client
{
    class ManagerParserBook
    {
        private List<IParser<BookInfo>> _parsers;
        // private ManagerParserCurrency _managerParserCurrency;
        // private CurrencyInfo[] _currencies;
        // private DateTime? _dateOfLastCurrencyParse;

        public ManagerParserBook()
        {
            _parsers = new List<IParser<BookInfo>>
            {
                new WildBerriesParser(),
                new OZParser(),
                new LabirintParser(),
                new OzonParser()
            };
            // _dateOfLastCurrencyParse = null;
            // _currencies = null;
        }

        public async Task<BookInfo[]> ParseAsync(string bookName) => await Task<BookInfo[]>.Run(() => Parse(bookName));

        public BookInfo[] Parse(string bookName)
        {
            var tasks = new List<Task<BookInfo[]>>();
            var bookInfo = new List<BookInfo>();

            _parsers.ForEach(x => tasks.Add(x.ParseAsync(bookName)));
            tasks.ForEach(x => bookInfo.AddRange(x.Result));

            return bookInfo.ToArray();
        }

        // private async void CheckActucalCurrency()
        // {
        //     if(isActualDate)
        //     {

        //     }
        // }

        // private bool isActualDate => _dateOfLastCurrencyParse?.Day == DateTime.Now.Day;
        

        
        // public BookInfo[] Parse(List<IParser<BookInfo>> parserList, string bookName)
        // {
        //     var bookInfo = new List<BookInfo>();
        //     parserList.ForEach(x => bookInfo.AddRange(x.Parse(bookName)));

        //     return bookInfo.ToArray();
        // }

        // public CurrencyInfo[] GetCurrency()
        // {
        //     ParserCurrency parserCurrency = new ParserCurrency();
        //     CurrencyInfo[] currencies = parserCurrency.Parse(CurrencyAbbreviation.USD | CurrencyAbbreviation.RUB);

        //     return currencies;
        // }

        // public void ConverBookPriceToBLR(ref BookInfo[] bookInfo, CurrencyInfo[] currencyInfo)
        // {
        //     CurrencyInfo rus = currencyInfo.First(x => x.Abbreviation == "RUB");

        //     for (var i = 0; i < bookInfo.Length; i++)
        //     {
        //         if(bookInfo[i].Currency == "RUB")
        //         {
        //             bookInfo[i].Price *= rus.OfficialRate / 100;
        //             bookInfo[i].Price = Math.Round(bookInfo[i].Price, 2);
        //             bookInfo[i].Currency = "BYN";
        //         }
        //     }
        // }

        // public static void WriteToFileAsJson(BookInfo[] bookInfo)
        // {
        //     string jsonBookInfo = BookInfoAdapterToJson.Convert(bookInfo);
            
        //     using StreamWriter sw = new StreamWriter("books.json");
        //     sw.Write(jsonBookInfo);
        //     sw.Close(); 
        // }
    }
}