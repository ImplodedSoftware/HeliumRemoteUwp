using System.Collections.Generic;
using System.Threading.Tasks;
using NeonShared.Pcl.Types;

namespace NeonShared.Pcl.Interfaces
{
    public interface ILetterVm
    {
        Task Populate(UwpViewTypes viewType, ViewParameters param);
        IEnumerable<LetterContainerItem> Letters { get; }
    }
}
