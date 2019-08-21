using JMovies.Common.Constants;
using JMovies.Entities.Framework;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.IMDb.Providers;
using JMovies.Web.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Tests.Helpers
{
    public class DIHelper
    {
        public static ServiceProvider Initialize()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IIMDbDataProvider, IMDbScraperDataProvider>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
