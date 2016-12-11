namespace FlickrSearch.Logic.BusinessLogic.Models
{
    public class PhotoGeolocation
    {
        public string PhotoId { get; private set; }
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        public PhotoGeolocation(string photoId, float latitude, float longitude)
        {
            PhotoId = photoId;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
