using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using StructureMap;

namespace MusicWave
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            var instance = ObjectFactory.Container.TryGetInstance(serviceType);
            if (instance == null && !serviceType.IsAbstract && !serviceType.IsInterface)
            {
                instance = ObjectFactory.GetInstance(serviceType);
            }
            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ObjectFactory.GetAllInstances(serviceType).Cast<object>();
        }

        public void Register(Type serviceType, Func<object> activator)
        {
            throw new NotImplementedException();
        }

        public void Register(Type serviceType, IEnumerable<Func<object>> activators)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}