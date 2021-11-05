using System;
using System.IO;
using static System.Console;
using Parser.WildBarries;
using Parser;


namespace Parser.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IParserBook wbParser = new WildBerriesParser("Angular");

            //HttpClient.DefaultProxy = new WebProxy("127.0.0.1", 8888);
            wbParser.Parse();
            BookInfo[] books = wbParser.GetResult();
            BookInfoAdapterToJson jsonAdapter = new BookInfoAdapterToJson(books);
            string jsonBook = jsonAdapter.Convert();
            WriteLine(jsonBook);

            using StreamWriter stream = new StreamWriter("books.json");

            stream.Write(jsonBook);
        }
    }
}
