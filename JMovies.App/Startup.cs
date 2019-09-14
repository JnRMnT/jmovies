using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JMovies.App.Business.Configuration;
using JMovies.App.Business.Context;
using JMovies.App.Business.Providers;
using JMovies.App.Business.Providers.StaticData;
using JMovies.Common.Constants;
using JMovies.Configuration.Flow;
using JMovies.DataAccess;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.Utilities.Providers;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace JMovies.App
{
    public class Startup
    {
        private IServiceProvider serviceProvider;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddApplicationInsightsTelemetry();
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.TypeNameHandling = Utilities.Serialization.JsonSerializer.Settings.TypeNameHandling;
                options.SerializerSettings.ReferenceLoopHandling = Utilities.Serialization.JsonSerializer.Settings.ReferenceLoopHandling;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IPathProvider, PathProvider>();
            services.AddSingleton<IContextProvider, ContextProvider>();
            services.AddSingleton<IFlowConfigurationProvider, JsonFileBasedFlowConfigurationProvider>();
            services.AddSingleton<IFlowExecutionConfigurationProvider, JsonFileBasedFlowExecutionConfigurationProvider>();
            services.AddSingleton<IFlowProvider, FlowProvider>();
            services.AddSingleton<IIMDbDataProvider, DBBasedIMDbDataProvider>();
            // Add our Config object so it can be injected
            services.Configure<AppConfiguration>(Configuration.GetSection(ConfigurationConstants.CustomConfigurationSectionName));
            services.Configure<CustomConfiguration>(Configuration.GetSection(ConfigurationConstants.CustomConfigurationSectionName));
            services.AddOptions();
            MainStaticDataProvider.RegisterProvider<IResourcesStaticDataProvider, ResourcesStaticDataProvider>(services);
            services.AddDbContext<JMoviesEntities>();
            serviceProvider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
           .AddEnvironmentVariables();
            Configuration = builder.Build();

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseResponseCompression();
        }
    }
}
