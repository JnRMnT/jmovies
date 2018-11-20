using JMovies.App.Proxy;
using JMovies.Common.Constants;
using JMovies.Entities;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using JMovies.Tests.Helpers;
using JMovies.Utilities.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JMovies.Tests
{
    [TestClass]
    public class UnityTest
    {
        [TestMethod]
        public void AppBasicResolutionTest()
        {
            UnityHelper.Initialize();
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
    }
}
