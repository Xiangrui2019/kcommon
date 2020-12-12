using System;
using System.Threading;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Caching;
using KCommon.Core.Autofac;
using KCommon.Core.Components;
using KCommon.Core.Configurations;
using KCommon.Core.MemoryCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KCommon.Core.UnitTests.Caching
{
    [TestClass]
    public class MemoryCacheTest
    {
        private ICache _cache;
        
        [TestInitialize]
        public void InitFramework()
        {
            Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseInMemoryCache()
                .BuildContainer();

            _cache = ObjectContainer.Resolve<ICache>();
        }

        [TestMethod]
        public async Task TestSetAsync()
        {
            await _cache.SetAsync("aaa", "123");

            var result = await _cache.GetAsync<string>("aaa");
            
            Assert.AreEqual(result, "123");
        }

        [TestMethod]
        public async Task TestGetAsync()
        {
            await _cache.SetAsync("bbb", "123");

            var result = await _cache.GetAsync<string>("bbb");
            
            Assert.AreEqual(result, "123");
        }
        
        [TestMethod]
        public async Task TestLifetimeAsync()
        {
            await _cache.SetAsync("ccc", "123", 1);

            var result = await _cache.GetAsync<string>("ccc");
            Assert.AreEqual(result, "123");
        }

        [TestMethod]
        public async Task TestGetAndCacheAsync()
        {
            var result = await _cache.GetAndCacheAsync("ddd", () => Task.FromResult(1));
            Assert.AreEqual(result, 1);

            result = await _cache.GetAndCacheAsync("ddd", () => Task.FromResult(2));
            Assert.AreEqual(result, 1);
            
            _cache.Clear("ddd");
            
            result = await _cache.GetAndCacheAsync("ddd", () => Task.FromResult(2));
            Assert.AreEqual(result, 2);
        }
        
        [TestMethod]
        public async Task TestTryGetAsync()
        {
            var (_, s) = await _cache.TryGetAsync<int>("eee");
            
            Assert.AreEqual(s, false);
            
            await _cache.SetAsync("eee", 10);
            
            (_, s) = await _cache.TryGetAsync<int>("eee");
            
            Assert.AreEqual(s, true);
        }
        
        [TestMethod]
        public void TestSet()
        {
            _cache.Set("aaa", "123");

            var result = _cache.Get<string>("aaa");
            
            Assert.AreEqual(result, "123");
        }

        [TestMethod]
        public void TestGet()
        {
            _cache.Set("bbb", "123");

            var result = _cache.Get<string>("bbb");
            
            Assert.AreEqual(result, "123");
        }
        
        [TestMethod]
        public void TestLifetime()
        {
            _cache.Set("ccc", "123", 1);

            var result = _cache.Get<string>("ccc");
            Assert.AreEqual(result, "123");
        }

        [TestMethod]
        public void TestGetAndCache()
        {
            var result = _cache.GetAndCache("ddd", () => 1);
            Assert.AreEqual(result, 1);

            result = _cache.GetAndCache("ddd", () => 2);
            Assert.AreEqual(result, 1);
            
            _cache.Clear("ddd");
            
            result = _cache.GetAndCache("ddd", () => 2);
            Assert.AreEqual(result, 2);
        }
        
        [TestMethod]
        public void TestTryGet()
        {
            var (_, s) = _cache.TryGet<int>("eee");
            
            Assert.AreEqual(s, false);
            
            _cache.Set("eee", 10);
            
            (_, s) = _cache.TryGet<int>("eee");
            
            Assert.AreEqual(s, true);
        }
    }
}