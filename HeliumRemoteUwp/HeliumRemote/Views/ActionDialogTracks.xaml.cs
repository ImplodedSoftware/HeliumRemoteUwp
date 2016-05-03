using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActionDialogTracks : ContentDialog
    {
        public ActionDialogTracks()
        {
            this.InitializeComponent();
        }

        public int ResultCode { get; set; }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button) sender;
            var tag = (string) btn.Tag;
            ResultCode = int.Parse(tag);
            Hide();
        }
    }
}
