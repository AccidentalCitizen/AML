using System.Configuration;
using System;
using System.Data.SqlClient;

namespace AI.EXE.UploadDataToAMLTablesEXE
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connection test");
            var sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["AMLconnectionString"]);
            sqlConnection.Open();
            sqlConnection.Close();
            sqlConnection.Dispose();
            Console.WriteLine("Success!!");
            var dr = new AI.DependencyResolution.DependencyResolution(); 
            dr.ConstructContainer();
            //Console.WriteLine("Kindly specify {person} or {entity} files to be read into the AML DataBase:");
            //var code = Console.ReadLine();

            if (args[0] == "1")
            {
                var readerPerson = dr.RegisterLowLevelReader(ConfigurationManager.AppSettings["PathPerson"]);
                var resultPerson = readerPerson.GetData();

                var writerPerson = dr.RegisterLowLevelWriter(ConfigurationManager.AppSettings["PersonTable"]
                    , ConfigurationManager.AppSettings["Delimiter"].ToCharArray()[0]);
                writerPerson.WriteData(resultPerson);
            }
            if (args[0] == "2")
            {
                var readerEntity = dr.RegisterLowLevelReader(ConfigurationManager.AppSettings["PathEntity"]);
                var resultEntity = readerEntity.GetData();

                var writerEntity = dr.RegisterLowLevelWriter(ConfigurationManager.AppSettings["EntityTable"]
                    , ConfigurationManager.AppSettings["Delimiter"].ToCharArray()[0]);
                writerEntity.WriteData(resultEntity);
            }

        }
    }
}
