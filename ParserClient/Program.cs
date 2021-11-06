using System;
using System.IO;
using static System.Console;
using Parser.WildBarries;
using Parser.OZ;
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

            // IParserBook ozParser = new OZParser("Angular");

            // ozParser.Parse();

            // var books = ozParser.GetResult();

            // BookInfoAdapterToJson jsonAdapter = new BookInfoAdapterToJson(books);
            // string jsonBook = jsonAdapter.Convert();
            // WriteLine(jsonBook);

        }
    }
}
