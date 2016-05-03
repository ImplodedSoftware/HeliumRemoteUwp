using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using HeliumRemote.Helpers;
using HeliumRemote.Interfaces;
using HeliumRemote.Views;
using NeonShared.Interfaces;
using NeonShared.Types;

namespace HeliumRemote.ViewModels
{
    public class LetterFacadeVm : ViewModelBase, ILetterFacadeVm
    {
        private readonly ILetterVm _letterVm;
        private ViewParameters _parameters;

        private ObservableCollection<LetterContainerItem> _letters;

        public LetterFacadeVm(ILetterVm letterVm)
        {
            _letterVm = letterVm;
        }

        public async Task Populate(ViewParameters param)
        {
            _parameters = param;
            await _letterVm.Populate(ViewType, param);
            Letters = new ObservableCollection<LetterContainerItem>(_letterVm.Letters);
            ((App) Application.Current).ActiveViewType = param.ViewType;
        }

        public ObservableCollection<LetterContainerItem> Letters
        {
            get { return _letters; }
            private set
            {
                _letters = value;
                RaisePropertyChanged();
            }
        }

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LetterContainerItem item = null;
            if (sender is ListBox)
            {
                var lst = (ListBox) sender;
                item = (LetterContainerItem) lst.SelectedItem;
            }
            else if (sender is ListView)
            {
                var lv = (ListView) sender;
                item = (LetterContainerItem) lv.SelectedItem;
            }
            if (item == null)
                return;
            switch (ViewType)
            {
                case UwpViewTypes.AlbumLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (AlbumListPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.AlbumLetters});
                    break;
                case UwpViewTypes.ArtistLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (ArtistListPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.ArtistLetters});
                    break;
                case UwpViewTypes.AlbumArtistLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (ArtistListPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.AlbumArtistLetters});
                    break;
                case UwpViewTypes.GenreLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (TracksPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.GenreLetters});
                    break;
                case UwpViewTypes.LabelLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (LettersPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.LabelsByLetter});
                    break;
                case UwpViewTypes.LabelsByLetter:
                    AppHelpers.ContentFrame.Navigate(typeof (AlbumListPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.LabelsByLetter});
                    break;
                case UwpViewTypes.RatingLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (TracksPage),
                        new ViewParameters {Value = item.Value, ViewType = UwpViewTypes.RatingLetters});
                    break;
                case UwpViewTypes.YearLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (AlbumListPage),
                        new ViewParameters
                        {
                            Letter = item.Letter,
                            Value = item.Value,
                            ViewType = UwpViewTypes.YearLetters
                        });
                    break;
                case UwpViewTypes.AddedDateYearLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (LettersPage),
                        new ViewParameters
                        {
                            Letter = item.Letter,
                            Value = item.Value,
                            ViewType = UwpViewTypes.AddedDateMonthLetters
                        });
                    break;
                case UwpViewTypes.AddedDateMonthLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (LettersPage),
                        new ViewParameters
                        {
                            Letter = item.Letter,
                            Value = item.Value,
                            ParentValue = _parameters.Value,
                            ViewType = UwpViewTypes.AddedDateDayLetters
                        });
                    break;
                case UwpViewTypes.AddedDateDayLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (AlbumListPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.AddedDateDayLetters});
                    break;
                case UwpViewTypes.PlayedDateYearLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (LettersPage),
                        new ViewParameters
                        {
                            Letter = item.Letter,
                            Value = item.Value,
                            ViewType = UwpViewTypes.PlayedDateMonthLetters
                        });
                    break;
                case UwpViewTypes.PlayedDateMonthLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (LettersPage),
                        new ViewParameters
                        {
                            Letter = item.Letter,
                            Value = item.Value,
                            ParentValue = _parameters.Value,
                            ViewType = UwpViewTypes.PlayedDateDayLetters
                        });
                    break;
                case UwpViewTypes.PlayedDateDayLetters:
                    AppHelpers.ContentFrame.Navigate(typeof (TracksPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.PlayedDateDayLetters});
                    break;
            }
        }

        public UwpViewTypes ViewType { get; set; }
    }
}