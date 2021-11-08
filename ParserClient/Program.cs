using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

using Parser.WildBarries;
using Parser.Labirint;
using Parser.OZ;
using Parser.Ozon;
using Parser;


namespace Parser.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var parserList = new List<IParser<BookInfo>>();

            string bookName = "C#";

            InitializeList(parserList, bookName);
            BookInfo[] bookInfo = ExecuteAllParser(parserList);
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

            parserList.ForEach(x => x.Parse());
            parserList.ForEach(x => bookInfo.AddRange(x.GetResult()));

            return bookInfo.ToArray();
        }

        private static BookInfo[] ExecuteAllParserWithAsync(List<IParser<BookInfo>> parserList)
        {
            var tasks = new List<Task>();
            var bookInfo = new List<BookInfo>();

            parserList.ForEach(x => tasks.Add(x.ParseAsync()));
            tasks.ForEach(x => x.Wait());
            parserList.ForEach(x => bookInfo.AddRange(x.GetResult()));

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
