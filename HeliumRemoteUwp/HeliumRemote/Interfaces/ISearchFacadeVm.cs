using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using GalaSoft.MvvmLight.Command;
using HeliumRemote.Types;
using HeliumRemote.ViewModels;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Types;

namespace HeliumRemote.Interfaces
{
    public interface ISearchFacadeVm
    {
        ObservableCollection<Artist> Artists { get; }
        ObservableCollection<AlbumContainer> Albums { get; }
        ObservableCollection<TrackContainer> Tracks { get; }
        Task Refresh(ViewParameters parameters);
        bool HasArtists { get; }
        bool HasAlbums { get; }
        bool HasTracks { get; }
        void GridView_OnTapped(object sender, TappedRoutedEventArgs e);
        RelayCommand ShowArtistsCommand { get; }
        RelayCommand ShowAlbumsCommand { get; }
        RelayCommand ShowTracksCommand { get; }
        Action UpdateUi { get; set; }
        SearchViews ActiveView { get;  }

    }
}
