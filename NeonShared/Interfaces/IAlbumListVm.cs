using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Types;

namespace NeonShared.Interfaces
{
    public interface IAlbumListVm
    {
        IEnumerable<Album> Albums { get; }
        Task Refresh(ViewParameters param);
    }
}
