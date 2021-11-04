﻿using System;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Parser;

namespace Parser.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IParser wbParser = new WildBerriesParser("Бизнес требования");

            //HttpClient.DefaultProxy = new WebProxy("127.0.0.1", 8888);
            wbParser.Parse();

        }
    }
}
