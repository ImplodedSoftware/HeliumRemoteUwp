using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using NeonShared.Types;

namespace UwpSharedViews.Interfaces
{
    public interface ILetterFacadeVm
    {
        Task Populate(ViewParameters param);
        ObservableCollection<LetterContainerItem> Letters { get; }
        void OnSelectionChanged(object sender, SelectionChangedEventArgs e);
        UwpViewTypes ViewType { get; set; }
    }
}
