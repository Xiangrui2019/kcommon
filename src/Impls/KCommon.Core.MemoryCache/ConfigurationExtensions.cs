using KCommon.Core.Abstract.Caching;
using KCommon.Core.Configurations;

namespace KCommon.Core.MemoryCache
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use memeory as the cache.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseInMemoryCache(this Configuration configuration)
        {
            configuration.SetDefault<
                Microsoft.Extensions.Caching.Memory.IMemoryCache,
                Microsoft.Extensions.Caching.Memory.MemoryCache>(
                new Microsoft.Extensions.Caching.Memory.MemoryCache(
                    new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions()));

            configuration.SetDefault<ICache, InMemoryCache>();

            return configuration;
        }
    }
}