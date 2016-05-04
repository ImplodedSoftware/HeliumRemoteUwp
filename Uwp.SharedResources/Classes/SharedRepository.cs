using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.SharedResources.Interfaces;

namespace Uwp.SharedResources.Classes
{
    public class SharedRepository
    {
        private static SharedRepository _instance;

        public ISharedRepository Repository { get; set; }

        public static SharedRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SharedRepository();
                }
                return _instance;
            }
        }
    }
}
