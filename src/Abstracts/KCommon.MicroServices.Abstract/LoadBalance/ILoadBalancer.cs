using KCommon.MicroServices.Abstract.ServiceDiscovery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.MicroServices.Abstract.LoadBalance
{
    public interface ILoadBalancer
    {
        Task<Endpoint> Endpoint(List<Endpoint> endpoints);
    }
}