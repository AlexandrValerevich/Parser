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

        public WildBerriesParser()
        {
            _bookName = String.Empty;
        }

        public async Task<BookInfo[]> ParseAsync(string bookName)
        {
            return await Task.Run(() => Parse(bookName));
        }

        public BookInfo[] Parse(string bookName)
        {
            _bookName = bookName.Replace(" ", "+");

            string allThingInJson = GetAllThingJsonFormat();
            BookInfo[] bookInfo = FilterOnlyBook(allThingInJson);

            return bookInfo;
        }
        private string GetAllThingJsonFormat()
        {
            (string query, string shardKey) = ParceQuertAndSharedKeyString();
            string xinfo = ParceXinfoString();

            RequestAllThings requestBook = new RequestAllThings(bookName: _bookName,
                                                                xinfoFild: xinfo,
                                                                queriFild: query,
                                                                shardKey: shardKey);

            string book = requestBook.GetResponce();

            return book;
        } 



        private (string query, string shardKey) ParceQuertAndSharedKeyString()
        {
            string responceBody = RequestQueryAndSharedKeyFild.GetResponce(_bookName);

            JObject jObject = JObject.Parse(responceBody);
            
            string query = jObject["query"].ToString();
            string shardKey = jObject["shardKey"].ToString();

            return (query, shardKey);
        }

        private string ParceXinfoString()
        {
            string responceBody = RequestXinfoFild.GetResponce(_bookName);

            JObject jObject = JObject.Parse(responceBody);
            string xInfo = jObject["xinfo"].ToString();

            return xInfo;
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

    }

    
}
