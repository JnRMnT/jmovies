using JMovies.Jobs.Common.Configuration;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Jobs.Common
{
    public class InitializationHelper
    {
        public static void Initialize(BaseJobConfiguration configuration)
        {
            if (configuration.ApplicationInsights != null)
            {
                TelemetryConfiguration telemetryConfiguration = TelemetryConfiguration.CreateDefault();
                telemetryConfiguration.InstrumentationKey = configuration.ApplicationInsights.InstrumentationKey;
                var telemetryClient = new TelemetryClient(telemetryConfiguration);
            }
        }
    }
}
