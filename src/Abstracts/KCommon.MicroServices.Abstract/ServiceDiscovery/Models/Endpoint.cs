using System;

namespace KCommon.MicroServices.Abstract.ServiceDiscovery.Models
{
    public class Endpoint
    {
        public Endpoint(Uri uri, bool isHealthy)
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
