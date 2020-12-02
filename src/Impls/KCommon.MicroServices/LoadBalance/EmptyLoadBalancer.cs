using KCommon.MicrosSrvices.Abstract.LoadBalance;
using KCommon.MicrosSrvices.Abstract.ServiceDiscovery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCommon.MicroServices.LoadBalance
{
    public class EmptyLoadBalancer : ILoadBalancer
    {
        public Task<ServiceEndpoint> Endpoint(List<ServiceEndpoint> endpoints)
        {
            throw new System.NotImplementedException();
        }
    }
}
