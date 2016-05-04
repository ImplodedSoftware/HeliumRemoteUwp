using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using NeonShared.Types;
using UwpSharedViews.Types;

namespace UwpSharedViews.Interfaces
{
    public interface ITracksVmFacade
    {
        ObservableCollection<TrackContainer> Tracks { get; }
        Task Refresh(ViewParameters parameters);
        Thickness ElementMargin { get; set; }
        void AdjustUiParts();

    }
}
