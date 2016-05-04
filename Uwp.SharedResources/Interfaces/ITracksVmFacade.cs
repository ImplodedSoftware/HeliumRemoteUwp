using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using NeonShared.Pcl.Types;
using Uwp.SharedResources.Types;

namespace Uwp.SharedResources.Interfaces
{
    public interface ITracksVmFacade
    {
        ObservableCollection<TrackContainer> Tracks { get; }
        Task Refresh(ViewParameters parameters);
        Thickness ElementMargin { get; set; }
        void AdjustUiParts();

    }
}
