using System;
using System.Net.Http;

namespace HttpFacade
{
    interface IHttpClientBulder
    {
        void Reset();
        HttpClient Build();
        IHttpClientBulder AddUri(string uri);
        IHttpClientBulder AddUri(Uri uri);
        IHttpClientBulder AddHeader(string key, string value);
        IHttpClientBulder AddHeaderHost(string URI);
        IHttpClientBulder AddHeaderReferer(string URI);
        IHttpClientBulder AddHeaderConnection(string connection);
        IHttpClientBulder AddHeaderUserAgent(string userAgent);
        IHttpClientBulder AddHeaderOrigin(string URI);
        IHttpClientBulder AddHeaderAcceptEncoding(string acceptEncoding);
        IHttpClientBulder AddHeaderAcceptLanguage(string language);
        IHttpClientBulder AddHeaderCookies(string cookies);
    }
}