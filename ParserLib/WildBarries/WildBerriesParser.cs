using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using HttpFacade;

namespace Parser.WildBarries
{
    public class WildBerriesParser : IParserBook
    {
        private string _bookName;
        private List<BookInfo> _bookInfo;
        private string _imageUriPrefix => "https://kemlenvg8e.a.trbcdn.net/c516x688/new/";
        private string _siteUriPrefix => "https://by.wildberries.ru/catalog/";


        public WildBerriesParser(string bookName)
        {
            _bookName = bookName.Replace(" ", "+");
            _bookInfo = new List<BookInfo>();
        }
        
        public BookInfo[] GetResult()
        {
            return _bookInfo.ToArray();
        }

        public void Parse()
        {
            string allThingInJson = GetAllThingJsonFormat();
            _bookInfo = FilterOnlyBook(allThingInJson);
        }

        private List<BookInfo> FilterOnlyBook(string allThingInJson)
        {
            JArray products = JObject.Parse(allThingInJson)["data"]["products"] as JArray;

            var productOfBook = products
            .Where(x => (int)x["subjectId"] == 381)
            .Select(x => new BookInfo(){
                id = (int)x["id"],
                root = (int)x["root"], 
                name = (string)x["name"],
                brand = (string)x["brand"],
                price = (int)x["salePriceU"]/100, // убираем 2 нуля
                uriSite = _siteUriPrefix + (string)x["id"] + "/detail.aspx?targetUrl=XS",
                uriImage = _imageUriPrefix + ((int)x["id"]/10000 * 10000) + "/" + (string)x["id"] + "-1.jpg" 
            }).ToList();

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
