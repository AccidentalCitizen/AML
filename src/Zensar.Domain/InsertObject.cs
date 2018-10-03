using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Domain
{
    public class InsertObject
    {
        private readonly IList<string> pipeDelimitedString;
        private string numericFieldsCode;
        public InsertObject(IList<string> pipeDelimitedString,string numericFieldsCode)
        {
            this.pipeDelimitedString = pipeDelimitedString;
            this.numericFieldsCode = numericFieldsCode;
        }

        public IList<string> GetInsertStrings(bool hasIndex)
        {
            var insertStrings = new List<string>();
            foreach(var str in pipeDelimitedString)
            {
                var valueArray = str.Split('|');
                var count = valueArray.Count(); // Since there is an extra Pipe at the end
                var retStr = "";
                if (hasIndex)
                {
                    retStr = ReturnString(valueArray, count, retStr,1);
                }
                else
                {
                    retStr = ReturnString(valueArray, count, retStr, 0);
                }
                insertStrings.Add(retStr);
            }
            return insertStrings;
        }

        private string ReturnString(string[] valueArray, int count, string retStr,int startPoint)
        {
            for (int i = startPoint; i < count; i++)
            {
                if (Intepret(i.ToString()))
                {
                    if (i == count - 1)
                    {
                        retStr += valueArray[i]==string.Empty?"0": valueArray[i];
                    }
                    else
                    {
                        retStr += valueArray[i] == string.Empty ? "0" + "," : valueArray[i] + ",";
                    }

                }
                else
                {
                    if (i == count - 1)
                    {
                        retStr += "'" + valueArray[i] + "'";
                    }
                    else
                    {
                        retStr += "'" + valueArray[i] + "'" + ",";
                    }

                }

            }

            return retStr;
        }

        private bool Intepret(string index)
        {
            var charList = numericFieldsCode.Split('|');

            if(charList.Contains(index))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
