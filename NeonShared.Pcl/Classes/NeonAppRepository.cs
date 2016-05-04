using NeonShared.Pcl.Interfaces;

namespace NeonShared.Pcl.Classes
{
    public class NeonAppRepository
    {
        private static NeonAppRepository _instance;

        public INeonAppRepository Repository { get; set; }

        public static NeonAppRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NeonAppRepository();
                }
                return _instance;
            }
        }
    }
}