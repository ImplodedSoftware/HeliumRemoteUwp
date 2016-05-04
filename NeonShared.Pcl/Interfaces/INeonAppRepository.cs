namespace NeonShared.Pcl.Interfaces
{
    public interface INeonAppRepository 
    {
        string Token { get; set; }
        string BaseUrl { get; set; }
        string GetString(string key);
    }
}
