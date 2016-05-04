using System;
using Windows.UI.Xaml.Data;
using NeonShared.Pcl.Helpers;

namespace Uwp.SharedResources.Converters
{
    public class RatingToImRatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return 0;
            var lr = (int) value;
            var dsr = NeonHelpers.DownsizeRating(lr);
            return dsr/2.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
