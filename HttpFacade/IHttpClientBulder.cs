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
        IHttpClientBulder AddHeaderHost(string host);
        IHttpClientBulder AddHeaderReferer(string referer);
        IHttpClientBulder AddHeaderConnection(string connection);
        IHttpClientBulder AddHeaderUserAgent(string userAgent);
        IHttpClientBulder AddHeaderUserAgent(string userAgent, string version);
        IHttpClientBulder AddHeaderOrigin(string origin);
        IHttpClientBulder AddHeaderAcceptEncoding(string acceptEncoding);
        IHttpClientBulder AddHeaderAcceptEncoding(string acceptEncoding, double quality);
        IHttpClientBulder AddHeaderAcceptLanguage(string language);
        IHttpClientBulder AddHeaderAcceptLanguage(string language, double quality);
        IHttpClientBulder AddHeaderCookies(string cookies);
    }
}