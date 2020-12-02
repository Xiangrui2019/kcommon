using KCommon.MicrosSrvices.Abstract.ServiceDiscovery;
using System.Threading.Tasks;

namespace KCommon.MicroServices.ServiceDiscovery
{
    public class EmptyServiceRegisterService : IServiceRegisterService
    {
        public Task StartRegisterServiceAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
