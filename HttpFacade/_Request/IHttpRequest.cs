using System;
using System.Threading.Tasks;

namespace HttpFacade
{
    public interface IHttpRequest : IDisposable
    {
        IHttpResponce Request();

        Task<IHttpResponce> RequestAsync();

        string RequestAsString();

        Task<string> RequestAsStringAsync();
    }
}