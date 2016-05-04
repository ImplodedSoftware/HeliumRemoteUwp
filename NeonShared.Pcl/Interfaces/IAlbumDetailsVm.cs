using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;

namespace NeonShared.Pcl.Interfaces
{
    public interface IAlbumDetailsVm
    {
        Album Album { get; }
        Task Refresh(int id);
        IEnumerable<Track> Tracks { get; } 
    }
}
