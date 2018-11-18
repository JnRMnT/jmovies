using JMovies.Common.Constants;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Utilities.Configuration;
using JMovies.Utilities.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Utilities.Providers
{
    public class JsonFileBasedFlowConfigurationProvider : IFlowConfigurationProvider
    {
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
            IPathProvider pathProvider = SingletonUnity.Resolve<IPathProvider>();
            flowConfigurations = new Dictionary<string, FlowConfiguration>();
            string flowConfigsPath = pathProvider.GetCurrentPath() + ConfigReader.Get(ConfigurationConstants.ConfigurationFilesPathConfigKey) + "Flows/";
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
