using System.Collections.Generic;

namespace Uwp.SharedResources.Types
{
    public class GroupInfoList<T> : List<object>
    {
        public object Key { get; set; }
        public new IEnumerator<object> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}
