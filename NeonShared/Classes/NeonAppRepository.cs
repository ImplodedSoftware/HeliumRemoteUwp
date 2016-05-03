using System;
using System.Collections.Generic;
using System.Text;
using NeonShared.Interfaces;

namespace NeonShared.Classes
{
    public class NeonAppRepository
    {
        private static NeonAppRepository _instance;
        public INeonAppRepository Repository { get; set; }

        public NeonAppRepository()
        {
            
        }

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
