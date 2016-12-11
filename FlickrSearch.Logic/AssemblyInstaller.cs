using System;
using FlickrClient.Framework;
using FlickrSearch.Logic.BusinessLogic.Models;
using FlickrSearch.Logic.BusinessLogic.Queries.GetPhotoLocation;
using FlickrSearch.Logic.BusinessLogic.Queries.SearchPhotos;
using FlickrSearch.Logic.DataAccessLayer.Providers.Photos;
using FlickrSearch.Logic.Infrastructure.Providers.Photos;
using FlickrSearch.Logic.Infrastructure.Query;

namespace FlickrSearch.Logic
{
    internal class AssemblyInstaller: IAssemblyInstaller
    {
        //each assembly should register types itself
        public void InstallDependencies(IContainer container)
        {
            container.RegisterInstance<IQueryProcessor, QueryProcessor>(new QueryProcessor(container.GetInstance));
            container.RegisterPerRequest<IPhotoSearchProvider, PhotoSearchProvider>();

            //Queries
            container.RegisterPerRequest<IQueryHandler<SearchPhotosQuery, IObservable<PhotoSearchResult>>, SearchPhotosQueryHandler>();
            container.RegisterPerRequest<IQueryHandler<GetPhotoLocationQuery, IObservable<PhotoGeolocation>>, GetPhotoLocationQueryHandler>();
        }
    }
}
