using System;
using System.Net;
using JM.Entities;
using JMovies.Configuration.Flow;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Utilities.ExceptionHandling;
using JMovies.Utilities.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace JMovies.App.Controllers
{
    [Route("app/")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private IContextProvider contextProvider;
        private IFlowProvider flowProvider;
        public AppController(IContextProvider contextProvider, IFlowProvider flowProvider)
        {
            this.contextProvider = contextProvider;
            this.flowProvider = flowProvider;
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
                BaseResponse response = flowProvider.ExecuteFlow(requestMessage.Action, requestMessage.Request);
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
                    JMResult result = ExceptionHandler.HandleException(e);
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
