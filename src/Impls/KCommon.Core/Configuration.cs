using System;
using KCommon.Core.Abstract.Components;
using KCommon.Core.Abstract.Configurations;
using KCommon.Core.Components;
using KCommon.Core.Configurations;

namespace KCommon.Core
{
    public class Configuration
    {
        /// <summary>Provides the singleton access instance.
        /// </summary>
        public static Configuration Instance { get; private set; }

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
            SetDefault<IAppConfig, EmptyAppConfig>();
            SetDefault<IAppEnvironment, EmptyAppEnvironment>();
            
            return this;
        }
        
        public Configuration BuildContainer()
        {
            ObjectContainer.Build();
            return this;
        }
    }
}