using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpFacade
{
    public class HttpClientBulder : IHttpClientBulder, IDisposable
    {
        static HttpClientBulder Create()
        {
            return new HttpClientBulder();
        }

        private HttpClient _httpClient;

        private HttpRequestHeaders _Header => _httpClient.DefaultRequestHeaders;

        private Uri _Uri
        {
            get 
            {
                return  _httpClient.BaseAddress;
            }
            set
            {
                _httpClient.BaseAddress = value;
            }
        }

        private HttpClientBulder()
        {
            _httpClient = new HttpClient();
        }
        
        public IHttpClientBulder AddHeader(string key, string? value)
        {
            _Header.Add(key, value);
            return this;
        }

        public IHttpClientBulder AddHeaderAcceptEncoding(string acceptEncoding)
        {
            _Header.AcceptEncoding.Add(new StringWithQualityHeaderValue(acceptEncoding));
            return this;
        }

        public IHttpClientBulder AddHeaderAcceptEncoding(string acceptEncoding, double quality)
        {
            _Header.AcceptEncoding.Add(new StringWithQualityHeaderValue(acceptEncoding, quality));
            return this;
        }

        public IHttpClientBulder AddHeaderAcceptLanguage(string language)
        {
            _Header.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
            return this;
        }

        public IHttpClientBulder AddHeaderAcceptLanguage(string language, double quality)
        {
            _Header.AcceptLanguage.Add(new StringWithQualityHeaderValue(language,quality));
            return this;
        }

        public IHttpClientBulder AddHeaderConnection(string connection)
        {
            _Header.Connection.Add(connection);
            return this;
        }

        public IHttpClientBulder AddHeaderCookies(string cookies)
        {
            _Header.Add("Cookie", cookies);
            return this;
        }

        public IHttpClientBulder AddHeaderHost(string host)
        {
            _Header.Host = host;
            return this;
        }

        public IHttpClientBulder AddHeaderOrigin(string origin)
        {
            _Header.Add("Origin", origin);
            return this;
        }

        public IHttpClientBulder AddHeaderReferer(string referer)
        {
            _Header.Referrer = new Uri(referer);
            return this;
        }

        public IHttpClientBulder AddHeaderUserAgent(string userAgent)
        {
            _Header.UserAgent.Add(new ProductInfoHeaderValue(userAgent));
            return this;
        }

        public IHttpClientBulder AddHeaderUserAgent(string userAgent, string version)
        {
             _Header.UserAgent.Add(new ProductInfoHeaderValue(userAgent, version));
            return this;
        }

        public IHttpClientBulder AddUri(string uri)
        {
            _Uri = new Uri(uri);
            return this;
        }

        public IHttpClientBulder AddUri(Uri uri)
        {
            _Uri = uri;
            return this;
        }

        public HttpClient Build()
        {
            return _httpClient;
        }

        public void Reset()
        {
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}