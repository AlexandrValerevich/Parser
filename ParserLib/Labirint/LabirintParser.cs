using System;
using Parser;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;
using System.Threading.Tasks;

namespace Parser.Labirint
{
    public class LabirintParser : IParser<BookInfo>
    {
        private string _bookName;
        private string _prefixUri = "https://www.labirint.ru";

        public LabirintParser(string bookName) 
        {
            _bookName = bookName;
        }

        public async Task<BookInfo[]> ParseAsync()
        {
            return await Task.Run(() => Parse());
        }

        public BookInfo[] Parse()
        {
            string responceBody = GetHtmlWithBook();
            BookInfo[] books = ConvertHtmlToBookInfo(responceBody);
            return books;
        }

        private string GetHtmlWithBook()
        {
            string responceBody = RequestHtmlFromLabirint.GetResponce(_bookName);

            return responceBody;
        }

        private BookInfo[] ConvertHtmlToBookInfo(string html)
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

            return books.ToArray();
        }
        
    }
}