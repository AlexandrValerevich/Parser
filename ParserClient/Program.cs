using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Console;
using Parser;
using Parser.WildBarries;


namespace Parser.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IParserBook wbParser = new WildBerriesParser("Разработка требований");

            //HttpClient.DefaultProxy = new WebProxy("127.0.0.1", 8888);
            wbParser.Parse();
        }
    }
}
