using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FilckrClient.MVVM
{
    public abstract class ViewModelAwarePage : Page
    {
        protected readonly ViewModel _viewModel;
        public ViewModelAwarePage()
        {
            if (DesignMode.DesignModeEnabled == false)
            {
                _viewModel = ViewModelLocator.LocateForView(this.GetType());
                DataContext = _viewModel;
                ConfigureStatusBar();
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var viewModel = DataContext as ViewModel;
            var viewModelType = viewModel.GetType();

            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                var initializeViewTask = viewModelType.GetMethod("OnInitialize", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(viewModel, new object[] { }) as Task;
                await initializeViewTask;
            }

            var activateTask =
                viewModelType.GetMethod("OnActivate", BindingFlags.Instance | BindingFlags.NonPublic)
                    .Invoke(viewModel, null) as Task;
            await activateTask;
        }

        private void ConfigureStatusBar()
        {
            //This logic should be moved to seperated class. It's simplified version.
            //Customization for Mobile
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.HideAsync();
                }
            }

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor =
                        (Application.Current.Resources["AppDarkRedSolidColorBrush"] as SolidColorBrush).Color;
                    titleBar.ButtonForegroundColor = Colors.White;
                    titleBar.BackgroundColor = (Application.Current.Resources["AppDarkGreySolidColorBrush"] as SolidColorBrush).Color;
                    titleBar.ForegroundColor = Colors.White;
                }
            }
        }
    }
}
