using KCommon.Core.Abstract.Cache;
using KCommon.Core.Configurations;
using StackExchange.Redis;
using System;

namespace KCommon.Core.RedisCache
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use memeory as the cache.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseInRedisCache(this Configuration configuration, string connectionString)
        {
            configuration.SetDefault<ConnectionMultiplexer, ConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(connectionString));

            configuration.SetDefault<ICache, InRedisCache>();

            return configuration;
        }
    }
}
