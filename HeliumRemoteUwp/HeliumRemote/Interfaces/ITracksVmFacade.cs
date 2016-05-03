using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HeliumRemote.Types;
using NeonShared.Types;

namespace HeliumRemote.Interfaces
{
    public interface ITracksVmFacade
    {
        ObservableCollection<TrackContainer> Tracks { get; }
        Task Refresh(ViewParameters parameters);

    }
}
