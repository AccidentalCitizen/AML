using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Infrastructure.Connections.Interfaces
{

        public interface ILowLevelConnection
        {
            IList<string> LoadData(int rowsFrom, int rowsTo);
        }
   
}
