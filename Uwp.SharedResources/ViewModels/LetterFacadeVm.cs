using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using NeonShared.Pcl.Interfaces;
using NeonShared.Pcl.Types;
using Uwp.SharedResources.Interfaces;
using Uwp.SharedResources.Types;
using Uwp.SharedResources.Views;

namespace Uwp.SharedResources.ViewModels
{
    public class LetterFacadeVm : ViewModelBase, ILetterFacadeVm
    {
        private readonly ILetterVm _letterVm;
        private readonly ISharedApp _sharedApp;
        private ViewParameters _parameters;

        private ObservableCollection<LetterContainerItem> _letters;

        public LetterFacadeVm(ILetterVm letterVm, ISharedApp sharedApp)
        {
            _letterVm = letterVm;
            _sharedApp = sharedApp;
        }

        public async Task Populate(ViewParameters param)
        {
            _parameters = param;
            await _letterVm.Populate(ViewType, param);
            Letters = new ObservableCollection<LetterContainerItem>(_letterVm.Letters);
            _sharedApp.ActiveViewType = param.ViewType;
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
                    _sharedApp.ContentFrame.Navigate(typeof (AlbumListPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.AlbumLetters});
                    break;
                case UwpViewTypes.ArtistLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (ArtistListPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.ArtistLetters});
                    break;
                case UwpViewTypes.AlbumArtistLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (ArtistListPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.AlbumArtistLetters});
                    break;
                case UwpViewTypes.GenreLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (TracksPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.GenreLetters});
                    break;
                case UwpViewTypes.LabelLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (LettersPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.LabelsByLetter});
                    break;
                case UwpViewTypes.LabelsByLetter:
                    _sharedApp.ContentFrame.Navigate(typeof (AlbumListPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.LabelsByLetter});
                    break;
                case UwpViewTypes.RatingLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (TracksPage),
                        new ViewParameters {Value = item.Value, ViewType = UwpViewTypes.RatingLetters});
                    break;
                case UwpViewTypes.YearLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (AlbumListPage),
                        new ViewParameters
                        {
                            Letter = item.Letter,
                            Value = item.Value,
                            ViewType = UwpViewTypes.YearLetters
                        });
                    break;
                case UwpViewTypes.AddedDateYearLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (LettersPage),
                        new ViewParameters
                        {
                            Letter = item.Letter,
                            Value = item.Value,
                            ViewType = UwpViewTypes.AddedDateMonthLetters
                        });
                    break;
                case UwpViewTypes.AddedDateMonthLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (LettersPage),
                        new ViewParameters
                        {
                            Letter = item.Letter,
                            Value = item.Value,
                            ParentValue = _parameters.Value,
                            ViewType = UwpViewTypes.AddedDateDayLetters
                        });
                    break;
                case UwpViewTypes.AddedDateDayLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (AlbumListPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.AddedDateDayLetters});
                    break;
                case UwpViewTypes.PlayedDateYearLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (LettersPage),
                        new ViewParameters
                        {
                            Letter = item.Letter,
                            Value = item.Value,
                            ViewType = UwpViewTypes.PlayedDateMonthLetters
                        });
                    break;
                case UwpViewTypes.PlayedDateMonthLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (LettersPage),
                        new ViewParameters
                        {
                            Letter = item.Letter,
                            Value = item.Value,
                            ParentValue = _parameters.Value,
                            ViewType = UwpViewTypes.PlayedDateDayLetters
                        });
                    break;
                case UwpViewTypes.PlayedDateDayLetters:
                    _sharedApp.ContentFrame.Navigate(typeof (TracksPage),
                        new ViewParameters {Letter = item.Letter, ViewType = UwpViewTypes.PlayedDateDayLetters});
                    break;
            }
        }

        public UwpViewTypes ViewType { get; set; }
    }
}