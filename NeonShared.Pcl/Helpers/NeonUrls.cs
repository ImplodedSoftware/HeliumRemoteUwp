using NeonShared.Pcl.Classes;

namespace NeonShared.Pcl.Helpers
{
    public class NeonUrls
    {
        private const string URL_ARTIST_IMAGE_LARGE = "/api/v3/images/artist/{0}";
        private const string URL_ARTIST_IMAGE_WITH_SIZE = "/api/v3/images/artist/{0}/{1}/{1}";
        private const string URL_ALBUM_IMAGE_LARGE = "/api/v3/images/album/{0}";
        private const string URL_ALBUM_IMAGE_WITH_SIZE = "/api/v3/images/album/{0}/{1}/{1}";

        private const string URL_ARTIST_LETTERS = "/api/v3/artists/letters";
        private const string URL_ALBUMARTIST_LETTERS = "/api/v3/albumartists/letters";
        private const string URL_ALBUM_LETTERS = "/api/v3/albums/letters";
        private const string URL_GENRE_LETTERS = "/api/v3/genres/";
        private const string URL_TRACKS_FROM_GENRE = "/api/v3/tracks/genre/{0}";
        private const string URL_LABEL_LETTERS = "/api/v3/labels/letters";
        private const string URL_LABELS_BY_LETTER = "/api/v3/labels/letters/{0}";



        private const string URL_HANDSHAKE = "/api/v3/handshake";
        private const string URL_REMOTETOKEN = "/api/v3/users/remotetoken/{0}";
        private const string URL_ALBUM_ITEMS = "/api/v3/albums/letter/{0}";
        private const string URL_ARTIST_ITEMS = "/api/v3/artists/letter/{0}";
        private const string URL_ARTIST_DETAILS = "/api/v3/artists/details/{0}";
        private const string URL_ALBUMARTIST_ITEMS = "/api/v3/albumartists/letter/{0}";
        private const string URL_PLAYQUEUE = "/api/v3/remote/playqueue";
        private const string URL_PLAYERSTATE = "/api/v3/remote/player/state";
        private const string URL_NOWPLAYING_INFO = "/api/v3/remote/player/nowplayinginfo";
        private const string URL_PLAYALBUM = "/api/v3/remote/album/play/{0}";
        private const string URL_ENQUEUEALBUMNEXT = "/api/v3/remote/album/enqueuenext/{0}";
        private const string URL_ENQUEUEALBUMLAST = "/api/v3/remote/album/enqueuelast/{0}";
        private const string URL_ALBUM_DETAILS = "/api/v3/albums/{0}";
        private const string URL_ALBUM_TRACKS = "/api/v3/tracks/album/{0}";
        private const string URL_PLAYTRACK = "/api/v3/remote/track/play/{0}";
        private const string URL_ENQUEUETRACKNEXT = "/api/v3/remote/track/enqueuenext/{0}";
        private const string URL_ENQUEUETRACKLAST = "/api/v3/remote/track/enqueuelast/{0}";
        private const string URL_PLAY = "/api/v3/remote/player/play";
        private const string URL_PAUSE = "/api/v3/remote/player/pause";
        private const string URL_PLAY_INDEX = "/api/v3/remote/playqueue/play/{0}";
        private const string URL_GET_ACTIVE_INDEX  = "/api/v3/remote/playqueue/activeindex";
        private const string URL_RATING_LETTERS = "/api/v3/ratings";
        private const string URL_YEAR_LETTERS = "/api/v3/years";
        private const string URL_ADDED_YEAR_LETTERS = "/api/v3/years/added";
        private const string URL_ADDED_MONTH_LETTERS = "/api/v3/years/added/{0}";
        private const string URL_ADDED_DATE_LETTERS = "/api/v3/years/added/{0}/{1}";
        private const string URL_PLAYED_YEAR_LETTERS = "/api/v3/years/played";
        private const string URL_PLAYED_MONTH_LETTERS = "/api/v3/years/played/{0}";
        private const string URL_PLAYED_DATE_LETTERS = "/api/v3/years/played/{0}/{1}";
        private const string URL_FAVOURITE_ALBUMS = "/api/v3/albums/favourites";
        private const string URL_ALBUMS_FROM_YEAR = "/api/v3/albums/year/{0}";
        private const string URL_ALBUMS_FROM_LABEL = "/api/v3/albums/label/{0}";
        private const string URL_FAVOURITE_ARTISTS = "/api/v3/artists/favourites";
        private const string URL_PLAYLISTS = "/api/v3/playlists";
        private const string URL_PLAY_PLAYLIST = "/api/v3/remote/playlist/play/{0}";
        private const string URL_SET_ALBUM_AS_FAVOURITE = "/api/v3/albums/setfavourite/{0}";
        private const string URL_UNSET_ALBUM_AS_FAVOURITE = "/api/v3/albums/unsetfavourite/{0}";
        private const string URL_ALBUMS_ADDED_DATE = "/api/v3/albums/addeddate/{0}";
        private const string URL_ALBUM_SEARCH = "/api/v3/albums/search/{0}";
        private const string URL_FAVOURITE_TRACKS = "/api/v3/tracks/favourites";
        private const string URL_TRACKS_BY_RATING = "/api/v3/tracks/rating/{0}";
        private const string URL_TRACKS_BY_PLAYED_DATE = "/api/v3/tracks/played/{0}";
        private const string URL_PLAYLIST_TRACKS = "/api/v3/tracks/playlist/{0}";
        private const string URL_SMARTPLAYLIST_TRACKS = "/api/v3/tracks/smartplaylist/{0}";
        private const string URL_TRACK_SEARCH = "/api/v3/tracks/search/{0}";
        private const string URL_TRACK_SIMILAR = "/api/v3/tracks/similar/{0}";
        private const string URL_SET_ARTIST_AS_FAVOURITE = "/api/v3/artists/setfavourite/{0}";
        private const string URL_UNSET_ARTIST_AS_FAVOURITE = "/api/v3/artists/unsetfavourite/{0}";
        private const string URL_ARTIST_SEARCH = "/api/v3/artists/search/{0}";
        private const string URL_NEXT = "/api/v3/remote/player/next";
        private const string URL_PREVIOUS = "/api/v3/remote/player/previous";
        private const string URL_RATE_TRACK = "/api/v3/tracks/setrating/{0}/{1}";
        private const string URL_SET_TRACK_AS_FAVOURITE = "/api/v3/tracks/setfavourite/{0}";
        private const string URL_UNSET_TRACK_AS_FAVOURITE = "/api/v3/tracks/unsetfavourite/{0}";
        private const string URL_SET_VOLUME = "/api/v3/remote/player/setvolume/{0}";
        private const string URL_SET_POSITION = "/api/v3/remote/player/setposition/{0}";
        private const string URL_GET_LYRICS = "/api/v3/tracks/lyrics/{0}";
        private const string URL_UPDATE_PCNT = "/api/v3/tracks/updateplaycounter/{0}";
        private const string URL_UPDATE_LASTFM = "/api/v3/tracks/posttolastfm/{0}";
        private const string URL_UPDATE_LASTFM_NOW_PLAYING = "/api/v3/tracks/updatelastfm/{0}";
        private const string URL_ENQUEUE_PLAYLIST_NEXT = "/api/v3/remote/playlist/enqueuenext/{0}";
        private const string URL_ENQUEUE_PLAYLIST_LAST = "/api/v3/remote/playlist/enqueuelast/{0}";
        private const string URL_GET_SMARTPLAYLISTS = "/api/v3/smartplaylists/";
        private const string URL_PLAY_SMARTPLAYLIST = "/api/v3/remote/smartplaylist/play/{0}";
        private const string URL_ENQUEUE_SMARTPLAYLIST_NEXT = "/api/v3/remote/smartplaylist/enqueuenext/{0}";
        private const string URL_ENQUEUE_SMARTPLAYLIST_LAST = "/api/v3/remote/smartplaylist/enqueuelast/{0}";
        private const string URL_VERSION = "/api/v3/version";
        private const string URL_GET_USERS = "/api/v3/users/list";
        private const string URL_GET_LOGIN = "/api/v3/users/login";
        private const string URL_GET_TRACK = "/api/v3/tracks/{0}";
        private const string URL_GET_STREAM = "/api/v3/stream/{0}";


        private static string baseUrl
        {
            get { return NeonAppRepository.Instance.Repository.BaseUrl; }
        }
        public static string LargeArtistImage(int id)
        {
            return baseUrl + string.Format(URL_ARTIST_IMAGE_LARGE, id);
        }
        public static string ArtistImage(int id, int size)
        {
            return baseUrl + string.Format(URL_ARTIST_IMAGE_WITH_SIZE, id, size);
        }
        public static string LargeAlbumImage(int id)
        {
            return baseUrl + string.Format(URL_ALBUM_IMAGE_LARGE, id);
        }
        public static string AlbumImage(int id, int size)
        {
            return baseUrl + string.Format(URL_ALBUM_IMAGE_WITH_SIZE, id, size);
        }
        public static string ArtistLetters
        {
            get { return baseUrl + URL_ARTIST_LETTERS; }
        }
        public static string AlbumArtistLetters
        {
            get { return baseUrl + URL_ALBUMARTIST_LETTERS; }
        }
        public static string AlbumLetters
        {
            get { return baseUrl + URL_ALBUM_LETTERS; }
        }
        public static string Handshake
        {
            get
            {
                var res = baseUrl + URL_HANDSHAKE;
                return res;
            }
        }
        public static string RemoteToken(string key)
        {
            return baseUrl + string.Format(URL_REMOTETOKEN, key); 
        }

        public static string AlbumLetterItems(string expr)
        {
            return baseUrl + string.Format(URL_ALBUM_ITEMS, expr);
        }
        public static string ArtistLetterItems(string expr)
        {
            return baseUrl + string.Format(URL_ARTIST_ITEMS, expr);
        }
        public static string ArtistDetails(int id)
        {
            return baseUrl + string.Format(URL_ARTIST_DETAILS, id);
        }
        public static string AlbumArtistLetterItems(string expr)
        {
            return baseUrl + string.Format(URL_ALBUMARTIST_ITEMS, expr);
        }
        public static string GenreLetters
        {
            get { return baseUrl + URL_GENRE_LETTERS; }
        }
        public static string TracksFromGenre(string expr)
        {
            return baseUrl + string.Format(URL_TRACKS_FROM_GENRE, expr);
        }

        public static string LabelLetters
        {
            get { return baseUrl + URL_LABEL_LETTERS; }
        }
        public static string LabelsByLetter(string expr)
        {
            return baseUrl + string.Format(URL_LABELS_BY_LETTER, expr);
        }


        public static string PlayQueue
        {
            get { return baseUrl + URL_PLAYQUEUE; }
        }
        public static string PlayerState
        {
            get { return baseUrl + URL_PLAYERSTATE; }
        }
        public static string NowPlayingInfo
        {
            get { return baseUrl + URL_NOWPLAYING_INFO; }
        }

        public static string PlayAlbum(int id)
        {
            return baseUrl + string.Format(URL_PLAYALBUM, id);
        }
        public static string EnqueueAlbumNext(int id)
        {
            return baseUrl + string.Format(URL_ENQUEUEALBUMNEXT, id);
        }
        public static string EnqueueAlbumLast(int id)
        {
            return baseUrl + string.Format(URL_ENQUEUEALBUMLAST, id);
        }
        public static string AlbumDetails(int id)
        {
            return baseUrl + string.Format(URL_ALBUM_DETAILS, id);
        }
        public static string AlbumTracks(int id)
        {
            return baseUrl + string.Format(URL_ALBUM_TRACKS, id);
        }
        public static string PlayTrack(int id)
        {
            return baseUrl + string.Format(URL_PLAYTRACK, id);
        }
        public static string EnqueueTrackNext(int id)
        {
            return baseUrl + string.Format(URL_ENQUEUETRACKNEXT, id);
        }
        public static string EnqueueTrackLast(int id)
        {
            return baseUrl + string.Format(URL_ENQUEUETRACKLAST, id);
        }

        public static string Play
        {
            get { return baseUrl + URL_PLAY; }
        }
        public static string Pause
        {
            get { return baseUrl + URL_PAUSE; }
        }

        public static string PlayByIndex(int index)
        {
            return baseUrl + string.Format(URL_PLAY_INDEX, index);
        }

        public static string PlayQueueIndex
        {
            get
            {
                return baseUrl + URL_GET_ACTIVE_INDEX;
            }
        }
        public static string RatingLetters
        {
            get
            {
                return baseUrl + URL_RATING_LETTERS;
            }
        }
        public static string YearLetters
        {
            get
            {
                return baseUrl + URL_YEAR_LETTERS;
            }
        }
        public static string AddedYearLetters
        {
            get
            {
                return baseUrl + URL_ADDED_YEAR_LETTERS;
            }
        }
        public static string AddedMonthLetters(int year)
        {
            return baseUrl + string.Format(URL_ADDED_MONTH_LETTERS, year);
        }
        public static string AddedDateLetters(int year, int month)
        {
            return baseUrl + string.Format(URL_ADDED_DATE_LETTERS, year, month);
        }

        public static string PlayedYearLetters => baseUrl + URL_PLAYED_YEAR_LETTERS;

        public static string PlayedMonthLetters(int year)
        {
            return baseUrl + string.Format(URL_PLAYED_MONTH_LETTERS, year);
        }
        public static string PlayedDateLetters(int year, int month)
        {
            return baseUrl + string.Format(URL_PLAYED_DATE_LETTERS, year, month);
        }
        public static string FavouriteAlbums => baseUrl + URL_FAVOURITE_ALBUMS;
        public static string AlbumsFromYear(int year)
        {
            return baseUrl + string.Format(URL_ALBUMS_FROM_YEAR, year);
        }
        public static string AlbumsFromLabel(string label)
        {
            return baseUrl + string.Format(URL_ALBUMS_FROM_LABEL, label);
        }
        public static string FavouriteArtists => baseUrl + URL_FAVOURITE_ARTISTS;
        public static string Playlists => baseUrl + URL_PLAYLISTS;
        public static string PlayPlaylist(int id)
        {
            return baseUrl + string.Format(URL_PLAY_PLAYLIST, id);
        }
        public static string SetAlbumAsFavourite(int id)
        {
            return baseUrl + string.Format(URL_SET_ALBUM_AS_FAVOURITE, id);
        }
        public static string UnsetAlbumAsFavourite(int id)
        {
            return baseUrl + string.Format(URL_UNSET_ALBUM_AS_FAVOURITE, id);
        }
        public static string AlbumsByAddedDate(string addedDate)
        {
            return baseUrl + string.Format(URL_ALBUMS_ADDED_DATE, addedDate);
        }
        public static string AlbumSearch(string expr)
        {
            return baseUrl + string.Format(URL_ALBUM_SEARCH, expr);
        }
        public static string FavouriteTracks => baseUrl + URL_FAVOURITE_TRACKS;
        public static string TracksByRating(int rating)
        {
            return baseUrl + string.Format(URL_TRACKS_BY_RATING, rating);
        }
        public static string TracksByPlayedDate(string date)
        {
            return baseUrl + string.Format(URL_TRACKS_BY_PLAYED_DATE, date);
        }
        public static string PlaylistTracks(int playlistId)
        {
            return baseUrl + string.Format(URL_PLAYLIST_TRACKS, playlistId);
        }
        public static string SmartPlaylistTracks(int smartPlaylistId)
        {
            return baseUrl + string.Format(URL_SMARTPLAYLIST_TRACKS, smartPlaylistId);
        }
        public static string TrackSearch(string expr)
        {
            return baseUrl + string.Format(URL_TRACK_SEARCH, expr);
        }
        public static string SimilarTracks(int id)
        {
            return baseUrl + string.Format(URL_TRACK_SIMILAR, id);
        }
        public static string SetArtistAsFavourite(int id)
        {
            return baseUrl + string.Format(URL_SET_ARTIST_AS_FAVOURITE, id);
        }
        public static string UnsetArtistAsFavourite(int id)
        {
            return baseUrl + string.Format(URL_UNSET_ARTIST_AS_FAVOURITE, id);
        }
        public static string ArtistSearch(string expr)
        {
            return baseUrl + string.Format(URL_ARTIST_SEARCH, expr);
        }
        public static string NextTrack => baseUrl + URL_NEXT;
        public static string PreviousTrack => baseUrl + URL_PREVIOUS;
        public static string RateTrack(int id, int rating)
        {
            return baseUrl + string.Format(URL_RATE_TRACK, id, rating);
        }
        public static string SetTrackAsFavourite(int id)
        {
            return baseUrl + string.Format(URL_SET_TRACK_AS_FAVOURITE, id);
        }
        public static string UnsetTrackAsFavourite(int id)
        {
            return baseUrl + string.Format(URL_UNSET_TRACK_AS_FAVOURITE, id);
        }
        public static string SetVolume(int volume)
        {
            return baseUrl + string.Format(URL_SET_VOLUME, volume);
        }
        public static string SetPosition(int position)
        {
            return baseUrl + string.Format(URL_SET_POSITION, position);
        }
        public static string GetLyrics(int detailId)
        {
            return baseUrl + string.Format(URL_GET_LYRICS, detailId);
        }
        public static string UpdatePcnt(int detailId)
        {
            return baseUrl + string.Format(URL_UPDATE_PCNT, detailId);
        }

        public static string UpdateLastFm(int detailId)
        {
            return baseUrl + string.Format(URL_UPDATE_LASTFM, detailId);
        }
        public static string UpdateLastFmNowPlaying(int detailId)
        {
            return baseUrl + string.Format(URL_UPDATE_LASTFM_NOW_PLAYING, detailId);
        }

        public static string EnqueuePlaylistNext(int playlistId)
        {
            return baseUrl + string.Format(URL_ENQUEUE_PLAYLIST_NEXT, playlistId);
        }
        public static string EnqueuePlaylistLast(int playlistId)
        {
            return baseUrl + string.Format(URL_ENQUEUE_PLAYLIST_LAST, playlistId);
        }
        public static string SmartPlaylists => baseUrl + URL_GET_SMARTPLAYLISTS;

        public static string PlaySmartPlaylist(int id)
        {
            return baseUrl + string.Format(URL_PLAY_SMARTPLAYLIST, id);
        }
        public static string EnqueueSmartPlaylistNext(int playlistId)
        {
            return baseUrl + string.Format(URL_ENQUEUE_SMARTPLAYLIST_NEXT, playlistId);
        }
        public static string EnqueueSmartPlaylistLast(int playlistId)
        {
            return baseUrl + string.Format(URL_ENQUEUE_SMARTPLAYLIST_LAST, playlistId);
        }

        public static string Version => baseUrl + URL_VERSION;
        public static string Users => baseUrl + URL_GET_USERS;
        public static string Login => baseUrl + URL_GET_LOGIN;
        public static string GetTrack(int detailId)
        {
            return baseUrl + string.Format(URL_GET_TRACK, detailId);
        }
        public static string GetStream(int detailId)
        {
            return baseUrl + string.Format(URL_GET_STREAM, detailId);
        }
    }
}
