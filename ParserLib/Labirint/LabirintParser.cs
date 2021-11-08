using System;
using Parser;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parser.Labirint
{
    public class LabirintParser : IParser<BookInfo>
    {
        private string _bookName;
        private List<BookInfo> _bookInfo;
        private string _prefixUri = "https://www.labirint.ru";

        public LabirintParser(string bookName) 
        {
            _bookName = bookName;
        }
        public BookInfo[] GetResult()
        {
            return _bookInfo.ToArray();
        }

        public void Parse()
        {
            string responceBody = GetHtmlWithBook();
            List<BookInfo> books = ConvertHtmlToBookInfo(responceBody);
            _bookInfo = books;
        }

        public async Task ParseAsync()
        {
            await Task.Run(() => Parse());
        }

        private string GetHtmlWithBook()
        {
            var requestHtmlFromLabirint = new RequestHtmlFromLabirint(_bookName);
            string responceBody = requestHtmlFromLabirint.GetResponceBody();

            return responceBody;
        }

        private List<BookInfo> ConvertHtmlToBookInfo(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var cards = doc.DocumentNode.QuerySelectorAll(".products-row-outer.responsive-cards .product");
            var books = from card in cards
                        select new BookInfo()
                        {
                            UriSite = _prefixUri + card.QuerySelector(".cover").Attributes["href"].Value,
                            UriImage = card.QuerySelector(".book-img-cover").Attributes["src"].Value,
                            Name = card.QuerySelector(".product-title").InnerText.Trim(),
                            Price = Decimal.Parse(card.QuerySelector(".price-val span").InnerHtml.Replace(" ", "")),
                            Currency = "RU"
                        };

            return books.ToList();
        }
        
    }
}