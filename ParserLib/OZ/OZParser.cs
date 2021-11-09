using System;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using Parser;
using System.Threading.Tasks;

#nullable enable

namespace Parser.OZ
{
    public class OZParser : IParser<BookInfo>
    {
        private string _bookName;
        private string _SearchUri => "https://oz.by/search/?c=1101523&q=" + _bookName;

        public OZParser(string bookName)
        {
            _bookName = bookName.Replace(" ", "+");
        }

        public BookInfo[] Parse()
        {
            string responceBody = GetHtmlWithBook();
            BookInfo[] books = ConvertHtmlToBookInfo(html: responceBody); 

            return books;
        }

        public async Task<BookInfo[]> ParseAsync()
        {
            return await Task.Run(() => Parse());
        }

        private string GetHtmlWithBook()
        {
            var requestHtmlFromOz = new RequestHtmlFromOz(_bookName);
            string responceBody = requestHtmlFromOz.GetResponceBody();

            return responceBody;
        }

        private BookInfo[] ConvertHtmlToBookInfo(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var cards = doc.DocumentNode
            .QuerySelectorAll(".item-type-card__inner");

            var books = from card in cards
                         select new BookInfo()
                         {
                            Name = card.QuerySelector(".item-type-card__title").InnerText.Trim(),
                            Price = GetPriceOfBookInCard(card),
                            Currency = "BY",
                            UriSite = "https://oz.by" + card.QuerySelector(".item-type-card__link--main")?.Attributes["href"].Value,
                            UriImage = card.QuerySelector(".viewer-type-list__img")?.Attributes["src"].Value
                         };

            return books.ToArray();
        }

        private decimal GetPriceOfBookInCard(HtmlNode card)
        {
            string? priceWithRub = card.QuerySelector(".item-type-card__btn")?.InnerText.Trim();
            string? price = priceWithRub?.Split("&")[0].Replace(",", ".");

            decimal result = 0;
            bool isDigit = decimal.TryParse(price, out result);

            return result;
        }  
    }
}