using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Pcl.Helpers;
using NeonShared.Pcl.Interfaces;
using NeonShared.Pcl.Types;

namespace NeonShared.Pcl.ViewModels
{
    public class LetterVm : ILetterVm
    {
        private readonly IWebService _webService;

        public LetterVm(IWebService webService)
        {
            _webService = webService;
        }

        public async Task Populate(UwpViewTypes viewType, ViewParameters param)
        {
            List<string> lst = null;
            switch (viewType)
            {
                case UwpViewTypes.ArtistLetters:
                    lst = (List<string>) await _webService.ArtistLetters();
                    break;
                case UwpViewTypes.AlbumLetters:
                    lst = (List<string>) await _webService.AlbumLetters();
                    break;
                case UwpViewTypes.AlbumArtistLetters:
                    lst = (List<string>) await _webService.AlbumArtistLetters();
                    break;
                case UwpViewTypes.GenreLetters:
                    var tmplst = (List<Genre>) await _webService.GenreLetters();
                    lst = new List<string>();
                    foreach (var genre in tmplst)
                    {
                        lst.Add(genre.Name);
                    }
                    break;
                case UwpViewTypes.LabelLetters:
                    lst = (List<string>) await _webService.LabelLetters();
                    break;
                case UwpViewTypes.LabelsByLetter:
                    lst = (List<string>) await _webService.LabelsByLetter(param.Letter);
                    break;
                case UwpViewTypes.RatingLetters:
                    lst = await _webService.GetRatingLetters();
                    break;
                case UwpViewTypes.YearLetters:
                    lst = await _webService.GetYearLetters();
                    break;
                case UwpViewTypes.AddedDateYearLetters:
                    lst = await _webService.GetAddedYearLetters();
                    break;
                case UwpViewTypes.AddedDateMonthLetters:
                    lst = await _webService.GetAddedMonthLetters(param.Value);
                    break;
                case UwpViewTypes.AddedDateDayLetters:
                    lst = await _webService.GetAddedDateLetters(param.ParentValue, param.Value);
                    break;
                case UwpViewTypes.PlayedDateYearLetters:
                    lst = await _webService.GetPlayedYearLetters();
                    break;
                case UwpViewTypes.PlayedDateMonthLetters:
                    lst = await _webService.GetPlayedMonthLetters(param.Value);
                    break;
                case UwpViewTypes.PlayedDateDayLetters:
                    lst = await _webService.GetPlayedDateLetters(param.ParentValue, param.Value);
                    break;
            }
            var res = new List<LetterContainerItem>();
            var idx = 0;
            foreach (var letter in lst)
            {
                if (viewType == UwpViewTypes.RatingLetters)
                {
                    var v = int.Parse(letter);
                    res.Add(new LetterContainerItem {Index = idx++, Letter = NeonHelpers.GetRatingName(v), Value = v});
                }
                else if (viewType == UwpViewTypes.AddedDateMonthLetters ||
                         viewType == UwpViewTypes.PlayedDateMonthLetters)
                {
                    var v = int.Parse(letter);
                    res.Add(new LetterContainerItem {Index = idx++, Letter = NeonHelpers.GetMonthName(v), Value = v});
                }
                else if (viewType == UwpViewTypes.YearLetters || viewType == UwpViewTypes.AddedDateYearLetters ||
                         viewType == UwpViewTypes.PlayedDateYearLetters)
                {
                    res.Add(new LetterContainerItem {Index = idx++, Letter = letter, Value = int.Parse(letter)});
                }
                else
                {
                    res.Add(new LetterContainerItem {Index = idx++, Letter = letter});
                }
            }
            Letters = new List<LetterContainerItem>(res);
        }

        public IEnumerable<LetterContainerItem> Letters { get; private set; }
    }
}