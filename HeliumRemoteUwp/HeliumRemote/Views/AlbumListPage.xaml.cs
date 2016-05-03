using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HeliumRemote.Bootstraper;
using HeliumRemote.Helpers;
using HeliumRemote.Interfaces;
using NeonShared.Types;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumListPage : Page
    {
        private ViewParameters _parameters;
        private readonly IAlbumListFacadeVm _vm;

        public AlbumListPage()
        {
            InitializeComponent();
            _vm = CompositionRoot.AlbumListFacadeVm;
            DataContext = _vm;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _parameters = (ViewParameters) e.Parameter;
            var verb = string.Empty;
            if (_parameters.ViewType == UwpViewTypes.AlbumLetters)
                verb = TranslationHelper.GetString("AlbumsTitle");
            else if (_parameters.ViewType == UwpViewTypes.AlbumArtistLetters)
                verb = TranslationHelper.GetString("AlbumArtistsTitle");
            else if (_parameters.ViewType == UwpViewTypes.FavouriteAlbums)
                verb = TranslationHelper.GetString("FavouriteAlbumsTitle");
            else if (_parameters.ViewType == UwpViewTypes.LabelsByLetter)
                verb = TranslationHelper.GetString("Label");
            else if (_parameters.ViewType == UwpViewTypes.AddedDateDayLetters)
                verb = TranslationHelper.GetString("AddedDateTitle");
            else if (_parameters.ViewType == UwpViewTypes.YearLetters)
                verb = TranslationHelper.GetString("Year");
            var res = string.Format("{0}: {1}", verb, _parameters.Letter);
            if (_parameters.ViewType == UwpViewTypes.FavouriteAlbums)
                res = verb;
            AppHelpers.UpdatePageTitle(res);
        }

        private async void AlbumListPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await _vm.Refresh(_parameters);
        }
    }
}