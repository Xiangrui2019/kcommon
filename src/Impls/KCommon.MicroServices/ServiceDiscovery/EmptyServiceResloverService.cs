using KCommon.MicroServices.Abstract.ServiceDiscovery;
using KCommon.MicroServices.Abstract.ServiceDiscovery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.MicroServices.ServiceDiscovery
{
    public class EmptyServiceResloverService : IServiceResloverService
    {
        public Task<Service> ResloveServiceAsync(string moduleName, string serviceName)
        {
            throw new NotImplementedException();
        }
    }
}
