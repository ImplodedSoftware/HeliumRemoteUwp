using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Pcl.Interfaces;

namespace NeonShared.Pcl.ViewModels
{
    public class AlbumDetailsVm : IAlbumDetailsVm
    {
        private readonly IWebService _webService;

        public AlbumDetailsVm(IWebService webService)
        {
            _webService = webService;
        }

        public Album Album { get; private set; }
        public IEnumerable<Track> Tracks { get; private set; }

        public async Task Refresh(int id)
        {
            Album = await _webService.AlbumDetails(id);
            Tracks = await _webService.TracksForAlbum(id);
        }
    }
}