using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Proxy
{
    interface IHttpClientBulder
    {
        HttpClient Releace();
        void AddAddress();
        void AddHeader();
        void AddProxy();
        void Reset();
    }
}