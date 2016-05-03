using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HeliumRemote.Types;
using NeonShared.Helpers;
using Newtonsoft.Json;

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
        public async static Task<bool> SettingsExists()
        {
            //var localSettings = ApplicationData.Current.LocalSettings;
            //var value = localSettings.Values[NEON_SETTINGS];
            //return value != null;

            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                var foundFile = await storageFolder.GetFileAsync(SETTINGS_FILE);
                return foundFile != null;
            }
            catch (FileNotFoundException)
            {
                return false;
            }


            //Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var lstfiles = await storageFolder.GetFilesAsync(CommonFileQuery.OrderByName);
            //var foundfile = lstfiles.FirstOrDefault(x => x.Name == SETTINGS_FILE);
            //return foundfile != null;
        }

        public async static Task SaveSettings(RemoteSettings settings)
        {
            //var json = JsonConvert.SerializeObject(settings);
            //ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            //localSettings.Values[NEON_SETTINGS] = json;

            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile settingsFile = await storageFolder.CreateFileAsync(SETTINGS_FILE, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            var json = JsonConvert.SerializeObject(settings);
            await Windows.Storage.FileIO.WriteTextAsync(settingsFile, json);

            //Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFile settingsFile = await storageFolder.CreateFileAsync(SETTINGS_FILE, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            //var json = JsonConvert.SerializeObject(settings);
            //await Windows.Storage.FileIO.WriteTextAsync(settingsFile, json);
        }

        public async static Task<RemoteSettings> LoadSettings()
        {
            //var localSettings = ApplicationData.Current.LocalSettings;
            //var value = localSettings.Values[NEON_SETTINGS];
            //var json = (string)value;
            //return JsonConvert.DeserializeObject<RemoteSettings>(json);
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile settingsFile = await storageFolder.GetFileAsync(SETTINGS_FILE);
            var json = await Windows.Storage.FileIO.ReadTextAsync(settingsFile);
            return JsonConvert.DeserializeObject<RemoteSettings>(json);
        }

        public static Frame ContentFrame
        {
            get
            {
                var app = (App) Application.Current;
                return app.ContentFrame;
            }
        }

        public static void UpdatePageTitle(string title)
        {
            var app = (App)Application.Current;
            app.RootViewModel.PageTitle = title;
        }

        public static int ActiveId
        {
            get { return ((App) Application.Current).ActiveId; }
        }

        public static string GetMonthName(int month)
        {            
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }

        public static string GetString(string key)
        {
            return TranslationHelper.GetString(key);
        }
        public static string GetRatingName(int rating)
        {
            switch (rating)
            {
                case NeonConstants.HELIUM_RATING_0:
                    return GetString("Rating0");
                case NeonConstants.HELIUM_RATING_0_5:
                    return GetString("Rating1");
                case NeonConstants.HELIUM_RATING_1:
                    return GetString("Rating2");
                case NeonConstants.HELIUM_RATING_1_5:
                    return GetString("Rating3");
                case NeonConstants.HELIUM_RATING_2:
                    return GetString("Rating4");
                case NeonConstants.HELIUM_RATING_2_5:
                    return GetString("Rating5");
                case NeonConstants.HELIUM_RATING_3:
                    return GetString("Rating6");
                case NeonConstants.HELIUM_RATING_3_5:
                    return GetString("Rating7");
                case NeonConstants.HELIUM_RATING_4:
                    return GetString("Rating8");
                case NeonConstants.HELIUM_RATING_4_5:
                    return GetString("Rating9");
                case NeonConstants.HELIUM_RATING_5:
                    return GetString("Rating10");
                default:
                    return GetString("Rating0");
            }
        }

    }
}
