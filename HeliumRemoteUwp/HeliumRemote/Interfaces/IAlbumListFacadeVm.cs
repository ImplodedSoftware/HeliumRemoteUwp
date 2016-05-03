using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using HeliumRemote.Types;
using NeonShared.Types;

namespace HeliumRemote.Interfaces
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
