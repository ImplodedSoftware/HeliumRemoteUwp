using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HeliumRemote.Views
{
    public sealed partial class ActionDialogAlbum : ContentDialog
    {
        public ActionDialogAlbum()
        {
            this.InitializeComponent();
        }

        public int ResultCode { get; set; }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var tag = (string)btn.Tag;
            ResultCode = int.Parse(tag);
            Hide();
        }
    }
}
