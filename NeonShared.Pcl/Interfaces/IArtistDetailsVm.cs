using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;

namespace NeonShared.Pcl.Interfaces
{
    public interface IArtistDetailsVm
    {
        Artist Artist { get; }
        Task Refresh(int id);
    }
}
