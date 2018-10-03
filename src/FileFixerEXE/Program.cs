using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace FileFixerEXE
{
    class Program
    {
        static void Main(string[] args)
        {
            char delimiter = ConfigurationManager.AppSettings["Delimiter"].ToCharArray()[0];
            var dr = new Zensar.DependencyResolution.DependencyResolution(); // This object should be Static!!
            dr.ConstructContainer();
            var readerEntity = dr.RegisterLowLevelReader(ConfigurationManager.AppSettings["PathPerson"]);
            var resultEntity = readerEntity.GetData();
            var count = resultEntity.Count();
            RemoveAllEmptyRows(resultEntity);
            FixSanctionsDescription(delimiter, resultEntity, 100000); // Test.
            Console.WriteLine("About to write to file.."); 
            WriteToFile(resultEntity);
        }

        private static void WriteToFile(IList<string> resultEntity)
        {
            string writeLines = "";
            foreach (var row in resultEntity)
            {
                writeLines += row + "\n";
            }
            var sr = new StreamWriter(ConfigurationManager.AppSettings["PathPersonResolved"]
                ,append:true);
            sr.Write(writeLines);
            sr.Close();
        }

        private static void FixSanctionsDescription(char delimiter, IList<string> resultEntity, int count)
        {
            int cnt = 0;
            int writeCnt = 0;
            int rem = 1;
            for (int i = 0; i < count; i++)
            {
                if (resultEntity[cnt].Split(delimiter).Count() < 5)
                {
                    resultEntity[cnt - 1] = resultEntity[cnt - 1] + resultEntity[cnt];
                    resultEntity.RemoveAt(cnt);
                    cnt--;
                }

                var div = Math.DivRem(i, 10000, out rem);
                if (rem == 0)
                {
                    Console.WriteLine("Fixed 10 000 :" + resultEntity[cnt] + "\n");
                    writeCnt++;
                }
                cnt++;
            }
        }

        private static void RemoveAllEmptyRows(IList<string> resultEntity)
        {
            int cnt = 0;
            foreach (var row in resultEntity)
            {
                if (row == "")
                {
                    resultEntity.RemoveAt(cnt);
                }
                cnt++;
            }
        }
    }
}
