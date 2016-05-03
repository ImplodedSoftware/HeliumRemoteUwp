using System;
using Windows.UI.Xaml.Data;

namespace HeliumRemote.Converters
{
    public class BoolToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return 0.3;
            var b = (bool) value;
            if (b)
                return 1.0;
            return 0.3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
