namespace UwpSharedViews.Interfaces
{
    public interface IViewFilter
    {
        void FilterData(string expr);
        void ClearFilter();
    }
}
