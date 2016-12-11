using System.Collections.Generic;
using Newtonsoft.Json;

namespace FlickrSearch.Logic.DataAccessLayer.DTO.Photos
{
    public class PhotoSet
    {
        [JsonProperty("page")]
        public uint CurrentPage { get; set; }

        [JsonProperty("pages")]
        public uint TotalPages { get; set; }

        [JsonProperty("perpage")]
        public uint PhotosPerPage { get; set; }

        [JsonProperty("total")]
        public string TotalPhotos { get; set; }

        [JsonProperty("photo")]
        public IEnumerable<Photo> Photos { get; set; }
    }
}
