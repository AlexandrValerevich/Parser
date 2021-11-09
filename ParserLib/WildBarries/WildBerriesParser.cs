using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Parser.WildBarries
{
    public class WildBerriesParser : IParser<BookInfo>
    {
        private string _bookName;
        private string _imageUriPrefix => "https://kemlenvg8e.a.trbcdn.net/c516x688/new/";
        private string _siteUriPrefix => "https://by.wildberries.ru/catalog/";


        public WildBerriesParser(string bookName)
        {
            _bookName = bookName.Replace(" ", "+");
        }

        public async Task<BookInfo[]> ParseAsync()
        {
            return await Task.Run(() => Parse());
        }

        public BookInfo[] Parse()
        {
            string allThingInJson = GetAllThingJsonFormat();
            BookInfo[] bookInfo = FilterOnlyBook(allThingInJson);

            return bookInfo;
        }

        private BookInfo[] FilterOnlyBook(string allThingInJson)
        {
            JArray products = JObject.Parse(allThingInJson)["data"]["products"] as JArray;

            var productOfBook = products
            .Where(x => (int)x["subjectId"] == 381)
            .Select(x => new BookInfo(){
                Name = (string)x["name"],
                Brand = (string)x["brand"],
                Price = (int)x["salePriceU"]/100, // убираем 2 нуля
                Currency = "RU",
                UriSite = _siteUriPrefix + (string)x["id"] + "/detail.aspx?targetUrl=XS",
                UriImage = _imageUriPrefix + ((int)x["id"]/10000 * 10000) + "/" + (string)x["id"] + "-1.jpg" 
            }).ToArray();

            return productOfBook;
        }

        private string GetAllThingJsonFormat()
        {
            (string query, string shardKey) = ParceQuertAndSharedKeyString();
            string xinfo = ParceXinfoString();

            RequestAllThings requestBook = new RequestAllThings(_bookName, xinfo, query, shardKey);

            string book = requestBook.GetResponceBody();

            return book;
        } 

        private (string query, string shardKey) ParceQuertAndSharedKeyString()
        {
            RequestQueryAndSharedKeyFild requstQueryAndSharedKeyFild = new RequestQueryAndSharedKeyFild(_bookName);
            string responceBody = requstQueryAndSharedKeyFild.GetResponceBody();

            JObject jObject = JObject.Parse(responceBody);
            
            string query = jObject["query"].ToString();
            string shardKey = jObject["shardKey"].ToString();

            return (query, shardKey);
        }

        private string ParceXinfoString()
        {
            RequestXinfoFild requestXinfoFild = new RequestXinfoFild(_bookName);
            string responceBody = requestXinfoFild.GetResponceBody();

            JObject jObject = JObject.Parse(responceBody);
            string xInfo = jObject["xinfo"].ToString();

            return xInfo;
        }

    }

    
}
