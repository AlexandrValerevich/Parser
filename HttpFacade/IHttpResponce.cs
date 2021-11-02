using System;
using System.Net.Http;

namespace HttpFacade
{
    interface IHttpResponce
    {
        IHttpResponce EnsureSuccessStatusCode();
        string ReadAsStringAsync();
        byte[] ReadAsByteArrayAsync();
        Stream ReadAsStream();
        
    }
}