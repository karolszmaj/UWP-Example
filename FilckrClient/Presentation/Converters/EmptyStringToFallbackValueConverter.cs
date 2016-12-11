using System;
using Windows.UI.Xaml.Data;

namespace FilckrClient.Presentation.Converters
{
    public class EmptyStringToFallbackValueConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && parameter != null && String.IsNullOrEmpty(value.ToString()))
            {
                return parameter.ToString();
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //dont care about this right now.
            return value;
        }
    }
}
