using KCommon.Core.Autofac;
using KCommon.Core.Components;
using KCommon.Core.Configurations;
using KCommon.Core.UnitTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.AOP
{
    [TestClass]
    public class HystrixTest
    {
        private AopMockService _service;
        
        [TestInitialize]
        public void InitFramework()
        {
            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .SetDefault<AopMockService, AopMockService>()
                .BuildContainer();

            _service = ObjectContainer.Resolve<AopMockService>();
        }

        [TestMethod]
        public void TestMethod1()
        {
            _service.HelloAsync("Test")
                .GetAwaiter()
                .GetResult();
        }

        [TestMethod]
        public void TestMethod2()
        {
            var s = _service.Add(1, 2);
            Assert.AreEqual(s, 3);

            s = _service.AddTest(1, 2);
            Assert.AreEqual(s, 0);
        }
    }
}