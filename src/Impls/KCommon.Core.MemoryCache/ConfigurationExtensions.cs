using KCommon.Core.Abstract.Cache;
using KCommon.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
