using System;
using System.Linq;
using System.Reflection;

namespace FilckrClient.MVVM
{
    public class ViewModelLocator
    {
        public static ViewModel LocateForView(Type viewType)
        {
            var viewName = viewType.Name;
            //we will take the simplest approach here. We will do simply assembly lookup for types like that.
            var viewModelName = viewName += "Model";

            //1. App assembly
            var appAssembly = typeof (App).GetTypeInfo().Assembly;
            var viewModelType = appAssembly.DefinedTypes
                .Where(x => x.IsSubclassOf(typeof (ViewModel)))
                .FirstOrDefault(x => x.Name == viewModelName);

            if (viewModelType == null)
            {
                throw new InvalidOperationException($"ViewModel {viewModelName} not found. Please check if your ViewModel is subsclass of ViewModel type.");
            }

            var instance = (App.Current as App).Container.GetInstance(viewModelType.AsType()) as ViewModel;
            return instance;
        }
    }
}
