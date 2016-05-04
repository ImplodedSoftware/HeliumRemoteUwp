using System;
using Windows.UI.Xaml.Data;
using NeonShared.Pcl.Helpers;

namespace Uwp.SharedResources.Converters
{
    public class DurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return "00:00";
            return NeonHelpers.FormatTime((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
