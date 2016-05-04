using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using HeliumRemote.Bootstraper;
using NeonShared.Pcl.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NowPlayingPage : Page
    {
        private bool _isPressed;
        private double _newValue;
        private bool _isValueChanged;


        public NowPlayingPage()
        {
            InitializeComponent();
            Uwp.SharedResources.UserControls.ImRating.PropagateValueChanged += async d =>
            {
                var usr = d*2.0;
                var rr = NeonHelpers.UpsizeRating((int) usr);
                if (rr != CompositionRoot.NowPlayingVm.NowPlayingInfo.Rating)
                    await CompositionRoot.WebService.RateTrack(CompositionRoot.NowPlayingVm.NowPlayingInfo.DetailId, rr);
            };
            DataContext = CompositionRoot.NowPlayingVm;
            CompositionRoot.NowPlayingVm.UpdatePosition += async () =>
            {
                if (!_isPressed)
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () => { PosSlider.Value = CompositionRoot.NowPlayingVm.TrackPosition; });
                }
            };

            Window.Current.CoreWindow.PointerPressed += (e, a) => { _isPressed = true; };

            Window.Current.CoreWindow.PointerReleased += async (e, a) =>
            {
                if (_isValueChanged)
                {
                    await CompositionRoot.WebService.SetPosition((int) _newValue);
                    CompositionRoot.NowPlayingVm.TrackPosition = (int) _newValue;
                }
                _isPressed = false;
            };
        }

        private void NowPlayingPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            CompositionRoot.NowPlayingVm.MainFrame = Frame;
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
    }
}