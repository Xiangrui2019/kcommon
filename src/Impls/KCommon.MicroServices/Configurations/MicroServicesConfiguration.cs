using KCommon.Core.Configurations;
using KCommon.Core.Validations;
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

        public static MicroServicesConfiguration CreateMicroServices(Configuration configuration, ConfigurationSetting setting)
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
            return this;
        }
    }
}
