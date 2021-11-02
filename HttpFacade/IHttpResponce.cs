using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

namespace HttpFacade
{
    interface IHttpResponce
    {
        IHttpResponce EnsureSuccessStatusCode();
        Task<string> ReadAsStringAsync();
        Task<byte[]> ReadAsByteArrayAsync();
        Stream ReadAsStream();
        
    }
}