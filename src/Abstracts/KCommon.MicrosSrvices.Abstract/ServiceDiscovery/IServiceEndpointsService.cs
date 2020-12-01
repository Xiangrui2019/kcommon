using KCommon.MicrosSrvices.Abstract.ServiceDiscovery.Models;
using System.Collections.Generic;

namespace KCommon.MicrosSrvices.Abstract.ServiceDiscovery
{
    public interface IServiceEndpointsService
    {
        List<ServiceEndpoint> GetEndpoints();
    }
}
