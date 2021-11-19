using Newtonsoft.Json.Linq;

#nullable enable

namespace Parser.Json
{
    static class JsonWorker
    {
        public static bool TryParseToJObject(this string json, ref JObject? jObject)
        {
            bool isTry = true;
            
            try
            {
                jObject = JObject.Parse(json);
            }
            catch (System.Exception)
            {
                isTry = false;
                jObject = null;
            }

            return isTry;
        }
    }

}