using System.IO;
using System.Threading.Tasks;

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