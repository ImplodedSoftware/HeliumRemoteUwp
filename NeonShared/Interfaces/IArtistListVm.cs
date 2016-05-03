using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Types;

namespace NeonShared.Interfaces
{
    public interface IArtistListVm
    {
        IEnumerable<Artist> Artists { get; }
        Task Refresh(ViewParameters parameters);
    }
}
