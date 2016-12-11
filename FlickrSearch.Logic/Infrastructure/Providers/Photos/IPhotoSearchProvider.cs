using System.Threading.Tasks;
using FlickrSearch.Logic.DataAccessLayer.DTO.Photos;


namespace FlickrSearch.Logic.Infrastructure.Providers.Photos
{
    public interface IPhotoSearchProvider
    {
        Task<PhotoSearchResult> SearchPhotosAsync(string title, int page);
        Task<PhotoLocationResult> GetPhotoLocationAsync(string photoId);
    }
}