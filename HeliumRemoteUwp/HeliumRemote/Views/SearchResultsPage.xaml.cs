using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HeliumRemote.Bootstraper;
using HeliumRemote.Helpers;
using HeliumRemote.Interfaces;
using HeliumRemote.ViewModels;
using NeonShared.Types;
using UwpSharedViews.Interfaces;
using UwpSharedViews.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchResultsPage : Page
    {
        private readonly ISearchFacadeVm _vm;
        private ViewParameters _parameters;

        public SearchResultsPage()
        {
            InitializeComponent();
            _vm = CompositionRoot.SearchFacadeVm;
            DataContext = _vm;
            _vm.UpdateUi += () =>
            {
                tbArtists.IsSelected = false;
                tbAlbums.IsSelected = false;
                tbTracks.IsSelected = false;
                if (_vm.ActiveView == SearchViews.Artists)
                    tbArtists.IsSelected = true;
                else if (_vm.ActiveView == SearchViews.Albums)
                    tbAlbums.IsSelected = true;
                else if (_vm.ActiveView == SearchViews.Tracks)
                    tbTracks.IsSelected = true;
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _parameters = (ViewParameters) e.Parameter;
        }

        private async void SearchResultsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            AppHelpers.UpdatePageTitle(TranslationHelper.GetString("SearchResults"));
            await _vm.Refresh(_parameters);
            var bc = 0;
            if (_vm.HasArtists) bc++;
            if (_vm.HasAlbums) bc++;
            if (_vm.HasTracks) bc++;
            if (bc != 0)
            {
                var bw = (int) (TopGrid.ActualWidth/bc);

                tbArtists.Width = bw;
                tbAlbums.Width = bw;
                tbTracks.Width = bw;
                tbArtists.SetCustomWidth(bw);
                tbAlbums.SetCustomWidth(bw);
                tbTracks.SetCustomWidth(bw);
                tbArtists.ClickCommand = _vm.ShowArtistsCommand;
                tbAlbums.ClickCommand = _vm.ShowAlbumsCommand;
                tbTracks.ClickCommand = _vm.ShowTracksCommand;
            }
            else
            {
                TopGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}