using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Pcl.Types;

namespace NeonShared.Pcl.Interfaces
{
    public interface IArtistListVm
    {
        IEnumerable<Artist> Artists { get; }
        Task Refresh(ViewParameters parameters);
    }
}
