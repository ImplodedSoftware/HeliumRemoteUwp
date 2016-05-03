using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using HeliumRemote.Helpers;
using HeliumRemote.Interfaces;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using HeliumRemote.Views;
using NeonShared.Types;

namespace HeliumRemote.ViewModels
{
    public class ArtistListFacadeVm : ViewModelBase, IArtistListFacadeVm, IViewFilter
    {
        private readonly IArtistListVm _artistListVm;

        public ArtistListFacadeVm(IArtistListVm artistListVm)
        {
            _artistListVm = artistListVm;
        }

        private void filterData(string expr)
        {
            artists.Clear();
            if (string.IsNullOrEmpty(expr))
            {
                clearFilter();
                return;
            }

            var sc = StringComparer.CurrentCultureIgnoreCase;
            var resd = originalArtists.Where(
                x => x.ArtistName.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0 
                );
            foreach (var artist in resd)
            {
                Artists.Add(artist);
            }
        }

        private void clearFilter()
        {
            artists.Clear();
            foreach (var artist in _artistListVm.Artists)
            {
                Artists.Add(artist);
            }
        }

        private List<Artist> originalArtists;
        private ObservableCollection<Artist> artists;

        public ObservableCollection<Artist> Artists
        {
            get { return artists; }
            private set
            {
                artists = value;
                RaisePropertyChanged("Artists");
            }
        }
        public async Task Refresh(ViewParameters parameters)
        {
            await _artistListVm.Refresh(parameters);
            originalArtists = (List<Artist>) _artistListVm.Artists;
            Artists = new ObservableCollection<Artist>(_artistListVm.Artists);
            ((App)Application.Current).ViewFilter = this;
            ((App) Application.Current).ActiveViewType = parameters.ViewType;
        }

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Artist item = null;
            if (sender is ListBox)
            {
                var lst = (ListBox)sender;
                item = (Artist)lst.SelectedItem;
            }
            else if (sender is GridView)
            {
                var lv = (GridView)sender;
                item = (Artist)lv.SelectedItem;
            }
            if (item == null)
                return;
            AppHelpers.ContentFrame.Navigate(typeof(ArtistDetailPage), item.Id);
        }

        public void FilterData(string expr)
        {
            filterData(expr);
        }

        public void ClearFilter()
        {
            clearFilter();
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }
    }
}
