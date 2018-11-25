using JMovies.Entities.Framework;
using JMovies.Utilities.Unity;
using JMovies.Web.Providers.StaticDataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JMovies.Web.UI.Controllers
{
    public class ResourcesController : ApiController
    {
        [HttpGet]
        [Route("api/resources")]
        public IEnumerable<Resource> Get()
        {
            ResourcesStaticDataProvider resourcesStaticDataProvider = SingletonUnity.Resolve<ResourcesStaticDataProvider>();
            return resourcesStaticDataProvider.GetResources();
        }
    }
}