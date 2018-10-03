using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Infrastructure.Connections.Interfaces
{
    public interface IDataConnection<T>
    {
        T LoadData();
    }
}
