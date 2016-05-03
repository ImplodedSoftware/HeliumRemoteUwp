using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeliumRemote.Bootstraper;
using HeliumRemote.Classes;
using HeliumRemote.Helpers;
using HeliumRemote.Interfaces;
using HeliumRemote.Types;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;
using HeliumRemote.Views;

namespace HeliumRemote.ViewModels
{
    public class ArtistDetailsFacadeVm : ViewModelBase, IArtistDetailsFacadeVm
    {
        public RelayCommand<int> PlayNowCommand { get; }
        public RelayCommand<int> EnqueueNextCommand { get; }
        public RelayCommand<int> EnqueueLastCommand { get; }
        public RelayCommand<int> AddToPlaylistCommand { get; }
        public RelayCommand ChangeFavouriteCommand { get; }
        public RelayCommand<int> ShowAlbumActionsCommand { get; }

        private Thickness _elementMargin;
        public Thickness ElementMargin
        {
            get { return _elementMargin; }
            set { _elementMargin = value; RaisePropertyChanged("ElementMargin"); }
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

        public CollectionViewSource Cvs { get; set; }
        private ObservableCollection<AlbumContainer> albumItems;

        public ObservableCollection<AlbumContainer> AlbumItems
        {
            get { return albumItems;}
            set { albumItems = value; RaisePropertyChanged("AlbumItems"); }
        }  
        private ObservableCollection<IArtistDetailItem> artistDetailCells;

        public ObservableCollection<IArtistDetailItem> ArtistDetailCells
        {
            get { return artistDetailCells; }
            set
            {
                artistDetailCells = value;
                RaisePropertyChanged("ArtistDetailCells");
            }
        }

        private readonly IArtistDetailsVm _artistDetailsVm;
        public ArtistDetailsFacadeVm(IArtistDetailsVm artistDetailsVm)
        {
            _artistDetailsVm = artistDetailsVm;
            PlayNowCommand = new RelayCommand<int>(playNowExecute, null);
            EnqueueNextCommand = new RelayCommand<int>(enqueueNextExecute, null);
            EnqueueLastCommand = new RelayCommand<int>(enqueueLastExecute, null);
            AddToPlaylistCommand = new RelayCommand<int>(addToPlaylistExecute, null);
            ChangeFavouriteCommand = new RelayCommand(changeFavouriteExecute, null);
            ShowAlbumActionsCommand = new RelayCommand<int>(showAlbumActionsExecute, null);
        }

        private async void showAlbumActionsExecute(int id)
        {
            var dlg = new ActionDialogAlbum();
            var result = await dlg.ShowAsync();
            switch (dlg.ResultCode)
            {
                case AppConstants.ALB_RES_CODE_PLAYNOW:
                    await CompositionRoot.WebService.PlayAlbum(id);
                    break;
                case AppConstants.ALB_RES_CODE_ENQUEUENEXT:
                    await CompositionRoot.WebService.EnqueueAlbumNext(id);
                    break;
                case AppConstants.ALB_RES_CODE_ENQUEUELAST:
                    await CompositionRoot.WebService.EnqueueAlbumLast(id);
                    break;
            }
        }

        private async void changeFavouriteExecute()
        {
            IsFavourite = !IsFavourite;
            Artist.IsFavourite = IsFavourite;
            RaisePropertyChanged("Artist");
            if (IsFavourite)
                await CompositionRoot.WebService.SetArtistAsFavourite(Artist.Id);
            else
                await CompositionRoot.WebService.UnsetArtistAsFavourite(Artist.Id);
        }

        private async void playNowExecute(int id)
        {
            await CompositionRoot.WebService.PlayAlbum(id);
        }
        private async void enqueueNextExecute(int id)
        {
            await CompositionRoot.WebService.EnqueueAlbumNext(id);
        }
        private async void enqueueLastExecute(int id)
        {
            await CompositionRoot.WebService.EnqueueAlbumLast(id);
        }
        private void addToPlaylistExecute(int id)
        {
        }

        private Artist _artist;

        public Artist Artist
        {
            get { return _artist; }
            set { _artist = value; RaisePropertyChanged("Artist"); }
        }

        public async Task Refresh(int id)
        {
            await _artistDetailsVm.Refresh(id);
            Artist = _artistDetailsVm.Artist;
            IsFavourite = Artist.IsFavourite;
            var groupedRaw = new ObservableCollection<AlbumContainer>();
            var res = new ObservableCollection<IArtistDetailItem>();
            res.Add(new ArtistDetailTopCell {Artist = _artist});
            if (_artist.Discography.Any())
            {
                res.Add(new ArtistDetailHeaderCell {Header = "Discography"});
                foreach (var item in _artist.Discography)
                {
                    res.Add(new ArtistDetailAlbumCell { Album = item });
                    groupedRaw.Add(new AlbumContainer {Album = item, IsDiscography = true});
                }
            }
            if (_artist.Appearences.Any())
            {
                res.Add(new ArtistDetailHeaderCell { Header = "Appearences" });
                foreach (var item in _artist.Appearences)
                {
                    res.Add(new ArtistDetailAlbumCell { Album = item });
                    groupedRaw.Add(new AlbumContainer { Album = item, IsDiscography = false });
                }
            }
            ArtistDetailCells = res;

            var releasesGrouped = new List<GroupInfoList<object>>();
            var query = from release in groupedRaw
                        orderby ((AlbumContainer)release).Album.Name
                        group release by ((AlbumContainer)release).IsDiscography into g
                        select new { GroupName = g.Key, Items = g };
            foreach (var g in query)
            {
                var info = new GroupInfoList<object>();
                info.Key = g.GroupName;
                foreach (var friend in g.Items)
                {
                    info.Add(friend);
                }
                releasesGrouped.Add(info);
            }
            if (releasesGrouped != null)
            {
                //var cvs = (CollectionViewSource)Resources["itemsViewSource"];
                Cvs.Source = releasesGrouped;
            }
            ((App)Application.Current).ActiveViewType = UwpViewTypes.ArtistDetail;
        }

        public void UIElement_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            Debug.WriteLine("Tpl righht-tap!");
            FrameworkElement senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
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
            else if (sender is ListBox)
            {
                var lb = (ListBox) sender;
                if (lb.SelectedItem is ArtistDetailAlbumCell)
                {
                    album = ((ArtistDetailAlbumCell) lb.SelectedItem).Album;
                }
            }
            if (album != null)
                AppHelpers.ContentFrame.Navigate(typeof (AlbumDetailsPage), album);
        }
    }
}
