using KCommon.Core.Components;
using KCommon.Core.Configurations;
using KCommon.Core.Validations;
using KCommon.MicroServices.Abstract.LoadBalance;
using KCommon.MicroServices.Abstract.ServiceDiscovery;
using KCommon.MicroServices.LoadBalance;
using KCommon.MicroServices.ServiceDiscovery;
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
            _configuration.SetDefault<IServiceEndpoints, ConfigurationServiceEndpoints>();
            _configuration.SetDefault<IServiceRegisterService, EmptyServiceRegisterService>();
            _configuration.SetDefault<IServiceResloverService, EmptyServiceResloverService>();
            _configuration.SetDefault<ILoadBalancer, RandomLoadBalancer>();

            return this;
        }

        public MicroServicesConfiguration UseK8sServiceEndpoint()
        {
            _configuration.SetDefault<IServiceEndpoints, K8sServiceEndpoints>();

            return this;
        }

        public MicroServicesConfiguration UseConfServiceEndpoint()
        {
            _configuration.SetDefault<IServiceEndpoints, ConfigurationServiceEndpoints>();

            return this;
        }

        public MicroServicesConfiguration UseServiceEndpoint<T>() where T : class, IServiceEndpoints
        {
            _configuration.SetDefault<IServiceEndpoints, T>();

            return this;
        }

        public MicroServicesConfiguration StartServiceRegister()
        {
            var service = ObjectContainer.Resolve<IServiceRegisterService>();
            service.StartAsync().GetAwaiter().GetResult();

            return this;
        }
    }
}
