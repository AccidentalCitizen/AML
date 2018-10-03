using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Formatting.Interfaces
{
    interface IFormat<T>
    {
        T Format();
    }
}
