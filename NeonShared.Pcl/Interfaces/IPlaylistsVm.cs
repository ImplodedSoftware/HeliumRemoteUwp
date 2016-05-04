using System.Collections.Generic;
using System.Threading.Tasks;
using NeonShared.Pcl.Types;

namespace NeonShared.Pcl.Interfaces
{
    public interface IPlaylistsVm
    {
        Task Populate(UwpViewTypes viewType, ViewParameters param);
        IEnumerable<PlaylistContainerItem> Playlists { get; }
    }
}
