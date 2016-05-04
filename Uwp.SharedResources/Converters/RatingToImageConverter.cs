using System;
using Windows.UI.Xaml.Data;
using NeonShared.Pcl.Helpers;

namespace Uwp.SharedResources.Converters
{
    public class RatingToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var rating = (int)value;
            rating = NeonHelpers.DownsizeRating(rating);
            if (parameter == null)
                return string.Format("/Images/a{0:0000}.png", rating);
            return string.Format("/Images/hr{0}.png", rating);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
