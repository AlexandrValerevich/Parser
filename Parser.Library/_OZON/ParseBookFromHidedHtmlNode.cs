using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using static Parser.Json.JsonWorker;
using Newtonsoft.Json.Linq;

#nullable enable

namespace Parser.Ozon
{
    static class ParseBookFromHidedHtmlNode
    {
        private static readonly string s_prefixUri = "https://www.ozon.ru";

        public static async Task<IEnumerable<BookInfo>> ParseBookAsync(HtmlDocument doc) =>
            await Task<IEnumerable<BookInfo>>.Run(() => ParseBook(doc));

        public static IEnumerable<BookInfo> ParseBook(HtmlDocument doc)
        {
            string json = ParseJsonWithBook(doc);
            
            json.TryParseToJObject(out JObject? jObject);
            JArray? books = jObject?["items"] as JArray;

            var convertedToBookInfo = books?
            .Select(book => book as JObject)
            .Select(book => new BookInfo()
                            {
                                UriSite = s_prefixUri + book?["action"]?["link"]?.ToString() ?? "",
                                UriImage = GetImageUriFromBook(book),
                                Name = GetNameFromMainState(book?["mainState"]),
                                Price = GetPriceFromMainState(book?["mainState"]),
                                Currency = "BYN"
                            }) ?? new List<BookInfo>();

            return convertedToBookInfo;
        }
        
        private static string ParseJsonWithBook(HtmlDocument doc)
        {
            HtmlNode divWithJsonInAttribute = doc.DocumentNode.QuerySelector("#state-searchResultsV2-311201-default-1");
            string jsonWithBook = divWithJsonInAttribute?.Attributes?["data-state"]?.Value ?? "";

            return jsonWithBook;
        }

        private static string GetNameFromMainState(JToken? mainState)
        {
            string name = mainState?.Where(obj => obj?["id"]?.ToString() == "name")
                                    ?.Select(obj => obj?["atom"]?["textAtom"]?["text"]?.ToString())
                                    ?.ElementAt(0) ?? "";
            return name;
        }
        
        private static decimal GetPriceFromMainState(JToken? mainState)
        {
            string priceWithCurrency = mainState?.Where(obj => obj?["atom"]?["type"]?.ToString() == "price")
                                    ?.Select(obj => obj?["atom"]?["price"]?["price"]?.ToString())
                                    ?.ElementAt(0) ?? "";

            string priceNum = priceWithCurrency
                            .Split()[0]
                            .Replace(",", ".");

            decimal convertedPrice = Decimal.Parse(priceNum);

            return convertedPrice;
        }
        
        private static string GetImageUriFromBook(JObject? book)
        {
            JArray? imagesBook = book?["tileImage"]?["images"] as JArray;
            JValue? firstImageBook = imagesBook?.Values()?.ElementAt(0) as JValue;
            string imageUri = firstImageBook?.Value?.ToString() ?? "";

            return imageUri;
        }

    }
}