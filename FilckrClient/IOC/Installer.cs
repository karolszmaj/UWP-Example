using System;
using System.Linq;
using System.Reflection;
using FilckrClient.Presentation.Views.HubPage;
using FilckrClient.Presentation.Views.PhotosPivotPage;
using FlickrClient.Framework;
using LightInject;

namespace FilckrClient.IOC
{
    class Installer
    {
        private static ServiceContainer _container = (App.Current as App).Container;
        public static void Install()
        {           
            
            _container.Register<IContainer, WinrtContainer>(new PerContainerLifetime());
            _container.Register<HubPageViewModel>(new PerContainerLifetime());
            _container.Register<PhotosPivotPageViewModel>(new PerContainerLifetime());

            InstallAssemblies();

        }

        private static void InstallAssemblies()
        {
            InstallAssembly(Assembly.Load(new AssemblyName("FlickrSearch.Logic")));
        }

        private static void InstallAssembly(Assembly assembly)
        {
            var installableType = assembly.DefinedTypes.FirstOrDefault(x => x.ImplementedInterfaces.Contains(typeof(IAssemblyInstaller)));
            if (installableType != null)
            {
                var installer = Activator.CreateInstance(installableType.AsType()) as IAssemblyInstaller;
                installer.InstallDependencies(_container.GetInstance<IContainer>());
            }
        }
    }
}
