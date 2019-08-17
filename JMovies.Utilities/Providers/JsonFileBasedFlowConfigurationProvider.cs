using JM.Entities.Framework;
using JMovies.Common.Constants;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;

namespace JMovies.Utilities.Providers
{
    public class JsonFileBasedFlowConfigurationProvider : IFlowConfigurationProvider
    {
        private IPathProvider pathProvider;
        private CustomConfiguration configuration;
        public JsonFileBasedFlowConfigurationProvider(IPathProvider pathProvider, IOptions<CustomConfiguration> configuration)
        {
            this.pathProvider = pathProvider;
            this.configuration = configuration.Value;
            this.Initialize();
        }

        private Dictionary<string, FlowConfiguration> flowConfigurations;
        public FlowConfiguration GetConfiguration(string name)
        {
            if (flowConfigurations.ContainsKey(name))
            {
                if (flowConfigurations.TryGetValue(name, out FlowConfiguration flowConfiguration))
                {
                    return flowConfiguration;
                }
                else
                {
                    throw new JMException(ErrorConstants.ActionNameNotFound);
                }
            }
            else
            {
                throw new JMException(ErrorConstants.ActionNameNotFound);
            }
        }

        public void Initialize()
        {
            flowConfigurations = new Dictionary<string, FlowConfiguration>();
            string flowConfigsPath = pathProvider.GetCurrentPath() + configuration.ConfigurationFilesPath + "Flows/";
            if (Directory.Exists(flowConfigsPath))
            {
                foreach (string flowConfigPath in Directory.GetFiles(flowConfigsPath, "*.json"))
                {
                    FlowConfiguration flowConfiguration = File.ReadAllText(flowConfigPath).FromJsonObject<FlowConfiguration>();
                    flowConfigurations.Add(flowConfiguration.Name, flowConfiguration);
                }
            }
        }
    }
}
