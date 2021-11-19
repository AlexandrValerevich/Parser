using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Parser.WildBarries
{
    static class Xinfo
    {
        static public async Task<string> ParseXinfoAsync(string bookName) => await Task<string>.Run(() => ParseXinfo(bookName));

        static public string ParseXinfo(string bookName)
        {
            string responceBody = RequestXinfoFild.GetResponce(bookName);

            JObject jObject = JObject.Parse(responceBody);
            string xInfo = jObject["xinfo"].ToString();

            return xInfo;
        }
    }
}