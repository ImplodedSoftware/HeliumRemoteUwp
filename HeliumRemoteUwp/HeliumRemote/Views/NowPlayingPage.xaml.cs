using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Imaging;
using HeliumRemote.Bootstraper;
using HeliumRemote.Interfaces;
using NeonShared.Pcl.Helpers;
using NeonShared.Pcl.Interfaces;
using Uwp.SharedResources.UserControls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NowPlayingPage : Page
    {
        private readonly IWebService _webService;
        private readonly INowPlayingVm _vm;
        private bool _isPressed;
        private bool _isValueChanged;
        private double _newValue;

        public NowPlayingPage()
        {
            InitializeComponent();
            _vm = CompositionRoot.NowPlayingVm;
            _webService = CompositionRoot.WebService;
            ImRating.PropagateValueChanged += async d =>
            {
                var usr = d*2.0;
                var rr = NeonHelpers.UpsizeRating((int) usr);
                if (rr != _vm.NowPlayingInfo.Rating)
                {
                    ((App) Application.Current).BlockUpdates = true;
                    _vm.NowPlayingInfo.Rating = rr;
                    await _webService.RateTrack(_vm.NowPlayingInfo.DetailId, rr);
                    ((App) Application.Current).BlockUpdates = false;
                }
            };
            DataContext = _vm;
            if (_vm.UpdatePosition == null)
            {
                _vm.UpdatePosition += async () =>
                {
                    if (!_isPressed)
                    {
                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                            () => { PosSlider.Value = _vm.TrackPosition; });
                    }
                };
            }

            Window.Current.CoreWindow.PointerPressed += (e, a) => { _isPressed = true; };

            Window.Current.CoreWindow.PointerReleased += async (e, a) =>
            {
                if (_isValueChanged)
                {
                    await _webService.SetPosition((int) _newValue);
                    _vm.TrackPosition = (int) _newValue;
                }
                _isPressed = false;
            };
        }

        private void NowPlayingPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            _vm.MainFrame = Frame;
        }

        private void RangeBase_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (!_isPressed)
            {
                _newValue = e.NewValue;
            }
            else
            {
                _isValueChanged = true;
                _newValue = e.NewValue;
            }
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var img = (Image) sender;
            img.Source = new BitmapImage(new Uri("ms-appx:///Images/no_album.png"));
        }
    }
}