using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;
using FlickrSearch.Logic.BusinessLogic.Models;
namespace FilckrClient.Presentation.Views.MapPage
{
    public sealed partial class MapPageView : Page
    {
        public MapPageView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var location = e.Parameter as PhotoGeolocation;
            var imageGeoPoint = new Geopoint(new BasicGeoposition()
            {
                Altitude = 0,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            });
            var mapPin = new MapIcon()
            {
                Location = imageGeoPoint,
                Title = "Photo was taken here!"
            };

            Map.MapElements.Add(mapPin);
            Map.Center = imageGeoPoint;
            Map.ZoomLevel = 18;
        }
    }
}
