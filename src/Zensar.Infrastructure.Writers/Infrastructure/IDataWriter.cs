using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Infrastructure.Writers.Infrastructure
{
    public interface IDataWriter<P>
    {
        void WriteData(P data);
    }

    public interface IDataWriterWithReturnType<P,Q>
    {
        Q WriteData(P data);
    }
}
