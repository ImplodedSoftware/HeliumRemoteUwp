using System.Collections.Generic;
using System.Threading.Tasks;
using NeonShared.Types;

namespace NeonShared.Interfaces
{
    public interface IPlaylistsVm
    {
        Task Populate(UwpViewTypes viewType, ViewParameters param);
        IEnumerable<PlaylistContainerItem> Playlists { get; }
    }
}
