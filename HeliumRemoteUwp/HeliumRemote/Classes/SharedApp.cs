using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HeliumRemote.Helpers;
using NeonShared.Pcl.Types;
using Uwp.SharedResources.Interfaces;
using Uwp.SharedResources.Types;
using System;

namespace HeliumRemote.Classes
{
    public class SharedApp : ISharedApp
    {
        public int ActiveId => ((App) Application.Current).ActiveId;

        public UwpViewTypes ActiveViewType
        {
            get { return ((App) Application.Current).ActiveViewType; }
            set { ((App) Application.Current).ActiveViewType = value; }
        }

        public Frame ContentFrame => ((App)Application.Current).ContentFrame;

        public void UpdatePageTitle(string title)
        {
            AppHelpers.UpdatePageTitle(title);
        }

        public IViewFilter ViewFilter
        {
            get { return ((App)Application.Current).ViewFilter; }
            set { ((App)Application.Current).ViewFilter = value; }
        }

        public int LargeImageSize { get { return AppHelpers.LargeImageSize; } }

        public int LargeImageSizeDownload
        {
            get { return (int)AppHelpers.LargeImageSizeDownload; }
        }
    }
}