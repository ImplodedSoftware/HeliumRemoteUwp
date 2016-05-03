using Windows.UI.Xaml;
using GalaSoft.MvvmLight;
using HeliumRemote.Classes;
using Neon.Api.Pcl.Models.Entities;

namespace HeliumRemote.Types
{
    public class TrackContainer : ViewModelBase
    {
        public Track Track { get; set; }
        public int CurrentCd { get; set; }
        private int _index;

        public int Index
        {
            get
            {
                var aid = ((App) Application.Current).ActiveId;
                if (Track.Id == aid)
                    return AppConstants.ACTIVE_ID_DECORATOR;
                else
                {
                    return _index;
                }
            }
            set { _index = value; RaisePropertyChanged("Index");}
        }
        public int PlayQueueIndex { get; set; }
    }
}
