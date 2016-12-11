using System;
using FlickrSearch.Logic.BusinessLogic.Models;
using FlickrSearch.Logic.Infrastructure.Query;

namespace FlickrSearch.Logic.BusinessLogic.Queries.GetPhotoLocation
{
    public class GetPhotoLocationQuery : IQuery<IObservable<PhotoGeolocation>>
    {
        public string PhotoId { get; private set; }

        public GetPhotoLocationQuery(string photoId)
        {
            PhotoId = photoId;
        }
    }
}
