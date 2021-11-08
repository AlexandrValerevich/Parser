using System;
using Parser;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable enable

namespace Parser.Ozon
{
    public class OzonParser : IParser<BookInfo>
    {
        private string _bookName;
        private List<BookInfo> _bookInfo;
        private string _prefixUri = "https://www.ozon.ru";

        public OzonParser(string bookName) 
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
            var requestHtmlFromLabirint = new RequestHtmlFromOzon(_bookName);
            string responceBody = requestHtmlFromLabirint.GetResponceBody();

            return responceBody;
        }

        private List<BookInfo> ConvertHtmlToBookInfo(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var cards = doc.DocumentNode.QuerySelectorAll(".a9x3 .bh6.bi");

            var books = from card in cards
                        select new BookInfo()
                        {
                            uriSite = _prefixUri + card.QuerySelector(".b0c8.tile-hover-target").Attributes["href"].Value,
                            uriImage = card.QuerySelector(".ui-o7").Attributes["src"].Value,
                            name = card.QuerySelector(".a7y.a8a2.a8a6.a8b2.f-tsBodyL.bj5").InnerText.Trim(),
                            price = GetPriceOfBookInCard(card),
                            currency = "BY"
                        };

            return books.ToList();
        }

        private decimal GetPriceOfBookInCard(HtmlNode card)
        {
            string? priceWithCurrency = card.QuerySelector(".ui-p5.ui-p8")?.InnerHtml.Replace(",", ".");
            string? price = priceWithCurrency?.Substring(0, priceWithCurrency.IndexOf(".") + 3);

            return Decimal.Parse(price ?? "0"); 
        }
        
    }
}