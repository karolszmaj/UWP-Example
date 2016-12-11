using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using FilckrClient.MVVM;
using FilckrClient.Presentation.DataSource;
using FilckrClient.Utils.Extensions;
using FlickrSearch.Logic.BusinessLogic.Models;
using FlickrSearch.Logic.BusinessLogic.Queries.SearchPhotos;
using FlickrSearch.Logic.Infrastructure.Query;

namespace FilckrClient.Presentation.Views.HubPage
{
    public class HubPageViewModel : ViewModel
    {
        private readonly IQueryProcessor _queryProcessor;
        private IncrementalPhotosDataSource _photos;
        private int _currentPhotosPage;
        private string _currentSearchQuery = string.Empty;
        private bool isLoading;
        private bool _allowRefresh;

        public bool AllowRefresh
        {
            get { return _allowRefresh; }
            set
            {
                _allowRefresh = value;
                NotifyPropertyChanged();
            }
        }

        public IncrementalPhotosDataSource Photos
        {
            get { return _photos; }
            set
            {
                _photos = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                NotifyPropertyChanged();
            }
        }


        public HubPageViewModel(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
            Photos = new IncrementalPhotosDataSource(Enumerable.Empty<Photo>(), FetchMorePhotos);
        }

        public void SearchPhotos(string query)
        {
            if (query.Length < 3)
            {
                OnDisplayMessage("You have to type atleast 3 characters!");
                return;
            }

            AllowRefresh = false;
            Photos.Clear();
            IsLoading = true;
            _currentSearchQuery = query;
            _currentPhotosPage = 1;
            Photos.CanFetchForMoreItems(false);

            _queryProcessor.Process(new SearchPhotosQuery(query, 1))
                .ObserveOnDispatcher()
                .Finally(() =>
                {
                    IsLoading = false;
                })
                .Subscribe(result =>
                {
                    Photos.AddRange(result.Photos);
                    Photos.CanFetchForMoreItems(true);
                }, error =>
                {
                    if (error is HttpRequestException)
                    {
                        OnDisplayMessage("Sorry but there is no Internet connection. Check your internet connection and use refresh button below.");
                    }
                    AllowRefresh = true;
                });
        }

        private Task<IEnumerable<Photo>> FetchMorePhotos()
        {
            if (string.IsNullOrEmpty(_currentSearchQuery))
            {
                return Task.FromResult(Enumerable.Empty<Photo>());
            }

            var observable = _queryProcessor.Process(new SearchPhotosQuery(_currentSearchQuery, _currentPhotosPage + 1))
                .Select(x => x.Photos);

            observable.Subscribe(sucessful =>
            {
                //we increment only if there was sucessful fetch.
                _currentPhotosPage++;
            });

            return observable.ToTask();
        }

        protected override async Task OnInitialize()
        {
            await base.OnInitialize();
            SearchPhotos("Berlin");
        }
    }
}
