using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Parser;

namespace Parser.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var wbParser = new WildBerriesParser("Angular 11");

            await wbParser.UseProxy().GetListProductAsync();

        }
    }
}
