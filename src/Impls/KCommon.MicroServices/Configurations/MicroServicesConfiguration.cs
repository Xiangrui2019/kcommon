using KCommon.Core.Configurations;
using KCommon.Core.Validations;
using KCommon.MicroServices.ServiceDiscovery;
using KCommon.MicrosSrvices.Abstract.ServiceDiscovery;
using System;

namespace KCommon.MicroServices.Configurations
{
    public class MicroServicesConfiguration
    {
        private readonly Configuration _configuration;

        public static MicroServicesConfiguration Instance { get; private set; }

        private MicroServicesConfiguration(Configuration configuration)
        {
            if (!new NotNull().IsValid(configuration))
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _configuration = configuration;
        }

        public static MicroServicesConfiguration CreateMicroServices(Configuration configuration)
        {
            Instance = new MicroServicesConfiguration(configuration);
            return Instance;
        }

        public Configuration GetCommonConfiguration()
        {
           return _configuration;
        }

        public MicroServicesConfiguration RegisterMicroServicesComponents()
        {
            _configuration.SetDefault<IServiceRegisterService, EmptyServiceRegisterService>();
            _configuration.SetDefault<IServiceResloverService, EmptyServiceResloverService>();

            return this;
        }
    }
}
