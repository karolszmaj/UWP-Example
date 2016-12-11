using System.Collections.Generic;

namespace FlickrSearch.Logic.BusinessLogic.Models
{
    public class PhotoSearchResult
    {
        public int Offset { get; private set; }
        public int Limit { get; private set; }
        public IEnumerable<Photo> Photos { get; private set; }

        public PhotoSearchResult(int offset, int limit, IEnumerable<Photo> photos )
        {
            Offset = offset;
            Limit = limit;
            Photos = photos;
        }
    }
}
