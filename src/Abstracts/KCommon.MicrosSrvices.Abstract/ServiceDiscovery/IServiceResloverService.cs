using KCommon.MicrosSrvices.Abstract.ServiceDiscovery.Models;
using System.Threading.Tasks;

namespace KCommon.MicrosSrvices.Abstract.ServiceDiscovery
{
    public interface IServiceResloverService
    {
        Task<Service> ResloveServiceAsync(string moduleName, string serviceName);
    }
}
