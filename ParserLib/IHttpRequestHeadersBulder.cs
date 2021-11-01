using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Proxy
{
    interface IHttpRequestHeadersBulder
    {
        HttpHeaders Releace();
        void Reset();
        void AddHost(string URI);
        void AddReferer(string URI);
        void AddConnection(string connection);
        void AddUserAgent(string userAgent);
        void AddOrigin(string URI);
        void Add(string key, string value);
        void AddAcceptEncoding(string acceptEncoding);
        void AddAcceptLanguage(string language);
        void AddCookies(string cookies);
    }
}