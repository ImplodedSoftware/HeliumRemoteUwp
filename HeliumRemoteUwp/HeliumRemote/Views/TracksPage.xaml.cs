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
    public sealed partial class TracksPage : Page
    {
        private ViewParameters _params;
        private readonly ITracksVmFacade _vm;
        public TracksPage()
        {
            this.InitializeComponent();
            _vm = CompositionRoot.TracksVmFacade;
            DataContext = _vm;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _params = (ViewParameters) e.Parameter;
            var tit = string.Format("{0}: {1}", TranslationHelper.GetString(_params.ViewType.ToString()),  _params.Letter);
            if (_params.ViewType == UwpViewTypes.RatingLetters)
            {
                var verb = TranslationHelper.GetString(string.Format("Rating{0}", NeonShared.Helpers.NeonHelpers.DownsizeRating(_params.Value)));
                tit = string.Format("{0}: {1}", TranslationHelper.GetString(_params.ViewType.ToString()), verb);
            }
            else if (_params.ViewType == UwpViewTypes.FavouriteTracks)
                tit = TranslationHelper.GetString("FavouriteTracksTitle");
            else if (_params.ViewType == UwpViewTypes.Playlists)
                tit = _params.Letter;
            else if (_params.ViewType == UwpViewTypes.SmartPlaylists)
                tit = _params.Letter;
            AppHelpers.UpdatePageTitle(tit);
        }

        private async void TracksPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await _vm.Refresh(_params);
        }

    }
}
