using JM.Entities.Framework;
using JMovies.Common.Constants;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace JMovies.Utilities.Providers
{
    public class JsonFileBasedFlowExecutionConfigurationProvider : IFlowExecutionConfigurationProvider
    {
        private IPathProvider pathProvider;
        private FlowExecutionConfiguration currentConfiguration;
        private CustomConfiguration configuration;

        public JsonFileBasedFlowExecutionConfigurationProvider(IPathProvider pathProvider, IOptions<CustomConfiguration> configuration)
        {
            this.pathProvider = pathProvider;
            this.configuration = configuration.Value;
            this.Initialize();
        }

        public FlowExecutionConfiguration GetActiveConfiguration()
        {
            return currentConfiguration;
        }

        public void Initialize()
        {
            string configPath = pathProvider.GetCurrentPath() + configuration.ConfigurationFilesPath + "FlowExecutionConfiguration.json";
            if (File.Exists(configPath))
            {
                currentConfiguration = File.ReadAllText(configPath).FromJsonObject<FlowExecutionConfiguration>();
            }
            else
            {
                throw new JMException(ErrorConstants.FlowExecutionConfigurationNotFound);
            }
        }
    }
}
