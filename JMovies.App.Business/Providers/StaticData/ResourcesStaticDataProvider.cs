using JMovies.DataAccess;
using JMovies.DataAccess.Entities;
using FWEntities = JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace JMovies.App.Business.Providers.StaticData
{
    public class ResourcesStaticDataProvider : IResourcesStaticDataProvider
    {
        private IEnumerable<FWEntities.Resource> resources = null;
        public IEnumerable<FWEntities.Resource> GetResources()
        {
            return resources;
        }

        public ResourcesStaticDataProvider()
        {

        }

        public object GetData()
        {
            return resources;
        }

        public void Initialize()
        {
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                List<FWEntities.Resource> resources = new List<FWEntities.Resource>();
                foreach (Resource resource in entities.Resource.Include(e=> e.Translations))
                {
                    if (resource.Translations != null)
                    {
                        foreach (ResourceTranslation resourceTranslation in resource.Translations)
                        {
                            resources.Add(new FWEntities.Resource
                            {
                                Key = resource.Key,
                                Culture = resourceTranslation.Culture,
                                Value = resourceTranslation.Value
                            });
                        }
                    }
                }
                this.resources = resources.ToArray();
            }
        }
    }
}
