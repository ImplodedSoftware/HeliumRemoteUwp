using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using HeliumRemote.Types;
using NeonShared.Types;

namespace HeliumRemote.Interfaces
{
    public interface ITracksVmFacade
    {
        ObservableCollection<TrackContainer> Tracks { get; }
        Task Refresh(ViewParameters parameters);
        Thickness ElementMargin { get; set; }
        void AdjustUiParts();

    }
}
