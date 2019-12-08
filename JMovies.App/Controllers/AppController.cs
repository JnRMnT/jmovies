using System;
using System.Net;
using JM.Entities;
using Microsoft.Extensions.DependencyInjection;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Utilities.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using JM.Entities.Interfaces;

namespace JMovies.App.Controllers
{
    [Route("app/")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private IContextProvider contextProvider;
        private IFlowProvider flowProvider;
        private IServiceProvider serviceProvider;
        public AppController(IContextProvider contextProvider, IFlowProvider flowProvider, IServiceProvider serviceProvider)
        {
            this.contextProvider = contextProvider;
            this.flowProvider = flowProvider;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        public string Get()
        {
            return ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.OK);
        }

        [HttpPost]
        public string Post([FromBody] RequestPayload requestMessage)
        {
            try
            {
                contextProvider.SetContext(requestMessage.Context);
                BaseResponse response = flowProvider.ExecuteFlow(serviceProvider, requestMessage.Action, requestMessage.Request);
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
                    JMResult result = serviceProvider.GetRequiredService<IExceptionHandler>().HandleException(e);
                    Context context = contextProvider.GetContext();
                    context.ActiveResult = result;
                    return new ResponsePayload
                    {
                        Context = context,
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
