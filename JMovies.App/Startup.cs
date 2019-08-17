﻿using System;
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
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Utilities.Providers;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JMovies.App
{
    public class Startup
    {
        private IServiceProvider serviceProvider;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IPathProvider, PathProvider>();
            services.AddSingleton<IContextProvider, ContextProvider>();
            services.AddSingleton<IFlowConfigurationProvider, JsonFileBasedFlowConfigurationProvider>();
            services.AddSingleton<IFlowExecutionConfigurationProvider, JsonFileBasedFlowExecutionConfigurationProvider>();
            services.AddSingleton<IFlowProvider, FlowProvider>();
            // Add our Config object so it can be injected
            services.Configure<AppConfiguration>(Configuration.GetSection(ConfigurationConstants.CustomConfigurationSectionName));
            services.Configure<CustomConfiguration>(Configuration.GetSection(ConfigurationConstants.CustomConfigurationSectionName));
            services.AddOptions();
            MainStaticDataProvider.RegisterProvider<IResourcesStaticDataProvider, ResourcesStaticDataProvider>(services);
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

            app.UseHttpsRedirection();
            app.UseMvc();
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            MainStaticDataProvider.Initialize(serviceProvider);
        }
    }
}
