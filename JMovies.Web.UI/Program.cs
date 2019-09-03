using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JMovies.Entities;
using JMovies.Utilities.Logging;
using JMovies.Utilities.Providers;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JMovies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            DefaultLogger.Info("Application initializing...");
            IWebHost webHost = CreateWebHostBuilder(args).Build();
            IContextProvider contextProvider = webHost.Services.GetRequiredService<IContextProvider>();
            //Set Temporary Context
            contextProvider.SetContext(new Context());
            MainStaticDataProvider.Initialize(webHost.Services);
            DefaultLogger.Info("Application initialized!");
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>();
    }
}
