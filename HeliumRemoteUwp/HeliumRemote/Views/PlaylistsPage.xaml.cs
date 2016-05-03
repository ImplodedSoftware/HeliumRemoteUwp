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
    public sealed partial class PlaylistsPage : Page
    {
        private ViewParameters _parameters;
        private readonly IPlaylistsFacadeVm _vm;

        public PlaylistsPage()
        {
            InitializeComponent();
            _vm = CompositionRoot.PlaylistsFacadeVm;
            DataContext = _vm;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _parameters = (ViewParameters) e.Parameter;
            if (_parameters.ViewType == UwpViewTypes.Playlists)
            {
                AppHelpers.UpdatePageTitle(TranslationHelper.GetString("PlaylistsTitle"));
                ItemsLb.ItemTemplate = Application.Current.Resources["PlaylistItem"] as DataTemplate;
            }
            else if (_parameters.ViewType == UwpViewTypes.SmartPlaylists)
            {
                AppHelpers.UpdatePageTitle(TranslationHelper.GetString("SmartPlaylistsTitle"));
                ItemsLb.ItemTemplate = Application.Current.Resources["SmartPlaylistItem"] as DataTemplate;
            }
        }

        private async void PlaylistsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await _vm.Populate(_parameters);
        }
    }
}