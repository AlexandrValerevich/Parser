using System;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;
using System.Threading.Tasks;

namespace Parser.Labirint
{
    public class LabirintParser : IParser<BookInfo>
    {
        private string _bookName;
        private HtmlDocument _doc;
        private static readonly string s_prefixUri = "https://www.labirint.ru";

        public LabirintParser() 
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

        private string GetHtmlWithBook() => RequestHtmlFromLabirint.GetResponce(_bookName);
 
        private BookInfo[] ConvertHtmlToBookInfo()
        {
            var cards = _doc.DocumentNode.QuerySelectorAll(".products-row-outer.responsive-cards .product");

            var books = from card in cards
                        select new BookInfo()
                        {
                            UriSite = s_prefixUri + card.QuerySelector(".cover").Attributes["href"].Value,
                            UriImage = card.QuerySelector(".book-img-cover").Attributes["src"].Value,
                            Name = card.QuerySelector(".product-title").InnerText.Trim(),
                            Price = Decimal.Parse(card.QuerySelector(".price-val span").InnerHtml.Replace(" ", "")),
                            Currency = "RUB"
                        };

            return books.ToArray();
        }
        
    }
}