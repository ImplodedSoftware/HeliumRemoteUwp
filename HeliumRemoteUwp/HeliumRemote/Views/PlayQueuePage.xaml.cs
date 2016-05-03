using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HeliumRemote.Helpers;
using HeliumRemote.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayQueuePage : Page
    {
        private readonly PlayQueueVm _vm;

        public PlayQueuePage()
        {
            InitializeComponent();
            _vm = new PlayQueueVm();
            DataContext = _vm;
        }

        public void UpdateUi()
        {
            _vm.PropagateUpdate();
        }

        private async void PlayQueuePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            AppHelpers.UpdatePageTitle(TranslationHelper.GetString("PlayQueueTitle"));
            await _vm.Refresh();
        }
    }
}