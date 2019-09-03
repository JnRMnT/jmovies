using JMovies.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Utilities.Common
{
    public class EnvironmentUtilities
    {
        public static string GetEnvironmentName()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public static bool IsProduction()
        {
            return GetEnvironmentName() == ConfigurationConstants.ProductionEnvironmentName;
        }
    }
}
