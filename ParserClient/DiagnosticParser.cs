using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

using Parser.WildBarries;
using Parser.Labirint;
using Parser.OZ;
using Parser.Ozon;

namespace Parser.Client
{
    class DiagnosticParsers
    {
        private List<IParser<BookInfo>> _parsers;

        public DiagnosticParsers()
        {
            _parsers = new List<IParser<BookInfo>>
            {
                new WildBerriesParser(),
                new OZParser(),
                new LabirintParser(),
                new OzonParser()
            };
        }

        public void Diagnostic(string bookName)
        {
            Stopwatch sw;
            _parsers.ForEach(x => 
            {
                string nameParser = x.GetType().Name;
                sw = Stopwatch.StartNew();
                
                x.Parse(bookName);
                
                sw.Stop();
                WriteLine($"{nameParser}: {sw.ElapsedMilliseconds}мс");
            });
        }

        public void DiagnosticWithAsync(string bookName)
        {
            var tasks = new List<Task<BookInfo[]>>();

            Stopwatch sw;
            sw = Stopwatch.StartNew();

            _parsers.ForEach(x => tasks.Add(x.ParseAsync(bookName)));
            Task.WaitAll(tasks.ToArray());

            sw.Stop();
            WriteLine($"AllParserAsync {sw.ElapsedMilliseconds}мс");
        }
       
    }
}