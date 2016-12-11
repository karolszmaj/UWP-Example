using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using FilckrClient.MVVM;
using FilckrClient.Presentation.Views.MapPage;
using FlickrSearch.Logic.BusinessLogic.Models;

namespace FilckrClient.Presentation.Views.PhotosPivotPage
{
    public sealed partial class PhotosPivotPageView : ViewModelAwarePage
    {
        public PhotosPivotPageView()
        {
            this.InitializeComponent();
            this._viewModel.DisplayMessage += ViewModelDisplayMessageEventHandler;
        }

        private void ViewModelDisplayMessageEventHandler(object sender, string e)
        {
            var messageDialog = new MessageDialog(e, "Warning");
            messageDialog.ShowAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                //this should be handled if we would have some data persistance.
                return;
            }

            var viewModel = (PhotosPivotPageViewModel)DataContext;
            var navData = e.Parameter as Dictionary<string, object>;
            var selectedPhotoId = navData["selectedPhotoId"] as string;
            var photos = navData["allItems"] as ObservableCollection<Photo>;

            viewModel.Photos = photos;
            viewModel.CurrentPhoto = photos.FirstOrDefault(x => x.Id == selectedPhotoId);
        }

        private void ShowMapEventHandler(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MapPageView),
                ((PhotosPivotPageViewModel)DataContext).GetLocationForCurrentPhoto());
        }

        private void OnHeaderSelectedItemChangedEventHandler(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView.SelectedItem != null)
            {
                listView.ScrollIntoView(listView.SelectedItem, ScrollIntoViewAlignment.Leading);
            }
        }
    }
}
