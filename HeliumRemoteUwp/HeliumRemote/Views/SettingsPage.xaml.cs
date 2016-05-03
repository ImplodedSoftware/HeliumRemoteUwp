using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HeliumRemote.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private readonly SettingsVm _vm;

        public SettingsPage()
        {
            InitializeComponent();
            _vm = new SettingsVm();
            DataContext = _vm;
        }

        private void SettingsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            _vm.MainFrame = Frame;
        }
    }
}