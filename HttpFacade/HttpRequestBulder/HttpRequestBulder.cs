using System;
using System.Net.Http;

namespace HttpFacade
{
    public class HttpRequestBuilder : IHttpRequestBulder
    {
        public static HttpRequestBuilder Create()
        {
            return new HttpRequestBuilder();
        }

        private bool _usePost;
        private IHttpRequest _httpRequest;
        private HttpClientBulder _httpClientBulder;

        private HttpRequestBuilder()
        {
            _httpClientBulder = HttpClientBulder.Create();
            _usePost = false;
        }

        public IHttpRequestBulder AddHeader(string key, string value)
        {
            _httpClientBulder.AddHeader(key, value);
            return this;
        }

        public IHttpRequestBulder AddHeaderAccept(string accept)
        {
            _httpClientBulder.AddHeaderAccept(accept);
            return this;
        }

        public IHttpRequestBulder AddHeaderAccept(string accept, double quality)
        {
            _httpClientBulder.AddHeaderAccept(accept, quality);
            return this;
        }

        public IHttpRequestBulder AddHeaderAcceptEncoding(string acceptEncoding)
        {
            _httpClientBulder.AddHeaderAcceptEncoding(acceptEncoding);
            return this;
        }

        public IHttpRequestBulder AddHeaderAcceptEncoding(string acceptEncoding, double quality)
        {
            _httpClientBulder.AddHeaderAcceptEncoding(acceptEncoding,quality);
            return this;
        }

        public IHttpRequestBulder AddHeaderAcceptLanguage(string language)
        {
            _httpClientBulder.AddHeaderAcceptLanguage(language);
            return this;
        }

        public IHttpRequestBulder AddHeaderAcceptLanguage(string language, double quality)
        {
            _httpClientBulder.AddHeaderAcceptLanguage(language, quality);
            return this;
        }

        public IHttpRequestBulder AddHeaderConnection(string connection)
        {
            _httpClientBulder.AddHeaderConnection(connection);
            return this;
        }

        public IHttpRequestBulder AddHeaderCookies(string cookies)
        {
            _httpClientBulder.AddHeaderCookies(cookies);
            return this;
        }

        public IHttpRequestBulder AddHeaderHost(string host)
        {
            _httpClientBulder.AddHeaderHost(host);
            return this;
        }

        public IHttpRequestBulder AddHeaderOrigin(string origin)
        {
            _httpClientBulder.AddHeaderOrigin(origin);
            return this;
        }

        public IHttpRequestBulder AddHeaderReferer(string referer)
        {
            _httpClientBulder.AddHeaderReferer(referer);
            return this;
        }

        public IHttpRequestBulder AddHeaderUserAgent(string userAgent)
        {
            _httpClientBulder.AddHeaderUserAgent(userAgent);
            return this;
        }

        public IHttpRequestBulder AddHeaderUserAgent(string userAgent, string version)
        {
            _httpClientBulder.AddHeaderUserAgent(userAgent, version);
            return this;
        }

        public IHttpRequestBulder AddUri(string uri)
        {
            _httpClientBulder.AddUri(uri);
            return this;
        }

        public IHttpRequestBulder AddUri(Uri uri)
        {
            _httpClientBulder.AddUri(uri);
            return this;
        }

        public IHttpRequest Build()
        {  
            HttpClient httpClient = _httpClientBulder.Build();
            IHttpRequest httpRequest = _usePost ? new HttpRequestPost(httpClient) : new HttpRequestGet(httpClient);
            
            return httpRequest;
        }

        public void Reset()
        {
            _httpClientBulder.Reset();
            _usePost = false;
            return;
        }

        public IHttpRequestBulder UsePost()
        {
            _usePost = true;
            return this;
        }
    }
}