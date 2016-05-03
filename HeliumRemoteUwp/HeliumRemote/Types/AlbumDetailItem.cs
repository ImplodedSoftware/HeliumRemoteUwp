using Neon.Api.Pcl.Models.Entities;

namespace HeliumRemote.Types
{
    public enum AlbumDetailCellTypes
    {
        TopItem,
        Header,
        TrackItem
    }
    public interface IAlbumDetailItem
    {
        AlbumDetailCellTypes CellType { get; }
    }

    public class AlbumDetailTopCell : IAlbumDetailItem
    {
        public Album Album { get; set; }
        public AlbumDetailCellTypes CellType { get { return AlbumDetailCellTypes.TopItem; } }
    }

    public class AlbumDetailHeaderCell : IAlbumDetailItem
    {
        public string Header { get; set; }
        public AlbumDetailCellTypes CellType { get { return AlbumDetailCellTypes.Header; } }
    }

    public class AlbumDetailTrackCell : IAlbumDetailItem
    {
        public Track Track { get; set; }
        public int Index { get; set; }
        public AlbumDetailCellTypes CellType { get { return AlbumDetailCellTypes.TrackItem; } }
    }
}
