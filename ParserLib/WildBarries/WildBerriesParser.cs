using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Parser.WildBarries
{
    public class WildBerriesParser : IParser<BookInfo>
    {
        private string _bookName;
        private string _imageUriPrefix => "https://kemlenvg8e.a.trbcdn.net/c516x688/new/";
        private string _siteUriPrefix => "https://by.wildberries.ru/catalog/";

        public WildBerriesParser()
        {
            _bookName = String.Empty;
        }

        public async Task<BookInfo[]> ParseAsync(string bookName) => await Task.Run(() => Parse(bookName));
        
        public BookInfo[] Parse(string bookName)
        {
            _bookName = bookName.Replace(" ", "+");

            string allThingInJson = GetAllThingJsonFormat();
            BookInfo[] bookInfo = FilterBookFromThings(allThingInJson);

            return bookInfo;
        }
        private string GetAllThingJsonFormat()
        {
            string things = RequestAllThings.GetResponce(_bookName);
            return things;
        } 

        private BookInfo[] FilterBookFromThings(string allThingInJson)
        {
            JArray products = JObject.Parse(allThingInJson)["data"]["products"] as JArray;

            var productOfBook = products
            .Where(product => (int)product["subjectId"] == 381)
            .Select(product => new BookInfo(){
                Name = (string)product["name"],
                Brand = (string)product["brand"],
                Price = (int)product["salePriceU"]/100, // убираем 2 нуля
                Currency = "RUB",
                UriSite = _siteUriPrefix + (string)product["id"] + "/detail.aspx?targetUrl=XS",
                UriImage = _imageUriPrefix + ((int)product["id"]/10000 * 10000) + "/" + (string)product["id"] + "-1.jpg" 
            }).ToArray();

            return productOfBook;
        }

    }

    
}
