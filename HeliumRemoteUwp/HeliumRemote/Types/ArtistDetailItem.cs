using GalaSoft.MvvmLight;
using Neon.Api.Pcl.Models.Entities;

namespace HeliumRemote.Types
{
    public enum ArtistDetailCellTypes
    {
        TopItem,
        Header,
        AlbumItem
    }

    public interface IArtistDetailItem
    {
        ArtistDetailCellTypes CellType { get; }
    }

    public class ArtistDetailTopCell : IArtistDetailItem
    {
        public Artist Artist { get; set; }

        public ArtistDetailCellTypes CellType
        {
            get { return ArtistDetailCellTypes.TopItem; }
        }
    }

    public class ArtistDetailHeaderCell : IArtistDetailItem
    {
        public string Header { get; set; }

        public ArtistDetailCellTypes CellType
        {
            get { return ArtistDetailCellTypes.Header; }
        }
    }

    public class ArtistDetailAlbumCell : IArtistDetailItem
    {
        public Album Album { get; set; }

        public ArtistDetailCellTypes CellType
        {
            get { return ArtistDetailCellTypes.AlbumItem; }
        }
    }


    public class ArtistDetailItem : ViewModelBase
    {
        private string _name;
        private string _picture;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public string Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                RaisePropertyChanged();
            }
        }
    }
}