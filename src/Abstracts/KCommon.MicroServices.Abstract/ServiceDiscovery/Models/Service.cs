using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.MicroServices.Abstract.ServiceDiscovery.Models
{
    public class Service
    {
        public Service(string moduleName, string serviceName)
        {
            ModuleName = moduleName;
            ServiceName = serviceName;
            ServiceEndpoints = new List<Endpoint>();
        }

        public string ModuleName { get; set; }
        public string ServiceName { get; set; }

        public List<Endpoint> ServiceEndpoints { get; set; }

        public void AddServiceEndpoints(Endpoint endpoint)
        {
            ServiceEndpoints.Add(endpoint);
        }
    }
}
