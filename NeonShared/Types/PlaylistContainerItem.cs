﻿using System;
using System.Collections.Generic;
using System.Text;
using Neon.Api.Pcl.Models.Entities;

namespace NeonShared.Types
{
    public class PlaylistContainerItem
    {
        public Playlist Playlist { get; set; }
        public int Index { get; set; }
    }
}
