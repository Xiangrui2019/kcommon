using System;
using System.Threading;
using KCommon.Core.AOP;
using KCommon.Core.Autofac;
using KCommon.Core.Components;
using KCommon.Core.Configurations;
using KCommon.Core.MemoryCache;
using KCommon.Core.UnitTests.Mocks;
using KCommon.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.AOP
{
    [TestClass]
    public class CacheTest
    {
        private TestService _service;

        [TestInitialize]
        public void InitFramework()
        {
            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseInMemoryCache()
                .SetDefault<TestService, TestService>()
                .BuildContainer();

            _service = ObjectContainer.Resolve<TestService>();
        }
        
        [TestMethod]
        public void TestBasic()
        {
            var t1 = _service.Rand();
            var t2 = _service.Rand();
            
            Assert.AreEqual(t1, t2);
        }

        [TestMethod]
        public void TestTimeout()
        {
            var t1 = _service.Rand();
            var t1_t = _service.Rand();
            Thread.Sleep(TimeSpan.FromMinutes(1));
            var t2 = _service.Rand();
            
            Assert.AreNotEqual(t1, t2);
            Assert.AreEqual(t1, t1_t);
        }
    }

    public class TestService
    {
        [AutoCache(CachedMinutes = 1)]
        public virtual ObjectId Rand()
        {
            return ObjectId.GenerateNewId();
        }
    }
}