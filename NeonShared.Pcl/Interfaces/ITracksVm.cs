﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Pcl.Types;

namespace NeonShared.Pcl.Interfaces
{
    public interface ITracksVm
    {
        IEnumerable<Track> Tracks { get; }
        Task Populate(ViewTypeTracks vt, ViewParameters param);
    }
}
