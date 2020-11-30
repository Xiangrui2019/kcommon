using KCommon.Core.Autofac;
using KCommon.Core.Components;
using KCommon.Core.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.FunctionalTests.IntegrationTests
{
    [TestClass]
    public class ObjectContainerTests
    {
        [TestMethod]
        public void TestService()
        {
            Configuration
               .Create()
               .UseAutofac()
               .RegisterCommonComponents()
               .SetDefault<TestService, TestService>()
               .BuildContainer();

            var testService = ObjectContainer.Resolve<TestService>();

            Assert.AreEqual(10, testService.Test());
        }
    }

    public class TestService
    {
        public int Test()
        {
            return 10;
        }
    }
}
