using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using JMovies.Entities;
using JMovies.App;
using JMovies.Entities.Framework;
using JMovies.Utilities.Configuration;
using JMovies.Common.Constants;
using Unity;
using JMovies.Utilities.Unity;

namespace JMovies.App.Proxy
{
    public class JMAppClient
    {
        public IContextProvider ContextProvider
        {
            get;
            internal set;
        }

        public JMAppClient(IContextProvider contextProvider)
        {
            this.ContextProvider = contextProvider;
        }

        public ResponsePayload CallAction(string actionName, BaseRequest request, JMovies.Entities.Context context)
        {
            RequestPayload requestPayload = new RequestPayload();
            requestPayload.Context = context;
            requestPayload.Request = request;

            var basicBinding = new BasicHttpBinding();
            var endPoint = new EndpointAddress(ConfigReader.Get(ConfigurationConstants.AppEndpoint));
            var channelFactory = new ChannelFactory<IService>(basicBinding, endPoint);

            IService client = null;
            try
            {
                client = channelFactory.CreateChannel();
                string response = client.ExecuteAction(actionName, requestPayload.ToJson());
                ((ICommunicationObject)client).Close();
                return response.FromJsonObject<ResponsePayload>();
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
                return null;
            }
        }

        public T CallAction<T>(string actionName, BaseRequest request, JMovies.Entities.Context context) where T : BaseResponse
        {
            ResponsePayload response = CallAction(actionName, request, context);
            if (response != null)
            {
                ContextProvider.SetContext(response.Context);
                return (T)response.Response;
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
