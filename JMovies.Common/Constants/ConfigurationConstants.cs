using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Common.Constants
{
    public class ConfigurationConstants
    {
        public static readonly string CustomConfigurationSectionName = "CustomConfigurations";

        public static readonly int PersisterRecordCountPerRun = 500;
        public static readonly long IMDBMaxID = 99999999;

        public static readonly string ProductionEnvironmentName = "Production";
        public static readonly string ConnectionStringEnvironmentName = "JMoviesConnectionString";

        public static readonly int MaxPasswordRetryCount = 3;
    }
}
