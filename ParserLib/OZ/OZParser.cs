using System;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using Parser;

namespace Parser.OZ
{
    public class OZParser : IParserBook
    {
        private string _bookName;
        private string _SearchUri => "https://oz.by/search/?c=1101523&q=" + _bookName;
        private List<BookInfo> _bookInfo;

        public OZParser(string bookName)
        {
            _bookName = bookName.Replace(" ", "+");
            _bookInfo = new List<BookInfo>();
        }
        
        public BookInfo[] GetResult()
        {
            return _bookInfo.ToArray();
        }

        public void Parse()
        {
            string responceBody = GetHtmlWithBook();
            List<BookInfo> books = ConvertHtmlToBookInfo(html: responceBody); 

            _bookInfo = books;
        }

        private string GetHtmlWithBook()
        {
            var requestHtmlFromOz = new RequestHtmlFromOz(_bookName);
            string responceBody = requestHtmlFromOz.GetResponceBody();

            return responceBody;
        }


        private List<BookInfo> ConvertHtmlToBookInfo(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var cards = doc.DocumentNode
            .QuerySelectorAll(".item-type-card__inner");

            var books = cards.Select(x => new BookInfo()
            {
                name = x.QuerySelector(".item-type-card__title").InnerText.Trim(),
                price = Decimal.Parse(x.QuerySelector(".item-type-card__btn")?.InnerText
                                       .Split("&")[0]
                                       .Replace(",", ".")),
                currency = "BY",
                uriSite = "https://oz.by" + x.QuerySelector(".item-type-card__link--main")?.Attributes["href"].Value,
                uriImage = x.QuerySelector(".viewer-type-list__img")?.Attributes["src"].Value
            }).ToList();

            return books;
        }      
    }
}