using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HeliumRemote.Types;
using UwpSharedViews.Types;

namespace HeliumRemote.Converters
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
