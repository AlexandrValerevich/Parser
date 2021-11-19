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
            var queryAndSharedKeyTask = ParseQueryAndSharedKeyAsync();
            var xinfoTask = ParseXinfoAsync();

            (string query, string shardKey) = queryAndSharedKeyTask.Result;
            string xinfo = xinfoTask.Result;

            RequestAllThings requestBook = new RequestAllThings(bookName: _bookName,
                                                                xinfoFild: xinfo,
                                                                queriFild: query,
                                                                shardKey: shardKey);

            string book = requestBook.GetResponce();

            return book;
        } 

        private async Task<(string query, string shardKey)> ParseQueryAndSharedKeyAsync() => 
            await Task.Run(() => ParseQueryAndSharedKey());

        private (string query, string shardKey) ParseQueryAndSharedKey()
        {
            string responceBody = RequestQueryAndSharedKeyFild.GetResponce(_bookName);

            JObject jObject = JObject.Parse(responceBody);
            
            string query = jObject["query"].ToString();
            string shardKey = jObject["shardKey"].ToString();

            return (query, shardKey);
        }

        private async Task<string> ParseXinfoAsync() => await Task.Run(() => ParseXinfo());

        private string ParseXinfo()
        {
            string responceBody = RequestXinfoFild.GetResponce(_bookName);

            JObject jObject = JObject.Parse(responceBody);
            string xInfo = jObject["xinfo"].ToString();

            return xInfo;
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
