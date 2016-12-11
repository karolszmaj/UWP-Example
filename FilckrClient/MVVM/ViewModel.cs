using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using FilckrClient.Annotations;

namespace FilckrClient.MVVM
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<string> DisplayMessage;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected async virtual Task OnInitialize()
        {

        }

        protected async virtual Task OnActivate()
        {

        }

        protected virtual void OnDisplayMessage(string e)
        {
            Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                DisplayMessage?.Invoke(this, e);
            });
        }
    }
}
