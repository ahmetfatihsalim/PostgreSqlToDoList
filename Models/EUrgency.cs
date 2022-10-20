using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum EUrgency
    {
        [Description("High")]
        High = 1,
        [Description("Medium")]
        Medium = 2,
        [Description("Low")]
        Low = 3,
    }
}
