using System.Threading.Tasks;
using FlickrSearch.Logic.DataAccessLayer.DTO.Photos;
using FlickrSearch.Logic.Infrastructure.Providers.Photos;

namespace FlickrSearch.Logic.DataAccessLayer.Providers.Photos
{
    public class PhotoSearchProvider : BaseApiProvider, IPhotoSearchProvider
    {
        const string ENDOINT_METHOD = "https://api.flickr.com/services/rest/";
        public PhotoSearchProvider() : base(ENDOINT_METHOD, "eceb9db0d3f23cfd7ae2c442e4f4e475")
        {
        }

        public Task<PhotoSearchResult> SearchPhotosAsync(string title, int page)
        {
            var destinationUrl = $"{_endpoint}?method=flickr.photos.search&format=json&nojsoncallback=1&api_key={_apiKey}&text={title}&page={page}&sort=interestingness-desc";
            return ExecuteAsync<PhotoSearchResult>(destinationUrl);
        }

        public Task<PhotoLocationResult> GetPhotoLocationAsync(string photoId)
        {
            var destinationUrl = $"{_endpoint}?method=flickr.photos.geo.getLocation&format=json&nojsoncallback=1&api_key={_apiKey}&photo_id={photoId}";
            return ExecuteAsync<PhotoLocationResult>(destinationUrl);
        }
    }
}
