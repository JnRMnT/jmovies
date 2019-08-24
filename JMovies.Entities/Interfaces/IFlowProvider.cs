using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Interfaces
{
    public interface IFlowProvider
    {
        BaseResponse ExecuteFlow(IServiceProvider serviceProvider, string actionName, object request);
    }
}
