using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
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
    public class TracksVmFacade : ViewModelBase, ITracksVmFacade, IViewFilter
    {
        public RelayCommand<int> ShowTrackActionsCommand { get; }

        private readonly ITracksVm _tracksVm;

        public TracksVmFacade(ITracksVm tracksVm)
        {
            _tracksVm = tracksVm;
            ShowTrackActionsCommand = new RelayCommand<int>(showTrackActionsExecute, null);
        }

        private List<Track> originalTracks;

        private ObservableCollection<TrackContainer> tracks;

        public ObservableCollection<TrackContainer> Tracks
        {
            get {return tracks;}
            set { tracks = value; RaisePropertyChanged("Tracks"); }
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
                res.Add(new TrackContainer {Index = idx++, Track = trk});
            }
            originalTracks = (List<Track>)_tracksVm.Tracks;
            Tracks = res;
            ((App)Application.Current).ViewFilter = this;
            ((App)Application.Current).ActiveViewType = param.ViewType;
        }

        public async Task OnLoaded(object sender, RoutedEventArgs e)
        {
        }
        public void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private void filterData(string expr)
        {
            Debug.WriteLine("filterData");
            tracks.Clear();
            var idx = 0;
            if (string.IsNullOrEmpty(expr))
            {
                clearFilter();
                return;
            }

            var sc = StringComparer.CurrentCultureIgnoreCase;
            var resd = originalTracks.Where(
                x => x.Title.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0 ||
                x.Artist.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0 ||
                x.Album.IndexOf(expr, StringComparison.OrdinalIgnoreCase) >= 0
                );
            idx = 0;
            foreach (var trk in resd)
            {
                Tracks.Add(new TrackContainer { Index = idx++, Track = trk });
            }
        }

        private void clearFilter()
        {
            tracks.Clear();
            var idx = 0;
            foreach (var trk in _tracksVm.Tracks)
            {
                Tracks.Add(new TrackContainer { Index = idx++, Track = trk });
            }
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
