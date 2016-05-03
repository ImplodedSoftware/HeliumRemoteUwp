using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using HeliumRemote.Helpers;
using HeliumRemote.Interfaces;
using NeonShared.Interfaces;
using NeonShared.Types;
using HeliumRemote.Views;

namespace HeliumRemote.ViewModels
{
    public class PlaylistFacadeVm : ViewModelBase, IPlaylistsFacadeVm
    {
        private readonly IPlaylistsVm _playlistsVm;
        private ViewParameters _params;

        public PlaylistFacadeVm(IPlaylistsVm playlistsVm)
        {
            _playlistsVm = playlistsVm;
        }

        public async Task Populate(ViewParameters param)
        {
            _params = param;
            await _playlistsVm.Populate(param.ViewType, param);
            var res = new ObservableCollection<PlaylistContainerItem>();
            foreach (var item in _playlistsVm.Playlists)
                res.Add(item);
            Playlists = res;
            ((App)Application.Current).ActiveViewType = param.ViewType;
        }

        private ObservableCollection<PlaylistContainerItem> playlists;

        public ObservableCollection<PlaylistContainerItem> Playlists
        {
            get { return playlists; }
            set { playlists = value; RaisePropertyChanged("Playlists"); }
        }

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlaylistContainerItem item = null;
            if (sender is ListBox)
            {
                var lst = (ListBox)sender;
                item = (PlaylistContainerItem)lst.SelectedItem;
            }
            if (item != null)
                AppHelpers.ContentFrame.Navigate(typeof(TracksPage), new ViewParameters { Value = item.Playlist.Id, ViewType = _params.ViewType, Letter = item.Playlist.Name});
        }
    }
}
