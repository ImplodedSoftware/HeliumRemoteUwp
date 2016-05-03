﻿using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using HeliumRemote.Bootstraper;
using HeliumRemote.Helpers;
using HeliumRemote.Interfaces;
using HeliumRemote.Types;
using Neon.Api.Pcl.Models.Entities;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumDetailsPage : Page
    {
        private Album _album;
        private readonly IAlbumDetailsFacadeVm _vm;
        public AlbumDetailsPage()
        {
            this.InitializeComponent();
            _vm = CompositionRoot.AlbumDetailsFacadeVm;
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
            AppHelpers.UpdatePageTitle(_album.Name);
        }

        private async void AlbumDetailsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_album == null)
                return;
            _vm.ImageSize = AppHelpers.LargeImageSize;
            await _vm.Refresh(_album.Id);
            if (DeviceTypeHelper.GetDeviceFormFactorType() == DeviceFormFactorType.Phone)
            {
                _vm.ElementMargin = new Thickness(0, 8, 0, 0);
                _vm.IconSize = 18;
                _vm.RatingWidth = 80;
            }
            else
            {
                _vm.ElementMargin = new Thickness(0, 8, 24, 0);
                _vm.IconSize = 36;
                _vm.RatingWidth = 120;
            }
        }

        //ToDo: Figure out why x:Bind makes this to crash
        private void Image_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Debug.WriteLine("ImageFailed");
            var img = (Image)sender;
            img.Source = Application.Current.Resources["DefaultAlbumImageLarge"] as BitmapImage;
        }

        private void Image_OnImageOpened(object sender, RoutedEventArgs e)
        {
            var img = (Image)sender;
            img.MaxWidth = AppHelpers.LargeImageSize;
            //img.MaxHeight = AppHelpers.LargeImageSize;
        }
    }
}
