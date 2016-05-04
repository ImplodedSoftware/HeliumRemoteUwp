﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeliumRemote;
using HeliumRemote.Helpers;
using HeliumRemote.Views;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;
using UwpSharedViews.Classes;
using UwpSharedViews.Interfaces;
using UwpSharedViews.Types;

namespace UwpSharedViews.ViewModels
{
    public class TracksVmFacade : ViewModelBase, ITracksVmFacade, IViewFilter
    {
        private readonly IPlayerProvider _playerProvider;
        private readonly ITracksVm _tracksVm;
        private readonly ISharedApp _sharedApp;

        private Thickness _elementMargin;

        private List<Track> _originalTracks;

        private ObservableCollection<TrackContainer> _tracks;

        public TracksVmFacade(ITracksVm tracksVm, IPlayerProvider playerProvider, ISharedApp sharedApp)
        {
            _tracksVm = tracksVm;
            _playerProvider = playerProvider;
            _sharedApp = sharedApp;
            ShowTrackActionsCommand = new RelayCommand<int>(ShowTrackActionsExecute, null);
        }

        public RelayCommand<int> ShowTrackActionsCommand { get; }

        public Thickness ElementMargin
        {
            get { return _elementMargin; }
            set
            {
                _elementMargin = value;
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

        public async Task Refresh(ViewParameters param)
        {
            var vt = ViewTypeTracks.None;
            if (param.ViewType == UwpViewTypes.GenreLetters)
                vt = ViewTypeTracks.Genre;
            else if (param.ViewType == UwpViewTypes.RatingLetters)
                vt = ViewTypeTracks.Rating;
            else if (param.ViewType == UwpViewTypes.PlayedDateDayLetters)
                vt = ViewTypeTracks.PlayedDate;
            else if (param.ViewType == UwpViewTypes.FavouriteTracks)
                vt = ViewTypeTracks.FavouriteTracks;
            else if (param.ViewType == UwpViewTypes.Playlists)
                vt = ViewTypeTracks.Playlists;
            else if (param.ViewType == UwpViewTypes.SmartPlaylists)
                vt = ViewTypeTracks.SmartPlaylists;
            await _tracksVm.Populate(vt, param);
            var res = new ObservableCollection<TrackContainer>();
            var idx = 0;
            foreach (var trk in _tracksVm.Tracks)
            {
                res.Add(new TrackContainer(_sharedApp) {Index = idx++, Track = trk});
            }
            _originalTracks = (List<Track>) _tracksVm.Tracks;
            Tracks = res;
            ((App) Application.Current).ViewFilter = this;
            ((App) Application.Current).ActiveViewType = param.ViewType;
        }

        public void AdjustUiParts()
        {
            if (DeviceTypeHelper.GetDeviceFormFactorType() == DeviceFormFactorType.Phone)
            {
                ElementMargin = new Thickness(0, 8, 8, 0);
            }
            else
            {
                ElementMargin = new Thickness(0, 8, 24, 0);
            }
        }

        public void FilterData(string expr)
        {
            _tracks.Clear();
            if (string.IsNullOrEmpty(expr))
            {
                ClearFilter();
                return;
            }

            var resd = _originalTracks.Where(
                x => x.Title.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0 ||
                     x.Artist.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0 ||
                     x.Album.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0
                );
            var idx = 0;
            foreach (var trk in resd)
            {
                Tracks.Add(new TrackContainer(_sharedApp) {Index = idx++, Track = trk});
            }
        }

        public void ClearFilter()
        {
            _tracks.Clear();
            var idx = 0;
            foreach (var trk in _tracksVm.Tracks)
            {
                Tracks.Add(new TrackContainer(_sharedApp) {Index = idx++, Track = trk});
            }
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
    }
}