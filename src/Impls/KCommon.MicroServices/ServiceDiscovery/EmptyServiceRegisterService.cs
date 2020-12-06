using KCommon.MicroServices.Abstract.ServiceDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.MicroServices.ServiceDiscovery
{
    public class EmptyServiceRegisterService : IServiceRegisterService
    {
        public Task StartAsync()
        {
            throw new NotImplementedException();
        }
    }
}