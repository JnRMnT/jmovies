using JMovies.Configuration.Flow;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Utilities.ExceptionHandling;
using JMovies.Utilities.Logging;
using JMovies.Utilities.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace JMovies.App
{
    public class Service : IService
    {
        public string ExecuteAction(string action, string request)
        {
            try
            {
                RequestPayload requestMessage = request.FromJsonObject<RequestPayload>();
                IContextProvider contextProvider = SingletonUnity.Resolve<IContextProvider>();
                contextProvider.SetContext(requestMessage.Context);
                BaseResponse response = FlowProvider.Current.ExecuteFlow(action, requestMessage.Request);
                ResponsePayload responseMessage = new ResponsePayload
                {
                    Context = contextProvider.GetContext(),
                    Response = response
                };

                return responseMessage.ToJson();
            }
            catch (Exception e)
            {
                try
                {
                    DefaultLogger.Error(e);
                    ExceptionHandler.HandleException(e);
                    IContextProvider contextProvider = SingletonUnity.Resolve<IContextProvider>();
                    return new ResponsePayload
                    {
                        Context = contextProvider.GetContext(),
                        Response = null
                    }.ToJson();
                }
                catch (Exception innerException)
                {
                    DefaultLogger.Error(innerException);
                    return new ResponsePayload().ToJson();
                }
            }
        }
    }
}
