using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using NeonShared.Types;

namespace UwpSharedViews.Interfaces
{
    public interface IPlaylistsFacadeVm
    {
        Task Populate(ViewParameters param);
        ObservableCollection<PlaylistContainerItem> Playlists { get; }
        void OnSelectionChanged(object sender, SelectionChangedEventArgs e);
    }
}