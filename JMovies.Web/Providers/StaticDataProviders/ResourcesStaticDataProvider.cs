using JMovies.App.Proxy;
using JMovies.Common.Constants;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using JMovies.Utilities.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Web.Providers.StaticDataProviders
{
    public class ResourcesStaticDataProvider : IResourcesStaticDataProvider
    {
        private IEnumerable<Resource> resources = null;
        public IEnumerable<Resource> GetResources()
        {
            return resources;
        }

        public object GetData()
        {
            return resources;
        }

        public void Initialize()
        {
            JMAppClient jmAppClient = SingletonUnity.Resolve<JMAppClient>();
            StaticDataManagementRequest request = new StaticDataManagementRequest();
            request.StaticDataProviderType = typeof(IResourcesStaticDataProvider);
            StaticDataManagementResponse response = jmAppClient.CallAction<StaticDataManagementResponse>(ActionNameConstants.StaticDataManagement, request);
            resources = response.StaticData as IEnumerable<Resource>;
        }
    }
}
