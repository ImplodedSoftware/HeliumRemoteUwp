using GalaSoft.MvvmLight;
using Neon.Api.Pcl.Models.Entities;
using Uwp.SharedResources.Classes;
using Uwp.SharedResources.Interfaces;

namespace Uwp.SharedResources.Types
{
    public class TrackContainer : ViewModelBase
    {
        private readonly ISharedApp _sharedApp;
        public TrackContainer(ISharedApp sharedApp)
        {
            _sharedApp = sharedApp;
        }
        private int _index;
        public Track Track { get; set; }
        public int CurrentCd { get; set; }

        public int Index
        {
            get
            {
                var aid = _sharedApp.ActiveId;
                return Track.Id == aid ? AppConstants.ACTIVE_ID_DECORATOR : _index;
            }
            set
            {
                _index = value;
                RaisePropertyChanged();
            }
        }

        public int PlayQueueIndex { get; set; }
    }
}