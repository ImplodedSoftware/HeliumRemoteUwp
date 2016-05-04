using System;
using Windows.UI.Xaml.Data;
using NeonShared.Pcl.Helpers;
using Uwp.SharedResources.Classes;

namespace Uwp.SharedResources.Converters
{
    public class AlbumImageDownloadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var id = (int) value;
            if (id <= 0)
                return string.Empty;
            var url = string.Empty;
            if (parameter == null)
                url = NeonUrls.AlbumImage(id, AppConstants.SMALL_IMAGE_SIZE);
            else
            {
                var param = (string) parameter;
                if (param.Equals("large", StringComparison.CurrentCultureIgnoreCase))
                    url = NeonUrls.LargeAlbumImage(id);
                else if (param.Equals("medium", StringComparison.CurrentCultureIgnoreCase))
                    url = NeonUrls.AlbumImage(id, AppConstants.MEDIUM_IMAGE_SIZE);
                else if (param.Equals("mega", StringComparison.CurrentCultureIgnoreCase))
                    url = NeonUrls.AlbumImage(id, (int)SharedRepository.Instance.Repository.SharedApp.LargeImageSizeDownload);
            }
            return url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return "ms-appx:///Images/s_no_album";
        }
    }
}
