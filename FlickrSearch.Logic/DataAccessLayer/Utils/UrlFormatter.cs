using System;
using static System.String;

namespace FlickrSearch.Logic.DataAccessLayer.Utils
{
    public class UrlFormatter
    {
        private readonly uint _farmId;
        private readonly string _serverId;
        private readonly string _photoId;
        private readonly string _secret;
        private readonly string _size;

        public UrlFormatter(uint farmId, string serverId, string photoId, string secret, string size)
        {
            _farmId = farmId;
            _serverId = serverId;
            _photoId = photoId;
            _secret = secret;
            _size = size;

            if (IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException("secret", "Parameter secret is required!");
            }
        }

        public string GenerateUrl()
        {
            var url = $"https://farm{_farmId}.staticflickr.com/{_serverId}/{_photoId}_{_secret}";
            if (IsNullOrEmpty(_size) == false)
            {
                url += "_" + _size;
            }

            url += ".jpg";
            return url;
        }
    }
}
