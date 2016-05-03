using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeliumRemote.Bootstraper;
using HeliumRemote.Helpers;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;
using HeliumRemote.Views;

namespace HeliumRemote.ViewModels
{
    public class RootViewModel : ViewModelBase
    {
        public Action CloseSlider { get; set; }
        private readonly DispatcherTimer _timer;

        public SplitView SplitView { get; set; }
        private string _pageTitle;

        public string PageTitle
        {
            get { return _pageTitle;}
            set { _pageTitle = value; RaisePropertyChanged("PageTitle"); }
        }
        public RelayCommand PlayPauseCommand { get; }
        public RelayCommand FilterCommand { get; }

        private bool _filterVisible;

        public bool FilterVisible
        {
            get { return _filterVisible; }
            set { _filterVisible = value; RaisePropertyChanged("FilterVisible"); }
        }

        private int _state;
        public int State
        {
            get { return _state; }
            set { _state = value; RaisePropertyChanged("State"); }
        }
        private PlayerState _playerState;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged("Title"); }
        }
        private string _artist;
        public string Artist
        {
            get { return _artist; }
            set { _artist = value; RaisePropertyChanged("Artist"); }
        }
        private int _albumId;

        public int AlbumId
        {
            get { return _albumId;}
            set { _albumId = value; RaisePropertyChanged("AlbumId"); }
        }

        public PlayerState PlayerState
        {
            get { return _playerState;}
            set
            {
                _playerState = value;
                RaisePropertyChanged("PlayerState");
                State = _playerState.State;
                TrackPosition = _playerState.Position;
                var aid = ((App) Application.Current).ActiveId;
                if (aid != _playerState.Id)
                {
                    ((App) Application.Current).ActiveId = _playerState.Id;
                    _updateAction();
                }
                CompositionRoot.NowPlayingVm.PlayerState = _playerState;
            }
        }

        private int _trackPosition;

        public int TrackPosition
        {
            get { return _trackPosition; }
            set
            {
                _trackPosition = value;
                RaisePropertyChanged("TrackPosition");
                CompositionRoot.NowPlayingVm.TrackPosition = _trackPosition;
            }
        }

        private int _duration;

        public int Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                RaisePropertyChanged("Duration");
                CompositionRoot.NowPlayingVm.Duration = value;
            }
        }

        private NowPlayingInfo _nowPlayingInfo;

        public NowPlayingInfo NowPlayingInfo
        {
            get { return _nowPlayingInfo; }
            set
            {
                _nowPlayingInfo = value;
                RaisePropertyChanged("NowPlayingInfo");
                AlbumId = _nowPlayingInfo.AlbumId;
                Title = _nowPlayingInfo.Title;
                Artist = _nowPlayingInfo.Artist;
                Duration = _nowPlayingInfo.Duration;

                CompositionRoot.NowPlayingVm.NowPlayingInfo = value;
            }
        }

        private IWebService _webService;
        private bool _isUpdating;
        private DispatcherTimer _updateTimer;
        private Frame _frame;

        public RelayCommand PlayQueueCommand { get; }
        public RelayCommand ArtistCommand { get; }
        public RelayCommand AlbumsCommand { get; }
        public RelayCommand AlbumArtistCommand { get; }
        public RelayCommand GenresCommand { get; }
        public RelayCommand LabelsCommand { get; }
        public RelayCommand RatingsCommand { get; }
        public RelayCommand YearsCommand { get; }
        public RelayCommand AddedDateCommand { get; }
        public RelayCommand PlayedDateCommand { get; }
        public RelayCommand FavArtistsCommand { get; }
        public RelayCommand FavAlbumsCommand { get; }
        public RelayCommand FavTracksCommand { get; }
        public RelayCommand PlaylistsCommand { get; }
        public RelayCommand SmartPlaylistsCommand { get; }

        private readonly Action _updateAction;
        public RootViewModel(Frame frame, Action updateAction)
        {
            _updateAction = updateAction;
            _webService = CompositionRoot.WebService;
            _frame = frame;
            PlayQueueCommand = new RelayCommand(playQueueExecute, null);
            ArtistCommand = new RelayCommand(artistViewExecute, null);
            AlbumsCommand = new RelayCommand(albumsViewExecute, null);
            AlbumArtistCommand = new RelayCommand(albumArtistViewExecute, null);
            GenresCommand = new RelayCommand(genresViewExecute, null);
            LabelsCommand = new RelayCommand(labelsViewExecute, null);
            RatingsCommand = new RelayCommand(ratingsViewExecute, null);
            YearsCommand = new RelayCommand(yearsViewExecute, null);
            AddedDateCommand = new RelayCommand(addedDateViewExecute, null);
            PlayedDateCommand = new RelayCommand(playedDateViewExecute, null);
            FavArtistsCommand = new RelayCommand(favArtistsViewExecute, null);
            FavAlbumsCommand = new RelayCommand(favAlbumsViewExecute, null);
            FavTracksCommand = new RelayCommand(favTracksViewExecute, null);
            PlaylistsCommand = new RelayCommand(playlistsViewExecute, null);
            SmartPlaylistsCommand = new RelayCommand(smartPlaylistsViewExecute, null);
            FilterCommand = new RelayCommand(filterExecute, null);
            _updateTimer = new DispatcherTimer();
            _updateTimer.Interval = new TimeSpan(1000);
            _updateTimer.Tick += async (sender, o) =>
            {
                if (_isUpdating)
                    return;
                _isUpdating = true;
                var oldFilename = string.Empty;
                if (_playerState != null)
                    oldFilename = _playerState.Filename;
                PlayerState = await _webService.GetPlayerState();
                if (NowPlayingInfo == null || !oldFilename.Equals(PlayerState.Filename))
                {
                    NowPlayingInfo = await _webService.GetNowPlayingInfo();
                }
                _isUpdating = false;
            };
            _updateTimer.Start();
            PlayPauseCommand = new RelayCommand(playPauseExecute, null);
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(AppHelpers.DISPATCHTIMER_LEN) };
            _timer.Tick += TimerOnTick;
        }

        private void filterExecute()
        {
            FilterVisible = !FilterVisible;
        }

        public void PlayPause_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            playPauseExecute();
        }

        private void playPauseExecute()
        {
            if (_playerState.State == 1)
                CompositionRoot.WebService.Pause();
            else if (_playerState.State == 2)
                CompositionRoot.WebService.Play();
        }

        private void playQueueExecute()
        {
            _frame.Navigate(typeof(PlayQueuePage));
            CloseSlider();
        }
        private void artistViewExecute()
        {
            _frame.Navigate(typeof (LettersPage), new ViewParameters {ViewType = UwpViewTypes.ArtistLetters});
            CloseSlider();
        }
        private void albumsViewExecute()
        {
            _frame.Navigate(typeof(LettersPage), new ViewParameters { ViewType = UwpViewTypes.AlbumLetters });
            CloseSlider();
        }
        private void albumArtistViewExecute()
        {
            _frame.Navigate(typeof(LettersPage), new ViewParameters { ViewType = UwpViewTypes.AlbumArtistLetters });
            CloseSlider();
        }

        private void genresViewExecute()
        {
            _frame.Navigate(typeof(LettersPage), new ViewParameters { ViewType = UwpViewTypes.GenreLetters });
            CloseSlider();
        }
        private void labelsViewExecute()
        {
            _frame.Navigate(typeof(LettersPage), new ViewParameters { ViewType = UwpViewTypes.LabelLetters });
            CloseSlider();
        }
        private void ratingsViewExecute()
        {
            _frame.Navigate(typeof(LettersPage), new ViewParameters { ViewType = UwpViewTypes.RatingLetters });
            CloseSlider();
        }
        private void yearsViewExecute()
        {
            _frame.Navigate(typeof(LettersPage), new ViewParameters { ViewType = UwpViewTypes.YearLetters });
            CloseSlider();
        }

        private void addedDateViewExecute()
        {
            _frame.Navigate(typeof(LettersPage), new ViewParameters { ViewType = UwpViewTypes.AddedDateYearLetters });
            CloseSlider();
        }
        private void playedDateViewExecute()
        {
            _frame.Navigate(typeof(LettersPage), new ViewParameters { ViewType = UwpViewTypes.PlayedDateYearLetters });
            CloseSlider();
        }

        private void favArtistsViewExecute()
        {
            _frame.Navigate(typeof(ArtistListPage), new ViewParameters { ViewType = UwpViewTypes.FavouriteArtists });
            CloseSlider();
        }
        private void favAlbumsViewExecute()
        {
            _frame.Navigate(typeof(AlbumListPage), new ViewParameters { ViewType = UwpViewTypes.FavouriteAlbums });
            CloseSlider();
        }
        private void favTracksViewExecute()
        {
            _frame.Navigate(typeof(TracksPage), new ViewParameters { ViewType = UwpViewTypes.FavouriteTracks });
            CloseSlider();
        }

        private void playlistsViewExecute()
        {
            _frame.Navigate(typeof(PlaylistsPage), new ViewParameters { ViewType = UwpViewTypes.Playlists });
            CloseSlider();
        }
        private void smartPlaylistsViewExecute()
        {
            _frame.Navigate(typeof(PlaylistsPage), new ViewParameters { ViewType = UwpViewTypes.SmartPlaylists });
            CloseSlider();
        }

        public void SearchContainer_OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            //SplitView.IsPaneOpen = false;
            var expr = args.QueryText;
            _frame.Navigate(typeof (SearchResultsPage), new ViewParameters {ViewType = UwpViewTypes.SearchResults, Letter = expr});
            CloseSlider();
        }

        private bool _filterAvailable;

        public bool FilterAvailable
        {
            get { return _filterAvailable;}
            set { _filterAvailable = value; RaisePropertyChanged("FilterAvailable"); }
        }

        public TextBox FilterTextBox { get; set; }

        public void FilterTb_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (FilterTextBox.Text.Length >= AppHelpers.MIN_TEXT_LENGTH)
            {
                _timer.Stop();
                _timer.Start();
            }
            else
            {
                if (FilterString != string.Empty)
                {
                    FilterString = string.Empty;
                }
            }
        }

        private void TimerOnTick(object sender, object o)
        {
            _timer.Stop();
            if (FilterTextBox.Text != _filterString)
            {
                FilterString = FilterTextBox.Text;
            }
        }

        private string _filterString;

        public string FilterString
        {
            get { return _filterString; }
            set
            {
                if (_filterString != value)
                {
                    var oldFilter = _filterString;
                    _filterString = value;
                    RaisePropertyChanged("FilterString");
                    if (_filterString.Length >= AppHelpers.MIN_TEXT_LENGTH)
                        filterData();
                    else
                    {
                        clearFilter();
                    }
                }
            }
        }

        private void filterData()
        {
            ((App)Application.Current).ViewFilter.FilterData(_filterString);
        }

        private void clearFilter()
        {
            ((App)Application.Current).ViewFilter.ClearFilter();
        }

    }
}
