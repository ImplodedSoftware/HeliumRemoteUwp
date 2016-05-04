using System.Threading.Tasks;
using NeonShared.Interfaces;
using UwpSharedViews.Interfaces;

namespace HeliumRemote.Classes
{
    public class RemotePlayerProvider : IPlayerProvider
    {
        private readonly IWebService _webService;
        public RemotePlayerProvider(IWebService webService)
        {
            _webService = webService;
        }
        public async Task PlayTrack(int id)
        {
            await _webService.PlayTrack(id);
        }

        public async Task EnqueueNext(int id)
        {
            await _webService.EnqueueTrackNext(id);
        }

        public async Task EnqueueLast(int id)
        {
            await _webService.EnqueueTrackLast(id);
        }

        public async Task PlayAlbum(int id)
        {
            await _webService.PlayAlbum(id);
        }

        public async Task EnqueueAlbumNext(int id)
        {
            await _webService.EnqueueAlbumNext(id);
        }

        public async Task EnqueueAlbumLast(int id)
        {
            await _webService.EnqueueAlbumLast(id);
        }
    }
}
