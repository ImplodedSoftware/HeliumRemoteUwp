﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
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
using UwpSharedViews.Interfaces;

namespace Uwp.SharedResources.ViewModels
{
    public class ArtistDetailsFacadeVm : ViewModelBase, IArtistDetailsFacadeVm
    {
        private readonly IArtistDetailsVm _artistDetailsVm;
        private readonly IPlayerProvider _playerProvider;
        private readonly ISharedApp _sharedApp;

        private Artist _artist;

        private Thickness _elementMargin;
        private bool _isFavourite;
        private int _ratingWidth;
        private ObservableCollection<AlbumContainer> _albumItems;
        private ObservableCollection<IArtistDetailItem> _artistDetailCells;

        public ArtistDetailsFacadeVm(IArtistDetailsVm artistDetailsVm, IPlayerProvider playerProvider, ISharedApp sharedApp)
        {
            _artistDetailsVm = artistDetailsVm;
            _playerProvider = playerProvider;
            _sharedApp = sharedApp;
            PlayNowCommand = new RelayCommand<int>(PlayNowExecute, null);
            EnqueueNextCommand = new RelayCommand<int>(EnqueueNextExecute, null);
            EnqueueLastCommand = new RelayCommand<int>(EnqueueLastExecute, null);
            AddToPlaylistCommand = new RelayCommand<int>(AddToPlaylistExecute, null);
            ChangeFavouriteCommand = new RelayCommand(ChangeFavouriteExecute, null);
            ShowAlbumActionsCommand = new RelayCommand<int>(ShowAlbumActionsExecute, null);
        }

        public RelayCommand<int> PlayNowCommand { get; }
        public RelayCommand<int> EnqueueNextCommand { get; }
        public RelayCommand<int> EnqueueLastCommand { get; }
        public RelayCommand<int> AddToPlaylistCommand { get; }
        public RelayCommand ChangeFavouriteCommand { get; }
        public RelayCommand<int> ShowAlbumActionsCommand { get; }

        public bool IsFavourite
        {
            get { return _isFavourite; }
            set
            {
                _isFavourite = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<AlbumContainer> AlbumItems
        {
            get { return _albumItems; }
            set
            {
                _albumItems = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<IArtistDetailItem> ArtistDetailCells
        {
            get { return _artistDetailCells; }
            set
            {
                _artistDetailCells = value;
                RaisePropertyChanged();
            }
        }

        public Thickness ElementMargin
        {
            get { return _elementMargin; }
            set
            {
                _elementMargin = value;
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

        public CollectionViewSource Cvs { get; set; }

        public Artist Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                RaisePropertyChanged();
            }
        }

        public async Task Refresh(int id)
        {
            await _artistDetailsVm.Refresh(id);
            Artist = _artistDetailsVm.Artist;
            IsFavourite = Artist.IsFavourite;
            var groupedRaw = new ObservableCollection<AlbumContainer>();
            var res = new ObservableCollection<IArtistDetailItem> {new ArtistDetailTopCell {Artist = _artist}};
            if (_artist.Discography.Any())
            {
                res.Add(new ArtistDetailHeaderCell {Header = TranslationHelper.GetString("Discography")});
                foreach (var item in _artist.Discography)
                {
                    res.Add(new ArtistDetailAlbumCell {Album = item});
                    groupedRaw.Add(new AlbumContainer {Album = item, IsDiscography = true});
                }
            }
            if (_artist.Appearences.Any())
            {
                res.Add(new ArtistDetailHeaderCell {Header = TranslationHelper.GetString("Appearences")});
                foreach (var item in _artist.Appearences)
                {
                    res.Add(new ArtistDetailAlbumCell {Album = item});
                    groupedRaw.Add(new AlbumContainer {Album = item, IsDiscography = false});
                }
            }
            ArtistDetailCells = res;

            var releasesGrouped = new List<GroupInfoList<object>>();
            var query = from release in groupedRaw
                orderby release.Album.Name
                group release by release.IsDiscography
                into g
                select new {GroupName = g.Key, Items = g};
            foreach (var g in query)
            {
                var info = new GroupInfoList<object> {Key = g.GroupName};
                foreach (var friend in g.Items)
                {
                    info.Add(friend);
                }
                releasesGrouped.Add(info);
            }
            if (releasesGrouped != null)
            {
                Cvs.Source = releasesGrouped;
            }
            _sharedApp.ActiveViewType = UwpViewTypes.ArtistDetail;
        }

        public void UIElement_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var senderElement = sender as FrameworkElement;
            var flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);
        }

        public void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Album album = null;
            if (sender is GridView)
            {
                var grd = (GridView) sender;
                album = ((AlbumContainer) grd.SelectedItem).Album;
            }
            else
            {
                var lb = sender as ListBox;
                if (lb?.SelectedItem is ArtistDetailAlbumCell)
                {
                    album = ((ArtistDetailAlbumCell) lb.SelectedItem).Album;
                }
            }
            if (album != null)
                _sharedApp.ContentFrame.Navigate(typeof (AlbumDetailsPage), album);
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

        private async void ChangeFavouriteExecute()
        {
            IsFavourite = !IsFavourite;
            Artist.IsFavourite = IsFavourite;
            RaisePropertyChanged("Artist");
            if (IsFavourite)
                await SharedRepository.Instance.Repository.WebService.SetArtistAsFavourite(Artist.Id);
            else
                await SharedRepository.Instance.Repository.WebService.UnsetArtistAsFavourite(Artist.Id);
        }

        private async void PlayNowExecute(int id)
        {
            await _playerProvider.PlayAlbum(id);
        }

        private async void EnqueueNextExecute(int id)
        {
            await _playerProvider.EnqueueAlbumNext(id);
        }

        private  async void EnqueueLastExecute(int id)
        {
            await _playerProvider.EnqueueAlbumLast(id);
        }

        private static void AddToPlaylistExecute(int id)
        {
        }

        public async Task AdjustUiParts(int artistId)
        {
            if (DeviceTypeHelper.GetDeviceFormFactorType() == DeviceFormFactorType.Phone)
            {
                ElementMargin = new Thickness(0, 8, 0, 0);
                RatingWidth = 80;
            }
            else
            {
                ElementMargin = new Thickness(0, 8, 24, 0);
                RatingWidth = 120;
            }
            await Refresh(artistId);
            _sharedApp.UpdatePageTitle(Artist.ArtistName);
        }
    }
}