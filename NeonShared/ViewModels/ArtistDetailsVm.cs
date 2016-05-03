using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;

namespace NeonShared.ViewModels
{
    public class ArtistDetailsVm : IArtistDetailsVm
    {
        private IWebService _webService;
        public ArtistDetailsVm(IWebService webService)
        {
            _webService = webService;
        }
        public Artist Artist { get; private set; }
        public async Task Refresh(int id)
        {
            Artist = await _webService.ArtistDetails(id);
        }
    }
}
