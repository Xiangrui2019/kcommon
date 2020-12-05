using KCommon.Core.Configurations;
using KCommon.Web.Middlewares.Failing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Web.Configurations
{
    public static class ConfigurationExtensions
    {
        public static Configuration AddFailingComponents(Configuration configuration, 
            Func<FailingOptions> options)
        {
            configuration.SetDefault<FailingOptions, FailingOptions>(options());

            return configuration;
        }

        public static Configuration AddFailingComponents(Configuration configuration)
        {
            configuration.SetDefault<FailingOptions, FailingOptions>(new FailingOptions());

            return configuration;
        }
    }
}
