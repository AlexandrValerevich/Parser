using System.IO;
using System.Diagnostics;
using static System.Console;
using System.Collections.Generic;

using Parser.Manager;
using Parser.Diagnostic;

namespace Parser.Client
{
    class Program
    {
        static readonly ManagerParser manager = new();
        static void Main()
        {
            //var parserList = new List<IParser<BookInfo>>();

            var diagnostic = new SpeedTestParsers();
            string bookName = "Angular";

            diagnostic.Diagnostic(bookName);
            diagnostic.DiagnosticWithAsync(bookName);

            

            for(int i = 0; i < 3; i++)
            {
                Test("React");
                Test("Требования разработки");
                Test(".Net");    
            } 

            //ConvertToJsonAndWriteToFile(books);
        }

        private static void Test(string bookName)
        {
            Stopwatch sw = Stopwatch.StartNew();
            BookInfo[] books = manager.Parse(bookName);
            sw.Stop();

            WriteLine($"{sw.ElapsedMilliseconds} ms");
        }
       
        // public static void ConvertToJsonAndWriteToFile(BookInfo[] bookInfo)
        // {
        //     string jsonBookInfo = BookInfoAdapterToJson.Convert(bookInfo);
            
        //     using var sw = new StreamWriter("books.json");
        //     sw.Write(jsonBookInfo);
        //     sw.Close(); 
        // }

        
    }
}
