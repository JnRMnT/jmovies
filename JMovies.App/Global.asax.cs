using Cosmonaut.ApplicationInsights;
using JMovies.App.Business.Context;
using JMovies.App.Business.Providers;
using JMovies.Entities;
using JMovies.Entities.Interfaces;
using JMovies.Utilities.Providers;
using JMovies.Utilities.Unity;
using log4net.Config;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace JMovies.App
{
    public class JMBusinessApplication : HttpApplication
    {
        /// <summary>
        /// Handles the global application startup event.
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            XmlConfigurator.Configure();
            SingletonUnity.ActiveContainer.RegisterType<IContextProvider, ContextProvider>();
            SingletonUnity.ActiveContainer.RegisterType<IPathProvider, PathProvider>();
            SingletonUnity.ActiveContainer.RegisterSingleton<IFlowConfigurationProvider, JsonFileBasedFlowConfigurationProvider>();
            SingletonUnity.ActiveContainer.RegisterSingleton<IFlowExecutionConfigurationProvider, JsonFileBasedFlowExecutionConfigurationProvider>();
            AppInsightsTelemetryModule.Instance.Initialize(new TelemetryConfiguration("766ad6f7-1622-4a65-9bda-c3c75458aa4a"));
        }
    }
}