using KCommon.Core.Configurations;
using KCommon.Core.Validations;
using System;

namespace KCommon.EDDD.Configurations
{
    public class EDDDConfiguration
    {
        private readonly Configuration _configuration;

        public static EDDDConfiguration Instance { get; private set; }

        private EDDDConfiguration(Configuration configuration)
        {
            if (!new NotNull().IsValid(configuration)) throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
        }

        public static EDDDConfiguration CreateEDDD(Configuration configuration)
        {
            Instance = new EDDDConfiguration(configuration);
            return Instance;
        }

        public Configuration GetCommonConfiguration()
        {
            return _configuration;
        }

        public EDDDConfiguration RegisterEDDDComponents()
        {
            return this;
        }
    }
}