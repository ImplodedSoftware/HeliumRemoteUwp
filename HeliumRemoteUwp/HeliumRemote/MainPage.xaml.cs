using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HeliumRemote.Bootstraper;
using HeliumRemote.Helpers;
using HeliumRemote.Views;
using NeonShared.Classes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HeliumRemote
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            var settingsExists = await AppHelpers.SettingsExists();
            if (!settingsExists)
            {
                this.Frame.Navigate(typeof(SettingsPage));
            }
            else
            {
                var remoteSettings = await AppHelpers.LoadSettings();
                var rep = NeonAppRepository.Instance.Repository;
                rep.BaseUrl = string.Format("{0}:{1}", remoteSettings.BaseAddress, remoteSettings.Port);
                var ws = CompositionRoot.WebService;
                try
                {
                    var handShake = await ws.Handshake();
                    if (handShake.Result)
                    {
                        var res = await ws.GetRemoteToken(handShake.AuthKey);
                        this.Frame.Navigate(typeof(RootPage));
                    }
                }
                catch (Exception)
                {
                    this.Frame.Navigate(typeof(SettingsPage));
                }
            }
        }
    }
}
