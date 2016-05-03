using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using HeliumRemote.Types;
using NeonShared.Types;

namespace HeliumRemote.Interfaces
{
    public interface ITracksVmFacade
    {
        ObservableCollection<TrackContainer> Tracks { get; }
        Task Refresh(ViewParameters parameters);
        void OnTapped(object sender, TappedRoutedEventArgs e);
        Task OnLoaded(object sender, RoutedEventArgs e);

    }
}
