using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;

namespace NeonShared.ViewModels
{
    public class TracksVm : ITracksVm
    {
        private readonly IWebService _webService;

        public TracksVm(IWebService webService)
        {
            _webService = webService;
        }
        public IEnumerable<Track> Tracks { get; private set; }
        public async Task Populate(ViewTypeTracks vt, ViewParameters param)
        {
            if (vt == ViewTypeTracks.Genre)
                Tracks = await _webService.TracksFromGenre(param.Letter);
            else if (vt == ViewTypeTracks.Rating)
                Tracks = await _webService.TracksByRating(param.Value);
            else if (vt == ViewTypeTracks.PlayedDate)
                Tracks = await _webService.TracksByPlayedDate(param.Letter);
            else if (vt == ViewTypeTracks.FavouriteTracks)
                Tracks = await _webService.FavouriteTracks();
            else if (vt == ViewTypeTracks.Playlists)
                Tracks = await _webService.PlaylistTracks(param.Value);
            else if (vt == ViewTypeTracks.SmartPlaylists)
                Tracks = await _webService.SmartPlaylistTracks(param.Value);
        }
    }
}
