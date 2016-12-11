using Newtonsoft.Json;

namespace FlickrSearch.Logic.DataAccessLayer.DTO
{
    public class StatusResponse
    {
        //ok or fail
        [JsonProperty("stat")]
        public string Status { get; set; }

        [JsonProperty("code")]
        public int? ErrorCode { get; set; }

        [JsonProperty("message")]
        public string ErrorMessage { get; set; }
    }
}
