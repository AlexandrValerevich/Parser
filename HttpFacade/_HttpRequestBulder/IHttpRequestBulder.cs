using System;

namespace HttpFacade
{
    public interface IHttpRequestBulder
    {
        void Reset();
        IHttpRequest Build();
        IHttpRequestBulder AddUri(string uri);
        IHttpRequestBulder AddUri(Uri uri);
        IHttpRequestBulder AddHeader(string key, string value);
        IHttpRequestBulder AddHeaderHost(string host);
        IHttpRequestBulder AddHeaderReferer(string referer);
        IHttpRequestBulder AddHeaderConnection(string connection);
        IHttpRequestBulder AddHeaderUserAgent(string userAgent);
        IHttpRequestBulder AddHeaderUserAgent(string userAgent, string version);
        IHttpRequestBulder AddHeaderOrigin(string origin);
        IHttpRequestBulder AddHeaderAccept(string accept);
        IHttpRequestBulder AddHeaderAccept(string accept, double quality);
        IHttpRequestBulder AddHeaderAcceptEncoding(string acceptEncoding);
        IHttpRequestBulder AddHeaderAcceptEncoding(string acceptEncoding, double quality);
        IHttpRequestBulder AddHeaderAcceptLanguage(string language);
        IHttpRequestBulder AddHeaderAcceptLanguage(string language, double quality);
        IHttpRequestBulder AddHeaderCookies(string cookies);
    }
}