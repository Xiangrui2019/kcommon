using KCommon.MicroServices.Abstract.ServiceDiscovery;
using KCommon.MicroServices.Abstract.ServiceDiscovery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.MicroServices.ServiceDiscovery
{
    public class K8sServiceEndpoints : IServiceEndpoints
    {
        public List<Endpoint> GetEndpoints()
        {
            var PodIp = Environment.GetEnvironmentVariable("POD_IP");
            var endpoints = new List<Endpoint>();

            endpoints.Add(new Endpoint(new Uri($"http://{PodIp}:80")));

            return endpoints;
        }
    }
}