using JMovies.Entities.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Interfaces
{
    public interface IJMAppClientProvider
    {
        ResponsePayload CallAction(string actionName, BaseRequest request, JMovies.Entities.Context context);
        T CallAction<T>(string actionName, BaseRequest request, JMovies.Entities.Context context) where T : BaseResponse;
        T CallAction<T>(string actionName, BaseRequest request) where T : BaseResponse;
    }
}
