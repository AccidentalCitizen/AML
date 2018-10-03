using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Infrastructure.Connections.Interfaces;

namespace AI.Infrastructure.Connections
{
    public class FlatFileLowLevelConnection : ILowLevelConnection
    {
        private string location;
        public FlatFileLowLevelConnection(string location)
        {
            this.location = location;
        }
        public IList<string> LoadData(int rowsFrom, int rowsTo)
        {
            var sr = new StreamReader(location);
            var streamList = new List<string>();
            int cntr = 1;
            int writeCnt = 0;
            int rem = 1;
            while (!sr.EndOfStream && cntr <= (rowsTo + 1))
            {
                var tmp = sr.ReadLine(); //This  is done to skip the first line as the first line containg headers.
                if (cntr >= (rowsFrom) && cntr <= (rowsTo))
                {
                    streamList.Add(tmp.Replace('\'', '-').Replace("\0",string.Empty).Trim());
                    var div = Math.DivRem(cntr, 10000, out rem);
                    if (rem == 0)
                    {
                        Console.WriteLine(writeCnt + " 10 000 \n" + tmp);
                        writeCnt++;
                    }
                }
                cntr++;
            }
            return streamList;
           
        }        
    }

    public class ConnectionReaderException : Exception
    {
        public ConnectionReaderException(string message) : base(message)
        {
        }
    }


}
