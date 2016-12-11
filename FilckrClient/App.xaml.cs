using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Bezysoftware.Navigation.BackButton;
using FilckrClient.IOC;
using FilckrClient.Presentation.Views.HubPage;
using LightInject;

namespace FilckrClient
{
    sealed partial class App : Application
    {
        public ServiceContainer Container { get; private set; }

        public App()
        {
            this.InitializeComponent();
            Container = new ServiceContainer();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Installer.Install();
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }
                Window.Current.Content = rootFrame;
            }
            BackButtonManager.RegisterFrame(rootFrame,true,true,true);
            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof (HubPageView));

            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
