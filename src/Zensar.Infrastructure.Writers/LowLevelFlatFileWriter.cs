using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Common.Log;
using AI.Infrastructure.Writers.Infrastructure;

namespace AI.Infrastructure.Writers
{
    public class LowLevelFlatFileWriter : IDataWriter<IList<string>>
    {
        private string tableName;
        private char delimiter;
        private int linesCount = 0;
        private string insertString;
        public LowLevelFlatFileWriter(string tableName)
        {
            this.tableName = tableName;
            this.delimiter = ConfigurationManager.AppSettings["Delimiter"].ToCharArray()[0];
        }
        public void WriteData(IList<string> data)
        {
            var sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["AMLconnectionString"]);
            sqlConnection.Open();
            Console.WriteLine("Writer : Connection opened successfully!!");
            var writeCnt = 0;
            var cntrPrint = 1;
            int rem = 1;

            foreach (var str in data)
            {
                linesCount++;
                insertString = "INSERT INTO " + tableName + " VALUES (";
                var rowValues = str.Split(delimiter);
                var count = rowValues.Count();
                int cntr = 0;
                foreach (var rw in rowValues)
                {
                    if (cntr == (count - 1))
                    {
                        insertString += "'" + rw.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ") + "'";
                    }
                    else
                    {
                        insertString += "'" + rw.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ") + "',";
                        //}

                        cntr++;
                    }
                }
                SqlCommand cmd = new SqlCommand(insertString + ")", sqlConnection);

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Lines written : {0}", linesCount);
                }
                            catch (Exception e)
                {
                    Console.WriteLine("Error writing : {0} {1}", linesCount, e.ToString());
                    Log.LogError(insertString + ")");
                }

            var div = Math.DivRem(cntrPrint, 10000, out rem);
                if (rem == 0)
                {
                    Console.WriteLine(writeCnt + " 10 000 Written!!\n");
                    writeCnt++;
                }
                cntrPrint++;
            }
            sqlConnection.Close();
            sqlConnection.Dispose();
        }       
    }
}

