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
        public Action UpdatePosition { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand PlayPauseCommand { get; }
        public RelayCommand PreviousCommand { get; }
        public RelayCommand NextCommand { get; }
        public Frame MainFrame { get; set; }

        public NowPlayingVm()
        {
            CloseCommand = new RelayCommand(closeExecute, null);
            PlayPauseCommand = new RelayCommand(playPauseExecute, null);
            PreviousCommand = new RelayCommand(previousExecute, null);
            NextCommand = new RelayCommand(nextExecute, null);
        }

        private async void previousExecute()
        {
            await CompositionRoot.WebService.PreviousTrack();
        }
        private async void nextExecute()
        {
            await CompositionRoot.WebService.NextTrack();
        }
        private async void playPauseExecute()
        {
            if (_playerState.State == 1)
                await CompositionRoot.WebService.Pause();
            else if (_playerState.State == 2)
                await CompositionRoot.WebService.Play();
        }
        private void closeExecute()
        {
            MainFrame.GoBack();
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
            get { return _albumId; }
            set { _albumId = value; RaisePropertyChanged("AlbumId"); }
        }

        public PlayerState PlayerState
        {
            get { return _playerState; }
            set
            {
                _playerState = value;
                RaisePropertyChanged("PlayerState");
                State = _playerState.State;
                TrackPosition = _playerState.Position;
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
                UpdatePosition?.Invoke();
            }
        }

        private int _duration;

        public int Duration
        {
            get { return _duration; }
            set
            {
                _duration = value; RaisePropertyChanged("Duration");
            }
        }

        private string _infoLine1;

        public string InfoLine1
        {
            get { return _infoLine1;}
            set { _infoLine1 = value; RaisePropertyChanged("InfoLine1"); }
        }
        private string _infoLine2;

        public string InfoLine2
        {
            get { return _infoLine2; }
            set { _infoLine2 = value; RaisePropertyChanged("InfoLine2"); }
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
                InfoLine1 = _nowPlayingInfo.Title;
                InfoLine2 = string.Format("{0} {1} {2}", _nowPlayingInfo.Artist, TranslationHelper.GetString("From.Text"),
                    _nowPlayingInfo.Album);
            }
        }

    }
}
