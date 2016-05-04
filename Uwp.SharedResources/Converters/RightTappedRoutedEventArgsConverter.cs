using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace Uwp.SharedResources.Converters
{
    public sealed class RightTappedRoutedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = value as RightTappedRoutedEventArgs;
            if (args == null)
                throw new ArgumentException("Value is not RightTappedRoutedEventArgs");
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
