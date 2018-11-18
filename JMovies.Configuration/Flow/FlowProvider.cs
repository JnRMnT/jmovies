using JMovies.Common.Constants;
using JMovies.Configuration.FlowItems;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using JMovies.Utilities.Configuration;
using JMovies.Utilities.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Configuration.Flow
{
    public class FlowProvider
    {
        private static bool Initialized;
        private static FlowProvider current;
        public static FlowProvider Current
        {
            get
            {
                if (!Initialized && current == null)
                {
                    current = new FlowProvider();
                }

                lock (current)
                {
                    current.Initialize();
                    Initialized = true;
                }
                return current;
            }
        }

        internal void Initialize()
        {
            IFlowConfigurationProvider flowConfigurationProvider = SingletonUnity.Resolve<IFlowConfigurationProvider>();
            flowConfigurationProvider.Initialize();
            IFlowExecutionConfigurationProvider flowExecutionConfigurationProvider = SingletonUnity.Resolve<IFlowExecutionConfigurationProvider>();
            flowExecutionConfigurationProvider.Initialize();
        }

        public BaseResponse ExecuteFlow(string actionName, BaseRequest request)
        {
            IFlowExecutionConfigurationProvider flowExecutionConfigurationProvider = SingletonUnity.Resolve<IFlowExecutionConfigurationProvider>();
            IFlowConfigurationProvider flowConfigurationProvider = SingletonUnity.Resolve<IFlowConfigurationProvider>();
            IContextProvider contextProvider = SingletonUnity.Resolve<IContextProvider>();
            FlowConfiguration flowConfiguration = flowConfigurationProvider.GetConfiguration(actionName);
            contextProvider.GetContext().ActiveFlowConfiguration = flowConfiguration;
            Type responseType = Type.GetType(flowConfiguration.ResponseIdentifier);
            BaseResponse response = Activator.CreateInstance(responseType) as BaseResponse;

            foreach (FlowItemDefinition flowItem in flowExecutionConfigurationProvider.GetActiveConfiguration().FlowItems)
            {
                Type flowItemType = Type.GetType(flowItem.TypeIdentifier);
                BaseFlowItem flowItemInstance = Activator.CreateInstance(flowItemType) as BaseFlowItem;
                flowItemInstance.ExecuteFlow(ref request, ref response);
            }

            return response;
        }
    }
}