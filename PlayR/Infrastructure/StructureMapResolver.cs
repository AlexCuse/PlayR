using System;
using System.Collections.Generic;
using System.Linq;
using SignalR;
using StructureMap;

namespace PlayR.Infrastructure
{
    public class StructureMapResolver : DefaultDependencyResolver
    {
        private IContainer _container;

        public StructureMapResolver(IContainer container)
        {
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            object service = null;
            if(!serviceType.IsAbstract && !serviceType.IsInterface && serviceType.IsClass)
            {
                // Concrete type resolution
                service = _container.GetInstance(serviceType);
            }
            else
            {
                // Other type resolution with base fallback
                service = _container.TryGetInstance(serviceType) ?? base.GetService(serviceType);
            }
            return service;
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            var objects = _container.GetAllInstances(serviceType).Cast<object>();
            objects.Concat(base.GetServices(serviceType));
            return objects;
        }
    }
}