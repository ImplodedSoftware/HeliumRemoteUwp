﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Pcl.Interfaces;
using NeonShared.Pcl.Types;
using Uwp.SharedResources.Classes;
using Uwp.SharedResources.Interfaces;
using Uwp.SharedResources.Types;
using Uwp.SharedResources.Views;

namespace Uwp.SharedResources.ViewModels
{
    public enum SearchViews
    {
        None,
        Artists,
        Albums,
        Tracks
    }

    public class SearchFacadeVm : ViewModelBase, ISearchFacadeVm
    {
        private readonly IPlayerProvider _playerProvider;
        private readonly ISharedApp _sharedApp;
        private readonly ISearchVm _vm;

        private SearchViews _activeView;
        private ObservableCollection<AlbumContainer> _albums;

        private ObservableCollection<Artist> _artists;

        private bool _hasAlbums;

        private bool _hasArtists;

        private bool _hasTracks;

        private ObservableCollection<TrackContainer> _tracks;

        public SearchFacadeVm(ISearchVm vm, ISharedApp sharedApp, IPlayerProvider playerProvider)
        {
            _vm = vm;
            _sharedApp = sharedApp;
            _playerProvider = playerProvider;
            ShowArtistsCommand = new RelayCommand(ShowArtistsExecute, null);
            ShowAlbumsCommand = new RelayCommand(ShowAlbumsExecute, null);
            ShowTracksCommand = new RelayCommand(ShowTracksExecute, null);
            ShowTrackActionsCommand = new RelayCommand<int>(ShowTrackActionsExecute, null);
            ShowAlbumActionsCommand = new RelayCommand<int>(ShowAlbumActionsExecute, null);
        }

        public RelayCommand<int> ShowTrackActionsCommand { get; }
        public RelayCommand<int> ShowAlbumActionsCommand { get; }

        public Action UpdateUi { get; set; }


        public RelayCommand ShowArtistsCommand { get; }
        public RelayCommand ShowAlbumsCommand { get; }
        public RelayCommand ShowTracksCommand { get; }

        public SearchViews ActiveView
        {
            get { return _activeView; }
            set
            {
                _activeView = value;
                RaisePropertyChanged();
                UpdateUi();
            }
        }

        public ObservableCollection<Artist> Artists
        {
            get { return _artists; }
            set
            {
                _artists = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<AlbumContainer> Albums
        {
            get { return _albums; }
            set
            {
                _albums = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<TrackContainer> Tracks
        {
            get { return _tracks; }
            set
            {
                _tracks = value;
                RaisePropertyChanged();
            }
        }

        public async Task Refresh(ViewParameters parameters)
        {
            await _vm.Populate(parameters);
            Artists = new ObservableCollection<Artist>(_vm.Artists);
            var res = new ObservableCollection<AlbumContainer>();
            foreach (var item in _vm.Albums)
                res.Add(new AlbumContainer {Album = item});
            Albums = res;
            var idx = 0;
            var trkres = new ObservableCollection<TrackContainer>();
            foreach (var trk in _vm.Tracks)
            {
                trkres.Add(new TrackContainer(_sharedApp) {Index = idx++, Track = trk});
            }
            Tracks = trkres;
            HasArtists = Artists.Count > 0;
            HasAlbums = Albums.Count > 0;
            HasTracks = Tracks.Count > 0;
            if (HasArtists)
                ActiveView = SearchViews.Artists;
            else if (HasAlbums)
                ActiveView = SearchViews.Albums;
            else if (HasTracks)
                ActiveView = SearchViews.Tracks;
            _sharedApp.ActiveViewType = parameters.ViewType;
        }

        public bool HasArtists
        {
            get { return _hasArtists; }
            set
            {
                _hasArtists = value;
                RaisePropertyChanged();
            }
        }

        public bool HasAlbums
        {
            get { return _hasAlbums; }
            set
            {
                _hasAlbums = value;
                RaisePropertyChanged();
            }
        }

        public bool HasTracks
        {
            get { return _hasTracks; }
            set
            {
                _hasTracks = value;
                RaisePropertyChanged();
            }
        }

        public void GridView_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (ActiveView == SearchViews.Artists)
            {
                Artist item = null;
                if (sender is GridView)
                {
                    var grd = (GridView) sender;
                    item = (Artist) grd.SelectedItem;
                }
                else if (sender is ListBox)
                {
                    var lb = (ListBox) sender;
                    item = (Artist) lb.SelectedItem;
                }
                if (item != null)
                    _sharedApp.ContentFrame.Navigate(typeof (ArtistDetailPage), item.Id);
            }
            else if (ActiveView == SearchViews.Albums)
            {
                AlbumContainer item = null;
                if (sender is GridView)
                {
                    var grd = (GridView) sender;
                    item = (AlbumContainer) grd.SelectedItem;
                }
                else if (sender is ListBox)
                {
                    var lb = (ListBox) sender;
                    item = (AlbumContainer) lb.SelectedItem;
                }
                if (item != null)
                    _sharedApp.ContentFrame.Navigate(typeof (AlbumDetailsPage), item);
            }
        }

        private void ShowArtistsExecute()
        {
            ActiveView = SearchViews.Artists;
        }

        private void ShowAlbumsExecute()
        {
            ActiveView = SearchViews.Albums;
        }

        private void ShowTracksExecute()
        {
            ActiveView = SearchViews.Tracks;
        }

        private async void ShowTrackActionsExecute(int id)
        {
            var dlg = new ActionDialogTracks();
            await dlg.ShowAsync();
            switch (dlg.ResultCode)
            {
                case AppConstants.TRK_RES_CODE_PLAYNOW:
                    await _playerProvider.PlayTrack(id);
                    break;
                case AppConstants.TRK_RES_CODE_ENQUEUENEXT:
                    await _playerProvider.EnqueueNext(id);
                    break;
                case AppConstants.TRK_RES_CODE_ENQUEUELAST:
                    await _playerProvider.EnqueueLast(id);
                    break;
            }
        }

        private async void ShowAlbumActionsExecute(int id)
        {
            var dlg = new ActionDialogAlbum();
            await dlg.ShowAsync();
            switch (dlg.ResultCode)
            {
                case AppConstants.ALB_RES_CODE_PLAYNOW:
                    await _playerProvider.PlayAlbum(id);
                    break;
                case AppConstants.ALB_RES_CODE_ENQUEUENEXT:
                    await _playerProvider.EnqueueAlbumNext(id);
                    break;
                case AppConstants.ALB_RES_CODE_ENQUEUELAST:
                    await _playerProvider.EnqueueAlbumLast(id);
                    break;
            }
        }
    }
}