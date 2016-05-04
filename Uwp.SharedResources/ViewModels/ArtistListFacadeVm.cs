using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Pcl.Interfaces;
using NeonShared.Pcl.Types;
using Uwp.SharedResources.Interfaces;
using Uwp.SharedResources.Types;
using Uwp.SharedResources.Views;

namespace Uwp.SharedResources.ViewModels
{
    public class ArtistListFacadeVm : ViewModelBase, IArtistListFacadeVm, IViewFilter
    {
        private readonly IArtistListVm _artistListVm;
        private readonly ISharedApp _sharedApp;

        private bool _isBusy;
        private ObservableCollection<Artist> _artists;

        private List<Artist> _originalArtists;

        public ArtistListFacadeVm(IArtistListVm artistListVm, ISharedApp sharedApp)
        {
            _artistListVm = artistListVm;
            _sharedApp = sharedApp;
        }

        public ObservableCollection<Artist> Artists
        {
            get { return _artists; }
            private set
            {
                _artists = value;
                RaisePropertyChanged();
            }
        }

        public async Task Refresh(ViewParameters parameters)
        {
            await _artistListVm.Refresh(parameters);
            _originalArtists = (List<Artist>) _artistListVm.Artists;
            Artists = new ObservableCollection<Artist>(_artistListVm.Artists);
            _sharedApp.ViewFilter = this;
            _sharedApp.ActiveViewType = parameters.ViewType;
        }

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Artist item = null;
            if (sender is ListBox)
            {
                var lst = (ListBox) sender;
                item = (Artist) lst.SelectedItem;
            }
            else if (sender is GridView)
            {
                var lv = (GridView) sender;
                item = (Artist) lv.SelectedItem;
            }
            if (item == null)
                return;
            _sharedApp.ContentFrame.Navigate(typeof (ArtistDetailPage), item.Id);
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public void FilterData(string expr)
        {
            _artists.Clear();
            if (string.IsNullOrEmpty(expr))
            {
                ClearFilter();
                return;
            }

            var resd = _originalArtists.Where(
                x => x.ArtistName.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0
                );
            foreach (var artist in resd)
            {
                Artists.Add(artist);
            }
        }

        public void ClearFilter()
        {
            _artists.Clear();
            foreach (var artist in _artistListVm.Artists)
            {
                Artists.Add(artist);
            }
        }

    }
}