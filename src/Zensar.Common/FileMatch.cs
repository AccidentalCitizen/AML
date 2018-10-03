using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AML.Common
{
    public static class FileMatch
    {
        public static string GetFullFileName(string directory, string fileName)
        {
            DirectoryInfo d = new DirectoryInfo(directory);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
            var flName = "";
            foreach (FileInfo file in Files)
            {
                var check = Regex.IsMatch(fileName, file.Name);
                if (check)
                {
                    flName = file.Name;
                    break;
                }
            }
            return flName;
        }

        public static IList<string> GetAllFilesWithinPath(string directory, string fileExtension)
        {
            var list = new List<string>();

            DirectoryInfo d = new DirectoryInfo(directory);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*."+ fileExtension); //Getting Text files

            foreach (FileInfo file in Files)
            {
                list.Add(file.Name);
            }

            return list;

        }
    }
}
