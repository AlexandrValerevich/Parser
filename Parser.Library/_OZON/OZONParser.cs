using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

#nullable enable

namespace Parser.Ozon
{
    public class OzonParser : IParser<BookInfo>
    {
        private string _bookName;
        private readonly HtmlDocument _doc;

        public OzonParser()
        {
            _bookName = String.Empty;
            _doc = new HtmlDocument();
        }

        public async Task<BookInfo[]> ParseAsync(string bookName) => await Task.Run(() => Parse(bookName));

        public BookInfo[] Parse(string bookName)
        {
            _bookName = bookName.Replace(" ", "+");

            string html = GetHtmlWithBook();
            _doc.LoadHtml(html);

            BookInfo[] books = ParseBookFromHtml();

            return books;
        }

        private string GetHtmlWithBook() => RequestHtmlFromOzon.GetResponce(_bookName);

        private BookInfo[] ParseBookFromHtml()
        {
            var result = new List<BookInfo>();

            //var booksFromVisibleHtmlNodes = BookFromVisibleNodeAsync();
            var booksFromHideHtmlNodes = BookFromHidedNodeAsync();

            //result.AddRange(booksFromVisibleHtmlNodes.Result);
            result.AddRange(booksFromHideHtmlNodes.Result);

            return result.ToArray();
        }

        //private Task<IEnumerable<BookInfo>> BookFromVisibleNodeAsync() => ParseBookFromVisibleHtmlNode.ParsreBookAsync(_doc);

        private Task<IEnumerable<BookInfo>> BookFromHidedNodeAsync() => ParseBookFromHidedHtmlNode.ParseBookAsync(_doc);

    }
}