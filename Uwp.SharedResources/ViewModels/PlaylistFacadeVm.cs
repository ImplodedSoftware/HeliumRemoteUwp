using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using NeonShared.Pcl.Interfaces;
using NeonShared.Pcl.Types;
using Uwp.SharedResources.Interfaces;
using Uwp.SharedResources.Types;
using Uwp.SharedResources.Views;

namespace Uwp.SharedResources.ViewModels
{
    public class PlaylistFacadeVm : ViewModelBase, IPlaylistsFacadeVm
    {
        private readonly IPlaylistsVm _playlistsVm;
        private readonly ISharedApp _sharedApp;
        private ViewParameters _params;

        private ObservableCollection<PlaylistContainerItem> _playlists;

        public PlaylistFacadeVm(IPlaylistsVm playlistsVm, ISharedApp sharedApp)
        {
            _playlistsVm = playlistsVm;
            _sharedApp = sharedApp;
        }

        public async Task Populate(ViewParameters param)
        {
            _params = param;
            await _playlistsVm.Populate(param.ViewType, param);
            var res = new ObservableCollection<PlaylistContainerItem>();
            foreach (var item in _playlistsVm.Playlists)
                res.Add(item);
            Playlists = res;
            _sharedApp.ActiveViewType = param.ViewType;
        }

        public ObservableCollection<PlaylistContainerItem> Playlists
        {
            get { return _playlists; }
            set
            {
                _playlists = value;
                RaisePropertyChanged();
            }
        }

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlaylistContainerItem item = null;
            if (sender is ListBox)
            {
                var lst = (ListBox) sender;
                item = (PlaylistContainerItem) lst.SelectedItem;
            }
            if (item != null)
                _sharedApp.ContentFrame.Navigate(typeof (TracksPage),
                    new ViewParameters
                    {
                        Value = item.Playlist.Id,
                        ViewType = _params.ViewType,
                        Letter = item.Playlist.Name
                    });
        }
    }
}