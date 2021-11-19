using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace Parser.Ozon
{
    static class ParseBookFromVisibleHtmlNode
    {
        static private string _prefixUri = "https://www.ozon.ru";

        static public async Task<IEnumerable<BookInfo>> ParsreBookAsync(HtmlDocument doc) =>
            await Task<IEnumerable<BookInfo>> .Run(() => ParserBook(doc));

        static public IEnumerable<BookInfo> ParserBook(HtmlDocument doc)
        {
            var cards = doc?.DocumentNode.QuerySelectorAll(".a9x3 .bh6.bi");

            var books = from card in cards
                        select new BookInfo()
                        {
                            UriSite = _prefixUri + card.QuerySelector(".b0c8.tile-hover-target").Attributes["href"].Value,
                            UriImage = card.QuerySelector(".ui-o7").Attributes["src"].Value,
                            Name = card.QuerySelector(".a7y.a8a2.a8a6.a8b2.f-tsBodyL.bj5").InnerText.Trim(),
                            Price = GetPriceOfBookInCard(card),
                            Currency = "BYN"
                        };
            
            return  books;
        }

        static private decimal GetPriceOfBookInCard(HtmlNode card)
        {
            string? priceWithCurrency = card.QuerySelector(".ui-p5.ui-p8")?.InnerHtml.Replace(",", ".");
            string? price = priceWithCurrency?.Substring(0, priceWithCurrency.IndexOf(".") + 3);

            return Decimal.Parse(price ?? "0"); 
        }

    }
}