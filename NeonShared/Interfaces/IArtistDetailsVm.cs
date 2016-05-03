using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;

namespace NeonShared.Interfaces
{
    public interface IArtistDetailsVm
    {
        Artist Artist { get; }
        Task Refresh(int id);
    }
}
