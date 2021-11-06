using Newtonsoft.Json;

namespace Parser
{
    public class BookInfoAdapterToJson
    {
        public static string Convert(BookInfo[] bookInfosArray)
        {
            string resultConvert = JsonConvert.SerializeObject(bookInfosArray);
            return resultConvert;
        }
    }
}
