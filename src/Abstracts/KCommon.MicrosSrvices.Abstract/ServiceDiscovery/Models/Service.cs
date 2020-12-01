using System;
using System.Collections.Generic;

namespace KCommon.MicrosSrvices.Abstract.ServiceDiscovery.Models
{
    public class Service
    {
        public Service(string moduleName, string serviceName)
        {
            ModuleName = moduleName;
            ServiceName = serviceName;
            Endpoints = new List<ServiceEndpoint>();
        }

        public string ModuleName { get; set; }
        public string ServiceName { get; set; }

        public List<ServiceEndpoint> Endpoints { get; set; }

        public void AddServiceEndpoint(ServiceEndpoint endpoint)
        {
            Endpoints.Add(endpoint);
        }
    }

    public class ServiceEndpoint
    {
        public ServiceEndpoint(Uri uri, bool isHealthy)
        {
            Url = uri;
            Host = Url.Host;
            Port = Url.Port;
            IsHealthy = isHealthy;
        }

        public Uri Url { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsHealthy { get; set; }
    }
}
