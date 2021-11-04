using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

namespace HttpFacade
{
    public interface IHttpResponce
    {
        IHttpResponce EnsureSuccessStatusCode();
        Task<string> ReadAsStringAsync();
        string ReadAsString();
        Task<byte[]> ReadAsByteArrayAsync();
        Stream ReadAsStream();
        
    }
}