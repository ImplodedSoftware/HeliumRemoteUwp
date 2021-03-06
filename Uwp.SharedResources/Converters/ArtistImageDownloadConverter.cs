﻿using System;
using Windows.UI.Xaml.Data;
using NeonShared.Pcl.Helpers;
using Uwp.SharedResources.Classes;

namespace Uwp.SharedResources.Converters
{
    public class ArtistImageDownloadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var id = (int)value;
            var url = string.Empty;
            if (parameter == null)
                url = NeonUrls.ArtistImage(id, AppConstants.SMALL_IMAGE_SIZE);
            else
            {
                var param = (string) parameter;
                if (param.Equals("medium"))
                    url = NeonUrls.ArtistImage(id, AppConstants.MEDIUM_IMAGE_SIZE);
                else if (param.Equals("mega", StringComparison.CurrentCultureIgnoreCase))
                    url = NeonUrls.ArtistImage(id, (int)SharedRepository.Instance.Repository.SharedApp.LargeImageSizeDownload);
                else
                    url = NeonUrls.LargeArtistImage(id);
            }
            return url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
