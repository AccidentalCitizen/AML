using AI.Calculators.Interfaces;
using AI.Domain;
using AI.Infrastructure.Readers.Interfaces;
using AML.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace AI.Calculators
{
    public class DowJonesFormatCalculator : IAMLFormat
    {
        IDataReaderPathAndFieldsParametric<IList<DjListOutputFieldsPerson>, IList<string>>
            dataReaderPathAndFieldsParametricPerson;
        IDataReaderPathAndFieldsParametric<IList<DjListOutputFieldsEntity>, IList<string>>
       dataReaderPathAndFieldsParametricEntity;
        public DowJonesFormatCalculator(
            IDataReaderPathAndFieldsParametric<IList<DjListOutputFieldsPerson>, IList<string>> 
            dataReaderPathAndFieldsParametricPerson
            , IDataReaderPathAndFieldsParametric<IList<DjListOutputFieldsEntity>, IList<string>> 
            dataReaderPathAndFieldsParametricEntity)
        {
            this.dataReaderPathAndFieldsParametricPerson = dataReaderPathAndFieldsParametricPerson;
            this.dataReaderPathAndFieldsParametricEntity = dataReaderPathAndFieldsParametricEntity;
        }
        public  void FormatPerson()
        {

            var personPath = ConfigurationManager.AppSettings["PersonReadPath"];
            var logWritePath = ConfigurationManager.AppSettings["LogPersonWritePath"];
            var outPersonFileWritePath = ConfigurationManager.AppSettings["OutPersonFileWritePath"];
            var path = personPath;
            var files = FileMatch.GetAllFilesWithinPath(path, "xml");
            var reader = dataReaderPathAndFieldsParametricPerson;
            Console.WriteLine("Read all file names from directory successfully.");

            foreach (var file in files)
            {
                var logWriter = new StreamWriter(logWritePath, append: true);
                Console.WriteLine("File:" + file);
                var res = reader.GetData(path + file,
                new List<string>() { });
                var list = reader.GetInStringFormat('|', res);
                var count = list.Count();
                var sr = new StreamWriter(outPersonFileWritePath, append: true);
                var lastLineValue = "";
                foreach (string g in list)
                {
                    sr.WriteLine(g);
                    Console.WriteLine(g);
                    lastLineValue = g;
                }
                logWriter.WriteLine("File {0} Number of records : {1} \n Last line value : {2}", file, count, lastLineValue);
                sr.Close();
                logWriter.Close();
            }
        }
        public  void FormatEntity()
        {

            var entityPath = ConfigurationManager.AppSettings["EntityReadPath"];
            var logWritePath = ConfigurationManager.AppSettings["LogEntityWritePath"];
            var outEntityFileWritePath = ConfigurationManager.AppSettings["OutEntityFileWritePath"];
            var path = entityPath;
            var files = FileMatch.GetAllFilesWithinPath(path, "xml");
            var reader = dataReaderPathAndFieldsParametricEntity;
            Console.WriteLine("Read all file names from directory successfully.");

            foreach (var file in files)
            {
                var logWriter = new StreamWriter(logWritePath, append: true);
                Console.WriteLine("File:" + file);
                var res = reader.GetData(path + file,
                new List<string>() { });
                var list = reader.GetInStringFormat('|', res);
                var count = list.Count();
                var sr = new StreamWriter(outEntityFileWritePath, append: true);
                var lastLineValue = "";
                foreach (string g in list)
                {
                    sr.WriteLine(g);
                    Console.WriteLine(g);
                    lastLineValue = g;
                }
                logWriter.WriteLine("File {0} Number of records : {1} \n Last line value : {2}", file, count, lastLineValue);
                sr.Close();
                logWriter.Close();
            }
        }
    }
}
