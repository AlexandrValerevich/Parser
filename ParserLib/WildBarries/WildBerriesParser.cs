using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;

using Parser.WildBarries;


// В самом начале нужно получить preset
// Потом нужно получить xInfo
// Затем уже сам каталог

// В коде есть повторяющиеся элементы.
// Стоит ли их разбивать на меньшие элементы?
// Или это прямая задача парсера и он больше не содержит причин изменений?
// Или из этих трех запросов, что я делаю в парсере, можно сделать 3 класса
// Которые будут реализовать единый интерфейс, и не зависить друг от друга 
// функционально, а только результативно
// Также в коде есть элементы разбивки JSON строки,
// стоит ли с этим производить какие либо изменения?

// preset не всегда созвращеат то что нужно, нужно смотреть на результат query

using HttpFacade;

namespace Parser
{
    public class WildBerriesParser : IParser
    {
        private string _bookName;
        //private string _SearchUri => "https://by.wildberries.ru/catalog/0/search.aspx?search=" + _bookName + "&xsubject=381";

        public WildBerriesParser(string bookName)
        {
            _bookName = bookName.Replace(" ", "+");
        }
        
        public BookInfo GetResult()
        {
            return new BookInfo(); 
        }

        public void Parse()
        {
            string allThingInJson = GetAllThingJsonFormat();
            
            JArray products = JObject.Parse(allThingInJson)["data"]["products"] as JArray;

            var productOfBook = products.Where(x => (int)x["subjectId"] == 381).Select(x => new{
                id = (int)x["id"],
                root = (int)x["root"], 
                name = (string)x["name"],
                brand = (string)x["brand"],
                salePriceU = (int)x["salePriceU"]
            });

            foreach (var item in productOfBook)
            {
                string value = $"id: {item.id}\nroot: {item.root}\nname: {item.name}\nbrand: {item.brand}\nPrice: {item.salePriceU}";
                Console.WriteLine(value);
            }

            //Console.WriteLine(allThingInJson);
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
            RequestQueryAndSharedKeyFild requstQueryFild = new RequestQueryAndSharedKeyFild(_bookName);
            string responceBody = requstQueryFild.GetResponceBody();

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
