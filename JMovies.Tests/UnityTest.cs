using System;
using JMovies.App.Proxy;
using JMovies.Common.Constants;
using JMovies.Entities;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using JMovies.Tests.Providers;
using JMovies.Utilities.Providers;
using JMovies.Utilities.Unity;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Lifetime;

namespace JMovies.Tests
{
    [TestClass]
    public class UnityTest
    {
        [TestMethod]
        public void AppBasicResolutionTest()
        {
            Initialize();
            JMAppClient jmAppClient = SingletonUnity.Resolve<JMAppClient>();
            Context dummyContext = new Context();
            jmAppClient.ContextProvider.SetContext(dummyContext);
            BasicResolutionTestRequest request = new BasicResolutionTestRequest();
            BasicResolutionTestResponse response = jmAppClient.CallAction<BasicResolutionTestResponse>(ActionNameConstants.BasicResolutionTest, request);
            Assert.IsTrue(response != null && jmAppClient.ContextProvider.GetContext().ActiveResult.IsSuccess);
        }

        [TestMethod]
        private void WebBasicResolutionTest()
        {
        }

        private void Initialize()
        {
            XmlConfigurator.Configure();
            SingletonUnity.ActiveContainer.RegisterType<IContextProvider, TemporaryContextProvider>();
            SingletonUnity.ActiveContainer.RegisterType<IPathProvider, PathProvider>();
            SingletonUnity.ActiveContainer.RegisterSingleton<IFlowConfigurationProvider, JsonFileBasedFlowConfigurationProvider>();
            SingletonUnity.ActiveContainer.RegisterType<JMAppClient>();
        }
    }
}
