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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArtistListPage : Page
    {
        private readonly IArtistListFacadeVm _vm;
        private ViewParameters _parameters;
        public ArtistListPage()
        {
            this.InitializeComponent();
            _vm = CompositionRoot.ArtistListFacadeVm;
            this.DataContext = _vm;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _parameters = (ViewParameters)e.Parameter;
            var tit = _parameters.ViewType.ToString();
            if (_parameters.ViewType == UwpViewTypes.ArtistLetters)
                tit = string.Format("{0}: {1}", TranslationHelper.GetString("ArtistsTitle"), _parameters.Letter);
            else if (_parameters.ViewType == UwpViewTypes.FavouriteArtists)
                tit = TranslationHelper.GetString("FavouriteArtistsTitle");
            else if (_parameters.ViewType == UwpViewTypes.AlbumArtistLetters)
                tit = string.Format("{0}: {1}", TranslationHelper.GetString("AlbumArtistLetters"), _parameters.Letter);
            //if (_parameters.ViewType == )
            //AppHelpers.UpdatePageTitle(string.Format("Artists: {0}", _parameters.Letter));
            AppHelpers.UpdatePageTitle(tit);
        }

        private async void ArtistListPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            _vm.IsBusy = true;
            await _vm.Refresh(_parameters);
            _vm.IsBusy = false;
        }
    }

}
