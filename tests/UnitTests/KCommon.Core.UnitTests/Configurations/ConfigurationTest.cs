using KCommon.Core.Autofac;
using KCommon.Core.Components;
using KCommon.Core.Configurations;
using KCommon.Core.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Configurations
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void TestBasic()
        {
            Configuration
                .Create()
                .UseAutofac()
                .BuildContainer();
        }

        [TestMethod]
        public void TestCommon()
        {
            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .BuildContainer();
        }

        [TestMethod]
        public void TestSetDefault()
        {
            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .SetDefault<MockTestService, MockTestService>()
                .BuildContainer();

            ObjectContainer.Resolve<MockTestService>().Test();
        }
    }
}