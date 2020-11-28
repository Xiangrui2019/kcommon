using KCommon.Core.Abstract.Components;
using KCommon.Core.Abstract.Logging;
using KCommon.Core.Components;
using KCommon.Core.Logging;

namespace KCommon.Core.Configurations
{
    public class Configuration
    {
        private static Configuration Instance { get; set; }

        private Configuration() { }

        public static Configuration Create()
        {
            Instance = new Configuration();
            return Instance;
        }
        
        public Configuration SetDefault<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.Register<TService, TImplementer>(serviceName, life);
            return this;
        }
        public Configuration SetDefault<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.RegisterInstance<TService, TImplementer>(instance, serviceName);
            return this;
        }

        public Configuration RegisterCommonComponents()
        {
            SetDefault<ILoggerFactory, EmptyLoggerFactory>();
            return this;
        }
        
        public Configuration BuildContainer()
        {
            ObjectContainer.Build();
            return this;
        }
    }
}