using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Types;

namespace NeonShared.Interfaces
{
    public interface ISearchVm
    {
        IEnumerable<Track> Tracks { get; }
        IEnumerable<Album> Albums { get; }
        IEnumerable<Artist> Artists { get; }
        Task Populate(ViewParameters param);
    }
}
