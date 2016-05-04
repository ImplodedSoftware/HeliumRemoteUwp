﻿using System.Threading.Tasks;

namespace UwpSharedViews.Interfaces
{
    public interface IPlayerProvider
    {
        Task PlayTrack(int id);
        Task EnqueueNext(int id);
        Task EnqueueLast(int id);
        Task PlayAlbum(int id);
        Task EnqueueAlbumNext(int id);
        Task EnqueueAlbumLast(int id);
    }
}
