using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.App.Business.Providers.StaticData
{
    public class ResourcesStaticDataProvider : IResourcesStaticDataProvider
    {
        private IEnumerable<Resource> resources = null;
        public IEnumerable<Resource> GetResources()
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
            //using (JMoviesEntities entities = new JMoviesEntities())
            //{
                List<Resource> resources = new List<Resource>();
                //foreach (JMResource resource in entities.JMResources)
                //{
                //    if (resource.JMResourceTranslations != null)
                //    {
                //        foreach (JMResourceTranslation resourceTranslation in resource.JMResourceTranslations)
                //        {
                //            resources.Add(new Resource
                //            {
                //                Key = resource.Key,
                //                Culture = resourceTranslation.Culture,
                //                Value = resourceTranslation.Value
                //            });
                //        }
                //    }
                //}
                this.resources = resources.ToArray();
            //}
        }
    }
}
