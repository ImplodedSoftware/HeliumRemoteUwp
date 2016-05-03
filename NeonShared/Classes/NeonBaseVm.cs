#if WINDOWS_UWP
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