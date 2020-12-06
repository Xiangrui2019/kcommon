using Consul;
using KCommon.Core.Abstract.Logging;
using KCommon.MicroServices.Abstract.ServiceDiscovery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace KCommon.MicroServices.Consul
{
    public class ConsulServiceRegisterService : IServiceRegisterService
    {
        private readonly ConsulClient _client;
        private readonly IConfiguration _configuration;
        private readonly IServiceEndpoints _serviceEndpoints;
        private readonly IApplicationLifetime _lifetime;
        private readonly ILogger _logger;

        public ConsulServiceRegisterService(
            ConsulClient client,
            IConfiguration configuration,
            IServiceEndpoints serviceEndpoints,
            ILoggerFactory loggerFactory,
            IApplicationLifetime lifetime)
        {
            _client = client;
            _configuration = configuration;
            _serviceEndpoints = serviceEndpoints;
            _lifetime = lifetime;
            _logger = loggerFactory.Create(GetType().FullName);
        }

        public async Task StartAsync()
        {
            if (string.IsNullOrWhiteSpace(_configuration["ServiceDiscovery:ServiceName"])
                || string.IsNullOrWhiteSpace(_configuration["ServiceDiscovery:ModuleName"]))
            {
                throw new ArgumentException("Service Name must be configured", _configuration["ServiceDiscoveryOptions:ServiceName"]);
            }

            var endpoints = _serviceEndpoints.GetEndpoints();

            foreach (var endpoint in endpoints)
            {
                var serviceName =
                    $"{_configuration["ServiceDiscoveryOptions:ModuleName"]}.{_configuration["ServiceDiscoveryOptions:ServiceName"]}";
                var serviceId =
                    $"{serviceName}_{endpoint.Host}:{endpoint.Port}";

                _logger.Info($"Registering service {serviceId} for address {endpoint.Url}.");

                var serviceCheck = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(4),
                    Interval = TimeSpan.FromSeconds(5),
                    HTTP = new Uri(endpoint.Url, _configuration["ServiceDiscovery:HealthCheckTemplate"]).OriginalString
                };

                var registation = new AgentServiceRegistration
                {
                    Check = serviceCheck,
                    Address = endpoint.Host,
                    ID = serviceId,
                    Name = serviceName,
                    Port = endpoint.Port
                };

                await _client.Agent.ServiceRegister(registation);
                _lifetime.ApplicationStopping.Register(() =>
                {
                    _client.Agent.ServiceDeregister(registation.ID).GetAwaiter().GetResult();
                });
            }
        }
    }
}
