using KCommon.Core.Abstract.Cache;
using KCommon.Core.Configurations;
using Microsoft.Extensions.Caching.Redis;

namespace KCommon.Core.MemoryCache
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use redis as the cache.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseRedisCache(this Configuration configuration)
        {
            configuration.SetDefault<
                Microsoft.Extensions.Caching.Distributed.IDistributedCache,
                Microsoft.Extensions.Caching.Redis.RedisCache>(
                    new Microsoft.Extensions.Caching.Redis.RedisCache(
                        new RedisCacheOptions()));

            configuration.SetDefault<ICacheService, RedisCacheService>();

            return configuration;
        }
    }
}
