using KCommon.MicrosSrvices.Abstract.ServiceDiscovery;
using KCommon.MicrosSrvices.Abstract.ServiceDiscovery.Models;
using System.Threading.Tasks;

namespace KCommon.MicroServices.ServiceDiscovery
{
    public class EmptyServiceResloverService : IServiceResloverService
    {
        public Task<Service> ResloveServiceAsync(string moduleName, string serviceName)
        {
            throw new System.NotImplementedException();
        }
    }
}
