using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;

namespace NeonShared.ViewModels
{
    public class AlbumListVm : IAlbumListVm
    {
        private readonly IWebService _webService;

        public AlbumListVm(IWebService webService)
        {
            _webService = webService;
        }

        public IEnumerable<Album> Albums { get; private set; }

        public async Task Refresh(ViewParameters param)
        {
            if (param.ViewType == UwpViewTypes.YearLetters)
                Albums = await _webService.AlbumsFromYear(param.Value);
            else if (param.ViewType == UwpViewTypes.AddedDateDayLetters)
                Albums = await _webService.AlbumsByAddedDate(param.Letter);
            else if (param.ViewType == UwpViewTypes.FavouriteAlbums)
                Albums = await _webService.FavouriteAlbums();
            else if (param.ViewType == UwpViewTypes.LabelsByLetter)
                Albums = await _webService.AlbumsFromLabel(param.Letter);
            else
                Albums = await _webService.AlbumsByLetter(param.Letter);
        }
    }
}