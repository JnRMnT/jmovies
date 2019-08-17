﻿using JMovies.Configuration.FlowItems;
using JMovies.Entities;
using JMovies.Entities.Framework;
using JMovies.Entities.Interfaces;
using Newtonsoft.Json.Linq;
using System;

namespace JMovies.Configuration.Flow
{
    public class FlowProvider: IFlowProvider
    {
        private IFlowConfigurationProvider flowConfigurationProvider;
        private IFlowExecutionConfigurationProvider flowExecutionConfigurationProvider;
        private IContextProvider contextProvider;
        private IServiceProvider serviceProvider;

        public FlowProvider(IFlowConfigurationProvider flowConfigurationProvider, IFlowExecutionConfigurationProvider flowExecutionConfigurationProvider, IContextProvider contextProvider, IServiceProvider serviceProvider)
        {
            this.flowConfigurationProvider = flowConfigurationProvider;
            this.flowExecutionConfigurationProvider = flowExecutionConfigurationProvider;
            this.contextProvider = contextProvider;
            this.serviceProvider = serviceProvider;
        }

        internal void Initialize()
        {
            flowConfigurationProvider.Initialize();
            flowExecutionConfigurationProvider.Initialize();
        }

        public BaseResponse ExecuteFlow(string actionName, object request)
        {
            FlowConfiguration flowConfiguration = flowConfigurationProvider.GetConfiguration(actionName);
            contextProvider.GetContext().ActiveFlowConfiguration = flowConfiguration;
            Type requestsType = Type.GetType(flowConfiguration.RequestIdentifier);
            JObject jRequest = request as JObject;
            object flowRequest = jRequest.ToObject(requestsType);

            Type responseType = Type.GetType(flowConfiguration.ResponseIdentifier);
            BaseResponse response = Activator.CreateInstance(responseType) as BaseResponse;

            foreach (FlowItemDefinition flowItem in flowExecutionConfigurationProvider.GetActiveConfiguration().FlowItems)
            {
                Type flowItemType = Type.GetType(flowItem.TypeIdentifier);
                BaseFlowItem flowItemInstance = Activator.CreateInstance(flowItemType) as BaseFlowItem;
                flowItemInstance.ExecuteFlow(serviceProvider, ref flowRequest, ref response);
            }

            return response;
        }
    }
}