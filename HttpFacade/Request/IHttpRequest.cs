using System;
using System.Threading.Tasks;

namespace HttpFacade
{
    public interface IHttpRequest
    {
        IHttpResponce Request();

        Task<IHttpResponce> RequestAsync();
    }
}