using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Neon.Api.Pcl.Models.Entities;
using Uwp.SharedResources.Classes;
using Uwp.SharedResources.Interfaces;
using Uwp.SharedResources.Types;
using UwpSharedViews.Interfaces;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Uwp.SharedResources.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumDetailsPage : Page
    {
        private readonly IAlbumDetailsFacadeVm _vm;
        private Album _album;

        public AlbumDetailsPage()
        {
            InitializeComponent();
            _vm = SharedRepository.Instance.Repository.AlbumDetailsFacadeVm;
            _vm.Cvs = GroupedReleasesSource;
            DataContext = _vm;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
                return;
            if (e.Parameter is AlbumContainer)
            {
                var alb = (AlbumContainer) e.Parameter;
                _album = alb.Album;
            }
            else if (e.Parameter is Album)
            {
                _album = (Album) e.Parameter;
            }
            SharedRepository.Instance.Repository.SharedApp.UpdatePageTitle(_album.Name);
        }

        private async void AlbumDetailsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_album == null)
                return;
            _vm.ImageSize = SharedRepository.Instance.Repository.SharedApp.LargeImageSize;
            await _vm.Refresh(_album.Id);
            _vm.AdjustUiParts();
        }

        private void Image_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var img = (Image) sender;
            img.Source = Application.Current.Resources["DefaultAlbumImageLarge"] as BitmapImage;
        }

        private void Image_OnImageOpened(object sender, RoutedEventArgs e)
        {
            var img = (Image) sender;
            img.MaxWidth = SharedRepository.Instance.Repository.SharedApp.LargeImageSize;
        }
    }
}