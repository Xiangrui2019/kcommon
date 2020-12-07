using System;
using KCommon.Core.Abstract.Cache;
using KCommon.Core.Autofac;
using KCommon.Core.Components;
using KCommon.Core.Configurations;
using KCommon.Core.JsonNet;
using KCommon.Core.MemoryCache;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.FunctionalTests.IntegrationTests
{
    [TestClass]
    public class MemoryCacheTest
    {
        [TestInitialize]
        public void InitFramework()
        {
            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseInMemoryCache()
                .UseJsonNet()
                .BuildContainer();
        }

        [TestMethod]
        public void TestSet()
        {
            var cache = ObjectContainer.Resolve<ICache>();

            cache.SetAsync("test", "tttt");
            
            Assert.AreEqual("tttt", cache.Get<string>("test"));
        }
    }
}