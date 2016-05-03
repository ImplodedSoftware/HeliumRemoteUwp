using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;

namespace NeonShared.ViewModels
{
    public class PlaylistsVm : IPlaylistsVm
    {
        readonly IWebService _webService;

        public PlaylistsVm(IWebService webService)
        {
            _webService = webService;
        }

        public async Task Populate(UwpViewTypes viewType, ViewParameters param)
        {
            IEnumerable<Playlist> playlists = null;
            if (param.ViewType == UwpViewTypes.Playlists)
                playlists = await _webService.Playlists();
            else if (param.ViewType == UwpViewTypes.SmartPlaylists)
                playlists = await _webService.SmartPlaylists();
            var res = new List<PlaylistContainerItem>();
            var idx = 0;
            foreach (var pls in playlists)
            {
                res.Add(new PlaylistContainerItem { Index = idx++, Playlist = pls });
            }
            Playlists = new List<PlaylistContainerItem>(res);
        }

        public IEnumerable<PlaylistContainerItem> Playlists { get; private set; }
    }
}
