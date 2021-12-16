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
        private static readonly string s_prefixUri = "https://www.ozon.ru";

        public static async Task<IEnumerable<BookInfo>> ParsreBookAsync(HtmlDocument doc) =>
            await Task<IEnumerable<BookInfo>>.Run(() => ParserBook(doc));

        public static IEnumerable<BookInfo> ParserBook(HtmlDocument doc)
        {
            var cards = doc?.DocumentNode.QuerySelectorAll(".a9x3 .bh6.bi");

            var books = from card in cards
                        select new BookInfo()
                        {
                            UriSite = s_prefixUri + card?.QuerySelector(".b0c8.tile-hover-target")?.Attributes["href"].Value,
                            UriImage = card?.QuerySelector(".ui-o7")?.Attributes["src"].Value,
                            Name = card?.QuerySelector(".a7y.a8a2.a8a6.a8b2.f-tsBodyL.bj5")?.InnerText.Trim(),
                            Price = ParsePrice(card),
                            Currency = "BYN"
                        };

            return books;
        }

        private static decimal ParsePrice(HtmlNode card)
        {
            decimal price;

            try
            {
                price = ParsePriceWithExeption(card);
            }
            catch (System.Exception)
            {
                price = 0;
            }

            return price;
        }

        private static decimal ParsePriceWithExeption(HtmlNode card)
        {
            string priceWithCurrency = card.QuerySelector(".ui-p5.ui-p8")?.InnerHtml.Replace(",", ".");
            string price = priceWithCurrency?[..(priceWithCurrency.IndexOf(".") + 3)];

            return decimal.Parse(price ?? "0");
        }

    }
}