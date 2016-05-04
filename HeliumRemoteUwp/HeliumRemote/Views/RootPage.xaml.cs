using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using HeliumRemote.Classes;
using HeliumRemote.ViewModels;
using Uwp.SharedResources.Helpers;
using Windows.UI.Xaml.Media.Imaging;
using System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootPage : Page
    {
        private readonly RootViewModel _vm;

        public RootPage()
        {
            InitializeComponent();
            _vm = new RootViewModel(MyFrame, UpdateAction);
            _vm.CloseSlider += () =>
            {
                _vm.FilterVisible = false;
                FilterTextBox.Text = string.Empty;
                SplitView.IsPaneOpen = false;
            };
            _vm.SplitView = SplitView;
            _vm.FilterTextBox = FilterTextBox;
            DataContext = _vm;
            ((App) Application.Current).ContentFrame = MyFrame;
            ((App) Application.Current).RootViewModel = _vm;
            ((App) Application.Current).ActiveId = -1;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Color.FromArgb(255, 0x55, 0x55, 0x56);
            titleBar.ForegroundColor = Color.FromArgb(255, 0xf1, 0xf1, 0xf1);
            titleBar.ButtonBackgroundColor = Color.FromArgb(255, 0x55, 0x55, 0x56);
            if (DeviceTypeHelper.GetDeviceFormFactorType() == DeviceFormFactorType.Phone)
            {
                SplitView.DisplayMode = SplitViewDisplayMode.Overlay;
            }
            else
            {
                SplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
            }
        }

        private void UpdateAction()
        {
            if (MyFrame.Content is PlayQueuePage)
            {
                var pq = (PlayQueuePage) MyFrame.Content;
                pq.UpdateUi();
            }
        }

        private void HamburgerRadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
            AdjustUi();
        }

        private void AdjustUi()
        {
            if (SplitView.IsPaneOpen)
            {
                SearchButton.Visibility = Visibility.Collapsed;
                SearchContainer.Visibility = Visibility.Visible;
            }
            else
            {
                SearchButton.Visibility = Visibility.Visible;
                SearchContainer.Visibility = Visibility.Collapsed;
            }
        }

        private void RootPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var frame = Window.Current.Content as Frame;
            frame.BackStack.Clear();
            ((App)Application.Current).UpdateBackNavigationButton();
            if (!NeonSession.Instance.Instansiated)
            {
                NeonSession.Instance.Instansiated = true;
            }
            _vm.PlayQueueCommand.Execute(null);
            AdjustUi();
        }

        private void MiniPlayer_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof (NowPlayingPage));
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = true;
            AdjustUi();
        }

        private void SplitView_PaneClosed(SplitView sender, object args)
        {
            AdjustUi();
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var img = (Image)sender;
            img.Source = new BitmapImage(new Uri("ms-appx:///Images/s_no_album.png"));
        }
    }
}