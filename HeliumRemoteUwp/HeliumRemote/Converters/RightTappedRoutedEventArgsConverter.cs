using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace HeliumRemote.Converters
{
    public sealed class RightTappedRoutedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = value as RightTappedRoutedEventArgs;
            if (args == null)
                throw new ArgumentException("Value is not RightTappedRoutedEventArgs");
            return null;
            //if (args.ClickedItem is TestItem)
            //{
            //    var selectedItem = args.ClickedItem as TestItem;
            //    return selectedItem;
            //}
            //else
            //    return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
