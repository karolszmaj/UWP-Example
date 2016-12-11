using Newtonsoft.Json;

namespace FlickrSearch.Logic.DataAccessLayer.DTO.Photos
{
    public class PhotoSearchResult: StatusResponse
    {
        [JsonProperty("photos")]
        public PhotoSet Photos { get; set; }
    }
}
