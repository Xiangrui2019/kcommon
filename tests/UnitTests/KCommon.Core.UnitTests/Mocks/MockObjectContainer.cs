using System;
using KCommon.Core.Abstract.Components;

namespace KCommon.Core.UnitTests.Mocks
{
    public class MockObjectContainer : IObjectContainer
    {
        public bool isBuilded { get; set; } = false;
        
        public void Build()
        {
            isBuilded = true;
        }

        public void RegisterType(Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {
            throw new NotImplementedException();
        }

        public void RegisterType(Type serviceType, Type implementationType, string serviceName = null,
            LifeStyle life = LifeStyle.Singleton)
        {
            throw new NotImplementedException();
        }

        public void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton) where TService : class where TImplementer : class, TService
        {
            throw new NotImplementedException();
        }

        public void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null) where TService : class where TImplementer : class, TService
        {
            throw new NotImplementedException();
        }

        public TService Resolve<TService>() where TService : class
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            throw new NotImplementedException();
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            throw new NotImplementedException();
        }

        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            throw new NotImplementedException();
        }

        public object ResolveNamed(string serviceName, Type serviceType)
        {
            throw new NotImplementedException();
        }

        public bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            throw new NotImplementedException();
        }
    }
}