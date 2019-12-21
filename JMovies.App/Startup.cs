using JM.Entities.Interfaces;
using JMovies.App.Business.Configuration;
using JMovies.App.Business.Context;
using JMovies.App.Business.Providers;
using JMovies.App.Business.Providers.StaticData;
using JMovies.App.ElasticSearch;
using JMovies.App.ElasticSearch.Interfaces;
using JMovies.Common.Constants;
using JMovies.Configuration.Flow;
using JMovies.DataAccess;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.Utilities.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace JMovies.App
{
    public class Startup
    {
        private const string enUSCulture = "en-US";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.TypeNameHandling = Utilities.Serialization.JsonSerializer.Settings.TypeNameHandling;
                options.SerializerSettings.ReferenceLoopHandling = Utilities.Serialization.JsonSerializer.Settings.ReferenceLoopHandling;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                new CultureInfo(enUSCulture),
                new CultureInfo("tr-TR")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: enUSCulture, uiCulture: enUSCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IPathProvider, PathProvider>();
            services.AddSingleton<IContextProvider, ContextProvider>();
            services.AddSingleton<IFlowConfigurationProvider, JsonFileBasedFlowConfigurationProvider>();
            services.AddSingleton<IFlowExecutionConfigurationProvider, JsonFileBasedFlowExecutionConfigurationProvider>();
            services.AddSingleton<IFlowProvider, FlowProvider>();
            services.AddSingleton<IIMDbDataProvider, DBBasedIMDbDataProvider>();
            services.AddSingleton<IExceptionHandler, ExceptionHandler>();
            services.AddSingleton<IElasticSearchConnectionProvider, ElasticSearchConnector>();
            // Add our Config object so it can be injected
            services.Configure<AppConfiguration>(Configuration.GetSection(ConfigurationConstants.CustomConfigurationSectionName));
            services.Configure<CustomConfiguration>(Configuration.GetSection(ConfigurationConstants.CustomConfigurationSectionName));
            services.AddOptions();
            MainStaticDataProvider.RegisterProvider<IResourcesStaticDataProvider, ResourcesStaticDataProvider>(services);
            MainStaticDataProvider.RegisterProvider<IResultConfigurationsStaticDataProvider, ResultConfigurationsStaticDataProvider>(services);
            services.AddDbContext<JMoviesEntities>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
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
            app.UseIPRestriction();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseResponseCompression();
            app.UseRequestLocalization();
        }
    }
}
