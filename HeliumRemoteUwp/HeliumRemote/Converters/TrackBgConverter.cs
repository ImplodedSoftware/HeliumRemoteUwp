using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using HeliumRemote.Classes;
using UwpSharedViews.Classes;

namespace HeliumRemote.Converters
{
    public class TrackBgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return Application.Current.Resources["RowColor1Brush"];
            var item = (int)value;
            if (item == AppConstants.ACTIVE_ID_DECORATOR)
                return Application.Current.Resources["PlayingTrackBgBrush"];
            if ((item & 1) == 1)
                return Application.Current.Resources["RowColor1Brush"];
            return Application.Current.Resources["RowColor2Brush"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
