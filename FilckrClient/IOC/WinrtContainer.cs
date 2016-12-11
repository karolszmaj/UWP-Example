using System;
using FlickrClient.Framework;
using LightInject;

namespace FilckrClient.IOC
{
    internal class WinrtContainer: IContainer
    {
        private readonly ServiceContainer _container;

        public WinrtContainer()
        {
            _container = (App.Current as App).Container;
        }

        public void RegisterPerRequest<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _container.Register<TInterface, TImplementation>();
        }

        public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _container.Register<TInterface, TImplementation>(new PerContainerLifetime());
        }

        public void RegisterInstance<TInterface, TImplementation>(TImplementation implementation) where TImplementation : TInterface
        {
            _container.RegisterInstance(typeof (TInterface), implementation);
        }

        public object GetInstance(Type type)
        {
            return _container.GetInstance(type);
        }
    }
}
