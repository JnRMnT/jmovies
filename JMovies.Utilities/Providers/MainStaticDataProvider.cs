using JMovies.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JMovies.Utilities.Providers
{
    public class MainStaticDataProvider
    {
        private static List<Type> staticDataProviderTypes = new List<Type>();
        public static void RegisterProvider<TService, TImplementation>(IServiceCollection services) where TImplementation : class, TService
        {
            staticDataProviderTypes.Add(typeof(TService));
            services.AddSingleton(typeof(TService), typeof(TImplementation));
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            Parallel.ForEach(staticDataProviderTypes, (staticDataProviderType) =>
            {
                IStaticDataProvider staticDataProviderInstance = serviceProvider.GetRequiredService(staticDataProviderType) as IStaticDataProvider;
                staticDataProviderInstance.Initialize();
            });
        }
    }
}
