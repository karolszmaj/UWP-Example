using Newtonsoft.Json;

namespace FlickrSearch.Logic.DataAccessLayer.DTO.Photos
{
    public class PhotoLocationResult : StatusResponse
    {
        public class PhotoWithLocation
        {
            public class PhotoLocation
            {
                public float Latitude { get; set; }
                public float Longitude { get; set; }
            }

            public string Id { get; set; }

            public PhotoLocation Location { get; set; }

        }

        [JsonProperty("photo")]
        public PhotoWithLocation Data { get; set; }
    }
}


