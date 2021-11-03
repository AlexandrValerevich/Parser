using System;

namespace HttpFacade
{
    public interface IHttpRequest
    {
        IHttpResponce Request();
    }
}