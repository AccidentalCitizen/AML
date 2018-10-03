
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AI.Infrastructure.Connections.Interfaces;
using AI.Infrastructure.Readers.Interfaces;
using System.Configuration;

namespace AI.Infrastructure.Readers
{
    public class LowLevelFlatFileReader : IDataReader<IList<string>>
    {
        private readonly ILowLevelConnection con;
        int rowFrom; int rowTo; string location;
        public LowLevelFlatFileReader(ILowLevelConnection con)
        {
            this.con = con;
        }

        public IList<string> GetData()
        {
            IList<string> result = new List<string>();
            IList<string> readerResult = new List<string>();
            var flatFileData =
               result = con.LoadData(0, 30000000);
            Console.WriteLine("About to return data.");
            var rows = result.Count();
            return result;
        }

        private static void MakeAllDecimalValuesCastable(IList<string> result, IList<string> readerResult)
        {
            foreach (var str in result)
            {
                var split = str.Split('|');
                var cnt1 = split.Count();

                var temp = "";
                foreach (var subString in split)
                {
                    var subStr = subString;
                    if (subStr != "")
                    {
                        if (subStr.Substring(0, 1) == ".")
                        {
                            subStr = "0" + subStr;
                            temp += subStr + "|";
                        }
                        else
                        {
                            temp += subStr + "|";
                        }
                    }
                    else
                    {
                        temp += subStr + "|";
                    }
                }
                var length = temp.Length;
                temp = temp.Substring(0, length - 1);
                readerResult.Add(temp);
            }
        }
    }
}
