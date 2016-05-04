using Windows.UI.Xaml.Controls;
using NeonShared.Pcl.Types;
using Uwp.SharedResources.Types;

namespace Uwp.SharedResources.Interfaces
{
    public interface  ISharedApp
    {
        int ActiveId { get; }
        UwpViewTypes ActiveViewType { get; set; }
        Frame ContentFrame { get; }
        void UpdatePageTitle(string title);
        IViewFilter ViewFilter { get; set; }
        int LargeImageSize { get; }
        int LargeImageSizeDownload { get; }
    }
}
