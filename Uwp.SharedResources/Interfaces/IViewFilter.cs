namespace Uwp.SharedResources.Interfaces
{
    public interface IViewFilter
    {
        void FilterData(string expr);
        void ClearFilter();
    }
}
