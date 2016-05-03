using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using HeliumRemote.Bootstraper;
using HeliumRemote.UserControls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NowPlayingPage : Page
    {
        private bool _isPressed;
        private bool isValueChanged;
        private double _newValue;


        public NowPlayingPage()
        {
            this.InitializeComponent();
            ImRating.PropagateValueChanged += async (d) =>
            {
                var usr = d*2.0;
                var rr = NeonShared.Helpers.NeonHelpers.UpsizeRating((int) usr);
                if (rr != CompositionRoot.NowPlayingVm.NowPlayingInfo.Rating)
                    await CompositionRoot.WebService.RateTrack(CompositionRoot.NowPlayingVm.NowPlayingInfo.DetailId, rr);
            };
            DataContext = CompositionRoot.NowPlayingVm;
            CompositionRoot.NowPlayingVm.UpdatePosition += () =>
            {
                if (!_isPressed)
                {
                    CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () =>
                        {
                            PosSlider.Value = CompositionRoot.NowPlayingVm.TrackPosition;
                        });
                }
            };

            Window.Current.CoreWindow.PointerPressed += (e, a) =>
            {
                _isPressed = true;
            };

            Window.Current.CoreWindow.PointerReleased += async (e, a) => 
            {
                if (isValueChanged)
                {
                    await CompositionRoot.WebService.SetPosition((int)_newValue);
                    CompositionRoot.NowPlayingVm.TrackPosition = (int)_newValue;
                    Debug.WriteLine("Set value: " + _newValue);
                }
                _isPressed = false;
            };
        }

        private void NowPlayingPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            CompositionRoot.NowPlayingVm.MainFrame = this.Frame;
        }

        private void RangeBase_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (!_isPressed)
            {
                _newValue = e.NewValue;
            }
            else {
                isValueChanged = true;
                _newValue = e.NewValue;
            }
        }
    }
}
