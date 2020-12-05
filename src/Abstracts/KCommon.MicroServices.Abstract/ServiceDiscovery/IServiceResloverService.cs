using KCommon.MicroServices.Abstract.ServiceDiscovery.Models;
using System.Threading.Tasks;

namespace KCommon.MicroServices.Abstract.ServiceDiscovery
{
    public interface IServiceResloverService
    {
        Task<Service> ResloveServiceAsync(string moduleName, string serviceName);
    }
}
