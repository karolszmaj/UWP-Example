using System;
using FlickrSearch.Logic.BusinessLogic.Models;
using FlickrSearch.Logic.Infrastructure.Query;

namespace FlickrSearch.Logic.BusinessLogic.Queries.SearchPhotos
{
    public class SearchPhotosQuery: IQuery<IObservable<PhotoSearchResult>>
    {
        public string Text { get; private set; }
        public int Page { get; private set; }

        public SearchPhotosQuery(string text, int page)
        {
            Text = text;
            Page = page;
        }
    }
}
