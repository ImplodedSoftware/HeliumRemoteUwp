namespace Uwp.SharedResources.Helpers
{
    public class TranslationHelper
    {
        public static string GetFallbackString(string text)
        {
            if (text == null)
                return string.Empty;
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var s = loader.GetString(text);
            if (string.IsNullOrEmpty(s))
                s = text;
            return s;
        }
        public static string GetString(string text)
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            return loader.GetString(text);
        }
    }
}
