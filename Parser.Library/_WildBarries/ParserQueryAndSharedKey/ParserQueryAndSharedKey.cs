using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Parser.WildBarries
{
    static class QueryAndSharedKey
    {
        public static async Task<(string query, string shardKey)> ParseQueryAndSharedKeyAsync(string bookName) =>
            await Task.Run(() => ParseQueryAndSharedKey(bookName));

        public static (string query, string shardKey) ParseQueryAndSharedKey(string bookName)
        {
            string responceBody = RequestQueryAndSharedKeyFild.GetResponce(bookName);

            JObject jObject = JObject.Parse(responceBody);

            string query = jObject["query"].ToString();
            string shardKey = jObject["shardKey"].ToString();

            return (query, shardKey);
        }
    }
}