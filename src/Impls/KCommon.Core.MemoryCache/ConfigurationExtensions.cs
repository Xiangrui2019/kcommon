using KCommon.Core.Configurations;
using System.Runtime.Caching;

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

            return configuration;
        }
    }
}
