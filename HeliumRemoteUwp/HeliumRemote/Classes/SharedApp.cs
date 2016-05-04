using Windows.UI.Xaml;
using UwpSharedViews.Interfaces;

namespace HeliumRemote.Classes
{
    public class SharedApp : ISharedApp
    {
        public int ActiveId => ((App) Application.Current).ActiveId;
    }
}
