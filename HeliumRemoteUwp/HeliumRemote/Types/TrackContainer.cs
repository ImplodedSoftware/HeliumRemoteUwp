using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using HeliumRemote.Classes;
using Neon.Api.Pcl.Models.Entities;

namespace HeliumRemote.Types
{
    public class TrackContainer : ViewModelBase
    {
        private int _index;
        public Track Track { get; set; }
        public int CurrentCd { get; set; }

        public int Index
        {
            get
            {
                var aid = ((App) Application.Current).ActiveId;
                if (Track.Id == aid)
                    return AppConstants.ACTIVE_ID_DECORATOR;
                return _index;
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