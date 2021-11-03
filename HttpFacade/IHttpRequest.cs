using System;
using System.Threading.Tasks;

namespace HttpFacade
{
    public interface IHttpRequest
    {
        Task<IHttpResponce> Request();
    }
}