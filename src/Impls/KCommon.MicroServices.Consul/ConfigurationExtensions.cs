using Consul;
using KCommon.Core.Abstract.Logging;
using KCommon.Core.Configurations;
using System;

namespace KCommon.MicroServices.Consul
{
    public static class ConfigurationExtensions
    {
        /// <summary>Use Consul as ServiceDiscovery
        /// </summary>
        /// <returns></returns>
        public static Configuration UseConsulServiceDiscovery(this Configuration configuration, Action<ConsulClientConfiguration> options)
        {
            var client = new ConsulClient(options);
            configuration.SetDefault<ConsulClient, ConsulClient>(client);

            return configuration;
        }
    }
}
