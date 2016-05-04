using System;
using Windows.UI.Xaml.Data;
using NeonShared.Pcl.Helpers;

namespace Uwp.SharedResources.Converters
{
    public class TooltipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return string.Empty;
            var d = (double) value;
            return NeonHelpers.FormatTime((long) d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
