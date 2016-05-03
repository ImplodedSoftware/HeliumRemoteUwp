using System;
using System.Collections.Generic;
using System.Text;

namespace NeonShared.Interfaces
{
    public interface INeonAppRepository 
    {
        string Token { get; set; }
        string BaseUrl { get; set; }
    }
}
