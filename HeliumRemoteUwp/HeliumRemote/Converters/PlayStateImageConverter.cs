using System;
using Windows.UI.Xaml.Data;

namespace HeliumRemote.Converters
{
    public class PlayStateImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return string.Empty;
            if (parameter == null)
            {
                if ((int) value == 1)
                    return "/Images/ic_action_pause.png";
                return "/Images/ic_action_play.png";
            }
            if ((string) parameter == "large")
            {
                if ((int)value == 1)
                    return "/Images/nbtn_pause.png";
                return "/Images/nbtn_play.png";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
