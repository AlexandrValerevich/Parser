using System;

namespace HttpFacade
{
    public interface IHttpRequest
    {
        void Request();
        IHttpResponce GetResponce();
        
    }
}