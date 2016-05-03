using System;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HeliumRemote.Bootstraper;
using HeliumRemote.Helpers;
using HeliumRemote.Types;
using NeonShared.Classes;
using HeliumRemote.Views;

namespace HeliumRemote.ViewModels
{
    public class SettingsVm : ViewModelBase
    {
        private bool isConnecting;
        public Frame MainFrame { get; set; }
        public RelayCommand<int> LoginCommand { get; }

        public SettingsVm()
        {
            LoginCommand = new RelayCommand<int>(loginCommandExecute, i => { return !isConnecting;});
        }

        private string _address;

        public string Address
        {
            get {  return _address;}
            set { _address = value; RaisePropertyChanged("Address"); }
        }

        private int _port;

        public int Port
        {
            get { return _port;}
            set { _port = value; RaisePropertyChanged("Port"); }
        }

        private async void loginCommandExecute(int id)
        {
            isConnecting = true;
            LoginCommand.RaiseCanExecuteChanged();
            var rep = NeonAppRepository.Instance.Repository;
            if (_address.IndexOf("http://") != 0)
                _address = "http://" + _address;
            var adr = string.Format("{0}:{1}", _address, _port);
            if (adr.IndexOf("http://") != 0)
                adr = "http://" + adr;
            rep.BaseUrl = adr;
            var ws = CompositionRoot.WebService;
            try
            {
                var handShake = await ws.Handshake();
                if (handShake.Result)
                {
                    var res = await ws.GetRemoteToken(handShake.AuthKey);
                    if (res)
                    {
                        await AppHelpers.SaveSettings(new RemoteSettings {BaseAddress = _address, Port = _port});
                        MainFrame.Navigate(typeof (RootPage));
                    }
                }
            }
            catch (Exception)
            {
                var dialog = new MessageDialog(TranslationHelper.GetString("UnableToConnect"));
                await dialog.ShowAsync();
                isConnecting = false;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public void PortTextBox_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case VirtualKey.Number0:
                case VirtualKey.Number1:
                case VirtualKey.Number2:
                case VirtualKey.Number3:
                case VirtualKey.Number4:
                case VirtualKey.Number5:
                case VirtualKey.Number6:
                case VirtualKey.Number7:
                case VirtualKey.Number8:
                case VirtualKey.Number9:
                    e.Handled = false;
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }
    }
}
