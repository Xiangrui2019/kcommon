using KCommon.MicroServices.Abstract.ServiceDiscovery;
using KCommon.MicroServices.Abstract.ServiceDiscovery.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.MicroServices.ServiceDiscovery
{
    public class ConfigurationServiceEndpoints : IServiceEndpoints
    {
        private readonly IConfiguration _configuration;

        public ConfigurationServiceEndpoints(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Endpoint> GetEndpoints()
        {
            var endpoints = new List<Endpoint>();

            endpoints.Add(
                new Endpoint(new Uri(_configuration["ServiceDiscovery:Endpoint"])));

            return endpoints;
        }
    }
}