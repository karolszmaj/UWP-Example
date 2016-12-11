using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FilckrClient.Utils.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> data)
        {
            foreach (var element in data)
            {
                collection.Add(element);
            }
        }
    }
}
