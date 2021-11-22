using System.IO;
using System.Diagnostics;
using static System.Console;

namespace Parser.Client
{
    class Program
    {
        static ManagerParser manager = new ManagerParser();
        static void Main(string[] args)
        {
            // var parserList = new List<IParser<BookInfo>>();

            // var diagnostic = new DiagnosticParsers();

            // string bookName = "Angular";

            // diagnostic.Diagnostic(bookName);
            // diagnostic.DiagnosticWithAsync(bookName);

            int i = 3;

            while(i > 0)
            {
                Test("React");
                Test("Требования разработки");
                Test(".Net");    
                i--;
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
       
        public static void ConvertToJsonAndWriteToFile(BookInfo[] bookInfo)
        {
            string jsonBookInfo = BookInfoAdapterToJson.Convert(bookInfo);
            
            using StreamWriter sw = new StreamWriter("books.json");
            sw.Write(jsonBookInfo);
            sw.Close(); 
        }

        
    }
}
