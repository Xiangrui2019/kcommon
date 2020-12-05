using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.MicroServices.Abstract.ServiceDiscovery
{
    public interface IServiceRegisterService
    {
        Task StartAsync();
    }
}
