using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeliumRemote.Bootstraper;
using HeliumRemote.Classes;
using HeliumRemote.Helpers;
using HeliumRemote.Types;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;

namespace HeliumRemote.ViewModels
{
    public class PlayQueueVm : ViewModelBase
    {
        private readonly IWebService _webService;

        private Thickness _elementMargin;
        private ObservableCollection<TrackContainer> _tracks;

        public PlayQueueVm()
        {
            _webService = CompositionRoot.WebService;
            PlayByIndexCommand = new RelayCommand<int>(PlayByIndexExecute);
        }

        public RelayCommand<int> PlayByIndexCommand { get; }

        public ObservableCollection<TrackContainer> Tracks
        {
            get { return _tracks; }
            set
            {
                _tracks = value;
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

        private void PlayByIndexExecute(int index)
        {
            _webService.PlayByIndex(index);
        }

        public async Task Refresh()
        {
            var res = (List<Track>) await _webService.GetPlayQueue();
            var trks = new List<TrackContainer>();
            for (var i = 0; i < res.Count; i++)
            {
                var trk = res[i];
                trks.Add(new TrackContainer {Track = trk, Index = i, PlayQueueIndex = i});
            }
            Tracks = new ObservableCollection<TrackContainer>(trks);
        }

        public void AdjustUiParts()
        {
            if (DeviceTypeHelper.GetDeviceFormFactorType() == DeviceFormFactorType.Phone)
            {
                ElementMargin = new Thickness(0, 0, 0, 0);
            }
            else
            {
                ElementMargin = new Thickness(0, 0, 24, 0);
            }
        }

        public void PropagateUpdate()
        {
            if (_tracks == null)
                return;
            for (var i = 0; i < _tracks.Count; i++)
            {
                var trk = Tracks[i];
                if (trk.Track.Id == AppHelpers.ActiveId)
                {
                    trk.Index = AppConstants.ACTIVE_ID_DECORATOR;
                }
                else
                {
                    trk.Index = (i & 1) == 1 ? 1 : 0;
                }
                trk.PlayQueueIndex = i;
            }
        }
    }
}