using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Uwp.SharedResources.Types;

namespace Uwp.SharedResources.Converters
{
    public class AlbumDetailsSelector : DataTemplateSelector
    {
        public DataTemplate TopTemplate { get; set; }
        public DataTemplate HeaderTemplate { get; set; }
        public DataTemplate TracksTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(System.Object item, DependencyObject container)
        {
            if (item is AlbumDetailTopCell)
            {
                return TopTemplate;
            }
            if (item is AlbumDetailTrackCell)
            {
                return TracksTemplate;
            }
            if (item is AlbumDetailHeaderCell)
            {
                return HeaderTemplate;
            }
            return null;
        }
    }
}
