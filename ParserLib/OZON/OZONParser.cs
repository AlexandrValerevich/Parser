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
        private string _prefixUri = "https://www.ozon.ru";
        private HtmlDocument _doc;

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
            List<BookInfo> result = new List<BookInfo>();

            IEnumerable<BookInfo> booksFromHtmlNodes = BookFromVisibleNode();
            IEnumerable<BookInfo> booksFromHideHtmlNodes = BookFromHidedNode(); 
            
            result.AddRange(booksFromHtmlNodes);
            result.AddRange(booksFromHideHtmlNodes);

            return result.ToArray();
        }

        private IEnumerable<BookInfo> BookFromVisibleNode() => ParseBookFromVisibleHtmlNode.ParserBook(_doc);

        private IEnumerable<BookInfo> BookFromHidedNode() => ParseBookFromHidedHtmlNode.ParserBook(_doc);
        
    }
}