using JM.Entities.Framework;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMovies.Web.UI.Controllers
{
    [Route("api/action")]
    public class ActionController : BaseApiController
    {
        private IJMAppClientProvider jmAppClientProvider;
        private IFlowConfigurationProvider flowConfigurationProvider;

        public ActionController(IJMAppClientProvider jmAppClientProvider, IFlowConfigurationProvider flowConfigurationProvider) : base()
        {
            this.jmAppClientProvider = jmAppClientProvider;
            this.flowConfigurationProvider = flowConfigurationProvider;
        }

        [HttpPost("{actionName}")]
        public BaseResponse Post(string actionName, [FromBody]JToken jsonBody)
        {
            FlowConfiguration flowConfiguration = flowConfigurationProvider.GetConfiguration(actionName);
            if (flowConfiguration == null)
            {
                throw new JMException("FlowConfigurationEmpty");
            }
            BaseRequest request = jsonBody.ToObject(Type.GetType(flowConfiguration.RequestIdentifier)) as BaseRequest;
            return jmAppClientProvider.CallAction<BaseResponse>(actionName, request);
        }
    }
}
