using KCommon.MicrosSrvices.Abstract.ServiceDiscovery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCommon.MicrosSrvices.Abstract.LoadBalance
{
    public interface ILoadBalancer
    {
        Task<ServiceEndpoint> Endpoint(List<ServiceEndpoint> endpoints);
    }
}
