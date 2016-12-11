using System;

namespace FlickrClient.Framework
{
    public interface IContainer
    {
        void RegisterPerRequest<TInterface, TImplementation>() 
            where TImplementation : TInterface;

        void RegisterSingleton<TInterface, TImplementation>() 
            where TImplementation : TInterface;

        void RegisterInstance<TInterface, TImplementation>(TImplementation implementation) 
            where TImplementation : TInterface;

        object GetInstance(Type type);
    }
}
