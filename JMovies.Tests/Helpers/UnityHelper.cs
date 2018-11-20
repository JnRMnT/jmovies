using JMovies.App.Proxy;
using JMovies.Entities;
using JMovies.Entities.Interfaces;
using JMovies.IMDb.Providers;
using JMovies.Tests.Providers;
using JMovies.Utilities.Providers;
using JMovies.Utilities.Unity;
using log4net.Config;
using Unity;

namespace JMovies.Tests.Helpers
{
    public class UnityHelper
    {
        public static void Initialize()
        {
            XmlConfigurator.Configure();
            SingletonUnity.ActiveContainer.RegisterType<IContextProvider, TemporaryContextProvider>();
            SingletonUnity.ActiveContainer.RegisterType<IPathProvider, PathProvider>();
            SingletonUnity.ActiveContainer.RegisterSingleton<IFlowConfigurationProvider, JsonFileBasedFlowConfigurationProvider>();
            SingletonUnity.ActiveContainer.RegisterSingleton<IIMDbDataProvider, IMDbScraperDataProvider>();
            SingletonUnity.ActiveContainer.RegisterType<JMAppClient>();
        }
    }
}
