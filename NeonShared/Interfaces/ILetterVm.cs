using System.Collections.Generic;
using System.Threading.Tasks;
using NeonShared.Types;

namespace NeonShared.Interfaces
{
    public interface ILetterVm
    {
        Task Populate(UwpViewTypes viewType, ViewParameters param);
        IEnumerable<LetterContainerItem> Letters { get; }
    }
}
