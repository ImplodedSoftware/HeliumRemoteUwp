using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HeliumRemote.Types;
using Newtonsoft.Json;
using Uwp.SharedResources.Helpers;

namespace HeliumRemote.Helpers
{
    public static class AppHelpers
    {
        public const int DISPATCHTIMER_LEN = 750;
        public const int MIN_TEXT_LENGTH = 3;
        public const string SETTINGS_FILE = "remotesettings.json";
        public const string NEON_SETTINGS = "neonSettings";

        public static int LargeImageSize
        {
            get
            {
                if (DeviceTypeHelper.GetDeviceFormFactorType() == DeviceFormFactorType.Phone)
                {
                    var di = DisplayInformation.GetForCurrentView();
                    var scaleFac = di.RawPixelsPerViewPixel;
                    if (scaleFac >= 4)
                        return 240;
                    if (scaleFac >= 3 && scaleFac < 4)
                        return 176;
                    if (scaleFac >= 2.5 && scaleFac < 3)
                        return 256;
                    if (scaleFac >= 2 && scaleFac < 2.5)
                        return 240;
                    if (scaleFac < 2)
                        return 208;
                }
                return 256;
            }
        }

        public static double LargeImageSizeDownload
        {
            get
            {
                var scaleFac = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
                return scaleFac*256;
            }
        }

        public static Frame ContentFrame
        {
            get
            {
                var app = (App) Application.Current;
                return app.ContentFrame;
            }
        }

        public static int ActiveId
        {
            get { return ((App) Application.Current).ActiveId; }
        }

        public static async Task<bool> SettingsExists()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            try
            {
                var foundFile = await storageFolder.GetFileAsync(SETTINGS_FILE);
                return foundFile != null;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        public static async Task SaveSettings(RemoteSettings settings)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var settingsFile =
                await storageFolder.CreateFileAsync(SETTINGS_FILE, CreationCollisionOption.ReplaceExisting);
            var json = JsonConvert.SerializeObject(settings);
            await FileIO.WriteTextAsync(settingsFile, json);
        }

        public static async Task<RemoteSettings> LoadSettings()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var settingsFile = await storageFolder.GetFileAsync(SETTINGS_FILE);
            var json = await FileIO.ReadTextAsync(settingsFile);
            return JsonConvert.DeserializeObject<RemoteSettings>(json);
        }

        public static void UpdatePageTitle(string title)
        {
            var app = (App) Application.Current;
            app.RootViewModel.PageTitle = title;
        }


        public static string GetString(string key)
        {
            return TranslationHelper.GetString(key);
        }

    }
}