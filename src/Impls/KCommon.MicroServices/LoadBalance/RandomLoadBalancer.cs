using KCommon.MicroServices.Abstract.LoadBalance;
using KCommon.MicroServices.Abstract.ServiceDiscovery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.MicroServices.LoadBalance
{
    public class RandomLoadBalancer : ILoadBalancer
    {
        public Task<Endpoint> Endpoint(List<Endpoint> endpoints)
        {
            throw new NotImplementedException();
        }
    }
}