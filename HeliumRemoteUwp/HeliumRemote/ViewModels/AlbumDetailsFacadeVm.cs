using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeliumRemote.Bootstraper;
using HeliumRemote.Classes;
using HeliumRemote.Interfaces;
using HeliumRemote.Types;
using HeliumRemote.Views;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;

namespace HeliumRemote.ViewModels
{
    public class AlbumDetailsFacadeVm : ViewModelBase, IAlbumDetailsFacadeVm
    {
        private readonly IAlbumDetailsVm _albumDetailsVm;

        private Thickness _elementMargin;

        private double _iconSize;

        private int _imageSize;
        private bool _isFavourite;
        private int _ratingWidth;

        private Album _album;

        private ObservableCollection<IAlbumDetailItem> _albumDetailCells;

        public AlbumDetailsFacadeVm(IAlbumDetailsVm albumDetailsVm)
        {
            _albumDetailsVm = albumDetailsVm;
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
            ((App) Application.Current).ActiveViewType = UwpViewTypes.AlbumDetail;
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

        private async void ChangeFavouriteExecute()
        {
            IsFavourite = !IsFavourite;
            Album.IsFavourite = IsFavourite;
            RaisePropertyChanged("Album");
            if (IsFavourite)
                await CompositionRoot.WebService.SetAlbumAsFavourite(Album.Id);
            else
                await CompositionRoot.WebService.UnsetAlbumAsFavourite(Album.Id);
        }

        private async void ShowTrackActionsExecute(int id)
        {
            var dlg = new ActionDialogTracks();
            await dlg.ShowAsync();
            switch (dlg.ResultCode)
            {
                case AppConstants.TRK_RES_CODE_PLAYNOW:
                    await CompositionRoot.WebService.PlayTrack(id);
                    break;
                case AppConstants.TRK_RES_CODE_ENQUEUENEXT:
                    await CompositionRoot.WebService.EnqueueTrackNext(id);
                    break;
                case AppConstants.TRK_RES_CODE_ENQUEUELAST:
                    await CompositionRoot.WebService.EnqueueTrackLast(id);
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
                    await CompositionRoot.WebService.PlayAlbum(_album.Id);
                    break;
                case AppConstants.ALB_RES_CODE_ENQUEUENEXT:
                    await CompositionRoot.WebService.EnqueueAlbumNext(_album.Id);
                    break;
                case AppConstants.ALB_RES_CODE_ENQUEUELAST:
                    await CompositionRoot.WebService.EnqueueAlbumLast(_album.Id);
                    break;
            }
        }
    }
}