using System;

namespace HttpFacade
{
    interface IHttpRequest
    {
        void Request();
        IHttpResponce GetResponce();
        
    }
}