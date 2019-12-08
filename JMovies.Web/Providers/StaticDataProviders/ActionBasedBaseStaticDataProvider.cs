using JMovies.Common.Constants;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;

namespace JMovies.Web.Providers.StaticDataProviders
{
    public class ActionBasedBaseStaticDataProvider<T>
    {
        protected IJMAppClientProvider jmAppClientProvider;

        public ActionBasedBaseStaticDataProvider(IJMAppClientProvider jmAppClientProvider)
        {
            this.jmAppClientProvider = jmAppClientProvider;
        }
        
        public void Initialize()
        {
            StaticDataManagementRequest request = new StaticDataManagementRequest();
            request.StaticDataProviderType = typeof(T);
            StaticDataManagementResponse response = jmAppClientProvider.CallAction<StaticDataManagementResponse>(ActionNameConstants.StaticDataManagement, request);
            HandleStaticDataResponse(response);
        }

        protected virtual void HandleStaticDataResponse(StaticDataManagementResponse response)
        {

        }
    }
}
