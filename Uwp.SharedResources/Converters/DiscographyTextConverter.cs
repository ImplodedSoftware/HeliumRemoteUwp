using System;
using Windows.UI.Xaml.Data;

namespace Uwp.SharedResources.Converters
{
    public class DiscographyTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return string.Empty;
            if ((bool) value)
                return "Discography";
            return "Appearances";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
