using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Common.Log
{
    public static class Log
    {
        public static void LogError(string entry)
        {
            var path = ConfigurationManager.AppSettings["LogFilePath"];
            var sr = new StreamWriter(path, append: true);
            sr.WriteLine(entry + "\n");
            sr.Close();
        }
    
    }
}
