using JMovies.Entities.Interfaces;
using JMovies.Utilities.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using Unity.Registration;

namespace JMovies.Utilities.Providers
{
    public class MainStaticDataProvider
    {
        private static List<Type> staticDataProviderTypes = new List<Type>();
        public static void RegisterProvider<T, X>() where T : IStaticDataProvider
        {
            staticDataProviderTypes.Add(typeof(T));
            SingletonUnity.ActiveContainer.RegisterType(typeof(T), typeof(X), new ContainerControlledLifetimeManager());
            SingletonUnity.ActiveContainer.RegisterInstance<X>(Activator.CreateInstance<X>());
        }

        public static void Initialize()
        {
            Parallel.ForEach(staticDataProviderTypes, (staticDataProviderType) =>
            {
                IStaticDataProvider staticDataProviderInstance = SingletonUnity.ActiveContainer.Resolve(staticDataProviderType) as IStaticDataProvider;
                staticDataProviderInstance.Initialize();
            });
        }
    }
}
