using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
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
            // IBookParser wbParser = new WildBerriesParser("Angular");

            // //HttpClient.DefaultProxy = new WebProxy("127.0.0.1", 8888);
            // wbParser.Parse();
            // BookInfo[] books = wbParser.GetResult();
            // BookInfoAdapterToJson jsonAdapter = new BookInfoAdapterToJson(books);
            // string jsonBook = jsonAdapter.Convert();
            // WriteLine(jsonBook);

            // using StreamWriter stream = new StreamWriter("books.json");

            // stream.Write(jsonBook);

            // IParserBook ozParser = new OZParser("Разработка требований");

            // ozParser.Parse();

            // var books = ozParser.GetResult();

            // BookInfoAdapterToJson jsonAdapter = new BookInfoAdapterToJson(books);
            // string jsonBook = jsonAdapter.Convert();
            // WriteLine(jsonBook);

            // IParserBook labirintParser = new LabirintParser("Разработка требований");

            // labirintParser.Parse();

            // var books = labirintParser.GetResult();

            // BookInfoAdapterToJson jsonAdapter = new BookInfoAdapterToJson(books);
            // string jsonBook = jsonAdapter.Convert();
            // WriteLine(jsonBook);

            // IParserBook wbParser = new WildBerriesParser("Разработка требований");
            // IParserBook ozParser = new OZParser("Разработка требований");
            // IParserBook labirintParser = new LabirintParser("Разработка требований");
            // IParserBook OzonParser = new OzonParser("Разработка требований");

            // OzonParser.Parse();

            // var books = OzonParser.GetResult();

            // BookInfoAdapterToJson jsonAdapter = new BookInfoAdapterToJson(books);
            // string jsonBook = jsonAdapter.Convert();
            // WriteLine(jsonBook);

            var parserList = new List<IParser<BookInfo>>();
            var tasks = new List<Task>();
            var bookInfoList = new List<BookInfo>();

            string bookName = "Angular";

            InitializeList(parserList, bookName);

            parserList.ForEach(x => tasks.Add(x.ParseAsync()));

            tasks.ForEach(x => x.Wait());

            parserList.ForEach(x => bookInfoList.AddRange(x.GetResult()));
            string jsonBookInfo = BookInfoAdapterToJson.Convert(bookInfoList.ToArray());

            using StreamWriter sw = new StreamWriter("books.json");

            sw.Write(jsonBookInfo); 
            

        }

        private static void InitializeList(List<IParser<BookInfo>> parserList, string bookName)
        {
            parserList.Add(new WildBerriesParser(bookName));
            parserList.Add(new OZParser(bookName));
            parserList.Add(new LabirintParser(bookName));
            parserList.Add(new OzonParser(bookName));
        }
    }
}
