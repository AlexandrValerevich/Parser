using System;
using Parser;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Parser.Labirint
{
    public class LabirintParser : IParserBook
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

            var cards = doc.DocumentNode.QuerySelectorAll(".product");
            var books = from card in cards
                        select new BookInfo()
                        {
                            uriSite = _prefixUri + card.QuerySelector(".cover").Attributes["href"].Value,
                            uriImage = card.QuerySelector(".book-img-cover").Attributes["src"].Value,
                            name = card.QuerySelector(".product-title").InnerText.Trim(),
                            price = Decimal.Parse(card.QuerySelector(".price-val span").InnerHtml.Replace(" ", "")),
                            currency = "RU"
                        };

            return books.ToList();
        }
        
    }
}