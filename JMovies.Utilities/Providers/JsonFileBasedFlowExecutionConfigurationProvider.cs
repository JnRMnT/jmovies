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
    public class JsonFileBasedFlowExecutionConfigurationProvider : IFlowExecutionConfigurationProvider
    {
        private FlowExecutionConfiguration currentConfiguration;
        public FlowExecutionConfiguration GetActiveConfiguration()
        {
            return currentConfiguration;
        }

        public void Initialize()
        {
            IPathProvider pathProvider = SingletonUnity.Resolve<IPathProvider>();
            string configPath = pathProvider.GetCurrentPath() + ConfigReader.Get(ConfigurationConstants.ConfigurationFilesPathConfigKey) + "FlowExecutionConfiguration.json";
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
