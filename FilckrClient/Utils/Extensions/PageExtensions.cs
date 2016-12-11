using Windows.UI.Xaml.Controls;

namespace FilckrClient.Utils.Extensions
{
    public static class PageExtensions
    {
        public static T GetViewModel<T>(this Page page)
        {
            return (T) page.DataContext;
        }
    }
}
