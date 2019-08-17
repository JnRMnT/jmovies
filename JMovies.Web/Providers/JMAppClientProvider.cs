using JM.Entities.Framework;
using JMovies.Common.Constants;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Web.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Web.Providers
{
    public class JMAppClientProvider : IJMAppClientProvider
    {
        private HttpClient httpClient;
        private WebConfiguration configuration;
        public IContextProvider ContextProvider
        {
            get;
            set;
        }

        public JMAppClientProvider(IContextProvider contextProvider, IOptions<WebConfiguration> configuration)
        {
            this.ContextProvider = contextProvider;
            this.httpClient = new HttpClient();
            this.configuration = configuration.Value;
        }

        public ResponsePayload CallAction(string actionName, BaseRequest request, Context context)
        {
            RequestPayload requestPayload = new RequestPayload();
            requestPayload.Context = context;
            requestPayload.Request = request;
            requestPayload.Action = actionName;

            string requestJson = requestPayload.ToJson();

            HttpContent httpContent = new StringContent(requestJson, UnicodeEncoding.UTF8, "application/json");
            string jsonResponse = Task.Run(async () =>
             {
                 HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(configuration.AppEndpoint, httpContent);
                 httpResponseMessage.EnsureSuccessStatusCode();
                 return await httpResponseMessage.Content.ReadAsStringAsync();
             }).Result;

            return jsonResponse.FromJsonObject<ResponsePayload>();
        }

        public T CallAction<T>(string actionName, BaseRequest request, Context context) where T : BaseResponse
        {
            ResponsePayload response = CallAction(actionName, request, context);
            if (response != null)
            {
                ContextProvider.SetContext(response.Context);
                if (response.Context.ActiveResult?.IsSuccess == true)
                {
                    return (T)response.Response;
                }
                else
                {
                    throw new JMException(response.Context.ActiveResult.Errors[0].Code, response.Context.ActiveResult.Errors[0].Message);
                }
            }
            else
            {
                return default(T);
            }
        }

        public T CallAction<T>(string actionName, BaseRequest request) where T : BaseResponse
        {
            return CallAction<T>(actionName, request, ContextProvider.GetContext());
        }
    }
}
