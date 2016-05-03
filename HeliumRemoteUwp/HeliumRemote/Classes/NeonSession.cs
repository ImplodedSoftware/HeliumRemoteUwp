namespace HeliumRemote.Classes
{
    public class NeonSession
    {
        public bool Instansiated { get; set; }

        private static NeonSession _instance;
        public static NeonSession Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NeonSession();
                return _instance;                
            }
        }
    }
}
