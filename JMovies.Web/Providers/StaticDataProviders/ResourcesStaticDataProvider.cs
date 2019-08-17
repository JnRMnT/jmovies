using JMovies.Common.Constants;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using System.Collections.Generic;

namespace JMovies.Web.Providers.StaticDataProviders
{
    public class ResourcesStaticDataProvider : IResourcesStaticDataProvider
    {
        private IJMAppClientProvider jmAppClientProvider;
        private IEnumerable<Resource> resources = null;
        public IEnumerable<Resource> GetResources()
        {
            return resources;
        }

        public ResourcesStaticDataProvider(IJMAppClientProvider jmAppClientProvider)
        {
            this.jmAppClientProvider = jmAppClientProvider;
        }

        public object GetData()
        {
            return resources;
        }

        public void Initialize()
        {
            StaticDataManagementRequest request = new StaticDataManagementRequest();
            request.StaticDataProviderType = typeof(IResourcesStaticDataProvider);
            StaticDataManagementResponse response = jmAppClientProvider.CallAction<StaticDataManagementResponse>(ActionNameConstants.StaticDataManagement, request);
            resources = response.StaticData as IEnumerable<Resource>;
        }
    }
}
