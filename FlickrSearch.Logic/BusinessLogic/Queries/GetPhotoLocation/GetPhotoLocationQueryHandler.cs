using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using FlickrSearch.Logic.BusinessLogic.Models;
using FlickrSearch.Logic.DataAccessLayer.DTO.Photos;
using FlickrSearch.Logic.Infrastructure.Providers.Photos;
using FlickrSearch.Logic.Infrastructure.Query;

namespace FlickrSearch.Logic.BusinessLogic.Queries.GetPhotoLocation
{
    public class GetPhotoLocationQueryHandler: IQueryHandler<GetPhotoLocationQuery, IObservable<PhotoGeolocation>>
    {
        private readonly IPhotoSearchProvider _photosProvider;

        public GetPhotoLocationQueryHandler(IPhotoSearchProvider photosProvider)
        {
            _photosProvider = photosProvider;
        }

        public IObservable<PhotoGeolocation> Handle(GetPhotoLocationQuery query)
        {
            return
                _photosProvider.GetPhotoLocationAsync(query.PhotoId)
                    .ToObservable()
                    .Select(t => Observable.Start(
                        () => MapToDomainResult(t))
                        .Catch(Observable.Empty<PhotoGeolocation>()))
                    .Merge();
        }

        private PhotoGeolocation MapToDomainResult(PhotoLocationResult result)
        {
            if (result.ErrorCode == 2)
            {
                throw new InvalidOperationException("Location for photo not found");
            }

            return new PhotoGeolocation(result.Data.Id, result.Data.Location.Latitude, result.Data.Location.Longitude);
        }
    }
}
