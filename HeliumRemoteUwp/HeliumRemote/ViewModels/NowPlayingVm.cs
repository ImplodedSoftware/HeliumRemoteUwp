using System;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeliumRemote.Bootstraper;
using HeliumRemote.Helpers;
using HeliumRemote.Interfaces;
using Neon.Api.Pcl.Models.Entities;

namespace HeliumRemote.ViewModels
{
    public class NowPlayingVm : ViewModelBase, INowPlayingVm
    {
        private int _albumId;
        private string _artist;

        private int _duration;

        private string _infoLine1;
        private string _infoLine2;
        private NowPlayingInfo _nowPlayingInfo;
        private PlayerState _playerState;

        private int _state;

        private string _title;

        private int _trackPosition;

        public NowPlayingVm()
        {
            CloseCommand = new RelayCommand(CloseExecute, null);
            PlayPauseCommand = new RelayCommand(PlayPauseExecute, null);
            PreviousCommand = new RelayCommand(PreviousExecute, null);
            NextCommand = new RelayCommand(NextExecute, null);
        }

        public RelayCommand CloseCommand { get; }
        public RelayCommand PlayPauseCommand { get; }
        public RelayCommand PreviousCommand { get; }
        public RelayCommand NextCommand { get; }

        public string InfoLine1
        {
            get { return _infoLine1; }
            set
            {
                _infoLine1 = value;
                RaisePropertyChanged();
            }
        }

        public string InfoLine2
        {
            get { return _infoLine2; }
            set
            {
                _infoLine2 = value;
                RaisePropertyChanged();
            }
        }

        public Action UpdatePosition { get; set; }
        public Frame MainFrame { get; set; }

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
            }
        }

        public int TrackPosition
        {
            get { return _trackPosition; }
            set
            {
                _trackPosition = value;
                RaisePropertyChanged();
                UpdatePosition?.Invoke();
            }
        }

        public int Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                RaisePropertyChanged();
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
                InfoLine1 = _nowPlayingInfo.Title;
                InfoLine2 = string.Format("{0} {1} {2}", _nowPlayingInfo.Artist,
                    TranslationHelper.GetString("From.Text"),
                    _nowPlayingInfo.Album);
            }
        }

        private async void PreviousExecute()
        {
            await CompositionRoot.WebService.PreviousTrack();
        }

        private async void NextExecute()
        {
            await CompositionRoot.WebService.NextTrack();
        }

        private async void PlayPauseExecute()
        {
            if (_playerState.State == 1)
                await CompositionRoot.WebService.Pause();
            else if (_playerState.State == 2)
                await CompositionRoot.WebService.Play();
        }

        private void CloseExecute()
        {
            MainFrame.GoBack();
        }
    }
}