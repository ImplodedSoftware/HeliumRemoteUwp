using System;

using Windows.UI.Xaml.Data;

namespace Uwp.SharedResources.Converters
{
    public class NowPlayingFavouriteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return "/Images/ad_fav_d.png";
            if ((bool)value)
                return "/Images/ad_fav.png";
            return "/Images/ad_fav_d.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
