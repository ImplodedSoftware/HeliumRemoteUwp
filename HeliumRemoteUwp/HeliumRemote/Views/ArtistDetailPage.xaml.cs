using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using HeliumRemote.Bootstraper;
using HeliumRemote.Helpers;
using HeliumRemote.Interfaces;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArtistDetailPage : Page
    {
        private int _artistId;
        private IArtistDetailsFacadeVm _vm;
        public ArtistDetailPage()
        {
            this.InitializeComponent();
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
            if (DeviceTypeHelper.GetDeviceFormFactorType() == DeviceFormFactorType.Phone)
            {
                _vm.ElementMargin = new Thickness(0, 8, 0, 0);
                _vm.RatingWidth = 80;
            }
            else
            {
                _vm.ElementMargin = new Thickness(0, 8, 24, 0);
                _vm.RatingWidth = 120;
            }
            await _vm.Refresh(_artistId);
            AppHelpers.UpdatePageTitle(_vm.Artist.ArtistName);
        }

        //ToDo: Figure out why x:Bind makes this to crash
        private void Image_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Debug.WriteLine("ImageFailed");
            var img = (Image) sender;
            img.Source = Application.Current.Resources["DefaultArtistImageLarge"] as BitmapImage;
        }

        private void Image_OnImageOpened(object sender, RoutedEventArgs e)
        {
            var img = (Image)sender;
            img.MaxWidth = AppHelpers.LargeImageSize;
            //img.MaxHeight = AppHelpers.LargeImageSize;
        }
    }
}
