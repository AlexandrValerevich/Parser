using System.Collections.Generic;
using System.Threading.Tasks;

using Parser.WildBarries;
using Parser.Labirint;
using Parser.OZ;
using Parser.Ozon;

namespace Parser.Client
{
    class ExecutorParserBook
    {
        private List<IParser<BookInfo>> _parsers;

        public ExecutorParserBook()
        {
            _parsers = new List<IParser<BookInfo>>
            {
                new WildBerriesParser(),
                new OZParser(),
                new LabirintParser(),
                new OzonParser()
            };
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
    }
}