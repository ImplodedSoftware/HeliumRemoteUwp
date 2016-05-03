using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HeliumRemote.Views;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;

namespace HeliumRemote.ViewModels
{
    public class AlbumListFacadeVm : ViewModelBase, IAlbumListFacadeVm, IViewFilter
    {
        private readonly IAlbumListVm _albumListVm;

        private ObservableCollection<AlbumContainer> _albums;

        private Thickness _elementMargin;
        private List<Album> _originalAlbums;

        public AlbumListFacadeVm(IAlbumListVm albumListVm)
        {
            _albumListVm = albumListVm;
            PlayNowCommand = new RelayCommand<int>(PlayNowExecute, null);
            EnqueueNextCommand = new RelayCommand<int>(EnqueueNextExecute, null);
            EnqueueLastCommand = new RelayCommand<int>(EnqueueLastExecute, null);
            AddToPlaylistCommand = new RelayCommand<int>(AddToPlaylistExecute, null);
            ShowAlbumActionsCommand = new RelayCommand<int>(ShowAlbumActionsExecute, null);
        }

        public RelayCommand<int> PlayNowCommand { get; }
        public RelayCommand<int> EnqueueNextCommand { get; }
        public RelayCommand<int> EnqueueLastCommand { get; }
        public RelayCommand<int> AddToPlaylistCommand { get; }
        public RelayCommand<int> ShowAlbumActionsCommand { get; }

        public ObservableCollection<AlbumContainer> Albums
        {
            get { return _albums; }
            private set
            {
                _albums = value;
                RaisePropertyChanged();
            }
        }

        public async Task Refresh(ViewParameters param)
        {
            await _albumListVm.Refresh(param);
            var res = new ObservableCollection<AlbumContainer>();
            foreach (var item in _albumListVm.Albums)
                res.Add(new AlbumContainer {Album = item});
            _originalAlbums = (List<Album>) _albumListVm.Albums;
            Albums = res;
            ((App) Application.Current).ViewFilter = this;
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
                AppHelpers.ContentFrame.Navigate(typeof (AlbumDetailsPage), item);
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
            _albums.Clear();
            if (string.IsNullOrEmpty(expr))
            {
                ClearFilter();
            }

            var resd = _originalAlbums.Where(
                x => x.Name.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0 ||
                     x.Artist.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0
                );
            foreach (var album in resd)
            {
                Albums.Add(new AlbumContainer {Album = album});
            }
        }

        public void ClearFilter()
        {
            _albums.Clear();
            foreach (var album in _albumListVm.Albums)
            {
                Albums.Add(new AlbumContainer {Album = album});
            }
        }

        private async void ShowAlbumActionsExecute(int id)
        {
            var dlg = new ActionDialogAlbum();
            await dlg.ShowAsync();
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

        private async void PlayNowExecute(int id)
        {
            await CompositionRoot.WebService.PlayAlbum(id);
        }

        private async void EnqueueNextExecute(int id)
        {
            await CompositionRoot.WebService.EnqueueAlbumNext(id);
        }

        private async void EnqueueLastExecute(int id)
        {
            await CompositionRoot.WebService.EnqueueAlbumLast(id);
        }

        private void AddToPlaylistExecute(int id)
        {
        }
    }
}