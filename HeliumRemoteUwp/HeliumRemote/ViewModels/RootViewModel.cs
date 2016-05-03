using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeliumRemote.Bootstraper;
using HeliumRemote.Helpers;
using HeliumRemote.Views;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Interfaces;
using NeonShared.Types;

namespace HeliumRemote.ViewModels
{
    public class RootViewModel : ViewModelBase
    {
        private readonly DispatcherTimer _timer;

        private readonly Action _updateAction;
        private int _albumId;
        private string _artist;

        private int _duration;

        private bool _filterAvailable;

        private string _filterString;

        private bool _filterVisible;
        private readonly Frame _frame;
        private bool _isUpdating;

        private NowPlayingInfo _nowPlayingInfo;
        private string _pageTitle;
        private PlayerState _playerState;

        private int _state;

        private string _title;

        private int _trackPosition;
        private readonly DispatcherTimer _updateTimer;

        private readonly IWebService _webService;

        public RootViewModel(Frame frame, Action updateAction)
        {
            _updateAction = updateAction;
            _webService = CompositionRoot.WebService;
            _frame = frame;
            PlayQueueCommand = new RelayCommand(PlayQueueExecute, null);
            ArtistCommand = new RelayCommand(ArtistViewExecute, null);
            AlbumsCommand = new RelayCommand(AlbumsViewExecute, null);
            AlbumArtistCommand = new RelayCommand(AlbumArtistViewExecute, null);
            GenresCommand = new RelayCommand(GenresViewExecute, null);
            LabelsCommand = new RelayCommand(LabelsViewExecute, null);
            RatingsCommand = new RelayCommand(RatingsViewExecute, null);
            YearsCommand = new RelayCommand(YearsViewExecute, null);
            AddedDateCommand = new RelayCommand(AddedDateViewExecute, null);
            PlayedDateCommand = new RelayCommand(PlayedDateViewExecute, null);
            FavArtistsCommand = new RelayCommand(FavArtistsViewExecute, null);
            FavAlbumsCommand = new RelayCommand(FavAlbumsViewExecute, null);
            FavTracksCommand = new RelayCommand(FavTracksViewExecute, null);
            PlaylistsCommand = new RelayCommand(PlaylistsViewExecute, null);
            SmartPlaylistsCommand = new RelayCommand(SmartPlaylistsViewExecute, null);
            FilterCommand = new RelayCommand(FilterExecute, null);
            _updateTimer = new DispatcherTimer {Interval = new TimeSpan(1000)};
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
            PlayPauseCommand = new RelayCommand(PlayPauseExecute, null);
            _timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(AppHelpers.DISPATCHTIMER_LEN)};
            _timer.Tick += TimerOnTick;
        }

        public Action CloseSlider { get; set; }

        public SplitView SplitView { get; set; }

        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                _pageTitle = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand PlayPauseCommand { get; }
        public RelayCommand FilterCommand { get; }

        public bool FilterVisible
        {
            get { return _filterVisible; }
            set
            {
                _filterVisible = value;
                RaisePropertyChanged();
            }
        }

        public int State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public string Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                RaisePropertyChanged();
            }
        }

        public int AlbumId
        {
            get { return _albumId; }
            set
            {
                _albumId = value;
                RaisePropertyChanged();
            }
        }

        public PlayerState PlayerState
        {
            get { return _playerState; }
            set
            {
                _playerState = value;
                RaisePropertyChanged();
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

        public int TrackPosition
        {
            get { return _trackPosition; }
            set
            {
                _trackPosition = value;
                RaisePropertyChanged();
                CompositionRoot.NowPlayingVm.TrackPosition = _trackPosition;
            }
        }

        public int Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                RaisePropertyChanged();
                CompositionRoot.NowPlayingVm.Duration = value;
            }
        }

        public NowPlayingInfo NowPlayingInfo
        {
            get { return _nowPlayingInfo; }
            set
            {
                _nowPlayingInfo = value;
                RaisePropertyChanged();
                AlbumId = _nowPlayingInfo.AlbumId;
                Title = _nowPlayingInfo.Title;
                Artist = _nowPlayingInfo.Artist;
                Duration = _nowPlayingInfo.Duration;

                CompositionRoot.NowPlayingVm.NowPlayingInfo = value;
            }
        }

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

        public bool FilterAvailable
        {
            get { return _filterAvailable; }
            set
            {
                _filterAvailable = value;
                RaisePropertyChanged();
            }
        }

        public TextBox FilterTextBox { get; set; }

        public string FilterString
        {
            get { return _filterString; }
            set
            {
                if (_filterString != value)
                {
                    _filterString = value;
                    RaisePropertyChanged();
                    if (_filterString.Length >= AppHelpers.MIN_TEXT_LENGTH)
                        FilterData();
                    else
                    {
                        ClearFilter();
                    }
                }
            }
        }

        private void FilterExecute()
        {
            FilterVisible = !FilterVisible;
        }

        public void PlayPause_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            PlayPauseExecute();
        }

        private void PlayPauseExecute()
        {
            if (_playerState.State == 1)
                CompositionRoot.WebService.Pause();
            else if (_playerState.State == 2)
                CompositionRoot.WebService.Play();
        }

        private void PlayQueueExecute()
        {
            _frame.Navigate(typeof (PlayQueuePage));
            CloseSlider();
        }

        private void ArtistViewExecute()
        {
            _frame.Navigate(typeof (LettersPage), new ViewParameters {ViewType = UwpViewTypes.ArtistLetters});
            CloseSlider();
        }

        private void AlbumsViewExecute()
        {
            _frame.Navigate(typeof (LettersPage), new ViewParameters {ViewType = UwpViewTypes.AlbumLetters});
            CloseSlider();
        }

        private void AlbumArtistViewExecute()
        {
            _frame.Navigate(typeof (LettersPage), new ViewParameters {ViewType = UwpViewTypes.AlbumArtistLetters});
            CloseSlider();
        }

        private void GenresViewExecute()
        {
            _frame.Navigate(typeof (LettersPage), new ViewParameters {ViewType = UwpViewTypes.GenreLetters});
            CloseSlider();
        }

        private void LabelsViewExecute()
        {
            _frame.Navigate(typeof (LettersPage), new ViewParameters {ViewType = UwpViewTypes.LabelLetters});
            CloseSlider();
        }

        private void RatingsViewExecute()
        {
            _frame.Navigate(typeof (LettersPage), new ViewParameters {ViewType = UwpViewTypes.RatingLetters});
            CloseSlider();
        }

        private void YearsViewExecute()
        {
            _frame.Navigate(typeof (LettersPage), new ViewParameters {ViewType = UwpViewTypes.YearLetters});
            CloseSlider();
        }

        private void AddedDateViewExecute()
        {
            _frame.Navigate(typeof (LettersPage), new ViewParameters {ViewType = UwpViewTypes.AddedDateYearLetters});
            CloseSlider();
        }

        private void PlayedDateViewExecute()
        {
            _frame.Navigate(typeof (LettersPage), new ViewParameters {ViewType = UwpViewTypes.PlayedDateYearLetters});
            CloseSlider();
        }

        private void FavArtistsViewExecute()
        {
            _frame.Navigate(typeof (ArtistListPage), new ViewParameters {ViewType = UwpViewTypes.FavouriteArtists});
            CloseSlider();
        }

        private void FavAlbumsViewExecute()
        {
            _frame.Navigate(typeof (AlbumListPage), new ViewParameters {ViewType = UwpViewTypes.FavouriteAlbums});
            CloseSlider();
        }

        private void FavTracksViewExecute()
        {
            _frame.Navigate(typeof (TracksPage), new ViewParameters {ViewType = UwpViewTypes.FavouriteTracks});
            CloseSlider();
        }

        private void PlaylistsViewExecute()
        {
            _frame.Navigate(typeof (PlaylistsPage), new ViewParameters {ViewType = UwpViewTypes.Playlists});
            CloseSlider();
        }

        private void SmartPlaylistsViewExecute()
        {
            _frame.Navigate(typeof (PlaylistsPage), new ViewParameters {ViewType = UwpViewTypes.SmartPlaylists});
            CloseSlider();
        }

        public void SearchContainer_OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var expr = args.QueryText;
            _frame.Navigate(typeof (SearchResultsPage),
                new ViewParameters {ViewType = UwpViewTypes.SearchResults, Letter = expr});
            CloseSlider();
        }

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

        private void FilterData()
        {
            ((App) Application.Current).ViewFilter.FilterData(_filterString);
        }

        private void ClearFilter()
        {
            ((App) Application.Current).ViewFilter.ClearFilter();
        }
    }
}