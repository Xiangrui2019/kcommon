using Consul;
using KCommon.Core.Abstract.Cache;
using KCommon.MicroServices.Abstract.ServiceDiscovery;
using KCommon.MicroServices.Abstract.ServiceDiscovery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.MicroServices.Consul
{
    public class ConsulServiceResloverService : IServiceResloverService
    {
        private readonly ConsulClient _client;
        private readonly ICache _cache;

        public ConsulServiceResloverService(
            ConsulClient client,
            ICache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<Service> ResloveServiceAsync(string moduleName, string serviceName)
        {
            var serviceEntries = await _cache.GetAndCacheAsync(
                $"ServiceDiscovery-{moduleName}-{serviceName}",
                async () => await GetServiceAsync(moduleName, serviceName));
            var service = new Service(moduleName, serviceName);

            foreach (var serviceEntry in serviceEntries)
                service.AddServiceEndpoints(
                    new Endpoint(serviceEntry.Service.Address, serviceEntry.Service.Port, true));

            return service;
        }

        private async Task<ServiceEntry[]> GetServiceAsync(string moduleName, string serviceName)
        {
            var services = await _client.Health.Service($"{moduleName}.{serviceName}", string.Empty, true);

            return services.Response;
        }
    }
}