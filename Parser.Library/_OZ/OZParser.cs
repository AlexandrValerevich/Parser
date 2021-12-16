using System;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;
using System.Threading.Tasks;

#nullable enable

namespace Parser.OZ
{
    public class OZParser : IParser<BookInfo>
    {
        private string _bookName;
        private readonly HtmlDocument _doc;

        public OZParser()
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
            
            BookInfo[] books = ConvertHtmlToBookInfo(); 

            return books;
        }

        private string GetHtmlWithBook() => RequestHtmlFromOz.GetResponce(_bookName);

        private BookInfo[] ConvertHtmlToBookInfo()
        {
            var cards = _doc.DocumentNode.QuerySelectorAll(".item-type-card__inner");

            var books = from card in cards
                         select new BookInfo()
                         {
                            Name = card.QuerySelector(".item-type-card__title").InnerText.Trim(),
                            Price = GetPriceOfBookInCard(card),
                            Currency = "BYN",
                            UriSite = "https://oz.by" + card.QuerySelector(".item-type-card__link--main")?.Attributes["href"].Value,
                            UriImage = card.QuerySelector(".viewer-type-list__img")?.Attributes["src"].Value
                         };

            return books.ToArray();
        }

        private static decimal GetPriceOfBookInCard(HtmlNode card)
        {
            string? priceWithRub = card.QuerySelector(".item-type-card__btn")?.InnerText.Trim();
            string? price = priceWithRub?.Split("&")[0].Replace(",", ".");

            bool isDigit = decimal.TryParse(price, out decimal result);

            return isDigit ? result : 0;
        }  
    }
}