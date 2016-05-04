#if HR || HMS 
using Foundation;
using HMMShared.Repositories;
#endif
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Pcl.Helpers;
using NeonShared.Pcl.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Version = Neon.Api.Pcl.Models.Entities.Version;

namespace NeonShared.Pcl.Classes
{
    public class WebService : IWebService
    {
        private async Task<string> generalHttpCall(string url)
        {
            var wc = new HttpClient();
            wc.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*.*");
            wc.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, defalate, sdch");
            wc.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "sv-SE, sv; q=0.8,en-US; q=0.6,en; q=0.4");
            wc.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 48.0.2564.109 Safari / 537.36");
            var rep = NeonAppRepository.Instance.Repository.Token;
            if (!string.IsNullOrEmpty(rep))
                wc.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Token " + rep);
            return await wc.GetStringAsync(url);
        }
        private async Task<object> generalBinaryHttpCall(string url)
        {
            var wc = new HttpClient();
            wc.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*.*");
            wc.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, defalate, sdch");
            wc.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "sv-SE, sv; q=0.8,en-US; q=0.6,en; q=0.4");
            wc.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 48.0.2564.109 Safari / 537.36");
            var rep = NeonAppRepository.Instance.Repository.Token;
            if (!string.IsNullOrEmpty(rep))
                wc.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Token " + rep);
            return await wc.GetByteArrayAsync(url);
        }
        internal class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private async Task<string> generalHttpPostCall(string url, string username, string password)
        {
            var lr = new LoginRequest { Password = password, Username = username };
            var json = JsonConvert.SerializeObject(lr);

            var token = string.Empty;
            var wc = new HttpClient();
            wc.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            wc.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, defalate, sdch");
            wc.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "sv-SE, sv; q=0.8,en-US; q=0.6,en; q=0.4");
            wc.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 48.0.2564.109 Safari / 537.36");
            var res = await wc.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var resp = await res.Content.ReadAsStringAsync();
                var jo = JObject.Parse(resp);
                var jt = jo["token"];
                token = jt.Value<string>();
            }
            NeonAppRepository.Instance.Repository.Token = token;
            return token;
        }

        public async Task<IEnumerable<string>> ArtistLetters()
        {
            var res = await generalHttpCall(NeonUrls.ArtistLetters);
            return JsonConvert.DeserializeObject<List<string>>(res);
        }
        public async Task<IEnumerable<string>> AlbumArtistLetters()
        {
            var res = await generalHttpCall(NeonUrls.AlbumArtistLetters);
            return JsonConvert.DeserializeObject<List<string>>(res);
        }
        public async Task<IEnumerable<string>> AlbumLetters()
        {
            var res = await generalHttpCall(NeonUrls.AlbumLetters);
            return JsonConvert.DeserializeObject<List<string>>(res);
        }

        public async Task<IEnumerable<Genre>> GenreLetters()
        {
            var res = await generalHttpCall(NeonUrls.GenreLetters);
            return JsonConvert.DeserializeObject<List<Genre>>(res);
        }

        public async Task<IEnumerable<Track>> TracksFromGenre(string genre)
        {
            var res = await generalHttpCall(NeonUrls.TracksFromGenre(genre));
            return JsonConvert.DeserializeObject<List<Track>>(res);
        }

        public async Task<IEnumerable<string>> LabelLetters()
        {
            var res = await generalHttpCall(NeonUrls.LabelLetters);
            return JsonConvert.DeserializeObject<List<string>>(res);
        }

        public async Task<IEnumerable<string>> LabelsByLetter(string letter)
        {
            var res = await generalHttpCall(NeonUrls.LabelsByLetter(letter));
            return JsonConvert.DeserializeObject<List<string>>(res);
        }

        public async Task<Handshake> Handshake()
        {
            var res = await generalHttpCall(NeonUrls.Handshake);
            return JsonConvert.DeserializeObject<Handshake>(res);
        }

        public async Task<bool> GetRemoteToken(string authKey)
        {
            var json = await generalHttpCall(NeonUrls.RemoteToken(authKey));
            var jo = JObject.Parse(json);
            var jt = jo["token"];
            var token =jt.Value<string>();
            NeonAppRepository.Instance.Repository.Token = token;
            return !string.IsNullOrEmpty(token);
            //#if WINDOWS_UWP
            //            var json = await generalHttpCall(NeonUrls.RemoteToken);
            //            var jo = JsonObject.Parse(json);
            //            var jt = jo["token"];
            //            var token = jt.GetString();
            //            AppRepository.Instance.Repository.Token = token;
            //            return !string.IsNullOrEmpty(token);
            //#else
            //            return true;
            //#endif
        }

        public async Task<IEnumerable<Album>> AlbumsByLetter(string letter)
        {
            var res = await generalHttpCall(NeonUrls.AlbumLetterItems(letter));
            return JsonConvert.DeserializeObject<List<Album>>(res);
        }

        public async Task<IEnumerable<Artist>> ArtistsByLetter(string letter)
        {
            var res = await generalHttpCall(NeonUrls.ArtistLetterItems(letter));
            return JsonConvert.DeserializeObject<List<Artist>>(res);
        }

        public async Task<Artist> ArtistDetails(int id)
        {
            var res = await generalHttpCall(NeonUrls.ArtistDetails(id));
            return JsonConvert.DeserializeObject<Artist>(res);
        }

        public async Task<IEnumerable<Artist>> AlbumArtistsByLetter(string letter)
        {
            var res = await generalHttpCall(NeonUrls.AlbumArtistLetterItems(letter));
            return JsonConvert.DeserializeObject<List<Artist>>(res);
        }

        public async Task<object> AlbumThumbnailImage(int id, int size)
        {
            return await generalBinaryHttpCall(NeonUrls.AlbumImage(id, size));
        }

        public async Task<IEnumerable<Track>> GetPlayQueue()
        {
            var json = await generalHttpCall(NeonUrls.PlayQueue);
            return JsonConvert.DeserializeObject<List<Track>>(json);
        }

        public async Task<PlayerState> GetPlayerState()
        {
            var json = await generalHttpCall(NeonUrls.PlayerState);
            return JsonConvert.DeserializeObject<PlayerState>(json);
        }

        public async Task<NowPlayingInfo> GetNowPlayingInfo()
        {
            var json = await generalHttpCall(NeonUrls.NowPlayingInfo);
            return JsonConvert.DeserializeObject<NowPlayingInfo>(json);
        }

        public async Task PlayAlbum(int id)
        {
            await generalHttpCall(NeonUrls.PlayAlbum(id));
        }
        public async Task EnqueueAlbumNext(int id)
        {
            await generalHttpCall(NeonUrls.EnqueueAlbumNext(id));
        }
        public async Task EnqueueAlbumLast(int id)
        {
            await generalHttpCall(NeonUrls.EnqueueAlbumLast(id));
        }

        public async Task<Album> AlbumDetails(int id)
        {
            var json = await generalHttpCall(NeonUrls.AlbumDetails(id));
            var alblst = JsonConvert.DeserializeObject<List<Album>>(json);
            return alblst.First();
        }

        public async Task<IEnumerable<Track>> TracksForAlbum(int id)
        {
            var json = await generalHttpCall(NeonUrls.AlbumTracks(id));
            return JsonConvert.DeserializeObject<List<Track>>(json);
        }

        public async Task PlayTrack(int id)
        {
            await generalHttpCall(NeonUrls.PlayTrack(id));
        }

        public async Task EnqueueTrackNext(int id)
        {
            await generalHttpCall(NeonUrls.EnqueueTrackNext(id));
        }

        public async Task EnqueueTrackLast(int id)
        {
            await generalHttpCall(NeonUrls.EnqueueTrackLast(id));
        }

        public async Task Play()
        {
            await generalHttpCall(NeonUrls.Play);
        }

        public async Task Pause()
        {
            await generalHttpCall(NeonUrls.Pause);
        }

        public async Task PlayByIndex(int index)
        {
            await generalHttpCall(NeonUrls.PlayByIndex(index));
        }

        public async Task<RemoteResult> GetPlayQueueIndex()
        {
            var json = await generalHttpCall(NeonUrls.PlayQueueIndex);
            return JsonConvert.DeserializeObject<RemoteResult>(json);
        }

        public async Task<List<string>> GetRatingLetters()
        {
            var json = await generalHttpCall(NeonUrls.RatingLetters);
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        public async Task<List<string>> GetGenreLetters()
        {
            var json = await generalHttpCall(NeonUrls.RatingLetters);
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        public async Task<List<string>> GetYearLetters()
        {
            var json = await generalHttpCall(NeonUrls.YearLetters);
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        public async Task<List<string>> GetAddedYearLetters()
        {
            var json = await generalHttpCall(NeonUrls.AddedYearLetters);
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        public async Task<List<string>> GetAddedMonthLetters(int year)
        {
            var json = await generalHttpCall(NeonUrls.AddedMonthLetters(year));
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        public async Task<List<string>> GetAddedDateLetters(int year, int month)
        {
            var json = await generalHttpCall(NeonUrls.AddedDateLetters(year, month));
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        public async Task<List<string>> GetPlayedYearLetters()
        {
            var json = await generalHttpCall(NeonUrls.PlayedYearLetters);
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        public async Task<List<string>> GetPlayedMonthLetters(int year)
        {
            var json = await generalHttpCall(NeonUrls.PlayedMonthLetters(year));
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        public async Task<List<string>> GetPlayedDateLetters(int year, int month)
        {
            var json = await generalHttpCall(NeonUrls.PlayedDateLetters(year, month));
            return JsonConvert.DeserializeObject<List<string>>(json);
        }

        public async Task<IEnumerable<Album>> FavouriteAlbums()
        {
            var json = await generalHttpCall(NeonUrls.FavouriteAlbums);
            return JsonConvert.DeserializeObject<List<Album>>(json);
        }

        public async Task<IEnumerable<Album>> AlbumsFromYear(int year)
        {
            var json = await generalHttpCall(NeonUrls.AlbumsFromYear(year));
            return JsonConvert.DeserializeObject<List<Album>>(json);
        }
        public async Task<IEnumerable<Album>> AlbumsFromLabel(string label)
        {
            var json = await generalHttpCall(NeonUrls.AlbumsFromLabel(label));
            return JsonConvert.DeserializeObject<List<Album>>(json);
        }
        public async Task<IEnumerable<Artist>> FavouriteArtists()
        { 
            var json = await generalHttpCall(NeonUrls.FavouriteArtists);
            return JsonConvert.DeserializeObject<List<Artist>>(json);
        }

        public async Task<IEnumerable<Playlist>> Playlists()
        {
            var json = await generalHttpCall(NeonUrls.Playlists);
            return JsonConvert.DeserializeObject<List<Playlist>>(json);
        }
        public async Task PlayPlaylist(int id)
        {
            await generalHttpCall(NeonUrls.PlayPlaylist(id));
        }
        public async Task SetAlbumAsFavourite(int albumId)
        {
            await generalHttpCall(NeonUrls.SetAlbumAsFavourite(albumId));
        }
        public async Task UnsetAlbumAsFavourite(int albumId)
        {
            await generalHttpCall(NeonUrls.UnsetAlbumAsFavourite(albumId));
        }
        public async Task<IEnumerable<Album>> AlbumsByAddedDate(string date)
        {
            var json = await generalHttpCall(NeonUrls.AlbumsByAddedDate(date));
            return JsonConvert.DeserializeObject<List<Album>>(json);
        }

        public async Task<IEnumerable<Album>> AlbumSearch(string expr)
        {
            var json = await generalHttpCall(NeonUrls.AlbumSearch(expr));
            return JsonConvert.DeserializeObject<List<Album>>(json);
        }

        public async Task<IEnumerable<Track>> FavouriteTracks()
        {
            var json = await generalHttpCall(NeonUrls.FavouriteTracks);
            return JsonConvert.DeserializeObject<List<Track>>(json);
        }

        public async Task<IEnumerable<Track>> TracksByRating(int rating)
        {
            var res = await generalHttpCall(NeonUrls.TracksByRating(rating));
            return JsonConvert.DeserializeObject<List<Track>>(res);
        }

        public async Task<IEnumerable<Track>> TracksByPlayedDate(string date)
        {
            var res = await generalHttpCall(NeonUrls.TracksByPlayedDate(date));
            return JsonConvert.DeserializeObject<List<Track>>(res);
        }

        public async Task<IEnumerable<Track>> PlaylistTracks(int playlistId)
        {
            var res = await generalHttpCall(NeonUrls.PlaylistTracks(playlistId));
            return JsonConvert.DeserializeObject<List<Track>>(res);
        }
        public async Task<IEnumerable<Track>> SmartPlaylistTracks(int smartPlaylistId)
        {
            var res = await generalHttpCall(NeonUrls.SmartPlaylistTracks(smartPlaylistId));
            return JsonConvert.DeserializeObject<List<Track>>(res);
        }

        public async Task<IEnumerable<Track>> TrackSearch(string expr)
        {
            var res = await generalHttpCall(NeonUrls.TrackSearch(expr));
            return JsonConvert.DeserializeObject<List<Track>>(res);
        }

        public async Task<IEnumerable<Track>> SimilarTracks(int id)
        {
            var res = await generalHttpCall(NeonUrls.SimilarTracks(id));
            return JsonConvert.DeserializeObject<List<Track>>(res);
        }

        public async Task SetArtistAsFavourite(int artistId)
        {
            await generalHttpCall(NeonUrls.SetArtistAsFavourite(artistId));
        }

        public async Task UnsetArtistAsFavourite(int artistId)
        {
            await generalHttpCall(NeonUrls.UnsetArtistAsFavourite(artistId));
        }

        public async Task<IEnumerable<Artist>> ArtistSearch(string expr)
        {
            var res = await generalHttpCall(NeonUrls.ArtistSearch(expr));
            return JsonConvert.DeserializeObject<List<Artist>>(res);
        }

        public async Task NextTrack()
        {
            await generalHttpCall(NeonUrls.NextTrack);
        }

        public async Task PreviousTrack()
        {
            await generalHttpCall(NeonUrls.PreviousTrack);
        }

        public async Task RateTrack(int id, int rating)
        {
            await generalHttpCall(NeonUrls.RateTrack(id, rating));
        }

        public async Task SetTrackAsFavourite(int id)
        {
            await generalHttpCall(NeonUrls.SetTrackAsFavourite(id));
        }

        public async Task UnsetTrackAsFavourite(int id)
        {
            await generalHttpCall(NeonUrls.UnsetTrackAsFavourite(id));
        }

        public async Task SetVolume(int volume)
        {
            await generalHttpCall(NeonUrls.SetVolume(volume));
        }

        public async Task SetPosition(int position)
        {
            await generalHttpCall(NeonUrls.SetPosition(position));
        }

        public async Task<string> GetLyrics(int detailId)
        {
            var json = await generalHttpCall(NeonUrls.GetLyrics(detailId));
            return JsonConvert.DeserializeObject<string>(json);
        }

        public async Task UpdatePlaycounter(int detailId)
        {
            await generalHttpCall(NeonUrls.UpdatePcnt(detailId));
        }

        public async Task UpdateLastFm(int detailId)
        {
            await generalHttpCall(NeonUrls.UpdateLastFm(detailId));
        }
        public async Task UpdateLastFmNowPlaying(int detailId)
        {
            await generalHttpCall(NeonUrls.UpdateLastFmNowPlaying(detailId));
        }

        public async Task EnqueuePlaylistNext(int playlistId)
        {
            await generalHttpCall(NeonUrls.EnqueuePlaylistNext(playlistId));
        }

        public async Task EnqueuePlaylistLast(int playlistId)
        {
            await generalHttpCall(NeonUrls.EnqueuePlaylistLast(playlistId));
        }

        public async Task<IEnumerable<Playlist>> SmartPlaylists()
        {
            var json = await generalHttpCall(NeonUrls.SmartPlaylists);
            return JsonConvert.DeserializeObject<List<Playlist>>(json);
        }
        public async Task PlaySmartPlaylist(int id)
        {
            await generalHttpCall(NeonUrls.PlaySmartPlaylist(id));
        }
        public async Task EnqueueSmartPlaylistNext(int playlistId)
        {
            await generalHttpCall(NeonUrls.EnqueueSmartPlaylistNext(playlistId));
        }

        public async Task EnqueueSmartPlaylistLast(int playlistId)
        {
            await generalHttpCall(NeonUrls.EnqueueSmartPlaylistLast(playlistId));
        }

        public async Task<Version> BuildNumber()
        {
            var url = NeonUrls.Version;
            var json = await generalHttpCall(url);
            return JsonConvert.DeserializeObject<Version>(json);
        }

        public async Task<IEnumerable<User>> Users()
        {
            var json = await generalHttpCall(NeonUrls.Users);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        public async Task<string> Login(string username, string password)
        {
            var res = await generalHttpPostCall(NeonUrls.Login, username, password);
            return res;
        }

        public async Task<Track> GetTrack(int detailId)
        {
            var json = await generalHttpCall(NeonUrls.GetTrack(detailId));
            var res = JsonConvert.DeserializeObject<List<Track>>(json);
            return res.First();
        }

        public async Task DownloadFile(int detailId)
        {
#if HR || HMS
            var rep = AppRepositoryManager.Instance.Repository;
            var url = string.Format("{0}/1.0/?method=database.getstream&id={1}", rep.BaseAdress, detailId);

            var ad = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var fn = string.Format("id_{0}.mp3", detailId);
            var filename = Path.Combine(ad, fn);


            var httpClient = new HttpClient();
            try
            {
                var contentsTask = httpClient.GetByteArrayAsync(url);
                var contents = await contentsTask;
                // load from bytes
                if (contents.Length > 0)
                {
                    var imgData = NSData.FromArray(contents);
                    imgData.Save(filename, false);
                }
            }
            catch (Exception)
            {
            }
#endif
        }
    }
}

