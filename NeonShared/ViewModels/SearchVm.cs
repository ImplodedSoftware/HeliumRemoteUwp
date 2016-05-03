using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;

namespace NeonShared.ViewModels
{
    public class SearchVm : ISearchVm
    {
        private readonly IWebService _webService;

        public SearchVm(IWebService webService)
        {
            _webService = webService;
        }

        public IEnumerable<Track> Tracks { get; private set; }
        public IEnumerable<Album> Albums { get; private set; }
        public IEnumerable<Artist> Artists { get; private set; }
        public async Task Populate(ViewParameters param)
        {
            Tracks = await _webService.TrackSearch(param.Letter);
            Artists = await _webService.ArtistSearch(param.Letter);
            Albums = await _webService.AlbumSearch(param.Letter);
        }
    }
}
