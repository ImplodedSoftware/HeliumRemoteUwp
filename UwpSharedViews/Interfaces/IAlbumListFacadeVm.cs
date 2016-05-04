using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using NeonShared.Types;
using UwpSharedViews.Types;

namespace UwpSharedViews.Interfaces
{
    public interface IAlbumListFacadeVm
    {
        ObservableCollection<AlbumContainer> Albums { get; }
        Task Refresh(ViewParameters param);
        void GridView_OnTapped(object sender, TappedRoutedEventArgs e);
        Thickness ElementMargin { get; set; }
        void AdjustUiParts();
    }
}
