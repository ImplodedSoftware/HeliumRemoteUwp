using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Uwp.SharedResources.Types;

namespace Uwp.SharedResources.Converters
{
    public class ArtistDetailSelector : DataTemplateSelector
    {
        public DataTemplate TopTemplate { get; set; }
        public DataTemplate HeaderTemplate { get; set; }
        public DataTemplate AlbumDetailTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(System.Object item, DependencyObject container)
        {
            if (item is ArtistDetailTopCell)
            {
                return TopTemplate;
            }
            if (item is ArtistDetailAlbumCell)
            {
                return AlbumDetailTemplate;
            }
            if (item is ArtistDetailHeaderCell)
            {
                return HeaderTemplate;                    
            }
            return null;
        }
    }
}
