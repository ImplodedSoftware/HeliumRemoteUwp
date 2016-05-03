﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;

namespace NeonShared.ViewModels
{
    public class ArtistListVm : IArtistListVm
    {
        private IWebService _webService;

        public ArtistListVm(IWebService webService)
        {
            _webService = webService;
        }
        public IEnumerable<Artist> Artists { get; private set; }
        public async Task Refresh(ViewParameters parameters)
        {
            IEnumerable<Artist> res = null;
            switch (parameters.ViewType)
            {
                case UwpViewTypes.ArtistLetters:
                    res = await _webService.ArtistsByLetter(parameters.Letter);
                    break;
                case UwpViewTypes.AlbumArtistLetters:
                    res = await _webService.AlbumArtistsByLetter(parameters.Letter);
                    break;
                case UwpViewTypes.FavouriteArtists:
                    res = await _webService.FavouriteArtists();
                    break;
            }
            Artists = res;
        }
    }
}
