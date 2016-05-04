using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Uwp.SharedResources.ViewModels;

namespace Uwp.SharedResources.Converters
{
    public class SearchViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return Visibility.Collapsed;
            var thisType = (SearchViews)value;
            var thisTypeString = thisType.ToString();
            var destType = (string) parameter;
            if (destType == thisTypeString)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
