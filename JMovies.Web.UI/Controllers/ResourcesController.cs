using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JMovies.Web.UI.Controllers
{
    public class ResourcesController : BaseApiController
    {
        private IResourcesStaticDataProvider resourcesStaticDataProvider;
        public ResourcesController(IResourcesStaticDataProvider resourcesStaticDataProvider)
        {
            this.resourcesStaticDataProvider = resourcesStaticDataProvider;
        }

        [HttpGet]
        [Route("api/resources")]
        public IEnumerable<Resource> Get()
        {
            return resourcesStaticDataProvider.GetResources();
        }
    }
}