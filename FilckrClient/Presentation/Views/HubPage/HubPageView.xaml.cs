using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using FilckrClient.MVVM;
using FilckrClient.Presentation.Views.PhotosPivotPage;
using FilckrClient.Utils.DeviceType;
using FilckrClient.Utils.Extensions;
using FlickrSearch.Logic.BusinessLogic.Models;

namespace FilckrClient.Presentation.Views.HubPage
{
    public sealed partial class HubPageView : ViewModelAwarePage
    {
        public HubPageView()
        {
            this.InitializeComponent();
            _viewModel.DisplayMessage += ViewModelDisplayMessageEventHandler;
        }

        private void ViewModelDisplayMessageEventHandler(object sender, string e)
        {
            var messageDialog = new MessageDialog(e);
            messageDialog.ShowAsync();
        }

        private void ItemsWrapGridSizeChangedEventHandler(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            var wrapGrid = sender as ItemsWrapGrid;
            int maxItems = DeviceTypeResolver.ResolveDeviceType() == DeviceType.Mobile ? 2 : 6;
            wrapGrid.ItemWidth = e.NewSize.Width/maxItems;
            wrapGrid.ItemHeight = wrapGrid.ItemWidth;
        }

        private void PhotosGridItemSelected(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as Photo;
            var vmData = ((HubPageViewModel) DataContext).Photos;
            var navigationParameters = new Dictionary<string, object>()
            {
                {"selectedPhotoId", clickedItem.Id},
                {"allItems", ((ObservableCollection<Photo>) vmData).DeepClone()}
            };
            Frame.Navigate(typeof(PhotosPivotPageView), navigationParameters);
        }

        private void SearchBoxQuerySumbitedEventHandler(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            this.GetViewModel<HubPageViewModel>().SearchPhotos(args.QueryText);
            SearchAppBarButton.IsChecked = false;
        }

        private void RefreshEventHandler(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            this.GetViewModel<HubPageViewModel>().SearchPhotos("Berlin");
        }
    }
}
