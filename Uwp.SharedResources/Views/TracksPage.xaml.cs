using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using NeonShared.Pcl.Helpers;
using NeonShared.Pcl.Types;
using Uwp.SharedResources.Classes;
using Uwp.SharedResources.Helpers;
using Uwp.SharedResources.Interfaces;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Uwp.SharedResources.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TracksPage : Page
    {
        private readonly ITracksVmFacade _vm;
        private ViewParameters _params;

        public TracksPage()
        {
            InitializeComponent();
            _vm = SharedRepository.Instance.Repository.TracksVmFacade;
            DataContext = _vm;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _params = (ViewParameters) e.Parameter;
            var tit = string.Format("{0}: {1}", TranslationHelper.GetString(_params.ViewType.ToString()), _params.Letter);
            if (_params.ViewType == UwpViewTypes.RatingLetters)
            {
                var verb =
                    TranslationHelper.GetString(string.Format("Rating{0}", NeonHelpers.DownsizeRating(_params.Value)));
                tit = string.Format("{0}: {1}", TranslationHelper.GetString(_params.ViewType.ToString()), verb);
            }
            else if (_params.ViewType == UwpViewTypes.FavouriteTracks)
                tit = TranslationHelper.GetString("FavouriteTracksTitle");
            else if (_params.ViewType == UwpViewTypes.Playlists)
                tit = _params.Letter;
            else if (_params.ViewType == UwpViewTypes.SmartPlaylists)
                tit = _params.Letter;
            SharedRepository.Instance.Repository.SharedApp.UpdatePageTitle(tit);
        }

        private async void TracksPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await _vm.Refresh(_params);
            _vm.AdjustUiParts();
        }
    }
}