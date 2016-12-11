using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using FilckrClient.Utils.Extensions;
using FlickrSearch.Logic.BusinessLogic.Models;

namespace FilckrClient.Presentation.DataSource
{
    public class IncrementalPhotosDataSource : ObservableCollection<Photo>, ISupportIncrementalLoading
    {
        private readonly Func<Task<IEnumerable<Photo>>> _fetchDelegate;
        private readonly CoreDispatcher _dispatcher;
        public bool HasMoreItems { get; private set; }

        public IncrementalPhotosDataSource(IEnumerable<Photo> photos, Func<Task<IEnumerable<Photo>>> fetchDelegate)
        {
            _fetchDelegate = fetchDelegate;
            _dispatcher = Window.Current.Dispatcher;
            this.AddRange(photos);
            HasMoreItems = false;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run(FetchMoreData).AsAsyncOperation();
        }

        private async Task<LoadMoreItemsResult> FetchMoreData()
        {
            Debug.WriteLine("IncrementalPhotosDataSource: 1. Started to fetch more data from data source.");
            IEnumerable<Photo> data = Enumerable.Empty<Photo>();

            try
            {
                data = await _fetchDelegate();
            }
            catch (Exception)
            {
                //all exceptions should be handled in viewModel
            }

            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.AddRange(data);
            }).AsTask().ConfigureAwait(false);
            
            Debug.WriteLine("IncrementalPhotosDataSource: 2. Finished adding new data to datasoruce");

            return new LoadMoreItemsResult()
            {
                Count = (uint) data.Count()
            };
        }

        public void CanFetchForMoreItems(bool enableIncrementalLoading)
        {
            HasMoreItems = enableIncrementalLoading;
        }
    }
}
