using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
using HeliumRemote.Views;
using NeonShared.Types;

namespace HeliumRemote.ViewModels
{
    public class AlbumListFacadeVm : ViewModelBase, IAlbumListFacadeVm, IViewFilter
    {
        private List<Album> originalAlbums;
        public RelayCommand<int> PlayNowCommand { get; }
        public RelayCommand<int> EnqueueNextCommand { get; }
        public RelayCommand<int> EnqueueLastCommand { get; }
        public RelayCommand<int> AddToPlaylistCommand { get; }
        public RelayCommand<int> ShowAlbumActionsCommand { get; }

        private readonly IAlbumListVm _albumListVm;
        public AlbumListFacadeVm(IAlbumListVm albumListVm)
        {
            _albumListVm = albumListVm;
            PlayNowCommand = new RelayCommand<int>(playNowExecute, null);
            EnqueueNextCommand = new RelayCommand<int>(enqueueNextExecute, null);
            EnqueueLastCommand = new RelayCommand<int>(enqueueLastExecute, null);
            AddToPlaylistCommand = new RelayCommand<int>(addToPlaylistExecute, null);
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

        private ObservableCollection<AlbumContainer> albums;

        public ObservableCollection<AlbumContainer> Albums
        {
            get { return albums;}
            private set
            {
                albums = value;
                RaisePropertyChanged("Albums");
            }
        }
        public async Task Refresh(ViewParameters param)
        {
            await _albumListVm.Refresh(param);
            var res = new ObservableCollection<AlbumContainer>();
            foreach (var item in _albumListVm.Albums)
                res.Add(new AlbumContainer {Album = item});
            originalAlbums = (List<Album>)_albumListVm.Albums;
            Albums = res;
            ((App)Application.Current).ViewFilter = this;
            ((App) Application.Current).ActiveViewType = param.ViewType;
        }

        public void GridView_OnTapped(object sender, TappedRoutedEventArgs e)
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
                AppHelpers.ContentFrame.Navigate(typeof(AlbumDetailsPage), item);
        }


        private void clearFilter()
        {
            albums.Clear();
            foreach (var album in _albumListVm.Albums)
            {
                Albums.Add(new AlbumContainer { Album = album });
            }
        }

        private void filterData(string expr)
        {
            Debug.WriteLine("filterData");
            albums.Clear();
            if (string.IsNullOrEmpty(expr))
            {
                clearFilter();
            }

            var sc = StringComparer.CurrentCultureIgnoreCase;
            var resd = originalAlbums.Where(
                x => x.Name.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0 ||
                x.Artist.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0 
                );
            foreach (var album in resd)
            {
                Albums.Add(new AlbumContainer { Album = album });
            }
        }


        public void FilterData(string expr)
        {
            filterData(expr);
        }

        public void ClearFilter()
        {
            clearFilter();
        }
    }
}
