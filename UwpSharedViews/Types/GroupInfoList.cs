using System.Collections.Generic;

namespace UwpSharedViews.Types
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
