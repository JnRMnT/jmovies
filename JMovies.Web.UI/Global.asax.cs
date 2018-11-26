using JMovies.Entities;
using JMovies.Entities.Interfaces;
using JMovies.IMDb.Providers;
using JMovies.Utilities.Providers;
using JMovies.Utilities.Unity;
using JMovies.Web.Providers;
using JMovies.Web.Providers.StaticDataProviders;
using log4net.Config;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace JMovies.Web.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SingletonUnity.ActiveContainer.RegisterType<IContextProvider, SessionBasedContextProvider>();
            SingletonUnity.ActiveContainer.RegisterType<IPathProvider, PathProvider>();
            SingletonUnity.ActiveContainer.RegisterSingleton<IFlowConfigurationProvider, JsonFileBasedFlowConfigurationProvider>();
            SingletonUnity.ActiveContainer.RegisterSingleton<IFlowExecutionConfigurationProvider, JsonFileBasedFlowExecutionConfigurationProvider>();
            SingletonUnity.ActiveContainer.RegisterType<IIMDbDataProvider, IMDbScraperDataProvider>(new HierarchicalLifetimeManager());
            MainStaticDataProvider.RegisterProvider<IResourcesStaticDataProvider, ResourcesStaticDataProvider>();
            IContextProvider contextProvider = SingletonUnity.ActiveContainer.Resolve<IContextProvider>();

            UnityDependencyResolver unityDependencyResolver = new UnityDependencyResolver(SingletonUnity.ActiveContainer);
            GlobalConfiguration.Configuration.DependencyResolver = unityDependencyResolver;

            //Set Temporary Context
            contextProvider.SetContext(new Entities.Context());
            MainStaticDataProvider.Initialize();
        }
    }
}
