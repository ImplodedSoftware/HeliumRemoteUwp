using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Neon.Api.Pcl.Models.Entities;
using NeonShared.Types;

namespace HeliumRemote.Interfaces
{
    public interface IArtistListFacadeVm
    {
        ObservableCollection<Artist> Artists { get; }
        Task Refresh(ViewParameters parameters);
        void OnSelectionChanged(object sender, SelectionChangedEventArgs e);
        bool IsBusy { get; set; }
    }
}
