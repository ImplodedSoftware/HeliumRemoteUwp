using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Neon.Api.Pcl.Models.Entities;

namespace UwpSharedViews.Interfaces
{
    public interface IArtistDetailsFacadeVm
    {
        Artist Artist { get; }
        Task Refresh(int id);
        CollectionViewSource Cvs { get; set; }
        void UIElement_OnRightTapped(object sender, RightTappedRoutedEventArgs e);
        void OnTapped(object sender, TappedRoutedEventArgs e);
        Thickness ElementMargin { get; set; }
        int RatingWidth { get; set; }
        Task AdjustUiParts(int artistId);
    }
}
