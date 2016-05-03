#if WINDOWS_UWP
using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using NeonShared.Interfaces;

namespace NeonShared.Classes
{

    public class NeonBaseVm : ViewModelBase
    {
        protected IWebService _webService;

        public NeonBaseVm(IWebService webService)
        {
            _webService = webService;
        }
    }
}
#endif
