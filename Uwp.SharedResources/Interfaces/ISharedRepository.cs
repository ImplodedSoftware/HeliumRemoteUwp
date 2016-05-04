using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeonShared.Pcl.Interfaces;
using UwpSharedViews.Interfaces;

namespace Uwp.SharedResources.Interfaces
{
    public interface ISharedRepository
    {
        IWebService WebService { get; }
        ILetterFacadeVm LetterFacadeVm { get; }
        ISharedApp SharedApp { get; }
        ITracksVmFacade TracksVmFacade { get; }
        IAlbumListFacadeVm AlbumListFacadeVm { get; }
        IArtistListFacadeVm ArtistListFacadeVm { get; }
        IAlbumDetailsFacadeVm AlbumDetailsFacadeVm { get; }
        IArtistDetailsFacadeVm ArtistDetailsFacadeVm { get; }
        IPlaylistsFacadeVm PlaylistsFacadeVm { get; }
        ISearchFacadeVm SearchFacadeVm { get; }
    }
}
