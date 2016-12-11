using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using FlickrSearch.Logic.BusinessLogic.Models;
using FlickrSearch.Logic.DataAccessLayer.Utils;
using FlickrSearch.Logic.Infrastructure.Providers.Photos;
using FlickrSearch.Logic.Infrastructure.Query;

namespace FlickrSearch.Logic.BusinessLogic.Queries.SearchPhotos
{
    internal class SearchPhotosQueryHandler : IQueryHandler<SearchPhotosQuery, IObservable<PhotoSearchResult>>
    {
        private readonly IPhotoSearchProvider _searchProvider;

        public SearchPhotosQueryHandler(IPhotoSearchProvider searchProvider)
        {
            _searchProvider = searchProvider;
        }

        public IObservable<PhotoSearchResult> Handle(SearchPhotosQuery query)
        {
            return
                _searchProvider.SearchPhotosAsync(query.Text, query.Page)
                    .ToObservable()
                    .Select(x => new PhotoSearchResult(query.Page*100,
                        int.Parse(x.Photos.TotalPhotos),
                        MapToDomainResult(x)));
        }

        private IEnumerable<Photo> MapToDomainResult(DataAccessLayer.DTO.Photos.PhotoSearchResult data)
        {
            return data.Photos.Photos.Select(x => new Photo()
            {
                Title = x.Title,
                Id = x.Id,
                MediumPhotoUrl = new UrlFormatter(x.Farm, x.Server, x.Id, x.Secret, "m").GenerateUrl(),
                SmallPhotoUrl = new UrlFormatter(x.Farm, x.Server, x.Id, x.Secret, "q").GenerateUrl(),
                LargePhotoUrl = new UrlFormatter(x.Farm, x.Server, x.Id, x.Secret, "b").GenerateUrl()
            });
        }
    }
}
