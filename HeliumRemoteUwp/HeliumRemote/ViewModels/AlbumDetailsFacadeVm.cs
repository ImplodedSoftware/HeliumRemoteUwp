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
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;
using HeliumRemote.Views;

namespace HeliumRemote.ViewModels
{
    public class AlbumDetailsFacadeVm : ViewModelBase, IAlbumDetailsFacadeVm
    {
        public CollectionViewSource Cvs { get; set; }
        private readonly IAlbumDetailsVm _albumDetailsVm;

        private Thickness _elementMargin;
        public Thickness ElementMargin
        {
            get { return _elementMargin;}
            set { _elementMargin = value; RaisePropertyChanged("ElementMargin"); }
        }

        private double _iconSize;

        public double IconSize
        {
            get { return _iconSize; }
            set { _iconSize = value; RaisePropertyChanged("IconSize"); }
        }
        private int _ratingWidth;
        public int RatingWidth
        {
            get { return _ratingWidth; }
            set { _ratingWidth = value; RaisePropertyChanged("RatingWidth"); }
        }
        private bool _isFavourite;
        public bool IsFavourite
        {
            get { return _isFavourite; }
            set { _isFavourite = value; RaisePropertyChanged("IsFavourite"); }
        }

        public RelayCommand<int> ShowTrackActionsCommand { get; }
        public RelayCommand ShowAlbumActionsCommand { get; }
        public RelayCommand ChangeFavouriteCommand { get; }

        public AlbumDetailsFacadeVm(IAlbumDetailsVm albumDetailsVm)
        {
            _albumDetailsVm = albumDetailsVm;
            ShowTrackActionsCommand = new RelayCommand<int>(showTrackActionsExecute, null);
            ShowAlbumActionsCommand = new RelayCommand(showAlbumActionsExecute, null);
            ChangeFavouriteCommand = new RelayCommand(changeFavouriteExecute, null);
            var scaleFac = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            ImageSize = (int)(AppConstants.MEDIUM_IMAGE_SIZE/scaleFac);
        }

        private async void changeFavouriteExecute()
        {
            IsFavourite = !IsFavourite;
            Album.IsFavourite = IsFavourite;
            RaisePropertyChanged("Album");
            if (IsFavourite)
                await CompositionRoot.WebService.SetAlbumAsFavourite(Album.Id);
            else
                await CompositionRoot.WebService.UnsetAlbumAsFavourite(Album.Id);
        }
        private async void showTrackActionsExecute(int id)
        {
            //var btn = sender as Button;
            var dlg = new ActionDialogTracks();
            var result = await dlg.ShowAsync();
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
            //btn.Content = "Result: " + result;
        }
        private async void showAlbumActionsExecute()
        {
            //var btn = sender as Button;
            var dlg = new ActionDialogAlbum();
            var result = await dlg.ShowAsync();
            switch (dlg.ResultCode)
            {
                case AppConstants.ALB_RES_CODE_PLAYNOW:
                    await CompositionRoot.WebService.PlayAlbum(album.Id);
                    break;
                case AppConstants.ALB_RES_CODE_ENQUEUENEXT:
                    await CompositionRoot.WebService.EnqueueAlbumNext(album.Id);
                    break;
                case AppConstants.ALB_RES_CODE_ENQUEUELAST:
                    await CompositionRoot.WebService.EnqueueAlbumLast(album.Id);
                    break;
            }
        }

        private Album album;
        public Album Album
        {
            get { return album; }
            set { album = value; RaisePropertyChanged("Album"); }
        }

        private ObservableCollection<IAlbumDetailItem> albumDetailCells;

        public ObservableCollection<IAlbumDetailItem> AlbumDetailCells
        {
            get { return albumDetailCells; }
            set
            {
                albumDetailCells = value;
                RaisePropertyChanged("AlbumDetailCells");
            }
        }

        private bool hasMultipleCds
        {
            get
            {
                var res = new List<int>();
                foreach(var trk in _albumDetailsVm.Tracks)
                {
                    if (res.IndexOf(trk.CurrentCd) == -1)
                        res.Add(trk.CurrentCd);
                }
                if (res.Count > 1)
                    return true;
                return false;
            }
        }

        private bool isEmptyCdAlbum
        {
            get
            {
                var res = new List<int>();
                foreach (var trk in _albumDetailsVm.Tracks)
                {
                    if (res.IndexOf(trk.CurrentCd) == -1)
                        res.Add(trk.CurrentCd);
                }
                if (res.Count == 0)
                    return true;
                if (res[0] <= 0)
                    return true;
                return false;
            }
        }
        public async Task Refresh(int id)
        {
            await _albumDetailsVm.Refresh(id);
            Album = _albumDetailsVm.Album;
            var tracks = _albumDetailsVm.Tracks;
            var res = new ObservableCollection<IAlbumDetailItem>();
            res.Add(new AlbumDetailTopCell {Album = Album});
            IsFavourite = Album.IsFavourite;

            //var groupedRaw = new ObservableCollection<TrackContainer>();
            //res.Add(new AlbumDetailHeaderCell {Header = "Tracks"});
            var idx = 0;
            if (hasMultipleCds)
            {
                var currCd = -2;
                var lst = _albumDetailsVm.Tracks.OrderBy(x => x.CurrentCd).ThenBy(x => x.CurrentTrack).ToList();
                foreach (var track in tracks)
                {
                    if (currCd != track.CurrentCd)
                    {
                        currCd = track.CurrentCd;
                        res.Add(new AlbumDetailHeaderCell { Header = "CD " + track.CurrentCd });
                    }
                    track.Title = string.Format("{0}.{1}", track.CurrentTrack, track.Title);
                    res.Add(new AlbumDetailTrackCell { Track = track, Index = idx++});
                    //res.Add(new AlbumDetailTrackCell { Track =  track });
                    //groupedRaw.Add(new TrackContainer {CurrentCd = track.CurrentCd, Track = track});
                }
            }
            else
            {
                foreach (var track in tracks)
                {
                    track.Title = string.Format("{0}.{1}", track.CurrentTrack, track.Title);
                    res.Add(new AlbumDetailTrackCell { Track = track, Index = idx++ });
                    //res.Add(new AlbumDetailTrackCell {Track = track});
                    //groupedRaw.Add(new TrackContainer { CurrentCd = track.CurrentCd, Track = track, Index = idx++ });
                }
            }
            AlbumDetailCells = res;
            ((App)Application.Current).ActiveViewType = UwpViewTypes.AlbumDetail;            

            //var tracksGrouped = new List<GroupInfoList<object>>();
            //var query = from release in groupedRaw
            //            orderby ((TrackContainer)release).Track.CurrentTrack
            //            group release by ((TrackContainer)release).CurrentCd into g
            //            select new { GroupName = g.Key, Items = g };
            //foreach (var g in query)
            //{
            //    var info = new GroupInfoList<object>();
            //    info.Key = g.GroupName;
            //    foreach (var friend in g.Items)
            //    {
            //        info.Add(friend);
            //    }
            //    tracksGrouped.Add(info);
            //}
            //if (tracksGrouped != null)
            //{
            //    Cvs.Source = tracksGrouped;
            //}
        }

        private int _imageSize;

        public int ImageSize
        {
            get { return _imageSize;}
            set { _imageSize = value; RaisePropertyChanged("ImageSize");}
        }
    }
}
