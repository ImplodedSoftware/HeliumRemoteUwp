using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeonShared.Types;

namespace NeonShared.Types
{
    public class ViewParameters
    {
        public string Letter { get; set; }
        public int Value { get; set; }
        public UwpViewTypes ViewType { get; set; }
        public int ParentValue { get; set; }
    }
}
