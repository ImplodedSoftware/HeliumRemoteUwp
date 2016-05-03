using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Neon.Api.Pcl.Models.Entities;

namespace HeliumRemote.Interfaces
{
    public interface IAlbumDetailsFacadeVm
    {
        Album Album { get; }
        Task Refresh(int id);
        CollectionViewSource Cvs { get; set; }
        int ImageSize { get; set; }
        Thickness ElementMargin { get; set; }
        double IconSize { get; set; }
        int RatingWidth { get; set; }
        void AdjustUiParts();
   }
}
