using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
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

            var parserList = new List<IParserBook>();

            string bookName = "Angular 12";

            parserList.Add(new WildBerriesParser(bookName));
            parserList.Add(new OZParser(bookName));
            parserList.Add(new LabirintParser(bookName));
            parserList.Add(new OzonParser(bookName));

            parserList.ForEach(x => x.Parse());

            var bookInfoList = new List<BookInfo>();

            parserList.ForEach(x => bookInfoList.AddRange(x.GetResult()));
            string jsonBookInfo = BookInfoAdapterToJson.Convert(bookInfoList.ToArray());

            using StreamWriter sw = new StreamWriter("books.json");

            sw.Write(jsonBookInfo); 
            

        }
    }
}
