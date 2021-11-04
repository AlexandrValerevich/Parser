using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
        private string _SearchUri => "https://by.wildberries.ru/catalog/0/search.aspx?search=" + _bookName;

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
            string catalogOfBook = GetBookInJsonFormat();

            Console.WriteLine(catalogOfBook);
        }

        private string GetBookInJsonFormat()
        {
            (string query, string shardKey) = ParceQuertAndSharedKeyString();
            string xinfo = ParceXinfoString();

            RequestBook requestBook = new RequestBook(_bookName, xinfo, query, shardKey);

            string book = requestBook.GetResponceBody();

            return book;
        }



        private (string query, string shardKey) ParceQuertAndSharedKeyString()
        {
            RequestQueryAndSharedKeyFild requstQueryFild = new RequestQueryAndSharedKeyFild(_bookName);
            string responceBody = requstQueryFild.GetResponceBody();

            Console.WriteLine(responceBody);
            JObject jObject = new JObject(responceBody);
            
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
