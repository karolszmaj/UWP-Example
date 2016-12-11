using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Reactive.Linq;
using FilckrClient.MVVM;
using FlickrSearch.Logic.BusinessLogic.Models;
using FlickrSearch.Logic.BusinessLogic.Queries.GetPhotoLocation;
using FlickrSearch.Logic.Infrastructure.Query;

namespace FilckrClient.Presentation.Views.PhotosPivotPage
{
    public class PhotosPivotPageViewModel : ViewModel
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly Dictionary<string, PhotoGeolocation> _photosLocation;

        private ObservableCollection<Photo> _photos;
        private Photo _currentPhoto;
        private bool _hasLocation;

        public bool HasLocation
        {
            get { return _hasLocation; }
            set
            {
                _hasLocation = value;
                NotifyPropertyChanged();
            }
        }

        public Photo CurrentPhoto
        {
            get { return _currentPhoto; }
            set
            {
                if (value == null)
                {
                    return;
                }

                _currentPhoto = value;
                NotifyPropertyChanged();
                ValidatePhotoLocation(_currentPhoto.Id);
            }
        }

        public ObservableCollection<Photo> Photos
        {
            get { return _photos; }
            set
            {
                _photos = value;
                NotifyPropertyChanged();
            }
        }

        public PhotosPivotPageViewModel(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
            _photosLocation = new Dictionary<string, PhotoGeolocation>();
        }

        public PhotoGeolocation GetLocationForCurrentPhoto()
        {
            PhotoGeolocation destGeolocation;
            _photosLocation.TryGetValue(CurrentPhoto.Id, out destGeolocation);
            return destGeolocation;
        }

        private void GetPhotoLocation(string photoId)
        {

            _queryProcessor.Process(new GetPhotoLocationQuery(photoId))
                .ObserveOnDispatcher()
                
                .Subscribe(result =>
                {
                    _photosLocation[result.PhotoId] = result;
                    ValidatePhotoLocation(result.PhotoId);
                }, error =>
                {
                    if (error is HttpRequestException)
                    {
                        OnDisplayMessage("Check your internet connection. Photo location won't be fetch without internet connection.");
                    }
                });
        }

        private void ValidatePhotoLocation(string photoId)
        {
            HasLocation = _photosLocation.ContainsKey(photoId);
            if (HasLocation == false)
            {
                GetPhotoLocation(photoId);
            }
        }

    }
}
