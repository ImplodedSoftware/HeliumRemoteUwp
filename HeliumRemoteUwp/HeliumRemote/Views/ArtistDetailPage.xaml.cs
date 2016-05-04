using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using HeliumRemote.Bootstraper;
using HeliumRemote.Helpers;
using HeliumRemote.Interfaces;
using UwpSharedViews.Interfaces;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArtistDetailPage : Page
    {
        private int _artistId;
        private readonly IArtistDetailsFacadeVm _vm;

        public ArtistDetailPage()
        {
            InitializeComponent();
            _vm = CompositionRoot.ArtistDetailsFacadeVm;
            _vm.Cvs = GroupedReleasesSource;
            DataContext = _vm;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _artistId = (int) e.Parameter;
        }


        private async void ArtistDetailPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await _vm.AdjustUiParts(_artistId);
        }

        private void Image_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var img = (Image) sender;
            img.Source = Application.Current.Resources["DefaultArtistImageLarge"] as BitmapImage;
        }

        private void Image_OnImageOpened(object sender, RoutedEventArgs e)
        {
            var img = (Image) sender;
            img.MaxWidth = AppHelpers.LargeImageSize;
        }
    }
}