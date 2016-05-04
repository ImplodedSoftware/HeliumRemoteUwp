using GalaSoft.MvvmLight.Ioc;
using HeliumRemote.Classes;
using HeliumRemote.Interfaces;
using HeliumRemote.ViewModels;
using NeonShared.Classes;
using NeonShared.Interfaces;
using NeonShared.ViewModels;

namespace HeliumRemote.Bootstraper
{
    public class CompositionRoot
    {
        private static CompositionRoot _instance;

        static CompositionRoot()
        {
            SimpleIoc.Default.Register<IWebService>(() => new WebService());
            SimpleIoc.Default.Register<IPlayerProvider, RemotePlayerProvider>();

            SimpleIoc.Default.Register<ILetterVm, LetterVm>();
            SimpleIoc.Default.Register<ILetterFacadeVm, LetterFacadeVm>();
            SimpleIoc.Default.Register<IAlbumListVm, AlbumListVm>();
            SimpleIoc.Default.Register<IAlbumListFacadeVm, AlbumListFacadeVm>();
            SimpleIoc.Default.Register<IArtistListVm, ArtistListVm>();
            SimpleIoc.Default.Register<IArtistListFacadeVm, ArtistListFacadeVm>();
            SimpleIoc.Default.Register<IArtistDetailsVm, ArtistDetailsVm>();
            SimpleIoc.Default.Register<IArtistDetailsFacadeVm, ArtistDetailsFacadeVm>();
            SimpleIoc.Default.Register<IAlbumDetailsVm, AlbumDetailsVm>();
            SimpleIoc.Default.Register<IAlbumDetailsFacadeVm, AlbumDetailsFacadeVm>();
            SimpleIoc.Default.Register<ITracksVm, TracksVm>();
            SimpleIoc.Default.Register<ITracksVmFacade, TracksVmFacade>();
            SimpleIoc.Default.Register<IPlaylistsVm, PlaylistsVm>();
            SimpleIoc.Default.Register<IPlaylistsFacadeVm, PlaylistFacadeVm>();
            SimpleIoc.Default.Register<ISearchVm, SearchVm>();
            SimpleIoc.Default.Register<ISearchFacadeVm, SearchFacadeVm>();
            SimpleIoc.Default.Register<INowPlayingVm, NowPlayingVm>();
        }

        public static IWebService WebService => SimpleIoc.Default.GetInstance<IWebService>();
        public static ILetterFacadeVm LetterFacadeVm => SimpleIoc.Default.GetInstance<ILetterFacadeVm>();
        public static IAlbumListFacadeVm AlbumListFacadeVm => SimpleIoc.Default.GetInstance<IAlbumListFacadeVm>();
        public static IArtistListFacadeVm ArtistListFacadeVm => SimpleIoc.Default.GetInstance<IArtistListFacadeVm>();
        public static IArtistDetailsFacadeVm ArtistDetailsFacadeVm => SimpleIoc.Default.GetInstance<IArtistDetailsFacadeVm>();
        public static IAlbumDetailsFacadeVm AlbumDetailsFacadeVm => SimpleIoc.Default.GetInstance<IAlbumDetailsFacadeVm>();
        public static ITracksVmFacade TracksVmFacade => SimpleIoc.Default.GetInstance<ITracksVmFacade>();
        public static IPlaylistsFacadeVm PlaylistsFacadeVm => SimpleIoc.Default.GetInstance<IPlaylistsFacadeVm>();
        public static ISearchFacadeVm SearchFacadeVm => SimpleIoc.Default.GetInstance<ISearchFacadeVm>();
        public static INowPlayingVm NowPlayingVm => SimpleIoc.Default.GetInstance<INowPlayingVm>();

        public static CompositionRoot Instance
        {
            get { return _instance ?? (_instance = new CompositionRoot()); }
        }
    }
}
