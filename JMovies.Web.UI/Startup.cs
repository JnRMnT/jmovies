using JM.Entities.Interfaces;
using JMovies.Common.Constants;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.IMDb.Entities.Interfaces;
using JMovies.Utilities.Cryptography;
using JMovies.Utilities.Providers;
using JMovies.Web.Configuration;
using JMovies.Web.Middlewares;
using JMovies.Web.Providers;
using JMovies.Web.Providers.StaticDataProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;

namespace JMovies
{
    public class Startup
    {
        private const string enUSCulture = "en-US";
        private const string trTRCulture = "tr-TR";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddMvc(option =>
            {
                option.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.TypeNameHandling = Utilities.Serialization.JsonSerializer.Settings.TypeNameHandling;
                    options.SerializerSettings.ReferenceLoopHandling = Utilities.Serialization.JsonSerializer.Settings.ReferenceLoopHandling;
                }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
            });
            services.AddDataProtection();
            // Add our Config object so it can be injected
            services.Configure<WebConfiguration>(Configuration.GetSection(ConfigurationConstants.CustomConfigurationSectionName));
            services.Configure<CustomConfiguration>(Configuration.GetSection(ConfigurationConstants.CustomConfigurationSectionName));
            services.AddOptions();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
            }).AddAuthorization();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            byte[] signKey = RandomHelper.GenerateRandom(256 / 8);
            byte[] encryptionKey = RandomHelper.GenerateRandom(256 / 8);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddCookie(options =>
           {
               options.SlidingExpiration = true;
               options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
               options.LoginPath = "/login";
               options.AccessDeniedPath = "/forbidden";
           })
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   IssuerSigningKey = new SymmetricSecurityKey(signKey),
                   RequireSignedTokens = false,
                   TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey),
                   ValidAudience = "jmovier",
                   ValidIssuer = "jmovies",
                   ValidateLifetime = true
               };
           });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                new CultureInfo(enUSCulture),
                new CultureInfo(trTRCulture)
                };

                options.DefaultRequestCulture = new RequestCulture(culture: enUSCulture, uiCulture: enUSCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IContextProvider, SessionBasedContextProvider>();
            services.AddSingleton<IPathProvider, PathProvider>();
            services.AddSingleton<IJMAppClientProvider, JMAppClientProvider>();
            services.AddSingleton<IFlowConfigurationProvider, JsonFileBasedFlowConfigurationProvider>();
            services.AddSingleton<IFlowExecutionConfigurationProvider, JsonFileBasedFlowExecutionConfigurationProvider>();
            services.AddSingleton<IIMDbDataProvider, ActionBasedIMDbDataProvider>();
            services.AddSingleton<IExceptionHandler, ExceptionHandler>();
            services.AddSingleton<IAuthenticationProvider, FlowBasedAuthenticationProvider>();
            services.AddSingleton<ITokenProvider, JWTTokenProvider>((serviceProvider) =>
            {
                return new JWTTokenProvider(signKey, encryptionKey);
            });
            MainStaticDataProvider.RegisterProvider<IResourcesStaticDataProvider, ResourcesStaticDataProvider>(services);
            MainStaticDataProvider.RegisterProvider<IResultConfigurationsStaticDataProvider, ResultConfigurationsStaticDataProvider>(services);
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
           .AddEnvironmentVariables();
            Configuration = builder.Build();
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSpaStaticFiles();
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseMiddleware<ContextInitializerMiddleware>();
            app.UseMiddleware<XsrfMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });
            app.UseRequestLocalization();
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
