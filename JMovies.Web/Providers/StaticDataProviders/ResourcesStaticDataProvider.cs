using JMovies.Common.Constants;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using System.Collections.Generic;

namespace JMovies.Web.Providers.StaticDataProviders
{
    public class ResourcesStaticDataProvider : ActionBasedBaseStaticDataProvider<IResourcesStaticDataProvider>, IResourcesStaticDataProvider
    {
        private IEnumerable<Resource> resources = null;
        public IEnumerable<Resource> GetResources()
        {
            return resources;
        }

        public ResourcesStaticDataProvider(IJMAppClientProvider jmAppClientProvider) : base(jmAppClientProvider)
        {
        }

        public object GetData()
        {
            return resources;
        }

        protected override void HandleStaticDataResponse(StaticDataManagementResponse response)
        {
            resources = response.StaticData as IEnumerable<Resource>;
        }
    }
}
