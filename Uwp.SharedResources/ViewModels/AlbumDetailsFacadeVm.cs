﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Pcl.Interfaces;
using NeonShared.Pcl.Types;
using Uwp.SharedResources.Classes;
using Uwp.SharedResources.Helpers;
using Uwp.SharedResources.Interfaces;
using Uwp.SharedResources.Types;
using Uwp.SharedResources.Views;

namespace Uwp.SharedResources.ViewModels
{
    public class AlbumDetailsFacadeVm : ViewModelBase, IAlbumDetailsFacadeVm
    {
        private readonly IAlbumDetailsVm _albumDetailsVm;
        private readonly IPlayerProvider _playerProvider;
        private readonly ISharedApp _sharedApp;
        private readonly IWebService _webService;

        private Album _album;

        private ObservableCollection<IAlbumDetailItem> _albumDetailCells;

        private Thickness _elementMargin;

        private double _iconSize;

        private int _imageSize;
        private bool _isFavourite;
        private int _ratingWidth;

        public AlbumDetailsFacadeVm(IAlbumDetailsVm albumDetailsVm, IPlayerProvider playerProvider, ISharedApp sharedApp, IWebService webService)
        {
            _albumDetailsVm = albumDetailsVm;
            _playerProvider = playerProvider;
            _sharedApp = sharedApp;
            _webService = webService;
            ShowTrackActionsCommand = new RelayCommand<int>(ShowTrackActionsExecute, null);
            ShowAlbumActionsCommand = new RelayCommand(ShowAlbumActionsExecute, null);
            ChangeFavouriteCommand = new RelayCommand(ChangeFavouriteExecute, null);
            var scaleFac = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            ImageSize = (int) (AppConstants.MEDIUM_IMAGE_SIZE/scaleFac);
        }

        public bool IsFavourite
        {
            get { return _isFavourite; }
            set
            {
                _isFavourite = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<int> ShowTrackActionsCommand { get; }
        public RelayCommand ShowAlbumActionsCommand { get; }
        public RelayCommand ChangeFavouriteCommand { get; }

        public ObservableCollection<IAlbumDetailItem> AlbumDetailCells
        {
            get { return _albumDetailCells; }
            set
            {
                _albumDetailCells = value;
                RaisePropertyChanged();
            }
        }

        private bool HasMultipleCds
        {
            get
            {
                var res = new List<int>();
                foreach (var trk in _albumDetailsVm.Tracks)
                {
                    if (res.IndexOf(trk.CurrentCd) == -1)
                        res.Add(trk.CurrentCd);
                }
                if (res.Count > 1)
                    return true;
                return false;
            }
        }

        public CollectionViewSource Cvs { get; set; }

        public Thickness ElementMargin
        {
            get { return _elementMargin; }
            set
            {
                _elementMargin = value;
                RaisePropertyChanged();
            }
        }

        public double IconSize
        {
            get { return _iconSize; }
            set
            {
                _iconSize = value;
                RaisePropertyChanged();
            }
        }

        public int RatingWidth
        {
            get { return _ratingWidth; }
            set
            {
                _ratingWidth = value;
                RaisePropertyChanged();
            }
        }

        public Album Album
        {
            get { return _album; }
            set
            {
                _album = value;
                RaisePropertyChanged();
            }
        }

        public async Task Refresh(int id)
        {
            await _albumDetailsVm.Refresh(id);
            Album = _albumDetailsVm.Album;
            var tracks = _albumDetailsVm.Tracks;
            var res = new ObservableCollection<IAlbumDetailItem> {new AlbumDetailTopCell {Album = Album}};
            IsFavourite = Album.IsFavourite;

            var idx = 0;
            if (HasMultipleCds)
            {
                var currCd = -2;
                foreach (var track in tracks)
                {
                    if (currCd != track.CurrentCd)
                    {
                        currCd = track.CurrentCd;
                        res.Add(new AlbumDetailHeaderCell {Header = "CD " + track.CurrentCd});
                    }
                    track.Title = string.Format("{0}. {1}", track.CurrentTrack, track.Title);
                    res.Add(new AlbumDetailTrackCell {Track = track, Index = idx++});
                }
            }
            else
            {
                foreach (var track in tracks)
                {
                    track.Title = string.Format("{0}. {1}", track.CurrentTrack, track.Title);
                    res.Add(new AlbumDetailTrackCell {Track = track, Index = idx++});
                }
            }
            AlbumDetailCells = res;
            _sharedApp.ActiveViewType = UwpViewTypes.AlbumDetail;
        }

        public int ImageSize
        {
            get { return _imageSize; }
            set
            {
                _imageSize = value;
                RaisePropertyChanged();
            }
        }

        public void AdjustUiParts()
        {
            if (DeviceTypeHelper.GetDeviceFormFactorType() == DeviceFormFactorType.Phone)
            {
                ElementMargin = new Thickness(0, 8, 8, 0);
                IconSize = 18;
                RatingWidth = 80;
            }
            else
            {
                ElementMargin = new Thickness(0, 8, 24, 0);
                IconSize = 36;
                RatingWidth = 120;
            }
        }

        private async void ChangeFavouriteExecute()
        {
            IsFavourite = !IsFavourite;
            Album.IsFavourite = IsFavourite;
            RaisePropertyChanged("Album");
            if (IsFavourite)
                await _webService.SetAlbumAsFavourite(Album.Id);
            else
                await _webService.UnsetAlbumAsFavourite(Album.Id);
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

        private async void ShowAlbumActionsExecute()
        {
            var dlg = new ActionDialogAlbum();
            await dlg.ShowAsync();
            switch (dlg.ResultCode)
            {
                case AppConstants.ALB_RES_CODE_PLAYNOW:
                    await _playerProvider.PlayAlbum(_album.Id);
                    break;
                case AppConstants.ALB_RES_CODE_ENQUEUENEXT:
                    await _playerProvider.EnqueueAlbumNext(_album.Id);
                    break;
                case AppConstants.ALB_RES_CODE_ENQUEUELAST:
                    await _playerProvider.EnqueueAlbumLast(_album.Id);
                    break;
            }
        }
    }
}