using System;
using Windows.UI.Xaml.Controls;
using Neon.Api.Pcl.Models.Entities;

namespace HeliumRemote.Interfaces
{
    public interface INowPlayingVm
    {
        int State { get; set; }
        string Title { get; set; }
        string Artist { get; set; }
        int AlbumId { get; set; }
        PlayerState PlayerState { get; set; }
        int TrackPosition { get; set; }
        int Duration { get; set; }
        NowPlayingInfo NowPlayingInfo { get; set; }
        Frame MainFrame { get; set; }
        Action UpdatePosition { get; set; }
    }
}
